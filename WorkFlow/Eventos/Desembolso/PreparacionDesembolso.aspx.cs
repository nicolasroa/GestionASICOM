using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WorkFlow.Eventos.Desembolso
{
    public partial class PreparacionDesembolso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
           
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = false;
                CargarAcciones();
                CargarDestinoFondo();
                CargarTipoDestinoFondo();
            }
        }
        public void ddlAccionEvento_SelectedIndexChanged(object sender, EventArgs e)
        {

            SeleccionarAccion();
        }
        public void btnAccion_Click(object sender, EventArgs e)
        {
            ProcesarEvento();
        }
        public void CargarAcciones()
        {
            try
            {
                AccionesInfo oAcciones = new AccionesInfo();
                NegAcciones negAcciones = new NegAcciones();
                Resultado<AccionesInfo> oResultado = new Resultado<AccionesInfo>();
                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();

                oBandeja = NegBandejaEntrada.oBandejaEntrada;
                oAcciones.Evento_Id = oBandeja.Evento_Id;

                oResultado = negAcciones.BuscarAccionesEvento(oAcciones);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<AccionesInfo>(ref ddlAccionEvento, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Acciones--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar las Acciones del Evento");
                }
            }
        }
        public void SeleccionarAccion()
        {
            try
            {
                int Accion_Id = int.Parse(ddlAccionEvento.SelectedValue);
                if (Accion_Id == -1)
                {
                    btnAccion.Visible = false;
                    return;
                }
                AccionesInfo oAccion = new AccionesInfo();
                oAccion = NegAcciones.lstAccionesEvento.FirstOrDefault(a => a.Id.Equals(Accion_Id));
                
                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "A"))//AVANZAR
                {
                    btnAccion.Text = "<span class='glyphicon glyphicon-forward'></span>Avanzar";
                    btnAccion.CssClass = "btn btn-sm btn-success";
                    btnAccion.Visible = true;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "D"))//DEVOLVER
                {
                    btnAccion.Text = "<span class='glyphicon glyphicon-backward'></span>Devolver";
                    btnAccion.CssClass = "btn btn-sm btn-primary";
                    btnAccion.Visible = true;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    btnAccion.Text = "<span class='glyphicon glyphicon-trash'></span>Finalizar Solicitud";
                    btnAccion.CssClass = "btn btn-sm btn-danger";
                    btnAccion.Visible = true;
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Acción no Configurada");
                    btnAccion.Visible = false;
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
                    Controles.MostrarMensajeError("Error al Seleccionar la Acción del Evento");
                }
            }
        }
        public void ProcesarEvento()
        {
            try
            {
                int Accion_Id = int.Parse(ddlAccionEvento.SelectedValue);

                AccionesInfo oAccion = new AccionesInfo();
                oAccion = NegAcciones.lstAccionesEvento.FirstOrDefault(a => a.Id.Equals(Accion_Id));

                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "A"))//AVANZAR
                {
                    if (!ValidarLiquidacion()) return;
                    if (!ValidarObservaciones()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!ValidarLiquidacion()) return;
                    if (!ValidarObservaciones()) return;
                }

                FlujoInfo oFlujo = new FlujoInfo();
                NegFlujo nFLujo = new NegFlujo();
                Resultado<FlujoInfo> rFlujo = new Resultado<FlujoInfo>();

                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();

                oBandeja = NegBandejaEntrada.oBandejaEntrada;
                oFlujo.Solicitud_Id = oBandeja.Solicitud_Id;
                oFlujo.Evento_Id = oBandeja.Evento_Id;
                oFlujo.Accion_Id = int.Parse(ddlAccionEvento.SelectedValue);

                oFlujo.Usuario_Id = NegUsuarios.Usuario.Rut;
                oFlujo.Bandeja_Id = oBandeja.Id;

                rFlujo = nFLujo.TerminarEvento(oFlujo);
                if (rFlujo.ResultadoGeneral)
                {

                    NegBandejaEntrada.ActualizarBandeja = true;
                    Controles.CerrarModal(1);
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
                    Controles.MostrarMensajeError("Error al Procesar el Evento");
                }
            }
        }

        protected void ddlTipoDestinoFondo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarBeneficiarios(int.Parse(ddlTipoDestinoFondo.SelectedValue));
        }
        protected void btnModificarDestinoFondo_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDestinoFondos.DataKeys[row.RowIndex].Values["Id"].ToString());
            DestinoFondoInfo oDestino = new DestinoFondoInfo();
            oDestino = NegDestinoFondo.lstDestinoFondos.FirstOrDefault(df => df.Id == Id);
            CargarInfoDestinoFondo(oDestino);
        }
        protected void btnEliminarDestinoFondo_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDestinoFondos.DataKeys[row.RowIndex].Values["Id"].ToString());
            EliminarDestinoFondo(Id);
        }
        private void CargarDestinoFondo()
        {
            try
            {
                NegDestinoFondo nDestino = new NegDestinoFondo();
                DestinoFondoInfo oDestino = new DestinoFondoInfo();
                Resultado<DestinoFondoInfo> rDestino = new Resultado<DestinoFondoInfo>();


                oDestino.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rDestino = nDestino.BuscarDestinoFondo(oDestino);
                if (rDestino.ResultadoGeneral)
                {
                    Controles.CargarGrid<DestinoFondoInfo>(ref gvDestinoFondos, rDestino.Lista, new string[] { "Id" });
                    txtTotalDestinar.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.MontoCredito);
                    txtDestinado.Text = string.Format("{0:F4}", rDestino.Lista.Sum(df => df.MontoUF));
                    txtPorDestinar.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.MontoCredito - rDestino.Lista.Sum(df => df.MontoUF));
                }
                else
                {
                    Controles.MostrarMensajeError(rDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Los Destinos de Fondo");
                }
            }
        }
        private void CargarInfoDestinoFondo(DestinoFondoInfo oDestino)
        {
            try
            {
                if (oDestino != null)
                {
                    NegDestinoFondo.objDestinoFondos = new DestinoFondoInfo();
                    NegDestinoFondo.objDestinoFondos = oDestino;
                    ddlTipoDestinoFondo.SelectedValue = oDestino.TipoDestinoFondo_Id.ToString();
                    CargarBeneficiarios(oDestino.TipoDestinoFondo_Id);
                    ddlBeneficiario.SelectedValue = oDestino.Beneficiario_Id.ToString();
                    txtMontoDestinoFondo.Text = string.Format("{0:F4}", oDestino.MontoUF);
                    NegDestinoFondo.objDestinoFondos = oDestino;

                    var MontoDestinado = decimal.Zero;
                    var MontoPorDestinar = decimal.Zero;

                    if (decimal.TryParse(txtDestinado.Text, out MontoDestinado))
                    {
                        MontoDestinado = MontoDestinado - oDestino.MontoUF;
                        txtDestinado.Text = string.Format("{0:F4}", MontoDestinado);
                    }
                    if (decimal.TryParse(txtPorDestinar.Text, out MontoPorDestinar))
                    {
                        MontoPorDestinar = MontoPorDestinar + oDestino.MontoUF;
                        txtPorDestinar.Text = string.Format("{0:F4}", MontoPorDestinar);
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
                    Controles.MostrarMensajeError("Error al Cargar Datos del Destino de Fondo");
                }
            }
        }
        private bool ValidarIngresoDestinoFondo()
        {
            try
            {
                if (ddlTipoDestinoFondo.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Destino");
                    return false;
                }
                if (ddlBeneficiario.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Beneficiario");
                    return false;
                }
                if (txtMontoDestinoFondo.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto");
                    return false;
                }
                var MontoDestino = decimal.Zero;
                var MontoPorDestinar = decimal.Zero;
                if (decimal.TryParse(txtMontoDestinoFondo.Text, out MontoDestino))
                {
                    if (decimal.TryParse(txtPorDestinar.Text, out MontoPorDestinar))
                    {
                        if (MontoDestino > MontoPorDestinar)
                        {
                            Controles.MostrarMensajeAlerta("El Monto Ingresado no puede superar al Monto por Destinar (" + string.Format("{0:F4}", MontoPorDestinar) + ")");
                            return false;
                        }
                    }
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
                    Controles.MostrarMensajeError("Error al Validar los Datos del Destino de Fondos");
                }
                return false;
            }
        }
        private void CargarTipoDestinoFondo()
        {
            try
            {
                NegDestinoFondo nTipoDestino = new NegDestinoFondo();
                TipoDestinoFondoInfo oTipoDestino = new TipoDestinoFondoInfo();
                Resultado<TipoDestinoFondoInfo> rTipoDestino = new Resultado<TipoDestinoFondoInfo>();

                oTipoDestino.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                rTipoDestino = nTipoDestino.BuscarTipoDestino(oTipoDestino);
                if (rTipoDestino.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoDestinoFondoInfo>(ref ddlTipoDestinoFondo, rTipoDestino.Lista, "Id", "Descripcion", "-- Tipo de Destino --", "-1");
                    Controles.CargarCombo<DestinoFondoInfo>(ref ddlBeneficiario, null, "Beneficiario_Id", "NombreBeneficiario", "-- Beneficiario --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(rTipoDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Tipos de Destino de Fondos");
                }
            }
        }
        private void CargarBeneficiarios(int Id)
        {
            try
            {
                NegDestinoFondo nTipoDestino = new NegDestinoFondo();
                TipoDestinoFondoInfo oTipoDestino = new TipoDestinoFondoInfo();
                Resultado<DestinoFondoInfo> rTipoDestino = new Resultado<DestinoFondoInfo>();

                oTipoDestino = NegDestinoFondo.lstTipoDestinoFondos.FirstOrDefault(tdf => tdf.Id == Id);
                if (oTipoDestino == null)
                {
                    Controles.CargarCombo<DestinoFondoInfo>(ref ddlBeneficiario, null, "Beneficiario_Id", "NombreBeneficiario", "-- Beneficiario --", "-1");
                    return;
                }
                oTipoDestino.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rTipoDestino = nTipoDestino.BuscarBeneficiarioDestino(oTipoDestino);
                if (rTipoDestino.ResultadoGeneral)
                {
                    Controles.CargarCombo<DestinoFondoInfo>(ref ddlBeneficiario, rTipoDestino.Lista, "Beneficiario_Id", "NombreBeneficiario", "-- Beneficiario --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(rTipoDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Beneficiarios");
                }
            }
        }
        private void LimpiarFormularioDestinoFondo()
        {
            ddlTipoDestinoFondo.ClearSelection();
            CargarBeneficiarios(-1);
            txtMontoDestinoFondo.Text = "";
            NegDestinoFondo.objDestinoFondos = null;
        }
        private void GrabarDestinoFondo()
        {
            try
            {
                NegDestinoFondo nDestino = new NegDestinoFondo();
                DestinoFondoInfo oDestino = new DestinoFondoInfo();
                Resultado<DestinoFondoInfo> rDestino = new Resultado<DestinoFondoInfo>();

                if (!ValidarIngresoDestinoFondo()) return;

                if (NegDestinoFondo.objDestinoFondos != null)
                    oDestino = NegDestinoFondo.objDestinoFondos;

                oDestino.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oDestino.TipoDestinoFondo_Id = int.Parse(ddlTipoDestinoFondo.SelectedValue);
                oDestino.Beneficiario_Id = int.Parse(ddlBeneficiario.SelectedValue);
                oDestino.NombreBeneficiario = ddlBeneficiario.SelectedItem.Text;
                oDestino.MontoUF = decimal.Parse(txtMontoDestinoFondo.Text);
                oDestino.Estado_Id = (int)NegTablas.IdentificadorMaestro("EST_DESTINO_FONDO", "I");

                rDestino = nDestino.GuardarDestinoFondo(oDestino);
                if (rDestino.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Grabado Correctamente");
                    LimpiarFormularioDestinoFondo();
                    CargarDestinoFondo();
                }
                else
                {
                    Controles.MostrarMensajeError(rDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar el Destino de Fondo");
                }
            }
        }
        private void EliminarDestinoFondo(int Id)
        {
            try
            {
                NegDestinoFondo nDestino = new NegDestinoFondo();
                DestinoFondoInfo oDestino = new DestinoFondoInfo();
                Resultado<DestinoFondoInfo> rDestino = new Resultado<DestinoFondoInfo>();


                oDestino.Id = Id;

                rDestino = nDestino.GuardarDestinoFondo(oDestino);
                if (rDestino.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    LimpiarFormularioDestinoFondo();
                    CargarDestinoFondo();
                }
                else
                {
                    Controles.MostrarMensajeError(rDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar el Destino de Fondo");
                }
            }
        }
        protected void btnGrabarDestinoFondo_Click(object sender, EventArgs e)
        {
            GrabarDestinoFondo();
        }
        protected void btnCancelarDestinoFondo_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDestinoFondo();
        }
        private bool ValidarObservaciones()
        {
            try
            {
                if (NegObservaciones.lstObservacionInfo.Count(o => o.Evento_Id == NegBandejaEntrada.oBandejaEntrada.Evento_Id && o.Estado_Id == 1) != 0)
                {
                    Controles.MostrarMensajeAlerta("Se deben subsanar todas las Observaciones realizaras en el Evento para continuar");
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
                    Controles.MostrarMensajeError("Error al Validar las Observaciones");
                }
            }
            return false;
        }
        private bool ValidarLiquidacion()
        {
            try
            {
                NegDestinoFondo nDestino = new NegDestinoFondo();
                DestinoFondoInfo oDestino = new DestinoFondoInfo();
                Resultado<DestinoFondoInfo> rDestino = new Resultado<DestinoFondoInfo>();


                oDestino.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rDestino = nDestino.BuscarDestinoFondo(oDestino);
                if (rDestino.ResultadoGeneral)
                {
                    Controles.CargarGrid<DestinoFondoInfo>(ref gvDestinoFondos, rDestino.Lista, new string[] { "Id" });
                    if (NegSolicitudes.objSolicitudInfo.MontoCredito - rDestino.Lista.Sum(df => df.MontoUF) > 0)
                    {
                        Controles.MostrarMensajeAlerta("Se debe Destinar el total de los Fondos para Continuar");
                        return false;
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar el Destino de Fondo");
                }
                return false;
            }
        }
        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
    }
}