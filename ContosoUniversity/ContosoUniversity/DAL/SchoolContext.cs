﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Instructor>()
                        .HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);
            modelBuilder.Entity<Course>()
                        .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                        .Map(t => t.MapLeftKey("CourseID")
                        .MapRightKey("InstructorID")
                        .ToTable("CourseInstructor"));
            modelBuilder.Entity<Department>()
                        .HasOptional(x => x.Administrator);
            //base.OnModelCreating(modelBuilder);
        }
    }
}