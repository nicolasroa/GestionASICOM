using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Mantenedores
{
    public partial class TipoGGOO : System.Web.UI.Page
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            Buscar();
        }

        #endregion

        #region METODOS
        /// <summary>
        /// Gastos Operacionales
        /// </summary>
        private void Buscar()
        {
            try
            {
                //var ObjInfo = new SimulacionHipotecariaInfo();
                //ObjInfo.Gracia = int.Parse(ddlMesesGracia.SelectedValue);
                //ObjInfo.ValorPropiedad = string.IsNullOrEmpty(txtPrecioVenta.Text) ? -1 : decimal.Parse(txtPrecioVenta.Text);
                //ObjInfo.SeguroIncendio_Id = int.Parse(ddlSeguroIncendio.SelectedValue);

                //var objResultado = new Resultado<SimulacionHipotecariaInfo>();
                //var objNegocio = new NegSimulacionHipotecaria();


                //objResultado = objNegocio.Buscar(ObjInfo);
                //if (objResultado.ResultadoGeneral)
                //{
                //    Controles.CargarGrid<SimulacionHipotecariaInfo>(ref gvBusqueda, objResultado.Lista, new string[] { "Plazo" });
                //}
                //else
                //{
                //    Controles.MostrarMensajeError(objResultado.Mensaje);
                //}


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message,Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Gastos Operacionales");
                }
            }
        }
#endregion



    }
}