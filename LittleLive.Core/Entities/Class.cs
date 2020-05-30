using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Core.Entities
{
    public class Class
    { 
        public Guid Id { get; set; }        
        public string Name { get; set; }        
        public Guid TeacherId { get; set; }
        public Guid SchoolId { get; set; }
        public User Teacher { get; set; }
        public School School { get; set; }
    }
}
