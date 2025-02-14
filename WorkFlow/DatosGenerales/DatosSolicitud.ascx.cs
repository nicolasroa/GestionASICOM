using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.DatosGenerales
{
    public partial class DatosSolicitud : System.Web.UI.UserControl
    {
        public static int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

        public static decimal UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                if (NegBandejaEntrada.oBandejaEntrada != null)
                {
                    if (NegBandejaEntrada.oBandejaEntrada.Malla_Id == 200)
                    {
                        ddlFinalidad.Enabled = true;
                        CargarFinalidadCredito();
                        CargarUtilidadCredito();
                        Controles.EjecutarJavaScript("DesplegarDatosAdicionales('1');");
                    }
                    else
                    {
                        Controles.EjecutarJavaScript("DesplegarDatosAdicionales('0');");
                        ddlFinalidad.Enabled = false;
                    }
                }
                else
                {
                    ddlFinalidad.Enabled = true;
                    CargarFinalidadCredito();
                    CargarUtilidadCredito();
                    Controles.EjecutarJavaScript("DesplegarDatosAdicionales('1');");
                }
                CargaCombos();
                CargarInformacionSolicitud();
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                lblValorUF.Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now));
                txtValorUF.Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now)).Replace(".", "");
                if (NegBandejaEntrada.oBandejaEntrada == null || !NegBandejaEntrada.oBandejaEntrada.IndModificaDatosCredito)
                {
                    Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.Button>(this).ForEach(c => c.Enabled = false);
                }
                if (NegBandejaEntrada.oBandejaEntrada != null)
                {
                    if (NegBandejaEntrada.oBandejaEntrada.Evento_Id == 27)//Evento Orden de Escrituración
                    {
                        Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Enabled = false);
                        Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.Enabled = false);
                        Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Enabled = false);
                        Controles.ModificarControles<Anthem.Button>(this).ForEach(c => c.Enabled = false);

                        ddlFinalidad.Enabled = true;
                        CargarFinalidadCredito();
                        CargarUtilidadCredito();
                        Controles.EjecutarJavaScript("DesplegarDatosAdicionales('1');");

                        ddlObjetivo.Enabled = true;
                        ddlDestino.Enabled = true;
                        ddlFinalidad.Enabled = true;
                        ddlUtilidad.Enabled = true;
                        btnGuardarSolicitud.Enabled = true;


                    }
                }
            }

        }

        protected void ddlTipoFinanciamiento_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTipoFinanciamiento.SelectedValue == "-1")
            {
                Controles.CargarCombo<ProductoInfo>(ref ddlProducto, null, "Id", "Descripcion", "-- Seleccione un Producto", "-1");
            }
            else
            {
                CargaComboProducto(int.Parse(ddlTipoFinanciamiento.SelectedValue));
            }
        }
        protected void ddlSubsidio_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitaTxtSubsidio();
        }

        protected void ddlObjetivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlObjetivo.SelectedValue == "-1")
            {
                ddlDestino.ClearSelection();
                Controles.CargarCombo<DestinoInfo>(ref ddlDestino, new List<DestinoInfo>(), "Id", "Descripcion", "-- Seleccione un Objetivo", "-1");
            }
            else
            {
                CargaComboDestino(int.Parse(ddlObjetivo.SelectedValue));
            }
        }
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionaProducto();
        }

        protected void ddlSubsidio_SelectedIndexChanged1(object sender, EventArgs e)
        {
            HabilitaTxtSubsidio();

        }
        #endregion

        #region Metodos

        ////Metodos de Carga Inicial

        private void CargaCombos()
        {
            Controles.CargarCombo<UsuarioInfo>(ref ddlProducto, null, "Id", "Descripcion", "-- Seleccione un Tipo de Financiamiento", "-1");
            CargaComboTipoFinanciamiento();
            CargaComboObjetivo();
            Controles.CargarCombo<UsuarioInfo>(ref ddlDestino, null, "Id", "Descripcion", "-- Seleccione un Objetivo", "-1");
            CargaComboSubsidio();
            CargarInstitucionesFinancieras();
        }
        private void CargarInstitucionesFinancieras()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("INSTITUCIONES_FINANCIERAS");
                if (Lista.Count != 0)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlBancoPac, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Institución Financiera --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Institución Financiera Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Institución Financiera");
                }
            }
        }
        private void CargaComboTipoFinanciamiento()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new TipoFinanciamientoInfo();
                var ObjResultado = new Resultado<TipoFinanciamientoInfo>();
                var NegProyecto = new NegTipoFinanciamiento();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                //ObjInfo.Rol_Id = Constantes.IdRolEjecutivoComercial;
                ObjResultado = NegProyecto.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoFinanciamientoInfo>(ref ddlTipoFinanciamiento, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Tipo Financiamiento--", "-1");
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
        private void CargaComboProducto(int TipoFinanciamiento_Id)
        {
            try
            {
                var productoInfo = new ProductoInfo();
                var oNegProducto = new NegProductos();
                var oResultado = new Resultado<ProductoInfo>();

                productoInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                productoInfo.TipoFinanciamiento_Id = TipoFinanciamiento_Id;
                oResultado = oNegProducto.Buscar(productoInfo);

                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProductoInfo>(ref ddlProducto, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Productos--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Productos");
                }
            }
        }
        private void CargaComboObjetivo()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ObjetivoInfo();
                var ObjResultado = new Resultado<ObjetivoInfo>();
                var objNegocio = new NegObjetivo();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                //ObjInfo.Rol_Id = Constantes.IdRolEjecutivoComercial;
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ObjetivoInfo>(ref ddlObjetivo, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Objetivo--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Objetivo");
                }
            }
        }
        private void CargaComboDestino(int Objetivo_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new DestinoInfo();
                var ObjResultado = new Resultado<DestinoInfo>();
                var objNegocio = new NegDestino();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjInfo.Objetivo_Id = Objetivo_Id;
                //ObjInfo.Rol_Id = Constantes.IdRolEjecutivoComercial;
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<DestinoInfo>(ref ddlDestino, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Destino--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Destino");
                }
            }
        }
        private void CargaComboSubsidio()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SubsidioInfo();
                var ObjResultado = new Resultado<SubsidioInfo>();
                var objNegocio = new NegSubsidio();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                //ObjInfo.Objetivo_Id = Objetivo_Id;
                //ObjInfo.Rol_Id = Constantes.IdRolEjecutivoComercial;
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SubsidioInfo>(ref ddlSubsidio, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Sin Subsidio--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Subsidio");
                }
            }
        }
        private void CargaComboPlazos(int Producto_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new PlazosSimuladorInfo();
                var ObjResultado = new Resultado<PlazosSimuladorInfo>();
                var objNegocio = new NegPlazosSimulador();
                ObjInfo.Producto_Id = Producto_Id;
                ////Asignacion de Variables
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    var objProducto = NegProductos.lstProductos.FirstOrDefault(p => p.Id == Producto_Id);
                    Controles.CargarCombo<PlazosSimuladorInfo>(ref ddlPlazo, ObjResultado.Lista, "Plazo", "Plazo", "--Plazos--", "-1");
                    ddlPlazoFlexible.Enabled = false;
                    ddlPlazoFlexible.ClearSelection();

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Plazos");
                }
            }
        }

        private void CargaComboPlazosSecundarios(int Producto_Id, int plazo)
        {
            try
            {

                var objProducto = NegProductos.lstProductos.FirstOrDefault(p => p.Id == Producto_Id);
                if (objProducto == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Producto");
                    return;
                }
                ////Declaracion de Variables
                var ObjInfo = new PlazosSimuladorInfo();
                var ObjResultado = new Resultado<PlazosSimuladorInfo>();
                var objNegocio = new NegPlazosSimulador();
                ObjInfo.Producto_Id = Producto_Id;
                ////Asignacion de Variables
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {


                    if (objProducto.IndDoblePlazo == true)
                    {
                        Controles.CargarCombo<PlazosSimuladorInfo>(ref ddlPlazoFlexible, ObjResultado.Lista.Where(p => p.Plazo < plazo).ToList(), "Plazo", "Plazo", "--Plazo Flexible--", "-1");
                        ddlPlazoFlexible.Enabled = true;
                    }
                    else
                    {
                        Controles.CargarCombo<PlazosSimuladorInfo>(ref ddlPlazoFlexible, null, "Plazo", "Plazo", "--Plazo Flexible--", "-1");
                        ddlPlazoFlexible.Enabled = false;

                    }

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Plazos");
                }
            }
        }


        private void CargaComboGracia(int Producto_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new GraciaInfo();
                var ObjResultado = new Resultado<GraciaInfo>();
                var objNegocio = new NegProductos();

                ////Asignacion de Variables
                ObjInfo.Producto_Id = Producto_Id;
                ObjResultado = objNegocio.BuscarGracia(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<GraciaInfo>(ref ddlMesesGracia, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Sin Periodo de Gracia--", "0");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Gracia");
                }
            }
        }
        public bool ValidarDatosCredito()
        {
            try
            {
                if (ddlTipoFinanciamiento.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Financiamiento");
                    return false;
                }
                if (ddlProducto.SelectedValue == "-1")
                {

                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Producto");

                    return false;
                }
                if (ddlObjetivo.SelectedValue == "-1")
                {

                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Objetivo");

                    return false;
                }

                if (ddlDestino.SelectedValue == "-1")
                {

                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Destino");

                    return false;
                }

                if (txtPrecioVenta.Text == "")
                {

                    Controles.MostrarMensajeAlerta("Debe Ingresar un Precio de Venta");

                    return false;
                }

                if (txtMontoCredito.Text == "")
                {

                    Controles.MostrarMensajeAlerta("Debe Ingresar el Monto del Crédito");

                    return false;
                }
                if (ddlPlazo.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Plazo");
                    return false;
                }
                if (ddlPlazoFlexible.SelectedValue == "-1" && ddlPlazoFlexible.Enabled)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Plazo Flexible");
                    return false;
                }
                NegConfiguracionHipotecaria nConfigHipo = new NegConfiguracionHipotecaria();
                ConfiguracionHipotecariaInfo oConfigHipo = new ConfiguracionHipotecariaInfo();
                Resultado<ConfiguracionHipotecariaInfo> rConfigHipo = new Resultado<ConfiguracionHipotecariaInfo>();

                oConfigHipo.Producto_Id = int.Parse(ddlProducto.SelectedValue);
                oConfigHipo.Destino_Id = int.Parse(ddlDestino.SelectedValue);
                oConfigHipo.Subsidio_Id = int.Parse(ddlSubsidio.SelectedValue);


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


                if (decimal.Parse(txtPorcentajeFinanciamiento.Text.Replace(".", ",")) > oConfigHipo.PorcentajeFinanciamientoMaximo)
                {

                    Controles.MostrarMensajeAlerta("El % de Finanmiento Máximo Permitido es de " + oConfigHipo.PorcentajeFinanciamientoMaximo.ToString() + "%");
                    return false;
                }

                if (decimal.Parse(txtPorcentajeFinanciamiento.Text.Replace(".", ",")) < oConfigHipo.PorcentajeFinanciamientoMinimo)
                {

                    Controles.MostrarMensajeAlerta("El % de Finanmiento Mínimo Permitido es de " + oConfigHipo.PorcentajeFinanciamientoMaximo.ToString() + "%");
                    return false;
                }

                if (decimal.Parse(ddlPlazo.SelectedValue) > oConfigHipo.PlazoMaximo)
                {

                    Controles.MostrarMensajeAlerta("El Plazo Máximo Permitido es de " + oConfigHipo.PlazoMaximo.ToString() + " Años");
                    return false;
                }

                if (decimal.Parse(ddlPlazo.SelectedValue) < oConfigHipo.PlazoMinimo&& ddlPlazoFlexible.Enabled)
                {

                    Controles.MostrarMensajeAlerta("El Plazo Mínimo Permitido es de " + oConfigHipo.PlazoMinimo.ToString() + " Años");
                    return false;
                }

                if (decimal.Parse(ddlPlazoFlexible.SelectedValue) > oConfigHipo.PlazoMaximo && ddlPlazoFlexible.Enabled)
                {

                    Controles.MostrarMensajeAlerta("El Plazo Flexible Máximo Permitido es de " + oConfigHipo.PlazoMaximo.ToString() + " Años");
                    return false;
                }

                if (decimal.Parse(ddlPlazoFlexible.SelectedValue) < oConfigHipo.PlazoMinimo && ddlPlazoFlexible.Enabled)
                {

                    Controles.MostrarMensajeAlerta("El Plazo Flexible Mínimo Permitido es de " + oConfigHipo.PlazoMinimo.ToString() + " Años");
                    return false;
                }

                if (decimal.Parse(txtMontoCredito.Text.Replace(".", ",")) > oConfigHipo.MontoCreditoMaximo)
                {

                    Controles.MostrarMensajeAlerta("El Monto Máximo de Crédito Permitido es de " + oConfigHipo.MontoCreditoMaximo.ToString() + " UF");
                    return false;
                }

                if (decimal.Parse(txtMontoCredito.Text.Replace(".", ",")) < oConfigHipo.MontoCreditoMinimo)
                {

                    Controles.MostrarMensajeAlerta("El Monto Mínimo de Crédito Permitido es de " + oConfigHipo.MontoCreditoMinimo.ToString() + " UF");
                    return false;
                }

                if (decimal.Parse(txtPorcentajeFinanciamiento.Text.Replace(".", ",")) > oConfigHipo.PorcentajeFinanciamientoMaximo && ddlMesesGracia.SelectedValue != "0")
                {

                    Controles.MostrarMensajeAlerta("Para % de financiamiento mayores a " + oConfigHipo.PorcentajeFinanciamientoMaximo.ToString() + "% no se permiten meses de Gracia");
                    return false;
                }





                decimal TasaMensual = decimal.Zero;
                var TMC = decimal.Zero;
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                //  NegSolicitudes.objSolicitudInfo


                TMC = NegParidad.ObtenerTMC(CodigoMoneda, DateTime.Now, int.Parse(ddlPlazo.SelectedValue), decimal.Parse(txtMontoCredito.Text));

                if (decimal.TryParse(txtTasaMensual.Text, out TasaMensual))
                {
                    if (TasaMensual <= 0)
                    {
                        Controles.MostrarMensajeAlerta("La Tasa debe ser Mayor a 0");
                        return false;
                    }
                    if (TasaMensual > TMC)
                    {
                        Controles.MostrarMensajeAlerta("La tasa Ingresada no puede ser mayor a la Tasa Máxima Convencional (" + string.Format("{0:F4}", TMC) + " %)");
                        return false;
                    }
                }
                else
                {

                    Controles.MostrarMensajeAlerta("Debe Ingresar una Tasa");
                    return false;

                }

                if (ddlUtilidad.Enabled)
                {
                    if (ddlFinalidad.SelectedValue == "1")
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar una Finalidad");
                        return false;
                    }

                    if (ddlUtilidad.SelectedValue == "1")
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar una Utilidad");
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
                    Controles.MostrarMensajeError("Error al Validar Datos Generales");
                }
                return false;
            }
        }
        private void SeleccionaProducto()
        {
            CargaComboGracia(int.Parse(ddlProducto.SelectedValue));
            CargaComboPlazos(int.Parse(ddlProducto.SelectedValue));

        }
        private void HabilitaTxtSubsidio()
        {
            if (int.Parse(ddlSubsidio.SelectedValue) == -1)
            {
                txtMontoSubsidio.Text = "0";
                txtBonoIntegracion.Text = "0";
                txtBonoCaptacion.Text = "0";
                txtMontoSubsidio.Enabled = false;
                txtBonoIntegracion.Enabled = false;
                txtBonoCaptacion.Enabled = false;
                Controles.EjecutarJavaScript("ActualizaMontosPreaprobacion('MontoSubsidio');");

            }
            else
            {
                txtMontoSubsidio.Enabled = true;
                txtBonoIntegracion.Enabled = true;
                txtBonoCaptacion.Enabled = true;
                txtMontoSubsidio.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.MontoSubsidio);
                txtBonoIntegracion.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.MontoBonoIntegracion);
                txtBonoCaptacion.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.MontoBonoCaptacion);
                Controles.EjecutarJavaScript("ActualizaMontosPreaprobacion('MontoSubsidio');");
            }
        }
        private void LimpiarFormulario()
        {

            NegClientes.objClienteInfo = new ClientesInfo();
            NegClientes.IndNuevoCliente = false;
        }
        private void CargarInformacionSolicitud()
        {
            try
            {
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegSolicitudes.objSolicitudInfo.Id;
                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {

                        oSolicitud = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                        NegSolicitudes.objSolicitudInfo = oSolicitud;
                        ddlTipoFinanciamiento.SelectedValue = oSolicitud.TipoFinanciamiento_Id.ToString();
                        CargaComboProducto(oSolicitud.TipoFinanciamiento_Id);
                        ddlProducto.SelectedValue = oSolicitud.Producto_Id.ToString();

                        ddlObjetivo.SelectedValue = oSolicitud.Objetivo_Id.ToString();
                        CargaComboDestino(oSolicitud.Objetivo_Id);
                        ddlDestino.SelectedValue = oSolicitud.Destino_Id.ToString();
                        txtPrecioVenta.Text = string.Format("{0:F4}", oSolicitud.MontoPropiedad);
                        ddlSubsidio.SelectedValue = oSolicitud.Subsidio_Id.ToString();
                        HabilitaTxtSubsidio();
                        txtMontoSubsidio.Text = string.Format("{0:F4}", oSolicitud.MontoSubsidio);
                        txtBonoIntegracion.Text = string.Format("{0:F4}", oSolicitud.MontoBonoIntegracion);
                        txtBonoCaptacion.Text = string.Format("{0:F4}", oSolicitud.MontoBonoCaptacion);

                        txtMontoContado.Text = string.Format("{0:F4}", oSolicitud.MontoContado);
                        txtMontoCredito.Text = string.Format("{0:F4}", oSolicitud.MontoCredito);
                        txtPorcentajeFinanciamiento.Text = string.Format("{0:0}", oSolicitud.PorcentajeFinanciamiento);

                        txtPrecioVentaPesos.Text = string.Format("{0:C0}", oSolicitud.MontoPropiedad * UF);
                        txtMontoContadoPesos.Text = string.Format("{0:C0}", oSolicitud.MontoContado * UF);
                        txtMontoCreditoPesos.Text = string.Format("{0:C0}", oSolicitud.MontoCredito * UF);
                        txtMontoSubsidioPesos.Text = string.Format("{0:C0}", oSolicitud.MontoSubsidio * UF);
                        txtBonoIntegracionPesos.Text = string.Format("{0:C0}", oSolicitud.MontoBonoIntegracion * UF);
                        txtBonoCaptacionPesos.Text = string.Format("{0:C0}", oSolicitud.MontoBonoCaptacion * UF);

                        CargaComboGracia(oSolicitud.Producto_Id);
                        ddlMesesGracia.SelectedValue = oSolicitud.Gracia.ToString();
                        CargaComboPlazos(oSolicitud.Producto_Id);
                        ddlPlazo.SelectedValue = oSolicitud.Plazo.ToString();
                        CargaComboPlazosSecundarios(oSolicitud.Producto_Id, oSolicitud.Plazo);
                        ddlPlazoFlexible.SelectedValue = oSolicitud.Plazo2.ToString();
                        chIndDfl2.Checked = oSolicitud.IndDfl2;
                        chkViviendaSocial.Checked = oSolicitud.IndViviendaSocial;
                        txtTasaMensual.Text = string.Format("{0:F4}", oSolicitud.TasaFinal);
                        ActualizarInformacionCalculada();

                        if (ddlFinalidad.Enabled)
                        {
                            if (oSolicitud.Finalidad_Id > 0)
                                ddlFinalidad.SelectedValue = oSolicitud.Finalidad_Id.ToString();
                            if (oSolicitud.Utilidad_Id > 0)
                                ddlUtilidad.SelectedValue = oSolicitud.Utilidad_Id.ToString();
                        }
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar datos de la Solicitud");
                }
            }
        }

        private void CargarFinalidadCredito()
        {
            try
            {
                NegSolicitudes negSolicitudes = new NegSolicitudes();
                FinalidadCreditoInfo oFinalidad = new FinalidadCreditoInfo();
                Resultado<FinalidadCreditoInfo> rFinalidad = new Resultado<FinalidadCreditoInfo>();

                rFinalidad = negSolicitudes.BuscarFinalidad(oFinalidad);
                if (rFinalidad.ResultadoGeneral)
                {
                    Controles.CargarCombo<FinalidadCreditoInfo>(ref ddlFinalidad, rFinalidad.Lista, "Id", "Descripcion", "", "");
                }
                else
                {
                    Controles.MostrarMensajeError(rFinalidad.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar la Finalidad del Credito");
                }
            }
        }

        private void CargarUtilidadCredito()
        {
            try
            {
                NegSolicitudes negSolicitudes = new NegSolicitudes();
                UtilidadCreditoInfo oUtilidad = new UtilidadCreditoInfo();
                Resultado<UtilidadCreditoInfo> rFinalidad = new Resultado<UtilidadCreditoInfo>();

                rFinalidad = negSolicitudes.BuscarUtilidad(oUtilidad);
                if (rFinalidad.ResultadoGeneral)
                {
                    Controles.CargarCombo<UtilidadCreditoInfo>(ref ddlUtilidad, rFinalidad.Lista, "Id", "Descripcion", "", "");
                }
                else
                {
                    Controles.MostrarMensajeError(rFinalidad.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar la Finalidad del Credito");
                }
            }
        }

        public bool GrabarSolicitud(bool Evento = false)
        {
            try
            {
                if (!ValidarDatosCredito()) return false;
                SolicitudInfo oSolicitud = new SolicitudInfo();
                NegSolicitudes negSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> oResultadoSolicitud = new Resultado<SolicitudInfo>();


                if (NegSolicitudes.objSolicitudInfo == null)
                {

                    if (NegBandejaEntrada.oBandejaEntrada == null)
                    {
                        Controles.MostrarMensajeError("No hay Solicitud para Procesar, favor volver a cargar la Bandeja de Entrada");
                        return false;
                    }
                    oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                    oResultadoSolicitud = negSolicitud.Buscar(oSolicitud);
                    if (oResultadoSolicitud.ResultadoGeneral)
                    {
                        if (oResultadoSolicitud.Lista.Count != 0)
                        {
                            NegSolicitudes.objSolicitudInfo = oResultadoSolicitud.Lista.FirstOrDefault();
                        }
                        else
                        {
                            Controles.MostrarMensajeError("No hay Solicitud para Procesar, favor volver a cargar la Bandeja de Entrada");
                            return false;
                        }
                    }
                    else
                    {
                        Controles.MostrarMensajeError(oResultadoSolicitud.Mensaje);
                        return false;
                    }
                }

                oSolicitud = NegSolicitudes.objSolicitudInfo;

                oSolicitud.TipoFinanciamiento_Id = int.Parse(ddlTipoFinanciamiento.SelectedValue);
                oSolicitud.Producto_Id = int.Parse(ddlProducto.SelectedValue);
                oSolicitud.Objetivo_Id = int.Parse(ddlObjetivo.SelectedValue);
                oSolicitud.Destino_Id = int.Parse(ddlDestino.SelectedValue);
                oSolicitud.Subsidio_Id = int.Parse(ddlSubsidio.SelectedValue);
                oSolicitud.PorcentajeFinanciamiento = decimal.Parse(txtPorcentajeFinanciamiento.Text.Replace(".", ","));
                oSolicitud.IndDfl2 = chIndDfl2.Checked;
                oSolicitud.Gracia = int.Parse(ddlMesesGracia.SelectedValue);
                oSolicitud.MontoPropiedad = string.IsNullOrEmpty(txtPrecioVenta.Text) ? 0 : decimal.Parse(txtPrecioVenta.Text.Replace(".", ","));
                oSolicitud.MontoCredito = string.IsNullOrEmpty(txtMontoCredito.Text) ? 0 : decimal.Parse(txtMontoCredito.Text.Replace(".", ","));
                oSolicitud.MontoContado = string.IsNullOrEmpty(txtMontoContado.Text) ? 0 : decimal.Parse(txtMontoContado.Text.Replace(".", ","));
                oSolicitud.MontoSubsidio = string.IsNullOrEmpty(txtMontoSubsidio.Text) ? 0 : decimal.Parse(txtMontoSubsidio.Text.Replace(".", ","));
                oSolicitud.MontoBonoIntegracion = string.IsNullOrEmpty(txtBonoIntegracion.Text) ? 0 : decimal.Parse(txtBonoIntegracion.Text.Replace(".", ","));
                oSolicitud.MontoBonoCaptacion = string.IsNullOrEmpty(txtBonoCaptacion.Text) ? 0 : decimal.Parse(txtBonoCaptacion.Text.Replace(".", ","));
                oSolicitud.Plazo = int.Parse(ddlPlazo.SelectedItem.ToString());
                oSolicitud.Plazo2 = int.Parse(ddlPlazoFlexible.SelectedValue);
                oSolicitud.TasaFinal = decimal.Parse(txtTasaMensual.Text);
                oSolicitud.TasaBase = decimal.Parse(txtTasaMensual.Text);
                oSolicitud.IndDfl2 = chIndDfl2.Checked;
                oSolicitud.IndViviendaSocial = chkViviendaSocial.Checked;
                oSolicitud.IndPac = chkIndPac.Checked;
                if (oSolicitud.IndPac)
                {
                    oSolicitud.Banco_Id = int.Parse(txtCuentaPac.Text);
                    oSolicitud.CuentaPac = txtCuentaPac.Text;
                }


                if (ddlFinalidad.Enabled)
                {
                    oSolicitud.Finalidad_Id = int.Parse(ddlFinalidad.SelectedValue);
                    oSolicitud.Utilidad_Id = int.Parse(ddlUtilidad.SelectedValue);
                }

                oResultadoSolicitud = negSolicitud.Guardar(ref oSolicitud);
                if (oResultadoSolicitud.ResultadoGeneral)
                {
                    NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                    NegSolicitudes.objSolicitudInfo = oSolicitud;
                    NegSolicitudes.RecalcularDividendo();

                    ActualizarInformacionCalculada();


                    if (!Evento)
                        Controles.MostrarMensajeExito("Datos de la Solicitud Grabados");

                }
                else
                {
                    Controles.MostrarMensajeError(oResultadoSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Generar la Solicitud");
                }
                return false;
            }
        }



        private void ActualizarInformacionCalculada()
        {
            //Actualizacion de Informacion Calculada
            txtCAE.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.CAE);
            txtCTC.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.CTC);
            txtValorCuota.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUF) + "(" + string.Format("{0:C0}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesos) + ")";
            txtValorDividendo.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUfTotal) + "(" + string.Format("{0:C0}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesosTotal) + ")";
            txtValorCuotaFlexible.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoFlexibleUF) + "(" + string.Format("{0:C0}", NegSolicitudes.objSolicitudInfo.ValorDividendoFlexiblePesos) + ")";
            txtValorDividendoFlexible.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.ValorDividendoFlexibleUfTotal) + "(" + string.Format("{0:C0}", NegSolicitudes.objSolicitudInfo.ValorDividendoFlexiblePesosTotal) + ")";
        }

        #endregion

        protected void btnGuardarSolicitud_Click(object sender, EventArgs e)
        {
            GrabarSolicitud();
        }

        protected void btnCancelarSolicitud_Click(object sender, EventArgs e)
        {

        }

        protected void ddlPlazo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlazo.SelectedValue == "-1")
            {
                Controles.CargarCombo<PlazosSimuladorInfo>(ref ddlPlazoFlexible, null, "Plazo", "Plazo", "--Plazo Flexible--", "-1");
                ddlPlazoFlexible.Enabled = false;
            }
            else
            {
                CargaComboPlazosSecundarios(int.Parse(ddlProducto.SelectedValue), int.Parse(ddlPlazo.SelectedValue));
            }
        }
    }
}