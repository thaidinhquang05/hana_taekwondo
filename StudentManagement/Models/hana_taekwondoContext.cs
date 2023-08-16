using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<ClassTimetable> ClassTimetables { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentClass> StudentClasses { get; set; } = null!;
        public virtual DbSet<StudentTimetable> StudentTimetables { get; set; } = null!;
        public virtual DbSet<Timetable> Timetables { get; set; } = null!;
        public virtual DbSet<Tuition> Tuitions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server = localhost; database = hana_taekwondo; uid = sa; password = 123456789;TrustServerCertificate=True");
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

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("due_date");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");
            });

            modelBuilder.Entity<ClassTimetable>(entity =>
            {
                entity.HasKey(e => new { e.ClassId, e.TimeTableId })
                    .HasName("class_time_table_pk");

                entity.ToTable("class_timetable");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.TimeTableId).HasColumnName("time_table_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassTimetables)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("class_time_table___fk_class");

                entity.HasOne(d => d.TimeTable)
                    .WithMany(p => p.ClassTimetables)
                    .HasForeignKey(d => d.TimeTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("class_time_table___fk_time");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("slot");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasColumnName("desc");
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

            modelBuilder.Entity<StudentTimetable>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.TimeTableId })
                    .HasName("student_time_table_pk");

                entity.ToTable("student_timetable");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.TimeTableId).HasColumnName("time_table_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_at");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentTimetables)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_time_table___fk_student");

                entity.HasOne(d => d.TimeTable)
                    .WithMany(p => p.StudentTimetables)
                    .HasForeignKey(d => d.TimeTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_time_table___fk_time");
            });

            modelBuilder.Entity<Timetable>(entity =>
            {
                entity.ToTable("timetable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SlotId).HasColumnName("slot_id");

                entity.Property(e => e.WeekDay)
                    .HasMaxLength(255)
                    .HasColumnName("week_day");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Timetables)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("time_table_slot_id_fk");
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
