using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace WorkFlow.DatosGenerales
{
    public partial class EstadoSituacion : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = false;


                CargarInstitucionesFinancieras();

                CargaComboMoneda();
                CargaComboRenta();
                DefinirFormatoMoneda(ref txtMontoTotalActivo, int.Parse(ddlMonedaActivo.SelectedValue), 0);
                DefinirFormatoMoneda(ref txtMontoTotalPasivo, int.Parse(ddlMonedaPasivo.SelectedValue), 0);
                DefinirFormatoMoneda(ref txtCuotaMensual, int.Parse(ddlMonedaPasivo.SelectedValue), 0);
                DefinirFormatoMoneda(ref txtMes1, 999, 0);
                DefinirFormatoMoneda(ref txtTotalPasivosCMF, 999, 0);
                CargarMesValorizacionCMF();
                //DefinirFormatoMoneda(ref txtMes2, 999, 0);
                //DefinirFormatoMoneda(ref txtMes3, 999, 0);
                //DefinirFormatoMoneda(ref txtMes4, 999, 0);
                //DefinirFormatoMoneda(ref txtMes5, 999, 0);
                //DefinirFormatoMoneda(ref txtMes6, 999, 0);
            }
        }

        protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(AgdvParticipante.DataKeys[row.RowIndex].Values["Id"].ToString());
            hdnIdParticipante.Value = Id.ToString();

            ParticipanteInfo oParticipante = new ParticipanteInfo();
            oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Id == Id);
            NegParticipante.ObjParticipante = oParticipante;
            hdnIdParticipante.Value = oParticipante.Rut.ToString();
            CargaComboTipoActivo(oParticipante.TipoPersona_Id);
            CargaComboTipoPasivo(oParticipante.TipoPersona_Id);
            CargaActivos(oParticipante.Rut);
            CargaPasivos(oParticipante.Rut);
            CargaRentas(oParticipante.Rut);
            CargarPasivosCMF(oParticipante);
            lblIngresoDeuda.Text = string.Format("Ingreso Deuda - {0} ({1})", oParticipante.NombreCliente, oParticipante.DescripcionTipoParticipante);

        }
        protected void AbtnAgregaActivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatosActivos()) return;

                ParticipanteInfo oParticipante = new ParticipanteInfo();
                oParticipante = NegParticipante.ObjParticipante;
                if (oParticipante == null || oParticipante.Rut == -1)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                    return;
                }
                NegActivosCliente nGenerales = new NegActivosCliente();
                ActivosClienteInfo oActivo = new ActivosClienteInfo();
                Resultado<ActivosClienteInfo> rGenerales = new Resultado<ActivosClienteInfo>();

                if (NegActivosCliente.objActivosClienteInfo != null)
                {
                    oActivo = NegActivosCliente.objActivosClienteInfo;
                }

                oActivo.TipoActivo_Id = int.Parse(ddlTipoActivo.SelectedValue);
                oActivo.Moneda_Id = int.Parse(ddlMonedaActivo.SelectedValue);
                oActivo.MontoTotal = decimal.Parse(txtMontoTotalActivo.Text);
                oActivo.Observacion = txtObservacionActivo.Text;
                oActivo.RutCliente = oParticipante.Rut;

                rGenerales = nGenerales.Guardar(ref oActivo);

                if (rGenerales.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Datos Guardados Correctamente");
                    CargaActivos(oParticipante.Rut);
                    LimpiarFormularioActivo();
                    NegActivosCliente.objActivosClienteInfo = null;
                    //Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusquedaPart();", true);                    
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
                    Controles.MostrarMensajeError("Error al Confirmar la Información");
                }

            }
        }
        protected void btnModificarActivo_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvActivos.DataKeys[row.RowIndex].Values["Id"].ToString());
            ObtenerDatosActivos(Id);

        }
        protected void btnEliminarActivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
                int Id = int.Parse(gvActivos.DataKeys[row.RowIndex].Values["Id"].ToString());
                NegActivosCliente nActivo = new NegActivosCliente();
                ActivosClienteInfo oActivo = new ActivosClienteInfo();
                Resultado<ActivosClienteInfo> rActivo = new Resultado<ActivosClienteInfo>();

                oActivo.Id = Id;

                rActivo = nActivo.Eliminar(oActivo);
                if (rActivo.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    ParticipanteInfo oParticipante = new ParticipanteInfo();
                    oParticipante = NegParticipante.ObjParticipante;
                    CargaActivos(oParticipante.Rut);
                }
                else
                {
                    Controles.MostrarMensajeError(rActivo.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Eliminar el Activo");
                }
            }
        }
        protected void gvActivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMtoTotal = (Label)e.Row.FindControl("lblMtoTotal");
                var monto = decimal.Parse(lblMtoTotal.Text);
                var moneda = e.Row.Cells[1].Text;

                if (moneda == "Pesos")
                {
                    lblMtoTotal.Text = String.Format("{0:0,0}", monto);
                }
                else
                {
                    lblMtoTotal.Text = String.Format("{0:0,0.0000}", monto);
                }
            }
        }
        protected void ddlMonedaActivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefinirFormatoMoneda(ref txtMontoTotalActivo, int.Parse(ddlMonedaActivo.SelectedValue), 0);

        }
        protected void AbtnAgregaPasivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatosPasivos()) return;

                ParticipanteInfo oParticipante = new ParticipanteInfo();
                oParticipante = NegParticipante.ObjParticipante;
                if (oParticipante == null || oParticipante.Rut == -1)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                    return;
                }
                NegPasivosCliente nGenerales = new NegPasivosCliente();
                PasivosClienteInfo oPasivo = new PasivosClienteInfo();
                Resultado<PasivosClienteInfo> rGenerales = new Resultado<PasivosClienteInfo>();

                if (NegPasivosCliente.objPasivosClienteInfo != null)
                {
                    oPasivo = NegPasivosCliente.objPasivosClienteInfo;
                }

                oPasivo.TipoPasivo_Id = int.Parse(ddlTipoPasivo.SelectedValue);
                oPasivo.Moneda_Id = int.Parse(ddlMonedaPasivo.SelectedValue);
                oPasivo.MontoTotal = decimal.Parse(txtMontoTotalPasivo.Text);
                oPasivo.CuotaMensual = decimal.Parse(txtCuotaMensual.Text);
                oPasivo.Institucion_Id = int.Parse(ddlInstitucionPasivo.SelectedValue);
                oPasivo.Observacion = txtObservacionPasivo.Text;
                oPasivo.RutCliente = oParticipante.Rut;

                rGenerales = nGenerales.Guardar(ref oPasivo);

                if (rGenerales.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Datos Guardados Correctamente");
                    CargaPasivos(oParticipante.Rut);
                    LimpiarFormularioPasivo();
                    NegPasivosCliente.objPasivosClienteInfo = null;
                    //Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusquedaPart();", true);                    
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
                    Controles.MostrarMensajeError("Error al Confirmar la Información");
                }

            }
        }
        protected void btnModificarPasivo_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvPasivos.DataKeys[row.RowIndex].Values["Id"].ToString());
            ObtenerDatosPasivos(Id);
        }
        protected void btnEliminarPasivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
                int Id = int.Parse(gvPasivos.DataKeys[row.RowIndex].Values["Id"].ToString());
                NegPasivosCliente nPasivo = new NegPasivosCliente();
                PasivosClienteInfo oPasivo = new PasivosClienteInfo();
                Resultado<PasivosClienteInfo> rPasivo = new Resultado<PasivosClienteInfo>();

                oPasivo.Id = Id;

                rPasivo = nPasivo.Eliminar(oPasivo);
                if (rPasivo.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    ParticipanteInfo oParticipante = new ParticipanteInfo();
                    oParticipante = oParticipante = NegParticipante.ObjParticipante;
                    CargaPasivos(oParticipante.Rut);
                }
                else
                {
                    Controles.MostrarMensajeError(rPasivo.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Eliminar el Pasivo");
                }
            }
        }
        protected void gvPasivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMtoTotalP = (Label)e.Row.FindControl("lblMtoTotalP");
                Label lblCuotaMensual = (Label)e.Row.FindControl("lblCuotaMensual");
                var monto = decimal.Parse(lblMtoTotalP.Text);
                var cuota = decimal.Parse(lblCuotaMensual.Text);

                var moneda = e.Row.Cells[2].Text;

                if (moneda == "Pesos")
                {
                    lblMtoTotalP.Text = String.Format("{0:0,0}", monto);
                    lblCuotaMensual.Text = String.Format("{0:0,0}", cuota);
                }
                else
                {
                    lblMtoTotalP.Text = String.Format("{0:0,0.0000}", monto);
                    lblCuotaMensual.Text = String.Format("{0:0,0.0000}", cuota);
                }
            }
        }
        protected void ddlMonedaPasivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefinirFormatoMoneda(ref txtMontoTotalPasivo, int.Parse(ddlMonedaPasivo.SelectedValue), 0);
            DefinirFormatoMoneda(ref txtCuotaMensual, int.Parse(ddlMonedaPasivo.SelectedValue), 0);
        }
        protected void AbtnAgregaRenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatosRentas()) return;

                ParticipanteInfo oParticipante = new ParticipanteInfo();
                oParticipante = NegParticipante.ObjParticipante;
                if (oParticipante == null || oParticipante.Rut == -1)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                    return;
                }

                NegRentasCliente nGenerales = new NegRentasCliente();
                RentasClienteInfo oRenta = new RentasClienteInfo();
                Resultado<RentasClienteInfo> rGenerales = new Resultado<RentasClienteInfo>();

                if (NegRentasCliente.objRentasClienteInfo != null)
                {
                    oRenta = NegRentasCliente.objRentasClienteInfo;
                }

                oRenta.TipoRenta_Id = int.Parse(ddlTipoRenta.SelectedValue);
                oRenta.Mes1 = decimal.Parse(txtMes1.Text);
                //oRenta.Mes2 = decimal.Parse(txtMes2.Text);
                //oRenta.Mes3 = decimal.Parse(txtMes3.Text);
                //oRenta.Mes4 = decimal.Parse(txtMes4.Text);
                //oRenta.Mes5 = decimal.Parse(txtMes5.Text);
                //oRenta.Mes6 = decimal.Parse(txtMes6.Text);
                oRenta.RutCliente = oParticipante.Rut;

                rGenerales = nGenerales.Guardar(ref oRenta);

                if (rGenerales.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Datos Guardados Correctamente");
                    CargaRentas(oParticipante.Rut);
                    LimpiarFormularioRenta();
                    NegRentasCliente.objRentasClienteInfo = null;
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
                    Controles.MostrarMensajeError("Error al Confirmar la Información");
                }

            }
        }
        protected void btnModificarRenta_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvRentas.DataKeys[row.RowIndex].Values["Id"].ToString());
            ObtenerDatosRentas(Id);
        }
        protected void btnEliminarRenta_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
                int Id = int.Parse(gvRentas.DataKeys[row.RowIndex].Values["Id"].ToString());
                NegRentasCliente nRenta = new NegRentasCliente();
                RentasClienteInfo oRenta = new RentasClienteInfo();
                Resultado<RentasClienteInfo> rRenta = new Resultado<RentasClienteInfo>();

                oRenta.Id = Id;

                rRenta = nRenta.Eliminar(oRenta);
                if (rRenta.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    ParticipanteInfo oParticipante = new ParticipanteInfo();
                    oParticipante = NegParticipante.ObjParticipante;
                    CargaRentas(oParticipante.Rut);
                }
                else
                {
                    Controles.MostrarMensajeError(rRenta.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Eliminar la renta");
                }
            }
        }







        private void ObtenerDatosActivos(int Id)
        {
            try
            {
                ActivosClienteInfo oActivo = new ActivosClienteInfo();

                oActivo = NegActivosCliente.lstActivosClienteInfo.FirstOrDefault(p => p.Id == Id);
                NegActivosCliente.objActivosClienteInfo = oActivo;

                if (oActivo != null)
                {
                    ddlTipoActivo.SelectedValue = oActivo.TipoActivo_Id.ToString();
                    ddlMonedaActivo.SelectedValue = oActivo.Moneda_Id.ToString();
                    if (oActivo.Moneda_Id == 999)
                        txtMontoTotalActivo.Text = String.Format("{0:0,0}", oActivo.MontoTotal);
                    else
                        txtMontoTotalActivo.Text = String.Format("{0:F4}", oActivo.MontoTotal);
                    txtObservacionActivo.Text = oActivo.Observacion;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Evento");
                }
            }
        }
        private void ObtenerDatosPasivos(int Id)
        {
            try
            {
                PasivosClienteInfo oPasivo = new PasivosClienteInfo();

                oPasivo = NegPasivosCliente.lstPasivosClienteInfo.FirstOrDefault(p => p.Id == Id);
                NegPasivosCliente.objPasivosClienteInfo = oPasivo;

                if (oPasivo != null)
                {
                    ddlTipoPasivo.SelectedValue = oPasivo.TipoPasivo_Id.ToString();
                    ddlMonedaPasivo.SelectedValue = oPasivo.Moneda_Id.ToString();

                    if (oPasivo.Moneda_Id == 999)
                    {
                        txtMontoTotalPasivo.Text = String.Format("{0:0,0}", oPasivo.MontoTotal);
                        txtCuotaMensual.Text = String.Format("{0:0,0}", oPasivo.CuotaMensual);
                    }
                    else
                    {
                        txtMontoTotalPasivo.Text = String.Format("{0:F4}", oPasivo.MontoTotal);
                        txtCuotaMensual.Text = String.Format("{0:F4}", oPasivo.CuotaMensual);
                    }
                    ddlInstitucionPasivo.SelectedValue = oPasivo.Institucion_Id.ToString();
                    txtObservacionPasivo.Text = oPasivo.Observacion;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Evento");
                }
            }
        }

        private void ObtenerDatosRentas(int Id)
        {
            try
            {
                RentasClienteInfo oRenta = new RentasClienteInfo();

                oRenta = NegRentasCliente.lstRentasClienteInfo.FirstOrDefault(p => p.Id == Id);
                NegRentasCliente.objRentasClienteInfo = oRenta;

                if (oRenta != null)
                {
                    ddlTipoRenta.SelectedValue = oRenta.TipoRenta_Id.ToString();
                    txtMes1.Text = string.Format("{0:F0}", oRenta.Mes1);
                    //txtMes2.Text = string.Format("{0:F0}", oRenta.Mes2);
                    //txtMes3.Text = string.Format("{0:F0}", oRenta.Mes3);
                    //txtMes4.Text = string.Format("{0:F0}", oRenta.Mes4);
                    //txtMes5.Text = string.Format("{0:F0}", oRenta.Mes5);
                    //txtMes6.Text = string.Format("{0:F0}", oRenta.Mes6);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Evento");
                }
            }
        }
        private bool ValidarDatosActivos()
        {
            try
            {


                if (NegParticipante.ObjParticipante == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                    return false;
                }

                if (ddlTipoActivo.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar el Tipo de Activo");
                    return false;
                }

                if (ddlMonedaActivo.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar el Tipo de Moneda");
                    return false;
                }


                if (txtMontoTotalActivo.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Total de Activo");
                    return false;
                }

                ActivosClienteInfo oActivo = new ActivosClienteInfo();

                if (NegActivosCliente.lstActivosClienteInfo != null)
                {
                    oActivo = NegActivosCliente.lstActivosClienteInfo.FirstOrDefault(p => p.TipoActivo_Id == int.Parse(ddlTipoActivo.SelectedValue));// Agregar Institudcion

                    if (oActivo != null && NegActivosCliente.objActivosClienteInfo == null)
                    {
                        Controles.MostrarMensajeAlerta("Tipo activo ya se encuentra ingresado");
                        return false;
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
                    Controles.MostrarMensajeError("Error al Validar Datos Personales");
                }
                return false;
            }
        }
        private bool ValidarDatosPasivos()
        {
            try
            {
                if (NegParticipante.ObjParticipante == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                    return false;
                }
                if (ddlTipoPasivo.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar el Tipo de Pasivo");
                    return false;
                }

                if (ddlMonedaPasivo.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar el Tipo de Moneda");
                    return false;
                }


                if (txtMontoTotalPasivo.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Total de Pasivo");
                    return false;
                }

                if (txtCuotaMensual.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Cuota Mensual");
                    return false;
                }

                PasivosClienteInfo oPasivo = new PasivosClienteInfo();

                if (NegPasivosCliente.lstPasivosClienteInfo != null)
                {
                    oPasivo = NegPasivosCliente.lstPasivosClienteInfo.FirstOrDefault(p => p.TipoPasivo_Id == int.Parse(ddlTipoPasivo.SelectedValue));

                    if (oPasivo != null && NegPasivosCliente.objPasivosClienteInfo == null)
                    {
                        Controles.MostrarMensajeAlerta("Tipo pasivo ya se encuentra ingresado");
                        return false;
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
                    Controles.MostrarMensajeError("Error al Validar Datos Personales");
                }
                return false;
            }
        }
        private bool ValidarDatosRentas()
        {
            try
            {
                if (NegParticipante.ObjParticipante == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                    return false;
                }
                if (ddlTipoRenta.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar el Tipo de Renta");
                    return false;
                }

                if (txtMes1.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el monto de la Renta Promedio");
                    return false;
                }

                //if (txtMes2.Text.Length == 0)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar el monto de renta del mes 2");
                //    return false;
                //}

                //if (txtMes3.Text.Length == 0)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar el monto de renta del mes 3");
                //    return false;
                //}

                //if (txtMes4.Text.Length == 0)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar el monto de renta del mes 4");
                //    return false;
                //}

                //if (txtMes5.Text.Length == 0)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar el monto de renta del mes 5");
                //    return false;
                //}

                //if (txtMes6.Text.Length == 0)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar el monto de renta del mes 6");
                //    return false;
                //}

                RentasClienteInfo oRenta = new RentasClienteInfo();

                if (NegRentasCliente.lstRentasClienteInfo != null)
                {
                    oRenta = NegRentasCliente.lstRentasClienteInfo.FirstOrDefault(p => p.TipoRenta_Id == int.Parse(ddlTipoRenta.SelectedValue));

                    if (oRenta != null && NegRentasCliente.objRentasClienteInfo == null)
                    {
                        Controles.MostrarMensajeAlerta("Tipo de renta ya se encuentra ingresado");
                        return false;
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
                    Controles.MostrarMensajeError("Error al Validar Datos Personales");
                }
                return false;
            }
        }
        private void CargaComboTipoActivo(int TipoPersona_Id)
        {
            try
            {
                TipoActivoInfo oTipoActivo = new TipoActivoInfo();
                NegActivosCliente nActivos = new NegActivosCliente();
                Resultado<TipoActivoInfo> rTipoActivo = new Resultado<TipoActivoInfo>();

                oTipoActivo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oTipoActivo.TipoPersona_Id = TipoPersona_Id;

                rTipoActivo = nActivos.BuscarTipo(oTipoActivo);
                if (rTipoActivo.ResultadoGeneral)
                    Controles.CargarCombo<TipoActivoInfo>(ref ddlTipoActivo, rTipoActivo.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Tipo Activo --", "-1");
                else
                    Controles.MostrarMensajeAlerta("Catálogo Tipo Activo Sin Datos");

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Tipo Activo");
                }
            }
        }
        private void CargaComboTipoPasivo(int TipoPersona_Id)
        {
            try
            {
                TipoPasivoInfo oTipoPasivo = new TipoPasivoInfo();
                NegPasivosCliente nPasivos = new NegPasivosCliente();
                Resultado<TipoPasivoInfo> rTipoPasivo = new Resultado<TipoPasivoInfo>();

                oTipoPasivo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oTipoPasivo.TipoPersona_Id = TipoPersona_Id;

                rTipoPasivo = nPasivos.BuscarTipo(oTipoPasivo);
                if (rTipoPasivo.ResultadoGeneral)
                    Controles.CargarCombo<TipoPasivoInfo>(ref ddlTipoPasivo, rTipoPasivo.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Tipo Pasivo --", "-1");
                else
                    Controles.MostrarMensajeAlerta("Catálogo Tipo Pasivo Sin Datos");
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Tipo Pasivo");
                }
            }
        }
        private void CargarInstitucionesFinancieras()
        {
            try
            {
                NegInstitucionesFinancieras oNegocio = new NegInstitucionesFinancieras();
                InstitucionesFinancierasBase oFiltro = new InstitucionesFinancierasBase();
                Resultado<InstitucionesFinancierasBase> oResultado = new Resultado<InstitucionesFinancierasBase>();

                oResultado = oNegocio.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo(ref ddlInstitucionPasivo, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Institución Financiera --", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Tasadores");
                }
            }
        }
        private void CargaComboRenta()
        {
            TipoRentaInfo oInfo = new TipoRentaInfo();
            NegTipoRenta oNegTipoRenta = new NegTipoRenta();
            Resultado<TipoRentaInfo> oResultado = new Resultado<TipoRentaInfo>();
            try
            {
                oResultado = oNegTipoRenta.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoRentaInfo>(ref ddlTipoRenta, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Tipo Renta --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Clientes");
                }
            }
        }
        private void CargaComboMoneda()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("MONEDAS");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlMonedaActivo, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Moneda --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlMonedaPasivo, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Moneda --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Monedas Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Monedas");
                }
            }


        }
        private void CargaActivos(int RutCliente)
        {
            ActivosClienteInfo oInfo = new ActivosClienteInfo();
            oInfo.RutCliente = RutCliente;
            NegActivosCliente oNegCliente = new NegActivosCliente();
            Resultado<ActivosClienteInfo> oResultado = new Resultado<ActivosClienteInfo>();
            try
            {
                oResultado = oNegCliente.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvActivos, oResultado.Lista, new string[] { "Id" });

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Clientes");
                }
            }
        }
        private void CargaPasivos(int RutCliente)
        {
            PasivosClienteInfo oInfo = new PasivosClienteInfo();
            oInfo.RutCliente = RutCliente;
            NegPasivosCliente oNegCliente = new NegPasivosCliente();
            Resultado<PasivosClienteInfo> oResultado = new Resultado<PasivosClienteInfo>();
            try
            {
                oResultado = oNegCliente.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvPasivos, oResultado.Lista, new string[] { "Id" });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Clientes");
                }
            }
        }
        private void CargarPasivosCMF(ParticipanteInfo oParticipante)
        {
            try
            {
                txtTotalPasivosCMF.Text = string.Format("{0:F0}",oParticipante.TotalPasivosCMF);
                ddlFechaValorizacionCMF.SelectedValue = oParticipante.MesPasivoCMF.ToString() + "-" + oParticipante.AñoPasivoCMF.ToString();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar el total de Pasivos CMF");
                }
            }
        }
        private void CargaRentas(int RutCliente)
        {
            RentasClienteInfo oInfo = new RentasClienteInfo();
            oInfo.RutCliente = RutCliente;
            NegRentasCliente oNegCliente = new NegRentasCliente();
            Resultado<RentasClienteInfo> oResultado = new Resultado<RentasClienteInfo>();
            try
            {
                oResultado = oNegCliente.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvRentas, oResultado.Lista, new string[] { "Id" });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Rentas");
                }
            }
        }

        private void LimpiarFormularioActivo()
        {
            ddlTipoActivo.SelectedIndex = 0;
            ddlMonedaActivo.SelectedIndex = 0;
            txtMontoTotalActivo.Text = "";
            txtObservacionActivo.Text = "";

        }

        private void LimpiarFormularioPasivo()
        {
            ddlTipoPasivo.SelectedIndex = 0;
            ddlMonedaPasivo.SelectedIndex = 0;
            txtMontoTotalPasivo.Text = "";
            txtCuotaMensual.Text = "";
            ddlInstitucionPasivo.ClearSelection();
            txtObservacionPasivo.Text = "";
         
        }

        private void LimpiarFormularioRenta()
        {
            ddlTipoRenta.SelectedIndex = 0;
            txtMes1.Text = "";
            //txtMes2.Text = "";
            //txtMes3.Text = "";
            //txtMes4.Text = "";
            //txtMes5.Text = "";
            //txtMes6.Text = "";
        }

        private void DefinirFormatoMoneda(ref Anthem.TextBox Control, int Moneda_Id, decimal Monto)
        {
            if (Moneda_Id == 998) // UF
            {

                Control.Enabled = true;

                Control.Attributes["onKeyPress"] = "return SoloNumeros(event,this.value,4,this);";
                //Control.Attributes.Add("onKeyPress", "return SoloNumeros(event,this.value,4,this);");
            }
            else if (Moneda_Id == 999)//Pesos
            {
                Control.Enabled = true;

                Control.Attributes["onKeyPress"] = "return SoloNumeros(event,this.value,0,this);";
                //Control.Attributes.Add("onKeyPress", "return SoloNumeros(event,this.value,0,this);");
            }
            else
            {
                Control.Enabled = false;
                Control.Text = "";
            }
        }

        private void CargarMesValorizacionCMF()
        {
            try
            {
                var lstMesEscritura = new List<MesValorizacionCMF>();
                DateTime Hoy = new DateTime();
                Hoy = DateTime.Now;
                DateTime Fecha = new DateTime();
                MesValorizacionCMF mes = new MesValorizacionCMF();
                for (int i = -4; i < 1; i++)
                {

                    Fecha = Hoy.AddMonths(i);
                    DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

                    mes = new MesValorizacionCMF();
                    mes.Id = Fecha.Month.ToString() + "-" + Fecha.Year.ToString();
                    mes.Descripcion = dtinfo.GetMonthName(Fecha.Month).ToUpper() + " - " + Fecha.Year.ToString();
                    lstMesEscritura.Add(mes);
                }
                Controles.CargarCombo<MesValorizacionCMF>(ref ddlFechaValorizacionCMF, lstMesEscritura, "Id", "Descripcion", "-- Mes de Valorización --", "-1");

               


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Mes de Escritura");
                }
            }
        }

        private class MesValorizacionCMF
        {
            public string Id { get; set; }
            public string Descripcion { get; set; }
        }

        protected void btnGrabarPasivoCMF_Click(object sender, EventArgs e)
        {
            ActualizarPasivoCMF();
        }

        private void ActualizarPasivoCMF()
        {
            try
            {
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                decimal PasivosCMF = decimal.Zero;

                oParticipante = NegParticipante.ObjParticipante;
                if (oParticipante == null || oParticipante.Rut == -1)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Participante");
                    return;
                }
                if (!decimal.TryParse(txtTotalPasivosCMF.Text, out PasivosCMF))
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un monto válido");
                    return;
                }


                if (ddlFechaValorizacionCMF.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una fecha parael total Pasivos CMF");
                    return;
                }
                NegParticipante negParticipante = new NegParticipante();
                Resultado<ParticipanteInfo> rParticipante = new Resultado<ParticipanteInfo>();


                var MesPasivoMCF = ddlFechaValorizacionCMF.SelectedValue.Split('-');
                oParticipante.MesPasivoCMF = int.Parse(MesPasivoMCF[0]);
                oParticipante.AñoPasivoCMF = int.Parse(MesPasivoMCF[1]);
                oParticipante.TotalPasivosCMF = PasivosCMF;
                rParticipante = negParticipante.GuardarParticipante(oParticipante);
                if (rParticipante.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Total Pasivos CMF Actualizados Correctamente");

                }
                else
                {
                    Controles.MostrarMensajeAlerta(rParticipante.Mensaje);

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}