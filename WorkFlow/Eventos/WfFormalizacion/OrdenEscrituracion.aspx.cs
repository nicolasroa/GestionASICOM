using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Eventos.WfFormalizacion
{
    public partial class OrdenEscrituracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {

                Session["DocumentoOrden"] = null;
                Session["DocumentoHojaResumen"] = null;
                Controles.EjecutarJavaScript("DesplegarDatosParticipante('0');");
                Controles.EjecutarJavaScript("DesplegarDatosPropiedad('0');");



                CargarAcciones();
                CargarSegurosContratados();
                CargarPropiedades();
                CargarParticipantes();
                CargaSegCesantia();
                CargaSegDesgravamen();
                CargarParticipacion();
                CargaSegIncendio();
                CargaComboAbogados(NegSolicitudes.objSolicitudInfo.FabricaAbogado_Id);
                CargaEventoDesembolso();
                CargarNotarias();
                CargarMesEscritura();



                //Solicitud
                CargarComboBeneficiarioTributario();
                CargaComboObjetivo();
                CargarFinalidadCredito();
                CargarUtilidadCredito();

                CargarInformacionSolicitud();


                //Destino de Fondos
                CargarDestinoFondo();
                CargarTipoDestinoFondo();

                // Subsidio
                CargarMesSubsidio();
                CargarAñoSubsidio();
                CargarInfoSubsidio(NegSolicitudes.objSolicitudInfo);

                //Actividades SII
                CargarCategoriasSii();
                lblNombreDeudor.Text = NegSolicitudes.objSolicitudInfo.NombreCliente;


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
                    Controles.CargarCombo(ref ddlAccionEvento, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Acciones--", "-1");
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

                    if (!ActualizarDatos()) return;
                    if (Session["DocumentoOrden"] == null)
                    {
                        Controles.MostrarMensajeAlerta("Debe Generar la Orden de Escrituración antes de Avanzar el Evento");
                        return;
                    }
                    if (Session["DocumentoHojaResumen"] == null)
                    {
                        Controles.MostrarMensajeAlerta("Debe Generar la Hoja Resumen antes de Avanzar el Evento");
                        return;
                    }
                    string resultadoDocumento = "";
                    resultadoDocumento = NegArchivosRepositorios.GuardarDocumentoSolicitud(NegSolicitudes.objSolicitudInfo.Id, 1, 85, "OrdenEscrituracion_" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf", (byte[])Session["DocumentoOrden"]);
                    if (resultadoDocumento != "")
                    {
                        Controles.MostrarMensajeAlerta(resultadoDocumento);
                        return;
                    }
                    resultadoDocumento = NegArchivosRepositorios.GuardarDocumentoSolicitud(NegSolicitudes.objSolicitudInfo.Id, 1, 85, "HojaResumen" + NegSolicitudes.objSolicitudInfo.Id.ToString() + ".pdf", (byte[])Session["DocumentoHojaResumen"]);
                    if (resultadoDocumento != "")
                    {
                        Controles.MostrarMensajeAlerta(resultadoDocumento);
                        return;
                    }
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {

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



        private bool ActualizarDatos(bool Graba = true)
        {
            if (!ValidarObservaciones()) return false;
            if (!ValidarDPS()) return false;
            if (!ActualizarActividadesEconominasDeudor()) return false;
            if (!ActualizaSolicitud()) return false;

            if (Graba)
                Controles.MostrarMensajeExito("Datos Grabados Correctamente");
            return true;
        }

        #region Solicitud
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


        private void CargarComboBeneficiarioTributario()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oParticipante = new ParticipanteInfo();
                var NegParticipantes = new NegParticipante();
                var oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Where(p => p.PorcentajeDominio > 0).ToList().Count() > 1)
                        Controles.CargarCombo<ParticipanteInfo>(ref ddlBeneficiarioTributario, oResultado.Lista.Where(p => p.PorcentajeDominio > 0).ToList(), "Rut", "NombreCliente", "--Beneficiario Tributario--", "-1");
                    else
                        Controles.CargarCombo<ParticipanteInfo>(ref ddlBeneficiarioTributario, oResultado.Lista.Where(p => p.PorcentajeDominio > 0).ToList(), "Rut", "NombreCliente", "", "");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Objetivo");
                }
            }
        }


        protected void btnGrabarDatos_Click(object sender, EventArgs e)
        {
            ActualizarDatos();
        }
        #endregion


        #region Actividades Económinas


        protected void ddlCategoria1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoria1.SelectedValue == "-1")
                Controles.CargarCombo<SubCategoriaSiiInfo>(ref ddlSubCategoria1, null, "Id", "Descripcion", "-- Sub Categoría --", "0");
            else
                CargarSubCategoriasSii(int.Parse(ddlCategoria1.SelectedValue), ref ddlSubCategoria1);
        }

        protected void ddlSubCategoria1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubCategoria1.SelectedValue == "-1")
                Controles.CargarCombo<SubCategoriaSiiInfo>(ref ddlActividad1, null, "Id", "Descripcion", "-- Actividad --", "0");
            else
                CargarActividadesSii(int.Parse(ddlSubCategoria1.SelectedValue), ref ddlActividad1);
        }

        protected void ddlCategoria2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoria2.SelectedValue == "-1")
                Controles.CargarCombo<SubCategoriaSiiInfo>(ref ddlSubCategoria2, null, "Id", "Descripcion", "-- Sub Categoría --", "0");
            else
                CargarSubCategoriasSii(int.Parse(ddlCategoria2.SelectedValue), ref ddlSubCategoria2);
        }

        protected void ddlSubCategoria2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubCategoria2.SelectedValue == "-1")
                Controles.CargarCombo<SubCategoriaSiiInfo>(ref ddlActividad2, null, "Id", "Descripcion", "-- Actividad --", "0");
            else
                CargarActividadesSii(int.Parse(ddlSubCategoria2.SelectedValue), ref ddlActividad2);
        }

        protected void ddlCategoria3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoria3.SelectedValue == "-1")
                Controles.CargarCombo<SubCategoriaSiiInfo>(ref ddlSubCategoria3, null, "Id", "Descripcion", "-- Sub Categoría --", "0");
            else
                CargarSubCategoriasSii(int.Parse(ddlCategoria3.SelectedValue), ref ddlSubCategoria3);
        }

        protected void ddlSubCategoria3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubCategoria3.SelectedValue == "-1")
                Controles.CargarCombo<SubCategoriaSiiInfo>(ref ddlActividad3, null, "Id", "Descripcion", "-- Actividad --", "0");
            else
                CargarActividadesSii(int.Parse(ddlSubCategoria3.SelectedValue), ref ddlActividad3);
        }



        private void CargarCategoriasSii()
        {
            try
            {
                CategoriaSiiInfo oCategoria = new CategoriaSiiInfo();
                NegClientes nClientes = new NegClientes();
                Resultado<CategoriaSiiInfo> rCategorias = new Resultado<CategoriaSiiInfo>();

                rCategorias = nClientes.BuscarCategoriaSii(oCategoria);

                if (rCategorias.ResultadoGeneral)
                {
                    Controles.CargarCombo<CategoriaSiiInfo>(ref ddlCategoria1, rCategorias.Lista, "Id", "Descripcion", "-- Categorias --", "0");
                    Controles.CargarCombo<CategoriaSiiInfo>(ref ddlCategoria2, rCategorias.Lista, "Id", "Descripcion", "-- Categorias --", "0");
                    Controles.CargarCombo<CategoriaSiiInfo>(ref ddlCategoria3, rCategorias.Lista, "Id", "Descripcion", "-- Categorias --", "0");
                }
                else
                {
                    Controles.MostrarMensajeError(rCategorias.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Categorías SII");
                }
            }
        }
        private void CargarSubCategoriasSii(int Categoria_Id, ref Anthem.DropDownList SubCategoria)
        {
            try
            {
                SubCategoriaSiiInfo oSubCategoria = new SubCategoriaSiiInfo();
                NegClientes nClientes = new NegClientes();
                Resultado<SubCategoriaSiiInfo> rSubCategoria = new Resultado<SubCategoriaSiiInfo>();

                oSubCategoria.Categoria_Id = Categoria_Id;
                rSubCategoria = nClientes.BuscarSubCategoriaSii(oSubCategoria);

                if (rSubCategoria.ResultadoGeneral)
                {
                    Controles.CargarCombo<SubCategoriaSiiInfo>(ref SubCategoria, rSubCategoria.Lista, "Id", "Descripcion", "-- Seleccione Categorias --", "0");
                }
                else
                {
                    Controles.MostrarMensajeError(rSubCategoria.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Sub Categorías SII");
                }
            }
        }
        private void CargarActividadesSii(int SubCategoria_Id, ref Anthem.DropDownList Actividad)
        {
            try
            {
                ActividadSiiInfo oActividad = new ActividadSiiInfo();
                NegClientes nClientes = new NegClientes();
                Resultado<ActividadSiiInfo> rActividad = new Resultado<ActividadSiiInfo>();

                oActividad.SubCategoria_Id = SubCategoria_Id;
                rActividad = nClientes.BuscarActividadSii(oActividad);

                if (rActividad.ResultadoGeneral)
                {
                    Controles.CargarCombo<ActividadSiiInfo>(ref Actividad, rActividad.Lista, "Id", "Descripcion", "-- Seleccione Sub Categoria --", "0");
                }
                else
                {
                    Controles.MostrarMensajeError(rActividad.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Actividades SII");
                }
            }
        }



        private void CargarActividadesEconomicasDeudor(int RutDeudor)
        {
            try
            {
                NegClientes nCliente = new NegClientes();
                ClientesInfo oCliente = new ClientesInfo();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();

                oCliente.Rut = RutDeudor;
                rCliente = nCliente.Buscar(oCliente);
                if (rCliente.ResultadoGeneral)
                {
                    oCliente = rCliente.Lista.FirstOrDefault();
                    if (oCliente != null)
                    {
                        if (oCliente.CategoriaSii1_Id > 0)
                            ddlCategoria1.SelectedValue = oCliente.CategoriaSii1_Id.ToString();
                        if (oCliente.SubCategoriaSii1_Id > 0)
                        {
                            CargarSubCategoriasSii(oCliente.CategoriaSii1_Id, ref ddlSubCategoria1);
                            ddlSubCategoria1.SelectedValue = oCliente.SubCategoriaSii1_Id.ToString();
                        }
                        if (oCliente.ActividadSii1_Id > 0)
                        {
                            CargarActividadesSii(oCliente.SubCategoriaSii1_Id, ref ddlActividad1);
                            ddlActividad1.SelectedValue = oCliente.ActividadSii1_Id.ToString();
                        }

                        if (oCliente.CategoriaSii2_Id > 0)
                            ddlCategoria2.SelectedValue = oCliente.CategoriaSii2_Id.ToString();
                        if (oCliente.SubCategoriaSii2_Id > 0)
                        {
                            CargarSubCategoriasSii(oCliente.CategoriaSii2_Id, ref ddlSubCategoria2);
                            ddlSubCategoria2.SelectedValue = oCliente.SubCategoriaSii2_Id.ToString();
                        }
                        if (oCliente.ActividadSii2_Id > 0)
                        {
                            CargarActividadesSii(oCliente.SubCategoriaSii2_Id, ref ddlActividad2);
                            ddlActividad2.SelectedValue = oCliente.ActividadSii2_Id.ToString();
                        }

                        if (oCliente.CategoriaSii3_Id > 0)
                            ddlCategoria3.SelectedValue = oCliente.CategoriaSii3_Id.ToString();
                        if (oCliente.SubCategoriaSii3_Id > 0)
                        {
                            CargarSubCategoriasSii(oCliente.CategoriaSii3_Id, ref ddlSubCategoria3);
                            ddlSubCategoria3.SelectedValue = oCliente.SubCategoriaSii3_Id.ToString();
                        }
                        if (oCliente.ActividadSii1_Id > 0)
                        {
                            CargarActividadesSii(oCliente.SubCategoriaSii3_Id, ref ddlActividad3);
                            ddlActividad3.SelectedValue = oCliente.ActividadSii3_Id.ToString();
                        }
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rCliente.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Actividades SII del Deudor");
                }
            }
        }

        private bool ValidarActividadesEconomicasDeudor()
        {

            try
            {
                if (("456").Contains(NegSolicitudes.objSolicitudInfo.Finalidad_Id.ToString()))
                {
                    if (ddlActividad1.SelectedValue == "" || ddlActividad1.SelectedValue == "0")
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleciconar al Menos la 1° Actividad Económica del Deudor");
                        return false;
                    }

                    if ((ddlCategoria2.SelectedValue != "" && ddlCategoria2.SelectedValue != "0") && (ddlActividad2.SelectedValue == "" || ddlActividad2.SelectedValue == "0"))
                    {
                        Controles.MostrarMensajeAlerta("Debe Completar los Datos de la 2° Actividad Económica del Deudor");
                        return false;
                    }
                    if ((ddlCategoria3.SelectedValue != "" && ddlCategoria3.SelectedValue != "0") && (ddlActividad3.SelectedValue == "" || ddlActividad3.SelectedValue == "0"))
                    {
                        Controles.MostrarMensajeAlerta("Debe Completar los Datos de la 3° Actividad Económica del Deudor");
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
                    Controles.MostrarMensajeError("Error al Validar las Actividades SII del Deudor");
                }
                return false;
            }
        }

        private bool ActualizarActividadesEconominasDeudor()
        {
            try
            {
                NegClientes nCliente = new NegClientes();
                ClientesInfo oCliente = new ClientesInfo();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();

                oCliente.Rut = NegSolicitudes.objSolicitudInfo.Rut;
                rCliente = nCliente.Buscar(oCliente);
                if (rCliente.ResultadoGeneral)
                {
                    oCliente = rCliente.Lista.FirstOrDefault();
                    if (oCliente != null)
                    {
                        if (("456").Contains(NegSolicitudes.objSolicitudInfo.Finalidad_Id.ToString()))
                        {
                            if (!ValidarActividadesEconomicasDeudor()) return false;
                            oCliente.CategoriaSii1_Id = int.Parse(ddlCategoria1.SelectedValue);
                            oCliente.SubCategoriaSii1_Id = int.Parse(ddlSubCategoria1.SelectedValue);
                            oCliente.ActividadSii1_Id = int.Parse(ddlActividad1.SelectedValue);
                            oCliente.CategoriaSii2_Id = int.Parse(ddlCategoria2.SelectedValue);
                            oCliente.SubCategoriaSii2_Id = ddlSubCategoria2.SelectedValue == "" ? -1 : int.Parse(ddlSubCategoria2.SelectedValue);
                            oCliente.ActividadSii2_Id = ddlActividad2.SelectedValue == "" ? -1 : int.Parse(ddlActividad2.SelectedValue);
                            oCliente.CategoriaSii3_Id = int.Parse(ddlCategoria3.SelectedValue);
                            oCliente.SubCategoriaSii3_Id = ddlSubCategoria3.SelectedValue == "" ? -1 : int.Parse(ddlSubCategoria3.SelectedValue);
                            oCliente.ActividadSii3_Id = ddlActividad3.SelectedValue == "" ? -1 : int.Parse(ddlActividad3.SelectedValue);
                        }
                        else
                        {
                            oCliente.CategoriaSii1_Id = 0;
                            oCliente.SubCategoriaSii1_Id = 0;
                            oCliente.ActividadSii1_Id = 0;
                            oCliente.CategoriaSii2_Id = 0;
                            oCliente.SubCategoriaSii2_Id = 0;
                            oCliente.ActividadSii2_Id = 0;
                            oCliente.CategoriaSii3_Id = 0;
                            oCliente.SubCategoriaSii3_Id = 0;
                            oCliente.ActividadSii3_Id = 0;
                        }
                        rCliente = nCliente.Guardar(oCliente);
                        if (!rCliente.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rCliente.Mensaje);
                            return false;
                        }
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(rCliente.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Validar las Actividades SII del Deudor");
                }
                return false;
            }
        }


        #endregion


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
        private bool ValidarDatosSolicitud()
        {
            try
            {

                if (NegPropiedades.lstTasaciones.Where(t => t.IndAlzamientoHipoteca == true).Count() > 0)
                {
                    if (chkIndCartaResguardo.Checked && chkIndInstruccionNotarial.Checked)
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar una opción para el Flujo de Alzamiento de Hipoteca");
                        return false;
                    }
                }

                if (ddlAbogado.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Abogado");
                    return false;
                }
                if (ddlEventoDesembolso.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Evento de Desembolso");
                    return false;
                }
                if (ddlNotaria.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Notaría");
                    return false;
                }
                if (ddlMesEscrituracion.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Mes de Escrituración");
                    return false;
                }


                if (NegSolicitudes.objSolicitudInfo.Subsidio_Id > 0)
                {

                    if (txtAhorroPrevioSubsidio.Text == "")
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar el Ahorro Previo del Subsidio");
                        return false;
                    }

                    if (txtSerieSubsidio.Text == "")
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar el Número de Serie del Subsidio");
                        return false;
                    }
                    if (txtNumeroCertificadoSubsidio.Text == "")
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar el Número de Certificado del Subsidio");
                        return false;
                    }

                    if (ddlMesCertificadoSubsidio.SelectedValue == "-1")
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar un Mes del Certificado de Subsidio");
                        return false;
                    }
                    if (ddlMesEscrituracion.SelectedValue == "-1")
                    {
                        Controles.MostrarMensajeAlerta("Debe Seleccionar un Año del Certificado de Subsidio");
                        return false;
                    }
                    if (txtNumeroLibretaSubsidio.Text == "")
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar el Número de Libreta de Ahorro del Subsidio");
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
                    Controles.MostrarMensajeError("Error al Validar las Observaciones");
                }
            }
            return false;
        }
        private bool ValidarDPS()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                NegParticipante NegParticipantes = new NegParticipante();
                Resultado<ParticipanteInfo> oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    if (oResultado.Lista.Where(p => p.EstadoDps_Id != 3 && p.PorcentajeDesgravamen > 0).Count() > 0)
                    {
                        var Participante = new ParticipanteInfo();
                        Participante = oResultado.Lista.FirstOrDefault(p => p.EstadoDps_Id != 3 && p.PorcentajeDesgravamen > 0);

                        Controles.MostrarMensajeAlerta("Falta Aprobar la DPS del Participante " + Participante.NombreCliente);
                        return false;
                    }
                }
                else
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Validar el Evento");
                }
                return false;
            }
        }
        private bool ActualizaSolicitud()
        {

            try
            {
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                NegSolicitudes nSolicitud = new NegSolicitudes();
                SolicitudInfo oSolicitud = new SolicitudInfo();
                if (!ValidarDatosSolicitud()) return false;
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

                var MesEscritura = ddlMesEscrituracion.SelectedValue.Split('-');

                NegSolicitudes.objSolicitudInfo.Abogado_Id = int.Parse(ddlAbogado.SelectedValue);
                NegSolicitudes.objSolicitudInfo.EventoDesembolso_Id = int.Parse(ddlEventoDesembolso.SelectedValue);
                NegSolicitudes.objSolicitudInfo.Notaria_Id = int.Parse(ddlNotaria.SelectedValue);
                NegSolicitudes.objSolicitudInfo.MesEscritura = int.Parse(MesEscritura[0]);
                NegSolicitudes.objSolicitudInfo.AñoEscritura = int.Parse(MesEscritura[1]);
                NegSolicitudes.objSolicitudInfo.GenerarNumeroOperacion = true;
                NegSolicitudes.objSolicitudInfo.GenerarSerie = true;
                NegSolicitudes.objSolicitudInfo.IndCartaResguardo = chkIndCartaResguardo.Checked;
                NegSolicitudes.objSolicitudInfo.IndInstruccionNotarial = chkIndInstruccionNotarial.Checked;
                NegSolicitudes.objSolicitudInfo.BeneficiarioTributario_Id = int.Parse(ddlBeneficiarioTributario.SelectedValue);

                if (rblArt150.SelectedValue != "")
                {
                    NegSolicitudes.objSolicitudInfo.IndArticulo150 = bool.Parse(rblArt150.SelectedValue);
                }

                //Subsidio
                if (NegSolicitudes.objSolicitudInfo.Subsidio_Id > 0)
                {
                    NegSolicitudes.objSolicitudInfo.AhorroPrevioSubsidio = decimal.Parse(txtAhorroPrevioSubsidio.Text);
                    NegSolicitudes.objSolicitudInfo.NumeroSerieSubsidio = txtSerieSubsidio.Text;
                    NegSolicitudes.objSolicitudInfo.NumeroCertificadoSubsidio = txtNumeroCertificadoSubsidio.Text;
                    NegSolicitudes.objSolicitudInfo.MesCertificadoSubsidio = int.Parse(ddlMesCertificadoSubsidio.SelectedValue);
                    NegSolicitudes.objSolicitudInfo.AñoCertificadoSubsidio = int.Parse(ddlAñoCertificadoSubsidio.SelectedValue);
                    NegSolicitudes.objSolicitudInfo.NumeroLibretaSubsidio = txtNumeroLibretaSubsidio.Text;
                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.AhorroPrevioSubsidio = -1;
                    NegSolicitudes.objSolicitudInfo.NumeroSerieSubsidio = "";
                    NegSolicitudes.objSolicitudInfo.NumeroCertificadoSubsidio = "";
                    NegSolicitudes.objSolicitudInfo.MesCertificadoSubsidio = -1;
                    NegSolicitudes.objSolicitudInfo.AñoCertificadoSubsidio = -1;
                    NegSolicitudes.objSolicitudInfo.NumeroLibretaSubsidio = "";
                }

                if (NegSolicitudes.objSolicitudInfo.NumeroOperacion.Length > 0)
                {
                    string NroOperacion = NegSolicitudes.objSolicitudInfo.NumeroOperacion;

                    var Mes = MesEscritura[0];
                    var Año = MesEscritura[1];

                    if (int.Parse(Mes) < 10)
                        Mes = "0" + Mes;

                    //2019070010A034
                    var AñoOpe = NroOperacion.Substring(0, 4);
                    var MesOpe = NroOperacion.Substring(4, 2);

                    NroOperacion = Año + Mes + NroOperacion.Substring(6, NroOperacion.Length - 6);
                    NegSolicitudes.objSolicitudInfo.NumeroOperacion = NroOperacion;

                }




                decimal DividendoRenta = decimal.Zero;
                ResumenResolucion.CargarRenta(ref DividendoRenta);
                NegSolicitudes.objSolicitudInfo.DividendoRenta = DividendoRenta;

                oSolicitud = NegSolicitudes.objSolicitudInfo;

                rSolicitud = nSolicitud.Guardar(ref oSolicitud);
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
                    Controles.MostrarMensajeError("Error al Actualizar la Solicitud");
                }
            }
            return false;
        }
        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
        private void GenerarOrdenEscrituracionPDF()
        {
            try
            {
                if (!NegSolicitudes.RecalcularDividendo()) return;
                if (!ActualizaSolicitud()) return;
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
                if (!ActualizaSolicitud()) return;
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


        private void CargarSegurosContratados()
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();


                oSeguros.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rSeguros = nSeguros.BuscarSegurosContratados(oSeguros);
                if (rSeguros.ResultadoGeneral)
                    Controles.CargarGrid<SegurosContratadosInfo>(ref gvSegurosContratados, rSeguros.Lista, new string[] { "Id" });
                else
                    Controles.MostrarMensajeError(rSeguros.Mensaje);


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Seguros");
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
                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvPropiedades, rTasacion.Lista, new string[] { "Id", "Propiedad_Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(rTasacion.Mensaje);
                }

                if (rTasacion.Lista.Where(t => t.IndAlzamientoHipoteca == true).Count() > 0)
                {
                    chkIndCartaResguardo.Enabled = true;
                    chkIndInstruccionNotarial.Enabled = true;
                }
                else
                {
                    chkIndCartaResguardo.Enabled = false;
                    chkIndInstruccionNotarial.Enabled = false;
                    chkIndInstruccionNotarial.Checked = false;
                    chkIndCartaResguardo.Checked = false;
                }


                if (rTasacion.Lista.Count(t => t.IndPropiedadPrincipal == true) > 1)
                    hdfSeleccionUnicaHipoteca.Value = "false";
                else
                    hdfSeleccionUnicaHipoteca.Value = "true";

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
        public void CargarParticipantes()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oParticipante = new ParticipanteInfo();
                var NegParticipantes = new NegParticipante();
                var oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ParticipanteInfo>(ref gvParticipantes, oResultado.Lista, new string[] { Constantes.StringId, "Rut", "PorcentajeDesgravamen", "FechaIngresoDPS", "FechaAprobacionDPS" });

                    foreach (GridViewRow row in gvParticipantes.Rows)
                    {
                        Anthem.Label lblFechaIngresoDPS = (Anthem.Label)row.FindControl("lblFechaIngresoDPS");
                        Anthem.Label lblFechaAprobacionDPS = (Anthem.Label)row.FindControl("lblFechaAprobacionDPS");

                        decimal PorcentajeDesgravamen = decimal.Parse(gvParticipantes.DataKeys[row.RowIndex].Values["PorcentajeDesgravamen"].ToString());

                        if (PorcentajeDesgravamen > 0)
                        {
                            lblFechaIngresoDPS.Text = gvParticipantes.DataKeys[row.RowIndex].Values["FechaIngresoDPS"] == null ? "Pendiente" : string.Format("{0:d}", gvParticipantes.DataKeys[row.RowIndex].Values["FechaIngresoDPS"].ToString());
                            lblFechaAprobacionDPS.Text = gvParticipantes.DataKeys[row.RowIndex].Values["FechaAprobacionDPS"] == null ? "Pendiente" : string.Format("{0:d}", gvParticipantes.DataKeys[row.RowIndex].Values["FechaAprobacionDPS"].ToString());

                        }
                        else
                        {
                            lblFechaIngresoDPS.Text = "No Aplica";
                            lblFechaAprobacionDPS.Text = "No Aplica";
                        }
                    }

                    //Participante Femenina, con regimen matrimonial sociedad Conyugal, Deudor principal o vendedor
                    if (NegParticipante.lstParticipantes.Count(p => p.RegimenMatrimonial_Id == 3 && p.Sexo_Id == 2 && (p.TipoParticipacion_Id == 1 || p.TipoParticipacion_Id == 4)) > 0)
                        rblArt150.Enabled = true;
                    else
                        rblArt150.Enabled = false;

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
        private void CargarParticipacion()
        {

            try
            {
                NegParticipante oNeg = new NegParticipante();
                TipoParticipanteInfo oFiltro = new TipoParticipanteInfo();
                Resultado<TipoParticipanteInfo> rParticipante = new Resultado<TipoParticipanteInfo>();
                oFiltro.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                rParticipante = oNeg.BuscarTipoParticipante(oFiltro);
                if (rParticipante.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoParticipanteInfo>(ref ddlTipoParticipacion, rParticipante.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Participación --", "-1");
                    txtPorcentajeDeuda.Text = "0";
                    txtPorcentajeDeuda.Enabled = false;
                    txtPorcentajeDominio.Text = "0";
                    txtPorcentajeDominio.Enabled = false;
                    txtPorcentajeDesgravamen.Text = "0";
                    txtPorcentajeDesgravamen.Enabled = false;
                    ddlSeguroDesgravamen.ClearSelection();
                    ddlSeguroDesgravamen.Enabled = false;
                    txtTasaSeguroDesgravamen.Text = "0";
                    txtPrimaSeguroDesgravamen.Text = "0";

                    ddlSeguroCesantia.ClearSelection();
                    ddlSeguroCesantia.Enabled = false;
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                }
                else
                {
                    Controles.MostrarMensajeError(rParticipante.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Tipo de Participantes");
                }
            }

        }
        private void CargaSegDesgravamen()
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
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroDesgravamen, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Seguro Desgravamen--", "-1");
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
        private void CargaSegCesantia()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                ////Asignacion de Variables
                ObjInfo.GrupoSeguro_Id = 3;
                ObjInfo.Subsidio_Id = NegSolicitudes.objSolicitudInfo.Subsidio_Id;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroCesantia, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Sin Seguro de Cesantía--", "-1");
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
        private void GrabarDatosGenerales()
        {
            try
            {
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                Resultado<ParticipanteInfo> rParticipante = new Resultado<ParticipanteInfo>();
                NegParticipante nParticipante = new NegParticipante();

                if (!ValidarFormularioDatosGenerales()) return;

                oParticipante.Id = NegParticipante.ObjParticipante.Id;
                oParticipante = DatosEntidadDatosGenerales(oParticipante);

                var PorcentajeDeuda = decimal.Zero;
                var PorcentajeDominio = decimal.Zero;
                var PorcentajeDesgravamen = decimal.Zero;
                var TasaSeguroDesgravamen = decimal.Zero;
                var PrimaSeguroDesgravamen = decimal.Zero;
                var TasaSeguroCesantia = decimal.Zero;
                var PrimaSeguroCesantia = decimal.Zero;


                if (!decimal.TryParse(txtPorcentajeDominio.Text, out PorcentajeDominio))
                    PorcentajeDeuda = 0;
                if (!decimal.TryParse(txtPorcentajeDominio.Text, out PorcentajeDeuda))
                    PorcentajeDominio = 0;
                if (!decimal.TryParse(txtPorcentajeDesgravamen.Text, out PorcentajeDesgravamen))
                    PorcentajeDesgravamen = 0;
                if (!decimal.TryParse(txtTasaSeguroDesgravamen.Text, out TasaSeguroDesgravamen))
                    TasaSeguroDesgravamen = 0;
                if (!decimal.TryParse(txtPrimaSeguroDesgravamen.Text, out PrimaSeguroDesgravamen))
                    PrimaSeguroDesgravamen = 0;
                if (!decimal.TryParse(txtTasaSeguroCesantia.Text, out TasaSeguroCesantia))
                    TasaSeguroCesantia = 0;
                if (!decimal.TryParse(txtPrimaSeguroCesantia.Text, out PrimaSeguroCesantia))
                    PrimaSeguroCesantia = 0;



                oParticipante.PorcentajeDeuda = PorcentajeDeuda;
                oParticipante.PorcentajeDominio = PorcentajeDominio;
                oParticipante.PorcentajeDesgravamen = PorcentajeDesgravamen;
                oParticipante.SeguroDesgravamen_Id = int.Parse(ddlSeguroDesgravamen.SelectedValue);
                oParticipante.TasaSeguroDesgravamen = TasaSeguroDesgravamen / 100;
                oParticipante.PrimaSeguroDesgravamen = PrimaSeguroDesgravamen;
                oParticipante.SeguroCesantia_Id = int.Parse(ddlSeguroCesantia.SelectedValue);
                oParticipante.TasaSeguroCesantia = TasaSeguroCesantia / 100;
                oParticipante.PrimaSeguroCesantia = PrimaSeguroCesantia;


                rParticipante = nParticipante.GuardarParticipante(ref oParticipante);

                if (rParticipante.ResultadoGeneral)
                {
                    NegParticipante.ObjParticipante = new ParticipanteInfo();
                    NegParticipante.ObjParticipante = oParticipante;
                    Controles.MostrarMensajeExito("Datos Guardados Correctamente");
                    ActualizarSegurosContratados(NegParticipante.ObjParticipante);
                    CargarSegurosContratados();
                    CargarParticipantes();
                    LimpiarDatosGenerales();
                    Controles.EjecutarJavaScript("DesplegarDatosParticipante('0');");
                }
                else
                    Controles.MostrarMensajeError(rParticipante.Mensaje);

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Grabar Datos Generales");
                }
            }
        }
        private void LlenarDatosGenerales(ParticipanteInfo oParticipante)
        {
            try
            {

                LimpiarDatosGenerales();
                NegParticipante.ObjParticipante = new ParticipanteInfo();
                NegParticipante.ObjParticipante = oParticipante;
                ddlTipoParticipacion.SelectedValue = oParticipante.TipoParticipacion_Id.ToString();
                SeleccionaTipoParticipacion();
                txtPorcentajeDeuda.Text = String.Format("{0:0.00}", oParticipante.PorcentajeDeuda);
                txtPorcentajeDominio.Text = String.Format("{0:0.00}", oParticipante.PorcentajeDominio);
                txtPorcentajeDesgravamen.Text = String.Format("{0:0.00}", oParticipante.PorcentajeDesgravamen);
                ddlSeguroDesgravamen.SelectedValue = oParticipante.SeguroDesgravamen_Id.ToString();
                ddlSeguroCesantia.SelectedValue = oParticipante.SeguroCesantia_Id.ToString();
                SeleccionarSeguroCesantia();
                SeleccionarSeguroDesgravamen();

                lblNombreParticipante.Text = oParticipante.NombreCliente;
                Controles.EjecutarJavaScript("DesplegarDatosParticipante('1');");
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Datos Generales");
                }
            }
        }
        private ParticipanteInfo DatosEntidadDatosGenerales(ParticipanteInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<ParticipanteInfo>();
                var oInfo = new ParticipanteInfo();
                var oNeg = new NegParticipante();

                oResultado = oNeg.BuscarParticipante(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oInfo = oResultado.Lista.FirstOrDefault();

                    if (oInfo != null)
                    {
                        NegParticipante.ObjParticipante = new ParticipanteInfo();
                        NegParticipante.ObjParticipante = oInfo;
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
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Participante");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Antecedentes Laborales");

                }
                return null;
            }
        }
        private void LimpiarDatosGenerales()
        {
            try
            {
                NegParticipante.ObjParticipante = null;
                ddlTipoParticipacion.ClearSelection();
                txtPorcentajeDeuda.Text = "";
                txtPorcentajeDominio.Text = "";
                txtPorcentajeDesgravamen.Text = "";
                ddlSeguroDesgravamen.ClearSelection();
                txtTasaSeguroDesgravamen.Text = "";
                txtPrimaSeguroDesgravamen.Text = "";
                ddlSeguroCesantia.ClearSelection();
                txtTasaSeguroCesantia.Text = "";
                txtPrimaSeguroCesantia.Text = "";
                NegParticipante.ObjParticipante = new ParticipanteInfo();
                Controles.EjecutarJavaScript("DesplegarDatosParticipante('0');");

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Limpiar Datos Generales");
                }
            }
        }
        private bool ValidarFormularioDatosGenerales()
        {
            try
            {
                if (ddlTipoParticipacion.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Participación");
                    return false;
                }
                if (txtPorcentajeDeuda.Enabled && txtPorcentajeDeuda.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Porcentaje de Deuda");
                    return false;
                }


                if (txtPorcentajeDominio.Enabled && txtPorcentajeDominio.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Porcentaje de Dominio");
                    return false;
                }

                if (txtPorcentajeDesgravamen.Enabled && txtPorcentajeDesgravamen.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Porcentaje de Desgravamen");
                    return false;
                }

                if (ddlSeguroDesgravamen.Enabled && ddlSeguroDesgravamen.SelectedValue == "-1" && txtTasaSeguroDesgravamen.Text.Length > 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Seguro de Desgravamen");
                    return false;
                }

                if (ddlSeguroCesantia.Enabled && ddlSeguroCesantia.SelectedValue == "-1")
                {
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                }
                if (NegSolicitudes.objSolicitudInfo.Subsidio_Id != -1 && ddlSeguroCesantia.Enabled && ddlSeguroCesantia.SelectedValue == "-1" && ddlTipoParticipacion.SelectedValue == "1")// Creditos con Subsidio y solo para el Solicitante Principal
                {
                    Controles.MostrarMensajeAlerta("El seguro de Cesantía es Obligatorio para los Créditos con Subsidio");
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
        protected void ddlTipoParticipacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionaTipoParticipacion();
        }
        private void SeleccionaTipoParticipacion()
        {
            try
            {
                if (ddlTipoParticipacion.SelectedValue == "-1")
                {
                    txtPorcentajeDeuda.Text = "0";
                    txtPorcentajeDeuda.Enabled = false;
                    txtPorcentajeDominio.Text = "0";
                    txtPorcentajeDominio.Enabled = false;
                    txtPorcentajeDesgravamen.Text = "0";
                    txtPorcentajeDesgravamen.Enabled = false;
                    ddlSeguroDesgravamen.ClearSelection();
                    ddlSeguroDesgravamen.Enabled = false;
                    txtTasaSeguroDesgravamen.Text = "0";
                    txtPrimaSeguroDesgravamen.Text = "0";

                    ddlSeguroCesantia.ClearSelection();
                    ddlSeguroCesantia.Enabled = false;
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                }
                else
                {
                    TipoParticipanteInfo oParticipacion = new TipoParticipanteInfo();
                    oParticipacion = NegParticipante.lstTipoParticipante.FirstOrDefault(tp => tp.Id == int.Parse(ddlTipoParticipacion.SelectedValue));
                    if (oParticipacion != null)
                    {
                        txtPorcentajeDeuda.Enabled = oParticipacion.IndDeudor == true ? true : false;
                        txtPorcentajeDominio.Enabled = oParticipacion.IndDominio == true ? true : false;
                        txtPorcentajeDesgravamen.Enabled = oParticipacion.IndDesgravamen == true ? true : false;
                        ddlSeguroDesgravamen.Enabled = oParticipacion.IndDesgravamen == true ? true : false;
                        ddlSeguroCesantia.Enabled = oParticipacion.IndCesantia == true ? true : false;
                    }

                    if (NegParticipante.ObjParticipante.TipoPersona_Id == 2)//Persona Juridica
                    {
                        txtPorcentajeDesgravamen.Text = "0";
                        txtPorcentajeDesgravamen.Enabled = false;
                        ddlSeguroDesgravamen.ClearSelection();
                        ddlSeguroDesgravamen.Enabled = false;
                        txtTasaSeguroDesgravamen.Text = "0";
                        txtPrimaSeguroDesgravamen.Text = "0";

                        ddlSeguroCesantia.ClearSelection();
                        ddlSeguroCesantia.Enabled = false;
                        txtTasaSeguroCesantia.Text = "0";
                        txtPrimaSeguroCesantia.Text = "0";
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
                    Controles.MostrarMensajeError("Error al Seleccionar Tipo de Participación");
                }
            }
        }
        protected void ddlSeguroDesgravamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSeguroDesgravamen();

        }
        protected void ddlSeguroCesantia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSeguroCesantia();
        }
        private void SeleccionarSeguroDesgravamen()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                if (ddlSeguroDesgravamen.SelectedValue == "-1")
                {
                    txtTasaSeguroDesgravamen.Text = "0";
                    txtPrimaSeguroDesgravamen.Text = "0";
                }
                else
                {
                    decimal PorcentajeSeguro = decimal.Zero;

                    if (!decimal.TryParse(txtPorcentajeDesgravamen.Text, out PorcentajeSeguro))
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar un % de Seguro de Desgravamen");
                        return;
                    }
                    if (PorcentajeSeguro < 0 || PorcentajeSeguro > 100)
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar un valor entre 0 y 100");
                        txtPorcentajeDesgravamen.Text = "0";
                        return;
                    }

                    ////Asignacion de Variables
                    ObjInfo.Id = int.Parse(ddlSeguroDesgravamen.SelectedValue);
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        PrimaSeguro = (ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.MontoCredito * PorcentajeSeguro) / 100;
                        txtPrimaSeguroDesgravamen.Text = string.Format("{0:0.00000000}", PrimaSeguro);
                        txtTasaSeguroDesgravamen.Text = string.Format("{0:0.00000000}", ObjInfo.TasaMensual * 100);
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
        private void SeleccionarSeguroCesantia()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                if (ddlSeguroCesantia.SelectedValue == "-1")
                {
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                }
                else
                {
                    ////Asignacion de Variables
                    ObjInfo.Id = int.Parse(ddlSeguroCesantia.SelectedValue);
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        if (ObjInfo.PorValorCuota)
                            PrimaSeguro = ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.ValorDividendoSinCesantiaUF;
                        else
                            PrimaSeguro = ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.MontoCredito;
                        txtPrimaSeguroCesantia.Text = String.Format("{0:0.00000000}", PrimaSeguro);
                        txtTasaSeguroCesantia.Text = String.Format("{0:0.00000000}", ObjInfo.TasaMensual * 100);
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
        protected void txtPorcentajeDesgravamen_TextChanged(object sender, EventArgs e)
        {

            SeleccionarSeguroDesgravamen();
        }
        protected void btnGuardarDatosGenerales_Click(object sender, EventArgs e)
        {
            GrabarDatosGenerales();
        }
        protected void btnCancelarDatosGenerales_Click(object sender, EventArgs e)
        {
            LimpiarDatosGenerales();
        }
        private void ActualizarSegurosContratados(ParticipanteInfo oParticipante)
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
                    if (oParticipante.MontoAseguradoDPS > 0)
                        oSegurosContratados.MontoAsegurado = oParticipante.MontoAseguradoDPS;
                    else
                        oSegurosContratados.MontoAsegurado = NegSolicitudes.objSolicitudInfo.MontoCredito * oParticipante.PorcentajeDesgravamen / 100;
                    oSegurosContratados.TasaMensual = oParticipante.TasaSeguroDesgravamen;
                    oSegurosContratados.PrimaMensual = oParticipante.PrimaSeguroDesgravamen;

                    rSeguros = nSeguros.GuardarSegurosContratados(oSegurosContratados);
                    if (rSeguros.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(rSeguros.Mensaje);
                        return;
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
                        return;
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
                    return;
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
        protected void btnModificarParticipante_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Rut = int.Parse(gvParticipantes.DataKeys[row.RowIndex].Values["Rut"].ToString());
            int Id = int.Parse(gvParticipantes.DataKeys[row.RowIndex].Values["Id"].ToString());
            ClientesInfo oCliente = new ClientesInfo();
            ParticipanteInfo oParticipante = new ParticipanteInfo();
            oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Id == Id);
            LlenarDatosGenerales(oParticipante);
        }
        protected void btnModificarProp_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvPropiedades.DataKeys[row.RowIndex].Values["Id"].ToString());
            TasacionInfo oTasacion = new TasacionInfo();
            oTasacion = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.Id == Id);
            CargarDatosTasacion(oTasacion);
            Controles.EjecutarJavaScript("DesplegarDatosPropiedad('1');");
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
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                if (ddlSeguroIncendio.SelectedValue == "-1")
                {
                    txtTasaSeguroIncendio.Text = "0";
                    txtPrimaSeguroIncendio.Text = "0";
                }
                else
                {
                    ////Asignacion de Variables
                    ObjInfo.Id = int.Parse(ddlSeguroIncendio.SelectedValue);
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        PrimaSeguro = (ObjInfo.TasaMensual * NegPropiedades.objTasacion.MontoAsegurado);

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



                oTasacion = NegPropiedades.objTasacion;
                oTasacion.Seguro_Id = int.Parse(ddlSeguroIncendio.SelectedValue);
                oTasacion.MontoAsegurado = txtMontoAseguradoIncendio.Text == "" ? 0 : decimal.Parse(txtMontoAseguradoIncendio.Text);
                oTasacion.TasaSeguro = txtTasaSeguroIncendio.Text == "" ? 0 : decimal.Parse(txtTasaSeguroIncendio.Text) / 100;
                oTasacion.PrimaSeguro = txtPrimaSeguroIncendio.Text == "" ? 0 : decimal.Parse(txtPrimaSeguroIncendio.Text);

                rTasacion = nTasacion.GuardarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Datos de la Propiedad Grabados");
                    ActualizarSegurosContratados(oTasacion);
                    CargarPropiedades();
                    CargarSegurosContratados();
                    LimpiarFormulario();

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
                    Controles.MostrarMensajeError("Error al Grabar la Propiedad");
                }
            }
        }
        private bool ValidarFormularioPropiedad()
        {
            try
            {
                if (NegPropiedades.objTasacion.PorcentajeSeguroIncendio > 0 && ddlSeguroIncendio.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Seguro de Incendio");
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

            ddlSeguroIncendio.ClearSelection();
            txtTasaSeguroIncendio.Text = "0";
            txtMontoAseguradoIncendio.Text = "0";
            txtPrimaSeguroIncendio.Text = "";
            ddlSeguroIncendio.Enabled = false;
            lblDireccion.Text = "";
            Controles.EjecutarJavaScript("DesplegarDatosPropiedad('0');");

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

                if (oTasacion.PorcentajeSeguroIncendio == 0)
                {
                    ddlSeguroIncendio.ClearSelection();
                    SeleccionarSeguroIncendio();
                    ddlSeguroIncendio.Enabled = false;
                }
                else
                {
                    ddlSeguroIncendio.SelectedValue = oTasacion.Seguro_Id.ToString();
                    ddlSeguroIncendio.Enabled = true;
                    txtTasaSeguroIncendio.Text = (oTasacion.TasaSeguro * 100).ToString();
                    txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado.ToString();
                    txtPrimaSeguroIncendio.Text = oTasacion.PrimaSeguro.ToString();
                }
                lblDireccion.Text = oTasacion.DireccionCompleta;
                NegPropiedades.objTasacion = new TasacionInfo();
                NegPropiedades.objTasacion = oTasacion;

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
        protected void ddlSeguroIncendio_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSeguroIncendio();
        }
        protected void btnGuardarProp_Click(object sender, EventArgs e)
        {
            GrabarPropiedad();
        }
        protected void btnCancelarProp_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

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
                    Fabrica_Id = 1;//Interna por Defecto
                }
                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Constantes.IdRolAbogado;
                ObjetoUsuario.Fabrica_Id = Fabrica_Id;
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref ddlAbogado, ObjetoResultado.Lista, "Rut", "Nombre", "--Abogado--", "-1");
                    ddlAbogado.SelectedValue = NegSolicitudes.objSolicitudInfo.Abogado_Id == 0 ? "-1" : NegSolicitudes.objSolicitudInfo.Abogado_Id.ToString();
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
                    Controles.MostrarMensajeError("Error al Cargar los Abogados");
                }
            }
        }
        private void CargaEventoDesembolso()
        {
            try
            {
                NegEventos nEvento = new NegEventos();
                EventosInfo oEvento = new EventosInfo();
                Resultado<EventosInfo> rEvento = new Resultado<EventosInfo>();

                oEvento.IndDesembolso = true;
                oEvento.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                rEvento = nEvento.Buscar(oEvento);

                if (rEvento.ResultadoGeneral)
                {
                    Controles.CargarCombo<EventosInfo>(ref ddlEventoDesembolso, rEvento.Lista, "Id", "Descripcion", "-- Eventos de Desembolso --", "-1");

                    //Asignacion de ventos de desembolso configurado en la inmobiliaria
                    if (NegSolicitudes.objSolicitudInfo.EventoDesembolsoInmobiliaria_Id != 0)
                        ddlEventoDesembolso.SelectedValue = NegSolicitudes.objSolicitudInfo.EventoDesembolsoInmobiliaria_Id.ToString();
                }
                else
                {
                    Controles.MostrarMensajeError(rEvento.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Eventos de Desembolso");
                }
            }
        }
        private void CargarNotarias()
        {
            try
            {
                NegNotarias nNotaria = new NegNotarias();
                NotariasInfo oNotaria = new NotariasInfo();
                Resultado<NotariasInfo> rNotaria = new Resultado<NotariasInfo>();


                oNotaria.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                rNotaria = nNotaria.Buscar(oNotaria);

                if (rNotaria.ResultadoGeneral)
                {
                    Controles.CargarCombo<NotariasInfo>(ref ddlNotaria, rNotaria.Lista, "Id", "Descripcion", "-- Notarias --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(rNotaria.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar las Notarias");
                }
            }
        }
        private void CargarMesEscritura()
        {
            try
            {
                var lstMesEscritura = new List<MesEscritura>();
                DateTime Hoy = new DateTime();
                Hoy = DateTime.Now;
                DateTime Fecha = new DateTime();
                MesEscritura mes = new MesEscritura();
                for (int i = -1; i < 2; i++)
                {

                    Fecha = Hoy.AddMonths(i);
                    DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

                    mes = new MesEscritura();
                    mes.Id = Fecha.Month.ToString() + "-" + Fecha.Year.ToString();
                    mes.Descripcion = dtinfo.GetMonthName(Fecha.Month).ToUpper() + " - " + Fecha.Year.ToString();
                    lstMesEscritura.Add(mes);
                }
                Controles.CargarCombo<MesEscritura>(ref ddlMesEscrituracion, lstMesEscritura, "Id", "Descripcion", "-- Mes de Escrituración --", "-1");

                ddlMesEscrituracion.SelectedValue = Hoy.Month.ToString() + "-" + Hoy.Year.ToString();


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
        private void CargarInformacionSolicitud()
        {
            try
            {
                if (NegSolicitudes.objSolicitudInfo != null)
                {
                    SolicitudInfo oSolicitud = new SolicitudInfo();
                    oSolicitud = NegSolicitudes.objSolicitudInfo;

                    //Credito
                    ddlObjetivo.SelectedValue = oSolicitud.Objetivo_Id.ToString();
                    CargaComboDestino(oSolicitud.Objetivo_Id);
                    ddlDestino.SelectedValue = oSolicitud.Destino_Id.ToString();

                    if (oSolicitud.Finalidad_Id > 0)
                    {
                        ddlFinalidad.SelectedValue = oSolicitud.Finalidad_Id.ToString();
                        SeleccionarFinalidad(int.Parse(ddlFinalidad.SelectedValue));
                        CargarActividadesEconomicasDeudor(oSolicitud.Rut);
                    }
                    if (oSolicitud.Utilidad_Id > 0)
                        ddlUtilidad.SelectedValue = oSolicitud.Utilidad_Id.ToString();



                    if (oSolicitud.BeneficiarioTributario_Id > 0)
                    {
                        ddlBeneficiarioTributario.SelectedValue = oSolicitud.BeneficiarioTributario_Id.ToString();
                    }
                    if (oSolicitud.IndArticulo150 != null)
                    {
                        rblArt150.SelectedValue = oSolicitud.IndArticulo150.ToString();
                    }



                    chkIndCartaResguardo.Checked = NegSolicitudes.objSolicitudInfo.IndCartaResguardo == null ? false : NegSolicitudes.objSolicitudInfo.IndCartaResguardo.Value;
                    chkIndInstruccionNotarial.Checked = NegSolicitudes.objSolicitudInfo.IndInstruccionNotarial == null ? false : NegSolicitudes.objSolicitudInfo.IndInstruccionNotarial.Value;

                    if (chkIndInstruccionNotarial.Checked)
                    {
                        ddlEventoDesembolso.SelectedValue = "57";// Evento Firma Participantes
                        ddlEventoDesembolso.Enabled = false;
                    }
                    else
                    {
                        ddlEventoDesembolso.ClearSelection();
                        ddlEventoDesembolso.Enabled = true;
                    }


                    if (NegSolicitudes.objSolicitudInfo.Abogado_Id != 0)
                        ddlAbogado.SelectedValue = NegSolicitudes.objSolicitudInfo.Abogado_Id.ToString();

                    if (NegSolicitudes.objSolicitudInfo.EventoDesembolso_Id != 0)
                        ddlEventoDesembolso.SelectedValue = NegSolicitudes.objSolicitudInfo.EventoDesembolso_Id.ToString();

                    if (NegSolicitudes.objSolicitudInfo.Notaria_Id != 0)
                        ddlNotaria.SelectedValue = NegSolicitudes.objSolicitudInfo.Notaria_Id.ToString();

                    if (NegSolicitudes.objSolicitudInfo.MesEscritura != 0)
                        ddlMesEscrituracion.SelectedValue = NegSolicitudes.objSolicitudInfo.MesEscritura.ToString() + "-" + NegSolicitudes.objSolicitudInfo.AñoEscritura.ToString();

                    if (NegSolicitudes.objSolicitudInfo.Subsidio_Id > 0)
                        Controles.EjecutarJavaScript("DesplegarDatosSubsidio('1');");
                    else
                        Controles.EjecutarJavaScript("DesplegarDatosSubsidio('0');");





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
                    Controles.MostrarMensajeError("Error al Cargar Datos de la Solicitud");
                }
            }
        }
        protected void ddlFinalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarFinalidad(int.Parse(ddlFinalidad.SelectedValue));
        }

        private void SeleccionarFinalidad(int Finalidad_Id)
        {
            try
            {
                if (("456").Contains(Finalidad_Id.ToString()))
                    Controles.EjecutarJavaScript("DesplegarDatosActividadEconominaDeudor('1');");
                else
                    Controles.EjecutarJavaScript("DesplegarDatosActividadEconominaDeudor('0');");
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Seleccionar la Finalidad");
                }
            }
        }
        private class MesEscritura
        {
            public string Id { get; set; }
            public string Descripcion { get; set; }
        }

        protected void ddlTipoDestinoFondo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarBeneficiarios(int.Parse(ddlTipoDestinoFondo.SelectedValue));
        }
        protected void btnModificarDestinoFondo_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDestinoFondos.DataKeys[row.RowIndex].Values["Id"].ToString());
            DestinoFondoInfo oDestino = new DestinoFondoInfo();
            oDestino = NegDestinoFondo.lstDestinoFondos.FirstOrDefault(df => df.Id == Id);
            CargarInfoDestinoFondo(oDestino);
        }
        protected void btnEliminarDestinoFondo_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDestinoFondos.DataKeys[row.RowIndex].Values["Id"].ToString());
            EliminarDestinoFondo(Id);
        }
        private void CargarDestinoFondo()
        {
            try
            {
                NegDestinoFondo nDestino = new NegDestinoFondo();
                DestinoFondoInfo oDestino = new DestinoFondoInfo();
                Resultado<DestinoFondoInfo> rDestino = new Resultado<DestinoFondoInfo>();


                oDestino.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rDestino = nDestino.BuscarDestinoFondo(oDestino);
                if (rDestino.ResultadoGeneral)
                {
                    Controles.CargarGrid<DestinoFondoInfo>(ref gvDestinoFondos, rDestino.Lista, new string[] { "Id" });
                    txtTotalDestinar.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.MontoCredito);
                    txtDestinado.Text = string.Format("{0:F4}", rDestino.Lista.Sum(df => df.MontoUF));
                    txtPorDestinar.Text = string.Format("{0:F4}", NegSolicitudes.objSolicitudInfo.MontoCredito - rDestino.Lista.Sum(df => df.MontoUF));
                }
                else
                {
                    Controles.MostrarMensajeError(rDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Los Destinos de Fondo");
                }
            }
        }
        private void CargarInfoDestinoFondo(DestinoFondoInfo oDestino)
        {
            try
            {
                if (oDestino != null)
                {
                    NegDestinoFondo.objDestinoFondos = new DestinoFondoInfo();
                    NegDestinoFondo.objDestinoFondos = oDestino;
                    ddlTipoDestinoFondo.SelectedValue = oDestino.TipoDestinoFondo_Id.ToString();
                    CargarBeneficiarios(oDestino.TipoDestinoFondo_Id);
                    ddlBeneficiario.SelectedValue = oDestino.Beneficiario_Id.ToString();
                    txtMontoDestinoFondo.Text = string.Format("{0:F4}", oDestino.MontoUF);
                    NegDestinoFondo.objDestinoFondos = oDestino;

                    var MontoDestinado = decimal.Zero;
                    var MontoPorDestinar = decimal.Zero;

                    if (decimal.TryParse(txtDestinado.Text, out MontoDestinado))
                    {
                        MontoDestinado = MontoDestinado - oDestino.MontoUF;
                        txtDestinado.Text = string.Format("{0:F4}", MontoDestinado);
                    }


                    if (decimal.TryParse(txtPorDestinar.Text, out MontoPorDestinar))
                    {
                        MontoPorDestinar = MontoPorDestinar + oDestino.MontoUF;
                        txtPorDestinar.Text = string.Format("{0:F4}", MontoPorDestinar);
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
                    Controles.MostrarMensajeError("Error al Cargar Datos del Destino de Fondo");
                }
            }
        }
        private bool ValidarIngresoDestinoFondo()
        {
            try
            {
                if (ddlTipoDestinoFondo.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Destino");
                    return false;
                }
                if (ddlBeneficiario.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Beneficiario");
                    return false;
                }
                if (txtMontoDestinoFondo.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Monto");
                    return false;
                }
                var MontoDestino = decimal.Zero;
                var MontoPorDestinar = decimal.Zero;
                if (decimal.TryParse(txtMontoDestinoFondo.Text, out MontoDestino))
                {
                    if (decimal.TryParse(txtPorDestinar.Text, out MontoPorDestinar))
                    {
                        if (MontoDestino > MontoPorDestinar)
                        {
                            Controles.MostrarMensajeAlerta("El Monto Ingresado no puede superar al Monto por Destinar (" + string.Format("{0:F4}", MontoPorDestinar) + ")");
                            return false;
                        }
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
                    Controles.MostrarMensajeError("Error al Validar los Datos del Destino de Fondos");
                }
                return false;
            }
        }
        private void CargarTipoDestinoFondo()
        {
            try
            {
                NegDestinoFondo nTipoDestino = new NegDestinoFondo();
                TipoDestinoFondoInfo oTipoDestino = new TipoDestinoFondoInfo();
                Resultado<TipoDestinoFondoInfo> rTipoDestino = new Resultado<TipoDestinoFondoInfo>();

                oTipoDestino.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                rTipoDestino = nTipoDestino.BuscarTipoDestino(oTipoDestino);
                if (rTipoDestino.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoDestinoFondoInfo>(ref ddlTipoDestinoFondo, rTipoDestino.Lista, "Id", "Descripcion", "-- Tipo de Destino --", "-1");
                    Controles.CargarCombo<DestinoFondoInfo>(ref ddlBeneficiario, null, "Beneficiario_Id", "NombreBeneficiario", "-- Beneficiario --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(rTipoDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Tipos de Destino de Fondos");
                }
            }
        }
        private void CargarBeneficiarios(int Id)
        {
            try
            {
                NegDestinoFondo nTipoDestino = new NegDestinoFondo();
                TipoDestinoFondoInfo oTipoDestino = new TipoDestinoFondoInfo();
                Resultado<DestinoFondoInfo> rTipoDestino = new Resultado<DestinoFondoInfo>();

                oTipoDestino = NegDestinoFondo.lstTipoDestinoFondos.FirstOrDefault(tdf => tdf.Id == Id);
                if (oTipoDestino == null)
                {
                    Controles.CargarCombo<DestinoFondoInfo>(ref ddlBeneficiario, null, "Beneficiario_Id", "NombreBeneficiario", "-- Beneficiario --", "-1");
                    return;
                }
                oTipoDestino.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rTipoDestino = nTipoDestino.BuscarBeneficiarioDestino(oTipoDestino);
                if (rTipoDestino.ResultadoGeneral)
                {
                    Controles.CargarCombo<DestinoFondoInfo>(ref ddlBeneficiario, rTipoDestino.Lista, "Beneficiario_Id", "NombreBeneficiario", "-- Beneficiario --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(rTipoDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar los Beneficiarios");
                }
            }
        }
        private void LimpiarFormularioDestinoFondo()
        {
            ddlTipoDestinoFondo.ClearSelection();
            CargarBeneficiarios(-1);
            txtMontoDestinoFondo.Text = "";
            NegDestinoFondo.objDestinoFondos = null;
        }
        private void GrabarDestinoFondo()
        {
            try
            {
                NegDestinoFondo nDestino = new NegDestinoFondo();
                DestinoFondoInfo oDestino = new DestinoFondoInfo();
                Resultado<DestinoFondoInfo> rDestino = new Resultado<DestinoFondoInfo>();

                if (!ValidarIngresoDestinoFondo()) return;

                if (NegDestinoFondo.objDestinoFondos != null)
                    oDestino = NegDestinoFondo.objDestinoFondos;

                oDestino.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oDestino.TipoDestinoFondo_Id = int.Parse(ddlTipoDestinoFondo.SelectedValue);
                oDestino.Beneficiario_Id = int.Parse(ddlBeneficiario.SelectedValue);
                oDestino.NombreBeneficiario = ddlBeneficiario.SelectedItem.Text;
                oDestino.MontoUF = decimal.Parse(txtMontoDestinoFondo.Text);
                oDestino.Estado_Id = (int)NegTablas.IdentificadorMaestro("EST_DESTINO_FONDO", "I");

                rDestino = nDestino.GuardarDestinoFondo(oDestino);
                if (rDestino.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Grabado Correctamente");
                    LimpiarFormularioDestinoFondo();
                    CargarDestinoFondo();
                }
                else
                {
                    Controles.MostrarMensajeError(rDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar el Destino de Fondo");
                }
            }
        }
        private void EliminarDestinoFondo(int Id)
        {
            try
            {
                NegDestinoFondo nDestino = new NegDestinoFondo();
                DestinoFondoInfo oDestino = new DestinoFondoInfo();
                Resultado<DestinoFondoInfo> rDestino = new Resultado<DestinoFondoInfo>();


                oDestino.Id = Id;

                rDestino = nDestino.GuardarDestinoFondo(oDestino);
                if (rDestino.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    LimpiarFormularioDestinoFondo();
                    CargarDestinoFondo();
                }
                else
                {
                    Controles.MostrarMensajeError(rDestino.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar el Destino de Fondo");
                }
            }
        }
        protected void btnGrabarDestinoFondo_Click(object sender, EventArgs e)
        {
            GrabarDestinoFondo();
        }
        protected void btnCancelarDestinoFondo_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDestinoFondo();
        }
        protected void btnImprimirOrden_Click(object sender, EventArgs e)
        {
            GenerarOrdenEscrituracionPDF();
        }

        protected void btnImprimirHojaResumen_Click(object sender, EventArgs e)
        {
            GenerarHojaResumenPDF();
        }

        protected void chkIndInstruccionNotarial_CheckedChanged(object sender, EventArgs e)
        {
            SeleccionInstruccionNotarial();
        }
        protected void chkIndCartaResguardo_CheckedChanged(object sender, EventArgs e)
        {
            SeleccionCartaResguardo();
        }

        private void SeleccionInstruccionNotarial()
        {
            try
            {
                if (chkIndInstruccionNotarial.Checked)
                {
                    if (chkIndCartaResguardo.Checked == true && hdfSeleccionUnicaHipoteca.Value == "true")
                    {
                        Controles.MostrarMensajeInfo("Solo se puede seleccionar una opción para el alzamiento de la Hipoteca ya que sólo existe una Institución de Alzamiento");
                        chkIndCartaResguardo.Checked = false;
                        return;
                    }
                    ddlEventoDesembolso.SelectedValue = "57";// Evento Firma Participantes
                    ddlEventoDesembolso.Enabled = false;
                }
                else
                {
                    ddlEventoDesembolso.ClearSelection();
                    ddlEventoDesembolso.Enabled = true;
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
                    Controles.MostrarMensajeError("Error al Seleccionar Instrucción Notarial");
                }
            }
        }

        private void SeleccionCartaResguardo()
        {
            try
            {
                if (chkIndCartaResguardo.Checked)
                {
                    if (chkIndInstruccionNotarial.Checked == true && hdfSeleccionUnicaHipoteca.Value == "true")
                    {
                        Controles.MostrarMensajeInfo("Solo se puede seleccionar una opción para el alzamiento de la Hipoteca ya que sólo existe una Institución de Alzamiento");
                        chkIndCartaResguardo.Checked = false;
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
                    Controles.MostrarMensajeError("Error al Seleccionar Carta de Resguardo");
                }
            }
        }
        private void ValidarNumeroOperacion()
        {
            try
            {




            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Validar el Número de Operación");
                }
            }

        }


        #region Subsidio

        private void CargarMesSubsidio()
        {
            try
            {
                var lstAños = new List<MesEscritura>();
                DateTime Hoy = new DateTime();
                Hoy = DateTime.Now;

                MesEscritura mes = new MesEscritura();
                for (int i = 1; i <= 12; i++)
                {

                    DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
                    mes = new MesEscritura();
                    mes.Id = i.ToString();
                    mes.Descripcion = dtinfo.GetMonthName(i).ToUpper();
                    lstAños.Add(mes);
                }
                Controles.CargarCombo<MesEscritura>(ref ddlMesCertificadoSubsidio, lstAños, "Id", "Descripcion", "-- Mes del Certificado --", "-1");




            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Mes del Subsidio");
                }
            }
        }

        private void CargarAñoSubsidio()
        {
            try
            {
                var lstAños = new List<MesEscritura>();
                DateTime Hoy = new DateTime();
                DateTime Fecha = new DateTime();
                Hoy = DateTime.Now;
                MesEscritura año = new MesEscritura();
                for (int i = -4; i <= 1; i++)
                {
                    Fecha = Hoy.AddYears(i);
                    DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
                    año = new MesEscritura();
                    año.Id = Fecha.Year.ToString();
                    año.Descripcion = Fecha.Year.ToString();
                    lstAños.Add(año);
                }
                Controles.CargarCombo<MesEscritura>(ref ddlAñoCertificadoSubsidio, lstAños, "Id", "Descripcion", "-- Año del Certificado --", "-1");
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Año del Subsidio");
                }
            }
        }

        private void CargarInfoSubsidio(SolicitudInfo solicitud)
        {
            if (solicitud.Subsidio_Id > 0)
            {
                txtAhorroPrevioSubsidio.Text = solicitud.AhorroPrevioSubsidio.ToString();
                txtSerieSubsidio.Text = solicitud.NumeroSerieSubsidio;
                txtNumeroCertificadoSubsidio.Text = solicitud.NumeroCertificadoSubsidio;
                ddlMesCertificadoSubsidio.SelectedValue = solicitud.MesCertificadoSubsidio.ToString();
                ddlAñoCertificadoSubsidio.SelectedValue = solicitud.AñoCertificadoSubsidio.ToString();
                txtNumeroLibretaSubsidio.Text = solicitud.NumeroLibretaSubsidio;
            }
        }

        #endregion


    }
}