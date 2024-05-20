using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    public class PerfilMap:IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("PERFIL");
            builder.HasKey(p=> p.Id);

            builder.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(50).IsRequired();
            builder.HasIndex(p => p.Nome).IsUnique();
        }
    }
}
