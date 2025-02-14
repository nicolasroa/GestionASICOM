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

namespace WorkFlow.Eventos
{
    public partial class IngresoEstadoSituacion : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarAcciones();

                CargaClientes();
                CargarMotivoRechazo();


            }
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
                btnAccion.Attributes.Remove("OnMouseDown");
                btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacion('¿Seguro que desea Procesar esta Acción para el Evento?',this);");

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
                    btnAccion.Attributes.Remove("OnMouseDown");
                    btnAccion.Attributes.Add("OnMouseDown", "javascript:MensajeConfirmacionAnularSolicitud('¿Esta seguro de Rechazar la Solicitud?',this);");
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
        public void ddlAccionEvento_SelectedIndexChanged(object sender, EventArgs e)
        {

            SeleccionarAccion();
        }
        public void btnAccion_Click(object sender, EventArgs e)
        {
            ProcesarEvento();
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
                    if (!NegSolicitudes.RecalcularDividendo()) return;

                }
                if (oAccion.Sentido_Id == NegTablas.IdentificadorMaestro("SENTIDOS_WF", "F"))//Finalizar
                {
                    if (!GrabarMotivoRechazo()) return;
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
        private bool GrabarMotivoRechazo()
        {
            try
            {
                if (ddlMotivoRechazo.SelectedValue.Equals("-1"))
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Motivo para el Rechazo");
                    return false;
                }
                NegSolicitudes.objSolicitudInfo.MotivoRechazo_Id = int.Parse(ddlMotivoRechazo.SelectedValue);
                return NegSolicitudes.ActualizarSolicitud(NegSolicitudes.objSolicitudInfo);
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
        private void CargarMotivoRechazo()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("mot_rechazo_sol");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlMotivoRechazo, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Motivo --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Sexo Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Sexo");
                }
            }
        }
        protected void btnGrabarMotivoRechazo_Click(object sender, EventArgs e)
        {
            ProcesarEvento();
        }

        private bool ValidarParticipantes()
        {
            try
            {

                if (NegParticipante.lstParticipantes == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar los Participantes de la Solicitud");
                    return false;
                }

                if (NegParticipante.lstParticipantes.Where(p => p.TipoParticipacion_Id == 1).Count() == 0)
                {
                    Controles.MostrarMensajeAlerta("Solicitud sin Participante Principal Ingresado");
                    return false;
                }

                ParticipanteInfo oParticipante = new ParticipanteInfo();
                oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.TipoParticipacion_Id == 1);

                if (oParticipante.SeguroDesgravamen_Id == -1 && oParticipante.PorcentajeDesgravamen >= 0 && oParticipante.TipoPersona_Id == 1)
                {
                    Controles.MostrarMensajeAlerta("El Participante Principal debe Tener Seguro de Desgravamen");
                    return false;
                }
                AntecedentesLaboralesInfo oFiltro = new AntecedentesLaboralesInfo();
                NegParticipante nLaborales = new NegParticipante();
                Resultado<AntecedentesLaboralesInfo> rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                ClientesInfo oCliente = new ClientesInfo();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();
                NegClientes nCliente = new NegClientes();


                foreach (var participante in NegParticipante.lstParticipantes)
                {
                    rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                    if (participante.TipoPersona_Id == 1 && participante.PorcentajeDeuda > 0 && (participante.TipoParticipacion_Id == 1 || participante.TipoParticipacion_Id == 2)) //Validación Solo para el Deudor Principal y Codeudor
                    {
                        oFiltro.RutCliente = participante.Rut;
                        rLaborales = nLaborales.BuscarAntecedentesLaborales(oFiltro);
                        if (rLaborales.ResultadoGeneral)
                        {
                            if (rLaborales.Lista.Count() == 0)
                            {
                                Controles.MostrarMensajeAlerta("Debe Ingresar los Antecedentes Laborales del Participante " + participante.NombreCliente);
                                return false;
                            }
                            if (rLaborales.Lista.Count(l => l.FechaInicioContrato == null ||
                                                            l.RutEmpleador == "" ||
                                                            l.NombreEmpleador == "" ||
                                                            l.Cargo == "" ||
                                                            l.Region_Id == -1 ||
                                                            l.Provincia_Id == -1 ||
                                                            l.Comuna_Id == -1 ||
                                                            l.Direccion == "") > 0)
                            {
                                Controles.MostrarMensajeAlerta("Debe Completar los Antecedentes Laborales del Participante " + participante.NombreCliente);
                                return false;
                            }
                        }
                    }
                    if (participante.TipoPersona_Id == 1 && participante.PorcentajeDeuda > 0 && (participante.TipoParticipacion_Id == 1 || participante.TipoParticipacion_Id == 2))
                    {
                        oCliente.Rut = participante.Rut;
                        rCliente = nCliente.Buscar(oCliente);
                        if (rCliente.ResultadoGeneral)
                        {
                            oCliente = rCliente.Lista.FirstOrDefault();

                            if (oCliente == null)
                            {
                                Controles.MostrarMensajeError("Error al Validar al Participante " + oParticipante.NombreCliente);
                                return false;
                            }
                            else
                            {
                                if (oCliente.TipoPersona_Id == 1)//Personas Naturales
                                {
                                    if (oCliente.EstadoCivil_Id == -1)
                                    {
                                        Controles.MostrarMensajeAlerta("Debe Seleccionar el Estado Civil del Participante " + oParticipante.NombreCliente);
                                        return false;
                                    }
                                    if (oCliente.RegimenMatrimonial_Id == -1 && oCliente.EstadoCivil_Id == 1)//CASADO-SOC.CONYUGAL 
                                    {
                                        Controles.MostrarMensajeAlerta("Debe Seleccionar el Regimen Matrimonial del Participante " + oParticipante.NombreCliente);
                                        return false;
                                    }

                                    if (oCliente.Nacionalidad_Id == -1)
                                    {
                                        Controles.MostrarMensajeAlerta("Debe Seleccionar la Nacionalidad del Participante " + oParticipante.NombreCliente);
                                        return false;
                                    }
                                    if (oCliente.Sexo_Id == -1)
                                    {
                                        Controles.MostrarMensajeAlerta("Debe Seleccionar el Sexo del Participante " + oParticipante.NombreCliente);
                                        return false;
                                    }
                                    if (oCliente.NivelEducacional_Id == -1)
                                    {
                                        Controles.MostrarMensajeAlerta("Debe Seleccionar el Nivel Educacional del Participante " + oParticipante.NombreCliente);
                                        return false;
                                    }
                                    if (oCliente.Residencia_Id == -1)
                                    {
                                        Controles.MostrarMensajeAlerta("Debe Seleccionar la Residencia del Participante " + oParticipante.NombreCliente);
                                        return false;
                                    }
                                    if (oCliente.FechaNacimiento == null || oCliente.FechaNacimiento < new DateTime(1901, 1, 1))
                                    {
                                        Controles.MostrarMensajeAlerta("Debe Ingresar la Fecha de Nacimiento del Participante " + oParticipante.NombreCliente);
                                        return false;
                                    }
                                }
                                else//Persona Jurídica
                                {
                                    if (oCliente.FechaNacimiento == null || oCliente.FechaNacimiento < new DateTime(1901, 1, 1))
                                    {
                                        Controles.MostrarMensajeAlerta("Debe Ingresar la Fecha de Inicio de Actividades del Participante " + oParticipante.NombreCliente);
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Controles.MostrarMensajeError(rCliente.Mensaje);
                            return false;
                        }
                    }

                    if (participante.TipoPersona_Id == 1 && participante.PorcentajeDeuda > 0) //Validación Solo para el Deudor Principal y Codeudor
                    {

                        RentasClienteInfo oInfo = new RentasClienteInfo();
                        oInfo.RutCliente = participante.Rut;
                        NegRentasCliente oNegCliente = new NegRentasCliente();
                        Resultado<RentasClienteInfo> oResultado = new Resultado<RentasClienteInfo>();
                        oResultado = oNegCliente.Buscar(oInfo);
                        if (oResultado.ResultadoGeneral)
                        {
                            if (oResultado.Lista.Count == 0)
                            {
                                Controles.MostrarMensajeAlerta("Debe Ingresar la Renta del Participante " + participante.NombreCliente);
                                return false;
                            }
                        }
                    }
                    if (participante.TipoParticipacion_Id == 1)//Solo se valida Deudor Principal
                    {

                        Resultado<DireccionClienteInfo> rDireccion = new Resultado<DireccionClienteInfo>();
                        DireccionClienteInfo oDireccion = new DireccionClienteInfo();

                        oDireccion.Cliente_Id = participante.Rut;
                        rDireccion = nCliente.BuscarDireccion(oDireccion);
                        if (rDireccion.ResultadoGeneral)
                        {
                            if (rDireccion.Lista.Count(d => d.TipoDireccion_Id == 2) == 0)// se valida que el participante tenga el tipo de dirección envío de correspondencia ingresado
                            {
                                Controles.MostrarMensajeAlerta("Debe Ingresar la Dirección de Envío de Correspondencia para el Participante " + participante.NombreCliente);
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


                foreach (var tasacion in NegPropiedades.lstTasaciones)
                {

                    Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();
                    PropiedadInfo oPropiedad = new PropiedadInfo();

                    NegPropiedades nPropiedades = new NegPropiedades();

                    oPropiedad.Id = tasacion.Propiedad_Id;
                    rPropiedad = nPropiedades.BuscarPropiedad(oPropiedad);
                    if (!rPropiedad.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rPropiedad.Mensaje);
                        return false;
                    }

                    oPropiedad = rPropiedad.Lista.FirstOrDefault();




                    if (oPropiedad.Region_Id == -1 || oPropiedad.Provincia_Id == -1 || oPropiedad.Comuna_Id == -1 || oPropiedad.Via_Id == -1 || oPropiedad.Direccion == "")
                    {
                        Controles.MostrarMensajeAlerta("Debe Completal la Información secundaria de las Propidades en Garantía de la Solicitud");
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
                    Controles.MostrarMensajeError("Error al Validar la Propiedad");
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
        #region INGRESO DEUDA
        private void CargaClientes()
        {
            try
            {
                DatosParticipante.CargarParticipantes();
                if (NegParticipante.lstParticipantes.Count != 0)
                {
                    NegActivosCliente.lstActivosClienteInfo = new List<ActivosClienteInfo>();
                    NegPasivosCliente.lstPasivosClienteInfo = new List<PasivosClienteInfo>();
                    NegParticipante.ObjParticipante = new ParticipanteInfo();
                    Controles.CargarGrid(ref EstadoSituacion.AgdvParticipante, NegParticipante.lstParticipantes.Where(p => p.PorcentajeDeuda > 0).ToList<ParticipanteInfo>(), new string[] { "Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Participantes");
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
        protected void btnIngresoDeuda_Click(object sender, EventArgs e)
        {
            CargaClientes();
        }

        #endregion


    }
}