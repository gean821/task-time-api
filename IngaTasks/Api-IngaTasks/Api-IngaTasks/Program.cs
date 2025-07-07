using Api_IngaTasks.Application.Dtos.Task;
using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Application.Validators.Task;
using Api_IngaTasks.Infraestructure.Configuracao;
using Api_IngaTasks.Infraestructure.Interfaces;
using Api_IngaTasks.Infraestructure.Repository;
using Api_IngaTasks.Services;
using Api_IngaTasks.Services.DependecyInjection;
using Api_IngaTasks.Services.Interfaces;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_IngaTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //Cria o builder, que é onde você registra serviços (injeção de dependência, banco de dados, autenticação, etc.).
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services
                .AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<AppDbContext>();
            builder.AddAuthenticationJwt();
            // Registra todos os Validators do Assembly onde está o DTO
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(TaskCreateDtoValidator));
            // builder.Services.AddMassTransit(x =>
            //{
            //  x.UsingRabbitMq((ctx, cfg) =>
            //cfg.Host("rabbitmq-ingaTasks", 5672, "/",h =>
            //{
            //  h.Username("root");
            // h.Password("maringaBRAZIL12");
            //}));
            //});
            // Add services to the container.
            builder.Services.AddTransient<ITokenService,TokenService >();
            builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
            builder.Services.AddTransient<ICollaboratorService, CollaboratorService>();
            builder.Services.AddTransient<ITimeTrackerService, TimeTrackerService>();
            builder.Services.AddTransient<IProjectService, ProjectService>();
            builder.Services.AddTransient<ITasksService, TasksService>();
            builder.Services.AddTransient<ITimeTrackerService, TimeTrackerService>();
            builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
            builder.Services.AddTransient<ITimeTrackerRepository, TimeTrackerRepository>();
            builder.Services.AddTransient<ITasksRepository, TasksRepository>();
            builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
            builder.Services.AddTransient<ITasksRepository, TasksRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build(); //Monta o app final, com todos os serviços e configurações já registradas.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();           ///Exibe o Swagger apenas se estiver em ambiente de desenvolvimento.
                app.UseSwaggerUI();
                app.MapSwagger().RequireAuthorization();
            }
            
            app.UseHttpsRedirection(); //Redireciona automaticamente chamadas HTTP para HTTPS.
            app.UseAuthentication();
            app.UseAuthorization(); //Ativa o filtro de autorização, mas não funciona sozinho — você ainda precisa configurar JWT/Auth.
            app.MapIdentityApi<ApplicationUser>();
            app.MapControllers(); //Mapeia os controllers com base nas rotas ([Route("api/[controller]")], etc.).
            app.Run();
        }
    }
}
