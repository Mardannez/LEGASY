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

        #region #################### Empresas ###################
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
              
               

                ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                db.Personaliza_P_GuardarEmpresas(Nombre, Descripcion, Persona, Direccion, Identidad1, Identidad2, Identidad3, Telefono1, Telefono2, Telefono3, Correo, Comentarios, this.UsuarioActual.entryCode, DateTime.Now,  Respuesta, Mensaje);

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
                

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarEmpresas(IdEmpresa, Nombre, Descripcion, Persona, Direccion, Identidad1, Identidad2, Identidad3, Telefono1, Telefono2, Telefono3, Correo, Comentarios, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

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
        #endregion

        #region ################ Sucursales #################
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

        #endregion

        #region ############### Cuentas de Banco ###################
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

        #endregion

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

        #region  ################ Tipos de Procedimiento  #################

        public ActionResult ListaTiposProcedimiento(string respuesta)
        {
            if (ValidaSesion(20))
            {
                List<Personaliza_Fn_ListaTiposProcedimiento_Result> Procedimientos = new List<Personaliza_Fn_ListaTiposProcedimiento_Result>();

                Procedimientos = db.Personaliza_Fn_ListaTiposProcedimiento().ToList();

                return View(Procedimientos);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTiposProcedimiento(string respuesta)
        {

            if (ValidaSesion(20))
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
        public ActionResult CrearTiposProcedimiento(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(20))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoProcedimiento(EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposProcedimiento");
                    }
                    else
                    {

                        return RedirectToAction("CrearTiposProcedimiento", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTiposProcedimiento", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTiposProcedimiento(int Id)
        {

            if (ValidaSesion(20))
            {

                Personaliza_Fn_TipoProcedimientoSeleccionado_Result Procedimiento = new Personaliza_Fn_TipoProcedimientoSeleccionado_Result();

                Procedimiento = db.Personaliza_Fn_TipoProcedimientoSeleccionado(Id).FirstOrDefault();

                return View(Procedimiento);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTiposProcedimiento(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(20))
            {
                try
                {
                    bool EstadoProcedimiento = false;

                    if (Estado == 1)
                    {
                        EstadoProcedimiento = true;
                    }
                    else
                    {
                        EstadoProcedimiento = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoProcedimiento(Id, EstadoProcedimiento, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposProcedimiento");
                    }
                    else
                    {

                        return RedirectToAction("EditarTiposProcedimiento", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTiposProcedimiento", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Demanda  #################

        public ActionResult ListaTiposDemanda(string respuesta)
        {
            if (ValidaSesion(21))
            {
                List<Personaliza_Fn_ListaTipoDemanda_Result> TiposDemandas = new List<Personaliza_Fn_ListaTipoDemanda_Result>();

                TiposDemandas = db.Personaliza_Fn_ListaTipoDemanda().ToList();

                return View(TiposDemandas);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTiposDemanda(string respuesta)
        {

            if (ValidaSesion(21))
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
        public ActionResult CrearTiposDemanda(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(21))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoDemanda(EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposDemanda");
                    }
                    else
                    {

                        return RedirectToAction("CrearTiposDemanda", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTiposDemanda", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoDemanda(int Id)
        {

            if (ValidaSesion(21))
            {

                Personaliza_Fn_TipoDemandaSeleccionada_Result TipoDemanda = new Personaliza_Fn_TipoDemandaSeleccionada_Result();

                TipoDemanda = db.Personaliza_Fn_TipoDemandaSeleccionada(Id).FirstOrDefault();

                return View(TipoDemanda);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoDemanda(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(21))
            {
                try
                {
                    bool EstadoDemanda = false;

                    if (Estado == 1)
                    {
                        EstadoDemanda = true;
                    }
                    else
                    {
                        EstadoDemanda = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoDemanda(Id, EstadoDemanda, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposDemanda");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoDemanda", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoDemanda", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Instancia  #################

        public ActionResult ListaTiposInstancia(string respuesta)
        {
            if (ValidaSesion(22))
            {
                List<Personaliza_Fn_ListaTipoInstancia_Result> TiposInstancias = new List<Personaliza_Fn_ListaTipoInstancia_Result>();

                TiposInstancias = db.Personaliza_Fn_ListaTipoInstancia().ToList();

                return View(TiposInstancias);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTiposIntancia(string respuesta)
        {

            if (ValidaSesion(22))
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
        public ActionResult CrearTiposIntancia(int Estado, Int64 Nivel,  string Nombre, string Descripcion)
        {
            if (ValidaSesion(22))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearInstancia(EstadoTipo, Nivel, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposInstancia");
                    }
                    else
                    {

                        return RedirectToAction("CrearTiposInstancia", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTiposInstancia", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoInstancia(int Id)
        {

            if (ValidaSesion(22))
            {

                Personaliza_Fn_TipoInstanciaSeleccionada_Result TipoInstancia = new Personaliza_Fn_TipoInstanciaSeleccionada_Result();

                TipoInstancia = db.Personaliza_Fn_TipoInstanciaSeleccionada(Id).FirstOrDefault();

                return View(TipoInstancia);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoInstancia(Int64 Id, int Estado, Int64 Nivel,  string Nombre, string Descripcion)
        {

            if (ValidaSesion(22))
            {
                try
                {
                    bool EstadoInstancia = false;

                    if (Estado == 1)
                    {
                        EstadoInstancia = true;
                    }
                    else
                    {
                        EstadoInstancia = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoInstancia(Id, EstadoInstancia, Nivel, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposInstancia");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoInstancia", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoInstancia", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Proceso  #################

        public ActionResult ListaTiposProceso(string respuesta)
        {
            if (ValidaSesion(23))
            {
                List<Personaliza_Fn_ListaTipoProceso_Result> TiposProceso = new List<Personaliza_Fn_ListaTipoProceso_Result>();

                TiposProceso = db.Personaliza_Fn_ListaTipoProceso().ToList();

                return View(TiposProceso);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTiposProceso(string respuesta)
        {

            if (ValidaSesion(23))
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
        public ActionResult CrearTiposProceso(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(23))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoProceso(EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposProceso");
                    }
                    else
                    {

                        return RedirectToAction("CrearTiposProceso", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTiposProceso", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoProceso(int Id)
        {

            if (ValidaSesion(23))
            {

                Personaliza_Fn_TipoProcesoSeleccionado_Result TipoProceso = new Personaliza_Fn_TipoProcesoSeleccionado_Result();

                TipoProceso = db.Personaliza_Fn_TipoProcesoSeleccionado(Id).FirstOrDefault();

                return View(TipoProceso);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoProceso(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(23))
            {
                try
                {
                    bool EstadoProceso = false;

                    if (Estado == 1)
                    {
                        EstadoProceso = true;
                    }
                    else
                    {
                        EstadoProceso = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoProceso(Id, EstadoProceso, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposProceso");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoProceso", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoProceso", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Parte de Casos  #################

        public ActionResult ListaTiposParte(string respuesta)
        {
            if (ValidaSesion(24))
            {
                List<Personaliza_Fn_ListaTipoParte_Result> TiposPartes = new List<Personaliza_Fn_ListaTipoParte_Result>();

                TiposPartes = db.Personaliza_Fn_ListaTipoParte().ToList();

               
                return View(TiposPartes);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTiposParte(string respuesta)
        {

            if (ValidaSesion(24))
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;
                }
                List<SelectListItem> TipoParte = new List<SelectListItem>();
                TipoParte.Add(new SelectListItem { Value = "", Text = "[-- Seleccionar Tipo de Parte --]" });
                foreach (var item in db.Personaliza_Fn_DropDown_TipoParte().ToList())
                {
                    TipoParte.Add(new SelectListItem { Value = item.entryCode.ToString(), Text = item.entryName.ToString() });
                }

                ViewBag.TiposdeParte = TipoParte;

                return View();
            }
            else
            {
                return Salir();
            }


        }
        [HttpPost]
        public ActionResult CrearTiposParte(Int64 TiposdeParte, int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(24))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoParte(TiposdeParte, EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposParte");
                    }
                    else
                    {

                        return RedirectToAction("CrearTiposParte", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTiposParte", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoParte(int Id)
        {

            if (ValidaSesion(24))
            {

                Personaliza_Fn_TipoParteSeleccionada_Result TipoParte = new Personaliza_Fn_TipoParteSeleccionada_Result();

                TipoParte = db.Personaliza_Fn_TipoParteSeleccionada(Id).FirstOrDefault();


                List<SelectListItem> TipoParteDrop = new List<SelectListItem>();
                TipoParteDrop.Add(new SelectListItem { Value = "", Text = "[-- Seleccionar Tipo de Parte --]" });
                foreach (var item in db.Personaliza_Fn_DropDown_TipoParte().ToList())
                {
                    TipoParteDrop.Add(new SelectListItem { Value = item.entryCode.ToString(), Text = item.entryName.ToString() });
                }

                ViewBag.TiposdeParte = TipoParteDrop;

                return View(TipoParte);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoParte(Int64 Id, Int64 TiposdeParte, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(24))
            {
                try
                {
                    bool EstadoProceso = false;

                    if (Estado == 1)
                    {
                        EstadoProceso = true;
                    }
                    else
                    {
                        EstadoProceso = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoParte(Id, TiposdeParte, EstadoProceso, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposParte");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoParte", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoParte", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Area de Caso  #################

        public ActionResult ListaTiposAreaCaso(string respuesta)
        {
            if (ValidaSesion(25))
            {
                List<Personaliza_Fn_ListaAreaCaso_Result> AreaCaso = new List<Personaliza_Fn_ListaAreaCaso_Result>();

                AreaCaso = db.Personaliza_Fn_ListaAreaCaso().ToList();

                return View(AreaCaso);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTiposAreaCaso(string respuesta)
        {

            if (ValidaSesion(25))
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
        public ActionResult CrearTiposAreaCaso(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(25))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoaAreaCaso(EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposAreaCaso");
                    }
                    else
                    {

                        return RedirectToAction("CrearTiposAreaCaso", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTiposAreaCaso", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoAreaCaso(int Id)
        {

            if (ValidaSesion(25))
            {

                Personaliza_Fn_TipoAreaCasoSeleccionado_Result TipoAreaCaso = new Personaliza_Fn_TipoAreaCasoSeleccionado_Result();

                TipoAreaCaso = db.Personaliza_Fn_TipoAreaCasoSeleccionado(Id).FirstOrDefault();

                return View(TipoAreaCaso);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoAreaCaso(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(25))
            {
                try
                {
                    bool EstadoProceso = false;

                    if (Estado == 1)
                    {
                        EstadoProceso = true;
                    }
                    else
                    {
                        EstadoProceso = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoAreaCaso(Id, EstadoProceso, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposAreaCaso");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoAreaCaso", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoAreaCaso", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Caso  #################

        public ActionResult ListaTiposCaso(string respuesta)
        {
            if (ValidaSesion(26))
            {
                List<Personaliza_Fn_ListaTipoCaso_Result> TipoCaso = new List<Personaliza_Fn_ListaTipoCaso_Result>();

                TipoCaso = db.Personaliza_Fn_ListaTipoCaso().ToList();

                return View(TipoCaso);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTipoCaso(string respuesta)
        {

            if (ValidaSesion(26))
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
        public ActionResult CrearTipoCaso(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(26))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoCaso(EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposCaso");
                    }
                    else
                    {

                        return RedirectToAction("CrearTipoCaso", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTipoCaso", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoCaso(int Id)
        {

            if (ValidaSesion(26))
            {

                Personaliza_Fn_TipoCasoSeleccionado_Result TipoCaso = new Personaliza_Fn_TipoCasoSeleccionado_Result();

                TipoCaso = db.Personaliza_Fn_TipoCasoSeleccionado(Id).FirstOrDefault();

                return View(TipoCaso);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoCaso(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(26))
            {
                try
                {
                    bool EstadoProceso = false;

                    if (Estado == 1)
                    {
                        EstadoProceso = true;
                    }
                    else
                    {
                        EstadoProceso = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoCaso(Id, EstadoProceso, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposCaso");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoCaso", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoCaso", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Pretension  #################

        public ActionResult ListaTiposPretension(string respuesta)
        {
            if (ValidaSesion(27))
            {
                List<Personaliza_Fn_ListaTipoPretension_Result> TipoPretension = new List<Personaliza_Fn_ListaTipoPretension_Result>();

                TipoPretension = db.Personaliza_Fn_ListaTipoPretension().ToList();

                return View(TipoPretension);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTipoPretension(string respuesta)
        {

            if (ValidaSesion(27))
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
        public ActionResult CrearTipoPretension(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(27))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoPretension(EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposPretension");
                    }
                    else
                    {

                        return RedirectToAction("CrearTipoPretension", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTipoPretension", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoPretension(int Id)
        {

            if (ValidaSesion(27))
            {

                Personaliza_Fn_TipoPretensionSeleccionada_Result TipoPretension = new Personaliza_Fn_TipoPretensionSeleccionada_Result();

                TipoPretension = db.Personaliza_Fn_TipoPretensionSeleccionada(Id).FirstOrDefault();

                return View(TipoPretension);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoPretension(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(27))
            {
                try
                {
                    bool EstadoProceso = false;

                    if (Estado == 1)
                    {
                        EstadoProceso = true;
                    }
                    else
                    {
                        EstadoProceso = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoPretension(Id, EstadoProceso, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposPretension");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoPretension", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoPretension", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Documento  #################

        public ActionResult ListaTiposDocumento(string respuesta)
        {
            if (ValidaSesion(28))
            {
                List<Personaliza_Fn_ListaTipoDocumento_Result> TiposDocumento = new List<Personaliza_Fn_ListaTipoDocumento_Result>();

                TiposDocumento = db.Personaliza_Fn_ListaTipoDocumento().ToList();


                return View(TiposDocumento);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTiposDocumento(string respuesta)
        {

            if (ValidaSesion(28))
            {
                if (respuesta != null)
                {
                    ViewBag.Respuesta = respuesta;
                }
                List<SelectListItem> TipoParte = new List<SelectListItem>();
                TipoParte.Add(new SelectListItem { Value = "", Text = "[-- Seleccionar Tipo de Parte --]" });
                foreach (var item in db.Personaliza_Fn_DropDown_TipoParte().ToList())
                {
                    TipoParte.Add(new SelectListItem { Value = item.entryCode.ToString(), Text = item.entryName.ToString() });
                }

                ViewBag.TiposdeParte = TipoParte;

                return View();
            }
            else
            {
                return Salir();
            }


        }
        [HttpPost]
        public ActionResult CrearTiposDocumento(Int64 TiposdeParte, int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(28))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoDocumento(TiposdeParte, EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposDocumento");
                    }
                    else
                    {

                        return RedirectToAction("CrearTiposDocumento", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTiposDocumento", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoDocumento(int Id)
        {

            if (ValidaSesion(28))
            {

                Personaliza_Fn_TipoDocumentoSeleccionado_Result TipoDocumento = new Personaliza_Fn_TipoDocumentoSeleccionado_Result();

                TipoDocumento = db.Personaliza_Fn_TipoDocumentoSeleccionado(Id).FirstOrDefault();


                List<SelectListItem> TipoParteDrop = new List<SelectListItem>();
                TipoParteDrop.Add(new SelectListItem { Value = "", Text = "[-- Seleccionar Tipo de Parte --]" });
                foreach (var item in db.Personaliza_Fn_DropDown_TipoParte().ToList())
                {
                    TipoParteDrop.Add(new SelectListItem { Value = item.entryCode.ToString(), Text = item.entryName.ToString() });
                }

                ViewBag.TiposdeParte = TipoParteDrop;

                return View(TipoDocumento);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoDocumento(Int64 Id, Int64 TiposdeParte, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(28))
            {
                try
                {
                    bool EstadoDocumento = false;

                    if (Estado == 1)
                    {
                        EstadoDocumento = true;
                    }
                    else
                    {
                        EstadoDocumento = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoDocumento(Id, TiposdeParte, EstadoDocumento, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposDocumento");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoDocumento", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoDocumento", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }

        #endregion

        #region  ################ Tipos de Gasto  #################

        public ActionResult ListaTiposGasto(string respuesta)
        {
            if (ValidaSesion(29))
            {
                List<Personaliza_Fn_ListaTipoGasto_Result> TipoGasto = new List<Personaliza_Fn_ListaTipoGasto_Result>();

                TipoGasto = db.Personaliza_Fn_ListaTipoGasto().ToList();

                return View(TipoGasto);

            }
            else
            {
                return Salir();
            }

        }

        public ActionResult CrearTipoGasto(string respuesta)
        {

            if (ValidaSesion(29))
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
        public ActionResult CrearTipoGasto(int Estado, string Nombre, string Descripcion)
        {
            if (ValidaSesion(29))
            {
                try
                {
                    bool EstadoTipo = false;

                    if (Estado == 1)
                    {
                        EstadoTipo = true;
                    }
                    else
                    {
                        EstadoTipo = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_CrearTipoGasto(EstadoTipo, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposGasto");
                    }
                    else
                    {

                        return RedirectToAction("CrearTipoGasto", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("CrearTipoGasto", new { @respuesta = Error });
                }
            }
            else
            {
                return Salir();
            }

        }


        public ActionResult EditarTipoGasto(int Id)
        {

            if (ValidaSesion(29))
            {

                Personaliza_Fn_TipoGastoSeleccionado_Result TipoGasto = new Personaliza_Fn_TipoGastoSeleccionado_Result();

                TipoGasto = db.Personaliza_Fn_TipoGastoSeleccionado(Id).FirstOrDefault();

                return View(TipoGasto);
            }
            else
            {
                return Salir();
            }


        }

        [HttpPost]
        public ActionResult EditarTipoGasto(Int64 Id, int Estado, string Nombre, string Descripcion)
        {

            if (ValidaSesion(29))
            {
                try
                {
                    bool EstadoProceso = false;

                    if (Estado == 1)
                    {
                        EstadoProceso = true;
                    }
                    else
                    {
                        EstadoProceso = false;
                    }

                    ObjectParameter Respuesta = new ObjectParameter("Respuesta", typeof(int));
                    ObjectParameter Mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.Personaliza_P_EditarTipoGasto(Id, EstadoProceso, Nombre, Descripcion, this.UsuarioActual.entryCode, DateTime.Now, Respuesta, Mensaje);

                    int RespValue = Convert.ToInt32(Respuesta.Value);
                    string MenValue = Convert.ToString(Mensaje.Value);

                    if (RespValue == 1)
                    {

                        return RedirectToAction("ListaTiposGasto");
                    }
                    else
                    {

                        return RedirectToAction("EditarTipoGasto", new { @respuesta = MenValue.ToString() });

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

                    return RedirectToAction("EditarTipoGasto", new { @respuesta = Error });
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