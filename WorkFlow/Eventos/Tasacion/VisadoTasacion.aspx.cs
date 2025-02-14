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
    public partial class VisadoTasacion : System.Web.UI.Page
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
                    if (!ValidarObservaciones()) return;
                    if (!ActualizaSolicitud()) return;
                    if (!NegSolicitudes.RecalcularDividendo()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!ValidarPropiedades()) return;
                    if (!ValidarObservaciones()) return;
                    if (!ActualizaSolicitud()) return;
                    if (!NegSolicitudes.RecalcularDividendo()) return;

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


                    oTasacion = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault();
                    NegPropiedades.objTasacion = new TasacionInfo();
                    NegPropiedades.objTasacion = oTasacion;
                    CargarInfoTasacion(oTasacion);

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


                //Validacion de % de Financiamiento segun Monto de Tasación

                NegConfiguracionHipotecaria nConfigHipo = new NegConfiguracionHipotecaria();
                ConfiguracionHipotecariaInfo oConfigHipo = new ConfiguracionHipotecariaInfo();
                Resultado<ConfiguracionHipotecariaInfo> rConfigHipo = new Resultado<ConfiguracionHipotecariaInfo>();

                oConfigHipo.Producto_Id = NegSolicitudes.objSolicitudInfo.Producto_Id;
                oConfigHipo.Destino_Id = NegSolicitudes.objSolicitudInfo.Destino_Id;
                oConfigHipo.Subsidio_Id = NegSolicitudes.objSolicitudInfo.Subsidio_Id;


                rConfigHipo = nConfigHipo.Buscar(oConfigHipo);
                if (rConfigHipo.ResultadoGeneral)
                {
                    if (rConfigHipo.Lista.Count() > 0)
                        oConfigHipo = rConfigHipo.Lista.FirstOrDefault();
                    else
                    {
                        Controles.MostrarMensajeError("Configuración Hipotecaria No Encontrada para esta combinación de financiamiento, Favor Informar al Administrador.");
                        return false;
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rConfigHipo.Mensaje);
                    return false;
                }

                decimal TotalMontoTasacion = decimal.Zero;
                TotalMontoTasacion = NegPropiedades.lstPropiedadesTasaciones.Sum(t => t.MontoTasacion);
                decimal MontoValidacion = decimal.Zero;

                if (TotalMontoTasacion > NegSolicitudes.objSolicitudInfo.MontoPropiedad)
                    MontoValidacion = NegSolicitudes.objSolicitudInfo.MontoPropiedad;
                else
                    MontoValidacion = TotalMontoTasacion;


                decimal PorcentajeFinanciamiento = decimal.Zero;
                PorcentajeFinanciamiento = (NegSolicitudes.objSolicitudInfo.MontoCredito * 100) / MontoValidacion;
                PorcentajeFinanciamiento = decimal.Round(PorcentajeFinanciamiento, 2);
                if (PorcentajeFinanciamiento > oConfigHipo.PorcentajeFinanciamientoMaximo)
                {
                    Controles.MostrarMensajeAlerta("El % de Finanmiento Máximo Permitido es de " + oConfigHipo.PorcentajeFinanciamientoMaximo.ToString() + "%");
                    return false;
                }

                if (PorcentajeFinanciamiento < oConfigHipo.PorcentajeFinanciamientoMinimo)
                {
                    Controles.MostrarMensajeAlerta("El % de Finanmiento Mínimo Permitido es de " + oConfigHipo.PorcentajeFinanciamientoMinimo.ToString() + "%");
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
        protected void btnSeleccionarPropTasacion_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvPropiedadesTasacion.DataKeys[row.RowIndex].Values["Id"].ToString());
            TasacionInfo oTasacion = new TasacionInfo();
            oTasacion = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault(t => t.Id == Id);
            NegPropiedades.objTasacion = new TasacionInfo();
            NegPropiedades.objTasacion = oTasacion;
            CargarInfoTasacion(oTasacion);
        }
        private void CargarInfoTasacion(TasacionInfo oTasacion)
        {
            try
            {
                if (oTasacion != null)
                {
                    Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();
                    PropiedadInfo oPropiedad = new PropiedadInfo();
                   
                    NegPropiedades nPropiedades = new NegPropiedades();

                    oPropiedad.Id = oTasacion.Propiedad_Id;
                    rPropiedad = nPropiedades.BuscarPropiedad(oPropiedad);
                    if (!rPropiedad.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rPropiedad.Mensaje);
                        return;
                    }

                    oPropiedad = rPropiedad.Lista.FirstOrDefault();

                    AddlFabTasadores.SelectedValue = oTasacion.FabricaTasador_Id.ToString();
                    CargaComboTasadores(oTasacion.FabricaTasador_Id);
                    AddlTasador.SelectedValue = oTasacion.Tasador_Id.ToString();
                    txtMontoTasacion.Text = string.Format("{0:F4}", oTasacion.MontoTasacion);
                    txtMontoAseguradoIncendio.Text = string.Format("{0:F4}", oTasacion.MontoAsegurado);
                    txtMontoLiquidacion.Text = string.Format("{0:F4}", oTasacion.MontoLiquidacion);
                    txtFechaTasacion.Text = oTasacion.FechaTasacion == null ? "" : oTasacion.FechaTasacion.Value.ToShortDateString();
                    txtMetrosTerreno.Text = string.Format("{0:F4}", oTasacion.MetrosTerreno);
                    txtMetrosConstruccion.Text = string.Format("{0:F4}", oTasacion.MetrosConstruidos);
                    txtAñoConstruccion.Text = oPropiedad.AñoConstruccion.ToString();
                    txtRolManzana.Text = oPropiedad.RolManzana.ToString();
                    txtRolSitio.Text = oPropiedad.RolSitio.ToString();
                    txtFechaRecepcionFinal.Text = oTasacion.FechaRecepcionFinal == null ? "" : oTasacion.FechaRecepcionFinal.Value.ToShortDateString();
                    txtPermisoEdificacion.Text = oTasacion.PermisoEdificacion;
                    lblDireccionTasacion.Text = oTasacion.DireccionCompleta;


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
                TasacionInfo oTasacion = new TasacionInfo();
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


                oPropiedad.RolManzana = txtRolManzana.Text == "" ? -1 : int.Parse(txtRolManzana.Text);
                oPropiedad.RolSitio = txtRolSitio.Text == "" ? -1 : int.Parse(txtRolSitio.Text);
                oPropiedad.AñoConstruccion = int.Parse(txtAñoConstruccion.Text);
                rPropiedad = nPropiedad.GuardarPropiedad(ref oPropiedad);
                if (rPropiedad.ResultadoGeneral)
                {
                    oTasacion = NegPropiedades.objTasacion;
                    oTasacion.MontoTasacion = decimal.Parse(txtMontoTasacion.Text);
                    oTasacion.MontoAsegurado = decimal.Parse(txtMontoAseguradoIncendio.Text);
                    oTasacion.MontoLiquidacion = decimal.Parse(txtMontoLiquidacion.Text);
                    oTasacion.MetrosConstruidos = decimal.Parse(txtMetrosConstruccion.Text);
                    oTasacion.MetrosTerreno = txtMetrosTerreno.Text == "" ? 0 : decimal.Parse(txtMetrosTerreno.Text);
                   
                    oTasacion.FechaTasacion = DateTime.Parse(txtFechaTasacion.Text);
                    oTasacion.FechaRecepcionFinal = DateTime.Parse(txtFechaRecepcionFinal.Text);
                    oTasacion.PermisoEdificacion = txtPermisoEdificacion.Text;

                    rTasacion = nTasacion.GuardarTasacion(oTasacion);
                    if (rTasacion.ResultadoGeneral)
                    {

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
        private void LimpiarInfoTasacion()
        {

            AddlTasador.ClearSelection();
            AddlFabTasadores.ClearSelection();
            txtFechaTasacion.Text = "";
            txtMontoTasacion.Text = "";
            txtMontoAseguradoIncendio.Text = "";
            txtMontoLiquidacion.Text = "";
            txtMetrosTerreno.Text = "";
            txtMetrosConstruccion.Text = "";
            txtAñoConstruccion.Text = "";
            lblDireccionTasacion.Text = "";
            txtFechaRecepcionFinal.Text = "";
            txtPermisoEdificacion.Text = "";
            NegPropiedades.objTasacion = null;
        }
        private bool ValidarInfoTasacion()
        {
            try
            {
                var MontoTasacion = decimal.Zero;
                var MontoAsegurado = decimal.Zero;
                var MontoLiquidacion = decimal.Zero;
                var MetrosContruidos = decimal.Zero;
                var MetrosTerreno = decimal.Zero;
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
                    if (MontoAsegurado <= 0 && txtMontoAseguradoIncendio.Enabled == true)
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

                if (txtRolManzana.Text == "")
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
                if (txtRolSitio.Text == "")
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
        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
        private bool ActualizaSolicitud()
        {

            try
            {
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                NegSolicitudes nSolicitud = new NegSolicitudes();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                    NegSolicitudes.objSolicitudInfo = rSolicitud.Lista.FirstOrDefault();
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    return false;
                }

                NegSolicitudes.objSolicitudInfo.FechaAprobacionTasacion = DateTime.Now;

                rSolicitud = nSolicitud.Guardar(NegSolicitudes.objSolicitudInfo);
                if (rSolicitud.ResultadoGeneral)
                {
                    return true;
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
                    return false;
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
                    Controles.MostrarMensajeError("Error al Cargar Información del EETT");
                }
            }
            return false;
        }
    }
}