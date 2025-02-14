using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Aplicaciones
{
    public partial class ConsultaEstadoAvance : System.Web.UI.Page
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
                oResultado = oNeg.Buscar(oFiltro, true);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<SolicitudInfo>(ref gvBusqueda, oResultado.Lista, new string[] { "Id" });


                    lblContador.Text = NegSolicitudes.lstSolicitudConsultaInfo.Count.ToString() + " Registro(s) Encontrado(s)";
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

        private void SeleccionarSolicitud(int Solicitud_Id)
        {
            try
            {
                NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                NegSolicitudes.objSolicitudInfo = NegSolicitudes.lstSolicitudConsultaInfo.FirstOrDefault(b => b.Id.Equals(Solicitud_Id));
                if (NegSolicitudes.objSolicitudInfo != null)
                {
                    Controles.AbrirEvento("~/Aplicaciones/EstadoAvance.aspx", "Consulta Estado Avance - Solicitud: " + NegSolicitudes.objSolicitudInfo.Id.ToString());
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
                    Controles.MostrarMensajeError("Error al cargar Consulta Estado Avance.");
                }
            }
        }


        protected void btnAbrirAvento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
            SeleccionarSolicitud(Id);

        }
    }
}