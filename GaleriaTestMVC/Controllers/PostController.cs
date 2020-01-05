using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GaleriaTestMVC.Models;
using GaleriaTestMVC.Service;
using PagedList;

namespace GaleriaTestMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly PostsService postsService = new PostsService();
        private readonly CommentariesService commentariesService = new CommentariesService();

        [HttpGet]
        public ActionResult Index(int? page)
        {
            IEnumerable<Posts> items;
            try
            {
                items = postsService.GetAll();
            }
            catch (Exception)
            {
                return View("Error");
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Show(int? id)
        {
            ViewPostWithCommentaries vp = null;
            try
            {
                if (id is null)
                {
                    return RedirectToAction("Index", "Post");
                }
                if (TempData.ContainsKey("msg"))
                {
                    ViewBag.Msg = TempData["msg"];
                }
                vp = new ViewPostWithCommentaries();
                vp.Post = postsService.FindById(Convert.ToInt32(id));
                vp.Commentaries = commentariesService.GetAllByIdPost(Convert.ToInt32(id));
            }
            catch (Exception)
            {
                return View("Error");
            }
            //
            return View(vp);
        }
        // 
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                if (Session["idUser"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Store(HttpPostedFileBase image, FormCollection fc)
        {
            Posts posts;
            try
            {
                if (Session["idUser"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                //
                int idUser = Convert.ToInt16(Session["idUser"]);
                string rootPath = Server.MapPath("/Images");
                string userPath = rootPath + "/" + idUser.ToString();
                //
                // 
                if (!Directory.Exists(userPath))
                {
                    Directory.CreateDirectory(userPath);
                }
                //
                string nombreImg = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
                string pic = Path.GetFileName(nombreImg + Path.GetExtension(image.FileName));
                string finalPath = Path.Combine(userPath, pic);
                //
                image.SaveAs(finalPath);
                //
                posts = new Posts();
                posts.Title = fc["title"].Trim().ToString();
                posts.Description = fc["description"].Trim().ToString();
                posts.UrlImage = $"/Images/{idUser}/{nombreImg + Path.GetExtension(image.FileName)}";
                posts.Creation = DateTime.Now;
                posts.UserId = idUser;
                //
                postsService.Create(posts);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            //
            TempData["msg"] = "Post Creado!";
            return RedirectToAction("MyPosts", "User");
        }

    }
}