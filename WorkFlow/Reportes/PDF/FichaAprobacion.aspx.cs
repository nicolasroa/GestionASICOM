using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web;

namespace WorkFlow.Reportes.PDF
{
    public partial class FichaAprobacion : System.Web.UI.Page
    {
        public decimal UF = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int CodigoMoneda = (int)NegTablas.IdentificadorMaestro("MONEDAS", "UF");

                UF = NegParidad.ObtenerParidad(CodigoMoneda, DateTime.Now);
                CargarParticipantes();
                CargarFlujosMensuales();
                CargarActivosSolicitud();
                CargarPasivosSolicitud();
                CargarCredito();
                CargaSeguros();
                CargarPropiedades();
                CargarIndicadores();
                CargaEjecutivo();
                //CargaObservaciones();
               
            }
        }

        private void CargarParticipantes()
        {

            try
            {
                var StrHtml = "";
                NegClientes nCliente = new NegClientes();
                ClientesInfo oCliente = new ClientesInfo();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();

                
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

                        StrHtml = StrHtml + "<td class='tdTitulo02'>Antecedentes Personales(" + participante.DescripcionTipoParticipante + ")</td>";
                        StrHtml = StrHtml + "</tr>";
                        StrHtml = StrHtml + "<tr>";
                        StrHtml = StrHtml + "<td>";
                        StrHtml = StrHtml + "<table style = 'width: 100%;'>";
                        if (oCliente.TipoPersona_Id == 1)//Persona Natural
                        {
                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Nombre</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.NombreCompleto + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Nacionalidad</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.DescripcionNacionalidad + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Residencia</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.DescripcionResidencia + "</td>";
                            StrHtml = StrHtml + "</tr>";
                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Rut</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.RutCompleto + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Sexo</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.DescripcionSexo + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Fecha de Nacimiento</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.FechaNacimiento.ToShortDateString() + "</td>";
                            StrHtml = StrHtml + "</tr>";
                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Mail</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.Mail + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Celular</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.TelefonoMovil + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>N° de Hijos</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.NumeroHijos.ToString() + "</td>";
                            StrHtml = StrHtml + "</tr>";
                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Estado Civil</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.DescripcionEstadoCivil + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Régimen Matrimonial</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.DescripcionRegimenMatrimonial + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Cargas Familiares</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.CargasFamiliares.ToString() + "</td>";
                            StrHtml = StrHtml + "</tr>";
                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Edad</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.Edad.ToString() + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Nivel Educacional</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.DescripcionNivelEducacional + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Cod. Act. Económica</td>";
                            if (oCliente.ActividadSii1_Id == -1)
                                StrHtml = StrHtml + "<td class='tdInfoData'></td>";
                            else
                                StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.ActividadSii1_Id.ToString() + "</td>";
                            StrHtml = StrHtml + "</tr>";
                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Profesión/Oficio</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.DescripcionProfesion + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Edad + Plazo</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + participante.EdadPlazo.ToString() + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Actividad</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + participante.DescripcionActividadSii1 + "</td>";
                            StrHtml = StrHtml + "</tr>";
                        }

                        else //Persona Jurídica
                        {

                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Rut</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.RutCompleto + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'></td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'></td>";
                            StrHtml = StrHtml + "<td class='tdInfo'></td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'></td>";
                            StrHtml = StrHtml + "</tr>";
                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Nombre</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.NombreCompleto + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Giro Comercial</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.Paterno + "</td>";
                            StrHtml = StrHtml + "<td class='tdInfo'>Inicio de Actividades</td>";
                            StrHtml = StrHtml + "<td class='tdInfoData'>" + oCliente.FechaNacimiento.ToShortDateString() + "</td>";
                            StrHtml = StrHtml + "</tr>";

                        }
                        StrHtml = StrHtml + "</table>";
                        StrHtml = StrHtml + "</td>";
                        StrHtml = StrHtml + "</tr>";

                        if (oCliente.TipoPersona_Id == 1)//Persona Natural
                        {
                            StrHtml = StrHtml + "<tr>";
                            StrHtml = StrHtml + "<td class='tdTitulo02'>Antecedentes Laborales(" + participante.DescripcionTipoParticipante + ")</td>";
                            StrHtml = StrHtml + "</tr>";


                            AntecedentesLaboralesInfo oFiltro = new AntecedentesLaboralesInfo();
                            NegParticipante nLaborales = new NegParticipante();
                            Resultado<AntecedentesLaboralesInfo> rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                            oFiltro.RutCliente = participante.Rut;
                            rLaborales = nLaborales.BuscarAntecedentesLaborales(oFiltro);
                            if (rLaborales.ResultadoGeneral)
                            {
                                foreach (var laboral in rLaborales.Lista)
                                {

                                    StrHtml = StrHtml + "<tr>";
                                    StrHtml = StrHtml + "<td>";
                                    StrHtml = StrHtml + "<table style = 'width: 100%;' >";
                                    StrHtml = StrHtml + "<tr>";
                                    StrHtml = StrHtml + "<td class='tdInfo'>Empleador</td>";
                                    StrHtml = StrHtml + "<td class='tdInfoData'>" + laboral.NombreEmpleador + "</td>";
                                    StrHtml = StrHtml + "<td class='tdInfo'>Rut Empleador</td>";
                                    StrHtml = StrHtml + "<td class='tdInfoData'>" + laboral.RutEmpleador + "</td>";
                                    StrHtml = StrHtml + "</tr>";
                                    StrHtml = StrHtml + "<tr>";
                                    StrHtml = StrHtml + "<td class='tdInfo'>Tipo de Actividad</td>";
                                    StrHtml = StrHtml + "<td class='tdInfoData'>" + laboral.DescripcionTipoActividad + "(" + laboral.DescripcionSituacionLaboral + ")</td>";
                                    StrHtml = StrHtml + "<td class='tdInfo'>Fecha Inicio</td>";
                                    if (laboral.FechaInicioContrato != null)
                                        StrHtml = StrHtml + "<td class='tdInfoData'>" + laboral.FechaInicioContrato.Value.ToShortDateString() + "</td>";
                                    else
                                        StrHtml = StrHtml + "<td class='tdInfoData'>No Ingresada</td>";
                                    StrHtml = StrHtml + "</tr>";
                                    StrHtml = StrHtml + "<tr>";
                                    StrHtml = StrHtml + "<td class='tdInfo'>Cargo</td>";
                                    StrHtml = StrHtml + "<td class='tdInfoData'>" + laboral.Cargo + "</td>";
                                    StrHtml = StrHtml + "<td class='tdInfo'>Fecha Término</td>";
                                    if (laboral.FechaTerminoContrato != null)
                                        StrHtml = StrHtml + "<td class='tdInfoData'>" + laboral.FechaTerminoContrato.Value.ToShortDateString() + "</td>";
                                    else
                                        StrHtml = StrHtml + "<td class='tdInfoData'>No Ingresada</td>";
                                    StrHtml = StrHtml + "</tr>";
                                    StrHtml = StrHtml + "<tr>";
                                    StrHtml = StrHtml + "<td class='tdInfo'>Dirección</td>";
                                    StrHtml = StrHtml + "<td class='tdInfoData' colspan='3'>" + laboral.Direccion + "</td>";
                                    StrHtml = StrHtml + "</tr>";
                                    StrHtml = StrHtml + "</table>";
                                    StrHtml = StrHtml + "</td>";
                                    StrHtml = StrHtml + "</tr>";
                                    StrHtml = StrHtml + "<tr>";
                                    StrHtml = StrHtml + "<td>";
                                    StrHtml = StrHtml + "<hr />";
                                    StrHtml = StrHtml + "</td>";
                                }
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
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar los participantes";
                }
            }


        }


        private void CargarFlujosMensuales()
        {
            try
            {
                var StrHtml = "";

                StrHtml = StrHtml + "<td class='tdTitulo02'>Flujos Mensuales</td>";
                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td>";
                StrHtml = StrHtml + "<table style = 'width: 100%;'>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' rowspan ='2'>INGRESOS</td >";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Deudor</td>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Codeudor</td>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Fiador/Aval</td>";
                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td >";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td >";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td>";
                StrHtml = StrHtml + "</tr>";



                FlujosMensualesInfo oFlujosMensuales = new FlujosMensualesInfo();
                NegRentasCliente nRentas = new NegRentasCliente();
                Resultado<FlujosMensualesInfo> rFlujosMensuales = new Resultado<FlujosMensualesInfo>();
                oFlujosMensuales.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rFlujosMensuales = nRentas.BuscarFlujoMensual(oFlujosMensuales);
                decimal TotalRentaDedor = 0;
                decimal TotalRentaCodeudor = 0;
                decimal TotalRentaAval = 0;

                if (rFlujosMensuales.ResultadoGeneral)
                {
                    foreach (var flujo in rFlujosMensuales.Lista)
                    {



                        StrHtml = StrHtml + "<tr>";
                        StrHtml = StrHtml + "<td class='tdInfo'>" + flujo.Descripcion + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", flujo.RentaDeudor) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", flujo.RentaDeudor / UF) + "</td>";

                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", flujo.RentaCodeudor) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", flujo.RentaCodeudor / UF) + "</td>";

                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", flujo.RentaAval) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", flujo.RentaAval / UF) + "</td>";

                        StrHtml = StrHtml + "</tr> ";

                        TotalRentaDedor = TotalRentaDedor + flujo.RentaDeudor;
                        TotalRentaCodeudor = TotalRentaCodeudor + flujo.RentaCodeudor;
                        TotalRentaAval = TotalRentaAval + flujo.RentaAval;


                    }

                    StrHtml = StrHtml + "<tr>";
                    StrHtml = StrHtml + "<td class='tdInfoData'>Total Ingresos</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalRentaDedor) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalRentaDedor / UF) + "</td>";

                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalRentaCodeudor) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalRentaCodeudor / UF) + "</td>";

                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalRentaAval) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalRentaAval / UF) + "</td>";

                    StrHtml = StrHtml + "</tr> ";

                    decimal Totalngresos = TotalRentaDedor + TotalRentaCodeudor + TotalRentaAval;


                    StrHtml = StrHtml + "<tr class='tdInfo'>";
                    StrHtml = StrHtml + "<td class='tdInfo'>TOTAL INGRESOS COMPLEMENTADOS</td>";
                    StrHtml = StrHtml + "<td  colspan ='6'>" + string.Format("{0:C}", Totalngresos) + "</td>";


                    StrHtml = StrHtml + "</tr> ";

                    StrHtml = StrHtml + "</table> ";
                    StrHtml = StrHtml + "</tr>";
                    StrHtml = StrHtml + "</td>";
                }
                else
                {
                    lblSolicitud.Text = rFlujosMensuales.Mensaje;
                }



                DivFlujosMensuales.InnerHtml = StrHtml;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar los Fujos Mensuales";
                }
            }
        }

        private void CargarActivosSolicitud()
        {
            try
            {
                var StrHtml = "";

                StrHtml = StrHtml + "<td class='tdTitulo02'>Estado de Situación</td>";
                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td>";
                StrHtml = StrHtml + "<table style = 'width: 100%;'>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' rowspan ='2'>ACTIVOS</td >";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Deudor</td>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Codeudor</td>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Fiador/Aval</td>";
                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td >";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td >";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td>";
                StrHtml = StrHtml + "</tr>";



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



                        StrHtml = StrHtml + "<tr>";
                        StrHtml = StrHtml + "<td class='tdInfo'>" + activos.Descripcion + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", activos.ActivosDeudor) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", activos.ActivosDeudor / UF) + "</td>";

                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", activos.ActivosCodeudor) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", activos.ActivosCodeudor / UF) + "</td>";

                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", activos.ActivosAval) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", activos.ActivosAval / UF) + "</td>";

                        StrHtml = StrHtml + "</tr> ";

                        TotalActivosDeudor = TotalActivosDeudor + activos.ActivosDeudor;
                        TotalActivosCodeudor = TotalActivosCodeudor + activos.ActivosCodeudor;
                        TotalActivosAval = TotalActivosAval + activos.ActivosAval;


                    }

                    StrHtml = StrHtml + "<tr>";
                    StrHtml = StrHtml + "<td class='tdInfoData'>Total Activos</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalActivosDeudor) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalActivosDeudor / UF) + "</td>";

                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalActivosCodeudor) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalActivosCodeudor / UF) + "</td>";

                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalActivosAval) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalActivosAval / UF) + "</td>";

                    StrHtml = StrHtml + "</tr> ";

                    decimal TotalActivos = TotalActivosDeudor + TotalActivosCodeudor + TotalActivosAval;


                    StrHtml = StrHtml + "<tr class='tdInfo'>";
                    StrHtml = StrHtml + "<td class='tdInfo'>TOTAL ACTIVOS COMPLEMENTADOS</td>";
                    StrHtml = StrHtml + "<td  colspan ='6'>" + string.Format("{0:C}", TotalActivos) + "</td>";


                    StrHtml = StrHtml + "</tr> ";

                    StrHtml = StrHtml + "</table> ";
                    StrHtml = StrHtml + "</tr>";
                    StrHtml = StrHtml + "</td>";
                }
                else
                {
                    lblSolicitud.Text = rActivosSolicitud.Mensaje;
                }



                DivActivosSolicitud.InnerHtml = StrHtml;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar los Activos Mensuales";
                }
            }
        }

        private void CargarPasivosSolicitud()
        {
            try
            {
                var StrHtml = "";

                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td>";
                StrHtml = StrHtml + "<table style = 'width: 100%;'>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' rowspan ='2'>PASIVOS</td >";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Deudor</td>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Codeudor</td>";
                StrHtml = StrHtml + "<td class='tdInfoData' style='text - align: center' colspan ='2'>Fiador/Aval</td>";
                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td >";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td >";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>$</td>";
                StrHtml = StrHtml + "<td class='tdInfoData'>UF</td>";
                StrHtml = StrHtml + "</tr>";



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



                        StrHtml = StrHtml + "<tr>";
                        StrHtml = StrHtml + "<td class='tdInfo'>" + Pasivos.Descripcion + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", Pasivos.PasivosDeudor) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", Pasivos.PasivosDeudor / UF) + "</td>";

                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", Pasivos.PasivosCodeudor) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", Pasivos.PasivosCodeudor / UF) + "</td>";

                        StrHtml = StrHtml + "<td>" + string.Format("{0:C}", Pasivos.PasivosAval) + "</td>";
                        StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", Pasivos.PasivosAval / UF) + "</td>";

                        StrHtml = StrHtml + "</tr> ";

                        TotalPasivosDeudor = TotalPasivosDeudor + Pasivos.PasivosDeudor;
                        TotalPasivosCodeudor = TotalPasivosCodeudor + Pasivos.PasivosCodeudor;
                        TotalPasivosAval = TotalPasivosAval + Pasivos.PasivosAval;


                    }

                    StrHtml = StrHtml + "<tr>";
                    StrHtml = StrHtml + "<td class='tdInfoData'>Total Pasivos</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalPasivosDeudor) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalPasivosDeudor / UF) + "</td>";

                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalPasivosCodeudor) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalPasivosCodeudor / UF) + "</td>";

                    StrHtml = StrHtml + "<td>" + string.Format("{0:C}", TotalPasivosAval) + "</td>";
                    StrHtml = StrHtml + "<td>" + string.Format("{0:N4}", TotalPasivosAval / UF) + "</td>";

                    StrHtml = StrHtml + "</tr> ";

                    decimal TotalPasivos = TotalPasivosDeudor + TotalPasivosCodeudor + TotalPasivosAval;


                    StrHtml = StrHtml + "<tr class='tdInfo'>";
                    StrHtml = StrHtml + "<td class='tdInfo'>TOTAL Pasivos COMPLEMENTADOS</td>";
                    StrHtml = StrHtml + "<td  colspan ='6'>" + string.Format("{0:C}", TotalPasivos) + "</td>";


                    StrHtml = StrHtml + "</tr> ";

                    StrHtml = StrHtml + "</table> ";
                    StrHtml = StrHtml + "</tr>";
                    StrHtml = StrHtml + "</td>";
                }
                else
                {
                    lblSolicitud.Text = rPasivosSolicitud.Mensaje;
                }



                DivPasivosSolicitud.InnerHtml = StrHtml;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar los Pasivos Mensuales";
                }
            }
        }
        private void CargarCredito()
        {
            try
            {


                SolicitudInfo oSolicitud = new SolicitudInfo();
                oSolicitud = NegSolicitudes.objSolicitudInfo;
                lblSolicitud.Text = oSolicitud.Id.ToString();
                lblTipoFinanciamiento.Text = oSolicitud.DescripcionTipoFinanciamiento;
                lblProducto.Text = oSolicitud.DescripcionProducto;
                lblObjetivo.Text = oSolicitud.DescripcionObjetivo;

                lblDestino.Text = oSolicitud.DescripcionDestino;
                lblNombreSubsidio.Text = oSolicitud.DescripcionSubsidio == "" ? "Sin Subsidio" : oSolicitud.DescripcionSubsidio;
                lblPrecioVenta.Text = "UF " + string.Format("{0:N4}", oSolicitud.MontoPropiedad);
                lblMontoSubsidio.Text = "UF " + string.Format("{0:N4}", oSolicitud.MontoSubsidio);
                lblMontoBonoIntegracion.Text = "UF " + string.Format("{0:N4}", oSolicitud.MontoBonoIntegracion);
                lblMontoBonoCaptacion.Text = "UF " + string.Format("{0:N4}", oSolicitud.MontoBonoCaptacion);
                lblMontoContado.Text = "UF " + string.Format("{0:N4}", oSolicitud.MontoContado);
                lblMontoCredito.Text = "UF " + string.Format("{0:N4}", oSolicitud.MontoCredito);
                lblPorcFinanciamiento.Text = "% " + oSolicitud.PorcentajeFinanciamiento;

                lblPrecioVentaPesos.Text = string.Format("{0:C}", oSolicitud.MontoPropiedad * UF);
                lblMontoSubsidioPesos.Text = string.Format("{0:C}", oSolicitud.MontoSubsidio * UF);
                lblMontoBonoIntegracionPesos.Text = string.Format("{0:C}", oSolicitud.MontoBonoIntegracion * UF);
                lblMontoBonoCaptacionPesos.Text = string.Format("{0:C}", oSolicitud.MontoBonoIntegracion * UF);
                lblMontoContadoPesos.Text = string.Format("{0:C}", oSolicitud.MontoContado * UF);
                lblMontoCreditoPesos.Text = string.Format("{0:C}", oSolicitud.MontoCredito * UF);

                lblDividendoNeto.Text = "UF " + string.Format("{0:N4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUF);
                lblDividendoNetoPesos.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesos);

                //Dividendo con Seguros
                lblDividendoConSeguros.Text = "UF " + String.Format("{0:N4}", NegSolicitudes.objSolicitudInfo.ValorDividendoUfTotal);
                lblDividendoConSegurosPesos.Text = String.Format("{0:C}", NegSolicitudes.objSolicitudInfo.ValorDividendoPesosTotal);


                lblPlazo.Text = oSolicitud.Plazo.ToString();
                lblTasa.Text = "% " + string.Format("{0:N4}", oSolicitud.TasaFinal);
                lblGracia.Text = oSolicitud.Gracia == 0 ? "Sin Meses de Gracia" : oSolicitud.Gracia.ToString();

                lblFecha.Text = DateTime.Now.ToShortDateString();
                lblValorUF.Text = string.Format("{0:C}", UF);


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar los datos de la Solicitud";
                }
            }
        }
        private void CargaSeguros()
        {
            try
            {


                NegSeguros nSeguros = new NegSeguros();
                //Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                //SegurosContratadosInfo oSeguros = new SegurosContratadosInfo();


                //oSeguros.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                //rSeguros = nSeguros.BuscarSegurosContratados(oSeguros);
                //if (rSeguros.ResultadoGeneral)
                //    Controles.CargarGrid<SegurosContratadosInfo>(ref gvSeguros, rSeguros.Lista, new string[] { "Id" });
                //else
                //    lblSolicitud.Text = rSeguros.Mensaje;


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar los Seguros Contratados";
                }
            }
        }



        private void CargarPropiedades()
        {
            try
            {
                Controles.CargarGrid<TasacionInfo>(ref gvPropiedades, NegPropiedades.lstTasaciones, new string[] { "Id" });

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar los datos de la Solicitud";
                }
            }
        }


        private void CargaEjecutivo()
        {
            try
            {
                Resultado<UsuarioInfo> rUsuario = new Resultado<UsuarioInfo>();
                NegUsuarios nUsuario = new NegUsuarios();
                UsuarioInfo oUsuario = new UsuarioInfo();

                oUsuario.Rut = NegSolicitudes.objSolicitudInfo.EjecutivoComercial_Id;
                rUsuario = nUsuario.Buscar(oUsuario);
                if (rUsuario.ResultadoGeneral)
                {
                    var objEjecutivo = rUsuario.Lista.FirstOrDefault(u => u.Rut == NegSolicitudes.objSolicitudInfo.EjecutivoComercial_Id);
                    lblEjecutivoNombre.Text = objEjecutivo.Nombre;
                    lblEjecutivoMail.Text = objEjecutivo.Email;
                }
                else
                    lblSolicitud.Text = rUsuario.Mensaje;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar el Ejecutuvo";
                }
            }
        }


        private void CargarIndicadores()
        {
            try
            {
                var oIndicadores = new ResumenIndicadores();
                oIndicadores = NegSolicitudes.objResumenIndicadores;
                if (oIndicadores != null)
                {
                    lblTotalPasivosPrincipalMO.Text = oIndicadores.PasivosPrincipal;
                    lblTotalPasivosPrincipalPesos.Text = oIndicadores.PasivosPrincipalPesos;
                    lblTotalActivosPrincipalMO.Text = oIndicadores.ActivosPrincipal;
                    lblTotalActivosPrincipalPesos.Text = oIndicadores.ActivosPrincipalPesos;
                    lblPatrimonioPrincipal.Text = oIndicadores.PatrimonioPrincipal;
                    lblPatrimonioPrincipalPesos.Text = oIndicadores.PatrimonioPrincipalPesos;

                    lblTotalPasivosComplementados.Text = oIndicadores.PasivosComplementados;
                    lblTotalPasivosComplementadosPesos.Text = oIndicadores.PasivosComplementadosPesos;
                    lblTotalActivosComplementados.Text = oIndicadores.ActivosComplementados;
                    lblTotalActivosComplementadosPesos.Text = oIndicadores.ActivosComplementadosPesos;
                    lblPatrimonioComplementado.Text = oIndicadores.PatrimonioComplementado;
                    lblPatrimonioComplementadoPesos.Text = oIndicadores.PatrimonioComplementadoPesos;

                    lblRentaNecesaria.Text = oIndicadores.RentaNecesaria;
                    lblRentaNecesariaPesos.Text = oIndicadores.RentaNecesariaPesos;
                    lblRentaPrincipal.Text = oIndicadores.RentaPrincipal;
                    lblRentaPrincipalPesos.Text = oIndicadores.RentaPrincipalPesos;

                    lblRentaCodeudor.Text = oIndicadores.RentaCodeudor;
                    lblRentaCodeudorPesos.Text = oIndicadores.RentaCodeudorPesos;
                    lblRentaComplementada.Text = oIndicadores.RentaComplementada;
                    lblRentaComplementadaPesos.Text = oIndicadores.RentaComplementadaPesos;
                    lblDividendoRentaPrincipal.Text = oIndicadores.DividendoRentaPrincipal;
                    lblDividendoRentaCodeudor.Text = oIndicadores.DividendoRentaCodeudor;
                    lblDividendoRentaComplementada.Text = oIndicadores.DividendoRentaComplementada;
                    lblCargaFinancieraPrincipal.Text = oIndicadores.CargaFinancieraPrincipal;
                    lblCargaFinancieraComplementada.Text = oIndicadores.CargaFinancieraComplementada;
                    lblCargaFinancieraHipotecariaComplementada.Text = oIndicadores.CargaFinancieraHipotecariaComplementada;
                    lblCargaFinancieraHipotecariaCodeudor.Text = oIndicadores.CargaFinancieraHipotecariaCodeudor;
                    lblCargaFinancieraHipotecariaPrincipal.Text = oIndicadores.CargaFinancieraHipotecariaPrincipal;



                }



            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    lblSolicitud.Text = Ex.Message;
                }
                else
                {
                    lblSolicitud.Text = "Error al Cargar los Indicadores";
                }
            }
        }


        //private void CargaObservaciones()
        //{
        //    try
        //    {
        //        var oInfo = new ObservacionInfo
        //        {
        //            Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id,
        //        };

        //        var ObjResultado = new Resultado<ObservacionInfo>();
        //        var objNegocio = new NegObservaciones();

        //        ////Asignacion de Variables
        //        ObjResultado = objNegocio.Buscar(oInfo);
        //        if (ObjResultado.ResultadoGeneral)
        //        {
        //            Controles.CargarGrid(ref gvObservaciones, ObjResultado.Lista.Where(o=> o.TipoObservacion_Id == 3 || o.TipoObservacion_Id == 4).ToList(), new string[] { "Id", "Estado_Id", "UsuarioIngreso_Id", "TipoObservacion_Id", "ResponsableIngreso" });

        //        }
        //        else
        //        {
        //            Controles.MostrarMensajeError(ObjResultado.Mensaje);
        //            return;
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
        //            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Observación");
        //        }
        //    }

        //}
    }
}