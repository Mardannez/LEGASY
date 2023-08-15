using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity.Core.Objects;
using LEGASY.Models;
using System.Net;
using LEGASY.Encripta;

namespace LEGASY.Controllers
{
    
    public class SeguridadController : PrincipalController
    {
        // GET: Seguridad

        public ActionResult Modulos()
        {
            if (ValidaSesion(1))
             {

                List<Seguridad_Fn_MostrarModulos_Result> Modulos = new List<Seguridad_Fn_MostrarModulos_Result>();
                Modulos = db.Seguridad_Fn_MostrarModulos().ToList();
                return View(Modulos);

            }
            else
            {
                return Salir();
            }
           
           
        }
        [HttpGet]
        public ActionResult GuardarModulos( string respuesta)
        {
            if (ValidaSesion(1))
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;
                }

                return View();
            }
            else
            {
                return Salir();
            }
           
        }

        [HttpPost]
        public ActionResult GuardarModulos( string Nombre, string NombreCorto, string Descripcion, int Estado)
        {
            if (ValidaSesion())
            {
                JsonResult respuesta = new JsonResult();
                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                bool State = false;

                if (Estado == 1)
                {
                    State = true;
                }

                db.Seguridad_P_GuardarModulo(State, Nombre, NombreCorto, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                int RespValue = Convert.ToInt32(Respuesta.Value);
                string MenValue = Convert.ToString(Mensaje.Value);

                if (RespValue == 1)
                {
                    respuesta.Data = new { estado = true, mensaje = "El Modulo se guardo correctamente!" };

                    return RedirectToAction("Modulos");
                }
                else
                {
                    ViewBag.Respuesta = MenValue;
                    return RedirectToAction("GuardarModulos", new { @respuesta = MenValue.ToString() });

                }
            }
            else
            {
                return Salir();
            }

         
        }
        [HttpGet]
        public ActionResult EditarModulo(int? Id, string respuesta)
        {
            if (ValidaSesion(1))
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;

                }

                var InfoModulo = db.Seguridad_Fn_ModuloSeleccionado(Id).FirstOrDefault();

                if (InfoModulo == null)
                {
                    ViewBag.Respuesta = "El Modulo ingresado no existe!";
                    return View();
                }
                else
                {

                    return View(InfoModulo);
                }
            }
            else
            {
                return Salir();
            }

    
        }
        [HttpPost]
        public ActionResult EditarModulo(int entryCode, int Estado, string Nombre, string NombreCorto, string Descripcion)
        {

            if (ValidaSesion())
            {
                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                bool State = false;

                if (Estado == 1)
                {
                    State = true;
                }

                db.Seguridad_P_EditarModulo(entryCode, State, Nombre, NombreCorto, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                int RespValue = Convert.ToInt32(Respuesta.Value);
                string MenValue = Convert.ToString(Mensaje.Value);

                if (RespValue == 1)
                {


                    return RedirectToAction("EditarModulo", new { @Id = entryCode, @respuesta = MenValue.ToString() });
                }
                else
                {
                    ViewBag.Respuesta = MenValue;
                    return RedirectToAction("EditarModulo", new { @Id = entryCode, @respuesta = MenValue.ToString() });

                }
            }
            else
            {
                return Salir();
            }
           


        }


        public ActionResult ListaPermisos(int IdModulo)
        {
            if (ValidaSesion())
            {
                List<Seguridad_Fn_MostrarPermisos_Result> Permisos = new List<Seguridad_Fn_MostrarPermisos_Result>();
                Permisos = db.Seguridad_Fn_MostrarPermisos(IdModulo).ToList();
                ViewBag.IdModulo = IdModulo;

                return View(Permisos);
            }
            else
            {
                return Salir();
            }
         
        }


        [HttpGet]
        public ActionResult GuardarPermiso(int IdModulo, string respuesta)
        {
            if (ValidaSesion())
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;
                }
                ViewBag.IdModulo = IdModulo;

                var DatosModulo = db.Seguridad_Fn_MostrarModulos().Where(x => x.entryCode == IdModulo).FirstOrDefault();

                return View(DatosModulo);

            }
            else
            {
                return Salir();
            }

        }
        [HttpPost]
        public ActionResult GuardarPermiso(int IdModulo, int Estado, string NombrePermiso, string Descripcion)
        {
            if (ValidaSesion())
            {
                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                bool State = false;

                if (Estado == 1)
                {
                    State = true;
                }

                db.Seguridad_P_GuardarPermiso(IdModulo, State, NombrePermiso, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                int RespValue = Convert.ToInt32(Respuesta.Value);
                string MenValue = Convert.ToString(Mensaje.Value);

                if (RespValue == 1)
                {


                    return RedirectToAction("ListaPermisos", new { @IdModulo = IdModulo });
                }
                else
                {
                    ViewBag.Respuesta = MenValue;
                    return RedirectToAction("GuardarPermiso", new { IdModulo = IdModulo, @respuesta = MenValue.ToString() });

                }
            }
            else
            {
                return Salir();
            }
          


        }
        [HttpGet]
        public ActionResult EditarPermiso(int? Id, string respuesta)
        {
            if (ValidaSesion())
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;

                }

                var InfoPermiso = db.Seguridad_Fn_PermisoSeleccionado(Id).FirstOrDefault();

                if (InfoPermiso == null)
                {
                    ViewBag.Respuesta = "El Permiso ingresado no existe!";
                    return View();
                }
                else
                {

                    return View(InfoPermiso);
                }

            }
            else
            {
                return Salir();
            }
         


        }
        [HttpPost]
        public ActionResult EditarPermiso(int entryCode, int Estado, string Nombre, string NombreCorto, string Descripcion)
        {
            if (ValidaSesion())
            {
                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));
                bool State = false;

                if (Estado == 1)
                {
                    State = true;
                }

                db.Seguridad_P_EditarModulo(entryCode, State, Nombre, NombreCorto, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                int RespValue = Convert.ToInt32(Respuesta.Value);
                string MenValue = Convert.ToString(Mensaje.Value);

                if (RespValue == 1)
                {


                    return RedirectToAction("EditarModulo", new { @Id = entryCode, @respuesta = MenValue.ToString() });
                }
                else
                {
                    ViewBag.Respuesta = MenValue;
                    return RedirectToAction("EditarModulo", new { @Id = entryCode, @respuesta = MenValue.ToString() });

                }
            }
            else
            {
                return Salir();
            }

        }
        public ActionResult ListaUsuarios()
        {
            if (ValidaSesion(3))
            {
                
                List<Seguridad_Fn_MostrarUsuarios_Result> Usuarios = new List<Seguridad_Fn_MostrarUsuarios_Result>();
                Usuarios = db.Seguridad_Fn_MostrarUsuarios().ToList();

                return View(Usuarios);
                
            }
            else
            {
                return Salir();
            }

         
        }
        [HttpGet]
        public ActionResult RegistrarUsuario(string respuesta)
        {
            if (ValidaSesion())
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;

                }

                return View();
            }
            else
            {
                return Salir();
            }
    

        }

        [HttpPost]
        public ActionResult RegistrarUsuario(string NombreDesplegable, string NombreUsuario, string Correo, string Contraseña, string Descripcion)
        {

            if (ValidaSesion())
            {
                EncryptMD5 UsingEncript = new EncryptMD5();

                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                string ContraseñaEncriptada = UsingEncript.Encriptar(Contraseña);
                //Tengo que encriptar la contraseña del usuario


                db.Seguridad_P_GuardarUsuario(true, NombreDesplegable, Correo, NombreUsuario, ContraseñaEncriptada, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                int RespValue = Convert.ToInt32(Respuesta.Value);
                string MenValue = Convert.ToString(Mensaje.Value);

                if (RespValue == 1)
                {


                    return RedirectToAction("ListaUsuarios");
                }
                else
                {

                    return RedirectToAction("RegistrarUsuario", new { @respuesta = MenValue.ToString() });

                }
            }
            else
            {
                return Salir();
            }
           

        }

        [HttpGet]
        public ActionResult EditarUsuario(int? Id, string respuesta)
        {
            if (ValidaSesion())
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;

                }

                var InfoUsuario = db.Seguridad_Fn_UsuarioSeleccionado(Id).FirstOrDefault();

                if (InfoUsuario == null)
                {
                    ViewBag.Respuesta = "El Usuario ingresado no existe!";
                    return View();
                }
                else
                {

                    return View(InfoUsuario);
                }

            }
            else
            {
                return Salir();
            }


        }

        public ActionResult ModulosUsuario(int? IdUsuario)
        {
            if (ValidaSesion())
            {
                Users U = db.Users.Find(IdUsuario); //Busqueda del Usuario
                ViewBag.UserName = U.entryUserName;
                ViewBag.UserId = U.entryCode;

                ViewBag.TodoslosModulos = db.UsersModules.Where(x => x.entryCodeStatus == true).ToList(); //Todos los modulos activos//
                return View(U.UsersRole.ToList()); //La tabla relacionada de Usuarios con modulos
            }
            else
            {
                return Salir();
            }
          
        }

        [HttpPost]
        public ActionResult ModulosUsuario(int Usuario, string Modulos)
        {
           if(ValidaSesion())
            {
                var elususario = this.db.Users.Find(Usuario);
                string[] los = Modulos.Split(',');
                db.Seguridad_P_Usuario_RemoveAllModulos(Usuario);
                for (int i = 0; i < los.Length; i++)
                {
                    if (los[i] != "")
                    {
                        int idmodulo = Convert.ToInt32(los[i]);
                        db.Seguridad_P_Usuario_SetModulo(Usuario, idmodulo, 1);
                    }
                }
                ViewBag.UserName = elususario.entryUserName;
                ViewBag.UserId = elususario.entryCode;
                ViewBag.TodoslosModulos = db.UsersModules.Where(x => x.entryCodeStatus == true).ToList();
                ViewBag.Mensaje = "Los modulos fueron asignados correctamente, por favor vuelva a definir los Permisos. ";
                return View(elususario.UsersRole.ToList());
            }
            else
            {
                return Salir();
            }
           
        }

        public ActionResult PermisosUsuario(int? IdUsuario)
        {
            if (ValidaSesion())
            {
                Users U = db.Users.Find(IdUsuario); //Busqueda del Usuario
                ViewBag.UserName = U.entryUserName;
                ViewBag.UserId = U.entryCode;
                ViewBag.LosModulos = U.UsersRole.Where(x => x.UsersModules.entryCodeStatus == true).ToList(); //La asignacion de los modulos al usuarios//
                return View(U.UsersAuthorization.ToList()); //La tabla relacionada de Usuarios con Permisos
            }
            else
            {
                return Salir();
            }
          
        }
        [HttpPost]
        public ActionResult PermisosUsuario(int? IdUsuario, string Permisos)
        {
            if (ValidaSesion())
            {
                Users elususario = db.Users.Find(IdUsuario);
                string[] los = Permisos.Split(',');
                db.Seguridad_P_Usuario_RemoveAllPermisos(IdUsuario);
                for (int i = 0; i < los.Length; i++)
                {
                    if (los[i] != "")
                    {
                        int idpermiso = Convert.ToInt32(los[i]);
                        db.Seguridad_P_Usuario_SetPermisos(IdUsuario, idpermiso, 1);
                    }
                }
                ViewBag.UserName = elususario.entryUserName;
                ViewBag.UserId = elususario.entryCode;
                ViewBag.LosModulos = elususario.UsersRole.Where(x => x.UsersModules.entryCodeStatus == true).ToList();
                ViewBag.Mensaje = "Los Permisos fueron asignados correctamente.";
                return View(elususario.UsersAuthorization.ToList());
            }
            else
            {
                return Salir();
            }
           
        }

    }
}