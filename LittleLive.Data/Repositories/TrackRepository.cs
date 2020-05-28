using LittleLive.Core.Models;
using LittleLive.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Data.Repositories
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        public TrackRepository(LittleLiveDbContext context)
            : base(context)
        { }

        private LittleLiveDbContext LittleLiveDbContext
        {
            get { return Context as LittleLiveDbContext; }
        }
    }
}
