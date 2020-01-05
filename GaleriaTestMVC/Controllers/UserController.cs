using GaleriaTestMVC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GaleriaTestMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult MyPosts(int? page)
        {
            IEnumerable<Posts> items = null;
            try
            {
                if (Session["idUser"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (TempData.ContainsKey("msg"))
                {
                    ViewBag.Msg = TempData["msg"];
                }
                //
                int id = Convert.ToInt16(Session["idUser"]);
                using (GaleriaTestDBEntities context = new GaleriaTestDBEntities())
                {
                    items = context.Posts.Where(p => p.UserId == id).ToList();
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }
    }
}