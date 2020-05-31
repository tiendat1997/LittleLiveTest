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
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public Entities.Role Role { get; set; }
        public Entities.SubscriptionType SubscriptionType { get; set; }
        public Entities.LicensePlan LicensePlan { get; set; }
    }
}
