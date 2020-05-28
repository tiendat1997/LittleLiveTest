using LittleLive.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISchoolRepository Schools { get; }
        IUserRepository Users { get; }
        IClassRepository Classes { get; }
        ITrackRepository Tracks { get; }
        Task<int> CommitAsync();
    }
}
