using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Aplicaciones.FlujosParalelos
{
    public partial class NuevoEstudioDeTitulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                NegBandejaEntrada.oBandejaEntrada = null;
            }
        }

        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            CargarSolicitudes();
        }

        protected void btnBuscar_Click(object sender, EventArgs e) => CargarSolicitudes();


        private void CargarSolicitudes()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                SolicitudInfo oFiltro = new SolicitudInfo();
                NegSolicitudes oNeg = new NegSolicitudes();
                Resultado<SolicitudInfo> oResultado = new Resultado<SolicitudInfo>();

                List<SolicitudInfo> lstSolicitudes = new List<SolicitudInfo>();

                //Asignación de Variables de Búsqueda
                if (txtNumeroSolicitud.Text.Length != 0)
                    oFiltro.Id = int.Parse(txtNumeroSolicitud.Text);
                if (txtRutCliente.Text.Length != 0)
                    oFiltro.Rut = txtRutCliente.Text.Length > 0 ? int.Parse(txtRutCliente.Text.Split('-')[0].ToString()) : -1;
                if (txtNombreCliente.Text.Length != 0)
                    oFiltro.Nombre = txtNombreCliente.Text.Length > 0 ? txtNombreCliente.Text : "";
                if (txtApePatCliente.Text.Length != 0)
                    oFiltro.Paterno = txtApePatCliente.Text.Length > 0 ? txtApePatCliente.Text : "";
                if (txtApeMatCliente.Text.Length != 0)
                    oFiltro.Materno = txtApeMatCliente.Text.Length > 0 ? txtApeMatCliente.Text : "";

                oFiltro.FabricaUsuario_Id = NegUsuarios.Usuario.Fabrica_Id;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarSolicitudesEstudioTitulo(oFiltro, true);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<SolicitudInfo>(ref gvBusqueda, oResultado.Lista, new string[] { "Id" });
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

        private void IniciarFlujoEETT(int Solicitud_Id)
        {
            try
            {
                NegFlujo nFlujo = new NegFlujo();
                FlujoInfo oFlujo = new FlujoInfo();
                Resultado<FlujoInfo> rFlujo = new Resultado<FlujoInfo>();

                oFlujo.Solicitud_Id = Solicitud_Id;
                oFlujo.TipoFlujo_Id = 1; // Flujo Individual
                rFlujo = nFlujo.IniciarFlujoEETT(oFlujo);
                if (rFlujo.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Flujo Iniciado Correctamente bajo la Solicitud: " + Solicitud_Id.ToString() + ", Consulta la Bandeja de Entrada");
                    CargarSolicitudes();
                    CargarPropiedades(Solicitud_Id);
                }
                else
                {
                    Controles.MostrarMensajeError(rFlujo.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Inciar el Flujo de EETT.");
                }
            }
        }


        protected void btnAbrirAvento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Solicitud_Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
            CargarPropiedades(Solicitud_Id);


        }

        public void CargarPropiedades(int Solicitud_Id)
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = Solicitud_Id;
                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    // Solo tasaciones Ingresadas o Terminadas
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedades, rTasacion.Lista.Where(t => (t.EstadoEstudioTitulo_Id == 1 || t.EstadoEstudioTitulo_Id == 3) && t.IndPropiedadPrincipal == true).ToList(), new string[] { "Id", "Propiedad_Id", "Solicitud_Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(rTasacion.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar las Propiedades");
                }
            }
        }
        protected void gvPropiedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void BtnProcesarEETT_Click(object sender, EventArgs e)
        {
            ProcesarInicioEETT();
        }


        private void ProcesarInicioEETT()
        {
            try
            {
                var oSolicitud = new SolicitudInfo();
                var rSolicitud = new Resultado<SolicitudInfo>();
                var nSolicitud = new NegSolicitudes();
                int SolicitudPropiedades_Id = 0;
                int PropiedadSeleccionada_Id = 0;
                int TasacionSeleccionada_Id = 0;


                var oTasacion = new TasacionInfo();
                var rTasacion = new Resultado<TasacionInfo>();
                var nPropiedad = new NegPropiedades();


                var oParticipante = new ParticipanteInfo();
                var nParticipante = new NegParticipante();
                var rParticipante = new Resultado<ParticipanteInfo>();


                foreach (GridViewRow Row in gvPropiedades.Rows)
                {
                    CheckBox chkIniciarFlujo = (CheckBox)Row.FindControl("chkIniciarFlujo");
                    if (chkIniciarFlujo.Checked)
                    {
                        SolicitudPropiedades_Id = int.Parse(gvPropiedades.DataKeys[Row.RowIndex].Values["Solicitud_Id"].ToString());
                        PropiedadSeleccionada_Id = int.Parse(gvPropiedades.DataKeys[Row.RowIndex].Values["Propiedad_Id"].ToString());
                        TasacionSeleccionada_Id = int.Parse(gvPropiedades.DataKeys[Row.RowIndex].Values["Id"].ToString());

                        oSolicitud.Id = SolicitudPropiedades_Id;
                        rSolicitud = nSolicitud.Buscar(oSolicitud);
                        if (rSolicitud.ResultadoGeneral)
                        {
                            oSolicitud = rSolicitud.Lista.FirstOrDefault();
                            oSolicitud.Id = -1;
                            rSolicitud = nSolicitud.Guardar(ref oSolicitud);
                            if (rSolicitud.ResultadoGeneral)
                            {
                                if (oSolicitud != null)
                                {
                                    foreach (var tasa in NegPropiedades.lstTasaciones.Where(t => t.Id == TasacionSeleccionada_Id || t.TasacionPadre_Id == TasacionSeleccionada_Id))
                                    {

                                        tasa.EstadoEstudioTitulo_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS_FLUJOS_PARALELOS", "EF");// En Flujo de Tasación
                                        rTasacion = nPropiedad.GuardarTasacion(tasa);
                                        if (!rTasacion.ResultadoGeneral)
                                        {

                                            Controles.MostrarMensajeError(rTasacion.Mensaje);
                                            return;
                                        }

                                        tasa.Solicitud_Id = oSolicitud.Id;
                                        tasa.SolicitudEstudioTitulo_Id = SolicitudPropiedades_Id;
                                        tasa.Id = -1;
                                        tasa.EstadoEstudioTitulo_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS_FLUJOS_PARALELOS", "EF");// En Flujo de Tasación
                                        rTasacion = nPropiedad.GuardarTasacion(tasa);
                                        if (!rTasacion.ResultadoGeneral)
                                        {

                                            Controles.MostrarMensajeError(rTasacion.Mensaje);
                                            return;
                                        }
                                    }

                                    oParticipante.Solicitud_Id = SolicitudPropiedades_Id;
                                    oParticipante.TipoParticipacion_Id = 1;// Participante Principal
                                    rParticipante = nParticipante.BuscarParticipante(oParticipante);
                                    if (rParticipante.ResultadoGeneral)
                                    {
                                        foreach (var participante in rParticipante.Lista)
                                        {
                                            participante.Solicitud_Id = oSolicitud.Id;
                                            participante.Id = -1;
                                            rParticipante = nParticipante.GuardarParticipante(participante);
                                            if (!rParticipante.ResultadoGeneral)
                                            {
                                                Controles.MostrarMensajeError(rParticipante.Mensaje);
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Controles.MostrarMensajeError(rParticipante.Mensaje);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                Controles.MostrarMensajeError(rSolicitud.Mensaje);
                                return;
                            }
                        }
                        else
                        {
                            Controles.MostrarMensajeError(rSolicitud.Mensaje);
                            return;
                        }

                        IniciarFlujoEETT(oSolicitud.Id);
                    }
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
                    Controles.MostrarMensajeError("Error al Procesar el Inicio del Flujo de EETT");
                }
            }
        }
    }
}