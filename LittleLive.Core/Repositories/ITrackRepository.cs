using LittleLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLive.Core.Repositories
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<IEnumerable<Track>> GetFullInformation(Guid teacherId, Guid classId);
        Task<IEnumerable<Track>> GetFullInformationByIds(List<Guid> classIds);
    }
}
