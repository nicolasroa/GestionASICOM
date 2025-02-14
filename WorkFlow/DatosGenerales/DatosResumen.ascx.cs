using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace WorkFlow.DatosGenerales
{
    public partial class DatosResumen : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {

                CargaComboCooperativa();
                CargaComboInmobiliaria();
                CargarDatos();
            }
        }

        protected void ddlInmobiliaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInmobiliaria.SelectedValue == "-1")
            {
                Controles.CargarCombo<UsuarioInfo>(ref ddlProyecto, null, "Id", "Descripcion", "-- Seleccione una Inmobiliaria", "-1");
            }
            else
            {
                CargaComboProyecto(int.Parse(ddlInmobiliaria.SelectedValue));
            }
        }
        private void CargaComboCooperativa()
        {

            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("COOP");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlCooperativa, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- No Aplica --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Cooperativa Sin Datos");
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Cooperativas");
                }
            }
        }
        private void CargaComboInmobiliaria()
        {
            try
            {
                ////Declaracion de Variables
                var objInmobiliaria = new InmobiliariaInfo();
                var objResultado = new Resultado<InmobiliariaInfo>();
                var NegInmobiliaria = new NegInmobiliarias();

                ////Asignacion de Variables
                objInmobiliaria.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                objResultado = NegInmobiliaria.Buscar(objInmobiliaria);
                if (objResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<InmobiliariaInfo>(ref ddlInmobiliaria, objResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(objResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Inmobiliaria");
                }
            }
        }
        private void CargaComboProyecto(int Inmbobiliaria_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ProyectoInfo();
                var ObjResultado = new Resultado<ProyectoInfo>();
                var NegProyecto = new NegProyectos();

                ////Asignacion de Variables
                ObjInfo.Inmobiliaria_Id = Inmbobiliaria_Id;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = NegProyecto.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProyectoInfo>(ref ddlProyecto, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Proyecto");
                }
            }
        }
        private void CargarDatos()
        {
            SolicitudInfo oInfo = new SolicitudInfo();
            oInfo.Id = NegSolicitudes.objSolicitudInfo.Id;
            NegSolicitudes oNegSolicitud = new NegSolicitudes();
            Resultado<SolicitudInfo> oResultado = new Resultado<SolicitudInfo>();
            AsignacionSolicitudInfo oAsignacionSolicitud = new AsignacionSolicitudInfo();
            Resultado<AsignacionSolicitudInfo> oResultadoAsignacion = new Resultado<AsignacionSolicitudInfo>();

            oAsignacionSolicitud.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
            try
            {
                oResultado = oNegSolicitud.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    lblNroSolicitud.Text = oResultado.Lista[0].Id.ToString();
                    lblNombre.Text = oResultado.Lista[0].NombreCliente;
                    lblRut.Text = oResultado.Lista[0].RutCliente;
                    lblSucursal.Text = oResultado.Lista[0].NombreSucursal;

                    lblEstado.Text = oResultado.Lista[0].DescripcionEstado;

                    lblSubEstado.Text = oResultado.Lista[0].DescripcionSubEstado;

                    ddlCooperativa.SelectedValue = oResultado.Lista[0].Cooperativa_Id.ToString();
                    ddlInmobiliaria.SelectedValue = oResultado.Lista[0].Inmobiliaria_Id.ToString();
                    CargaComboProyecto(oResultado.Lista[0].Inmobiliaria_Id);
                    ddlProyecto.SelectedValue = oResultado.Lista[0].Proyecto_Id.ToString();
                    lblNotaria.Text = oResultado.Lista[0].DescripcionNotaria == "" ? "Sin Notaría Ingresada" : oResultado.Lista[0].DescripcionNotaria;


                }

                oResultadoAsignacion = oNegSolicitud.BuscarAsignacion(oAsignacionSolicitud);
                if (oResultadoAsignacion.ResultadoGeneral)
                {
                    Controles.CargarGrid<AsignacionSolicitudInfo>(ref gvAsignacion, oResultadoAsignacion.Lista, new string[] { "Id" });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Datos Resumen");
                }
            }


        }

        protected void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            ActualizarDatosSolicitud();
        }

        private void ActualizarDatosSolicitud()
        {
            try
            {
                SolicitudInfo oSolicitud = new SolicitudInfo();
                NegSolicitudes negSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> oResultadoSolicitud = new Resultado<SolicitudInfo>();
                oSolicitud = NegSolicitudes.objSolicitudInfo;
                oSolicitud.Inmobiliaria_Id = int.Parse(ddlInmobiliaria.SelectedValue);
                oSolicitud.Proyecto_Id = int.Parse(ddlProyecto.SelectedValue);
                oSolicitud.Cooperativa_Id = int.Parse(ddlCooperativa.SelectedValue);



                oResultadoSolicitud = negSolicitud.Guardar(ref oSolicitud);
                if (oResultadoSolicitud.ResultadoGeneral)
                {
                    NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                    NegSolicitudes.objSolicitudInfo = oSolicitud;

                    Controles.MostrarMensajeExito("Datos de la Solicitud Grabados");
                }
                else
                {
                    Controles.MostrarMensajeError(oResultadoSolicitud.Mensaje);

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
                    Controles.MostrarMensajeError("Error al Grabar Datos de la Solicitud");
                }
            }
        }
    }
}