using Microsoft.EntityFrameworkCore;

namespace EvoPC.Models.Entities
{
    public partial class EvoPCContext : DbContext
    {
       
        public EvoPCContext(DbContextOptions<EvoPCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersProcesor> OrdersProcesors { get; set; }
        public virtual DbSet<Procesor> Procesors { get; set; }
        public virtual DbSet<ProcesorSpecialTag> ProcesorSpecialTags { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<SocketType> SocketTypes { get; set; }
        public virtual DbSet<SpecialTag> SpecialTags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.CommentTitle)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Procesor)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ProcesorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Procesor");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Feedback_Users");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CustomerAddress)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CustomerPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrdersProcesor>(entity =>
            {
                entity.HasKey(e => new { e.ProcesorId, e.OrderId });

                entity.ToTable("OrdersProcesor");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersProcesors)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrdersProcesor_Orders");

                entity.HasOne(d => d.Procesor)
                    .WithMany(p => p.OrdersProcesors)
                    .HasForeignKey(d => d.ProcesorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersProcesor_Procesor");
            });

            modelBuilder.Entity<Procesor>(entity =>
            {
                entity.ToTable("Procesor");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Pret).HasColumnType("numeric(20, 2)");

                entity.HasOne(d => d.Socket)
                    .WithMany(p => p.Procesors)
                    .HasForeignKey(d => d.SocketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procesor_ProcesorTypes");
            });

            modelBuilder.Entity<ProcesorSpecialTag>(entity =>
            {
                entity.HasKey(e => new { e.SpecialTagId, e.ProcesorId })
                    .HasName("PK_ProcesorSpecialTag");

                entity.HasOne(d => d.Procesor)
                    .WithMany(p => p.ProcesorSpecialTags)
                    .HasForeignKey(d => d.ProcesorId)
                    .HasConstraintName("FK_ProcesorSpecialTags_Procesor");

                entity.HasOne(d => d.SpecialTag)
                    .WithMany(p => p.ProcesorSpecialTags)
                    .HasForeignKey(d => d.SpecialTagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcesorSpecialTags_SpecialTags");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.NumeRol)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SocketType>(entity =>
            {
                entity.ToTable("SocketType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SpecialTag>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UK_UserName")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.IsActiv)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
