using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;


namespace WorkFlow.Eventos
{
    public partial class ConfeccionInformeEETTPiloto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioSolicitudEETT();");
            if (!Page.IsPostBack)
            {
                CargarAcciones();
                CargaComboFabricaAbogados();
                CargarInfoSolicitud();
                DatosDocumentosEstudioTitulo.DesplegarDatosEstudioTitulo(true);
                DatosDocumentosEstudioTitulo.PermitirActualizacionDocumentos(false);
                DatosInmobiliariaProyecto.CargaComboRegion();
                DatosInmobiliariaProyecto.CargaComboInmobiliaria();
                DatosInmobiliariaProyecto.ddlInmobiliaria.SelectedValue = NegSolicitudes.objSolicitudInfo.Inmobiliaria_Id.ToString();
                DatosInmobiliariaProyecto.CargaComboProyecto(NegSolicitudes.objSolicitudInfo.Inmobiliaria_Id);
                DatosInmobiliariaProyecto.ddlProyecto.SelectedValue = NegSolicitudes.objSolicitudInfo.Proyecto_Id.ToString();
                DatosInmobiliariaProyecto.ObtenerDatosInmobiliaria(NegSolicitudes.objSolicitudInfo.Inmobiliaria_Id);
                DatosInmobiliariaProyecto.ObtenerDatosProyecto(NegSolicitudes.objSolicitudInfo.Proyecto_Id);
                DatosInmobiliariaProyecto.PermiteActualizacion(false);


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
                    if (!ValidarDocumentos()) return;
                    if (!ValidarObservaciones()) return;
                    if (!ActualizaSolicitud()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {

                    if (!ValidarDocumentos()) return;
                    if (!ValidarObservaciones()) return;
                    if (!ActualizaSolicitud()) return;
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
        private bool ValidarDocumentos()
        {
            try
            {
                RegistroDocumentosEstudioTituloInfo oDocumento = new RegistroDocumentosEstudioTituloInfo();
                Resultado<RegistroDocumentosEstudioTituloInfo> rAntecedentes = new Resultado<RegistroDocumentosEstudioTituloInfo>();
                NegDocumentosEstudioTitulo nDocumentos = new NegDocumentosEstudioTitulo();


                oDocumento.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oDocumento.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oDocumento.Tasacion_Id = -1;


                rAntecedentes = nDocumentos.BuscarRegistroDocumentosEstudioTitulo(oDocumento);
                if (rAntecedentes.ResultadoGeneral)
                {

                    if (rAntecedentes.Lista.Where(d => d.Obligatorio == true && d.Seleccionado == false).Count() > 0)
                    {
                        var Tasacion = new TasacionInfo();
                        Controles.MostrarMensajeAlerta("Falta Ingresar Información del Documento: " + rAntecedentes.Lista.FirstOrDefault(d => d.Obligatorio == true && d.Seleccionado == false).DescripcionDocumento);
                        return false;
                    }

                }
                else
                {
                    Controles.MostrarMensajeError(rAntecedentes.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Procesar el Evento");
                }
                return false;
            }
        }
        private void CargarInfoSolicitud()
        {
            try
            {
                ddlFabAbogados.SelectedValue = NegSolicitudes.objSolicitudInfo.FabricaAbogado_Id.ToString();
                CargaComboAbogados(NegSolicitudes.objSolicitudInfo.FabricaAbogado_Id);
                ddlAbogado.SelectedValue = NegSolicitudes.objSolicitudInfo.Abogado_Id.ToString();
                txtFechaSolicitudTasacion.Text = NegSolicitudes.objSolicitudInfo.FechaSolicitudEstudioTitulo == null ? "" : NegSolicitudes.objSolicitudInfo.FechaSolicitudEstudioTitulo.Value.ToShortDateString();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Información de la Solicitud");
                }
            }
        }
        private void CargaComboFabricaAbogados()
        {
            try
            {
                NegFabricas oNegocio = new NegFabricas();
                Resultado<AsignacionTipoFabricaInfo> oResultado = new Resultado<AsignacionTipoFabricaInfo>();
                AsignacionTipoFabricaInfo oFabrica = new AsignacionTipoFabricaInfo();

                oFabrica.TipoFabrica_Id = 2;//Fábricas de Abogados
                oResultado = oNegocio.BuscarAsignacionTipoFabrica(oFabrica);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref ddlFabAbogados, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "--Fábrica Abogados--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar Combo de Fábricas de Abogados");
                }
            }
        }
        protected void ddlFabAbogados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboAbogados(int.Parse(ddlFabAbogados.SelectedValue));
        }
        private void CargaComboAbogados(int Fabrica_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();
                if (Fabrica_Id == -1)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref ddlAbogado, new System.Collections.Generic.List<UsuarioInfo>(), "Rut", "Nombre", "--Abogado--", "-1");
                    return;
                }
                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolAbogado;
                ObjetoUsuario.Fabrica_Id = Fabrica_Id;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref ddlAbogado, ObjetoResultado.Lista, "Rut", "Nombre", "--Abogado--", "-1");
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
        private bool ActualizaSolicitud()
        {

            try
            {
                if (ddlFabAbogados.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Fábrica");
                    return false;
                }
                if (ddlAbogado.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Abogado");
                    return false;
                }
                if (txtFechaSolicitudTasacion.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe ingresar una Fecha de Aprobación");
                    return false;
                }


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

                NegSolicitudes.objSolicitudInfo.FechaAprobacionEstudioTitulo = DateTime.Now;

                rSolicitud = nSolicitud.Guardar(NegSolicitudes.objSolicitudInfo);
                if (rSolicitud.ResultadoGeneral)
                {
                    AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
                    Resultado<AsignacionSolicitudInfo> rAsignacion = new Resultado<AsignacionSolicitudInfo>();
                    oAsignacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                    oAsignacion.Usuario_Id = NegUsuarios.Usuario.Rut;

                    //Asinacion de Abogado
                    oAsignacion.Rol_Id = Constantes.IdRolAbogado;
                    oAsignacion.Responsable_Id = int.Parse(ddlAbogado.SelectedValue);
                    rAsignacion = nSolicitud.GuardarAsignacion(oAsignacion);
                    if (rAsignacion.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(rAsignacion.Mensaje);
                        return false;
                    }

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