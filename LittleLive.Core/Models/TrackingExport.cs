using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Core.Models
{
    public class BaseTrackingExport
    {
        public string ChildName { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public DateTimeOffset TimeCheckIn { get; set; }
        public DateTimeOffset TimeCheckOut { get; set; }
    }
}
