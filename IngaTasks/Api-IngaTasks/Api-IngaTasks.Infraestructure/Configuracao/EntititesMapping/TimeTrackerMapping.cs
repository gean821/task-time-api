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
    public class TimeTrackerMapping : IEntityTypeConfiguration<TimeTracker>
    {
        public void Configure(EntityTypeBuilder<TimeTracker> builder)
        {
            builder.ToTable(nameof(TimeTracker));
            
            builder.HasKey(c => c.Id);

            builder.Property(c => c.StartDate)
                .IsRequired();

            builder.Property(c => c.EndDate)
                .IsRequired(true);

            builder.Property(c => c.TimeZoneId)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(c => c.TaskId)
                .IsRequired();

            builder.Property(c=>c.CollaboratorId)
                .IsRequired(false);
            builder.Property(c => c.CreatedAt).
                IsRequired();

            builder.Property(c => c.UpdatedAt);

            builder.Property(c => c.DeletedAt)
                .IsRequired(false);

            builder.HasOne<TaskEntity>(c => c.Task)
                .WithMany(c => c.TimeTrackers)
                .HasForeignKey(c => c.TaskId);  
        }
    }
}
