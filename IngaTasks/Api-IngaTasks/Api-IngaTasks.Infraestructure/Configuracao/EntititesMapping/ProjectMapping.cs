﻿using Api_IngaTasks.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Configuracao.EntititesMapping
{
    public class ProjectMapping : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(c => c.CreatedAt)
                .IsRequired();
            builder.Property(c=> c.UpdatedAt)
                .IsRequired(false);    
            builder.Property(c=> c.DeletedAt)
                .IsRequired(false);
        }
    }
}
