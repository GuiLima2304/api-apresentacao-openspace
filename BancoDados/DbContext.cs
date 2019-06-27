using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OpenSpace.BancoDados
{
    public partial class DbOpenSpace : DbContext
    {
        public DbOpenSpace()
        {
        }

        public DbOpenSpace(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apresentacao> Apresentacao { get; set; }
        public virtual DbSet<Arquivo> Arquivo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=OPENSPACE;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Apresentacao>(entity =>
            {
                entity.Property(e => e.Descricao).HasColumnType("text");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Apresentacao)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apresenta__Usuar__25869641");
            });

            modelBuilder.Entity<Arquivo>(entity =>
            {
                entity.Property(e => e.Link)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Apresentacao)
                    .WithMany(p => p.Arquivo)
                    .HasForeignKey(d => d.ApresentacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Arquivo__Apresen__286302EC");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
