using System;

namespace LittleLive.Core.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string ChildName { get; set; }                
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
        public DateTimeOffset TimeCheckIn { get; set; }
        public DateTimeOffset TimeCheckOut { get; set; }
    }
}
