using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.DatosGenerales
{
    public partial class DatosPropiedad : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarPropiedades();
                CargaSegIncendio();
                CargaComboTipoInmueble(true);
                CargaComboAntiguedad();
                CargaComboDestino();
                CargaComboVia();
                CargarTipoConstruccion();
                CargarRegion(ref ddlRegionProp);
                Controles.CargarCombo<ProvinciaInfo>(ref ddlProvinciaProp, null, "Id", "Descripcion", "-- Provincia--", "-1");
                Controles.CargarCombo<ComunaInfo>(ref ddlComunaProp, null, "Id", "Descripcion", "-- Comuna--", "-1");
                if (NegBandejaEntrada.oBandejaEntrada == null || !NegBandejaEntrada.oBandejaEntrada.IndModificaDatosPropiedades)
                {
                    Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.Button>(this).ForEach(c => c.Enabled = false);
                    gvPropiedades.Columns[gvPropiedades.Columns.Count - 1].Visible = false;
                }
                if (rblPropiedadPrincipal.SelectedValue == "true")
                {
                    ddlTasacionPadre.ClearSelection();
                    CargaComboTipoInmueble(true);
                    CargarInfoTasacionPadre();
                    ddlTasacionPadre.Enabled = false;
                }
                else
                {
                    ddlTasacionPadre.Enabled = true;
                    CargaComboTipoInmueble(false);
                    CargarTasacionPadre();
                }

            }
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormularioProp();", true);
        }

        protected void btnModificarProp_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvPropiedades.DataKeys[row.RowIndex].Values["Id"].ToString());
            TasacionInfo oTasacion = new TasacionInfo();
            oTasacion = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.Id == Id);
            CargarDatosTasacion(oTasacion);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormularioProp();", true);
        }

        protected void gvPropiedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnGuardarProp_Click(object sender, EventArgs e)
        {
            GrabarPropiedad();
        }

        protected void btnCancelarProp_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusquedaProp();", true);
        }


        protected void ddlSeguroIncendio_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSeguroIncendio();
        }

        public void CargarPropiedades()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedades, rTasacion.Lista, new string[] { "Id", "Propiedad_Id" });

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
        private PropiedadInfo DatosEntidadPropiedad(PropiedadInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<PropiedadInfo>();
                var oInfo = new PropiedadInfo();
                var oNeg = new NegPropiedades();

                oResultado = oNeg.BuscarPropiedad(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oInfo = oResultado.Lista.FirstOrDefault();

                    if (oInfo != null)
                    {
                        return oInfo;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Propiedad");
                        }
                        return null;
                    }
                }
                else
                {
                    return null;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Propiedad");

                }
                return null;
            }

        }
        private TasacionInfo DatosEntidadTasacion(TasacionInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<TasacionInfo>();
                var oInfo = new TasacionInfo();
                var oNeg = new NegPropiedades();

                oResultado = oNeg.BuscarTasacion(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oInfo = oResultado.Lista.FirstOrDefault();

                    if (oInfo != null)
                    {
                        return oInfo;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Tasación");
                        }
                        return null;
                    }
                }
                else
                {
                    return null;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Tasación");

                }
                return null;
            }

        }

        private void CargarDatosTasacion(TasacionInfo oTasacion)
        {
            try
            {
                NegPropiedades nPropiedades = new NegPropiedades();
                Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();
                PropiedadInfo oPropiedad = new PropiedadInfo();
                oPropiedad.Id = oTasacion.Propiedad_Id;
                rPropiedad = nPropiedades.BuscarPropiedad(oPropiedad);
                if (!rPropiedad.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(rPropiedad.Mensaje);
                    return;
                }

                oPropiedad = rPropiedad.Lista.FirstOrDefault();

                hfIdTasacion.Value = oTasacion.Id.ToString();
                hfIdPropiedad.Value = oTasacion.Propiedad_Id.ToString();

                if (oTasacion.IndPropiedadPrincipal == true)
                    rblPropiedadPrincipal.SelectedValue = "true";
                else
                    rblPropiedadPrincipal.SelectedValue = "false";
                if (rblPropiedadPrincipal.SelectedValue == "true")
                {
                    ddlTasacionPadre.ClearSelection();
                    CargaComboTipoInmueble(true);
                    CargarInfoTasacionPadre();
                    ddlTasacionPadre.Enabled = false;
                }
                else
                {
                    ddlTasacionPadre.Enabled = true;
                    CargaComboTipoInmueble(false);
                    CargarTasacionPadre();
                    ddlTasacionPadre.SelectedValue = oTasacion.TasacionPadre_Id.ToString();
                }
                ddlTipoInmueble.SelectedValue = oPropiedad.TipoInmueble_Id.ToString();
                SeleccionarTipoInmueble();
                ddlAntiguedadProp.SelectedValue = oPropiedad.Antiguedad_Id.ToString();
                ddlDestinoProp.SelectedValue = oPropiedad.Destino_Id.ToString();
                ddlRegionProp.SelectedValue = oPropiedad.Region_Id.ToString();
                CargarProvincia(ref ddlProvinciaProp, oPropiedad.Region_Id);
                ddlProvinciaProp.SelectedValue = oPropiedad.Provincia_Id.ToString();
                CargarComuna(ref ddlComunaProp, oPropiedad.Provincia_Id);
                ddlComunaProp.SelectedValue = oPropiedad.Comuna_Id.ToString();
                ddlVia.SelectedValue = oPropiedad.Via_Id.ToString();
                ddlTipoConstruccion.SelectedValue = oPropiedad.TipoConstruccion_Id == 0 ? "-1" : oPropiedad.TipoConstruccion_Id.ToString();
                txtDireccionProp.Text = oPropiedad.Direccion;
                txtNumeroProp.Text = oPropiedad.Numero;
                txtDeptoOficinaProp.Text = oPropiedad.DeptoOficina;
                txtUbicacionProp.Text = oPropiedad.Ubicacion;
                chIndDfl2.Checked = oPropiedad.IndDfl2;
                txtRolManzana.Text = oPropiedad.RolManzana.ToString();
                txtRolSitio.Text = oPropiedad.RolSitio.ToString();
                chkIndUsoGoce.Checked = oPropiedad.IndUsoGoce == true ? true : false;
                ProcesarUsoGoce(chkIndUsoGoce.Checked);
                ddlSeguroIncendio.SelectedValue = oTasacion.Seguro_Id.ToString();
                txtTasaSeguroIncendio.Text = (oTasacion.TasaSeguro * 100).ToString();
                txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado.ToString();
                txtPrimaSeguroIncendio.Text = oTasacion.PrimaSeguro.ToString();
                NegPropiedades.objTasacion = new TasacionInfo();
                NegPropiedades.objTasacion = oTasacion;
                if (NegBandejaEntrada.oBandejaEntrada != null)
                {
                    if (!NegBandejaEntrada.oBandejaEntrada.IndModificaDatosPropiedades)
                    {
                        ddlSeguroIncendio.Enabled = false;
                        btnCancelarProp.Text = "Volver";
                        btnCancelarProp.Enabled = true;
                    }
                }
                else
                {
                    ddlSeguroIncendio.Enabled = false;
                    btnCancelarProp.Visible = false;
                    btnCancelarProp.Enabled = true;

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
                    Controles.MostrarMensajeError("Error al Llenar el Formulario");
                }
            }
        }
        private void CargarRegion(ref Anthem.DropDownList Combo)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new RegionInfo();
                var ObjetoResultado = new Resultado<RegionInfo>();
                var NegComuna = new NegRegion();

                ////Asignacion de Variables
                ObjetoResultado = NegComuna.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<RegionInfo>(ref Combo, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Región--", "-1");

                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Comuna");
                }
            }
        }
        private void CargarProvincia(ref Anthem.DropDownList Combo, int Region_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ProvinciaInfo();
                var ObjetoResultado = new Resultado<ProvinciaInfo>();
                var NegComuna = new NegProvincia();

                ////Asignacion de Variables
                ObjInfo.Region_Id = Region_Id;
                ObjetoResultado = NegComuna.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProvinciaInfo>(ref Combo, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Provincia--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Comuna");
                }
            }
        }
        private void CargarComuna(ref Anthem.DropDownList Combo, int Provincia_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ComunaInfo();
                var ObjetoResultado = new Resultado<ComunaInfo>();
                var NegComuna = new NegComunas();

                ////Asignacion de Variables
                ObjInfo.Provincia_Id = Provincia_Id;
                ObjetoResultado = NegComuna.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ComunaInfo>(ref Combo, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Comuna--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Comuna");
                }
            }
        }
        private void CargaComboTipoInmueble(bool IndPropiedadPrincipal)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new TipoInmuebleInfo();
                var ObjResultado = new Resultado<TipoInmuebleInfo>();
                var objNegocio = new NegPropiedades();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjInfo.IndPropiedadPrincipal = IndPropiedadPrincipal;
                ObjResultado = objNegocio.BuscarTipoInmueble(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoInmuebleInfo>(ref ddlTipoInmueble, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Tipo Inmueble--", "-1");
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Tipo Inmueble");
                }
            }
        }
        private void CargaComboAntiguedad()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("ANTIGUEDAD_PROPIEDAD");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlAntiguedadProp, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Antigüedad --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablaEstado + " Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Antigüedad");
                }
            }
        }
        private void CargaComboDestino()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("DESTINO_PROP");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlDestinoProp, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Destino --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Destino Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Destino");
                }
            }
        }

        private void CargaComboVia()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("VIA");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlVia, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Vía --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Vía Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Vía");
                }
            }
        }
        private void CargarTipoConstruccion()
        {
            try
            {
                NegPropiedades nProp = new NegPropiedades();
                TipoConstruccionInfo oTipoConstruccion = new TipoConstruccionInfo();
                Resultado<TipoConstruccionInfo> rTipoConstruccion = new Resultado<TipoConstruccionInfo>();


                oTipoConstruccion.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                rTipoConstruccion = nProp.BuscarTipoConstruccion(oTipoConstruccion);
                if (rTipoConstruccion.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoConstruccionInfo>(ref ddlTipoConstruccion, rTipoConstruccion.Lista, "Id", "Descripcion", "-- Tipo de Construcción --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(rTipoConstruccion.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Tipo de Construcción");
                }
            }
        }

        private void CargaSegIncendio()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                ////Asignacion de Variables
                ObjInfo.GrupoSeguro_Id = 1;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroIncendio, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Seguro Incendio--", "-1");
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Seguro Incendio");
                }
            }
        }
        private void SeleccionarSeguroIncendio()
        {
            try
            {
                ////Declaracion de Variables
                var IdTipoInmueble = int.Parse(ddlTipoInmueble.SelectedValue);
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();
                var oTipoInmueble = new TipoInmuebleInfo();
                if (ddlSeguroIncendio.SelectedValue == "-1")
                {
                    txtTasaSeguroIncendio.Text = "0";
                    txtPrimaSeguroIncendio.Text = "0";
                }
                else
                {
                    oTipoInmueble = NegPropiedades.lstTipoInmuebles.FirstOrDefault(ti => ti.Id == IdTipoInmueble);


                    if (oTipoInmueble == null)
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Inmueble");
                        ddlSeguroIncendio.SelectedValue = "-1";
                        return;
                    }
                    ////Asignacion de Variables
                    ObjInfo.Id = int.Parse(ddlSeguroIncendio.SelectedValue);
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        decimal FactorCobertura = (decimal)(oTipoInmueble.PorcentajeSeguroIncendio / 100.0000);


                        if (NegPropiedades.objTasacion != null)
                        {
                            if (NegPropiedades.objTasacion.MontoAsegurado != 0)
                                PrimaSeguro = (ObjInfo.TasaMensual * NegPropiedades.objTasacion.MontoAsegurado);
                            else
                                PrimaSeguro = (ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.MontoPropiedad * FactorCobertura);
                        }
                        else
                            PrimaSeguro = (ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.MontoPropiedad * FactorCobertura);
                        txtPrimaSeguroIncendio.Text = string.Format("{0:0.00000000}", PrimaSeguro);
                        txtTasaSeguroIncendio.Text = string.Format("{0:0.00000000}", ObjInfo.TasaMensual * 100);
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Seleccionar el Seguro de Desgravamen");
                }
            }
        }
        private void SeleccionarTipoInmueble()
        {
            try
            {
                if (ddlTipoInmueble.SelectedValue != "-1")
                {
                    var Id = int.Parse(ddlTipoInmueble.SelectedValue);
                    var oTipoInmueble = new TipoInmuebleInfo();
                    ddlSeguroIncendio.Enabled = true;
                    oTipoInmueble = NegPropiedades.lstTipoInmuebles.FirstOrDefault(ti => ti.Id == Id);




                    if (NegSolicitudes.objSolicitudInfo.MontoPropiedad > 0)
                    {
                        txtMontoAseguradoIncendio.Text = ((NegSolicitudes.objSolicitudInfo.MontoPropiedad * oTipoInmueble.PorcentajeSeguroIncendio) / 100).ToString();

                        if (oTipoInmueble.PorcentajeSeguroIncendio == 0)
                        {
                            ddlSeguroIncendio.ClearSelection();
                            txtTasaSeguroIncendio.Text = "0";
                            txtMontoAseguradoIncendio.Text = "0";
                            txtPrimaSeguroIncendio.Text = "0";
                            ddlSeguroIncendio.Enabled = false;
                        }
                    }
                    else
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar un Precio de Venta a La Solicitud");
                        ddlTipoInmueble.ClearSelection();
                        ddlSeguroIncendio.ClearSelection();
                        txtTasaSeguroIncendio.Text = "0";
                        txtMontoAseguradoIncendio.Text = "0";
                        txtPrimaSeguroIncendio.Text = "0";
                        ddlSeguroIncendio.Enabled = false;
                    }

                }
                else
                {
                    ddlSeguroIncendio.ClearSelection();
                    txtTasaSeguroIncendio.Text = "0";
                    txtMontoAseguradoIncendio.Text = "0";
                    txtPrimaSeguroIncendio.Text = "0";
                    ddlSeguroIncendio.Enabled = false;
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
                    Controles.MostrarMensajeError("Error al Seleccionar Tipo de Inmueble");
                }
            }
        }
        private void GrabarPropiedad()
        {
            try
            {
                if (!ValidarFormularioPropiedad()) return;

                NegPropiedades nPropiedad = new NegPropiedades();
                PropiedadInfo oPropiedad = new PropiedadInfo();
                Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();

                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                if (hfIdPropiedad.Value != "")
                {
                    oPropiedad.Id = int.Parse(hfIdPropiedad.Value);
                    oPropiedad = DatosEntidadPropiedad(oPropiedad);

                }
                oPropiedad.TipoInmueble_Id = int.Parse(ddlTipoInmueble.SelectedValue);

                oPropiedad.Antiguedad_Id = int.Parse(ddlAntiguedadProp.SelectedValue);
                oPropiedad.Destino_Id = int.Parse(ddlDestinoProp.SelectedValue);
                oPropiedad.Region_Id = int.Parse(ddlRegionProp.SelectedValue);
                oPropiedad.Provincia_Id = int.Parse(ddlProvinciaProp.SelectedValue);
                oPropiedad.Comuna_Id = int.Parse(ddlComunaProp.SelectedValue);
                oPropiedad.Via_Id = int.Parse(ddlVia.SelectedValue);
                oPropiedad.TipoConstruccion_Id = int.Parse(ddlTipoConstruccion.SelectedValue);
                oPropiedad.Direccion = txtDireccionProp.Text;
                oPropiedad.Numero = txtNumeroProp.Text;
                oPropiedad.DeptoOficina = txtDeptoOficinaProp.Text;
                oPropiedad.Ubicacion = txtUbicacionProp.Text;
                oPropiedad.RolManzana = txtRolManzana.Text == "" ? -1 : int.Parse(txtRolManzana.Text);
                oPropiedad.RolSitio = txtRolSitio.Text == "" ? -1 : int.Parse(txtRolSitio.Text);
                oPropiedad.IndDfl2 = chIndDfl2.Checked;
                oPropiedad.IndUsoGoce = chkIndUsoGoce.Checked;
                

                rPropiedad = nPropiedad.GuardarPropiedad(ref oPropiedad);
                if (rPropiedad.ResultadoGeneral)
                {
                    hfIdPropiedad.Value = oPropiedad.Id.ToString();
                    if (hfIdTasacion.Value != "")
                    {
                        oTasacion.Id = int.Parse(hfIdTasacion.Value);
                        oTasacion = DatosEntidadTasacion(oTasacion);

                    }
                    else
                    {
                        oTasacion.EstadoTasacion_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS_FLUJOS_PARALELOS", "I");
                        oTasacion.EstadoEstudioTitulo_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS_FLUJOS_PARALELOS", "I");
                    }
                    oTasacion.Propiedad_Id = oPropiedad.Id;
                    oTasacion.TasacionPadre_Id = int.Parse(ddlTasacionPadre.SelectedValue);
                    oTasacion.IndPropiedadPrincipal = bool.Parse(rblPropiedadPrincipal.SelectedValue);
                    oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                    oTasacion.Seguro_Id = int.Parse(ddlSeguroIncendio.SelectedValue);
                    oTasacion.MontoAsegurado = txtMontoAseguradoIncendio.Text == "" ? 0 : decimal.Parse(txtMontoAseguradoIncendio.Text);
                    oTasacion.TasaSeguro = txtTasaSeguroIncendio.Text == "" ? 0 : decimal.Parse(txtTasaSeguroIncendio.Text) / 100;
                    oTasacion.PrimaSeguro = txtPrimaSeguroIncendio.Text == "" ? 0 : decimal.Parse(txtPrimaSeguroIncendio.Text);

                    rTasacion = nTasacion.GuardarTasacion(ref oTasacion);
                    if (rTasacion.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeExito("Datos de la Propiedad Grabados");
                        ActualizarSegurosContratados(oTasacion);
                        CargarPropiedades();
                        LimpiarFormulario();
                        Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusquedaProp();", true);
                    }
                    else
                    {
                        Controles.MostrarMensajeError(rTasacion.Mensaje);
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rPropiedad.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar la Propiedade");
                }
            }
        }
        private bool ValidarFormularioPropiedad()
        {
            try
            {

                if (ddlTipoInmueble.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe seleccionar un Tipo de Inmueble");
                    return false;
                }
                if (ddlAntiguedadProp.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe seleccionar la Antigüedad del Inmueble");
                    return false;
                }

                if (ddlTasacionPadre.Enabled == true && ddlTasacionPadre.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe seleccionar una Propiedad Principal");
                    return false;
                }
                var oTipoInmueble = new TipoInmuebleInfo();

                oTipoInmueble = NegPropiedades.lstTipoInmuebles.FirstOrDefault(ti => ti.Id == int.Parse(ddlTipoInmueble.SelectedValue));
                if (oTipoInmueble.PorcentajeSeguroIncendio > 0 && ddlSeguroIncendio.SelectedValue == "-1" && chkIndUsoGoce.Checked == false && rblPropiedadPrincipal.SelectedValue == "True")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Seguro de Incendio");
                    return false;
                }



                if (ddlDestinoProp.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe seleccionar un Destino");
                    return false;
                }


                //Validar Información Secundaria
                if (ddlRegionProp.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar la Región");
                    return false;
                }
                if (ddlProvinciaProp.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar la Provincia");
                    return false;
                }
                if (ddlComunaProp.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar la Comuna");
                    return false;
                }
                if (ddlVia.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Vía");
                    return false;
                }
                if (txtDireccionProp.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Dirección (Calle)");
                    return false;
                }
                if (txtNumeroProp.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Número");
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
                    Controles.MostrarMensajeError("Error al Validar el Formulario de la Propiedad");
                }
                return false;
            }
        }
        private void LimpiarFormulario()
        {
            NegPropiedades.objTasacion = null;
            hfIdPropiedad.Value = "";
            hfIdTasacion.Value = "";
            ddlTipoInmueble.ClearSelection();
            rblPropiedadPrincipal.SelectedValue = "true";
            ddlAntiguedadProp.ClearSelection();
            ddlDestinoProp.ClearSelection();
            ddlRegionProp.ClearSelection();
            ddlProvinciaProp.ClearSelection();
            ddlComunaProp.ClearSelection();
            ddlVia.ClearSelection();
            ddlTipoConstruccion.ClearSelection();
            txtDireccionProp.Text = "";
            txtNumeroProp.Text = "";
            txtDeptoOficinaProp.Text = "";
            txtUbicacionProp.Text = "";
            txtRolManzana.Text = "";
            txtRolSitio.Text = "";
            chkIndUsoGoce.Checked = false;
            chIndDfl2.Checked = false;
            ddlSeguroIncendio.ClearSelection();
            txtTasaSeguroIncendio.Text = "0";
            txtMontoAseguradoIncendio.Text = "0";
            txtPrimaSeguroIncendio.Text = "";
            ddlSeguroIncendio.Enabled = false;
            if (rblPropiedadPrincipal.SelectedValue == "true")
            {
                ddlTasacionPadre.ClearSelection();
                CargarInfoTasacionPadre();
                ddlTasacionPadre.Enabled = false;
            }
            else
            {
                ddlTasacionPadre.Enabled = true;
                CargarTasacionPadre();
            }

        }
        protected void ddlTipoInmueble_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarTipoInmueble();
        }

        protected void ddlRegionProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegionProp.SelectedValue == "-1")
            {
                Controles.CargarCombo<ProvinciaInfo>(ref ddlProvinciaProp, null, "Id", "Descripcion", "-- Provincia--", "-1");
            }
            else
            {
                CargarProvincia(ref ddlProvinciaProp, int.Parse(ddlRegionProp.SelectedValue));

            }
        }

        protected void ddlProvinciaProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvinciaProp.SelectedValue == "-1")
            {
                Controles.CargarCombo<ComunaInfo>(ref ddlComunaProp, null, "Id", "Descripcion", "-- Comuna--", "-1");
            }
            else
            {
                CargarComuna(ref ddlComunaProp, int.Parse(ddlProvinciaProp.SelectedValue));

            }
        }


        private void ActualizarSegurosContratados(TasacionInfo oTasacion)
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();
                //Actualizacion del Seguro de Desgravamen

                if (ddlSeguroIncendio.Enabled)
                {
                    oSeguros = new SegurosContratadosInfo();
                    oSeguros.TipoSeguro_Id = 1;//Incendio
                    oSeguros.Solicitud_Id = oTasacion.Solicitud_Id;
                    oSeguros.Seguro_Id = oTasacion.Seguro_Id;
                    oSeguros.Tasacion_Id = oTasacion.Id;
                    oSeguros.MontoAsegurado = oTasacion.MontoAsegurado;
                    oSeguros.TasaMensual = oTasacion.TasaSeguro;
                    oSeguros.PrimaMensual = oTasacion.PrimaSeguro;
                    rSeguros = nSeguros.GuardarSegurosContratados(oSeguros);
                    if (rSeguros.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(rSeguros.Mensaje);
                        return;
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
                    Controles.MostrarMensajeError("Error al Actualizar los Seguros Contratados");
                }
            }
        }

        protected void btnEliminarPropiedad_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
                int Id = int.Parse(gvPropiedades.DataKeys[row.RowIndex].Values["Id"].ToString());
                NegPropiedades nPropiedad = new NegPropiedades();
                TasacionInfo oPropiedad = new TasacionInfo();
                Resultado<TasacionInfo> rPropiedad = new Resultado<TasacionInfo>();

                oPropiedad.Id = Id;

                rPropiedad = nPropiedad.EliminarTasacion(oPropiedad);
                if (rPropiedad.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    CargarPropiedades();
                }
                else
                {
                    Controles.MostrarMensajeError(rPropiedad.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Eliminar el Propiedad");
                }
            }
        }

        protected void ddlTasacionPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarInfoTasacionPadre();
        }

        protected void rblPropiedadPrincipal_CheckedChanged(object sender, EventArgs e)
        {
            if (rblPropiedadPrincipal.SelectedValue == "true")
            {
                ddlTasacionPadre.ClearSelection();
                ddlTasacionPadre.Enabled = false;
                CargaComboTipoInmueble(true);
                CargarInfoTasacionPadre();

            }
            else
            {
                ddlTasacionPadre.Enabled = true;
                CargaComboTipoInmueble(false);
                CargarTasacionPadre();
            }
        }

        private void CargarTasacionPadre()
        {
            try
            {
                int IdTasacion = 0;
                if (!int.TryParse(hfIdPropiedad.Value, out IdTasacion))
                {
                    IdTasacion = -1;

                }

                List<TasacionInfo> lstTasaciones = new List<TasacionInfo>();
                lstTasaciones = NegPropiedades.lstTasaciones.Where(t => t.IndPropiedadPrincipal == true && t.Id != IdTasacion).ToList<TasacionInfo>();

                if (lstTasaciones.Count > 0)
                {
                    Controles.CargarCombo<TasacionInfo>(ref ddlTasacionPadre, lstTasaciones, "Id", "DescripcionCompleta", "-- Propiedad Principal --", "-1");
                    ddlTasacionPadre.Enabled = true;
                }

                else
                {
                    Controles.MostrarMensajeInfo("Solicitud sin Propiedad Principal Ingresada");
                    Controles.CargarCombo<TasacionInfo>(ref ddlTasacionPadre, lstTasaciones, "Id", "DescripcionCompleta", "-- Propiedad Principal --", "-1");
                    rblPropiedadPrincipal.SelectedValue = "true";
                    ddlTasacionPadre.Enabled = false;
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
                    Controles.MostrarMensajeError("Error al Cargar Propeidad Padre");
                }
            }
        }

        private void CargarInfoTasacionPadre()
        {
            try
            {
                TasacionInfo oTasacioPadre = new TasacionInfo();
                List<TasacionInfo> lstTasaciones = new List<TasacionInfo>();
                lstTasaciones = NegPropiedades.lstTasaciones.Where(t => t.IndPropiedadPrincipal == true).ToList<TasacionInfo>();
                oTasacioPadre = lstTasaciones.FirstOrDefault(t => t.Id == int.Parse(ddlTasacionPadre.SelectedValue));

                if (oTasacioPadre != null)
                {
                    NegPropiedades nPropiedades = new NegPropiedades();
                    Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();
                    PropiedadInfo oPropiedadPadre = new PropiedadInfo();
                    PropiedadInfo oPropiedad = new PropiedadInfo();


                    oPropiedad.Id = oTasacioPadre.Propiedad_Id;
                    rPropiedad = nPropiedades.BuscarPropiedad(oPropiedad);
                    if (!rPropiedad.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rPropiedad.Mensaje);
                        return;
                    }

                    oPropiedad = rPropiedad.Lista.FirstOrDefault();



                    ddlAntiguedadProp.SelectedValue = oPropiedad.Antiguedad_Id.ToString();
                    ddlDestinoProp.SelectedValue = oPropiedad.Destino_Id.ToString();
                    ddlRegionProp.SelectedValue = oPropiedad.Region_Id.ToString();
                    CargarProvincia(ref ddlProvinciaProp, oPropiedad.Region_Id);
                    ddlProvinciaProp.SelectedValue = oPropiedad.Provincia_Id.ToString();
                    CargarComuna(ref ddlComunaProp, oPropiedad.Provincia_Id);
                    ddlComunaProp.SelectedValue = oPropiedad.Comuna_Id.ToString();
                    ddlVia.SelectedValue = oPropiedad.Via_Id.ToString();
                    ddlTipoConstruccion.SelectedValue = oPropiedad.TipoConstruccion_Id == 0 ? "-1" : oPropiedad.TipoConstruccion_Id.ToString();
                    txtDireccionProp.Text = oPropiedad.Direccion;
                    txtNumeroProp.Text = oPropiedad.Numero;
                }
                else
                {
                    ddlAntiguedadProp.ClearSelection();
                    ddlDestinoProp.ClearSelection();
                    ddlRegionProp.ClearSelection();
                    ddlProvinciaProp.ClearSelection();
                    ddlComunaProp.ClearSelection();
                    ddlVia.ClearSelection();
                    ddlTipoConstruccion.ClearSelection();
                    txtDireccionProp.Text = "";
                    txtNumeroProp.Text = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void chkIndUsoGoce_CheckedChanged(object sender, EventArgs e)
        {
            ProcesarUsoGoce(chkIndUsoGoce.Checked);
        }

        protected void ProcesarUsoGoce(bool IndUsoGoce)
        {
            if (IndUsoGoce)
            {
                txtRolManzana.Enabled = false;
                txtRolManzana.Text = "";
                txtRolSitio.Enabled = false;
                txtRolSitio.Text = "";
                ddlSeguroIncendio.ClearSelection();
                ddlSeguroIncendio.Enabled = false;
                txtTasaSeguroIncendio.Text = "0";
                txtMontoAseguradoIncendio.Text = "0";
                txtPrimaSeguroIncendio.Text = "0";
            }
            else
            {
                txtRolManzana.Enabled = true;
                txtRolSitio.Enabled = true;
                SeleccionarTipoInmueble();
            }
        }
    }
}