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

namespace WorkFlow.Eventos.Tasacion
{
    public partial class ConfeccionInforme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioSolicitudTasaciones();");

            if (!Page.IsPostBack)
            {

                CargarAcciones();
                CargarPropiedades();
                CargaComboFabricaTasadores();
                CargarTasadores();
                CargarParticipantes();
                CargaComboTipoInmueble();
                CargaComboAntiguedad();
                CargaComboDestino();
                CargarTipoConstruccion();
                CargaComboInmobiliaria();
                CargarDatosSolicitud();
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
                    if (!ValidarPropiedades()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!ValidarPropiedades()) return;
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
        private void CargaComboTasadores(int Fabrica_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();

                if (Fabrica_Id == -1)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref AddlTasador, new List<UsuarioInfo>(), "Rut", "Nombre", "--Tasador--", "-1");
                    return;
                }
                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolTasador;
                ObjetoUsuario.Fabrica_Id = Fabrica_Id;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref AddlTasador, ObjetoResultado.Lista, "Rut", "Nombre", "--Tasador--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Ejecutivo");
                }
            }
        }
        private void CargaComboFabricaTasadores()
        {
            try
            {
                NegFabricas oNegocio = new NegFabricas();
                Resultado<AsignacionTipoFabricaInfo> oResultado = new Resultado<AsignacionTipoFabricaInfo>();
                AsignacionTipoFabricaInfo oFabrica = new AsignacionTipoFabricaInfo();

                oFabrica.TipoFabrica_Id = 4;//Fábricas de Tasadores
                oResultado = oNegocio.BuscarAsignacionTipoFabrica(oFabrica);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabTasadores, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "--Fábrica Tasadores--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar Combo de Fábricas de Tasadores");
                }
            }
        }
        private void CargarTasadores()
        {
            try
            {
                Controles.CargarCombo<UsuarioInfo>(ref AddlTasador, null, "Rut", "Nombre", "--Tasador--", "-1");
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
        public void CargarPropiedades()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oTasacion.IndFlujoTasacion = true;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedadesTasacion, rTasacion.Lista, new string[] { "Id", "Propiedad_Id" });
                    NegPropiedades.lstPropiedadesTasaciones = new List<TasacionInfo>();
                    NegPropiedades.lstPropiedadesTasaciones = rTasacion.Lista;

                    if (NegPropiedades.lstPropiedadesTasaciones.Count() == 1)
                    {
                        TasacionInfo oTasacionPadre = new TasacionInfo();
                        oTasacion = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault();
                        NegPropiedades.objTasacion = new TasacionInfo();
                        NegPropiedades.objTasacion = oTasacion;
                        oTasacionPadre = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault(t => t.Id == oTasacion.TasacionPadre_Id);
                        CargarInfoTasacion(oTasacion, oTasacionPadre);
                    }

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
        private bool ValidarPropiedades()
        {
            try
            {
                if (NegPropiedades.lstPropiedadesTasaciones == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Propiedad de la Solicitud");
                    return false;
                }

                if (NegPropiedades.lstPropiedadesTasaciones.Count == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Propiedad de la Solicitud");
                    return false;
                }

                if (NegPropiedades.lstPropiedadesTasaciones.Count(t => t.FechaTasacion == null && t.IndFlujoTasacion == true) != 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Completar los Datos de la Tasación de todas las Propiedades");
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
                    Controles.MostrarMensajeError("Error al Validar la Propiedad");
                }
            }
            return false;
        }
        protected void btnSeleccionarPropTasacion_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvPropiedadesTasacion.DataKeys[row.RowIndex].Values["Id"].ToString());
            TasacionInfo oTasacion = new TasacionInfo();
            TasacionInfo oTasacionPadre = new TasacionInfo();
            oTasacion = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault(t => t.Id == Id);
            NegPropiedades.objTasacion = new TasacionInfo();
            NegPropiedades.objTasacion = oTasacion;
            oTasacionPadre = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault(t => t.Id == oTasacion.TasacionPadre_Id);
            CargarInfoTasacion(oTasacion, oTasacionPadre);
        }
        private void CargarInfoTasacion(TasacionInfo oTasacion, TasacionInfo oTasacionPadre)
        {
            try
            {
                if (oTasacion != null)
                {

                    NegComunas nComuna = new NegComunas();
                    Resultado<ComunaSiiInfo> rComuna = new Resultado<ComunaSiiInfo>();
                    ComunaSiiInfo oComuna = new ComunaSiiInfo();

                    NegPropiedades nPropiedades = new NegPropiedades();
                    Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();
                    PropiedadInfo oPropiedadPadre = new PropiedadInfo();
                    PropiedadInfo oPropiedad = new PropiedadInfo();



                    if (oTasacionPadre != null)
                    {



                        oPropiedadPadre.Id = oTasacion.Propiedad_Id;
                        rPropiedad = nPropiedades.BuscarPropiedad(oPropiedadPadre);
                        if (!rPropiedad.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rPropiedad.Mensaje);
                            return;
                        }

                        oPropiedadPadre = rPropiedad.Lista.FirstOrDefault();




                        //Datos Propiedad
                        oComuna.Descripcion = oPropiedadPadre.DescripcionComuna;
                        rComuna = nComuna.BuscarComunaSii(oComuna);
                        if (rComuna.ResultadoGeneral)
                        {
                            if (rComuna.Lista.Count == 1)
                                Controles.CargarCombo<ComunaSiiInfo>(ref ddlComunaSii, rComuna.Lista, "Id", "Descripcion", "", "");
                            else
                                Controles.CargarCombo<ComunaSiiInfo>(ref ddlComunaSii, rComuna.Lista, "Id", "Descripcion", "-- Seleccione una Comuna --", "-1");
                        }
                        ddlInmobiliaria.SelectedValue = oPropiedadPadre.Inmobiliaria_Id.ToString();
                        CargaComboProyecto(oPropiedadPadre.Inmobiliaria_Id);
                        ddlProyecto.SelectedValue = oPropiedadPadre.Proyecto_Id.ToString();
                        chIndDfl2.Checked = oPropiedadPadre.IndDfl2;
                        txtAñoConstruccion.Text = oPropiedadPadre.AñoConstruccion == 0 ? "" : oPropiedadPadre.AñoConstruccion.ToString();
                        ddlTipoConstruccion.SelectedValue = oPropiedadPadre.TipoConstruccion_Id.ToString();
                        txtRolManzana.Text = oPropiedadPadre.RolManzana == 0 ? "" : oPropiedadPadre.RolManzana.ToString();
                        txtRolSitio.Text = oPropiedadPadre.RolSitio == 0 ? "" : oPropiedadPadre.RolSitio.ToString();
                        chkIndUsoGoce.Checked = oPropiedadPadre.IndUsoGoce == true ? true : false;
                        ProcesarUsoGoce(chkIndUsoGoce.Checked);
                        if (oPropiedadPadre.ComunaSii_Id > 0)
                            ddlComunaSii.SelectedValue = oPropiedadPadre.ComunaSii_Id.ToString();
                        ddlTipoInmueble.SelectedValue = oPropiedadPadre.TipoInmueble_Id.ToString();
                        ddlDestinoProp.SelectedValue = oPropiedadPadre.Destino_Id.ToString();
                        ddlAntiguedadProp.SelectedValue = oPropiedadPadre.Antiguedad_Id.ToString();


                        //Datos Tasacion
                        if (oTasacion.PorcentajeSeguroIncendio > 0)
                        {
                            txtMontoAseguradoIncendio.Enabled = true;
                            txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoAsegurado);
                        }
                        else
                        {
                            txtMontoAseguradoIncendio.Text = "0";
                            txtMontoAseguradoIncendio.Enabled = false;
                        }
                        txtValorComercial.Text = oTasacion.ValorComercial == 0 ? "" : string.Format("{0:F4}", oTasacion.ValorComercial);
                        txtMontoTasacion.Text = oTasacion.MontoTasacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoTasacion);

                        txtMontoLiquidacion.Text = oTasacion.MontoLiquidacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoLiquidacion);
                        txtMetrosTerreno.Text = oTasacion.MetrosTerreno == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerreno);
                        txtMetrosTerraza.Text = oTasacion.MetrosTerraza == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerraza);
                        txtMetrosLogia.Text = oTasacion.MetrosLogia == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosLogia);
                        txtMetrosConstruccion.Text = oTasacion.MetrosConstruidos == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosConstruidos);




                        //Datos Heredador de la Tasación Padre
                        AddlFabTasadores.SelectedValue = oTasacionPadre.FabricaTasador_Id.ToString();
                        CargaComboTasadores(oTasacionPadre.FabricaTasador_Id);
                        AddlTasador.SelectedValue = oTasacionPadre.Tasador_Id.ToString();
                        txtFechaTasacion.Text = oTasacionPadre.FechaTasacion == null ? "" : oTasacionPadre.FechaTasacion.Value.ToShortDateString();
                        txtFechaRecepcionFinal.Text = oTasacionPadre.FechaRecepcionFinal == null ? "" : oTasacionPadre.FechaRecepcionFinal.Value.ToShortDateString();
                        txtPermisoEdificacion.Text = oTasacionPadre.PermisoEdificacion;
                        lblDireccionTasacion.Text = oTasacionPadre.DireccionCompleta;





                    }
                    else
                    {



                        oPropiedad.Id = oTasacion.Propiedad_Id;
                        rPropiedad = nPropiedades.BuscarPropiedad(oPropiedad);
                        if (!rPropiedad.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rPropiedad.Mensaje);
                            return;
                        }
                        oPropiedad = rPropiedad.Lista.FirstOrDefault();


                        //Datos de la Propiedad

                        oComuna.Descripcion = oPropiedad.DescripcionComuna;
                        rComuna = nComuna.BuscarComunaSii(oComuna);
                        if (rComuna.ResultadoGeneral)
                        {
                            if (rComuna.Lista.Count == 1)
                                Controles.CargarCombo<ComunaSiiInfo>(ref ddlComunaSii, rComuna.Lista, "Id", "Descripcion", "", "");
                            else
                                Controles.CargarCombo<ComunaSiiInfo>(ref ddlComunaSii, rComuna.Lista, "Id", "Descripcion", "-- Seleccione una Comuna --", "-1");
                        }
                        ddlInmobiliaria.SelectedValue = oPropiedad.Inmobiliaria_Id.ToString();
                        CargaComboProyecto(oPropiedad.Inmobiliaria_Id);
                        ddlProyecto.SelectedValue = oPropiedad.Proyecto_Id.ToString();
                        chIndDfl2.Checked = oPropiedad.IndDfl2;
                        txtAñoConstruccion.Text = oPropiedad.AñoConstruccion == 0 ? "" : oPropiedad.AñoConstruccion.ToString();
                        txtRolManzana.Text = oPropiedad.RolManzana == 0 ? "" : oPropiedad.RolManzana.ToString();
                        txtRolSitio.Text = oPropiedad.RolSitio == 0 ? "" : oPropiedad.RolSitio.ToString();
                        chkIndUsoGoce.Checked = oPropiedad.IndUsoGoce == true ? true : false;
                        ProcesarUsoGoce(chkIndUsoGoce.Checked);
                        if (oPropiedad.ComunaSii_Id > 0)
                            ddlComunaSii.SelectedValue = oPropiedad.ComunaSii_Id.ToString();
                        ddlTipoInmueble.SelectedValue = oPropiedad.TipoInmueble_Id.ToString();
                        ddlDestinoProp.SelectedValue = oPropiedad.Destino_Id.ToString();
                        ddlAntiguedadProp.SelectedValue = oPropiedad.Antiguedad_Id.ToString();
                        ddlTipoConstruccion.SelectedValue = oPropiedad.TipoConstruccion_Id.ToString();


                        //Datos de la Tasacion
                        if (oTasacion.PorcentajeSeguroIncendio > 0)
                        {
                            txtMontoAseguradoIncendio.Enabled = true;
                            txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoAsegurado);
                        }
                        else
                        {
                            txtMontoAseguradoIncendio.Text = "0";
                            txtMontoAseguradoIncendio.Enabled = false;
                        }

                        AddlFabTasadores.SelectedValue = oTasacion.FabricaTasador_Id.ToString();
                        CargaComboTasadores(oTasacion.FabricaTasador_Id);
                        AddlTasador.SelectedValue = oTasacion.Tasador_Id.ToString();
                        txtValorComercial.Text = oTasacion.ValorComercial == 0 ? "" : string.Format("{0:F4}", oTasacion.ValorComercial);
                        txtMontoTasacion.Text = oTasacion.MontoTasacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoTasacion);

                        txtMontoLiquidacion.Text = oTasacion.MontoLiquidacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoLiquidacion);
                        txtFechaTasacion.Text = oTasacion.FechaTasacion == null ? "" : oTasacion.FechaTasacion.Value.ToShortDateString();
                        txtFechaRecepcionFinal.Text = oTasacion.FechaRecepcionFinal == null ? "" : oTasacion.FechaRecepcionFinal.Value.ToShortDateString();
                        txtMetrosTerreno.Text = oTasacion.MetrosTerreno == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerreno);
                        txtMetrosTerraza.Text = oTasacion.MetrosTerraza == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerraza);
                        txtMetrosLogia.Text = oTasacion.MetrosLogia == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosLogia);
                        txtMetrosConstruccion.Text = oTasacion.MetrosConstruidos == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosConstruidos);
                        txtPermisoEdificacion.Text = oTasacion.PermisoEdificacion;
                        lblDireccionTasacion.Text = oTasacion.DireccionCompleta;




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
                    Controles.MostrarMensajeError("Error al Cargar los datos de la Propiedad");
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
        private void GrabarInfoTasacion()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();


                NegPropiedades nPropiedad = new NegPropiedades();
                PropiedadInfo oPropiedad = new PropiedadInfo();
                Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();

                if (NegPropiedades.objTasacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Inmueble");
                    return;
                }
                if (!ValidarInfoTasacion()) return;

                oPropiedad.Id = NegPropiedades.objTasacion.Propiedad_Id;
                oPropiedad = DatosEntidadPropiedad(oPropiedad);
                oPropiedad.RolManzana = txtRolManzana.Text == "" ? 0 : int.Parse(txtRolManzana.Text);
                oPropiedad.RolSitio = txtRolSitio.Text == "" ? 0 : int.Parse(txtRolSitio.Text);
                oPropiedad.IndUsoGoce = chkIndUsoGoce.Checked;
                oPropiedad.ComunaSii_Id = int.Parse(ddlComunaSii.SelectedValue);
                oPropiedad.TipoInmueble_Id = int.Parse(ddlTipoInmueble.SelectedValue);
                oPropiedad.Antiguedad_Id = int.Parse(ddlAntiguedadProp.SelectedValue);
                oPropiedad.Destino_Id = int.Parse(ddlDestinoProp.SelectedValue);
                oPropiedad.TipoConstruccion_Id = int.Parse(ddlTipoConstruccion.SelectedValue);
                oPropiedad.Inmobiliaria_Id = int.Parse(ddlInmobiliaria.SelectedValue);
                oPropiedad.Proyecto_Id = int.Parse(ddlProyecto.SelectedValue);
                oPropiedad.IndDfl2 = chIndDfl2.Checked;
                oPropiedad.AñoConstruccion = int.Parse(txtAñoConstruccion.Text);

                rPropiedad = nPropiedad.GuardarPropiedad(ref oPropiedad);
                if (rPropiedad.ResultadoGeneral)
                {
                    NegPropiedades.objTasacion.ValorComercial = decimal.Parse(txtValorComercial.Text);
                    NegPropiedades.objTasacion.MontoTasacion = decimal.Parse(txtMontoTasacion.Text);
                    NegPropiedades.objTasacion.MontoAsegurado = decimal.Parse(txtMontoAseguradoIncendio.Text);
                    NegPropiedades.objTasacion.MontoLiquidacion = decimal.Parse(txtMontoLiquidacion.Text);
                    NegPropiedades.objTasacion.MetrosConstruidos = decimal.Parse(txtMetrosConstruccion.Text);
                    NegPropiedades.objTasacion.MetrosTerreno = txtMetrosTerreno.Text == "" ? 0 : decimal.Parse(txtMetrosTerreno.Text);
                    NegPropiedades.objTasacion.MetrosLogia = txtMetrosLogia.Text == "" ? 0 : decimal.Parse(txtMetrosLogia.Text);
                    NegPropiedades.objTasacion.MetrosTerraza = txtMetrosTerraza.Text == "" ? 0 : decimal.Parse(txtMetrosTerraza.Text);

                    NegPropiedades.objTasacion.FechaTasacion = DateTime.Parse(txtFechaTasacion.Text);
                    NegPropiedades.objTasacion.FechaRecepcionFinal = DateTime.Parse(txtFechaRecepcionFinal.Text);
                    NegPropiedades.objTasacion.PermisoEdificacion = txtPermisoEdificacion.Text;
                    NegPropiedades.objTasacion.PrimaSeguro = (NegPropiedades.objTasacion.TasaSeguro * NegPropiedades.objTasacion.MontoAsegurado);
                    rTasacion = nTasacion.GuardarTasacion(NegPropiedades.objTasacion);
                    if (rTasacion.ResultadoGeneral)
                    {


                        if (!NegSolicitudes.ActualizarSolicitud(NegSolicitudes.objSolicitudInfo)) return;
                        if (!ActualizarSegurosContratados(NegPropiedades.objTasacion)) return;
                        Controles.MostrarMensajeExito("Datos Grabados");
                        CargarPropiedades();
                        LimpiarInfoTasacion();
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
                    Controles.MostrarMensajeError("Error al Grabar los datos de la Tasación");
                }
            }
        }
        private bool ActualizarSegurosContratados(TasacionInfo oTasacion)
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();
                //Actualizacion del Seguro de Desgravamen


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
                    Controles.MostrarMensajeError("Error al Actualizar los Seguros Contratados");
                }
                return false;
            }
        }
        private void LimpiarInfoTasacion()
        {

            AddlTasador.ClearSelection();
            AddlFabTasadores.ClearSelection();
            txtFechaTasacion.Text = "";
            txtValorComercial.Text = "";
            txtMontoTasacion.Text = "";
            txtMontoAseguradoIncendio.Text = "";
            txtMontoLiquidacion.Text = "";
            txtMetrosTerreno.Text = "";
            txtMetrosTerraza.Text = "";
            txtMetrosLogia.Text = "";
            txtMetrosConstruccion.Text = "";
            txtAñoConstruccion.Text = "";
            lblDireccionTasacion.Text = "";
            txtFechaRecepcionFinal.Text = "";
            txtPermisoEdificacion.Text = "";
            txtRolSitio.Text = "";
            txtRolManzana.Text = "";
            chkIndUsoGoce.Checked = false;
            ddlComunaSii.ClearSelection();
            ddlTipoInmueble.ClearSelection();
            ddlAntiguedadProp.ClearSelection();
            ddlDestinoProp.ClearSelection();
            ddlTipoConstruccion.ClearSelection();
            NegPropiedades.objTasacion = null;
        }
        private bool ValidarInfoTasacion()
        {
            try
            {
                var ValorComercial = decimal.Zero;
                var MontoTasacion = decimal.Zero;
                var MontoAsegurado = decimal.Zero;
                var MontoLiquidacion = decimal.Zero;
                var MetrosContruidos = decimal.Zero;
                var MetrosTerreno = decimal.Zero;
                var MetrosLogia = decimal.Zero;
                var MetrosTerraza = decimal.Zero;
                var RolManzana = decimal.Zero;
                var RolSitio = decimal.Zero;
                var AñoConstruccion = 0;

                if (AddlTasador.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tasador");
                    return false;
                }
                if (txtFechaTasacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar una Fecha de Tasación");
                    return false;
                }
                if (txtFechaRecepcionFinal.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar una Fecha de Recepción Final");
                    return false;
                }
                if (txtPermisoEdificacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Permiso de Edificación");
                    return false;
                }
                if (txtAñoConstruccion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Año de Construcción");
                    return false;
                }
                if (int.TryParse(txtAñoConstruccion.Text, out AñoConstruccion))
                {
                    if (AñoConstruccion > DateTime.Now.Year)
                    {
                        Controles.MostrarMensajeAlerta("El año de Construicción no puede ser mayor al Año en curso");
                        return false;
                    }
                    if (AñoConstruccion < 1900)
                    {
                        Controles.MostrarMensajeAlerta("Año de Construcción Invalido (Debe se entre los años 1900 y " + DateTime.Now.Year.ToString() + ")");
                        return false;
                    }
                }

                if (txtValorComercial.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor Comercial");
                    return false;
                }
                if (decimal.TryParse(txtValorComercial.Text, out ValorComercial))
                {
                    if (ValorComercial <= 0)
                    {
                        Controles.MostrarMensajeAlerta("El Valor Comercial debe ser mayor a 0");
                        return false;
                    }
                }

                if (txtMontoTasacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto de Tasación");
                    return false;
                }
                if (decimal.TryParse(txtMontoTasacion.Text, out MontoTasacion))
                {
                    if (MontoTasacion <= 0)
                    {
                        Controles.MostrarMensajeAlerta("El Monto de Tasación debe ser mayor a 0");
                        return false;
                    }
                }



                if (txtMontoAseguradoIncendio.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto Asegurado");
                    return false;
                }
                if (decimal.TryParse(txtMontoAseguradoIncendio.Text, out MontoAsegurado))
                {
                    if (MontoAsegurado <= 0 && txtMontoAseguradoIncendio.Enabled == true && chkIndUsoGoce.Checked == false)
                    {
                        Controles.MostrarMensajeAlerta("El Monto Asegurado debe ser mayor a 0");
                        return false;
                    }
                }

                if (txtMontoLiquidacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto de Liquidación");
                    return false;
                }
                if (decimal.TryParse(txtMontoLiquidacion.Text, out MontoLiquidacion))
                {
                    if (MontoLiquidacion <= 0)
                    {
                        Controles.MostrarMensajeAlerta("El Monto de Liquidación debe ser mayor a 0");
                        return false;
                    }
                }

                if (txtMetrosTerreno.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor a los Metros del Terreno");
                    return false;
                }
                if (decimal.TryParse(txtMetrosTerreno.Text, out MetrosTerreno))
                {
                    if (MetrosTerreno <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Los Metros del Terreno deben ser mayor a 0");
                        return false;
                    }
                }

                if (txtMetrosConstruccion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor a los Metros Construidos");
                    return false;
                }
                if (decimal.TryParse(txtMetrosConstruccion.Text, out MetrosContruidos))
                {
                    if (MetrosContruidos <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Los Metros Construidos deben ser mayor a 0");
                        return false;
                    }
                }

                if (txtRolManzana.Text == "" && chkIndUsoGoce.Checked == false)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor a Rol - Manzana");
                    return false;
                }
                if (decimal.TryParse(txtRolManzana.Text, out RolManzana))
                {
                    if (RolManzana <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Rol - Manzana deben ser mayor a 0");
                        return false;
                    }
                }
                if (txtRolSitio.Text == "" && chkIndUsoGoce.Checked == false)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Valor a Rol - Sitio");
                    return false;
                }
                if (decimal.TryParse(txtRolSitio.Text, out RolSitio))
                {
                    if (RolSitio <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Rol - Sitio deben ser mayor a 0");
                        return false;
                    }
                }

                if (decimal.TryParse(txtMetrosLogia.Text, out MetrosLogia))
                {
                    if (MetrosLogia <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Los Metros de la Logia deben ser mayor a 0");
                        return false;
                    }
                }

                if (decimal.TryParse(txtMetrosTerraza.Text, out MetrosTerraza))
                {
                    if (MetrosTerraza <= 0)
                    {
                        Controles.MostrarMensajeAlerta("Los Metros de la Terraza deben ser mayor a 0");
                        return false;
                    }
                }
                if (ddlComunaSii.SelectedValue == "-1" || ddlComunaSii.SelectedValue == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Comuna - SII");
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
                    Controles.MostrarMensajeError("Error al Validar la Información de Tasación");
                }
                return false;
            }
        }
        protected void btnGrabarTasacion_Click(object sender, EventArgs e)
        {
            GrabarInfoTasacion();
        }
        protected void btnCancelarTasacion_Click(object sender, EventArgs e)
        {
            LimpiarInfoTasacion();
        }
        public void CargarParticipantes()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                NegParticipante NegParticipantes = new NegParticipante();
                Resultado<ParticipanteInfo> oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                oParticipante.TipoParticipacion_Id = Constantes.IdSolicitantePrincipal;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {


                    Controles.CargarGrid(ref gvParticipantes, oResultado.Lista, new string[] { Constantes.StringId, "Rut" });
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
        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
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
                txtMontoAseguradoIncendio.Text = "0";
            }
            else
            {
                txtRolManzana.Enabled = true;
                txtRolSitio.Enabled = true;
            }
        }
        private void CargaComboTipoInmueble()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new TipoInmuebleInfo();
                var ObjResultado = new Resultado<TipoInmuebleInfo>();
                var objNegocio = new NegPropiedades();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjInfo.VisibleEnSimulador = true;
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
        private void CargaComboInmobiliaria()
        {
            try
            {
                ////Declaracion de Variables
                var objInmobiliaria = new InmobiliariaInfo();
                var objResultado = new Resultado<InmobiliariaInfo>();
                var NegInmobiliaria = new NegInmobiliarias();

                ////Asignacion de Variables
                objInmobiliaria.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                objResultado = NegInmobiliaria.Buscar(objInmobiliaria);
                if (objResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<InmobiliariaInfo>(ref ddlInmobiliaria, objResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Inmobiliaria");
                }
            }
        }
        private void CargaComboProyecto(int Inmbobiliaria_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ProyectoInfo();
                var ObjResultado = new Resultado<ProyectoInfo>();
                var NegProyecto = new NegProyectos();

                ////Asignacion de Variables
                ObjInfo.Inmobiliaria_Id = Inmbobiliaria_Id;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = NegProyecto.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProyectoInfo>(ref ddlProyecto, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Proyecto");
                }
            }
        }
        protected void ddlInmobiliaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInmobiliaria.SelectedValue == "-1")
            {
                Controles.CargarCombo<UsuarioInfo>(ref ddlProyecto, null, "Id", "Descripcion", "-- Seleccione una Inmobiliaria", "-1");
            }
            else
            {
                CargaComboProyecto(int.Parse(ddlInmobiliaria.SelectedValue));
            }
        }
        private void CargarDatosSolicitud()
        {

            try
            {

                if (NegSolicitudes.objSolicitudInfo != null)
                {



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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Datos Resumen");
                }
            }


        }
    }
}