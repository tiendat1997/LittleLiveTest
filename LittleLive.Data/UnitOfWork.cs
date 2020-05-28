using LittleLive.Core;
using LittleLive.Core.Repositories;
using LittleLive.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LittleLiveDbContext _context;
        private IUserRepository _userRepository;
        private ISchoolRepository _schoolRepository;
        private ITrackRepository _trackRepository;
        private IClassRepository _classRepository;

        public UnitOfWork(LittleLiveDbContext context)
        {
            this._context = context;
        }

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);
        public ISchoolRepository Schools => _schoolRepository = _schoolRepository ?? new SchoolRepository(_context);
        public IClassRepository Classes => _classRepository = _classRepository ?? new ClassRepository(_context);
        public ITrackRepository Tracks => _trackRepository = _trackRepository ?? new TrackRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
