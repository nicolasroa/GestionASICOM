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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                NegSolicitudes.IndComentario = true;
                CargarAcciones();
                CargaFormulario();
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


        protected void AddlFabFormalizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboFormalizador(int.Parse(AddlFabFormalizacion.SelectedValue));
        }

        protected void AddlFabAbogados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboAbogados(int.Parse(AddlFabAbogados.SelectedValue));
        }

        protected void AddlFabNotarias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboNotaria(int.Parse(AddlFabNotarias.SelectedValue));
        }

        protected void AddlFabTasadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboTasadores(int.Parse(AddlFabTasadores.SelectedValue));
        }
        //protected void AddlFabFormalizacionGestion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CargaComboFormalizadorGestion(int.Parse(AddlFabFormalizacionGestion.SelectedValue));
        //}

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
                    //validacion
                    if (!GuardaAsignacion()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    //validacion
                    if (!GuardaAsignacion()) return;

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

        private void CargaFormulario()
        {
            CargaAsignacion();
            CargaCombosFabrica();
            //CargaCombosEjecutivosFabrica();
        }
        private void CargaAsignacion()
        {
            try
            {
                NegSolicitudes nSolicitud = new NegSolicitudes();
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                rSolicitud = nSolicitud.Buscar(oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    if (rSolicitud.Lista.Count != 0)
                    {
                        var ObjInfo = new SolicitudInfo();
                        ObjInfo = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        AtxtEjecutivoComercial.Text = string.IsNullOrEmpty(ObjInfo.DescripcionEjecutivoComercial) ? "Sin Información" : ObjInfo.DescripcionEjecutivoComercial.ToString();
                        AtxtEjecutivoComercial.Enabled = false;
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
                    Controles.MostrarMensajeError("Error al Procesar el Evento");
                }
            }
        }
     
     

        private void CargaCombosFabrica()
        {
            CargaComboFabricaFormalizacion();
            //CargaComboFabricaFormalizacionGestion();
            CargaComboFabricaAbogados();
            CargaComboFabricaNotaria();
            CargaComboFabricaTasadores();
        }

        private void CargaComboFabricaFormalizacion()
        {
            try
            {
                NegFabricas oNegocio = new NegFabricas();
                Resultado<AsignacionTipoFabricaInfo> oResultado = new Resultado<AsignacionTipoFabricaInfo>();
                AsignacionTipoFabricaInfo oFabrica = new AsignacionTipoFabricaInfo();

                oFabrica.TipoFabrica_Id = 1;//Fábricas de Formalización
                oResultado = oNegocio.BuscarAsignacionTipoFabrica(oFabrica);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Count == 1)
                    {
                        Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabFormalizacion, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "", "");
                        CargaComboFormalizador(int.Parse(AddlFabFormalizacion.SelectedValue));
                    }
                    else
                        Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabFormalizacion, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "--Fábrica Ejecutivos Hipotecarios--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar Combo de Fábricas de Formalización");
                }
            }
        }
        //private void CargaComboFabricaFormalizacionGestion()
        //{
        //    try
        //    {
        //        NegFabricas oNegocio = new NegFabricas();
        //        Resultado<AsignacionTipoFabricaInfo> oResultado = new Resultado<AsignacionTipoFabricaInfo>();
        //        AsignacionTipoFabricaInfo oFabrica = new AsignacionTipoFabricaInfo();

        //        oFabrica.TipoFabrica_Id = 5;//Fábricas de Formalización de Gestión
        //        oResultado = oNegocio.BuscarAsignacionTipoFabrica(oFabrica);
        //        if (oResultado.ResultadoGeneral)
        //        {
        //            Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabFormalizacionGestion, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "--Fábrica Formalización de Gestión--", "-1");
        //        }
        //        else
        //        {
        //            Controles.MostrarMensajeError(oResultado.Mensaje);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        if (Constantes.ModoDebug == true)
        //        {
        //            Controles.MostrarMensajeError(Ex.Message, Ex);
        //        }
        //        else
        //        {
        //            Controles.MostrarMensajeError("Error al Cargar Combo de Fábricas de Formalización");
        //        }
        //    }
        //}
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
                    if (oResultado.Lista.Count == 1)
                    {
                        Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabAbogados, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "", "");
                        CargaComboAbogados(int.Parse(AddlFabAbogados.SelectedValue));
                    }
                    else
                        Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabAbogados, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "--Fábrica Abogados--", "-1");
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
        private void CargaComboFabricaNotaria()
        {
            try
            {
                NegFabricas oNegocio = new NegFabricas();
                Resultado<AsignacionTipoFabricaInfo> oResultado = new Resultado<AsignacionTipoFabricaInfo>();
                AsignacionTipoFabricaInfo oFabrica = new AsignacionTipoFabricaInfo();

                oFabrica.TipoFabrica_Id = 3;//Fábricas de Notarías
                oResultado = oNegocio.BuscarAsignacionTipoFabrica(oFabrica);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Count == 1)
                    {
                        Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabNotarias, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "", "");
                        CargaComboNotaria(int.Parse(AddlFabNotarias.SelectedValue));
                    }
                    else
                        Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabNotarias, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "--Fábrica Notaría--", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar Combo de Fábricas de Notaía");
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
                    if (oResultado.Lista.Count == 1)
                    {
                        Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabTasadores, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "", "");
                        CargaComboTasadores(int.Parse(AddlFabTasadores.SelectedValue));
                    }
                    else
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

        private void CargaCombosEjecutivosFabrica()
        {

            Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizador, null, "Rut", "Nombre", "--Ejecutivo Hipotecario--", "-1");
            // Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizadorGestion, null, "Rut", "Nombre", "--Formalizador de Gestión--", "-1");
            Controles.CargarCombo<UsuarioInfo>(ref AddlAbogados, null, "Rut", "Nombre", "--Abogado--", "-1");
            Controles.CargarCombo<UsuarioInfo>(ref AddlNotaria, null, "Rut", "Nombre", "--Notaría--", "-1");
            Controles.CargarCombo<UsuarioInfo>(ref AddlTasador, null, "Rut", "Nombre", "--Tasador--", "-1");
        }

        private void CargaComboFormalizador(int Fabrica_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();
                if (Fabrica_Id == -1)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizador, new System.Collections.Generic.List<UsuarioInfo>(), "Rut", "Nombre", "--Ejecutivo Hipotecario--", "-1");
                    return;
                }
                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolFormalizador;
                ObjetoUsuario.Fabrica_Id = Fabrica_Id;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizador, ObjetoResultado.Lista, "Rut", "Nombre", "--Ejecutivo Hipotecario--", "-1");
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
        //private void CargaComboFormalizadorGestion(int Fabrica_Id)
        //{
        //    try
        //    {
        //        ////Declaracion de Variables
        //        var ObjetoUsuario = new UsuarioInfo();
        //        var ObjetoResultado = new Resultado<UsuarioInfo>();
        //        var NegUsuario = new NegUsuarios();
        //        if (Fabrica_Id == -1)
        //        {
        //            Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizadorGestion, new System.Collections.Generic.List<UsuarioInfo>(), "Rut", "Nombre", "--Formalizador de Gestión--", "-1");
        //            return;
        //        }
        //        ////Asignacion de Variables
        //        ObjetoUsuario.Rol_Id = Constantes.IdRolFormalizadorGestion;
        //        ObjetoUsuario.Fabrica_Id = Fabrica_Id;
        //        ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
        //        ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
        //        if (ObjetoResultado.ResultadoGeneral)
        //        {
        //            Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizadorGestion, ObjetoResultado.Lista, "Rut", "Nombre", "--Formalizador de Gestión--", "-1");
        //        }
        //        else
        //        {
        //            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        if (Constantes.ModoDebug == true)
        //        {
        //            Controles.MostrarMensajeError(Ex.Message, Ex);
        //        }
        //        else
        //        {
        //            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Ejecutivo");
        //        }
        //    }
        //}
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
                    Controles.CargarCombo<UsuarioInfo>(ref AddlAbogados, new System.Collections.Generic.List<UsuarioInfo>(), "Rut", "Nombre", "--Abogado--", "-1");
                    return;
                }
                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolAbogado;
                ObjetoUsuario.Fabrica_Id = Fabrica_Id;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref AddlAbogados, ObjetoResultado.Lista, "Rut", "Nombre", "--Abogado--", "-1");
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
        private void CargaComboNotaria(int Fabrica_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();
                if (Fabrica_Id == -1)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref AddlNotaria, new System.Collections.Generic.List<UsuarioInfo>(), "Rut", "Nombre", "--Notaría--", "-1");
                    return;
                }
                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolNotaria;
                ObjetoUsuario.Fabrica_Id = Fabrica_Id;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref AddlNotaria, ObjetoResultado.Lista, "Rut", "Nombre", "--Notaría--", "-1");
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
                    Controles.CargarCombo<UsuarioInfo>(ref AddlTasador, new System.Collections.Generic.List<UsuarioInfo>(), "Rut", "Nombre", "--Tasador--", "-1");
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

        private bool ValidarFormulario()
        {
            try
            {
               

                if (AddlFormalizador.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Ejecutivo Hipotecario");
                    return false;
                }
                //if (AddlFormalizadorGestion.SelectedValue == "-1")
                //{
                //    Controles.MostrarMensajeAlerta("Debe Seleccionar un Formalizador de Gestión");
                //    return false;
                //}

                if (AddlAbogados.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Abogado");
                    return false;
                }

                if (AddlNotaria.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Asistente de Notaría");
                    return false;
                }

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
                    Controles.MostrarMensajeError("Error al Validar Formulario");
                }
                return false;
            }
        }
        private bool GuardaAsignacion()
        {
            try
            {
                if (!ValidarFormulario()) { return false; }

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
                        ObjInfo.FabricaFormalizacion_Id = int.Parse(AddlFabFormalizacion.SelectedValue);
                        //ObjInfo.FabricaFormalizacionGestion_Id = int.Parse(AddlFabFormalizacionGestion.SelectedValue);
                        ObjInfo.FabricaAbogado_Id = int.Parse(AddlFabAbogados.SelectedValue);
                        ObjInfo.FabricaNotaria_Id = int.Parse(AddlFabNotarias.SelectedValue);
                        ObjInfo.FabricaTasadores_Id = int.Parse(AddlFabTasadores.SelectedValue);
                        ObjInfo.EjecutivoControlEtapa_Id = int.Parse(AddlFormalizador.SelectedValue);
                        ObjInfo.Abogado_Id = int.Parse(AddlAbogados.SelectedValue);
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

                ////Asinacion de Jefe de Producto
                //oAsignacion.Rol_Id = Constantes.IdRolJefeProducto;
                //oAsignacion.Responsable_Id = int.Parse(AddlJefeProducto.SelectedValue);
                //rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
                //if (rAsignacion.ResultadoGeneral == false)
                //{
                //    Controles.MostrarMensajeError(rAsignacion.Mensaje);
                //    return false;
                //}


                ////Asinacion de Visador
                //oAsignacion.Rol_Id = Constantes.IdRolVisador;
                //oAsignacion.Responsable_Id = int.Parse(AddlVisador.SelectedValue);
                //rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
                //if (rAsignacion.ResultadoGeneral == false)
                //{
                //    Controles.MostrarMensajeError(rAsignacion.Mensaje);
                //    return false;
                //}

                //Asinacion de Formalizador
                oAsignacion.Rol_Id = Constantes.IdRolFormalizador;
                oAsignacion.Responsable_Id = int.Parse(AddlFormalizador.SelectedValue);
                rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
                if (rAsignacion.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(rAsignacion.Mensaje);
                    return false;
                }
                ////Asinacion de Formalizador de Gestión
                //oAsignacion.Rol_Id = Constantes.IdRolFormalizadorGestion;
                //oAsignacion.Responsable_Id = int.Parse(AddlFormalizadorGestion.SelectedValue);
                //rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
                //if (rAsignacion.ResultadoGeneral == false)
                //{
                //    Controles.MostrarMensajeError(rAsignacion.Mensaje);
                //    return false;
                //}

                //Asinacion de Abogado
                oAsignacion.Rol_Id = Constantes.IdRolAbogado;
                oAsignacion.Responsable_Id = int.Parse(AddlAbogados.SelectedValue);
                rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
                if (rAsignacion.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(rAsignacion.Mensaje);
                    return false;
                }

                //Asinacion de Asistente de Notaria
                oAsignacion.Rol_Id = Constantes.IdRolNotaria;
                oAsignacion.Responsable_Id = int.Parse(AddlNotaria.SelectedValue);
                rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
                if (rAsignacion.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(rAsignacion.Mensaje);
                    return false;
                }

                //Asinacion de Tasador
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
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