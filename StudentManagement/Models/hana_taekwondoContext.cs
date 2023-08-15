using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models
{
    public partial class hana_taekwondoContext : DbContext
    {
        public hana_taekwondoContext()
        {
        }

        public hana_taekwondoContext(DbContextOptions<hana_taekwondoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ClassTimeTable> ClassTimeTables { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentClass> StudentClasses { get; set; } = null!;
        public virtual DbSet<StudentTimeTable> StudentTimeTables { get; set; } = null!;
        public virtual DbSet<TimeTable> TimeTables { get; set; } = null!;
        public virtual DbSet<Tuition> Tuitions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Desc).HasColumnName("desc");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ClassTimeTable>(entity =>
            {
                entity.HasKey(e => new { e.ClassId, e.TimeTableId })
                    .HasName("class_time_table_pk");

                entity.ToTable("class_time_table");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.TimeTableId).HasColumnName("time_table_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("dob");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("full_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");

                entity.Property(e => e.ParentName)
                    .HasMaxLength(255)
                    .HasColumnName("parent_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(1)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<StudentClass>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ClassId })
                    .HasName("student_class_pk");

                entity.ToTable("student_class");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_class___fk_class");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_class___fk_student");
            });

            modelBuilder.Entity<StudentTimeTable>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.TimeTableId })
                    .HasName("student_time_table_pk");

                entity.ToTable("student_time_table");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.TimeTableId).HasColumnName("time_table_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentTimeTables)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_time_table___fk_student");

                entity.HasOne(d => d.TimeTable)
                    .WithMany(p => p.StudentTimeTables)
                    .HasForeignKey(d => d.TimeTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_time_table___fk_time");
            });

            modelBuilder.Entity<TimeTable>(entity =>
            {
                entity.ToTable("time_table");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Slot).HasColumnName("slot");

                entity.Property(e => e.WeekDay)
                    .HasMaxLength(255)
                    .HasColumnName("week_day");
            });

            modelBuilder.Entity<Tuition>(entity =>
            {
                entity.ToTable("tuition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActualAmount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("actual_amount");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("due_date");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.PaidDate)
                    .HasColumnType("datetime")
                    .HasColumnName("paid_date");

                entity.Property(e => e.StudentId).HasColumnName("student_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
