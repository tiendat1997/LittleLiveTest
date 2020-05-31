using Bogus;
using Bogus.Extensions.Italy;
using LittleLive.Core.Entities;
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
        public DbSet<Country> Country { get; set; }
        public LittleLiveDbContext(DbContextOptions<LittleLiveDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new SchoolConfiguration());
            builder.ApplyConfiguration(new TrackConfiguration());
            builder.ApplyConfiguration(new ClassConfiguration());

            ApplySeeder(builder); 
        }

        protected void ApplySeeder(ModelBuilder builder)
        {
            // Fake Countries 
            Faker<Country> fakeCountryRules = new Faker<Country>()
                .CustomInstantiator(u => new Country { Id = Guid.NewGuid() })
                .RuleFor(u => u.Code, f => f.Address.CountryCode())
                .RuleFor(u => u.Name, f => f.Address.Country());

            List<Country> mockCountries = new List<Country>();
            for (int i = 0; i < 5; i++)
            {
                mockCountries.Add(fakeCountryRules.Generate());
            }

            // Fake Users
            Faker<User> fakeUserRules = new Faker<User>()
                .CustomInstantiator(u => new User { Id = Guid.NewGuid() })
                .RuleFor(u => u.Role, f => f.PickRandom<Role>())
                .RuleFor(u => u.SubscriptionType, f => f.PickRandom<SubscriptionType>())
                .RuleFor(u => u.LicensePlan, f => f.PickRandom<LicensePlan>())
                .RuleFor(u => u.Password, f => f.Random.Number(100000, 999999).ToString())
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName())                
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName())
                .RuleFor(u => u.CountryId, f => f.PickRandom<Guid>(mockCountries.Select(u => u.Id).ToList()));

            List<User> mockUsers = new List<User>();
            for (int i = 0; i < 100; i++)
            {
                mockUsers.Add(fakeUserRules.Generate());
            }

            List<User> teachers = mockUsers.Where(u => u.Role.Equals(Role.Teacher)).ToList();
            List<User> hqOnwers = mockUsers.Where(u => u.Role.Equals(Role.HQOwner)).ToList();
            List<User> schoolOwners = mockUsers.Where(u => u.Role.Equals(Role.SchoolOwner)).ToList();

            // Fake Head Quarter
            Faker<School> fakeHQSchoolRules = new Faker<School>()
                .CustomInstantiator(f => new School { Id = Guid.NewGuid() })
                .RuleFor(s => s.Code, f => $"SCHOOL{f.UniqueIndex}")
                .RuleFor(s => s.OwnerId, f => f.PickRandom<Guid>(hqOnwers.Select(u => u.Id).ToList()))
                .RuleFor(s => s.SchoolPayment, f => f.PickRandom<SchoolPayment>())
                .RuleFor(s => s.Name, f => f.Company.CompanyName());

            List<School> hqSchools = new List<School>();
            for (int i = 0; i < 10; i++)
            {
                hqSchools.Add(fakeHQSchoolRules.Generate());
            }

            // Fake Children Schools
            Faker<School> fakeSchoolRules = new Faker<School>()
                .CustomInstantiator(f => new School { Id = Guid.NewGuid() })
                .RuleFor(s => s.Code, f => $"SCHOOL{f.UniqueIndex}")
                .RuleFor(s => s.ParentId, f => f.PickRandom<Guid>(hqSchools.Select(h => h.Id).ToList()))
                .RuleFor(s => s.OwnerId, f => f.PickRandom<Guid>(schoolOwners.Select(u => u.Id).ToList()))
                .RuleFor(s => s.SchoolPayment, f => f.PickRandom<SchoolPayment>())
                .RuleFor(s => s.Name, f => f.Company.CompanyName());

            List<School> schools = new List<School>();
            for (int i = 0; i < 50; i++)
            {
                schools.Add(fakeSchoolRules.Generate());
            }

            List<School> allSchools = new List<School>();
            allSchools.AddRange(hqSchools);
            allSchools.AddRange(schools);

            // Fake Classes
            Faker<Class> fakeClassRules = new Faker<Class>()
                .CustomInstantiator(f => new Class { Id = Guid.NewGuid() })
                .RuleFor(s => s.TeacherId, f => f.PickRandom<Guid>(teachers.Select(u => u.Id).ToList()))
                .RuleFor(s => s.Name, f => f.Company.CompanyName())
                .RuleFor(s => s.SchoolId, f => f.PickRandom<Guid>(allSchools.Select(s => s.Id).ToList()));

            List<Class> classes = new List<Class>();
            for (int i = 0; i < 500; i++)
            {
                classes.Add(fakeClassRules.Generate());
            }

            // Fake Track
            int trackIndex = 1;
            Faker<Track> fakeTrackRules = new Faker<Track>()
                .CustomInstantiator(f => new Track {Id = trackIndex++ })
                .RuleFor(s => s.ChildName, f => f.Name.FullName())
                .RuleFor(s => s.ClassId, f => f.PickRandom<Guid>(classes.Select(c => c.Id).ToList()))
                .RuleFor(s => s.TimeCheckIn, f => f.Date.RecentOffset().ToOffset(TimeSpan.Zero))
                .RuleFor(s => s.TimeCheckOut, f => f.Date.SoonOffset().ToOffset(TimeSpan.Zero));

            List<Track> tracks = new List<Track>();
            for (int i = 0; i < 2000; i++)
            {
                tracks.Add(fakeTrackRules.Generate());
            }

            builder.Entity<Country>().HasData(mockCountries);
            builder.Entity<User>().HasData(mockUsers);
            builder.Entity<School>().HasData(allSchools);
            builder.Entity<Class>().HasData(classes);
            builder.Entity<Track>().HasData(tracks);
        }
    }
}