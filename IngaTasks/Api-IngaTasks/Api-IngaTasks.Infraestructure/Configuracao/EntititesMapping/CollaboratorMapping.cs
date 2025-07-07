using Api_IngaTasks.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Configuracao.EntititesMapping
{
    public class CollaboratorMapping : IEntityTypeConfiguration<Collaborator>
    {
        public void Configure(EntityTypeBuilder<Collaborator> builder)
        {
            builder.ToTable(nameof(Collaborator));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(c => c.CreatedAt)
                .IsRequired();
            builder.Property(c => c.DeletedAt)
                .IsRequired(false);
            builder.Property(c => c.UpdatedAt)
                .IsRequired(false);

            builder.HasOne<ApplicationUser>(c => c.ApplicationUser)
                .WithOne(c => c.Collaborator)
                .HasForeignKey<Collaborator>(c => c.ApplicationUserId);
        }
    }
}
