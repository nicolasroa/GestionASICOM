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

namespace WorkFlow.Aplicaciones
{
    public partial class ActualizarSolicitud : System.Web.UI.Page
    {
        public decimal UF = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
                UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
                CargarControlEventos();
                CargaComboEjecutivoComercial();
                if (NegSolicitudes.objSolicitudInfo.SubEstado_Id == (int)NegTablas.IdentificadorMaestro("SUBEST_SOLICITUD", "EVA"))//Solicitud en Evaluación
                {
                    DivAsignacionFlujoFormalizacion.Visible = false;
                    DivSupervisoresFormalizacion.Visible = false;
                    TabGGOO.Visible = false;
                }
                else
                {
                    CargaComboJefeProducto();
                    CargaComboVisador();
                    CargaCombosFabrica();
                    CargaAsignacion();
                    CargarInfoSolicitud();
                    CargarGastosOperacionales();
                    CargarInfoCuentaGGOO();
                }
            }
        }

        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            NegBandejaEntrada.oBandejaEntrada = new BandejaEntradaInfo();
            NegBandejaEntrada.oBandejaEntrada.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }

        protected void gdvControlEventos_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gdvControlEventos.PageIndex = e.NewPageIndex;
            CargarControlEventos();
        }

        //Asignacion
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

        private void CargaAsignacion()
        {
            try
            {


                AddlFabFormalizacion.SelectedValue = NegSolicitudes.objSolicitudInfo.FabricaFormalizacion_Id.ToString();
                AddlFabAbogados.SelectedValue = NegSolicitudes.objSolicitudInfo.FabricaAbogado_Id.ToString();
                AddlFabNotarias.SelectedValue = NegSolicitudes.objSolicitudInfo.FabricaNotaria_Id.ToString();
                AddlFabTasadores.SelectedValue = NegSolicitudes.objSolicitudInfo.FabricaTasadores_Id.ToString();


                CargaComboFormalizador(NegSolicitudes.objSolicitudInfo.FabricaFormalizacion_Id);
                CargaComboAbogados(NegSolicitudes.objSolicitudInfo.FabricaAbogado_Id);
                CargaComboNotaria(NegSolicitudes.objSolicitudInfo.FabricaNotaria_Id);
                CargaComboTasadores(NegSolicitudes.objSolicitudInfo.FabricaTasadores_Id);


                NegSolicitudes negSolicitudes = new NegSolicitudes();
                AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
                Resultado<AsignacionSolicitudInfo> oResultado = new Resultado<AsignacionSolicitudInfo>();
                oAsignacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oResultado = negSolicitudes.BuscarAsignacion(oAsignacion);

                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Count > 0)
                    {
                        var Asistente_Id = -1;
                        var Abogado_Id = -1;
                        var Notaria_Id = -1;
                        var Tasador_Id = -1;
                        var JefeProducto_Id = -1;
                        var EjecutivoComercial = -1;

                        Asistente_Id = oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolFormalizador) == null ? -1 : oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolFormalizador).Responsable_Id;
                        Abogado_Id = oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolAbogado) == null ? -1 : oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolAbogado).Responsable_Id;
                        Notaria_Id = oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolNotaria) == null ? -1 : oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolNotaria).Responsable_Id;
                        Tasador_Id = oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolTasador) == null ? -1 : oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolTasador).Responsable_Id;
                        JefeProducto_Id = oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolJefeProducto) == null ? -1 : oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolJefeProducto).Responsable_Id;
                        EjecutivoComercial = oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolEjecutivoComercial) == null ? -1 : oResultado.Lista.FirstOrDefault(a => a.Rol_Id == Constantes.IdRolEjecutivoComercial).Responsable_Id;


                        AddlFormalizador.SelectedValue = Asistente_Id.ToString();
                        AddlAbogados.SelectedValue = Abogado_Id.ToString();
                        AddlNotaria.SelectedValue = Notaria_Id.ToString();
                        AddlTasador.SelectedValue = Tasador_Id.ToString();
                        AddlJefeProducto.SelectedValue = JefeProducto_Id.ToString();
                        AddlEjecutivoComercial.SelectedValue = EjecutivoComercial.ToString();

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
                    Controles.MostrarMensajeError("Error al Procesar el Evento");
                }
            }
        }
        private void CargaComboJefeProducto()
        {
            try
            {
                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();

                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolJefeProducto;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (ObjetoResultado.Lista.Count == 1)
                        Controles.CargarCombo<UsuarioInfo>(ref AddlJefeProducto, ObjetoResultado.Lista, "Rut", "Nombre", "", "");
                    else
                        Controles.CargarCombo<UsuarioInfo>(ref AddlJefeProducto, ObjetoResultado.Lista, "Rut", "Nombre", "--Jefe Producto--", "-1");
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
        private void CargaComboVisador()
        {
            try
            {
                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();

                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolVisador;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (ObjetoResultado.Lista.Count == 1)
                        Controles.CargarCombo<UsuarioInfo>(ref AddlVisador, ObjetoResultado.Lista, "Rut", "Nombre", "", "");
                    else
                        Controles.CargarCombo<UsuarioInfo>(ref AddlVisador, ObjetoResultado.Lista, "Rut", "Nombre", "--Visador--", "-1");
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

        private void CargaComboEjecutivoComercial()
        {
            try
            {
                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();

                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolEjecutivoComercial;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (ObjetoResultado.Lista.Count == 1)
                        Controles.CargarCombo<UsuarioInfo>(ref AddlEjecutivoComercial, ObjetoResultado.Lista, "Rut", "Nombre", "", "");
                    else
                        Controles.CargarCombo<UsuarioInfo>(ref AddlEjecutivoComercial, ObjetoResultado.Lista, "Rut", "Nombre", "--Ejecutivo Comercial--", "-1");

                    AddlEjecutivoComercial.SelectedValue = NegSolicitudes.objSolicitudInfo.EjecutivoComercial_Id.ToString();

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
                        Controles.CargarCombo<AsignacionTipoFabricaInfo>(ref AddlFabFormalizacion, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica", "--Fábrica Asistente--", "-1");
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

            Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizador, null, "Rut", "Nombre", "--Asistente--", "-1");
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
                    Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizador, new System.Collections.Generic.List<UsuarioInfo>(), "Rut", "Nombre", "--Asistente--", "-1");
                    return;
                }
                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolFormalizador;
                ObjetoUsuario.Fabrica_Id = Fabrica_Id;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref AddlFormalizador, ObjetoResultado.Lista, "Rut", "Nombre", "--Asistente--", "-1");
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
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Asistente");
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
                    CargarControlEventos();
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
        private void CargarControlEventos()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                BandejaEntradaFiltro oFiltro = new BandejaEntradaFiltro();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaInfo> oResultado = new Resultado<BandejaEntradaInfo>();

                List<BandejaEntradaInfo> lstBandeja = new List<BandejaEntradaInfo>();

                //Asignación de Variables de Búsqueda
                oFiltro.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.BuscarEstadoAvance(oFiltro);
                if (oResultado.ResultadoGeneral)
                {


                    Controles.CargarGrid<BandejaEntradaInfo>(ref gdvControlEventos, oResultado.Lista, new string[] { "Id", "Evento_Id", "Estado_Id", "UsuarioResponsable" });

                    var Lista = new List<TablaInfo>();

                    Lista = NegTablas.BuscarCatalogo("EST_EVE");//Lista los estados de un Evento



                    foreach (GridViewRow Row in gdvControlEventos.Rows)
                    {
                        Anthem.DropDownList ddlEstado = (Anthem.DropDownList)Row.FindControl("ddlEstadoEvento");
                        Controles.CargarCombo<TablaInfo>(ref ddlEstado, Lista, "CodigoInterno", "Nombre", "", "");
                        ddlEstado.SelectedValue = gdvControlEventos.DataKeys[Row.RowIndex].Values["Estado_Id"].ToString();
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Eventos");
                }
            }
        }

        protected void btnGuardarAsignacion_Click(object sender, EventArgs e)
        {
            GuardaAsignacion();
        }



        //GGOO
        private void CargarInfoSolicitud()
        {
            try
            {
                txtRutSolicitante.Text = NegSolicitudes.objSolicitudInfo.RutCliente;
                txtNombreSolicitante.Text = NegSolicitudes.objSolicitudInfo.NombreCliente;
                txtDestino.Text = NegSolicitudes.objSolicitudInfo.DescripcionDestino;
                chkIndDfl2.Checked = NegSolicitudes.objSolicitudInfo.IndDfl2;
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
        private void CargarInfoCuentaGGOO()
        {
            try
            {
                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                IntegracionGGOOInfo oIntegracion = new IntegracionGGOOInfo();
                Resultado<IntegracionGGOOInfo> rIntegracion = new Resultado<IntegracionGGOOInfo>();

                oIntegracion.NumeroSolicitud = NegSolicitudes.objSolicitudInfo.Id;
                rIntegracion = nGastos.BuscarResumenCuenta(oIntegracion);
                if (rIntegracion.ResultadoGeneral)
                {
                    oIntegracion = rIntegracion.Lista.FirstOrDefault();
                    if (oIntegracion != null)
                    {
                        txtMontoDisponible.Text = string.Format("{0:C}", oIntegracion.MontoDisponible);
                        txtMontoProvisionado.Text = string.Format("{0:C}", oIntegracion.MontoProvisionado);
                        txtMontoUtilizado.Text = string.Format("{0:C}", oIntegracion.MontoUtilizado);
                    }
                }
                else
                { Controles.MostrarMensajeError(rIntegracion.Mensaje); }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Información de la Cuenta de GGOO");
                }
            }
        }
        private bool SolicitarProvision(GridViewRow row)
        {
            try
            {
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
                NegClientes nCliente = new NegClientes();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();
                ClientesInfo oCliente = new ClientesInfo();
                ParticipanteInfo oParticipante = new ParticipanteInfo();

                decimal ValorUF = decimal.Zero;
                decimal ValorPesos = decimal.Zero;



                oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.TipoParticipacion_Id == 1);//Solicitante Principal

                oCliente.Rut = oParticipante.Rut;
                rCliente = nCliente.Buscar(oCliente);
                if (rCliente.ResultadoGeneral)
                {
                    oCliente = rCliente.Lista.FirstOrDefault();
                }


                Anthem.DropDownList ddlQuienPaga = (Anthem.DropDownList)row.FindControl("ddlQuienPaga");
                Anthem.DropDownList ddlComoPaga = (Anthem.DropDownList)row.FindControl("ddlComoPaga");
                Anthem.TextBox txtValorUF = (Anthem.TextBox)row.FindControl("txtValorUF");

                Anthem.CheckBox chkProvisionSolicitada = (Anthem.CheckBox)row.FindControl("chkProvisionSolicitada");
                var TipoGasto_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["TipoGastoOperacional_Id"].ToString());


                int Moneda_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Moneda_Id"].ToString());

                if (Moneda_Id == 998)
                {
                    ValorUF = decimal.Parse(txtValorUF.Text);
                    ValorPesos = ValorUF * UF;
                }
                else
                {
                    ValorUF = decimal.Parse(txtValorUF.Text) / UF;
                    ValorPesos = ValorUF;
                }

                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                IntegracionGGOOInfo oIntegracion = new IntegracionGGOOInfo();
                Resultado<IntegracionGGOOInfo> rIntegracion = new Resultado<IntegracionGGOOInfo>();

                oIntegracion.NumeroSolicitud = NegSolicitudes.objSolicitudInfo.Id;
                oIntegracion.NombreCliente = oCliente.NombreCompleto;
                oIntegracion.RutCliente = oCliente.RutCompleto;
                oIntegracion.MailCliente = oCliente.Mail;
                oIntegracion.CelularCliente = oCliente.TelefonoMovil;
                oIntegracion.TipoGasto_Id = TipoGasto_Id;
                oIntegracion.QuienPaga_Id = int.Parse(ddlQuienPaga.SelectedValue);
                oIntegracion.ComoPaga_Id = int.Parse(ddlComoPaga.SelectedValue);
                oIntegracion.ValorUF = ValorUF;
                oIntegracion.ValorPesos = ValorPesos;
                oIntegracion.FechaValorizacion = DateTime.Now;
                oIntegracion.IndProvisionSolicitada = chkProvisionSolicitada.Checked;

                rIntegracion = nGastos.SolicitarProvision(oIntegracion);
                if (rIntegracion.ResultadoGeneral)
                {
                    return true;

                }
                else
                {
                    Controles.MostrarMensajeError(rIntegracion.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Solicitar Provisión de GGOO");
                }
                return false;
            }
        }
        private void CargarGastosOperacionales()
        {
            try
            {
                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                GastoOperacionalInfo oGastos = new GastoOperacionalInfo();
                Resultado<GastoOperacionalInfo> rGastos = new Resultado<GastoOperacionalInfo>();


                ComoPagaGGOOInfo oComoPaga = new ComoPagaGGOOInfo();
                QuienPagaGGOOInfo oQuienPaga = new QuienPagaGGOOInfo();
                Resultado<ComoPagaGGOOInfo> rComoPaga = new Resultado<ComoPagaGGOOInfo>();
                Resultado<QuienPagaGGOOInfo> rQuienPaga = new Resultado<QuienPagaGGOOInfo>();

                List<ComoPagaGGOOInfo> lstComoPaga = new List<ComoPagaGGOOInfo>();
                List<QuienPagaGGOOInfo> lstQuienPaga = new List<QuienPagaGGOOInfo>();

                oGastos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;

                rGastos = nGastos.BuscarGastos(oGastos);
                if (rGastos.ResultadoGeneral)
                {

                    rComoPaga = nGastos.BuscarComoPaga(oComoPaga);
                    if (rComoPaga.ResultadoGeneral)
                    {
                        lstComoPaga = rComoPaga.Lista;
                    }
                    rQuienPaga = nGastos.BuscarQuienPaga(oQuienPaga);
                    if (rQuienPaga.ResultadoGeneral)
                    {
                        lstQuienPaga = rQuienPaga.Lista;
                    }

                    if (rGastos.Lista.Count > 0)
                    {
                        Controles.CargarGrid<GastoOperacionalInfo>(ref gvGastosOperacionales, rGastos.Lista, new string[] { "Id", "Moneda_Id", "TipoGastoOperacional_Id", "QuienPaga_Id", "ComoPaga_Id", "Valor", "IndProvisionSolicitada" });
                    }
                    else
                    {
                        ////Declaracion de Variables
                        var ObjInfo = new GastoOperacionalInfo();
                        var ObjResultado = new Resultado<GastoOperacionalInfo>();
                        var objNegocio = new NegGastosOperacionales();


                        ObjInfo.ValorPropiedad = NegSolicitudes.objSolicitudInfo.MontoPropiedad;
                        ObjInfo.MontoCredito = NegSolicitudes.objSolicitudInfo.MontoCredito;
                        ObjInfo.IndDfl2 = NegSolicitudes.objSolicitudInfo.IndDfl2;
                        ObjInfo.ViviendaSocial = NegSolicitudes.objSolicitudInfo.IndViviendaSocial;
                        ObjInfo.IndSimulacion = false;
                        ObjInfo.Destino_Id = NegSolicitudes.objSolicitudInfo.Destino_Id;

                        ////Asignacion de Variables
                        ObjResultado = objNegocio.CalcularGastosSimulacion(ObjInfo);
                        if (ObjResultado.ResultadoGeneral)
                        {
                            Controles.CargarGrid(ref gvGastosOperacionales, ObjResultado.Lista, new string[] { "Id", "Moneda_Id", "TipoGastoOperacional_Id", "QuienPaga_Id", "ComoPaga_Id", "Valor", "IndProvisionSolicitada" });
                            NegGastosOperacionales.lstGastosOperacionales = new List<GastoOperacionalInfo>();
                            NegGastosOperacionales.lstGastosOperacionales = ObjResultado.Lista;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ObjResultado.Mensaje);
                        }



                    }
                    if (gvGastosOperacionales.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvGastosOperacionales.Rows)
                        {

                            Anthem.DropDownList ddlQuienPaga = (Anthem.DropDownList)row.FindControl("ddlQuienPaga");
                            Anthem.DropDownList ddlComoPaga = (Anthem.DropDownList)row.FindControl("ddlComoPaga");
                            Anthem.TextBox txtValorUF = (Anthem.TextBox)row.FindControl("txtValorUF");
                            Anthem.CheckBox chkProvisionSolicitada = (Anthem.CheckBox)row.FindControl("chkProvisionSolicitada");



                            Controles.CargarCombo<QuienPagaGGOOInfo>(ref ddlQuienPaga, lstQuienPaga, "Id", "Descripcion", "-- Quién Paga? --", "-1");
                            Controles.CargarCombo<ComoPagaGGOOInfo>(ref ddlComoPaga, lstComoPaga, "Id", "Descripcion", "-- Cómo Paga? --", "-1");

                            ddlQuienPaga.SelectedValue = gvGastosOperacionales.DataKeys[row.RowIndex].Values["QuienPaga_Id"].ToString();
                            ddlComoPaga.SelectedValue = gvGastosOperacionales.DataKeys[row.RowIndex].Values["ComoPaga_Id"].ToString();
                            chkProvisionSolicitada.Checked = bool.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["IndProvisionSolicitada"].ToString());
                            if (chkProvisionSolicitada.Checked)
                            {
                                chkProvisionSolicitada.Enabled = false;
                            }

                            int Moneda_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Moneda_Id"].ToString());
                            txtValorUF.Attributes.Remove("onKeyPress");
                            if (Moneda_Id == 998)
                            {
                                txtValorUF.Attributes.Add("onKeyPress", "return SoloNumeros(event,this.value,4,this);");
                                txtValorUF.Text = string.Format("{0:F4}", decimal.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Valor"].ToString()));
                            }
                            else
                            {
                                txtValorUF.Attributes.Add("onKeyPress", "return SoloNumeros(event,this.value,0,this);");
                                txtValorUF.Text = string.Format("{0:F0}", decimal.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["Valor"].ToString()));
                            }

                        }
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
                    Controles.MostrarMensajeError("Error al Cargar los Gastos Operacionales");
                }
            }
        }
        private void GrabarGastosOperacionales(bool CargaInicial = false, bool ConValor0 = false, GridViewRow row = null)
        {
            try
            {
                Anthem.CheckBox chkProvisionSolicitada = new Anthem.CheckBox();

                
                if (row != null)
                {
                    chkProvisionSolicitada = (Anthem.CheckBox)row.FindControl("chkProvisionSolicitada");
                    if (!chkProvisionSolicitada.Enabled)
                    {
                        Controles.MostrarMensajeAlerta("Provisión ya Solicitada al Sistema de Adminsitración de GGOO, no se puede modificar.");
                        return;
                    }
                }
               
                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                GastoOperacionalInfo oGastos = new GastoOperacionalInfo();
                Resultado<GastoOperacionalInfo> rGastos = new Resultado<GastoOperacionalInfo>();


                Anthem.DropDownList ddlQuienPaga = (Anthem.DropDownList)row.FindControl("ddlQuienPaga");
                Anthem.DropDownList ddlComoPaga = (Anthem.DropDownList)row.FindControl("ddlComoPaga");
                Anthem.TextBox txtValorUF = (Anthem.TextBox)row.FindControl("txtValorUF");



                if (ddlQuienPaga.SelectedValue == "-1" && CargaInicial == false)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar Quien Paga");
                    return;
                }
                if (ddlComoPaga.SelectedValue == "-1" && CargaInicial == false)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar Como Paga");
                    return;
                }

                oGastos = NegGastosOperacionales.lstGastosOperacionales.FirstOrDefault(go => go.TipoGastoOperacional_Id == int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["TipoGastoOperacional_Id"].ToString()));


                if (txtValorUF.Text == "")
                {
                    txtValorUF.Text = "0";
                }
                oGastos.TipoGastoOperacional_Id = int.Parse(gvGastosOperacionales.DataKeys[row.RowIndex].Values["TipoGastoOperacional_Id"].ToString());
                oGastos.QuienPaga_Id = int.Parse(ddlQuienPaga.SelectedValue);
                oGastos.ComoPaga_Id = int.Parse(ddlComoPaga.SelectedValue);
                oGastos.Valor = decimal.Parse(txtValorUF.Text);
                oGastos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oGastos.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADO_GASTOS_OPERACIONALES", "I");
                oGastos.IndProvisionSolicitada = chkProvisionSolicitada.Checked;
                rGastos = nGastos.GuardarGasto(oGastos);
                if (!rGastos.ResultadoGeneral)
                {

                    Controles.MostrarMensajeError(rGastos.Mensaje);
                    return;
                }
                else
                {
                    if (!SolicitarProvision(row)) return;
                    if (CargaInicial == false)
                        Controles.MostrarMensajeExito("Gastos Operacionales Actualizados Correctamente");

                    if (CargaInicial == false)
                        CargarGastosOperacionales();
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
                    Controles.MostrarMensajeError("Error al Grabar Gasto Operacional");
                }
            }
        }
        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            GrabarGastosOperacionales(row: row);
        }
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GrabarGastosOperacionales();
        }
        private bool ValidarGastosOperacionales()
        {
            try
            {

                if (NegGastosOperacionales.lstGastosOperacionales.Count(go => go.QuienPaga_Id == 0) > 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Indicar quien Provisiona para todos los Gastos Operacionales");
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
                    Controles.MostrarMensajeError("Error al Validar Gasto Operacional");
                }
                return false;
            }
        }

        //Documentos

        private void GenerarOrdenEscrituracionPDF()
        {
            try
            {
                if (!NegSolicitudes.RecalcularDividendo()) return;
                if (NegSolicitudes.objSolicitudInfo.FechaProtocolizacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe gestionar la Fecha de Protocolización de la Serie del Crédito ");
                    return;
                }
                if (NegSolicitudes.objSolicitudInfo.NotariaProtocolizacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe gestionar la Notaría en donde se realizó la protocolización de la Serie del Crédito ");
                    return;
                }
                if (NegSolicitudes.objSolicitudInfo.RepertorioProtocolizacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe gestionar el Repertorio de la Serie del Crédito ");
                    return;
                }


                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                string urlDocumento = "~/Reportes/PDF/OrdenEscrituracion.aspx";
                string ContenidoHtml = "";
                string Archivo = "OrdenEscrituracion_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Documentacion Requerida Evaluación";

                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = ContenidoHtml + htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);

                Session["DocumentoOrden"] = Binarios;
                Response.AddHeader("Content-Type", "application/pdf");
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + Archivo + "; size={0}", Binarios.Length.ToString()));
                Response.BinaryWrite(Binarios);
                Response.End();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar el Reporte PDF");
                }
            }
        }


        private void GenerarHojaResumenPDF()
        {
            try
            {
                if (!NegSolicitudes.RecalcularDividendo()) return;
                if (NegSolicitudes.objSolicitudInfo.FechaProtocolizacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe gestionar la Fecha de Protocolización de la Serie del Crédito ");
                    return;
                }
                if (NegSolicitudes.objSolicitudInfo.NotariaProtocolizacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe gestionar la Notaría en donde se realizó la protocolización de la Serie del Crédito ");
                    return;
                }
                if (NegSolicitudes.objSolicitudInfo.RepertorioProtocolizacion == null)
                {
                    Controles.MostrarMensajeAlerta("Debe gestionar el Repertorio de la Serie del Crédito ");
                    return;
                }

                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;

                string urlDocumento = "~/Reportes/PDF/HojaResumen.aspx";
                string ContenidoHtml = "";
                string Archivo = "HojaResumen" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf";
                Pdf.NombreDocumentoPDF = Archivo;
                Pdf.ModuloDocumentoPDF = "Documentacion Requerida Evaluación";

                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlDocumento, htmlStringWriter);
                ContenidoHtml = ContenidoHtml + htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();

                byte[] Binarios = Pdf.GenerarPDF(ContenidoHtml, Archivo);

                Session["DocumentoHojaResumen"] = Binarios;
                Response.AddHeader("Content-Type", "application/pdf");
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + Archivo + "; size={0}", Binarios.Length.ToString()));
                Response.BinaryWrite(Binarios);
                Response.End();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar el Reporte PDF");
                }
            }
        }

        protected void btnImprimirOrden_Click(object sender, EventArgs e)
        {
            GenerarOrdenEscrituracionPDF();
        }

        protected void btnImprimirHojaResumen_Click(object sender, EventArgs e)
        {
            GenerarHojaResumenPDF();
        }

        protected void ddlEstadoEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gdvControlEventos.DataKeys[row.RowIndex].Values["Id"].ToString());
            ActualizarEvento(Id, int.Parse(((DropDownList)sender).SelectedValue));
        }

        private void ActualizarEvento(int Id, int Estado_Id)
        {
            try
            {
                BandejaEntradaInfo oInfo = new BandejaEntradaInfo();
                NegBandejaEntrada oNeg = new NegBandejaEntrada();
                Resultado<BandejaEntradaInfo> oResultado = new Resultado<BandejaEntradaInfo>();

                oInfo.Id = Id;
                oInfo.Estado_Id = Estado_Id;

                oResultado = oNeg.Guardar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Estado del Evento Actualizado");
                    CargarControlEventos();
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnActualizarEjecutivoComercial_Click(object sender, EventArgs e)
        {
            if (AddlEjecutivoComercial.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar un Ejecutivo Comercial");
                return;
            }

            NegSolicitudes oNegocio = new NegSolicitudes();
            AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
            Resultado<AsignacionSolicitudInfo> rAsignacion = new Resultado<AsignacionSolicitudInfo>();
            oAsignacion.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            oAsignacion.Usuario_Id = NegUsuarios.Usuario.Rut;


            //Asinacion de EjecutivoComercial
            oAsignacion.Rol_Id = Constantes.IdRolEjecutivoComercial;
            oAsignacion.Responsable_Id = int.Parse(AddlEjecutivoComercial.SelectedValue);
            rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
            if (rAsignacion.ResultadoGeneral == false)
                Controles.MostrarMensajeError(rAsignacion.Mensaje);
            else
            {

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
                        ObjInfo.EjecutivoComercial_Id = int.Parse(AddlEjecutivoComercial.SelectedValue);
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                    return;
                }

                ////Asignacion de Variables
                oResultado = oNegocio.Guardar(ref ObjInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Asignación Actualizada");
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                    return;
                }




            }
        }

        protected void btnActualizarJefeProducto_Click(object sender, EventArgs e)
        {

            if (AddlJefeProducto.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar un Jefe de Producto");
                return;
            }

            NegSolicitudes oNegocio = new NegSolicitudes();
            AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
            Resultado<AsignacionSolicitudInfo> rAsignacion = new Resultado<AsignacionSolicitudInfo>();
            oAsignacion.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            oAsignacion.Usuario_Id = NegUsuarios.Usuario.Rut;

            //Asinacion de Jefe de Producto
            oAsignacion.Rol_Id = Constantes.IdRolJefeProducto;
            oAsignacion.Responsable_Id = int.Parse(AddlJefeProducto.SelectedValue);
            rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
            if (rAsignacion.ResultadoGeneral == false)
                Controles.MostrarMensajeError(rAsignacion.Mensaje);
            else
                Controles.MostrarMensajeExito("Asignación Actualizada");
        }

        protected void btnActualizarVisador_Click(object sender, EventArgs e)
        {

            if (AddlVisador.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar un Visador");
                return;
            }

            NegSolicitudes oNegocio = new NegSolicitudes();
            AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
            Resultado<AsignacionSolicitudInfo> rAsignacion = new Resultado<AsignacionSolicitudInfo>();
            oAsignacion.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
            oAsignacion.Usuario_Id = NegUsuarios.Usuario.Rut;


            //Asinacion de Visador
            oAsignacion.Rol_Id = Constantes.IdRolVisador;
            oAsignacion.Responsable_Id = int.Parse(AddlVisador.SelectedValue);
            rAsignacion = oNegocio.GuardarAsignacion(oAsignacion);
            if (rAsignacion.ResultadoGeneral == false)
                Controles.MostrarMensajeError(rAsignacion.Mensaje);
            else
                Controles.MostrarMensajeExito("Asignación Actualizada");
        }
    }
}