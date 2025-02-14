using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;

namespace WorkFlow.Mantenedores
{
    public partial class GastosOperacionales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarGastos();
            }
        }

        private void CargarGastos()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new GastoOperacionalInfo();
                var ObjResultado = new Resultado<GastoOperacionalInfo>();
                var objNegocio = new NegGastosOperacionales();
                ObjInfo = NegGastosOperacionales.objGastoOperacional;

                ////Asignacion de Variables
                ObjResultado = objNegocio.CalcularGastosSimulacion(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvBusqueda, ObjResultado.Lista, new string[] { "Id" });
                    txtTotalGastos.Text = "$" + string.Format("{0:0,0}", ObjResultado.Lista.Sum(item => item.ValorPesos));
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
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Gastos Operacionales");
                }
            }
        }

        protected void btnCerar_Click(object sender, EventArgs e)
        {
            Controles.CerrarModal();
        }
    }
}