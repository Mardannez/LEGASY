using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEGASY.Models;
using System.Data.Entity.Core.Objects;
using LEGASY.Encripta;
using System.Data;


namespace LEGASY.Controllers
{
    public class LoginController : PrincipalController
    {
        // GET: Login

        //private HttpCookie CookieSesion { get; set; } //Cookies para usar en todo el sistema
        //private HttpCookie CookieToken { get; set; } //Cookies para usar en todo el sistema
        //EncryptMD5 UsingEncript = new EncryptMD5(); //Clase para Encriptar en MD5

        public ActionResult Index(string respuesta)
        {
            if (respuesta != null)
            {
                ViewBag.Respuesta = respuesta;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Usuario, string Password)
        {
            try
            {
                string Key = UsingEncript.Encriptar(Password);
                
                var Desencripta = "";

                var DatosUsuario = (from user in db.Users where user.entryUserName == Usuario && user.entryPassword == Key select user).FirstOrDefault();
                
                if(DatosUsuario != null)
                {
                    Desencripta = UsingEncript.DesEncriptar(DatosUsuario.entryPassword);


                    if (Desencripta != Password)
                    {
                        ViewBag.Error = "Usuario o Contraseña Incorrectas";

                        return RedirectToAction("Index", new { @respuesta = ViewBag.Error });
                    }
                    else
                    {

                        Session["User"] = DatosUsuario;
                        this.CreaSesion(Convert.ToInt32(DatosUsuario.entryCode));
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ViewBag.Error = "Usuario o Contraseña Incorrectas";

                    return RedirectToAction("Index", new { @respuesta = ViewBag.Error });
                }
                 
  
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View("Index", new { @respuesta = ViewBag.Error });
            }
           
        }
      
        public ActionResult CerrarSistema()
        {
            //Cerrar sesion en tabla de servicio

            Session.RemoveAll();
            Session.Abandon();
            return Salir();
            //return RedirectToAction("Index", "Login");
        }





    }
}