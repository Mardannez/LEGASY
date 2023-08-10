using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEGASY.Models;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Configuration;
using System.IO;
using LEGASY.Encripta;
using System.Data.Entity;

namespace LEGASY.Controllers
{
    public class PrincipalController : Controller
    {
        protected LegasyEntities db = new LegasyEntities();
        public HttpCookie CookieSesion { get; set; } //Cookies para usar en todo el sistema
        public HttpCookie CookieToken { get; set; } //Cookies para usar en todo el sistema
        public EncryptMD5 UsingEncript = new EncryptMD5(); //Clase para Encriptar en MD5

        // GET: Principal
        protected Users UsuarioActual
        {
            get
            {
                return this.usuarioactual;
            }
        }
        Users usuarioactual = new Users();

        protected string MensajeNoPermiso
        {
            get { return "Usted no tiene suficientes permisos para ejecutar esta funcion. Por favor consulte al administrador del sistema."; }
        }

        public virtual bool ValidaSesion(int RefPermisoId = 0)
        {
           
                this.CookieSesion = base.HttpContext.Request.Cookies.Get("Sesion");
                this.CookieToken = base.HttpContext.Request.Cookies.Get("Token");
                if (this.CookieSesion != null && this.CookieToken != null)
                {
                    int sesionid = 0;
                    string token = this.CookieToken.Value;
                    if (Int32.TryParse(this.CookieSesion.Value, out sesionid) && token != null)
                    {
                        ObjectParameter r = new ObjectParameter("Respuesta", typeof(bool));
                        ObjectParameter u = new ObjectParameter("Usuario", typeof(int));
                        this.db.SP_ValidaSesion(sesionid, token, this.Request.UserHostAddress, r, u);
                        if (Convert.ToBoolean(r.Value))
                        {

                            this.PerfilSesion(Convert.ToInt32(u.Value));
                            if (this.UsuarioActual != null)
                            {
                                ViewBag.UsuarioActual = this.UsuarioActual;
                                if (db.Database.Connection.State == ConnectionState.Open) { db.Database.Connection.Close(); }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                             
                            
 
                        }
                    }
                }
                if (db.Database.Connection.State == ConnectionState.Open) { db.Database.Connection.Close(); }
                return false;  
        }

        public virtual bool CreaSesion(int UsuarioId)
        {
            DateTime tiempo = DateTime.Now.AddMinutes(1);



            this.CookieSesion = new HttpCookie("Sesion");
            this.CookieToken = new HttpCookie("Token");
            Random ObjToken = new Random();
            int token = ObjToken.Next(1, 999999999);
            Sesions lasesion = db.Sesions.Add(new Sesions()
            {
                entryCodeUser = UsuarioId,
                Estado = 0,
                Creacion = DateTime.Now,
                Expiracion = tiempo,
                HostCliente = base.Request.UserHostAddress,
                DispositivoCliente = "Unknow",
                PlataformaCliente = base.Request.Browser.Platform,
                NavegadorCliente = base.Request.Browser.Browser,
                Key = UsingEncript.Encriptar(token.ToString()),
                Token = UsingEncript.Encriptar(base.Request.UserHostAddress + "-" + token)
            });
            db.SaveChanges();
            this.CookieSesion.Value = lasesion.entryCode.ToString();
            this.CookieSesion.Expires = lasesion.Expiracion.Value;
            this.CookieToken.Value = lasesion.Token;
            this.CookieToken.Expires = lasesion.Expiracion.Value;
            base.HttpContext.Response.Cookies.Add(this.CookieSesion);
            base.HttpContext.Response.Cookies.Add(this.CookieToken);
            if (db.Database.Connection.State == ConnectionState.Open) { db.Database.Connection.Close(); }
            return true;
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = 86400, VaryByParam = "Usuario")]
        private void PerfilSesion(int Usuario)
        {
            var usuario = this.db.Users.Find(Usuario);
            if (db.Database.Connection.State == ConnectionState.Open) { db.Database.Connection.Close(); }
            this.usuarioactual = usuario;
        }
        //GET: /Login/Salir
        [HttpGet]
        public ActionResult Salir(string Direccion = "/")
        {
            this.CookieSesion = base.HttpContext.Request.Cookies.Get("Sesion");
            this.CookieToken = base.HttpContext.Request.Cookies.Get("Token");
            if (this.CookieSesion != null && this.CookieToken != null)
            {
                int IdCookie = 0;
                if (Int32.TryParse(this.CookieSesion.Value, out IdCookie))
                {
                    Sesions lasesion = db.Sesions.Find(IdCookie);
                    if (lasesion != null)
                    {
                        lasesion.Estado = 1;
                        lasesion.Expiracion = DateTime.Now;
                        db.Entry(lasesion).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        if (db.Database.Connection.State == ConnectionState.Open) { db.Database.Connection.Close(); }
                    }
                }
                this.DeleteCookies();
            }
            return Redirect(Direccion);
        }

        [HttpPost]
        public ActionResult CerrarSesion(int SesionId)
        {
            JsonResult respuesta = new JsonResult();
            if (ValidaSesion())
            {
                this.DeleteCookies();
                Sesions sesion = this.db.Sesions.Find(SesionId);
                sesion.Estado = 1;
                sesion.Expiracion = DateTime.Now;
                this.db.Entry(sesion).State = EntityState.Modified;
                this.db.SaveChanges();
                if (db.Database.Connection.State == ConnectionState.Open) { db.Database.Connection.Close(); }
                respuesta.Data = new { estado = true };
            }
            else
            {
                respuesta.Data = new { estado = false, mensaje = this.MensajeNoPermiso };
            }
            return respuesta;
        }

        private void DeleteCookies()
        {
            this.CookieSesion.Expires = DateTime.Now.AddDays(-1);
            this.CookieToken.Expires = DateTime.Now.AddDays(-1);
            base.HttpContext.Request.Cookies.Set(this.CookieSesion);
            base.HttpContext.Request.Cookies.Set(this.CookieToken);
        }
       
    }

}