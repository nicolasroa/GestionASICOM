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

namespace WorkFlow.Eventos.WfFormalizacion
{
    public partial class AntecedentesOperacion : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioAntecedentes();");

            if (!Page.IsPostBack)
            {
                Controles.EjecutarJavaScript("DesplegarDatosTasacion('0');");
                Controles.EjecutarJavaScript("DesplegarDatosEstudioTitulo('0');");
                NegSolicitudes.IndComentario = false;
                CargarAcciones();
                CargaComboFabricaTasadores();
                CargarTasadores();
                CargarInstitucionesFinancieras();
                CargarInformacionFlujos();
                CargaComboInmobiliaria();

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
                    if (!DatosSolicitud.GrabarSolicitud(true)) return;
                    if (!ValidarParticipantes()) return;
                    if (!ValidarPropiedades()) return;
                    if (!ValidarFlujoTasacion()) return;
                    if (!ProcesarFlujoTasacion()) return;
                    if (!ValidarFlujoEETT()) return;
                    if (!ProcesarFlujoEETT()) return;
                    if (!ActualizaSolicitud()) return;
                    if (!NegSolicitudes.RecalcularDividendo()) return;
                }
                else if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//FINALIZAR
                {
                    if (!DatosSolicitud.GrabarSolicitud(true)) return;
                    if (!ValidarParticipantes()) return;
                    if (!ValidarPropiedades()) return;
                    if (!ValidarFlujoTasacion()) return;
                    if (!ProcesarFlujoTasacion()) return;
                    if (!ValidarFlujoEETT()) return;
                    if (!ProcesarFlujoEETT()) return;
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
        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
        private bool ValidarParticipantes()
        {
            try
            {

                if (NegParticipante.lstParticipantes == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar los participantes de la Solicitud");
                    return false;
                }

                if (NegParticipante.lstParticipantes.Where(p => p.TipoParticipacion_Id == 1).Count() == 0)
                {
                    Controles.MostrarMensajeAlerta("Solicitud sin Participante Principal Ingresado");
                    return false;
                }
                if (NegParticipante.lstParticipantes.Where(p => p.TipoParticipacion_Id == 4).Count() == 0 && (NegSolicitudes.objSolicitudInfo.Destino_Id == 1 || NegSolicitudes.objSolicitudInfo.Destino_Id == 4))// Se Valida Vendedor solo para las CompraVenta
                {
                    Controles.MostrarMensajeAlerta("Solicitud sin Vendedor Ingresado");
                    return false;
                }

                //Solicitar Mandatario
                if (NegParticipante.lstParticipantes.Where(p => p.TipoParticipacion_Id == 9).Count() == 0)// Se Valida Mandatario Judicial
                {
                    Controles.MostrarMensajeAlerta("Solicitud sin Mandatario Judicial Ingresado");
                    return false;
                }


                ParticipanteInfo oParticipante = new ParticipanteInfo();
                oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.TipoParticipacion_Id == 1);

                if (oParticipante.SeguroDesgravamen_Id == -1 && oParticipante.PorcentajeDesgravamen > 0)
                {
                    Controles.MostrarMensajeAlerta("El Participante Principal debe Tener Seguro de Desgravamen");
                    return false;
                }


                AntecedentesLaboralesInfo oFiltro = new AntecedentesLaboralesInfo();
                NegParticipante nLaborales = new NegParticipante();
                Resultado<AntecedentesLaboralesInfo> rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                foreach (var participante in NegParticipante.lstParticipantes)
                {
                    rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                    if (participante.TipoPersona_Id == 1 && participante.PorcentajeDeuda > 0 && (participante.TipoParticipacion_Id == 1 || participante.TipoParticipacion_Id == 2)) //Validación Solo para el Deudor Principal y Codeudor
                    {
                        oFiltro.RutCliente = participante.Rut;
                        rLaborales = nLaborales.BuscarAntecedentesLaborales(oFiltro);
                        if (rLaborales.ResultadoGeneral)
                        {
                            if (rLaborales.Lista.Count == 0)
                            {
                                Controles.MostrarMensajeAlerta("Debe Ingresar los Antecedentes Laborales del Participante " + participante.NombreCliente);
                                return false;
                            }
                        }
                    }
                }

                NegClientes nCliente = new NegClientes();
                Resultado<DireccionClienteInfo> rDireccion = new Resultado<DireccionClienteInfo>();
                DireccionClienteInfo oDireccion = new DireccionClienteInfo();


                foreach (var participante in NegParticipante.lstParticipantes)
                {

                    oDireccion.Cliente_Id = participante.Rut;
                    rDireccion = nCliente.BuscarDireccion(oDireccion);
                    if (rDireccion.ResultadoGeneral)
                    {
                        if (rDireccion.Lista.Count(d => d.TipoDireccion_Id == 1) == 0)
                        {
                            Controles.MostrarMensajeAlerta("Debe Ingresar la Dirección Principal del Participante " + participante.NombreCliente);
                            return false;
                        }
                    }
                }
                Resultado<ClientesInfo> rClientes = new Resultado<ClientesInfo>();
                ClientesInfo oCliente = new ClientesInfo();

                if (("456").Contains(NegSolicitudes.objSolicitudInfo.Finalidad_Id.ToString()))
                {
                    foreach (var participante in NegParticipante.lstParticipantes)
                    {
                        oCliente.Rut = participante.Rut;
                        rClientes = nCliente.Buscar(oCliente);
                        if (rClientes.ResultadoGeneral)
                        {
                            if (rClientes.Lista.Count(d => d.ActividadSii1_Id <= 0 && d.TipoPersona_Id == 1) > 0)
                            {
                                Controles.MostrarMensajeAlerta("Debe Ingresar la Actividad Económina del Participante " + participante.NombreCliente);
                                return false;
                            }
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
                    Controles.MostrarMensajeError("Error al Validar los Participantes");
                }
                return false;
            }
        }
        private bool ValidarPropiedades()
        {
            try
            {
                if (NegPropiedades.lstTasaciones == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Propiedad de la Solicitud");
                    return false;
                }

                if (NegPropiedades.lstTasaciones.Count == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Propiedad de la Solicitud");
                    return false;
                }

                if (NegPropiedades.lstTasaciones.Count(t => t.IndPropiedadPrincipal == true) == 0)
                {
                    Controles.MostrarMensajeAlerta("Al Menos una Propiedad debe estar marcada como Principal");
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
        private bool ValidarFlujoTasacion()
        {
            try
            {

                if (rblIndFlujoTasacion.SelectedValue == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Opción para la Tasación");
                    return false;
                }
                if (rblIndFlujoTasacion.SelectedValue == "0")//Tasación Piloto
                {
                    NegPropiedades nTasacion = new NegPropiedades();
                    TasacionInfo oTasacion = new TasacionInfo();
                    Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                    oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                    oTasacion.IndFlujoTasacion = true;

                    rTasacion = nTasacion.BuscarTasacion(oTasacion);
                    if (rTasacion.ResultadoGeneral)
                    {
                        if (rTasacion.Lista.Where(t => t.Tasador_Id == -1 || t.Tasador_Id == 0).Count() > 0)
                        {
                            Controles.MostrarMensajeAlerta("Debe Completar la Informacion de la Tasacion en Todos los Inmuebles");
                            return false;
                        }
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
                    TotalMontoTasacion = rTasacion.Lista.Sum(t => t.MontoTasacion);
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
                    Controles.MostrarMensajeError("Error al Validar el Flujo de Tasación");
                }
                return false;
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
        private bool ProcesarFlujoTasacion()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oTasacion.IndFlujoTasacion = true;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (!rTasacion.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(rTasacion.Mensaje);
                    return false;
                }
                if (rblIndFlujoTasacion.SelectedValue == "1")//Flujo de Tasación
                {
                    foreach (var inmueble in rTasacion.Lista)
                    {

                        if (inmueble.FechaSolicitudTasacion == null)
                        {
                            inmueble.IndFlujoTasacion = true;
                            inmueble.Tasador_Id = -1;
                            inmueble.MontoTasacion = 0;
                            inmueble.MontoAsegurado = 0;
                            inmueble.MontoLiquidacion = 0;
                            inmueble.MetrosConstruidos = 0;
                            inmueble.MetrosTerreno = 0;
                            inmueble.MetrosTerraza = 0;
                            inmueble.MetrosLogia = 0;
                            inmueble.FechaTasacion = null;
                            inmueble.FechaRecepcionFinal = null;
                            inmueble.PermisoEdificacion = "";
                            rTasacion = nTasacion.GuardarTasacion(inmueble);
                            if (!rTasacion.ResultadoGeneral)
                            {
                                Controles.MostrarMensajeError(rTasacion.Mensaje);
                                return false;
                            }
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
                    Controles.MostrarMensajeError("Error al Procesar el Flujo de Tasación");
                }
                return false;
            }
        }
        private bool ValidarFlujoEETT()
        {
            try
            {
                if (rblIndFlujoEstudioTitulo.SelectedValue == "0")//EETT Piloto
                {
                    NegPropiedades nTasacion = new NegPropiedades();
                    TasacionInfo oTasacion = new TasacionInfo();
                    Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                    oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                    oTasacion.IndFlujoEstudioTitulo = true;

                    rTasacion = nTasacion.BuscarTasacion(oTasacion);
                    if (rTasacion.ResultadoGeneral)
                    {
                        if (rTasacion.Lista.Where(t => t.IndAlzamientoHipoteca == null).Count() > 0)
                        {
                            Controles.MostrarMensajeAlerta("Debe Completar la Informacion del EETT en Todos los Inmuebles");
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
                    Controles.MostrarMensajeError("Error al Validar el Flujo de EETT");
                }
                return false;
            }
        }
        private bool ProcesarFlujoEETT()
        {
            try
            {

                if (rblIndFlujoEstudioTitulo.SelectedValue == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Opción para el EETT");
                    return false;
                }

                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oTasacion.IndFlujoEstudioTitulo = true;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (!rTasacion.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(rTasacion.Mensaje);
                    return false;
                }
                if (rblIndFlujoEstudioTitulo.SelectedValue == "1")//Flujo de EETT
                {
                    foreach (var inmueble in rTasacion.Lista)
                    {
                        if (NegSolicitudes.objSolicitudInfo.FechaSolicitudEstudioTitulo == null)
                        {
                            inmueble.IndAlzamientoHipoteca = null;
                            inmueble.InstitucionAlzamientoHipoteca_Id = -1;
                            inmueble.IndFlujoEstudioTitulo = true;

                            rTasacion = nTasacion.GuardarTasacion(inmueble);
                            if (!rTasacion.ResultadoGeneral)
                            {
                                Controles.MostrarMensajeError(rTasacion.Mensaje);
                                return false;
                            }
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
                    Controles.MostrarMensajeError("Error al Procesar el Flujo de EETT");
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

                NegSolicitudes.objSolicitudInfo.IndFlujoEstudioTitulo = rblIndFlujoEstudioTitulo.SelectedValue == "0" ? false : true;
                NegSolicitudes.objSolicitudInfo.IndFlujoTasacion = rblIndFlujoTasacion.SelectedValue == "0" ? false : true;
                if (rblIndFlujoTasacion.SelectedValue == "1")//Flujo de Tasación
                {
                    NegSolicitudes.objSolicitudInfo.IndFlujoTasacion = true;
                    NegSolicitudes.objSolicitudInfo.FechaAprobacionTasacion = null;

                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.IndFlujoTasacion = false;
                    NegSolicitudes.objSolicitudInfo.FechaAprobacionTasacion = DateTime.Now;
                }

                if (rblIndFlujoEstudioTitulo.SelectedValue == "1")
                {
                    NegSolicitudes.objSolicitudInfo.IndFlujoEstudioTitulo = true;
                    NegSolicitudes.objSolicitudInfo.FechaAprobacionEstudioTitulo = null;
                }
                else
                {
                    NegSolicitudes.objSolicitudInfo.IndFlujoEstudioTitulo = false;
                    NegSolicitudes.objSolicitudInfo.FechaAprobacionEstudioTitulo = DateTime.Now;
                }
                rSolicitud = nSolicitud.Guardar(NegSolicitudes.objSolicitudInfo);
                if (!rSolicitud.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(rSolicitud.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Actualizar la Solicitud");
                }
                return false;
            }
        }

        protected void rblIndFlujoTasacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblIndFlujoTasacion.SelectedValue == "0")
            {
                Controles.EjecutarJavaScript("DesplegarDatosTasacion('1');");
                CargarPropiedadesTasacion();
            }
            else
            {
                gvPropiedadesTasacion.DataSource = null;
                gvPropiedadesTasacion.DataBind();
                Controles.EjecutarJavaScript("DesplegarDatosTasacion('0');");
            }
        }
        protected void rblIndFlujoEstudioTitulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblIndFlujoEstudioTitulo.SelectedValue == "0")
            {
                Controles.EjecutarJavaScript("DesplegarDatosEstudioTitulo('1');");
                CargarPropiedadesEETT();
            }
            else
            {
                gvPropiedadesEETT.DataSource = null;
                gvPropiedadesEETT.DataBind();
                Controles.EjecutarJavaScript("DesplegarDatosEstudioTitulo('0');");
            }
        }
        public void CargarPropiedadesTasacion()
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
        public void CargarPropiedadesEETT()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                TasacionInfo oTasacion = new TasacionInfo();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                oTasacion.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oTasacion.IndFlujoEstudioTitulo = true;

                rTasacion = nTasacion.BuscarTasacion(oTasacion);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.CargarGrid<TasacionInfo>(ref gvPropiedadesEETT, rTasacion.Lista, new string[] { "Id", "Propiedad_Id" });
                    NegPropiedades.lstPropiedadesEETT = new List<TasacionInfo>();
                    NegPropiedades.lstPropiedadesEETT = rTasacion.Lista;

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
        protected void AddlFabTasadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboTasadores(int.Parse(AddlFabTasadores.SelectedValue));
        }
        protected void gvPropiedadesTasacion_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

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
        protected void btnSeleccionarPropEETT_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvPropiedadesEETT.DataKeys[row.RowIndex].Values["Id"].ToString());
            TasacionInfo oTasacion = new TasacionInfo();
            oTasacion = NegPropiedades.lstPropiedadesEETT.FirstOrDefault(t => t.Id == Id);
            NegPropiedades.objEETT = new TasacionInfo();
            NegPropiedades.objEETT = oTasacion;
            CargarInfoEstudioTitulo(oTasacion);
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
                        txtValorComercial.Text = oTasacion.ValorComercial == 0 ? "" : string.Format("{0:F4}", oTasacion.ValorComercial);
                        txtMontoTasacion.Text = oTasacion.MontoTasacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoTasacion);
                        txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoAsegurado);
                        txtMontoLiquidacion.Text = oTasacion.MontoLiquidacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoLiquidacion);
                        txtMetrosTerreno.Text = oTasacion.MetrosTerreno == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerreno);
                        txtMetrosTerraza.Text = oTasacion.MetrosTerraza == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerraza);
                        txtMetrosLogia.Text = oTasacion.MetrosLogia == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosLogia);
                        txtMetrosConstruccion.Text = oTasacion.MetrosConstruidos == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosConstruidos);


                        if (oTasacion.PorcentajeSeguroIncendio > 0)
                            txtMontoAseguradoIncendio.Enabled = true;
                        else
                        {
                            txtMontoAseguradoIncendio.Text = "0";
                            txtMontoAseguradoIncendio.Enabled = false;
                        }

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
                        AddlFabTasadores.SelectedValue = oTasacion.FabricaTasador_Id.ToString();
                        CargaComboTasadores(oTasacion.FabricaTasador_Id);
                        AddlTasador.SelectedValue = oTasacion.Tasador_Id.ToString();
                        txtValorComercial.Text = oTasacion.ValorComercial == 0 ? "" : string.Format("{0:F4}", oTasacion.ValorComercial);
                        txtMontoTasacion.Text = oTasacion.MontoTasacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoTasacion);
                        txtMontoAseguradoIncendio.Text = oTasacion.MontoAsegurado == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoAsegurado);
                        txtMontoLiquidacion.Text = oTasacion.MontoLiquidacion == 0 ? "" : string.Format("{0:F4}", oTasacion.MontoLiquidacion);
                        txtFechaTasacion.Text = oTasacion.FechaTasacion == null ? "" : oTasacion.FechaTasacion.Value.ToShortDateString();
                        txtFechaRecepcionFinal.Text = oTasacion.FechaRecepcionFinal == null ? "" : oTasacion.FechaRecepcionFinal.Value.ToShortDateString();
                        txtMetrosTerreno.Text = oTasacion.MetrosTerreno == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerreno);
                        txtMetrosTerraza.Text = oTasacion.MetrosTerraza == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosTerraza);
                        txtMetrosLogia.Text = oTasacion.MetrosLogia == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosLogia);
                        txtMetrosConstruccion.Text = oTasacion.MetrosConstruidos == 0 ? "" : string.Format("{0:F4}", oTasacion.MetrosConstruidos);
                        txtPermisoEdificacion.Text = oTasacion.PermisoEdificacion;
                        lblDireccionTasacion.Text = oTasacion.DireccionCompleta;


                        if (oTasacion.PorcentajeSeguroIncendio > 0)
                            txtMontoAseguradoIncendio.Enabled = true;
                        else
                        {
                            txtMontoAseguradoIncendio.Text = "0";
                            txtMontoAseguradoIncendio.Enabled = false;
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

                    NegPropiedades.objTasacion.Tasador_Id = int.Parse(AddlTasador.SelectedValue);
                    NegPropiedades.objTasacion.FabricaTasador_Id = int.Parse(AddlFabTasadores.SelectedValue);
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
                    NegPropiedades.objTasacion.IndFlujoTasacion = false;

                    oTasacion = NegPropiedades.objTasacion;
                    if (!ActualizaSeguros(ref oTasacion)) return;
                    if (!ActualizarSegurosContratados(oTasacion)) return;

                    rTasacion = nTasacion.GuardarTasacion(oTasacion);
                    if (rTasacion.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeExito("Datos Grabados");
                        CargarPropiedadesTasacion();
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



        private bool ActualizaSeguros(ref TasacionInfo oTasacion)
        {
            try
            {
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                if (oTasacion.Seguro_Id == -1)
                {
                    oTasacion.PrimaSeguro = 0;
                }
                else
                {
                    ObjInfo.Id = oTasacion.Seguro_Id;
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        PrimaSeguro = (ObjInfo.TasaMensual * oTasacion.MontoAsegurado);
                        oTasacion.PrimaSeguro = PrimaSeguro;
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Actualizar el Seguro de Incendio");
                }
                return false;
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

                if (oTasacion.Seguro_Id > 0)
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
                    Controles.MostrarMensajeError("Error al Actualizar los Seguros Contratados");
                }
                return false;
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
        private void GrabarInfoEETT()
        {
            try
            {
                NegPropiedades nTasacion = new NegPropiedades();
                Resultado<TasacionInfo> rTasacion = new Resultado<TasacionInfo>();

                if (NegPropiedades.objEETT == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Inmueble");
                    return;
                }
                if (!ValidarInfoEETT()) return;
                NegPropiedades.objEETT.IndAlzamientoHipoteca = chkIndAlzamientoHipoteca.Checked;
                NegPropiedades.objEETT.InstitucionAlzamientoHipoteca_Id = int.Parse(ddlInstitucionAlzamientoHIpoteca.SelectedValue);
                NegPropiedades.objEETT.IndFlujoEstudioTitulo = false;

                rTasacion = nTasacion.GuardarTasacion(NegPropiedades.objEETT);
                if (rTasacion.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Datos Grabados");
                    CargarPropiedadesEETT();
                    LimpiarInfoEETT();
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
        private bool ValidarInfoTasacion()
        {
            try
            {
                var MontoTasacion = decimal.Zero;
                var MontoAsegurado = decimal.Zero;
                var MontoLiquidacion = decimal.Zero;
                var MetrosContruidos = decimal.Zero;
                var MetrosTerreno = decimal.Zero;
                var MetrosLogia = decimal.Zero;
                var MetrosTerraza = decimal.Zero;
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


                if (txtRolManzana.Text == "" && chkIndUsoGoce.Checked == false)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un valor para el Rol - Manzana");
                    return false;
                }

                if (txtRolSitio.Text == "" && chkIndUsoGoce.Checked == false)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un valor para el Rol - Sitio");
                    return false;
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
        private bool ValidarInfoEETT()
        {
            try
            {
                if (chkIndAlzamientoHipoteca.Checked == true && ddlInstitucionAlzamientoHIpoteca.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar una Institución de Alzamiento de Hipoteca");
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
        private void LimpiarInfoTasacion()
        {
            AddlTasador.ClearSelection();
            AddlFabTasadores.ClearSelection();
            txtFechaTasacion.Text = "";
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
            NegPropiedades.objTasacion = null;
        }
        private void LimpiarInfoEETT()
        {
            chkIndAlzamientoHipoteca.Checked = false;
            ddlInstitucionAlzamientoHIpoteca.Enabled = true;
            ddlInstitucionAlzamientoHIpoteca.ClearSelection();
            lblDireccionEETT.Text = "";
            NegPropiedades.objEETT = null;
        }
        protected void btnGrabarTasacion_Click(object sender, EventArgs e)
        {
            GrabarInfoTasacion();
        }
        protected void btnCancelarTasacion_Click(object sender, EventArgs e)
        {
            LimpiarInfoTasacion();
        }
        private void CargarInstitucionesFinancieras()
        {
            try
            {
                NegInstitucionesFinancieras oNegocio = new NegInstitucionesFinancieras();
                InstitucionesFinancierasBase oFiltro = new InstitucionesFinancierasBase();
                Resultado<InstitucionesFinancierasBase> oResultado = new Resultado<InstitucionesFinancierasBase>();

                oResultado = oNegocio.Buscar(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo(ref ddlInstitucionAlzamientoHIpoteca, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Institución Financiera --", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Tasadores");
                }
            }
        }
        private void CargarInfoEstudioTitulo(TasacionInfo oEETT)
        {
            try
            {
                chkIndAlzamientoHipoteca.Checked = oEETT.IndAlzamientoHipoteca == null ? false : oEETT.IndAlzamientoHipoteca.Value;
                lblDireccionEETT.Text = oEETT.DireccionCompleta;
                if (oEETT.IndAlzamientoHipoteca == true)
                {
                    ddlInstitucionAlzamientoHIpoteca.Enabled = true;
                    ddlInstitucionAlzamientoHIpoteca.SelectedValue = oEETT.InstitucionAlzamientoHipoteca_Id.ToString();
                }
                else
                {
                    ddlInstitucionAlzamientoHIpoteca.ClearSelection();
                    ddlInstitucionAlzamientoHIpoteca.Enabled = false;
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
        }
        protected void chkIndAlzamientoHipoteca_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndAlzamientoHipoteca.Checked == true)
            {
                ddlInstitucionAlzamientoHIpoteca.Enabled = true;
            }
            else
            {
                ddlInstitucionAlzamientoHIpoteca.ClearSelection();
                ddlInstitucionAlzamientoHIpoteca.Enabled = false;
            }
        }
        protected void gvPropiedadesEETT_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnGrabarEstudioTitulo_Click(object sender, EventArgs e)
        {
            GrabarInfoEETT();
        }

        protected void btnCancelarEstudioTitulo_Click(object sender, EventArgs e)
        {
            LimpiarInfoEETT();
        }

        private void CargarInformacionFlujos()
        {
            try
            {
                if (NegSolicitudes.objSolicitudInfo.IndFlujoEstudioTitulo != null)
                {
                    if (NegSolicitudes.objSolicitudInfo.IndFlujoEstudioTitulo == false)
                        rblIndFlujoEstudioTitulo.SelectedValue = "0";
                    if (NegSolicitudes.objSolicitudInfo.IndFlujoEstudioTitulo == true)
                        rblIndFlujoEstudioTitulo.SelectedValue = "1";

                    if (rblIndFlujoEstudioTitulo.SelectedValue == "0")
                    {
                        Controles.EjecutarJavaScript("DesplegarDatosEstudioTitulo('1');");
                        CargarPropiedadesEETT();
                    }
                    else
                    {
                        gvPropiedadesEETT.DataSource = null;
                        gvPropiedadesEETT.DataBind();
                        Controles.EjecutarJavaScript("DesplegarDatosEstudioTitulo('0');");
                    }
                }
                if (NegSolicitudes.objSolicitudInfo.IndFlujoTasacion != null)
                {
                    if (NegSolicitudes.objSolicitudInfo.IndFlujoTasacion == false)
                        rblIndFlujoTasacion.SelectedValue = "0";
                    if (NegSolicitudes.objSolicitudInfo.IndFlujoTasacion == true)
                        rblIndFlujoTasacion.SelectedValue = "1";

                    if (rblIndFlujoTasacion.SelectedValue == "0")
                    {
                        Controles.EjecutarJavaScript("DesplegarDatosTasacion('1');");
                        CargarPropiedadesTasacion();
                    }
                    else
                    {
                        gvPropiedadesTasacion.DataSource = null;
                        gvPropiedadesTasacion.DataBind();
                        Controles.EjecutarJavaScript("DesplegarDatosTasacion('0');");
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
                    Controles.MostrarMensajeError("Error al cargar los Datos de los Flujos");
                }
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
                txtMontoAseguradoIncendio.Text = "0";
            }
            else
            {
                txtRolManzana.Enabled = true;
                txtRolSitio.Enabled = true;
            }
        }


    }
}