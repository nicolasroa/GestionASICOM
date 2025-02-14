using WorkFlow.Entidades;
using WorkFlow.Entidades.Documental;
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


namespace WorkFlow.Aplicaciones
{
    public partial class SimuladorHipotecario : System.Web.UI.Page
    {

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (NegClientes.IndClienteModal == true && NegClientes.objClienteInfo != null)
            {
                CargaCliente();
                NegClientes.IndClienteModal = false;
                Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "AvanzaCliente();", true);
            }
            if (NegSimulacionHipotecaria.indSeleccionSimulacionCliente == true && NegSimulacionHipotecaria.oSimulacion != null)
            {

                CargarSimulacion();
                Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "AvanzaCliente();", true);
                NegSimulacionHipotecaria.indSeleccionSimulacionCliente = false;
            }
            if (!Page.IsPostBack)
            {
                CargaCombos();
                NegSimulacionHipotecaria.oSimulacion = new SimulacionHipotecariaInfo();
                NegClientes.objClienteInfo = new ClientesInfo();
                Controles.EjecutarJavaScript("MostrarSimulacionMinvu('false');");
                Controles.EjecutarJavaScript("MostrarSimulacionFlexible('false');");
            }
        }
        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente();
        }
        protected void ddlTipoFinanciamiento_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTipoFinanciamiento.SelectedValue == "-1")
            {
                Controles.CargarCombo<UsuarioInfo>(ref ddlProducto, null, "Id", "Descripcion", "-- Seleccione un Tipo de Financiamiento", "-1");
            }
            else
            {
                CargaComboProducto(int.Parse(ddlTipoFinanciamiento.SelectedValue));
            }
        }
        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSucursal.SelectedValue == "-1")
            {
                Controles.CargarCombo<UsuarioInfo>(ref ddlEjecutivo, null, "Id", "Nombre", "-- Seleccione una Sucursal", "-1");
            }
            else
            {
                CargaComboEjecutivo(int.Parse(ddlSucursal.SelectedValue));
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
        protected void ddlProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProyecto.SelectedValue == "-1")
            {
                ddlComuna.ClearSelection();
            }
            else
            {

                SeleccionarComunaProyecto(int.Parse(ddlProyecto.SelectedValue));
            }
        }

        protected void btnGGOO_Click(object sender, EventArgs e)
        {

            NegGastosOperacionales.objGastoOperacional = new GastoOperacionalInfo();
            NegGastosOperacionales.objGastoOperacional.ValorPropiedad = decimal.Parse(txtPrecioVenta.Text.Replace(".", ","));
            NegGastosOperacionales.objGastoOperacional.MontoCredito = decimal.Parse(txtMontoCredito.Text.Replace(".", ","));
            NegGastosOperacionales.objGastoOperacional.IndDfl2 = chIndDfl2.Checked;
            NegGastosOperacionales.objGastoOperacional.ViviendaSocial = chkViviendaSocial.Checked;
            NegGastosOperacionales.objGastoOperacional.IndSimulacion = true;
            NegGastosOperacionales.objGastoOperacional.Destino_Id = int.Parse(ddlDestino.SelectedValue);

            Controles.AbrirPopup("~/Mantenedores/GastosOperacionales.aspx", "Gastos Operacionales");
        }
        protected void ddlObjetivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlObjetivo.SelectedValue == "-1")
            {
                Controles.CargarCombo<UsuarioInfo>(ref ddlDestino, null, "Id", "Descripcion", "-- Seleccione un Objetivo", "-1");
            }
            else
            {
                CargaComboDestino(int.Parse(ddlObjetivo.SelectedValue));
            }



        }
        protected void btnSimular_Click(object sender, EventArgs e)
        {
            Simular();
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarReportePDF();
        }
        protected void btnGenerarSolicitud_Click(object sender, EventArgs e)
        {
            GenerarSolicitud();
        }
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionaProducto();
        }
        protected void btnModificarCliente_Click(object sender, EventArgs e)
        {
            if (NegClientes.objClienteInfo == null)
            {
                Controles.MostrarMensajeAlerta("Debe Registrar un Cliente en la Simulación para Continuar");
                return;
            }
            if (NegClientes.objClienteInfo.Rut == -1 || NegClientes.objClienteInfo.Rut == 0)
            {
                Controles.MostrarMensajeAlerta("Debe Registrar un Cliente en la Simulación para Continuar");
                return;
            }
            NegClientes.IndNuevoCliente = false;
            Controles.AbrirPopup("~/Mantenedores/Clientes.aspx", "Actualización de Cliente");
        }
        protected void ddlSeguroIncendio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtieneTasaSeguro(ref txtTasaSeguroInc, int.Parse(ddlSeguroIncendio.SelectedValue));
        }
        protected void ddlSeguroDesgravamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtieneTasaSeguro(ref txtTasaSeguroDes, int.Parse(ddlSeguroDesgravamen.SelectedValue));
        }
        protected void ddlSeguroCesantia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtieneTasaSeguro(ref txtTasaSeguroCes, int.Parse(ddlSeguroCesantia.SelectedValue));
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
        protected void ddlSubsidio_SelectedIndexChanged(object sender, EventArgs e)
        {

            CargaComboSegCesantia();
            HabilitaTxtSubsidio();

        }
        protected void btnSiguienteGeneral_Click(object sender, EventArgs e)
        {
            ValidarDatosGeneral();
        }
        protected void btnSiguientgeCredito_Click(object sender, EventArgs e)
        {
            ValidarDatosCredito();
        }
        protected void ddlTipoInmueble_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarTipoInmueble();
        }
        protected void btHistorialSimulaciones_Click(object sender, EventArgs e)
        {
            if (NegClientes.objClienteInfo == null)
            {
                Controles.MostrarMensajeAlerta("Debe Registrar un Cliente en la Simulación para Continuar");
                return;
            }
            if (NegClientes.objClienteInfo.Rut == -1 || NegClientes.objClienteInfo.Rut == 0)
            {
                Controles.MostrarMensajeAlerta("Debe Registrar un Cliente en la Simulación para Continuar");
                return;
            }
            Controles.AbrirPopup("~/Aplicaciones/HistorialSimulaciones.aspx", "Historias de Simulaciones");
        }
        #endregion

        #region Metodos

        ////Metodos de Carga Inicial
        private void BuscarCliente()
        {
            if (txtRutCliente.Text.Length == 0)
            {
                Controles.MostrarMensajeInfo("Debe ingresar un RUT para continuar");
                return;
            }

            ClientesInfo oClientesInfo = new ClientesInfo();
            oClientesInfo.Rut = int.Parse(txtRutCliente.Text.Split('-')[0].ToString());
            oClientesInfo.Dv = txtRutCliente.Text.Split('-')[1].ToString();
            oClientesInfo.RutCompleto = txtRutCliente.Text;
            NegClientes oNegCliente = new NegClientes();
            Resultado<ClientesInfo> oResultado = new Resultado<ClientesInfo>();
            try
            {
                oResultado = oNegCliente.Buscar(oClientesInfo);
                if (oResultado.ResultadoGeneral && oResultado.ValorDecimal != 0)
                {
                    LimpiarFormulario();

                    NegClientes.objClienteInfo = new ClientesInfo();
                    NegClientes.objClienteInfo = oResultado.Lista.FirstOrDefault(c => c.Rut == oClientesInfo.Rut);
                    Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "AvanzaCliente();", true);
                    CargaCliente();

                }
                else
                {
                    NegClientes.IndNuevoCliente = true;
                    NegClientes.objClienteInfo = oClientesInfo;
                    Controles.AbrirPopup("~/Mantenedores/Clientes.aspx", "Ingreso de Cliente");
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
        private void CargaCliente()
        {
            alblRutCompleto.Text = NegClientes.objClienteInfo.RutCompleto.ToString();
            txtRutCliente.Text = NegClientes.objClienteInfo.RutCompleto.ToString();
            alblNombreCompleto.Text = NegClientes.objClienteInfo.NombreCompleto.ToString();
            alblMail.Text = NegClientes.objClienteInfo.Mail.ToString();
            alblCelular.Text = "+56 " + NegClientes.objClienteInfo.TelefonoMovil.ToString();
            alblFono.Text = NegClientes.objClienteInfo.TelefonoFijo.ToString();
            alblFechaNacimiento.Text = NegClientes.objClienteInfo.FechaNacimiento.ToShortDateString();
            if (NegClientes.objClienteInfo.TipoPersona_Id == 2)//Persona Juridica
            {
                ddlSubsidio.ClearSelection();
                ddlSubsidio.Enabled = false;
                CargaComboSegCesantia();
                HabilitaTxtSubsidio();
                ddlSeguroCesantia.SelectedValue = "-1";
                ddlSeguroCesantia.Enabled = false;
            }
            else//Persona Natural
            {
                ddlSubsidio.ClearSelection();
                ddlSubsidio.Enabled = true;
                CargaComboSegCesantia();
                HabilitaTxtSubsidio();
                ddlSeguroCesantia.Enabled = true;
            }

        }
        private void CargaCombos()
        {

            Controles.CargarCombo<UsuarioInfo>(ref ddlEjecutivo, null, "Rut", "Nombre", "--Seleccione una Sucursal--", "-1");
            Controles.CargarCombo<ProyectoInfo>(ref ddlProyecto, null, "Rut", "Descripcion", "--No Aplica--", "-1");
            Controles.CargarCombo<ProductoInfo>(ref ddlProducto, null, "Id", "Descripcion", "-- Seleccione un Producto", "-1");
            CargaComboCooperativa();
            CargaComboInmobiliaria();
            CargaComboComuna();
            CargaComboTipoInmueble();
            CargaComboAntiguedad();
            /*  COMBOS CREDITO   */
            CargaComboTipoFinanciamiento();
            CargaComboObjetivo();
            Controles.CargarCombo<UsuarioInfo>(ref ddlDestino, null, "Id", "Descripcion", "-- Seleccione un Objetivo", "-1");
            CargaComboSubsidio();


            /*  COMBOS SEGURO   */
            CargaComboSegIncendio();
            CargaComboSegDesgravamen();
            CargaComboSegCesantia();


            CargaComboSucursal();
        }
        private void CargaComboSucursal()
        {
            try
            {
                var oSucursalInfo = new SucursalInfo();
                var oNegSucursal = new NegSucursales();
                var oResultado = new Resultado<SucursalInfo>();
                oSucursalInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oResultado = oNegSucursal.Buscar(oSucursalInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SucursalInfo>(ref ddlSucursal, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Sucursal--", "-1");

                    if (oResultado.Lista.FirstOrDefault(s => s.Id == NegUsuarios.Usuario.Sucursal_Id) != null)
                    {
                        ddlSucursal.SelectedValue = NegUsuarios.Usuario.Sucursal_Id.ToString();
                        ddlSucursal.Enabled = false;
                        var lstEjecutivos = CargaComboEjecutivo((int)NegUsuarios.Usuario.Sucursal_Id);
                        if (lstEjecutivos != null)
                        {
                            if (lstEjecutivos.Count(e => e.Rut == NegUsuarios.Usuario.Rut) > 0)
                            {
                                ddlEjecutivo.SelectedValue = NegUsuarios.Usuario.Rut.ToString();
                                ddlEjecutivo.Enabled = false;
                                Session["IndEjecutivoComercial"] = true;
                            }
                        }
                    }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Sucursal");
                }
            }
        }
        private List<UsuarioInfo> CargaComboEjecutivo(int Sucursal_Id)
        {
            try
            {

                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();

                ////Asignacion de Variables
                ObjetoUsuario.Sucursal_Id = Sucursal_Id;
                ObjetoUsuario.Rol_Id = Constantes.IdRolEjecutivoComercial;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref ddlEjecutivo, ObjetoResultado.Lista, "Rut", "Nombre", "--Ejecutivo--", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
                return ObjetoResultado.Lista;
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
                return null;
            }
        }
        private void CargaComboCooperativa()
        {

            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("COOP");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlCooperativa, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- No Aplica --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Cooperativa Sin Datos");
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Cooperativas");
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
        private void CargaComboComuna()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ComunaInfo();
                var ObjetoResultado = new Resultado<ComunaInfo>();
                var NegComuna = new NegComunas();

                ////Asignacion de Variables
                ObjetoResultado = NegComuna.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ComunaInfo>(ref ddlComuna, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Comuna--", "-1");
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
        private void SeleccionarComunaProyecto(int Proyecto_Id)
        {
            try
            {
                ProyectoInfo oProyecto = new ProyectoInfo();
                oProyecto = NegProyectos.lstProyectos.FirstOrDefault(p => p.Id == Proyecto_Id);
                ddlComuna.SelectedValue = oProyecto.Comuna_Id.ToString();

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
                    Controles.CargarCombo<TablaInfo>(ref ddlAntiguedad, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Antigüedad --", "-1");
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
        private void CargaComboSegIncendio()
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
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroIncendio, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Seg Incendio--", "-1");
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
        private void CargaComboSegDesgravamen()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                ////Asignacion de Variables
                ObjInfo.GrupoSeguro_Id = 2;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroDesgravamen, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Seg Desgravamen--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Seguro Desgravamen");
                }
            }
        }
        private void CargaComboSegCesantia()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                ////Asignacion de Variables
                ObjInfo.GrupoSeguro_Id = 3;
                ObjInfo.Subsidio_Id = int.Parse(ddlSubsidio.SelectedValue);
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    txtTasaSeguroCes.Text = "";
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroCesantia, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Sin Seg Cesantia--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Seguro Cesantía");
                }
            }
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
                ddlSeguroCesantia.Enabled = true;
                ddlSeguroDesgravamen.Enabled = true;
                Controles.EjecutarJavaScript("ActualizaMontos('MontoSubsidio');");

            }
            else
            {
                txtMontoSubsidio.Enabled = true;
                txtBonoIntegracion.Enabled = true;
                txtBonoCaptacion.Enabled = true;

                var objSubsidio = new SubsidioInfo();
                objSubsidio = NegSubsidio.lstSubsidios.FirstOrDefault(s => s.Id == int.Parse(ddlSubsidio.SelectedValue));
                ddlSeguroCesantia.SelectedValue = objSubsidio.SeguroCesantia_Id.ToString();
                ObtieneTasaSeguro(ref txtTasaSeguroCes, objSubsidio.SeguroCesantia_Id);
                ddlSeguroCesantia.Enabled = false;
                ddlSeguroDesgravamen.SelectedValue = objSubsidio.SeguroDesgravamen_Id.ToString();
                ObtieneTasaSeguro(ref txtTasaSeguroDes, objSubsidio.SeguroDesgravamen_Id);
                ddlSeguroDesgravamen.Enabled = false;



            }
        }
        private void Simular()
        {
            try
            {
                var TMC = decimal.Zero;
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");




                if (!ValidarDatosCredito()) { return; }
                if (!ValidarDatosGeneral()) { return; }



                var ObjInfo = new SimulacionHipotecariaInfo();
                if (NegSimulacionHipotecaria.oSimulacion != null)
                    ObjInfo = NegSimulacionHipotecaria.oSimulacion;
                ObjInfo.DiaVencimiento = 10;
                ObjInfo.Sucursal_Id = int.Parse(ddlSucursal.SelectedValue);
                ObjInfo.Ejecutivo_Id = int.Parse(ddlEjecutivo.SelectedValue);
                ObjInfo.TipoFinanciamiento_Id = int.Parse(ddlTipoFinanciamiento.SelectedValue);
                ObjInfo.Producto_Id = int.Parse(ddlProducto.SelectedValue);
                ObjInfo.Objetivo_Id = int.Parse(ddlObjetivo.SelectedValue);
                ObjInfo.Destino_Id = int.Parse(ddlDestino.SelectedValue);
                ObjInfo.TipoPropiedad_Id = int.Parse(ddlTipoInmueble.SelectedValue);
                ObjInfo.Subsidio_Id = int.Parse(ddlSubsidio.SelectedValue);
                ObjInfo.PorcentajeFinanciamiento = decimal.Parse(txtPorcentajeFinanciamiento.Text);
                ObjInfo.IndDfl2 = chIndDfl2.Checked;
                ObjInfo.Antiguedad_Id = int.Parse(ddlAntiguedad.SelectedValue);
                ObjInfo.Gracia = int.Parse(ddlMesesGracia.SelectedValue);
                ObjInfo.ValorPropiedad = string.IsNullOrEmpty(txtPrecioVenta.Text) ? 0 : decimal.Parse(txtPrecioVenta.Text.Replace(".", ","));
                ObjInfo.MontoCredito = string.IsNullOrEmpty(txtMontoCredito.Text) ? 0 : decimal.Parse(txtMontoCredito.Text.Replace(".", ","));
                ObjInfo.MontoContado = string.IsNullOrEmpty(txtMontoContado.Text) ? 0 : decimal.Parse(txtMontoContado.Text.Replace(".", ","));
                ObjInfo.MontoSubsidio = string.IsNullOrEmpty(txtMontoSubsidio.Text) ? 0 : decimal.Parse(txtMontoSubsidio.Text.Replace(".", ","));
                ObjInfo.MontoBonoIntegracion = string.IsNullOrEmpty(txtBonoIntegracion.Text) ? 0 : decimal.Parse(txtBonoIntegracion.Text.Replace(".", ","));
                ObjInfo.MontoBonoCaptacion = string.IsNullOrEmpty(txtBonoCaptacion.Text) ? 0 : decimal.Parse(txtBonoCaptacion.Text.Replace(".", ","));
                ObjInfo.Plazo = int.Parse(ddlPlazo.SelectedItem.ToString());
                if (NegClientes.objClienteInfo.TipoPersona_Id == 1)//Persona Natural
                    ObjInfo.CantidadDeudores = (string.IsNullOrEmpty(txtNroCodeudores.Text) ? 0 : int.Parse(txtNroCodeudores.Text)) + 1;
                else//Persona Juridica
                    ObjInfo.CantidadDeudores = (string.IsNullOrEmpty(txtNroCodeudores.Text) ? 0 : int.Parse(txtNroCodeudores.Text));
                ObjInfo.SeguroDesgravamen_Id = int.Parse(ddlSeguroDesgravamen.SelectedValue);
                ObjInfo.SeguroCesantia_Id = int.Parse(ddlSeguroCesantia.SelectedValue);
                ObjInfo.SeguroIncendio_Id = int.Parse(ddlSeguroIncendio.SelectedValue);
                ObjInfo.MontoAseguradoSeguroIncendio = txtMontoAsegurado.Text == "" ? 0 : decimal.Parse(txtMontoAsegurado.Text);
                ObjInfo.Cooperativa_Id = int.Parse(ddlCooperativa.SelectedValue);
                ObjInfo.Inmobiliaria_Id = int.Parse(ddlInmobiliaria.SelectedValue);
                ObjInfo.Proyecto_Id = int.Parse(ddlProyecto.SelectedValue);
                ObjInfo.Comuna_Id = int.Parse(ddlComuna.SelectedValue);

                TMC = NegParidad.ObtenerTMC(CodigoMoneda, DateTime.Now, ObjInfo.Plazo, ObjInfo.MontoCredito);
                if (!chkIndTasaEspecial.Checked)
                {

                    if (ddlSubsidio.SelectedValue != "-1")
                    {

                        ObjInfo.TasaAnual = TMC - NegConfiguracionGeneral.Obtener().DescuentoTMC;
                    }
                    else
                    {
                        ObjInfo.TasaAnual = 0;
                    }
                }
                else
                {
                    if (txtTasaEspecial.Text == "")
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar una Tasa para Simular");
                        return;
                    }
                    if (decimal.Parse(txtTasaEspecial.Text) > TMC)
                    {
                        Controles.MostrarMensajeAlerta("La tasa Ingresada no puede ser mayor a la Tasa Máxima Convencional (" + String.Format("{0:0.0000}", TMC) + " %)");
                        return;
                    }
                    if (decimal.Parse(txtTasaEspecial.Text) <= 0)
                    {
                        Controles.MostrarMensajeAlerta("La tasa ingresada debe ser mayor a 0'");
                        return;
                    }
                    ObjInfo.TasaAnual = decimal.Parse(txtTasaEspecial.Text);
                }
                var objResultado = new Resultado<SimulacionHipotecariaInfo>();
                var objNegocio = new NegSimulacionHipotecaria();
                var objProducto = NegProductos.lstProductos.FirstOrDefault(p => p.Id == ObjInfo.Producto_Id);



                objResultado = objNegocio.RealizarSimulacion(ObjInfo);
                if (objResultado.ResultadoGeneral)
                {
                    NegSimulacionHipotecaria.oReporteSimulacion = new ReporteSimulacion();
                    NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion = new SimulacionHipotecariaInfo();
                    NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion = ObjInfo;

                    //Simulacion Minvu
                    //if (ddlSubsidio.SelectedValue != "-1")
                    //{
                    //    Controles.EjecutarJavaScript("MostrarSimulacionMinvu('true');");
                    //    Controles.CargarGrid(ref gvSimulacion, objResultado.Lista, new string[] { "Plazo" });
                    //    Controles.CargarGrid(ref gvSimulacionMinvu, objResultado.Lista, new string[] { "Plazo" });
                    //}
                    //else
                    //{
                    //    Controles.CargarGrid(ref gvSimulacion, objResultado.Lista, new string[] { "Plazo" });
                    //}




                    if (objProducto.IndDoblePlazo == true)
                    {

                        List<SimulacionHipotecariaInfo> lstSimulacionRealizada = new List<SimulacionHipotecariaInfo>();
                        List<SimulacionHipotecariaInfo> lstSimulacionFlexible = new List<SimulacionHipotecariaInfo>();

                        lstSimulacionRealizada = NegSimulacionHipotecaria.lstSimulacion;

                        ObjInfo.Plazo = int.Parse(ddlPlazoFlexible.SelectedValue);
                        ObjInfo.PlazoUnico = true;
                        objResultado = objNegocio.RealizarSimulacion(ObjInfo);
                        if (objResultado.ResultadoGeneral)
                        {
                            lstSimulacionFlexible = NegSimulacionHipotecaria.lstSimulacion;
                            foreach (var simu in lstSimulacionRealizada.Where(s => s.Plazo == int.Parse(ddlPlazo.SelectedValue)))
                            {
                                simu.DividendoFlexibleTotal = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoTotal;
                                simu.DividendoFlexibleTotalPesos = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoPesos;
                                simu.DividendoFlexibleUF = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoTotal;
                                simu.DividendoFlexiblePesos = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoPesos;
                                simu.PlazoFlexible = ObjInfo.Plazo;
                            }

                            Controles.CargarGrid(ref gvSimulacionFlexible, lstSimulacionFlexible, new string[] { "Plazo" });
                            Controles.CargarGrid(ref gvSimulacion, lstSimulacionRealizada, new string[] { "Plazo" });
                            Controles.EjecutarJavaScript("MostrarSimulacionFlexible('true');");
                        }

                        NegSimulacionHipotecaria.lstSimulacion = lstSimulacionRealizada;

                    }
                    else
                    {
                        Controles.CargarGrid(ref gvSimulacion, objResultado.Lista, new string[] { "Plazo" });
                        Controles.EjecutarJavaScript("MostrarSimulacionFlexible('false');");
                    }



                    foreach (GridViewRow row in gvSimulacion.Rows)
                    {
                        if (int.Parse(gvSimulacion.DataKeys[row.RowIndex].Values["Plazo"].ToString()) == int.Parse(ddlPlazo.SelectedValue))
                            row.Font.Bold = true;
                    }



                    ObjInfo.Plazo = int.Parse(ddlPlazo.SelectedValue);
                    ObjInfo.PlazoFlexible = int.Parse(ddlPlazoFlexible.SelectedValue);
                    objResultado = new Resultado<SimulacionHipotecariaInfo>();
                    ObjInfo.RutCliente = NegClientes.objClienteInfo.Rut;
                    ObjInfo.DividendoUF = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoUF;
                    ObjInfo.DividendoPesos = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoPesos;
                    ObjInfo.DividendoTotal = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoTotal;
                    ObjInfo.DividendoTotalPesos = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoTotalPesos;
                    ObjInfo.CAE = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).CAE;
                    ObjInfo.CAEMinvu = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).CAEMinvu;
                    ObjInfo.CTC = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).CTC;
                    ObjInfo.CTCMinvu = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).CTCMinvu;
                    ObjInfo.SegDesgravamen = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).SegDesgravamen;
                    ObjInfo.SegIncendio = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).SegIncendio;
                    ObjInfo.SegCesantia = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).SegCesantia;
                    ObjInfo.SegCesantiaMinvu = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).SegCesantiaMinvu;
                    ObjInfo.TasaAnual = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).TasaAnual;
                    ObjInfo.IndTasaEspecial = chkIndTasaEspecial.Checked;
                    ObjInfo.DividendoFlexibleTotal = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoFlexibleTotal;
                    ObjInfo.DividendoFlexibleTotalPesos = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoFlexibleTotalPesos;
                    ObjInfo.DividendoFlexibleUF = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoFlexibleUF;
                    ObjInfo.DividendoFlexiblePesos = NegSimulacionHipotecaria.lstSimulacion.FirstOrDefault(s => s.Plazo == ObjInfo.Plazo).DividendoFlexiblePesos;
                    //REGISTRA SIMULACION REALIDAZA
                    objResultado = objNegocio.Guardar(ref ObjInfo);
                    if (!objResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(objResultado.Mensaje);
                        return;
                    }
                    NegSimulacionHipotecaria.oSimulacion = ObjInfo;


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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Simular");
                }
            }
        }

        private bool ValidarDatosGeneral()
        {
            try
            {
                if (ddlSucursal.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionGeneral();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Sucursal");
                    return false;
                }
                if (ddlEjecutivo.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionGeneral();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Ejecutivo");
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
                    Controles.MostrarMensajeError("Error al Validar Datos Generales");
                }
                return false;
            }
        }
        private bool ValidarDatosCredito()
        {
            try
            {

                NegConfiguracionHipotecaria nConfigHipo = new NegConfiguracionHipotecaria();
                ConfiguracionHipotecariaInfo oConfigHipo = new ConfiguracionHipotecariaInfo();
                Resultado<ConfiguracionHipotecariaInfo> rConfigHipo = new Resultado<ConfiguracionHipotecariaInfo>();





                if (ddlTipoFinanciamiento.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Financiamiento");
                    return false;
                }
                if (ddlProducto.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Producto");


                    return false;
                }
                if (ddlObjetivo.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Objetivo");

                    return false;
                }

                if (ddlDestino.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Destino");

                    return false;
                }

                if (txtPrecioVenta.Text == "")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Precio de Venta");

                    return false;
                }

                if (txtMontoCredito.Text == "")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto del Crédito");

                    return false;
                }
                if (ddlPlazo.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Plazo");

                    return false;
                }
                if (ddlPlazoFlexible.SelectedValue == "-1" && ddlPlazoFlexible.Enabled)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Plazo Secundario");

                    return false;
                }
                if (ddlTipoInmueble.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Inmueble");

                    return false;
                }
                if (ddlAntiguedad.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Antigüedad");

                    return false;
                }
                if (ddlSeguroIncendio.Enabled == true && ddlSeguroIncendio.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Seguro de Incendio");

                    return false;
                }
                if (ddlSubsidio.SelectedValue != "-1" && ddlSeguroCesantia.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Seguro de Cesantía");

                    return false;
                }
                if (NegClientes.objClienteInfo.Rut < 50000000 && ddlSeguroDesgravamen.SelectedValue == "-1")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Seguro de Desgravamen");
                    return false;
                }


                oConfigHipo.Producto_Id = int.Parse(ddlProducto.SelectedValue);
                oConfigHipo.Destino_Id = int.Parse(ddlDestino.SelectedValue);
                oConfigHipo.Subsidio_Id = int.Parse(ddlSubsidio.SelectedValue);
                oConfigHipo.AntiguedadVivienda_Id = int.Parse(ddlAntiguedad.SelectedValue);

                rConfigHipo = nConfigHipo.Buscar(oConfigHipo);
                if (rConfigHipo.ResultadoGeneral)
                {
                    if (rConfigHipo.Lista.Count() > 0)
                        oConfigHipo = rConfigHipo.Lista.FirstOrDefault();
                    else
                    {
                        Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                        Controles.MostrarMensajeError("Configuración Hipotecaria No Encontrada para esta combinación de financiamiento, Favor Informar al Administrador.");
                        return false;
                    }
                }
                else
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeError(rConfigHipo.Mensaje);
                    return false;
                }


                if (decimal.Parse(txtPorcentajeFinanciamiento.Text.Replace(".", ",")) > oConfigHipo.PorcentajeFinanciamientoMaximo)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("El % de Finanmiento Máximo Permitido es de " + oConfigHipo.PorcentajeFinanciamientoMaximo.ToString() + "%");
                    return false;
                }

                if (decimal.Parse(txtPorcentajeFinanciamiento.Text.Replace(".", ",")) < oConfigHipo.PorcentajeFinanciamientoMinimo)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("El % de Finanmiento Mínimo Permitido es de " + oConfigHipo.PorcentajeFinanciamientoMinimo.ToString() + "%");
                    return false;
                }

                if (decimal.Parse(ddlPlazo.SelectedValue) > oConfigHipo.PlazoMaximo)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("El Plazo Máximo Permitido es de " + oConfigHipo.PlazoMaximo.ToString() + " Años");
                    return false;
                }

                if (decimal.Parse(ddlPlazo.SelectedValue) < oConfigHipo.PlazoMinimo)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("El Plazo Mínimo Permitido es de " + oConfigHipo.PlazoMinimo.ToString() + " Años");
                    return false;
                }

                if (decimal.Parse(ddlPlazoFlexible.SelectedValue) > oConfigHipo.PlazoMaximo && ddlPlazoFlexible.Enabled)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("El Plazo Máximo| Permitido es de " + oConfigHipo.PlazoMaximo.ToString() + " Años");
                    return false;
                }

                if (decimal.Parse(ddlPlazoFlexible.SelectedValue) < oConfigHipo.PlazoMinimo && ddlPlazoFlexible.Enabled)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("El Plazo Mínimo Flexible Permitido es de " + oConfigHipo.PlazoMinimo.ToString() + " Años");
                    return false;
                }

                if (decimal.Parse(txtMontoCredito.Text.Replace(".", ",")) > oConfigHipo.MontoCreditoMaximo)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("El Monto Máximo de Crédito Permitido es de " + oConfigHipo.MontoCreditoMaximo.ToString() + " UF");
                    return false;
                }

                if (decimal.Parse(txtMontoCredito.Text.Replace(".", ",")) < oConfigHipo.MontoCreditoMinimo)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("El Monto Mínimo de Crédito Permitido es de " + oConfigHipo.MontoCreditoMinimo.ToString() + " UF");
                    return false;
                }

                if (decimal.Parse(txtPorcentajeFinanciamiento.Text.Replace(".", ",")) > oConfigHipo.PorcentajeFinanciamientoMaximo && ddlMesesGracia.SelectedValue != "0")
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Para % de financiamiento mayores a " + oConfigHipo.PorcentajeFinanciamientoMaximo.ToString() + "% no se permiten meses de Gracia");
                    return false;
                }
                int EdadPlazo = NegClientes.objClienteInfo.Edad + int.Parse(ddlPlazo.SelectedValue);
                if (EdadPlazo > 80 && NegClientes.objClienteInfo.TipoPersona_Id == 1)
                {
                    Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                    Controles.MostrarMensajeAlerta("Edad Cliente + Plazo no debe superar los 80 años.");
                    return false;
                }

                var TMC = decimal.Zero;
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                TMC = NegParidad.ObtenerTMC(CodigoMoneda, DateTime.Now, int.Parse(ddlPlazo.SelectedValue), decimal.Parse(txtMontoCredito.Text.Replace(".", ",")));


                if (chkIndTasaEspecial.Checked)
                {
                    if (txtTasaEspecial.Text == "")
                    {
                        Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                        Controles.MostrarMensajeAlerta("Debe Ingresar una Tasa para Simular");
                        return false;
                    }
                    if (decimal.Parse(txtTasaEspecial.Text) > TMC)
                    {
                        Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                        Controles.MostrarMensajeAlerta("La tasa Ingresada no puede ser mayor a la Tasa Máxima Convencional (" + String.Format("{0:0.0000}", TMC) + " %)");
                        return false;
                    }
                    if (decimal.Parse(txtTasaEspecial.Text) <= 0)
                    {
                        Controles.EjecutarJavaScript("MostrarInformacionCredito();");
                        Controles.MostrarMensajeAlerta("La tasa ingresada debe ser mayor a 0'");
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
        private void SeleccionarTipoInmueble()
        {
            try
            {
                if (ddlTipoInmueble.SelectedValue != "-1")
                {
                    var Id = int.Parse(ddlTipoInmueble.SelectedValue);
                    var oTipoInmueble = new TipoInmuebleInfo();
                    var PrecioVenta = decimal.Zero;
                    ddlSeguroIncendio.Enabled = true;
                    oTipoInmueble = NegPropiedades.lstTipoInmuebles.FirstOrDefault(ti => ti.Id == Id);
                    if (decimal.TryParse(txtPrecioVenta.Text.Replace(".", ","), out PrecioVenta))
                    {
                        txtMontoAsegurado.Text = ((PrecioVenta * oTipoInmueble.PorcentajeSeguroIncendio) / 100).ToString();

                        if (oTipoInmueble.PorcentajeSeguroIncendio == 0)
                        {
                            ddlSeguroIncendio.ClearSelection();
                            txtTasaSeguroInc.Text = "0";
                            txtMontoAsegurado.Text = "0";
                            ddlSeguroIncendio.Enabled = false;
                        }
                    }
                    else
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar un Precio de Venta");
                        ddlTipoInmueble.ClearSelection();
                        ddlSeguroIncendio.ClearSelection();
                        txtTasaSeguroInc.Text = "0";
                        txtMontoAsegurado.Text = "0";
                        ddlSeguroIncendio.Enabled = false;
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
                    Controles.MostrarMensajeError("Error al Seleccionar Tipo de Inmueble");
                }
            }
        }
        private void ObtieneTasaSeguro(ref Anthem.TextBox objControl, int Seguro_Id)
        {
            try
            {
                if (Seguro_Id != -1)
                {
                    ////Declaracion de Variables
                    var ObjInfo = new SeguroInfo();
                    var ObjResultado = new Resultado<SeguroInfo>();
                    var objNegocio = new NegSeguros();

                    ////Asignacion de Variables
                    ObjInfo.Id = Seguro_Id;
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        objControl.Text = (ObjResultado.Lista[0].TasaMensual * 100).ToString();
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjResultado.Mensaje);
                    }
                }
                else
                {
                    objControl.Text = "0.00";
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Seguro");
                }
            }
        }

        private void LimpiarFormulario()
        {
            txtRutCliente.Text = "";
            NegClientes.objClienteInfo = new ClientesInfo();
            NegClientes.IndNuevoCliente = false;
            NegSimulacionHipotecaria.lstSimulacion = null;
            NegSimulacionHipotecaria.indSeleccionSimulacionCliente = null;
            NegSimulacionHipotecaria.oReporteSimulacion = null;
            NegSimulacionHipotecaria.oSimulacion = null;
            NegGastosOperacionales.lstGastosOperacionales = null;
            Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Text = "");
            Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.ClearSelection());
            Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Checked = false);
            if (ddlSucursal.Enabled == false)
            {
                CargaComboSucursal();
            }
            btnGenerarSolicitud.Visible = true;
            int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
            ((Anthem.TextBox)Master.FindControl("txtValorUF")).Text = string.Format("{0:0,0.00}", NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now)).Replace(".", "");


        }
        private void CargaParrafoPDF()
        {

            try
            {
                ////Declaracion de Variables
                var ObjInfo = new TextoReportesInfo();
                var ObjetoResultado = new Resultado<TextoReportesInfo>();
                var objNegocio = new NegTextoReportes();

                ////Asignacion de Variables

                ObjInfo.NombreReporte = "SIMULACIONCREDITO";
                ObjetoResultado = objNegocio.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    NegSimulacionHipotecaria.oReporteSimulacion.lstParrafos = ObjetoResultado.Lista;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Parrafos PDF");
                }
            }

        }
        private void GenerarReportePDF(bool desplegar = true)
        {
            try
            {
                if (NegSimulacionHipotecaria.oReporteSimulacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Realizar la Simulación para Generar el Reporte");
                    return;
                }

                if (NegSimulacionHipotecaria.lstSimulacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Realizar la Simulación para Generar el Reporte");
                    return;
                }
                if (NegGastosOperacionales.lstGastosOperacionales == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Consultar los  Gastos Operacionales para Generar el Reporte");
                    return;
                }
                string urlDocumento = "~/Reportes/PDF/SimulacionCreditoHipotecario.aspx";
                string ContenidoHtml = "";
                string Archivo = "Simulación Hipotecaria de " + NegClientes.objClienteInfo.NombreCompleto + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Reporte de Simulación";
                NegSimulacionHipotecaria.oReporteSimulacion.oCliente = new ClientesInfo();
                NegSimulacionHipotecaria.oReporteSimulacion.oCliente = NegClientes.objClienteInfo;
                NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion = NegSimulacionHipotecaria.lstSimulacion;
                NegSimulacionHipotecaria.oReporteSimulacion.lstGastoOperacional = NegGastosOperacionales.lstGastosOperacionales;
                NegSimulacionHipotecaria.oReporteSimulacion.oEjecutivo = NegUsuarios.Usuario;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionTipoInmueble = ddlTipoInmueble.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionComuna = ddlComuna.SelectedValue == "-1" ? "------" : ddlComuna.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionCooperativa = ddlCooperativa.SelectedValue == "-1" ? "------" : ddlCooperativa.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionInmobiliaria = ddlInmobiliaria.SelectedValue == "-1" ? "------" : ddlInmobiliaria.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionProyecto = ddlProyecto.SelectedValue == "-1" ? "------" : ddlProyecto.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionTipoFinanciamiento = ddlTipoFinanciamiento.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionProducto = ddlProducto.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionObjetivo = ddlObjetivo.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionDestino = ddlDestino.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionSubsidio = ddlSubsidio.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionSegDesgravamen = ddlSeguroDesgravamen.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionSegIncendio = ddlSeguroIncendio.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionSegCesantia = ddlSeguroCesantia.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrPrecioVentaPesos = txtPrecioVentaPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoSubsidioPesos = txtMontoSubsidioPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoBonoIntegracionPesos = txtBonoIntegracionPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoBonoCaptacionPesos = txtBonoCaptacionPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoContadoPesos = txtMontoContadoPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoCreditoPesos = txtMontoCreditoPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrValorUF = "Valor UF $ " + ((Anthem.Label)Master.FindControl("lblValorUF")).Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionGracia = ddlMesesGracia.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrPorcentajeFinanciamiento = txtPorcentajeFinanciamiento.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.TasaAnual = decimal.Parse(txtTasaEspecial.Text == "" ? "0" : txtTasaEspecial.Text);

                CargaParrafoPDF();
                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);

                Session["ArchivoSimulacion"] = Binarios;
                if (desplegar)
                {
                    Response.AddHeader("Content-Type", "application/pdf");
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + Archivo + "; size={0}", Binarios.Length.ToString()));
                    Response.BinaryWrite(Binarios);
                    Response.Flush();
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
                    Controles.MostrarMensajeError("Error al Cargar el Reporte PDF");
                }
            }
        }

        private void GenerarSolicitud()
        {
            try
            {

                if (NegSimulacionHipotecaria.oReporteSimulacion == null || NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Generar el Reporte de Simulación para antes de crear la Solicitud");
                    return;
                }


                SolicitudInfo oSolicitud = new SolicitudInfo();
                NegSolicitudes negSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> oResultadoSolicitud = new Resultado<SolicitudInfo>();
                SimulacionHipotecariaInfo objReporteSimulacion = new SimulacionHipotecariaInfo();

                oSolicitud.Estado_Id = (int)NegTablas.IdentificadorMaestro("EST_SOLICITUD", "EC");
                oSolicitud.SubEstado_Id = (int)NegTablas.IdentificadorMaestro("SUBEST_SOLICITUD", "ING");
                oSolicitud.FechaIngreso = DateTime.Now;
                oSolicitud.Producto_Id = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.Producto_Id;
                oSolicitud.Destino_Id = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.Destino_Id;
                oSolicitud.MontoPropiedad = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.ValorPropiedad;
                oSolicitud.MontoCredito = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.MontoCredito;
                oSolicitud.MontoContado = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.MontoContado;
                oSolicitud.MontoSubsidio = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.MontoSubsidio;
                oSolicitud.MontoBonoIntegracion = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.MontoBonoIntegracion;
                oSolicitud.MontoBonoCaptacion = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.MontoBonoCaptacion;
                oSolicitud.PorcentajeFinanciamiento = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.PorcentajeFinanciamiento;
                oSolicitud.Subsidio_Id = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.Subsidio_Id;
                oSolicitud.Plazo = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.Plazo;
                objReporteSimulacion = NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion.FirstOrDefault(s => s.Plazo == oSolicitud.Plazo);
                oSolicitud.TasaBase = objReporteSimulacion.TasaBaseAnual;
                oSolicitud.Spread = objReporteSimulacion.TasaSpreadAnual;
                oSolicitud.TasaFinal = objReporteSimulacion.TasaAnual;
                oSolicitud.IndTasaEspecial = chkIndTasaEspecial.Checked;
                oSolicitud.Gracia = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.Gracia;
                oSolicitud.EjecutivoComercial_Id = int.Parse(ddlEjecutivo.SelectedValue);
                oSolicitud.CAE = objReporteSimulacion.CAE;
                oSolicitud.CTC = objReporteSimulacion.CTC;
                oSolicitud.ValorDividendoUF = objReporteSimulacion.DividendoUF;
                oSolicitud.ValorUltimoDividendoUF = objReporteSimulacion.UltimoDividendoUF;
                oSolicitud.ValorDividendoSinCesantiaUF = objReporteSimulacion.DividendoUF + objReporteSimulacion.SegDesgravamen + objReporteSimulacion.SegIncendio;
                oSolicitud.ValorDividendoPesos = objReporteSimulacion.DividendoPesos;
                oSolicitud.ValorDividendoUfTotal = objReporteSimulacion.DividendoTotal;
                oSolicitud.ValorDividendoPesosTotal = objReporteSimulacion.DividendoTotalPesos;


                oSolicitud.ValorDividendoFlexibleUF = objReporteSimulacion.DividendoFlexibleUF;
                oSolicitud.ValorDividendoFlexiblePesos = objReporteSimulacion.DividendoFlexiblePesos;
                oSolicitud.ValorDividendoFlexibleUfTotal = objReporteSimulacion.DividendoFlexibleTotal;
                oSolicitud.ValorDividendoFlexiblePesosTotal = objReporteSimulacion.DividendoFlexibleTotalPesos;

                oSolicitud.IndDfl2 = chIndDfl2.Checked;
                oSolicitud.IndViviendaSocial = chkViviendaSocial.Checked;
                oSolicitud.Cooperativa_Id = int.Parse(ddlCooperativa.SelectedValue);
                oSolicitud.Inmobiliaria_Id = int.Parse(ddlInmobiliaria.SelectedValue);
                oSolicitud.Proyecto_Id = int.Parse(ddlProyecto.SelectedValue);
                oSolicitud.Sucursal_Id = int.Parse(ddlSucursal.SelectedValue);
                oSolicitud.Plazo2 = int.Parse(ddlPlazoFlexible.SelectedValue);
                oResultadoSolicitud = negSolicitud.Guardar(ref oSolicitud);
                if (oResultadoSolicitud.ResultadoGeneral)
                {
                    if (!IngresarParticipante(oSolicitud)) { return; }
                    if (!IniciarFlujo(oSolicitud)) { return; }
                    if (!GrabarPropiedad(oSolicitud)) { return; }

                    NegSimulacionHipotecaria.oSimulacion.Solicitud_Id = oSolicitud.Id;

                    Resultado<SimulacionHipotecariaInfo> rSimulacion = new Resultado<SimulacionHipotecariaInfo>();
                    NegSimulacionHipotecaria nSimulacion = new NegSimulacionHipotecaria();

                    rSimulacion = nSimulacion.Guardar(NegSimulacionHipotecaria.oSimulacion);
                    if (!rSimulacion.ResultadoGeneral)
                    {
                        NegSimulacionHipotecaria.indSeleccionSimulacionCliente = false;
                        NegSimulacionHipotecaria.oSimulacion = null;
                        Controles.MostrarMensajeError(rSimulacion.Mensaje);
                        return;
                    }
                    
                    btnGenerarSolicitud.Visible = false;
                    GenerarReportePDF(false);
                    string resultado = NegArchivosRepositorios.GuardarDocumentoSolicitud(oSolicitud.Id, 1, 85, "Simulación Hipotecaria de " + NegClientes.objClienteInfo.NombreCompleto + ".pdf", (byte[])Session["ArchivoSimulacion"]);
                    if (resultado != "")
                    {
                        Controles.MostrarMensajeAlerta(resultado);
                        return;
                    }
                    Controles.MostrarMensajeExito("Se ha generado la Solicitud N° " + oSolicitud.Id.ToString() + " Favor Revisar su Bandeja de Entrada");
                }
                else
                {
                    Controles.MostrarMensajeError(oResultadoSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Generar la Solicitud");
                }
            }
        }
        private bool IniciarFlujo(SolicitudInfo oSolicitud)
        {
            try
            {
                NegFlujo oNegFlujo = new NegFlujo();
                Resultado<SolicitudInfo> oResultado = new Resultado<SolicitudInfo>();

                oResultado = oNegFlujo.IniciarFlujo(oSolicitud);

                if (oResultado.ResultadoGeneral)
                {
                    return true;
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                    return false;
                }


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                    return false;
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Iniciar el Flujo de la Solicitud");
                    return false;
                }
            }

        }
        private bool IngresarParticipante(SolicitudInfo oSolicitud)
        {
            try
            {
                NegParticipante oNeg = new NegParticipante();
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                Resultado<ParticipanteInfo> oResultado = new Resultado<ParticipanteInfo>();

                oParticipante.Solicitud_Id = oSolicitud.Id;
                oParticipante.Rut = NegClientes.objClienteInfo.Rut;
                oParticipante.TipoParticipacion_Id = Constantes.IdSolicitantePrincipal;
                if (NegClientes.objClienteInfo.TipoPersona_Id == 1)//Persona Natural
                {
                    oParticipante.PorcentajeDesgravamen = 100;
                    oParticipante.PorcentajeDeuda = 100;
                    oParticipante.PorcentajeDominio = 100;
                    oParticipante.SeguroDesgravamen_Id = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.SeguroDesgravamen_Id;
                    oParticipante.TasaSeguroDesgravamen = txtTasaSeguroDes.Text == "" ? 0 : decimal.Parse(txtTasaSeguroDes.Text) / 100;
                    oParticipante.PrimaSeguroDesgravamen = (decimal)NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion.FirstOrDefault(s => s.Plazo == oSolicitud.Plazo).SegDesgravamen / (decimal)NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.CantidadDeudores;
                    oParticipante.SeguroCesantia_Id = NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.SeguroCesantia_Id;
                    oParticipante.TasaSeguroCesantia = txtTasaSeguroCes.Text == "" ? 0 : decimal.Parse(txtTasaSeguroCes.Text) / 100;
                    oParticipante.PrimaSeguroCesantia = NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion.FirstOrDefault(s => s.Plazo == oSolicitud.Plazo).SegCesantia;
                }
                else//Persona Juridica
                {
                    oParticipante.PorcentajeDesgravamen = 0;
                    oParticipante.PorcentajeDeuda = 100;
                    oParticipante.PorcentajeDominio = 100;
                    oParticipante.SeguroDesgravamen_Id = -1;
                    oParticipante.TasaSeguroDesgravamen = 0;
                    oParticipante.PrimaSeguroDesgravamen = 0;
                    oParticipante.SeguroCesantia_Id = -1;
                    oParticipante.TasaSeguroCesantia = 0;
                    oParticipante.PrimaSeguroCesantia = 0;
                }
                oResultado = oNeg.GuardarParticipante(ref oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    if (!ActualizarSegurosContratados(oParticipante)) return false;
                    return true;
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                    return false;
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                    return false;
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Agregar el Participante de la Solicitud");
                    return false;
                }
            }
        }
        private bool GrabarPropiedad(SolicitudInfo oSolicitud)
        {
            try
            {
                SimulacionHipotecariaInfo objReporteSimulacion = new SimulacionHipotecariaInfo();
                objReporteSimulacion = NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion.FirstOrDefault(s => s.Plazo == oSolicitud.Plazo);

                NegPropiedades nPropiedad = new NegPropiedades();
                PropiedadInfo oPropiedad = new PropiedadInfo();
                Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();

                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oPropiedad.TipoInmueble_Id = int.Parse(ddlTipoInmueble.SelectedValue);

                oPropiedad.Antiguedad_Id = int.Parse(ddlAntiguedad.SelectedValue);
                oPropiedad.Destino_Id = -1;
                oPropiedad.Region_Id = -1;
                oPropiedad.Provincia_Id = -1;
                oPropiedad.Comuna_Id = -1;
                oPropiedad.Via_Id = -1;
                oPropiedad.TipoConstruccion_Id = -1;

                rPropiedad = nPropiedad.GuardarPropiedad(ref oPropiedad);
                if (rPropiedad.ResultadoGeneral)
                {

                    oTasacion.EstadoTasacion_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS_FLUJOS_PARALELOS", "I");
                    oTasacion.EstadoEstudioTitulo_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS_FLUJOS_PARALELOS", "I");
                    oTasacion.Propiedad_Id = oPropiedad.Id;
                    oTasacion.TasacionPadre_Id = -1;
                    oTasacion.IndPropiedadPrincipal = true;
                    oTasacion.Solicitud_Id = oSolicitud.Id;
                    oTasacion.Seguro_Id = int.Parse(ddlSeguroIncendio.SelectedValue);
                    oTasacion.MontoAsegurado = txtMontoAsegurado.Text == "" ? 0 : decimal.Parse(txtMontoAsegurado.Text);
                    oTasacion.TasaSeguro = txtTasaSeguroInc.Text == "" ? 0 : decimal.Parse(txtTasaSeguroInc.Text) / 100;
                    oTasacion.PrimaSeguro = objReporteSimulacion.SegIncendio;

                    rTasacion = nTasacion.GuardarTasacion(ref oTasacion);
                    if (rTasacion.ResultadoGeneral)
                    {

                        ActualizarSegurosContratados(oTasacion);
                        return true;
                    }
                    else
                    {
                        Controles.MostrarMensajeError(rTasacion.Mensaje);
                        return false;
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rPropiedad.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar la Propiedade");
                }
                return false;
            }
        }
        private bool ActualizarSegurosContratados(ParticipanteInfo oParticipante)
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSegurosContratados = new SegurosContratadosInfo();
                //Actualizacion del Seguro de Desgravamen

                if (oParticipante.PorcentajeDesgravamen > 0)
                {
                    oSegurosContratados = new SegurosContratadosInfo();
                    oSegurosContratados.TipoSeguro_Id = 2;//Desgravamen
                    oSegurosContratados.Solicitud_Id = oParticipante.Solicitud_Id;
                    oSegurosContratados.RutCliente = oParticipante.Rut;
                    oSegurosContratados.Seguro_Id = oParticipante.SeguroDesgravamen_Id;
                    oSegurosContratados.MontoAsegurado = NegSolicitudes.objSolicitudInfo.MontoCredito * oParticipante.PorcentajeDesgravamen / 100;
                    oSegurosContratados.TasaMensual = oParticipante.TasaSeguroDesgravamen;
                    oSegurosContratados.PrimaMensual = oParticipante.PrimaSeguroDesgravamen;

                    rSeguros = nSeguros.GuardarSegurosContratados(oSegurosContratados);
                    if (rSeguros.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(rSeguros.Mensaje);
                        return false;
                    }
                }

                //Actualizacion del Seguro de Cesantía

                if (oParticipante.SeguroCesantia_Id != -1)
                {
                    var oSeguro = new SeguroInfo();
                    var oResultadoSeguro = new Resultado<SeguroInfo>();
                    oSeguro.Id = int.Parse(ddlSeguroCesantia.SelectedValue);
                    oResultadoSeguro = nSeguros.Buscar(oSeguro);
                    if (oResultadoSeguro.ResultadoGeneral)
                    {
                        oSeguro = oResultadoSeguro.Lista.FirstOrDefault(s => s.Id == oSeguro.Id);
                        decimal MontoAsegurado = decimal.Zero;
                        if (oSeguro.PorValorCuota)
                            MontoAsegurado = NegSolicitudes.objSolicitudInfo.ValorDividendoSinCesantiaUF;
                        else
                            MontoAsegurado = NegSolicitudes.objSolicitudInfo.MontoCredito;


                        oSegurosContratados = new SegurosContratadosInfo();
                        oSegurosContratados.TipoSeguro_Id = 3;//Cesantia
                        oSegurosContratados.Solicitud_Id = oParticipante.Solicitud_Id;
                        oSegurosContratados.RutCliente = oParticipante.Rut;
                        oSegurosContratados.Seguro_Id = oParticipante.SeguroCesantia_Id;
                        oSegurosContratados.MontoAsegurado = MontoAsegurado;
                        oSegurosContratados.TasaMensual = oParticipante.TasaSeguroCesantia;
                        oSegurosContratados.PrimaMensual = oParticipante.PrimaSeguroCesantia;

                    }
                    else
                    {
                        Controles.MostrarMensajeError(oResultadoSeguro.Mensaje);
                        return false;
                    }
                }
                else
                {
                    oSegurosContratados = new SegurosContratadosInfo();
                    oSegurosContratados.TipoSeguro_Id = 3;//Cesantia
                    oSegurosContratados.Solicitud_Id = oParticipante.Solicitud_Id;
                    oSegurosContratados.RutCliente = oParticipante.Rut;
                    oSegurosContratados.Seguro_Id = oParticipante.SeguroCesantia_Id;
                }
                rSeguros = nSeguros.GuardarSegurosContratados(oSegurosContratados);
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

        private void CargarSimulacion()
        {
            try
            {
                //CargaCombos();
                SimulacionHipotecariaInfo oSimulacion = new SimulacionHipotecariaInfo();
                oSimulacion = NegSimulacionHipotecaria.oSimulacion;

                if (ddlSubsidio.Enabled)

                    ddlCooperativa.SelectedValue = oSimulacion.Cooperativa_Id.ToString();
                ddlInmobiliaria.SelectedValue = oSimulacion.Inmobiliaria_Id.ToString();
                CargaComboProyecto(oSimulacion.Inmobiliaria_Id);
                ddlProyecto.SelectedValue = oSimulacion.Proyecto_Id.ToString();
                ddlComuna.SelectedValue = oSimulacion.Comuna_Id.ToString();
                ddlTipoFinanciamiento.SelectedValue = oSimulacion.TipoFinanciamiento_Id.ToString();
                CargaComboProducto(oSimulacion.TipoFinanciamiento_Id);
                ddlProducto.SelectedValue = oSimulacion.Producto_Id.ToString();
                CargaComboGracia(oSimulacion.Producto_Id);
                CargaComboPlazos(oSimulacion.Producto_Id);
                ddlMesesGracia.SelectedValue = oSimulacion.Gracia.ToString();
                ddlObjetivo.SelectedValue = oSimulacion.Objetivo_Id.ToString();
                CargaComboDestino(oSimulacion.Objetivo_Id);
                ddlDestino.SelectedValue = oSimulacion.Destino_Id.ToString();
                txtPrecioVenta.Text = String.Format("{0:0.0000}", oSimulacion.ValorPropiedad);
                ddlSubsidio.SelectedValue = oSimulacion.Subsidio_Id.ToString();
                CargaComboSegCesantia();
                HabilitaTxtSubsidio();
                txtMontoSubsidio.Text = String.Format("{0:0.0000}", oSimulacion.MontoSubsidio);
                txtBonoIntegracion.Text = String.Format("{0:0.0000}", oSimulacion.MontoBonoIntegracion);
                txtBonoCaptacion.Text = String.Format("{0:0.0000}", oSimulacion.MontoBonoCaptacion);
                txtMontoContado.Text = String.Format("{0:0.0000}", oSimulacion.MontoContado);
                txtMontoCredito.Text = String.Format("{0:0.0000}", oSimulacion.MontoCredito);
                txtPorcentajeFinanciamiento.Text = String.Format("{0:0}", oSimulacion.PorcentajeFinanciamiento);
                Controles.EjecutarJavaScript("ActualizaMontos('');");
                ddlMesesGracia.SelectedValue = oSimulacion.Gracia.ToString();
                ddlPlazo.SelectedValue = oSimulacion.Plazo.ToString();
                CargaComboPlazosSecundarios(int.Parse(ddlProducto.SelectedValue), int.Parse(ddlPlazo.SelectedValue));
                ddlPlazoFlexible.SelectedValue = oSimulacion.PlazoFlexible.ToString();
                chIndDfl2.Checked = oSimulacion.IndDfl2;
                ddlTipoInmueble.SelectedValue = oSimulacion.TipoPropiedad_Id.ToString();
                ddlAntiguedad.SelectedValue = oSimulacion.Antiguedad_Id.ToString();
                ddlSeguroDesgravamen.SelectedValue = oSimulacion.SeguroDesgravamen_Id.ToString();
                ObtieneTasaSeguro(ref txtTasaSeguroDes, int.Parse(ddlSeguroDesgravamen.SelectedValue));
                ddlSeguroIncendio.SelectedValue = oSimulacion.SeguroIncendio_Id.ToString();
                ObtieneTasaSeguro(ref txtTasaSeguroInc, int.Parse(ddlSeguroIncendio.SelectedValue));
                ddlSeguroCesantia.SelectedValue = oSimulacion.SeguroCesantia_Id.ToString();
                ObtieneTasaSeguro(ref txtTasaSeguroCes, int.Parse(ddlSeguroCesantia.SelectedValue));
                txtNroCodeudores.Text = (oSimulacion.CantidadDeudores - 1).ToString();
                txtMontoAsegurado.Text = oSimulacion.MontoAseguradoSeguroIncendio.ToString();
                chkIndTasaEspecial.Checked = oSimulacion.IndTasaEspecial;
                if (chkIndTasaEspecial.Checked)
                {
                    txtTasaEspecial.Enabled = true;
                    txtTasaEspecial.Text = String.Format("{0:0.0000}", oSimulacion.TasaAnual);
                }
                else
                {
                    txtTasaEspecial.Enabled = false;
                    txtTasaEspecial.Text = "";
                }

                if (oSimulacion.Solicitud_Id != -1)
                    btnGenerarSolicitud.Visible = false;
                else
                    btnGenerarSolicitud.Visible = true;
                Simular();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar el la Simulación");
                }
            }
        }








        #endregion


        protected void chkIndTasaEspecial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndTasaEspecial.Checked)
            {
                txtTasaEspecial.Enabled = true;

            }
            else
            {
                txtTasaEspecial.Enabled = false;
                txtTasaEspecial.Text = "";
            }
        }

        protected void btnEnviarSimulacion_Click(object sender, EventArgs e)
        {
            try
            {
                Mail.MailInfo oMail = new Mail.MailInfo();
                Resultado<Mail.MailInfo> oResultado = new Resultado<Mail.MailInfo>();


                oMail.Asunto = "Simulación Hipotecaria";
                oMail.Destinatarios = NegClientes.objClienteInfo.Mail;

                string urlPlantillaMail = "~/Reportes/PlantillasMail/EnvioAdjunto.aspx";
                string ContenidMail = "";
                Mail.objInformacionAdjunto = new Mail.InformacionAdjunto();
                Mail.objInformacionAdjunto.NombreAdjunto = "su Simulación Hipotecaria Realizada el " + DateTime.Now.ToLongDateString();
                Mail.objInformacionAdjunto.NombreCliente = NegClientes.objClienteInfo.NombreCompleto;


                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlPlantillaMail, htmlStringWriter);
                ContenidMail = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                oMail.Texto = ContenidMail;


                if (NegSimulacionHipotecaria.oReporteSimulacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Realizar la Simulación para Generar el Reporte");
                    return;
                }

                if (NegSimulacionHipotecaria.lstSimulacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Realizar la Simulación para Generar el Reporte");
                    return;
                }
                if (NegGastosOperacionales.lstGastosOperacionales == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Consultar los  Gastos Operacionales para Generar el Reporte");
                    return;
                }
                byte[] Binarios = null;
                string Archivo = "Simulación Hipotecaria de " + NegClientes.objClienteInfo.NombreCompleto + ".pdf";

                string urlDocumento = "~/Reportes/PDF/SimulacionCreditoHipotecario.aspx";
                string ContenidoHtml = "";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Reporte de Simulación";
                NegSimulacionHipotecaria.oReporteSimulacion.oCliente = new ClientesInfo();
                NegSimulacionHipotecaria.oReporteSimulacion.oCliente = NegClientes.objClienteInfo;
                NegSimulacionHipotecaria.oReporteSimulacion.lstSimulacion = NegSimulacionHipotecaria.lstSimulacion;
                NegSimulacionHipotecaria.oReporteSimulacion.lstGastoOperacional = NegGastosOperacionales.lstGastosOperacionales;
                NegSimulacionHipotecaria.oReporteSimulacion.oEjecutivo = NegUsuarios.Usuario;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionTipoInmueble = ddlTipoInmueble.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionComuna = ddlComuna.SelectedValue == "-1" ? "------" : ddlComuna.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionCooperativa = ddlCooperativa.SelectedValue == "-1" ? "------" : ddlCooperativa.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionInmobiliaria = ddlInmobiliaria.SelectedValue == "-1" ? "------" : ddlInmobiliaria.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionProyecto = ddlProyecto.SelectedValue == "-1" ? "------" : ddlProyecto.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionTipoFinanciamiento = ddlTipoFinanciamiento.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionProducto = ddlProducto.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionObjetivo = ddlObjetivo.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionDestino = ddlDestino.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionSubsidio = ddlSubsidio.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionSegDesgravamen = ddlSeguroDesgravamen.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionSegIncendio = ddlSeguroIncendio.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionSegCesantia = ddlSeguroCesantia.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrPrecioVentaPesos = txtPrecioVentaPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoSubsidioPesos = txtMontoSubsidioPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoBonoIntegracionPesos = txtBonoIntegracionPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoBonoCaptacionPesos = txtBonoCaptacionPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoContadoPesos = txtMontoContadoPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrMontoCreditoPesos = txtMontoCreditoPesos.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrValorUF = "Valor UF $ " + ((Anthem.Label)Master.FindControl("lblValorUF")).Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.DescripcionGracia = ddlMesesGracia.SelectedItem.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.StrPorcentajeFinanciamiento = txtPorcentajeFinanciamiento.Text;
                NegSimulacionHipotecaria.oReporteSimulacion.oSimulacion.TasaAnual = decimal.Parse(txtTasaEspecial.Text == "" ? "0" : txtTasaEspecial.Text);

                CargaParrafoPDF();
                htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();
                Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);
                Session["ArchivoSimulacion"] = Binarios;


                oMail.Adjuntos.Add(new Mail.AdjuntosMail() { Archivo = Binarios, NombreArchivo = Archivo });

                oResultado = Mail.EnviarMail(oMail, NegConfiguracionGeneral.Obtener());
                if (!oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                   
                }
                else
                {
                    Controles.MostrarMensajeExito("Simulación Enviada Correctamente");
                   
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
                    Controles.MostrarMensajeError("Error al Enviar la Simulación");
                }
            }
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