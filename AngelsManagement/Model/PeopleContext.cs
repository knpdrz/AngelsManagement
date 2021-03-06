﻿using AngelsManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AngelsManagement.Globals;
using Microsoft.EntityFrameworkCore;

namespace AngelsManagement.Model
{
    public class PeopleContext : DbContext
    {
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Guardian> Guardians { get; set; }

        public PeopleContext() :base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configuring join entity type
            modelBuilder.Entity<VolStu>()
                .HasKey(vs => new { vs.VolunteerId, vs.StudentId });

            //configuring many to many volunteer-student relationship
            modelBuilder.Entity<VolStu>()
                .HasOne(vs => vs.Volunteer)
                .WithMany(v => v.VolunteerStudents)
                .HasForeignKey(vs => vs.VolunteerId);

            modelBuilder.Entity<VolStu>()
                .HasOne(vs => vs.Student)
                .WithMany(s => s.VolunteerStudents)
                .HasForeignKey(vs => vs.StudentId);

            //same for guardian-Student relationship
            modelBuilder.Entity<StuPar>()
                .HasKey(sp => new { sp.StudentId, sp.GuardianId });

            modelBuilder.Entity<StuPar>()
                .HasOne(sp => sp.Student)
                .WithMany(s => s.StudentGuardians)
                .HasForeignKey(sp => sp.StudentId);

            modelBuilder.Entity<StuPar>()
                .HasOne(sp => sp.Guardian)
                .WithMany(p => p.StudentGuardians)
                .HasForeignKey(sp => sp.GuardianId);
        }


    }
}
