using Bogus;
using Bogus.Extensions.Italy;
using LittleLive.Core.Models;
using LittleLive.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleLive.Data
{
    public class LittleLiveDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Class> Classes { get; set; }
        public LittleLiveDbContext(DbContextOptions<LittleLiveDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new SchoolConfiguration());
            builder.ApplyConfiguration(new TrackConfiguration());
            builder.ApplyConfiguration(new ClassConfiguration());

            ApplySeeder(builder); 
        }

        protected void ApplySeeder(ModelBuilder builder)
        {
            Faker<User> fakeUserRules = new Faker<User>()
                .CustomInstantiator(u => new User { Id = Guid.NewGuid() })
                .RuleFor(u => u.Role, f => f.PickRandom<Role>())
                .RuleFor(u => u.Password, f => f.Random.Number(100000, 999999).ToString())
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName())                
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName());

            List<User> mockUsers = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                mockUsers.Add(fakeUserRules.Generate());
            }

            List<User> teachers = mockUsers.Where(u => u.Role.Equals(Role.Teacher)).ToList();
            List<User> hqOnwers = mockUsers.Where(u => u.Role.Equals(Role.HQOwner)).ToList();
            List<User> schoolOwners = mockUsers.Where(u => u.Role.Equals(Role.SchoolOwner)).ToList();

            Faker<School> fakeHQSchoolRules = new Faker<School>()
                .CustomInstantiator(f => new School { Id = Guid.NewGuid() })
                .RuleFor(s => s.Code, f => $"SCHOOL{f.UniqueIndex}")
                .RuleFor(s => s.OwnerId, f => f.PickRandom<Guid>(hqOnwers.Select(u => u.Id).ToList()))
                .RuleFor(s => s.PaymentType, f => f.PickRandom<PaymentType>())
                .RuleFor(s => s.Name, f => f.Company.CompanyName());

            List<School> hqSchools = new List<School>();
            for (int i = 0; i < 10; i++)
            {
                hqSchools.Add(fakeHQSchoolRules.Generate());
            }

            builder.Entity<User>().HasData(mockUsers);
            builder.Entity<School>().HasData(hqSchools);
        }
    }
}
