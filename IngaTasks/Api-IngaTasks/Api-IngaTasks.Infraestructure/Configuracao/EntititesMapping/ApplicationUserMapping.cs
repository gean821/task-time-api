using Api_IngaTasks.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Configuracao.EntitiesMapping
{
    public class ApplicationUserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(c => c.CreatedAt)
                .IsRequired();
            builder.Property(c=> c.UpdatedAt)
                .IsRequired(false);
            builder.Property(c => c.DeletedAt)
                .IsRequired(false);

            builder.HasOne<Collaborator>(c=> c.Collaborator)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<Collaborator>(c => c.ApplicationUserId);
        }
    }
}
