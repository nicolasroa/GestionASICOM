using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Eventos.Desembolso
{
    public partial class Pago : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargarDestinoFondo();
                CargarTipoDestinoFondo();
                CargarFormaPago();
            }
        }

        public void ddlAccionEvento_SelectedIndexChanged(object sender, EventArgs e)
        {

            SeleccionarAccion();
        }
        protected void btnAccion_Click(object sender, EventArgs e)
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
                    if (!ValidarPagoLiquidacion()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!ValidarPagoLiquidacion()) return;
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

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }

        private bool ValidarPagoLiquidacion()
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
                    if (rDestino.Lista.Where(df => df.DescripcionFormaPago == "").Count() > 0)
                    {
                        Controles.MostrarMensajeAlerta("Se debe estacificar la Forma de Pago de Todos los Beneficiarios para Continuar");
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

        private void CargarFormaPago()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("FORMA_PAGO");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlFormaPago, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Forma de Pago --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Forma de Pagos Sin Datos");
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Forma de Pago");
                }
            }
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
                    txtTotalDestinar.Text = string.Format("{0:C0}", NegSolicitudes.objSolicitudInfo.MontoLiquidacionPesos);
                    txtDestinado.Text = string.Format("{0:C0}", rDestino.Lista.Sum(df => df.MontoPesos));
                    txtPorDestinar.Text = string.Format("{0:C0}", NegSolicitudes.objSolicitudInfo.MontoLiquidacionPesos - rDestino.Lista.Sum(df => df.MontoPesos));
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
                    txtMontoDestinoFondo.Text = string.Format("{0:C0}", oDestino.MontoPesos);
                    ddlFormaPago.SelectedValue = oDestino.FormaPago_Id.ToString();
                    NegDestinoFondo.objDestinoFondos = oDestino;

                    var MontoDestinado = decimal.Zero;
                    var MontoPorDestinar = decimal.Zero;

                    if (decimal.TryParse(txtDestinado.Text, out MontoDestinado))
                    {
                        MontoDestinado = MontoDestinado - oDestino.MontoPesos;
                        txtDestinado.Text = string.Format("{0:F4}", MontoDestinado);
                    }


                    if (decimal.TryParse(txtPorDestinar.Text, out MontoPorDestinar))
                    {
                        MontoPorDestinar = MontoPorDestinar + oDestino.MontoPesos;
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
                if (ddlFormaPago.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Forma de Pago");
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
            ddlFormaPago.ClearSelection();
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
                oDestino.FormaPago_Id = int.Parse(ddlFormaPago.SelectedValue);
                oDestino.Estado_Id = (int)NegTablas.IdentificadorMaestro("EST_DESTINO_FONDO", "P");

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
    }
}