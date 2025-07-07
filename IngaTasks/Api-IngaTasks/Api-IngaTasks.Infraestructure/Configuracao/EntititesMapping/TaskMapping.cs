using Api_IngaTasks.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Configuracao.EntititesMapping
{
    public class TaskMapping : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable(nameof(TaskEntity));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.CreatedAt)
                .IsRequired();
            builder.Property(c => c.DeletedAt)
                .IsRequired(false);
            builder.Property(c => c.UpdatedAt)
                .IsRequired(false);
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(maxLength:1000000);
            builder.HasOne<Project>(c => c.Project)
                .WithMany(c => c.Tasks)
                .HasForeignKey(c => c.ProjectId);
        }
    }
}