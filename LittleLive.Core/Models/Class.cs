using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Core.Models
{
    public class Class
    { 
        public int Id { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }        
        public Guid TeacherId { get; set; }
        public int SchoolId { get; set; }
        public User Teacher { get; set; }
        public School School { get; set; }
    }
}
