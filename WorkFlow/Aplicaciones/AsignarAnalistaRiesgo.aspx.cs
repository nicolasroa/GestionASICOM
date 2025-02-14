using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WorkFlow
{
    public partial class AsignarAnalistaRiesgo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                CargarBandejaEntradaAnalista();
            }
        }


        private void ProcesarReasignacion()
        {
            try
            {

                foreach (GridViewRow Row in gvSolicitudesParaAsignar.Rows)
                {
                    CheckBox chkAsignar = (CheckBox)Row.FindControl("chkAsignar");
                    if (chkAsignar.Checked)
                    {
                        Anthem.DropDownList ddlAnalistaRiesgo = (Anthem.DropDownList)Row.FindControl("ddlAnalistaRiesgo");
                        int Solicitud_Id = int.Parse(gvSolicitudesParaAsignar.DataKeys[Row.RowIndex].Values["Solicitud_Id"].ToString());

                        if (ddlAnalistaRiesgo.SelectedValue == "-1")
                        {
                            Controles.MostrarMensajeAlerta("Debe Seleccionar un Analista Para la Solicitud " + Solicitud_Id.ToString());
                            return;
                        }
                    }
                }



                foreach (GridViewRow Row in gvSolicitudesParaAsignar.Rows)
                {
                    CheckBox chkAsignar = (CheckBox)Row.FindControl("chkAsignar");
                    if (chkAsignar.Checked)
                    {
                        Anthem.DropDownList ddlAnalistaRiesgo = (Anthem.DropDownList)Row.FindControl("ddlAnalistaRiesgo");
                        int Solicitud_Id = int.Parse(gvSolicitudesParaAsignar.DataKeys[Row.RowIndex].Values["Solicitud_Id"].ToString());
                        if (!GuardaAsignacion(ddlAnalistaRiesgo, Solicitud_Id))
                            return;

                        BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();
                        oBandeja = NegBandejaEntrada.lstBandejaEntrada.FirstOrDefault(b => b.Solicitud_Id == Solicitud_Id);
                        if (!TerminarEventoAsignacion(oBandeja))
                            return;
                    }
                }
                CargarBandejaEntradaAnalista();
                Controles.MostrarMensajeExito("Asignación Procesada Correctamente");
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Procesar la Asignación");
                }

            }
        }
        protected void btnSeleccionarTodas_Click(object sender, EventArgs e)
        {
            SeleccionarSolicitudes(1);
        }
        protected void btnSeleccionarMitad_Click(object sender, EventArgs e)
        {
            SeleccionarSolicitudes(2);
        }
        private void SeleccionarSolicitudes(int Porcentaje)
        {
            try
            {
                foreach (GridViewRow Row in gvSolicitudesParaAsignar.Rows)
                {
                    CheckBox chkAsignar = (CheckBox)Row.FindControl("chkAsignar");
                    chkAsignar.Checked = false;
                }

                for (int i = 0; i < gvSolicitudesParaAsignar.Rows.Count / Porcentaje; i++)
                {
                    CheckBox chkAsignar = (CheckBox)gvSolicitudesParaAsignar.Rows[i].FindControl("chkAsignar");
                    chkAsignar.Checked = true;
                }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Seleccionar Solicitudes");
                }

            }
        }
        protected void btnProcesarReasignacion_Click(object sender, EventArgs e)
        {
            ProcesarReasignacion();
        }



        private void CargarBandejaEntradaAnalista()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaInfo> oResultado = new Resultado<BandejaEntradaInfo>();

                List<BandejaEntradaInfo> lstBandeja = new List<BandejaEntradaInfo>();

                //Asignación de Variables de Búsqueda
                oFiltro.Usuario_Id = (int)NegUsuarios.Usuario.Rut;
                oFiltro.Evento_Id = 152;//Evento Asignación Analista Riesgo


                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    lstBandeja = oResultado.Lista;
                    Controles.CargarGrid<BandejaEntradaInfo>(ref gvSolicitudesParaAsignar, lstBandeja, new string[] { "Id", "Solicitud_Id" });

                    ////Declaracion de Variables
                    var ObjetoUsuario = new UsuarioInfo();
                    var ObjetoResultado = new Resultado<UsuarioInfo>();
                    var NegUsuario = new NegUsuarios();
                    var lstAnalistas = new List<UsuarioInfo>();

                    ////Asignacion de Variables
                    ObjetoUsuario.Rol_Id = Constantes.IdRolAnalistaRiesgo;
                    ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                    ObjetoUsuario.Fabrica_Id = NegUsuarios.Usuario.Fabrica_Id;
                    ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                    if (ObjetoResultado.ResultadoGeneral)
                    {
                        lstAnalistas = ObjetoResultado.Lista;
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    }



                    foreach (GridViewRow Row in gvSolicitudesParaAsignar.Rows)
                    {
                        Anthem.DropDownList ddlAnalistaRiesgo = (Anthem.DropDownList)Row.FindControl("ddlAnalistaRiesgo");
                        Controles.CargarCombo<UsuarioInfo>(ref ddlAnalistaRiesgo, lstAnalistas, "Rut", "Nombre", "", "");

                    }



                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Eventos");
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarBandejaEntradaAnalista();
        }

        private bool GuardaAsignacion(Anthem.DropDownList ddlAnalistaRiesgo, int Solicitud_Id)
        {
            try
            {

                NegSolicitudes oNegocio = new NegSolicitudes();
                AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
                Resultado<AsignacionSolicitudInfo> rAsignacion = new Resultado<AsignacionSolicitudInfo>();


                oAsignacion.Solicitud_Id = Solicitud_Id;
                oAsignacion.Usuario_Id = NegUsuarios.Usuario.Rut;

                //Asinacion de Analista de Riesgo
                oAsignacion.Rol_Id = Constantes.IdRolAnalistaRiesgo;
                oAsignacion.Responsable_Id = int.Parse(ddlAnalistaRiesgo.SelectedValue);
                rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
                if (rAsignacion.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(rAsignacion.Mensaje);
                    return false;
                }



                return true;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
                }
                return false;
            }

        }


        private bool TerminarEventoAsignacion(BandejaEntradaInfo oBandeja)
        {
            try
            {
                FlujoInfo oFlujo = new FlujoInfo();
                NegFlujo nFLujo = new NegFlujo();
                Resultado<FlujoInfo> rFlujo = new Resultado<FlujoInfo>();

                               
                oFlujo.Solicitud_Id = oBandeja.Solicitud_Id;
                oFlujo.Evento_Id = oBandeja.Evento_Id;
                oFlujo.Accion_Id = 5;//Accion Avanzar

                oFlujo.Usuario_Id = NegUsuarios.Usuario.Rut;
                oFlujo.Bandeja_Id = oBandeja.Id;

                rFlujo = nFLujo.TerminarEvento(oFlujo);
                if (!rFlujo.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(rFlujo.Mensaje);
                    return false;
                }
                return true;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Terminar el Evento");
                }
                return false;
            }
        }
    }
}