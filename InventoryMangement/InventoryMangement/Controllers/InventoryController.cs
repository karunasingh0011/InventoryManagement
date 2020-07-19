using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryMangement.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace InventoryMangement.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
           
        }
        public ActionResult GetInventoryDetail()
        {
            InventoryDBHandle dbhandle = new InventoryDBHandle();
            ModelState.Clear();
            return PartialView(dbhandle.GetInventory());
        }
        [HttpPost]
        public ActionResult Index(InvClass smodel, HttpPostedFileBase file)
        {
            try
            {
                
                    InventoryDBHandle sdb = new InventoryDBHandle();
                  
                    if (sdb.AddInventory(smodel,file))
                    {
                        ViewBag.Message = "Inventory Details Added Successfully";
                        ModelState.Clear();
                    }
                
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteInventory(int id)
        {
            try
            {
                InventoryDBHandle sdb = new InventoryDBHandle();
                if (sdb.DeleteInventory(id))
                {
                    ViewBag.AlertMsg = "Inventory Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
          

            InventoryDBHandle sdb = new InventoryDBHandle();
            return View(sdb.GetInventory().Find(smodel => smodel.Id == id));
        }




    }
}