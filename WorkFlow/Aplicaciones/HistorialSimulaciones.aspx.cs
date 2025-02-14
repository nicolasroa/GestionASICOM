using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Aplicaciones
{
    public partial class HistorialSimulaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarSimulaciones();
            }
        }


        private void CargarSimulaciones()
        {
            try
            {
                var ObjInfo = new SimulacionHipotecariaInfo();
                var objResultado = new Resultado<SimulacionHipotecariaInfo>();
                var objNegocio = new NegSimulacionHipotecaria();


               

                ObjInfo.RutCliente = NegClientes.objClienteInfo.Rut;
                objResultado = objNegocio.Buscar(ObjInfo);

                if (objResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<SimulacionHipotecariaInfo>(ref gvBusqueda, objResultado.Lista, new string[] { "Id" });

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
                    Controles.MostrarMensajeError("Error al Cargar las Simulaciones");
                }
            }
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {

            GridViewRow row = ((Anthem.LinkButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
            NegSimulacionHipotecaria.oSimulacion = new SimulacionHipotecariaInfo();
            NegSimulacionHipotecaria.oSimulacion = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Id == Id);
            NegSimulacionHipotecaria.indSeleccionSimulacionCliente = true;
            Controles.CerrarModal(1);

        }
    }
}