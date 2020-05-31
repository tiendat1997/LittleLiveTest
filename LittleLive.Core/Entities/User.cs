using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LittleLive.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public LicensePlan LicensePlan { get; set; }
        public List<Class> Classes { get; set; }
    }
}
