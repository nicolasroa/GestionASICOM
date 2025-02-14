using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.DatosGenerales
{
    public partial class ResumenResolucion : System.Web.UI.UserControl
    {
        private decimal DeudaMensualPrincipal = decimal.Zero;
        private decimal DeudaMensualCodeudor = decimal.Zero;
        private decimal DeudaMensualComplementada = decimal.Zero;
        private decimal DeudaMensualHipotecariaPrincipal = decimal.Zero;
        private decimal DeudaMensualHipotecariaCodeudor = decimal.Zero;
        private decimal DeudaMensualHipotecariaComplementada = decimal.Zero;
        public decimal UF = 0;


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");
            UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
            if (!Page.IsPostBack)
            {
                NegSolicitudes.objResumenIndicadores = new ResumenIndicadores();

                
                lblValorUF.Text = string.Format("{0:0,0.00}", UF);
                txtValorUF.Text = string.Format("{0:0,0}", UF).Replace(".", "");
                CargaParticipantes();
                CargaDetalleParticipantes();
                CargarActivosSolicitud();
                CargarPasivosSolicitud();
                CargaIndicador();
                CargaDeudorGarantia();
                CargaAntecedentesOperacion();
                CargarPatrimonio();
                decimal DividendoRenta = 0;
                CargarRenta(ref DividendoRenta);
                CargaSeguros();

            }
        }

        public void ActualizarInformacion()
        {
            CargaParticipantes();
            CargaDetalleParticipantes();
            CargarActivosSolicitud();
            CargarPasivosSolicitud();
            CargaIndicador();
            CargaDeudorGarantia();
            CargaAntecedentesOperacion();
            CargarPatrimonio();
            decimal DividendoRenta = 0;
            CargarRenta(ref DividendoRenta);
            CargaSeguros();
        }

        #endregion

        #region Metodos

        private void CargaParticipantes()
        {

            try
            {
                BandejaEntradaInfo oBandeja = new BandejaEntradaInfo();
                oBandeja = NegBandejaEntrada.oBandejaEntrada;
                var oInfo = new ParticipanteInfo();

                oInfo.Solicitud_Id = oBandeja.Solicitud_Id;

                var ObjResultado = new Resultado<ParticipanteInfo>();
                var objNegocio = new NegParticipante();

                ////Asignacion de Variables
                ObjResultado = objNegocio.BuscarParticipante(oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gdvParticipantes, ObjResultado.Lista, new string[] { "Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
                }
            }

        }
        private void CargaDetalleParticipantes()
        {

            try
            {
                var StrHtml = "";
                NegClientes nCliente = new NegClientes();
                ClientesInfo oCliente = new ClientesInfo();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();

                if (NegParticipante.lstParticipantes == null)
                {
                    NegParticipante nParticipantes = new NegParticipante();
                    Resultado<ParticipanteInfo> rParticipantes = new Resultado<ParticipanteInfo>();
                    ParticipanteInfo oParticipante = new ParticipanteInfo();
                    oParticipante.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                    rParticipantes = nParticipantes.BuscarParticipante(oParticipante);

                    if (!rParticipantes.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(rParticipantes.Mensaje);
                        return;
                    }
                }

                foreach (var participante in NegParticipante.lstParticipantes)
                {

                    nCliente = new NegClientes();
                    oCliente = new ClientesInfo();
                    rCliente = new Resultado<ClientesInfo>();

                    oCliente.Rut = participante.Rut;
                    rCliente = nCliente.Buscar(oCliente);
                    if (rCliente.ResultadoGeneral)
                    {
                        oCliente = rCliente.Lista.FirstOrDefault();


                        StrHtml = StrHtml + "<div class='panel panel-success'>";
                        StrHtml = StrHtml + "<div class='panel-heading'>";
                        StrHtml = StrHtml + "<h3 class='panel-title'>Antecedentes Personales(" + participante.DescripcionTipoParticipante + ")</h3>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='panel-body small'>";
                        if (oCliente.TipoPersona_Id == 1)//Persona Natural
                        {

                            StrHtml = StrHtml + "<div class='row'>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Rut'" + oCliente.Id.ToString() + ">Rut</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Rut'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.RutCompleto + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='nombre'" + oCliente.Id.ToString() + ">Nombre</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='nombre'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.NombreCompleto + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Nacionalidad'" + oCliente.Id.ToString() + ">Nacionalidad</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Nacionalidad'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.DescripcionNacionalidad + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Residencia'" + oCliente.Id.ToString() + ">Residencia</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Residencia'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.DescripcionResidencia + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Mail'" + oCliente.Id.ToString() + ">Mail</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Mail'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.Mail + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Sexo'" + oCliente.Id.ToString() + ">Sexo</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Sexo'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.DescripcionSexo + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";


                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='row'>";



                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='FechaNacimiento'" + oCliente.Id.ToString() + ">Fecha de Nacimiento</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='FechaNacimiento'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.FechaNacimiento.ToShortDateString() + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Celular'" + oCliente.Id.ToString() + ">Celular</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Celular'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.TelefonoMovil + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='EstadoCivil'" + oCliente.Id.ToString() + ">Estado Civil</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='EstadoCivil'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.DescripcionEstadoCivil + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='RegimenMatrimonial'" + oCliente.Id.ToString() + ">Régimen Matrimonial</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='RegimenMatrimonial'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.DescripcionRegimenMatrimonial + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='CargasFamiliares'" + oCliente.Id.ToString() + ">Cargas Familiares</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='CargasFamiliares'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.CargasFamiliares.ToString() + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";


                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Edad'" + oCliente.Id.ToString() + ">Edad</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Edad'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.Edad.ToString() + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";


                            StrHtml = StrHtml + "</div>";





                            StrHtml = StrHtml + "<div class='row'>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='NivelEducacional'" + oCliente.Id.ToString() + ">Nivel Educacional</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='NivelEducacional'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.DescripcionNivelEducacional + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Hijos'" + oCliente.Id.ToString() + ">N° de Hijos</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Hijos'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.NumeroHijos.ToString() + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='ActEconomica'" + oCliente.Id.ToString() + ">Cod. Act. Económica</label>";
                            if (oCliente.ActividadSii1_Id != -1)
                                StrHtml = StrHtml + "<input class='form-control' disabled id='ActEconomica'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.ActividadSii1_Id.ToString() + "'>";
                            else
                                StrHtml = StrHtml + "<input class='form-control' disabled id='ActEconomica'" + oCliente.Id.ToString() + " type='text' value=''>";

                            StrHtml = StrHtml + "</div>";


                            StrHtml = StrHtml + "</div>";



                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Actividad'" + oCliente.Id.ToString() + ">Actividad</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Actividad'" + oCliente.Id.ToString() + " type='text' value='" + participante.DescripcionActividadSii1 + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='ProfesionOficio'" + oCliente.Id.ToString() + ">Profesión/Oficio</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='ProfesionOficio'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.DescripcionProfesion + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-2'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='EdadPlazo'" + oCliente.Id.ToString() + ">Edad + Plazo</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='EdadPlazo'" + oCliente.Id.ToString() + " type='text' value='" + participante.EdadPlazo.ToString() + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";




                            StrHtml = StrHtml + "</div>";

                        }

                        else //Persona Jurídica
                        {

                            StrHtml = StrHtml + "<div class='row'>";

                            StrHtml = StrHtml + "<div class='col-lg-3'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Rut'" + oCliente.Id.ToString() + ">Rut</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Rut'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.RutCompleto + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-3'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='Nombre'" + oCliente.Id.ToString() + ">Nombre</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='Nombre'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.NombreCompleto + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-3'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='GiroComercial'" + oCliente.Id.ToString() + ">Giro Comercial</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='GiroComercial'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.Paterno + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";

                            StrHtml = StrHtml + "<div class='col-lg-3'>";
                            StrHtml = StrHtml + "<div class='form-group'>";
                            StrHtml = StrHtml + "<label class='control-label' for='InicioActividades'" + oCliente.Id.ToString() + ">Inicio de Actividades</label>";
                            StrHtml = StrHtml + "<input class='form-control' disabled id='InicioActividades'" + oCliente.Id.ToString() + " type='text' value='" + oCliente.FechaNacimiento.ToShortDateString() + "'>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "</div>";


                            StrHtml = StrHtml + "</div>";

                        }
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "</div>";

                        if (oCliente.TipoPersona_Id == 1)//Persona Natural
                        {
                            StrHtml = StrHtml + "<div class='panel panel-success'>";
                            StrHtml = StrHtml + "<div class='panel-heading'>";
                            StrHtml = StrHtml + "<h3 class='panel-title'>Antecedentes Laborales(" + participante.DescripcionTipoParticipante + ")</h3>";
                            StrHtml = StrHtml + "</div>";
                            StrHtml = StrHtml + "<div class='panel-body'>";


                            AntecedentesLaboralesInfo oFiltro = new AntecedentesLaboralesInfo();
                            NegParticipante nLaborales = new NegParticipante();
                            Resultado<AntecedentesLaboralesInfo> rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                            oFiltro.RutCliente = participante.Rut;
                            rLaborales = nLaborales.BuscarAntecedentesLaborales(oFiltro);
                            if (rLaborales.ResultadoGeneral)
                            {
                                foreach (var laboral in rLaborales.Lista)
                                {


                                    StrHtml = StrHtml + "<div class='row'>";

                                    StrHtml = StrHtml + "<div class='col-lg-3'>";
                                    StrHtml = StrHtml + "<div class='form-group'>";
                                    StrHtml = StrHtml + "<label class='control-label' for='Empleador'" + laboral.Id.ToString() + ">Empleador</label>";
                                    StrHtml = StrHtml + "<input class='form-control' disabled id='Empleador'" + laboral.Id.ToString() + " type='text' value='" + laboral.NombreEmpleador + "'>";
                                    StrHtml = StrHtml + "</div>";
                                    StrHtml = StrHtml + "</div>";

                                    StrHtml = StrHtml + "<div class='col-lg-3'>";
                                    StrHtml = StrHtml + "<div class='form-group'>";
                                    StrHtml = StrHtml + "<label class='control-label' for='RutEmpleador'" + laboral.Id.ToString() + ">Rut Empleador</label>";
                                    StrHtml = StrHtml + "<input class='form-control' disabled id='RutEmpleador'" + laboral.Id.ToString() + " type='text' value='" + laboral.RutEmpleador + "'>";
                                    StrHtml = StrHtml + "</div>";
                                    StrHtml = StrHtml + "</div>";

                                    StrHtml = StrHtml + "<div class='col-lg-3'>";
                                    StrHtml = StrHtml + "<div class='form-group'>";
                                    StrHtml = StrHtml + "<label class='control-label' for='TipoActividad'" + laboral.Id.ToString() + ">Tipo de Actividad</label>";
                                    StrHtml = StrHtml + "<input class='form-control' disabled id='TipoActividad'" + laboral.Id.ToString() + " type='text' value='" + laboral.DescripcionTipoActividad + "(" + laboral.DescripcionSituacionLaboral + ")'>";
                                    StrHtml = StrHtml + "</div>";
                                    StrHtml = StrHtml + "</div>";

                                    StrHtml = StrHtml + "<div class='col-lg-3'>";
                                    StrHtml = StrHtml + "<div class='form-group'>";
                                    StrHtml = StrHtml + "<label class='control-label' for='FechaInicio'" + laboral.Id.ToString() + ">Fecha Inicio</label>";
                                    StrHtml = StrHtml + "<input class='form-control' disabled id='FechaInicio'" + laboral.Id.ToString() + " type='text' value='" + (laboral.FechaInicioContrato == null ? "No INgresada" : laboral.FechaInicioContrato.Value.ToShortDateString()) + "'>";
                                    StrHtml = StrHtml + "</div>";
                                    StrHtml = StrHtml + "</div>";

                                    StrHtml = StrHtml + "<div class='col-lg-3'>";
                                    StrHtml = StrHtml + "<div class='form-group'>";
                                    StrHtml = StrHtml + "<label class='control-label' for='FechaTermino'" + laboral.Id.ToString() + ">Fecha Término</label>";
                                    StrHtml = StrHtml + "<input class='form-control' disabled id='FechaTermino'" + laboral.Id.ToString() + " type='text' value='" + (laboral.FechaTerminoContrato == null ? "No Ingresada" : laboral.FechaTerminoContrato.Value.ToShortDateString()) + "'>";
                                    StrHtml = StrHtml + "</div>";
                                    StrHtml = StrHtml + "</div>";

                                    StrHtml = StrHtml + "<div class='col-lg-3'>";
                                    StrHtml = StrHtml + "<div class='form-group'>";
                                    StrHtml = StrHtml + "<label class='control-label' for='Cargo'" + laboral.Id.ToString() + ">Cargo</label>";
                                    StrHtml = StrHtml + "<input class='form-control' disabled id='Cargo'" + laboral.Id.ToString() + " type='text' value='" + laboral.Cargo + "'>";
                                    StrHtml = StrHtml + "</div>";
                                    StrHtml = StrHtml + "</div>";

                                    StrHtml = StrHtml + "<div class='col-lg-3'>";
                                    StrHtml = StrHtml + "<div class='form-group'>";
                                    StrHtml = StrHtml + "<label class='control-label' for='Direccion'" + laboral.Id.ToString() + ">Dirección</label>";
                                    StrHtml = StrHtml + "<input class='form-control' disabled id='Direccion'" + laboral.Id.ToString() + " type='text' value='" + laboral.Direccion + "'>";
                                    StrHtml = StrHtml + "</div>";
                                    StrHtml = StrHtml + "</div>";


                                    StrHtml = StrHtml + "</div>";

                                }
                                StrHtml = StrHtml + "</div>";
                                StrHtml = StrHtml + "</div>";
                            }
                            else
                            {
                                Controles.MostrarMensajeError(rLaborales.Mensaje);
                            }
                            StrHtml = StrHtml + "</tr>";
                            StrHtml = StrHtml + "<tr>";
                        }


                    }
                }
                DivParticipantes.InnerHtml = StrHtml;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los participantes");
                }
            }


        }


        private void CargarActivosSolicitud()
        {
            try
            {
                var StrHtml = "";





                ActivosSolicitudInfo oActivosSolicitud = new ActivosSolicitudInfo();
                NegActivosCliente nActivosSolicitud = new NegActivosCliente();
                Resultado<ActivosSolicitudInfo> rActivosSolicitud = new Resultado<ActivosSolicitudInfo>();
                oActivosSolicitud.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rActivosSolicitud = nActivosSolicitud.BuscarActivosSolicitud(oActivosSolicitud);
                decimal TotalActivosDeudor = 0;
                decimal TotalActivosCodeudor = 0;
                decimal TotalActivosAval = 0;

                if (rActivosSolicitud.ResultadoGeneral)
                {
                    foreach (var activos in rActivosSolicitud.Lista)
                    {






                        StrHtml = StrHtml + "<div class='row'>";
                        StrHtml = StrHtml + "<div class='col-xl-3 col-lg-3 col-md-3 col-xs-3 text-left'>";
                        StrHtml = StrHtml + "<label>" + activos.Descripcion + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:C}", activos.ActivosDeudor) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:N4}", activos.ActivosDeudor / UF) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:C}", activos.ActivosCodeudor) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:N4}", activos.ActivosCodeudor / UF) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:C}", activos.ActivosAval) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:N4}", activos.ActivosAval / UF) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "</div>";






                       

                        TotalActivosDeudor = TotalActivosDeudor + activos.ActivosDeudor;
                        TotalActivosCodeudor = TotalActivosCodeudor + activos.ActivosCodeudor;
                        TotalActivosAval = TotalActivosAval + activos.ActivosAval;


                    }

                    lblTotalActivosDeudor.Text = string.Format("{0:C}", TotalActivosDeudor);
                    lblTotalActivosDeudorUF.Text = string.Format("{0:N4}", TotalActivosDeudor / UF);
                    lblTotalActivosCodeudor.Text = string.Format("{0:C}", TotalActivosCodeudor);
                    lblTotalActivosCodeudorUF.Text = string.Format("{0:N4}", TotalActivosCodeudor / UF);
                    lblTotalActivosAval.Text = string.Format("{0:C}", TotalActivosAval);
                    lblTotalActivosAvalUF.Text = string.Format("{0:N4}", TotalActivosAval / UF);

                    decimal TotalActivos = TotalActivosDeudor + TotalActivosCodeudor + TotalActivosAval;

                    lblTotalActivosComplementados.Text = string.Format("{0:C}", TotalActivos);
                    lblTotalActivosComplementadosUF.Text = string.Format("{0:N4}", TotalActivos / UF);

                }
                else
                {
                    Controles.MostrarMensajeError(rActivosSolicitud.Mensaje);
                }



                DivActivosSolicitud.InnerHtml = StrHtml;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Activos Mensuales");
                }
            }
        }

        private void CargarPasivosSolicitud()
        {
            try
            {
                var StrHtml = "";





                PasivosSolicitudInfo oPasivosSolicitud = new PasivosSolicitudInfo();
                NegPasivosCliente nPasivosSolicitud = new NegPasivosCliente();
                Resultado<PasivosSolicitudInfo> rPasivosSolicitud = new Resultado<PasivosSolicitudInfo>();
                oPasivosSolicitud.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rPasivosSolicitud = nPasivosSolicitud.BuscarPasivosSolicitud(oPasivosSolicitud);
                decimal TotalPasivosDeudor = 0;
                decimal TotalPasivosCodeudor = 0;
                decimal TotalPasivosAval = 0;

                if (rPasivosSolicitud.ResultadoGeneral)
                {
                    foreach (var Pasivos in rPasivosSolicitud.Lista)
                    {

                        StrHtml = StrHtml + "<div class='row'>";
                        StrHtml = StrHtml + "<div class='col-xl-3 col-lg-3 col-md-3 col-xs-3 text-left'>";
                        StrHtml = StrHtml + "<label>" + Pasivos.Descripcion + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:C}", Pasivos.PasivosDeudor) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:N4}", Pasivos.PasivosDeudor / UF) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:C}", Pasivos.PasivosCodeudor) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:N4}", Pasivos.PasivosCodeudor / UF) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-2 col-lg-2 col-md-2 col-xs-2 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:C}", Pasivos.PasivosAval) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "<div class='col-xl-1 col-lg-1 col-md-1 col-xs-1 text-center'>";
                        StrHtml = StrHtml + "<label>" + string.Format("{0:N4}", Pasivos.PasivosAval / UF) + "</label>";
                        StrHtml = StrHtml + "</div>";
                        StrHtml = StrHtml + "</div>";

                        TotalPasivosDeudor = TotalPasivosDeudor + Pasivos.PasivosDeudor;
                        TotalPasivosCodeudor = TotalPasivosCodeudor + Pasivos.PasivosCodeudor;
                        TotalPasivosAval = TotalPasivosAval + Pasivos.PasivosAval;


                    }

                    lblTotalPasivosDeudor.Text = string.Format("{0:C}", TotalPasivosDeudor);
                    lblTotalPasivosDeudorUF.Text = string.Format("{0:N4}", TotalPasivosDeudor / UF);
                    lblTotalPasivosCodeudor.Text = string.Format("{0:C}", TotalPasivosCodeudor);
                    lblTotalPasivosCodeudorUF.Text = string.Format("{0:N4}", TotalPasivosCodeudor / UF);
                    lblTotalPasivosAval.Text = string.Format("{0:C}", TotalPasivosAval);
                    lblTotalPasivosAvalUF.Text = string.Format("{0:N4}", TotalPasivosAval / UF);

                    decimal TotalPasivos = TotalPasivosDeudor + TotalPasivosCodeudor + TotalPasivosAval;

                    lblTotalPasivosComplementados.Text = string.Format("{0:C}", TotalPasivos);
                    lblTotalPasivosComplementadosUF.Text = string.Format("{0:N4}", TotalPasivos / UF);
                }
                else
                {
                    Controles.MostrarMensajeError(rPasivosSolicitud.Mensaje);
                }



                DivPasivosSolicitud.InnerHtml = StrHtml;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar los Pasivos Mensuales");
                }
            }
        }



        private void CargarPatrimonio()
        {
            try
            {
                decimal UF;
                UF = NegParidad.ObtenerParidad((int)NegTablas.IdentificadorMaestro("MONEDAS", "UF"), DateTime.Now);

                decimal ActivosPrincipal = decimal.Zero;
                decimal PasivosPrincipal = decimal.Zero;
                decimal PatrimonioPrincipal = decimal.Zero;

                decimal ActivosCodeudor = decimal.Zero;
                decimal PasivosCodeudor = decimal.Zero;
                decimal PatrimonioCodeudor = decimal.Zero;

                decimal ActivosComplentado = decimal.Zero;
                decimal PasivosComplentado = decimal.Zero;
                decimal PatrimoniosComplentado = decimal.Zero;

                ActivosClienteInfo oActivo = new ActivosClienteInfo();
                PasivosClienteInfo oPasivo = new PasivosClienteInfo();

                NegActivosCliente nActivos = new NegActivosCliente();
                NegPasivosCliente nPasivos = new NegPasivosCliente();

                Resultado<ActivosClienteInfo> rActivos = new Resultado<ActivosClienteInfo>();
                Resultado<PasivosClienteInfo> rPasivos = new Resultado<PasivosClienteInfo>();



                foreach (var participante in NegParticipante.lstParticipantes)
                {
                    if (participante.TipoParticipacion_Id == 1 && participante.PorcentajeDeuda > 0)//Deudor Principal
                    {
                        //ACTIVOS
                        oActivo.RutCliente = participante.Rut;
                        rActivos = nActivos.BuscarTotal(oActivo);
                        if (rActivos.ResultadoGeneral)
                        {
                            ActivosPrincipal = ActivosPrincipal + rActivos.Lista.FirstOrDefault().TotalUF * UF;
                            ActivosPrincipal = ActivosPrincipal + rActivos.Lista.FirstOrDefault().TotalPesos;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(rActivos.Mensaje);
                            return;
                        }

                        //PASIVOS
                        oPasivo.RutCliente = participante.Rut;
                        rPasivos = nPasivos.BuscarTotal(oPasivo);
                        if (rPasivos.ResultadoGeneral)
                        {
                            PasivosPrincipal = PasivosPrincipal + rPasivos.Lista.FirstOrDefault().TotalUF * UF;
                            PasivosPrincipal = PasivosPrincipal + rPasivos.Lista.FirstOrDefault().TotalPesos;

                            DeudaMensualPrincipal = DeudaMensualPrincipal + rPasivos.Lista.FirstOrDefault().TotalCuotaUF * UF;
                            DeudaMensualPrincipal = DeudaMensualPrincipal + rPasivos.Lista.FirstOrDefault().TotalCuotaPesos;

                            DeudaMensualHipotecariaPrincipal = DeudaMensualHipotecariaPrincipal + rPasivos.Lista.FirstOrDefault().TotalDeudaHipotecariaCuotaUF * UF;
                            DeudaMensualHipotecariaPrincipal = DeudaMensualHipotecariaPrincipal + rPasivos.Lista.FirstOrDefault().TotalDeudaHipotecariaCuotaPesos;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(rPasivos.Mensaje);
                            return;
                        }
                    }

                    if (participante.TipoParticipacion_Id == 2 && participante.PorcentajeDeuda > 0)//Codeudor
                    {
                        //ACTIVOS
                        oActivo.RutCliente = participante.Rut;
                        rActivos = nActivos.BuscarTotal(oActivo);
                        if (rActivos.ResultadoGeneral)
                        {
                            ActivosCodeudor = ActivosCodeudor + rActivos.Lista.FirstOrDefault().TotalUF * UF;
                            ActivosCodeudor = ActivosCodeudor + rActivos.Lista.FirstOrDefault().TotalPesos;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(rActivos.Mensaje);
                            return;
                        }

                        //PASIVOS
                        oPasivo.RutCliente = participante.Rut;
                        rPasivos = nPasivos.BuscarTotal(oPasivo);
                        if (rPasivos.ResultadoGeneral)
                        {
                            PasivosCodeudor = PasivosCodeudor + rPasivos.Lista.FirstOrDefault().TotalUF * UF;
                            PasivosCodeudor = PasivosCodeudor + rPasivos.Lista.FirstOrDefault().TotalPesos;

                            DeudaMensualCodeudor = DeudaMensualCodeudor + rPasivos.Lista.FirstOrDefault().TotalCuotaUF * UF;
                            DeudaMensualCodeudor = DeudaMensualCodeudor + rPasivos.Lista.FirstOrDefault().TotalCuotaPesos;

                            DeudaMensualHipotecariaCodeudor = DeudaMensualHipotecariaCodeudor + rPasivos.Lista.FirstOrDefault().TotalDeudaHipotecariaCuotaUF * UF;
                            DeudaMensualHipotecariaCodeudor = DeudaMensualHipotecariaCodeudor + rPasivos.Lista.FirstOrDefault().TotalDeudaHipotecariaCuotaPesos;
                        }
                        else
                        {
                            Controles.MostrarMensajeError(rPasivos.Mensaje);
                            return;
                        }
                    }



                    if (participante.PorcentajeDeuda > 0)//Complementada
                    {
                        //ACTIVOS
                        oActivo.RutCliente = participante.Rut;
                        rActivos = nActivos.BuscarTotal(oActivo);
                        if (rActivos.ResultadoGeneral)
                        {
                            ActivosComplentado = ActivosComplentado + rActivos.Lista.FirstOrDefault().TotalUF * UF;
                            ActivosComplentado = ActivosComplentado + rActivos.Lista.FirstOrDefault().TotalPesos;


                        }
                        else
                        {
                            Controles.MostrarMensajeError(rActivos.Mensaje);
                            return;
                        }


                        //PASIVOS
                        oPasivo.RutCliente = participante.Rut;
                        rPasivos = nPasivos.BuscarTotal(oPasivo);
                        if (rPasivos.ResultadoGeneral)
                        {
                            PasivosComplentado = PasivosComplentado + rPasivos.Lista.FirstOrDefault().TotalUF * UF;
                            PasivosComplentado = PasivosComplentado + rPasivos.Lista.FirstOrDefault().TotalPesos;
                            DeudaMensualComplementada = DeudaMensualComplementada + rPasivos.Lista.FirstOrDefault().TotalCuotaUF * UF;
                            DeudaMensualComplementada = DeudaMensualComplementada + rPasivos.Lista.FirstOrDefault().TotalCuotaPesos;

                            DeudaMensualHipotecariaComplementada = DeudaMensualHipotecariaComplementada + rPasivos.Lista.FirstOrDefault().TotalDeudaHipotecariaCuotaUF * UF;
                            DeudaMensualHipotecariaComplementada = DeudaMensualHipotecariaComplementada + rPasivos.Lista.FirstOrDefault().TotalDeudaHipotecariaCuotaPesos;

                        }
                        else
                        {
                            Controles.MostrarMensajeError(rPasivos.Mensaje);
                            return;
                        }
                    }

                }
                if (ActivosPrincipal > 0)
                {
                    lblTotalActivosPrincipalMO.Text = "UF " + String.Format("{0:N4}", ActivosPrincipal / UF);
                    lblTotalActivosPrincipalCLP.Text = String.Format("{0:C}", ActivosPrincipal);

                    NegSolicitudes.objResumenIndicadores.ActivosPrincipal = "UF " + String.Format("{0:N4}", ActivosPrincipal / UF);
                    NegSolicitudes.objResumenIndicadores.ActivosPrincipalPesos = String.Format("{0:C}", ActivosPrincipal);
                }

                if (PasivosPrincipal > 0)
                {
                    lblTotalPasivosPrincipalMO.Text = "UF " + String.Format("{0:N4}", PasivosPrincipal / UF);
                    lblTotalPasivosPrincipalCLP.Text = String.Format("{0:C}", PasivosPrincipal);

                    NegSolicitudes.objResumenIndicadores.PasivosPrincipal = "UF " + String.Format("{0:N4}", PasivosPrincipal / UF);
                    NegSolicitudes.objResumenIndicadores.PasivosPrincipalPesos = String.Format("{0:C}", PasivosPrincipal);

                }


                if (ActivosComplentado > 0)
                {
                    lblTotalActivosComplementadoMO.Text = "UF " + String.Format("{0:N4}", ActivosComplentado / UF);
                    lblTotalActivosComplementadoCLP.Text = String.Format("{0:C}", ActivosComplentado);

                    NegSolicitudes.objResumenIndicadores.ActivosComplementados = "UF " + String.Format("{0:N4}", ActivosComplentado / UF);
                    NegSolicitudes.objResumenIndicadores.ActivosComplementadosPesos = String.Format("{0:C}", ActivosComplentado);

                }

                if (PasivosComplentado > 0)
                {
                    lblTotalPasivosComplementadoMO.Text = "UF " + String.Format("{0:N4}", PasivosComplentado / UF);
                    lblTotalPasivosComplementadoCLP.Text = String.Format("{0:C}", PasivosComplentado);

                    NegSolicitudes.objResumenIndicadores.PasivosComplementados = "UF " + String.Format("{0:N4}", PasivosComplentado / UF);
                    NegSolicitudes.objResumenIndicadores.PasivosComplementadosPesos = String.Format("{0:C}", PasivosComplentado);

                }


                PatrimonioPrincipal = ActivosPrincipal - PasivosPrincipal;
                PatrimonioCodeudor = ActivosCodeudor - PasivosCodeudor;
                PatrimoniosComplentado = ActivosComplentado - PasivosComplentado;

                if (PatrimonioPrincipal != 0)
                {
                    lblPatrimonioPrincipal.Text = "UF " + String.Format("{0:N4}", PatrimonioPrincipal / UF);
                    lblPatrimonioPrincipalCLP.Text = String.Format("{0:C}", PatrimonioPrincipal);
                    NegSolicitudes.objResumenIndicadores.PatrimonioPrincipal = "UF " + String.Format("{0:N4}", PatrimonioPrincipal / UF);
                    NegSolicitudes.objResumenIndicadores.PatrimonioPrincipalPesos = String.Format("{0:C}", PatrimonioPrincipal);



                }
                if (PatrimoniosComplentado != 0)
                {
                    lblPatrimonioComplementado.Text = "UF " + String.Format("{0:N4}", PatrimoniosComplentado / UF);
                    lblPatrimonioComplementadoCLP.Text = String.Format("{0:C}", PatrimoniosComplentado);

                    NegSolicitudes.objResumenIndicadores.PatrimonioComplementado = "UF " + String.Format("{0:N4}", PatrimoniosComplentado / UF);
                    NegSolicitudes.objResumenIndicadores.PatrimonioComplementadoPesos = String.Format("{0:C}", PatrimoniosComplentado);


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
                    Controles.MostrarMensajeError("Error al Cargar Patrimonio");
                }
            }
        }

        public void CargarRenta(ref decimal DividendoRenta)
        {
            try
            {
                decimal UF;
                UF = NegParidad.ObtenerParidad((int)NegTablas.IdentificadorMaestro("MONEDAS", "UF"), DateTime.Now);

                decimal RentaPrincipal = decimal.Zero;
                decimal RentaCodeudor = decimal.Zero;
                decimal RentaComplementada = decimal.Zero;
                decimal DividendoRentaPrincipal = decimal.Zero;
                decimal DividendoRentaCodeudor = decimal.Zero;
                decimal DividendoRentaComplementada = decimal.Zero;


                RentasClienteInfo oRenta = new RentasClienteInfo();
                NegRentasCliente nRenta = new NegRentasCliente();
                Resultado<RentasClienteInfo> rRenta = new Resultado<RentasClienteInfo>();

                foreach (var participante in NegParticipante.lstParticipantes)
                {
                    if (participante.TipoParticipacion_Id == 1 && participante.PorcentajeDeuda > 0)//Deudor Principal
                    {
                        oRenta.RutCliente = participante.Rut;
                        rRenta = nRenta.BuscarPromedio(oRenta);
                        if (rRenta.ResultadoGeneral)
                        {
                            if (rRenta.Lista.Count > 0)
                                RentaPrincipal = RentaPrincipal + rRenta.Lista.FirstOrDefault().RentaPromedio;
                            else
                                RentaPrincipal = RentaPrincipal + 0;

                        }
                    }
                    if (participante.TipoParticipacion_Id == 2 && participante.PorcentajeDeuda > 0)//Codeudor
                    {
                        oRenta.RutCliente = participante.Rut;
                        rRenta = nRenta.BuscarPromedio(oRenta);
                        if (rRenta.ResultadoGeneral)
                        {
                            if (rRenta.Lista.Count > 0)
                                RentaCodeudor = RentaCodeudor + rRenta.Lista.FirstOrDefault().RentaPromedio;
                            else
                                RentaCodeudor = RentaCodeudor + 0;

                        }
                    }

                    if (participante.PorcentajeDeuda > 0)//Complementada
                    {
                        oRenta.RutCliente = participante.Rut;
                        rRenta = nRenta.BuscarPromedio(oRenta);
                        if (rRenta.ResultadoGeneral)
                        {
                            if (rRenta.Lista.Count > 0)
                                RentaComplementada = RentaComplementada + rRenta.Lista.FirstOrDefault().RentaPromedio;
                            else
                                RentaComplementada = RentaComplementada + 0;
                        }
                    }
                }

                if (RentaPrincipal != 0)
                {
                    lblRentaPrincipal.Text = "UF " + String.Format("{0:N4}", RentaPrincipal / UF);
                    lblRentaPrincipalCLP.Text = String.Format("{0:C}", RentaPrincipal);

                    DividendoRentaPrincipal = (NegSolicitudes.objSolicitudInfo.ValorDividendoPesos / RentaPrincipal) * 100;
                    lblDividendoRentaPrincipalMO.Text = String.Format("{0:F2}", DividendoRentaPrincipal) + "%";

                    NegSolicitudes.objResumenIndicadores.RentaPrincipal = "UF " + String.Format("{0:N4}", RentaPrincipal / UF);
                    NegSolicitudes.objResumenIndicadores.RentaPrincipalPesos = String.Format("{0:C}", RentaPrincipal);
                    NegSolicitudes.objResumenIndicadores.DividendoRentaPrincipal = String.Format("{0:F2}", DividendoRentaPrincipal) + "%";


                    if (DividendoRentaPrincipal > 25)
                        lblDividendoRentaPrincipalMO.ForeColor = System.Drawing.Color.Red;
                    else
                        lblDividendoRentaPrincipalMO.ForeColor = System.Drawing.Color.Black;



                    var CargaPrincipal = ((NegSolicitudes.objSolicitudInfo.ValorDividendoPesos + DeudaMensualPrincipal) / RentaPrincipal) * 100;

                    lblCargaFinancieraPrincipalMO.Text = String.Format("{0:F2}", CargaPrincipal) + "%";
                    if (CargaPrincipal > 60)
                        lblCargaFinancieraPrincipalMO.ForeColor = System.Drawing.Color.Red;
                    else
                        lblCargaFinancieraPrincipalMO.ForeColor = System.Drawing.Color.Black;


                    NegSolicitudes.objResumenIndicadores.CargaFinancieraPrincipal = String.Format("{0:F2}", CargaPrincipal) + "%";

                    var CargaHipotecariaPrincipal = ((NegSolicitudes.objSolicitudInfo.ValorDividendoPesos + DeudaMensualHipotecariaPrincipal) / RentaPrincipal) * 100;
                    lblCargaFinancieraHipotecariaPrincipalMO.Text = String.Format("{0:F2}", CargaHipotecariaPrincipal) + "%";
                    NegSolicitudes.objResumenIndicadores.CargaFinancieraHipotecariaPrincipal = String.Format("{0:F2}", CargaHipotecariaPrincipal) + "%";


                }
                lblRentaCodeudor.Text = "No Ingresada";
                lblRentaCodeudorCLP.Text = "No Ingresada";
                lblDividendoRentaCodeudorlMO.Text = "No Registrada";
                lblCargaFinancieraHipotecariaCodeudorMO.Text = "No Registrada";
                if (RentaCodeudor != 0)
                {

                    lblRentaCodeudor.Text = "UF " + String.Format("{0:N4}", RentaCodeudor / UF);
                    lblRentaCodeudorCLP.Text = String.Format("{0:C}", RentaCodeudor);


                    DividendoRentaCodeudor = (NegSolicitudes.objSolicitudInfo.ValorDividendoPesos / RentaCodeudor) * 100;
                    lblDividendoRentaCodeudorlMO.Text = String.Format("{0:F2}", DividendoRentaCodeudor) + "%";

                    NegSolicitudes.objResumenIndicadores.RentaCodeudor = "UF " + String.Format("{0:N4}", RentaCodeudor / UF);
                    NegSolicitudes.objResumenIndicadores.RentaCodeudorPesos = String.Format("{0:C}", RentaCodeudor);
                    NegSolicitudes.objResumenIndicadores.DividendoRentaCodeudor = String.Format("{0:F2}", DividendoRentaCodeudor) + "%";

                    if (DividendoRentaCodeudor > 25)
                        lblDividendoRentaCodeudorlMO.ForeColor = System.Drawing.Color.Red;
                    else
                        lblDividendoRentaCodeudorlMO.ForeColor = System.Drawing.Color.Black;


                    var CargaHipotecariaCodeudor = ((NegSolicitudes.objSolicitudInfo.ValorDividendoPesos + DeudaMensualHipotecariaCodeudor) / RentaComplementada) * 100;
                    lblCargaFinancieraHipotecariaCodeudorMO.Text = String.Format("{0:F2}", CargaHipotecariaCodeudor) + "%";
                    NegSolicitudes.objResumenIndicadores.CargaFinancieraHipotecariaCodeudor = String.Format("{0:F2}", CargaHipotecariaCodeudor) + "%";

                }



                if (RentaComplementada != 0)
                {
                    lblRentaComplementada.Text = "UF " + String.Format("{0:N4}", RentaComplementada / UF);
                    lblRentaComplementadaCLP.Text = String.Format("{0:C}", RentaComplementada);
                    DividendoRentaComplementada = (NegSolicitudes.objSolicitudInfo.ValorDividendoPesos / RentaComplementada) * 100;
                    lblDividendoRentaComplementadaMO.Text = String.Format("{0:F2}", DividendoRentaComplementada) + "%";

                    NegSolicitudes.objResumenIndicadores.RentaComplementada = "UF " + String.Format("{0:N4}", RentaComplementada / UF);
                    NegSolicitudes.objResumenIndicadores.RentaComplementadaPesos = String.Format("{0:C}", RentaComplementada);
                    NegSolicitudes.objResumenIndicadores.DividendoRentaComplementada = String.Format("{0:F2}", DividendoRentaComplementada) + "%";


                    if (DividendoRentaComplementada > 25)
                        lblDividendoRentaComplementadaMO.ForeColor = System.Drawing.Color.Red;
                    else
                        lblDividendoRentaComplementadaMO.ForeColor = System.Drawing.Color.Black;

                    DividendoRenta = DividendoRentaComplementada;


                    var CargaComplementada = ((NegSolicitudes.objSolicitudInfo.ValorDividendoPesos + DeudaMensualComplementada) / RentaComplementada) * 100;
                    lblCargaFinancieraComplementadaMO.Text = String.Format("{0:F2}", CargaComplementada) + "%";
                    if (CargaComplementada > 60)
                        lblCargaFinancieraComplementadaMO.ForeColor = System.Drawing.Color.Red;
                    else
                        lblCargaFinancieraComplementadaMO.ForeColor = System.Drawing.Color.Black;


                    NegSolicitudes.objResumenIndicadores.CargaFinancieraComplementada = String.Format("{0:F2}", CargaComplementada) + "%";

                    var CargaHipotecariaComplementada = ((NegSolicitudes.objSolicitudInfo.ValorDividendoPesos + DeudaMensualHipotecariaComplementada) / RentaComplementada) * 100;
                    lblCargaFinancieraHipotecariaComplementadaMO.Text = String.Format("{0:F2}", CargaHipotecariaComplementada) + "%";
                    NegSolicitudes.objResumenIndicadores.CargaFinancieraHipotecariaComplementada = String.Format("{0:F2}", CargaHipotecariaComplementada) + "%";
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
                    Controles.MostrarMensajeError("Error al Cargar Renta");
                }
            }
        }



        private void CargaIndicador()
        {

            CargarInformacionSolicitud();

            //Dividendo Neto
            lblMtoDividendoNetoMO.Text = "UF " + string.Format("{0:N4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUF);
            lblMtoDividendoNetoCLP.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesos);

            //Dividendo con Seguros
            lblMtoDividendoConSeguroMO.Text = "UF " + String.Format("{0:N4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUfTotal);
            lblMtoDividendoConSeguroCLP.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesosTotal);

            //Renta Necesaria
            lblMtoRentaNecesariaMO.Text = "UF " + String.Format("{0:N4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUF * 4);
            lblMtoRentaNecesariaCLP.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesos * 4);


            NegSolicitudes.objResumenIndicadores.RentaNecesaria = "UF " + String.Format("{0:N4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUF * 4);
            NegSolicitudes.objResumenIndicadores.RentaNecesariaPesos = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesos * 4);




        }

        private void CargarInformacionSolicitud()
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
                        oSolicitud = rSolicitud.Lista.FirstOrDefault(s => s.Id == oSolicitud.Id);
                        NegSolicitudes.objSolicitudInfo = new SolicitudInfo();
                        NegSolicitudes.objSolicitudInfo = oSolicitud;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CargaDeudorGarantia()
        {
            alblMtoDeudaGarantiaMO.Text = String.Format("{0:F2}", 0) + "%";

            if (NegSolicitudes.objSolicitudInfo.MontoPropiedad != 0)
            {
                //Deuda/Garantia
                alblMtoDeudaGarantiaMO.Text = String.Format("{0:F2}", (NegSolicitudes.objSolicitudInfo.MontoCredito / NegSolicitudes.objSolicitudInfo.MontoPropiedad) * 100) + "%";
            }

        }
        private void CargaSeguros()
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();


                oSeguros.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rSeguros = nSeguros.BuscarSegurosContratados(oSeguros);
                if (rSeguros.ResultadoGeneral)
                    Controles.CargarGrid<SegurosContratadosInfo>(ref gvSeguros, rSeguros.Lista, new string[] { "Id" });
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

        private void CargaAntecedentesOperacion()
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
                        lblTipoFinanciamiento.Text = ObjInfo.DescripcionTipoFinanciamiento;
                        lblProducto.Text = ObjInfo.DescripcionProducto.ToString();
                        lblDestino.Text = ObjInfo.DescripcionDestino.ToString();
                        lblObjetivo.Text = ObjInfo.DescripcionObjetivo.ToString();

                        lblTasa.Text = String.Format("{0:F2}", ObjInfo.TasaFinal) + "%";


                        lblPlazo.Text = ObjInfo.Plazo.ToString();
                        lblGracia.Text = ObjInfo.Gracia == 0 ? "Sin Meses de Gracia" : ObjInfo.Gracia.ToString();
                        decimal UF;
                        UF = NegParidad.ObtenerParidad((int)NegTablas.IdentificadorMaestro("MONEDAS", "UF"), DateTime.Now);
                        lblValorVentaMO.Text = "UF " + String.Format("{0:N4}", ObjInfo.MontoPropiedad);
                        lblValorVentaCLP.Text = String.Format("{0:C}", ObjInfo.MontoPropiedad * UF);
                        lblMontoSubsidioMO.Text = "UF " + String.Format("{0:N4}", ObjInfo.MontoSubsidio);
                        lblMontoSubsidioCLP.Text = String.Format("{0:C}", ObjInfo.MontoSubsidio * UF);
                        lblMontoBonoIntegracionMO.Text = "UF " + String.Format("{0:N4}", ObjInfo.MontoBonoIntegracion);
                        lblMontoBonoIntegracionCLP.Text = String.Format("{0:C}", ObjInfo.MontoBonoIntegracion * UF);
                        lblMontoBonoCaptacionMO.Text = "UF " + String.Format("{0:N4}", ObjInfo.MontoBonoCaptacion);
                        lblMontoBonoCaptacionCLP.Text = String.Format("{0:C}", ObjInfo.MontoBonoCaptacion * UF);
                        lblMontoSolicitadoMO.Text = "UF " + String.Format("{0:N4}", ObjInfo.MontoCredito);
                        lblMontoSolicitadoCLP.Text = String.Format("{0:C}", ObjInfo.MontoCredito * UF);
                        lblPagoContadoMO.Text = "UF " + String.Format("{0:N4}", ObjInfo.MontoContado);
                        lblPagoContadoCLP.Text = String.Format("{0:C}", ObjInfo.MontoContado * UF);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Simular");
                }
            }

        }

        #endregion

    }
}