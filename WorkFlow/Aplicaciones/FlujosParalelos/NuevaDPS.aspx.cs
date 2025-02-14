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
    public partial class NuevaDPS : System.Web.UI.Page
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
                oResultado = oNeg.BuscarSolicitudesDPS(oFiltro, true);
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

        private void IniciarFlujoDPS(int Solicitud_Id)
        {
            try
            {
                NegFlujo nFlujo = new NegFlujo();
                FlujoInfo oFlujo = new FlujoInfo();
                Resultado<FlujoInfo> rFlujo = new Resultado<FlujoInfo>();

                oFlujo.Solicitud_Id = Solicitud_Id;
                oFlujo.TipoFlujo_Id = 1; // Flujo Individual
                rFlujo = nFlujo.IniciarFlujoDPS(oFlujo);
                if (rFlujo.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Flujo Iniciado Correctamente bajo la Solicitud: " + Solicitud_Id.ToString() + ", Consulta la Bandeja de Entrada");
                    CargarSolicitudes();
                    CargarParticipantes(Solicitud_Id);
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
                    Controles.MostrarMensajeError("Error al Inciar el Flujo de DPS.");
                }
            }
        }


        protected void btnAbrirAvento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Solicitud_Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
            CargarParticipantes(Solicitud_Id);


        }

       


        public void CargarParticipantes(int Solicitud_Id)
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oParticipante = new ParticipanteInfo();
                var NegParticipantes = new NegParticipante();
                var oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = Solicitud_Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ParticipanteInfo>(ref gvParticipantes, oResultado.Lista.Where(p=> p.PorcentajeDesgravamen>0 && p.EstadoDps_Id != 1 && p.EstadoDps_Id != 4).ToList(), new string[] { Constantes.StringId, "TipoParticipacion_Id","Solicitud_Id" });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Participantes");
                }
            }
        }
        protected void gvPropiedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void BtnProcesarDPS_Click(object sender, EventArgs e)
        {
            ProcesarInicioDPS();
        }


        private void ProcesarInicioDPS()
        {
            try
            {
                var oSolicitud = new SolicitudInfo();
                var rSolicitud = new Resultado<SolicitudInfo>();
                var nSolicitud = new NegSolicitudes();
                int SolicitudParticipante_Id = 0;
                int TipoParticipacion_Id = 0;
                int ParticipanteSeleccionado_Id = 0;


                var oParticipante = new ParticipanteInfo();
                var nParticipante = new NegParticipante();
                var rParticipante = new Resultado<ParticipanteInfo>();


                foreach (GridViewRow Row in gvParticipantes.Rows)
                {
                    CheckBox chkIniciarFlujo = (CheckBox)Row.FindControl("chkIniciarFlujo");
                    if (chkIniciarFlujo.Checked)
                    {
                        SolicitudParticipante_Id = int.Parse(gvParticipantes.DataKeys[Row.RowIndex].Values["Solicitud_Id"].ToString());
                        TipoParticipacion_Id = int.Parse(gvParticipantes.DataKeys[Row.RowIndex].Values["TipoParticipacion_Id"].ToString());
                        ParticipanteSeleccionado_Id = int.Parse(gvParticipantes.DataKeys[Row.RowIndex].Values["Id"].ToString());

                        oSolicitud.Id = SolicitudParticipante_Id;
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
                                    oParticipante.Solicitud_Id = SolicitudParticipante_Id;
                                    rParticipante = nParticipante.BuscarParticipante(oParticipante);
                                    if (rParticipante.ResultadoGeneral)
                                    {
                                        foreach (var participante in rParticipante.Lista.Where(p=>p.Id == ParticipanteSeleccionado_Id))
                                        {
                                            participante.Id = ParticipanteSeleccionado_Id;
                                            participante.EstadoDps_Id = 4;//En Curso
                                            rParticipante = nParticipante.GuardarParticipante(participante);
                                            if (!rParticipante.ResultadoGeneral)
                                            {
                                                Controles.MostrarMensajeError(rParticipante.Mensaje);
                                                return;
                                            }


                                            participante.Solicitud_Id = oSolicitud.Id;
                                            participante.SolicitudDPS_Id = SolicitudParticipante_Id;
                                            participante.Id = -1;
                                            participante.TipoParticipacion_Id = TipoParticipacion_Id;
                                            participante.EstadoDps_Id = 4;//En Curso
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

                        IniciarFlujoDPS(oSolicitud.Id);
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
                    Controles.MostrarMensajeError("Error al Procesar el Inicio del Flujo de DPS");
                }
            }
        }
    }
}