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

            if (ValidaSesion())
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
            else
            {
                return Salir();
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
            if (ValidaSesion())
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
            else
            {
                return Salir();
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

        [HttpGet]
        public ActionResult CrearCuentaBanco(string respuesta)
        {

            if (ValidaSesion(9))
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;
                }

                List<SelectListItem> TipoCuenta = new List<SelectListItem>();
                TipoCuenta.Add(new SelectListItem { Value = "", Text = "[-- Seleccionar Tipo Cuenta --]" });
                foreach (var item in db.Personaliza_Fn_TipoCuenta().ToList())
                {
                    TipoCuenta.Add(new SelectListItem { Value = item.entryCode.ToString(), Text = item.entryName.ToString() });
                }

                ViewBag.TipoCuentaBancos = TipoCuenta;

                List<SelectListItem> Moneda = new List<SelectListItem>();
                Moneda.Add(new SelectListItem { Value = "", Text = "[-- Seleccionar Moneda --]" });
                foreach (var item in db.Personaliza_Fn_Monedas().ToList())
                {
                    Moneda.Add(new SelectListItem { Value = item.entryCode.ToString(), Text = item.entryName.ToString() });
                }

                ViewBag.Monedas = Moneda;

                return View();
            }
            else
            {
                return Salir();
            }

        }
        #region  ################ Periodos de Presupuesto  #################

        public ActionResult ListaPeriodosPresupuesto(string respuesta)
        {
            if (ValidaSesion(18))
            {
                List<Personaliza_Fn_ListaPeriodosPresupuesto_Result> Periodos = new List<Personaliza_Fn_ListaPeriodosPresupuesto_Result>();

                Periodos = db.Personaliza_Fn_ListaPeriodosPresupuesto().ToList();

                return View(Periodos);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearPeriodosPresupuesto(string respuesta)
        {

            if(ValidaSesion(18))
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;
                }
                return View();
            }
            else{
                return Salir();
            }


        }
        [HttpPost]
        public ActionResult CrearPeriodosPresupuesto(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(18))
            {
                try
                {   bool EstadoPeriodo = false;

                    if(Estado == 1)
                    {
                        EstadoPeriodo = true;
                    }
                    else
                    {
                        EstadoPeriodo= false;
                    }
                    
                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearPeriodoPresupuesto(EstadoPeriodo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {
                      
                        return RedirectToAction("ListaPeriodosPresupuesto");
                    }
                    else
                    {

                        return RedirectToAction("CrearPeriodosPresupuesto", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearPeriodosPresupuesto", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }
            
        }


        public ActionResult EditarPeriodoPresupuesto(int Id)
        {

            if (ValidaSesion(18))
            {

                Personaliza_Fn_PeriodoSeleccionado_Result Periodo = new Personaliza_Fn_PeriodoSeleccionado_Result();

                Periodo = db.Personaliza_Fn_PeriodoSeleccionado(Id).FirstOrDefault();

                return View(Periodo);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarPeriodoPresupuesto(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(18))
            {
                try
                {
                    bool EstadoPeriodo = false;

                    if (Estado == 1)
                    {
                        EstadoPeriodo = true;
                    }
                    else
                    {
                        EstadoPeriodo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarPeriodoPresupuesto(Id, EstadoPeriodo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaPeriodosPresupuesto");
                    }
                    else
                    {

                        return RedirectToAction("EditarPeriodoPresupuesto", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarPeriodoPresupuesto", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion


        #region  ################ Conceptos de Presupuesto  #################

        public ActionResult ListaConceptosPresupuesto(string respuesta)
        {
            if (ValidaSesion(19))
            {
                List<Personaliza_Fn_ListaConceptosPresupuesto_Result> Conceptos = new List<Personaliza_Fn_ListaConceptosPresupuesto_Result>();

                Conceptos = db.Personaliza_Fn_ListaConceptosPresupuesto().ToList();

                return View(Conceptos);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearConceptosPresupuesto(string respuesta)
        {

            if (ValidaSesion(19))
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
        public ActionResult CrearConceptosPresupuesto(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(19))
            {
                try
                {
                    bool EstadoPeriodo = false;

                    if (Estado == 1)
                    {
                        EstadoPeriodo = true;
                    }
                    else
                    {
                        EstadoPeriodo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearConceptoPresupuesto(EstadoPeriodo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaConceptosPresupuesto");
                    }
                    else
                    {

                        return RedirectToAction("CrearConceptosPresupuesto", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearConceptosPresupuesto", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarConceptoPresupuesto(int Id)
        {

            if (ValidaSesion(19))
            {

                Personaliza_Fn_ConceptoPSeleccionado_Result Concepto = new Personaliza_Fn_ConceptoPSeleccionado_Result();

                Concepto = db.Personaliza_Fn_ConceptoPSeleccionado(Id).FirstOrDefault();

                return View(Concepto);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarConceptoPresupuesto(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(19))
            {
                try
                {
                    bool EstadoConcepto = false;

                    if (Estado == 1)
                    {
                        EstadoConcepto = true;
                    }
                    else
                    {
                        EstadoConcepto = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarConceptoPresupuesto(Id, EstadoConcepto, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaConceptosPresupuesto");
                    }
                    else
                    {

                        return RedirectToAction("EditarConceptoPresupuesto", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarPeriodoPresupuesto", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion



    }
}