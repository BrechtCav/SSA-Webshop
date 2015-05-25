using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.ViewModel;
using Webshop_Models;
using Webshop_BusinessLayer.Repositories;
using Webshop_BusinessLayer.Services;

namespace Webshop.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ToevoegenController : Controller
    {
        /*public IGenericRepository<ProgrammingFramework> frameworkRepository;
        public IGenericRepository<Webshop.Models.OperatingSystem> OSRepository;
        public DeviceRepository DeviceRepository;*/


        private IProductService ps;

        public ToevoegenController(IProductService ps)
        {
            this.ps = ps;
        }

        // GET: Toevoegen
        [HttpGet]
        public ActionResult Index()
        {
            AddDeviceVM advm = new AddDeviceVM();
            advm.OperatingSystems = new List<Webshop_Models.OperatingSystem>(ps.GetAllOperatingSystems());
            advm.Frameworks = new List<ProgrammingFramework>(ps.GetAllFrameworks());
            return View(advm);
        }
        [HttpPost]
        public ActionResult Index(AddDeviceVM advm)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    advm.NewDevice.OperatingSystems = new List<Webshop_Models.OperatingSystem>();
                    if(advm.SelectedOS != null)
                    {
                        foreach (int id in advm.SelectedOS)
                            advm.NewDevice.OperatingSystems.Add(ps.GetOSById(id));
                    }
                    advm.NewDevice.Frameworks = new List<ProgrammingFramework>();
                    if (advm.SelectedFrameworks != null)
                    { 
                        foreach (int id in advm.SelectedFrameworks)
                            advm.NewDevice.Frameworks.Add(ps.GetFrameworkById(id));
                    }
                    ps.AddDevice(advm.NewDevice);
                }
                return RedirectToAction("Index", "Cataloog");
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult UploadPictures(int deviceId)
        {
            ViewBag.DeviceID = deviceId;
            return View();
        }
        [HttpPost]
        public ActionResult UploadPictures(int deviceId, HttpPostedFileBase picture)
        {
            if(picture != null)
            {
                ps.UpdatePicture(deviceId, picture);
            }
            return RedirectToAction("Index", "Cataloog");
        }
    }
}