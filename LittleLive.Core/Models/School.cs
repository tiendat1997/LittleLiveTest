using LittleLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Core.Models
{
    public class School
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public School? ParentSchool { get; set; }
        public SchoolPayment PaymentType { get; set; }
        public List<Class> Classes { get; set; }
    }
}
