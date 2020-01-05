using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GaleriaTestMVC.Models;

namespace GaleriaTestMVC.Controllers
{
    public class AccountController : Controller
    {
        
        [Authorize]
        [HttpGet]
        public ActionResult Update()
        {
            Users users = new Users();
            try
            {
                if (Session["idUser"] == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    //
                    // Buscar Datos Usuario
                    int idUser = Convert.ToInt16(Session["idUser"]);
                    using (GaleriaTestDBEntities context = new GaleriaTestDBEntities())
                    {
                        users = context.Users.Where(u => u.Id == idUser).FirstOrDefault();
                    }
                    //
                    // 

                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            //
            return View(users);
            
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(FormCollection fc)
        {
            Users users = new Users();
            try
            {
                if (Session["idUser"] == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    if(fc != null)
                    {
                        int idUser = Convert.ToInt16(Session["idUser"]);

                        using (GaleriaTestDBEntities context = new GaleriaTestDBEntities())
                        {
                            users = context.Users.Where(u => u.Id == idUser).FirstOrDefault();
                            users.Name = fc["name"].Trim().ToString();
                            users.LastName = fc["lastname"].Trim().ToString();
                            users.Birthdate = Convert.ToDateTime(fc["birthdate"].Trim().ToString());
                            users.Email = fc["email"].Trim().ToString();
                            context.SaveChanges();
                        }
                        TempData["msg"] = "Datos Actualizados!";
                    }
                    else
                    {
                        TempData["msg"] = "No Hubo Cambios.!";
                    }
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction("MyPosts", "User");
        }

        // Form
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                if (TempData.ContainsKey("msg"))
                {
                    ViewBag.Msg = TempData["msg"];
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            return View();
        }
        // Store new user
        [HttpPost]
        public ActionResult Store(FormCollection fc)
        {
            try
            {
                if (fc != null)
                {
                    string password = fc["password"].Trim().ToString();
                    string password2 = fc["password2"].Trim().ToString();
                    if (!password.Equals(password2))
                    {
                        TempData["msg"] = "contraseñas no coinciden";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        // Guardar Datos
                        //
                        string passEncr = Seguridad.Encriptar(password);
                        Users users = new Users();
                        users.Name = fc["name"].Trim().ToString();
                        users.LastName = fc["lastname"].Trim().ToString();
                        users.Birthdate = Convert.ToDateTime(fc["birthdate"]);
                        users.Email = fc["email"].ToString();
                        users.Password = passEncr;
                        using (GaleriaTestDBEntities context = new GaleriaTestDBEntities())
                        {
                            context.Users.Add(users);
                            context.SaveChanges();
                        }
                        TempData["msg"] = "Cuenta se creo con exito!";
                    }
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction("Login");


        }

        // Login
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                if (TempData.ContainsKey("msg"))
                {
                    ViewBag.Msg = TempData["msg"];
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            try
            {
                if (fc != null)
                {
                    string email = fc["email"].Trim().ToString();
                    string password = fc["password"].Trim().ToString();
                    string passEncr = Seguridad.Encriptar(password);
                    int idUser = 0;
                    Users users = null;
                    //
                    using (GaleriaTestDBEntities context = new GaleriaTestDBEntities())
                    {
                        users = context.Users.Where(u => u.Email == email && u.Password == passEncr).FirstOrDefault();
                        idUser = users.Id;
                    }
                    if (users != null)
                    {
                        FormsAuthentication.SetAuthCookie(email, true);
                        Session["idUser"] = idUser;
                        Session["email"] = email;
                        return RedirectToAction("MyPosts", "User");
                    }
                    else
                    {
                        TempData["msg"] = "Credenciales Incorrectas";
                    }
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            try
            {
                if (Session["idUser"] == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            return RedirectToAction("Login");
        }
    }
}