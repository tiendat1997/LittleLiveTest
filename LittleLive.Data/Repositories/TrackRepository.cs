using AutoMapper;
using LittleLive.Core.Entities;
using LittleLive.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = LittleLive.Core.Models;

namespace LittleLive.Data.Repositories
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        public TrackRepository(LittleLiveDbContext context)
            : base(context)
        {
        }

        private LittleLiveDbContext LittleLiveDbContext
        {
            get { return Context as LittleLiveDbContext; }
        }

        public async Task<IEnumerable<Track>> GetFullInformation(Guid teacherId, Guid classId)
        {
            IEnumerable<Track> efResult = await this.DbSet.Where(c => c.ClassId.Equals(classId) && c.Class.TeacherId.Equals(teacherId))
                                                    .Include("Class")
                                                    .Include("Class.Teacher")
                                                    .Include("Class.School")
                                                    .ToListAsync();
            return efResult;
        }

        public async Task<IEnumerable<Track>> GetFullInformationByIds(List<Guid> classIds)
        {
            IEnumerable<Track> efResult = await this.DbSet.Where(c => classIds.Contains(c.ClassId))
                                                    .Include("Class")
                                                    .Include("Class.Teacher")
                                                    .Include("Class.School")
                                                    .ToListAsync();
            return efResult;
        }      
    }
}
