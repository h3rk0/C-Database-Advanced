using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public StudentSystemContext()
        {

        }
        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=.;Database=StudentSystem;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Student
            builder.Entity<StudentCourse>().HasKey(a => new { a.StudentId,a.CourseId});
            builder.Entity<Student>().
                HasMany(s => s.HomeworkSubmissions).
                WithOne(hs => hs.Student).
                HasForeignKey(hs => hs.StudentId);
            builder.Entity<Course>().
                HasMany(c => c.Resources).
                WithOne(r => r.Course).
                HasForeignKey(r => r.CourseId);
            builder.Entity<Course>().
                HasMany(c => c.HomeworkSubmissions).
                WithOne(hs => hs.Course).
                HasForeignKey(hs => hs.CourseId);
            builder.Entity<StudentCourse>().
                HasOne(sc => sc.Student).
                WithMany(s => s.StudentCourses).
                HasForeignKey(sc => sc.StudentId);
            builder.Entity<StudentCourse>().
                HasOne(sc => sc.Course).
                WithMany(c => c.StudentCourses).
                HasForeignKey(sc => sc.CourseId);
        }
    }
}
