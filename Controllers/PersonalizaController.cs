using LEGASY.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEGASY.Encripta;

namespace LEGASY.Controllers
{
    [ValidarSesion]
    public class PersonalizaController : PrincipalController
    {
        // GET: Personaliza
        public ActionResult Index()
        {
            if (ValidaSesion(7))
            {
                List<Personaliza_Fn_ListaEmpresas_Result> listaEmpresas = new List<Personaliza_Fn_ListaEmpresas_Result>();

                listaEmpresas = db.Personaliza_Fn_ListaEmpresas().ToList();

                return View(listaEmpresas);
            }
            else
            {
                return Salir();
            }
            
        }

        [HttpGet]
        public ActionResult GuardarEmpresa(string respuesta)
        {

            if (ValidaSesion(7))
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
        public ActionResult GuardarEmpresa(string Nombre, string Descripcion, string Persona, string Direccion, string Identidad1, string Identidad2, string Identidad3, string Telefono1, string Telefono2, string Telefono3, string Correo, string Comentarios)
        {
            try
            {
              
                long IdUsuario = (long)Session["UsuarioId"];

                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                db.Personaliza_P_GuardarEmpresas(Nombre, Descripcion, Persona, Direccion, Identidad1, Identidad2, Identidad3, Telefono1, Telefono2, Telefono3, Correo, Comentarios, IdUsuario, DateTime.Now,  Respuesta, Mensaje);

                int RespValue = Convert.ToInt32(Respuesta.Value);
                string MenValue = Convert.ToString(Mensaje.Value);

                if (RespValue == 1)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                  
                    return RedirectToAction("GuardarEmpresa", new { @respuesta = MenValue.ToString() });

                }
            }
          
            catch (Exception ex)
            {
                string Error;

                if(ex.InnerException != null)
                {
                    Error = ex.InnerException.Message;
                }
                else
                {
                    Error = ex.Message;
                }

                return RedirectToAction("GuardarEmpresa", new { @respuesta = Error });
            }
        }

        [HttpGet]
        public ActionResult EditarEmpresa(int IdEmpresa)
        {
            if (ValidaSesion(7))
            {
                Personaliza_Fn_EmpresaSeleccionada_Result DatosEmpresa = new Personaliza_Fn_EmpresaSeleccionada_Result();

                DatosEmpresa = db.Personaliza_Fn_EmpresaSeleccionada(IdEmpresa).FirstOrDefault();

                return View(DatosEmpresa);
            }
            else
            {
                return Salir();
            }
          
        }

        [HttpPost]
        public ActionResult EditarEmpresa(Int64 IdEmpresa, string Nombre, string Descripcion, string Persona, string Direccion, string Identidad1, string Identidad2, string Identidad3, string Telefono1, string Telefono2, string Telefono3, string Correo, string Comentarios)
        {
            try
            {
                long IdUsuario = (long)Session["UsuarioId"];

                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                db.Personaliza_P_EditarEmpresas(IdEmpresa, Nombre, Descripcion, Persona, Direccion, Identidad1, Identidad2, Identidad3, Telefono1, Telefono2, Telefono3, Correo, Comentarios, IdUsuario, DateTime.Now, Respuesta, Mensaje);

                int RespValue = Convert.ToInt32(Respuesta.Value);
                string MenValue = Convert.ToString(Mensaje.Value);

                if (RespValue == 1)
                {

                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("EditarEmpresa", new { @respuesta = MenValue.ToString() });

                }
            }

            catch (Exception ex)
            {
                string Error;

                if (ex.InnerException != null)
                {
                    Error = ex.InnerException.Message;
                }
                else
                {
                    Error = ex.Message;
                }

                return RedirectToAction("EditarEmpresa", new { @respuesta = Error });
            }
        }

        public ActionResult GuardarSucursal(int IdEmpresa)
        {
            if (ValidaSesion(8))
            {
                Personaliza_Fn_EmpresaSeleccionada_Result Empresa = new Personaliza_Fn_EmpresaSeleccionada_Result();

                Empresa = db.Personaliza_Fn_EmpresaSeleccionada(IdEmpresa).FirstOrDefault();

                return View(Empresa);
            }
            else
            {
                return Salir();
            }
        
        }

        [HttpPost]
        public ActionResult GuardarSucursal(int IdEmpresa, string Nombre, string Descripcion, string Persona, string Direccion, string Identidad1, string Identidad2, string Identidad3, string Telefono1, string Telefono2, string Telefono3, string Correo, string Comentarios)
        {
            try
            {

                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                db.Personaliza_P_GuardarSucursal(IdEmpresa, Nombre, Descripcion, Persona, Direccion, Identidad1, Identidad2, Identidad3, Telefono1, Telefono2, Telefono3, Correo, Comentarios, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje); ;

                int RespValue = Convert.ToInt32(Respuesta.Value);
                string MenValue = Convert.ToString(Mensaje.Value);

                if (RespValue == 1)
                {
                    //Listado de Sucursales
                    return RedirectToAction("ListaSucursales");
                }
                else
                {

                    return RedirectToAction("GuardarEmpresa", new { @respuesta = MenValue.ToString() });

                }
            }

            catch (Exception ex)
            {
                string Error;

                if (ex.InnerException != null)
                {
                    Error = ex.InnerException.Message;
                }
                else
                {
                    Error = ex.Message;
                }

                return RedirectToAction("GuardarEmpresa", new { @respuesta = Error });
            }
        }
        public ActionResult ListaSucursales(string respuesta)
        {
            if (ValidaSesion(8))
            {
                List<Personaliza_Fn_ListaSucursales_Result> Sucursales = new List<Personaliza_Fn_ListaSucursales_Result>();

                Sucursales = db.Personaliza_Fn_ListaSucursales().ToList();

                return View(Sucursales);

            }
            else
            {
                return Salir();
            }

        }
        public ActionResult ListaCuentasBanco(string respuesta)
        {
            if (ValidaSesion(9))
            {
                List<Personaliza_Fn_CuentasdeBanco_Result> CuentasBanco = new List<Personaliza_Fn_CuentasdeBanco_Result>();

                CuentasBanco = db.Personaliza_Fn_CuentasdeBanco().ToList();

                return View(CuentasBanco);

            }
            else
            {
                return Salir();
            }

        }

    }
}