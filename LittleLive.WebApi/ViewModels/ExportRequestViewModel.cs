using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi.ViewModels
{
    public class TeacherActivityExportRequest
    {
        public Guid ClassId { get; set; }
        public Guid UserId { get; set; }
    }

    public class SchoolOwnerActivityExportRequest
    {
        public Guid SchoolId { get; set; }
        public Nullable<Guid> ClassId { get; set; }
        public Guid UserId { get; set; }
    }

    public class HQOwnerActivityExportRequest
    {
        public Guid SchoolId { get; set; }
        public Guid UserId { get; set; }
    }
}
