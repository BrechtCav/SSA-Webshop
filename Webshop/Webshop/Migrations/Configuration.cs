namespace Webshop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Web.Configuration;
    using Webshop.Models;
    using Webshop_Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Webshop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //Toevoegen van account

            string roleAdmin = "Administrator";
            IdentityResult roleResult;
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(roleAdmin))
            {
                roleResult = roleManager.Create(new IdentityRole(roleAdmin));
            }


            if (!context.Users.Any(u => u.Email.Equals("brecht.caveye@student.howest.be")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Caveye",
                    Firstname = "Brecht",
                    Email = "brecht.caveye@student.howest.be",
                    UserName = "brecht.caveye@student.howest.be",
                    Address = "Reepstraat 125",
                    City = "Sint-Gillis-Waas",
                    Zipcode = "9170"
                };
                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, roleAdmin);
            }

            //Toevoegen van info uit txt bestand.

            string basePath = WebConfigurationManager.AppSettings["bin"];
            List<Webshop_Models.OperatingSystem> OS = ReadOS(basePath + "Os.txt");
            foreach (Webshop_Models.OperatingSystem os in OS)
            {
                context.OperatingSystem.AddOrUpdate(Os => Os.Name, os); 
            }
            context.SaveChanges();
            List<ProgrammingFramework> Frameworks = ReadFramework(basePath + "ProgrammingFramework.txt");
            foreach (ProgrammingFramework framework in Frameworks)
            {
                context.Framework.AddOrUpdate(f => f.Name, framework);
            }
            context.SaveChanges();
            List<Device> toestellen = ReadDevices(basePath + "Devices.txt", context);
            foreach (Device toestel in toestellen)
            {
                context.Device.AddOrUpdate(t => t.Name, toestel);
            }
            context.SaveChanges();
        }
        private List<Webshop_Models.OperatingSystem> ReadOS(string path)
        {
            List<Webshop_Models.OperatingSystem> OS = new List<Webshop_Models.OperatingSystem>();
            StreamReader sr = new StreamReader(path);
            string lijn = sr.ReadLine();
            while(lijn != null)
            {
                string[] stukken = lijn.Split(';');
                Webshop_Models.OperatingSystem os = new Webshop_Models.OperatingSystem();
                os.Name = stukken[1];
                OS.Add(os);
                lijn = sr.ReadLine();
            }
            sr.Close();
            return OS;
        }
        private List<ProgrammingFramework> ReadFramework(string path)
        {
            List<ProgrammingFramework> Frameworks = new List<ProgrammingFramework>();
            StreamReader sr = new StreamReader(path);
            string lijn = sr.ReadLine();
            while (lijn != null)
            {
                string[] stukken = lijn.Split(';');
                ProgrammingFramework framework = new ProgrammingFramework();
                framework.Name = stukken[1];
                Frameworks.Add(framework);
                lijn = sr.ReadLine();
            }
            sr.Close();
            return Frameworks;
        }
        private List<Device> ReadDevices(string path, ApplicationDbContext context)
        {

            List<Device> toestellen = new List<Device>();
            StreamReader sr = new StreamReader(path);
            string lijn = sr.ReadLine();
            lijn = sr.ReadLine();
            while (lijn != null)
            {
                string[] stukken = lijn.Split(';');
                Device toestel = new Device();
                toestel.Name = stukken[1];
                toestel.Price = Convert.ToInt32(stukken[2]);
                toestel.RentPrice = Convert.ToInt32(stukken[3]);
                toestel.Stock = Convert.ToInt32(stukken[4]);
                toestel.Picture = stukken[5];
                toestel.OperatingSystems = getOSFromString(stukken[6], context);
                toestel.Frameworks = getFWFromString(stukken[7], context);
                toestel.Description = stukken[8];
                toestellen.Add(toestel);
                lijn = sr.ReadLine();
            }
            sr.Close();
            return toestellen;
        }
        private List<ProgrammingFramework> getFWFromString(string lijn, ApplicationDbContext context)
        {
            List<ProgrammingFramework> FW = new List<ProgrammingFramework>();
            string[] ids = lijn.Split('-');
            for (int i = 0; i < ids.Length; i++)
            {
                int id = Convert.ToInt32(ids[i]);
                var query = (from f in context.Framework where f.ID == id select f);
                FW.Add(query.SingleOrDefault<ProgrammingFramework>());
            }
            return FW;
        }
        private List<Webshop_Models.OperatingSystem> getOSFromString(string lijn, ApplicationDbContext context)
        {
            List<Webshop_Models.OperatingSystem> OS = new List<Webshop_Models.OperatingSystem>();
            string[] ids = lijn.Split('-');
            for (int i = 0; i < ids.Length; i++)
            {
                int id = Convert.ToInt32(ids[i]);
                var query = (from b in context.OperatingSystem where b.ID == id select b);
                OS.Add(query.SingleOrDefault<Webshop_Models.OperatingSystem>());
            }
            return OS;

        }
    }
}
