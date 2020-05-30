using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Entities.Role Role { get; set; }
    }
}
