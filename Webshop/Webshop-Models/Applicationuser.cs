using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace Webshop_Models
{
    public class Applicationuser : IdentityUser
    {
        public String Name { get; set; }
        public String Firstname { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Zipcode { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Applicationuser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
