using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GaleriaTestMVC.Models;
using GaleriaTestMVC.Service;

namespace GaleriaTestMVC.Controllers
{
    public class CommentaryController : Controller
    {
        private readonly CommentariesService commentariesService = new CommentariesService();
       
        [Authorize]
        [HttpPost]
        public ActionResult Store(FormCollection fc)
        {
            Commentaries comm = null;
            try
            {
                if (Session["idUser"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (fc is null)
                {
                    return RedirectToAction("Index", "Post");
                }
                int idUser = Convert.ToInt32(Session["idUser"]);
                //
                comm = new Commentaries();
                comm.Commentary = fc["commentary"].Trim().ToString();
                comm.Creation = DateTime.Now;
                comm.PostId = Convert.ToInt32(fc["idPost"].Trim().ToString());
                comm.UserId = idUser;
                //
                commentariesService.Create(comm);
                //
                TempData["msg"] = "Comentario Ingresado!";
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            return RedirectToAction("Show", "Post", new { id = comm.PostId });
        }

    }
}