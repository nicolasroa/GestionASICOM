using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.DatosGenerales
{
    public partial class DatosReparoRiesgo : System.Web.UI.UserControl
    {

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaObservaciones();
        }

        protected void btnAgregaComentario_Click(object sender, EventArgs e) => AgregaComentario();

        #endregion

        #region Metodos

        private void CargaObservaciones()
        {

            try
            {
                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();
                oBandeja = NegBandejaEntrada.oBandejaEntrada;
                var oInfo = new ObservacionInfo
                {
                    UsuarioIngreso_Id = NegUsuarios.Usuario.Rut.Value,
                    TipoObservacion_Id = 1,
                    Solicitud_Id = oBandeja.Solicitud_Id,
                    Evento_Id = oBandeja.Evento_Id,
                    Estado_Id = 1
                };

                var ObjResultado = new Resultado<ObservacionInfo>();
                var objNegocio = new NegObservaciones();

                ////Asignacion de Variables
                ObjResultado = objNegocio.Buscar(oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gdvComentarios, ObjResultado.Lista, new string[] { "Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
                    return;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
                }
            }

        }

        private void AgregaComentario()
        {

            try
            {
                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();
                oBandeja = NegBandejaEntrada.oBandejaEntrada;

                var oInfo = new ObservacionInfo();
                if (!ValidarFormulario()) { return; }

                oInfo.UsuarioIngreso_Id = NegUsuarios.Usuario.Rut.Value;
                oInfo.TipoObservacion_Id = 2;
                oInfo.Descripcion = txtObservacion.Text;
                oInfo.Solicitud_Id = oBandeja.Solicitud_Id;
                oInfo.Evento_Id = oBandeja.Evento_Id;
                oInfo.Estado_Id = 1;

                var ObjResultado = new Resultado<ObservacionInfo>();
                var objNegocio = new NegObservaciones();

                ////Asignacion de Variables
                ObjResultado = objNegocio.Guardar(ref oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                    CargaObservaciones();

                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
                    return;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
                }
            }
        }

        private bool ValidarFormulario()
        {
            if (txtObservacion.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtObservacion.ClientID);
                return false;
            }
            return true;
        }

        #endregion

    }
}