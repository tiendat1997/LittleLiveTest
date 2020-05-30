using System;
using System.Threading.Tasks;

namespace LittleLive.Core.Services
{
    public interface ITrackService
    {
        Task<byte[]> ExportActivityForTeacher(Guid ExportActivityForTeacher, Guid classId);
        Task<byte[]> ExportActivityForSchoolOnwer(Guid userId, Guid schoolId, Nullable<Guid> classId);
    }
}
