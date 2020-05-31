using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Core.Entities
{
    public class School
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public Guid? ParentId { get; set; }
        public School? ParentSchool { get; set; }

        public Guid OwnerId { get; set; }        
        public User Owner { get; set; }  
        
        public SchoolPayment SchoolPayment { get; set; }
        public List<School>  ChildSchools { get; set; }
        public List<Class> Classes { get; set; }
    }
}
