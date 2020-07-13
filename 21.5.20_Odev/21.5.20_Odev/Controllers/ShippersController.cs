using _21._5._20_Odev.Models;
using _21._5._20_Odev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _21._5._20_Odev.Controllers
{
    public class ShippersController : Controller
    {
        NORTHWNDEntities db;
        static int shipperID;
        public ShippersController()
        {
            db = new NORTHWNDEntities();
        }
        public ActionResult Shippers()
        {
            List<ShipperViewModel> shippers = db.Shippers.Select(a => new ShipperViewModel { ShipperID = a.ShipperID, CompanyName = a.CompanyName, Phone = a.Phone }).ToList();
            return View(shippers);
        }
        public ActionResult Select(int id)
        {
            ShipperViewModel shipper = db.Shippers.Where(a => a.ShipperID == id).Select(a => new ShipperViewModel { ShipperID = a.ShipperID, CompanyName = a.CompanyName, Phone = a.Phone }).SingleOrDefault();
            if (shipper != null)
            {
                //TempData["ID"] = shipper.ShipperID;
                shipperID = shipper.ShipperID;
                return Json(shipper, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int id)
        {
            Shipper shipper = db.Shippers.Find(id);
            
            if (shipper != null)
            {
                db.Shippers.Remove(shipper);
                db.SaveChanges();
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Add(ShipperViewModel shipper)
        {
            try
            {
                Shipper s = new Shipper();
                s.CompanyName = shipper.CompanyName;
                s.Phone = shipper.Phone;
                db.Shippers.Add(s);
                db.SaveChanges();
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Update(Shipper s)
        {
            try
            {
                Shipper shipper = db.Shippers.Find(shipperID);
                //Shipper s = (Shipper)TempData["tempShipper"];
                //Shipper shipper = db.Shippers.Find(id);
                shipper.CompanyName = s.CompanyName;
                shipper.Phone = s.Phone;
                db.SaveChanges();
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}