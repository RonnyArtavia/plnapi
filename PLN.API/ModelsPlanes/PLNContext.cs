using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PLN.API.ModelsPlanes
{
    public partial class PLNContext : DbContext
    {
        public PLNContext()
        {
        }

        public PLNContext(DbContextOptions<PLNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Distelec> Distelecs { get; set; }
        public virtual DbSet<Padron> Padrons { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<PlanesPiloto> PlanesPilotos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=kbsoft.database.windows.net;Initial Catalog=PLN;Persist Security Info=False;User ID=kb;Password=Kabata2021.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Distelec>(entity =>
            {
                entity.HasKey(e => e.Codele);

                entity.ToTable("Distelec");

                entity.Property(e => e.Codele)
                    .ValueGeneratedNever()
                    .HasColumnName("CODELE");

                entity.Property(e => e.Canton)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CANTON");

                entity.Property(e => e.Distrito)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("DISTRITO");

                entity.Property(e => e.Provincia)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PROVINCIA");
            });

            modelBuilder.Entity<Padron>(entity =>
            {
                entity.HasKey(e => e.Cedula);

                entity.ToTable("PADRON");

                entity.Property(e => e.Cedula)
                    .ValueGeneratedNever()
                    .HasColumnName("CEDULA");

                entity.Property(e => e.Apellido1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("APELLIDO1");

                entity.Property(e => e.Apellido2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("APELLIDO2");

                entity.Property(e => e.Codelec).HasColumnName("CODELEC");

                entity.Property(e => e.Fechacaduc).HasColumnName("FECHACADUC");

                entity.Property(e => e.Junta)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("JUNTA");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Relleno)
                    .HasMaxLength(1)
                    .HasColumnName("RELLENO");

                entity.HasOne(d => d.CodelecNavigation)
                    .WithMany(p => p.Padrons)
                    .HasForeignKey(d => d.Codelec)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PADRON_Distelec");
            });

            modelBuilder.Entity<Plane>(entity =>
            {
                entity.HasKey(e => e.Telefono);

                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.Property(e => e.Cedula).HasMaxLength(50);

                entity.Property(e => e.FechaAceptaTerminos).HasColumnType("datetime");

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(255);

                entity.Property(e => e.Plan).HasMaxLength(50);

                entity.Property(e => e.Telco).HasMaxLength(50);

                entity.Property(e => e.Ubicacion).HasMaxLength(255);
            });

            modelBuilder.Entity<PlanesPiloto>(entity =>
            {
                entity.HasKey(e => e.Telefono);

                entity.ToTable("PlanesPiloto");

                entity.Property(e => e.Telefono).ValueGeneratedNever();

                entity.Property(e => e.Cedula).HasMaxLength(250);

                entity.Property(e => e.Correo).HasMaxLength(250);

                entity.Property(e => e.FechaAceptaTerminos).HasColumnType("datetime");

                entity.Property(e => e.Modificado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.NombrePadron).HasMaxLength(500);

                entity.Property(e => e.Plan).HasMaxLength(250);

                entity.Property(e => e.Telco).HasMaxLength(250);

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
