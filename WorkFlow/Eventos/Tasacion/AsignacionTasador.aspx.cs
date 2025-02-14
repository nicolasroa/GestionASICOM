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
    public partial class AsignacionTasador : System.Web.UI.Page
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
                oTasacion.IndPropiedadPrincipal = true;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedadesTasacion, rTasacion.Lista, new string[] { "Id", "Propiedad_Id" });
                    NegPropiedades.lstPropiedadesTasaciones = new List<TasacionInfo>();
                    NegPropiedades.lstPropiedadesTasaciones = rTasacion.Lista;
                    if (NegPropiedades.lstPropiedadesTasaciones.Count() == 1)
                    {
                        NegPropiedades.objTasacion = new TasacionInfo();
                        NegPropiedades.objTasacion = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault();
                        CargarInfoTasacion(NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault());
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

                if (NegPropiedades.lstPropiedadesTasaciones.Count(t => t.NombreTasador == "" && t.IndPropiedadPrincipal == true) != 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Indicar el Tasador de todas las Propiedades");
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
            if (oTasacion.TasacionPadre_Id > 0)
            {

                oTasacionPadre = NegPropiedades.lstPropiedadesTasaciones.FirstOrDefault(t => t.Id == oTasacion.TasacionPadre_Id);
                NegPropiedades.objTasacion = new TasacionInfo();
                NegPropiedades.objTasacion = oTasacionPadre;
                CargarInfoTasacion(oTasacionPadre);
            }
            else
            {
                NegPropiedades.objTasacion = new TasacionInfo();
                NegPropiedades.objTasacion = oTasacion;
                CargarInfoTasacion(oTasacion);
            }
        }
        private void CargarInfoTasacion(TasacionInfo oTasacion)
        {
            try
            {
                if (oTasacion != null)
                {
                    lblDireccionTasacion.Text = oTasacion.DireccionCompleta;
                    AddlFabTasadores.SelectedValue = oTasacion.FabricaTasador_Id.ToString();
                    CargaComboTasadores(oTasacion.FabricaTasador_Id);
                    AddlTasador.SelectedValue = oTasacion.Tasador_Id.ToString();
                    txtFechaSolicitudTasacion.Text = oTasacion.FechaSolicitudTasacion == null ? oTasacion.FechaTasacionPrevia.Value.ToShortDateString() : oTasacion.FechaSolicitudTasacion.Value.ToShortDateString();
                    lblFechaSolicitudTasacion.Text = oTasacion.FechaSolicitudTasacion == null ? "Fecha de Tasación Previa" : "Fecha Solicitud Tasación";
                    txtNombreContactoTasacion.Text = oTasacion.NombreContactoTasacion;
                    txtEmailContactoTasacion.Text = oTasacion.EmailContactoTasacion;
                    txtTelefonoContactoTasacion.Text = oTasacion.TelefonoContactoTasacion;


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
        private void GrabarInfoTasacion()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();
                TasacionInfo oTasacion = new TasacionInfo();

                if (NegPropiedades.objTasacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Inmueble");
                    return;
                }
                if (!ValidarInfoTasacion()) return;
                oTasacion = NegPropiedades.objTasacion;
                oTasacion.FabricaTasador_Id = int.Parse(AddlFabTasadores.SelectedValue);
                oTasacion.Tasador_Id = int.Parse(AddlTasador.SelectedValue);
                rTasacion = nTasacion.GuardarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {

                    if (!ActualizarAsignacion()) return;

                    Controles.MostrarMensajeExito("Datos Grabados");
                    CargarPropiedades();
                    LimpiarInfoTasacion();
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
                    Controles.MostrarMensajeError("Error al Grabar los datos de la Tasación");
                }
            }
        }

        private bool ActualizarAsignacion()
        {

            try
            {

                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();
                oBandeja = NegBandejaEntrada.oBandejaEntrada;


                NegSolicitudes oNegocio = new NegSolicitudes();
                Resultado<SolicitudInfo> oResultado = new Resultado<SolicitudInfo>();
                SolicitudInfo oInfo = new SolicitudInfo();
                var ObjInfo = new SolicitudInfo();
                oInfo.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                oResultado = oNegocio.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Count != 0)
                    {

                        ObjInfo = oResultado.Lista.FirstOrDefault(s => s.Id == oInfo.Id);
                        ObjInfo.FabricaTasadores_Id = int.Parse(AddlFabTasadores.SelectedValue);
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                    return false;
                }

                ////Asignacion de Variables
                oResultado = oNegocio.Guardar(ref ObjInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                    return false;
                }

                AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
                Resultado<AsignacionSolicitudInfo> rAsignacion = new Resultado<AsignacionSolicitudInfo>();
                oAsignacion.Solicitud_Id = ObjInfo.Id;
                oAsignacion.Usuario_Id = NegUsuarios.Usuario.Rut;

                oAsignacion.Rol_Id = Constantes.IdRolTasador;
                oAsignacion.Responsable_Id = int.Parse(AddlTasador.SelectedValue);
                rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
                if (rAsignacion.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(rAsignacion.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Actualizar la Asignación");
                }
                return false;
            }
        }
        private void LimpiarInfoTasacion()
        {

            lblDireccionTasacion.Text = "";
            AddlTasador.ClearSelection();
            AddlFabTasadores.ClearSelection();
            txtFechaSolicitudTasacion.Text = "";
            txtEmailContactoTasacion.Text = "";
            txtNombreContactoTasacion.Text = "";
            txtTelefonoContactoTasacion.Text = "";
            NegPropiedades.objTasacion = null;
        }
        private bool ValidarInfoTasacion()
        {
            try
            {
                if (AddlTasador.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tasador");
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
    }
}