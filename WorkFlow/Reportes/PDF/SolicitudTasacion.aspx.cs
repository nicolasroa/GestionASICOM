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
using System.Web;
using System.Globalization;

namespace WorkFlow.Reportes.PDF
{
    public partial class SolicitudTasacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                CargarEmpresasTasacion();
                CargarPropiedades();
                DatosGGOO();

                CargarDatosContacto();
            }
        }

        private void DatosGGOO()
        {
            try
            {
                NegGastosOperacionales nGastos = new NegGastosOperacionales();
                GastoOperacionalInfo oGastos = new GastoOperacionalInfo();
                Resultado<GastoOperacionalInfo> rGastos = new Resultado<GastoOperacionalInfo>();

                int SolicitudPrincipal_Id = 0;
                SolicitudPrincipal_Id = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.Solicitud_Id == NegSolicitudes.objSolicitudInfo.Id).SolicitudTasacion_Id;


                oGastos.Solicitud_Id = SolicitudPrincipal_Id;

                rGastos = nGastos.BuscarGastos(oGastos);
                if (rGastos.ResultadoGeneral)
                {

                    oGastos = rGastos.Lista.FirstOrDefault(g => g.TipoGastoOperacional_Id == 2);//GGOO Tasación
                    if (oGastos != null)
                        lblValorTasacion.Text = "UF " + string.Format("{0:N1}", oGastos.Valor);
                    else
                        lblValorTasacion.Text = "UF " + string.Format("{0:N1}", 0);

                }
                else
                {
                    Response.Write(rGastos.Mensaje);
                }


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Response.Write(Ex.Message);
                }
                else
                {
                    Response.Write("Error al Cargar los Gastos Operacionales");
                }
            }
        }
        private void CargarEmpresasTasacion()
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
                    Controles.CargarChekBoxList<AsignacionTipoFabricaInfo>(ref chkEmpresasTasacion, oResultado.Lista, "Fabrica_Id", "DescripcionFabrica");
                }
                else
                {
                    Response.Write(oResultado.Mensaje);
                }



            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Response.Write(Ex.Message);
                }
                else
                {
                    Response.Write("Error al Cargar Fábricas de Tasadores");
                }
            }
        }
        private void CargarPropiedades()
        {
            try
            {

                string html = "";

                NegPropiedades nPropiedades = new NegPropiedades();
                TasacionInfo oFiltroTasaciones = new TasacionInfo();
                Resultado<TasacionInfo> rTasaciones = new Resultado<TasacionInfo>();

                oFiltroTasaciones.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                rTasaciones = nPropiedades.BuscarTasacion(oFiltroTasaciones);
                if (rTasaciones.ResultadoGeneral)
                {
                    foreach (var tasacion in NegPropiedades.lstTasaciones)
                    {
                       
                        Resultado<PropiedadInfo> rPropiedad = new Resultado<PropiedadInfo>();
                        PropiedadInfo oPropiedadPadre = new PropiedadInfo();
                        PropiedadInfo prop = new PropiedadInfo();


                        prop.Id = tasacion.Propiedad_Id;
                        rPropiedad = nPropiedades.BuscarPropiedad(prop);
                        if (!rPropiedad.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rPropiedad.Mensaje);
                            return;
                        }

                        prop = rPropiedad.Lista.FirstOrDefault();


                        html = html + "<table style='width:100%;'>";
                        html = html + "<tr>";
                        html = html + "<td class='auto-style4' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Tipo de Vivienda</td>";
                        html = html + "<td class='auto-style5' style='border: thin solid #000000'>" + prop.DescripcionTipoInmueble + "</td>";
                        html = html + "<td class='auto-style6' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>ROL Propiedad</td>";
                        html = html + "<td class='auto-style7' style='border: thin solid #000000'>" + prop.RolSitio.ToString() + "-" + prop.RolManzana.ToString() + "</td>";
                        html = html + "</tr>";
                        html = html + "<tr>";
                        html = html + "<td class='auto-style1' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Dirección</td>";
                        html = html + "<td colspan='3' style='border: thin solid #000000'>" + prop.Direccion + "</td>";
                        html = html + "</tr>";
                        html = html + "<tr>";
                        html = html + "<td class='auto-style1' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Nº Depto./Casa</td>";
                        html = html + "<td class='auto-style2' style='border: thin solid #000000'>" + prop.Numero.ToString() + "</td>";
                        html = html + "<td class='auto-style3' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Comuna</td>";
                        html = html + "<td style='border: thin solid #000000'>" + prop.DescripcionComuna + "</td>";
                        html = html + "</tr>";
                        html = html + "<tr>";
                        html = html + "<td class='auto-style1' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Provincia</td>";
                        html = html + "<td class='auto-style2' style='border: thin solid #000000'>" + prop.DescripcionProvincia + "</td>";
                        html = html + " <td class='auto-style3' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Región</td>";
                        html = html + "<td style='border: thin solid #000000'>" + prop.DescripcionRegion + "</td>";
                        html = html + "</tr>";
                        html = html + "<tr>";
                        html = html + "<td class='auto-style1' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Nº Estacionamiento</td>";
                        html = html + "<td class='auto-style2' style='border: thin solid #000000'>" + prop.DeptoOficina + "</td>";
                        html = html + "<td class='auto-style3' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Nº Bodega</td>";
                        html = html + "<td style='border: thin solid #000000'>" + prop.DeptoOficina + "</td>";
                        html = html + "</tr>";
                        html = html + "<tr>";
                        html = html + "<td class='auto-style1' style='background-color: #0099CC; border: thin solid #000000; color: #FFFFFF; text-align: center;'>Proyecto</td>";
                        html = html + "<td colspan='3' style='border: thin solid #000000'>" + NegSolicitudes.objSolicitudInfo.DescripcionProyecto + "</td>";
                        html = html + "</tr>";
                        html = html + "</table>";
                        html = html + " <br />";

                    }
                }
                divPropiedades.InnerHtml = html;


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CargarDatosContacto()
        {

            lblNombreContacto.Text = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.IndPropiedadPrincipal == true).NombreContactoTasacion;
            lblMailContacto.Text = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.IndPropiedadPrincipal == true).EmailContactoTasacion;
            lblTelefonoContacto.Text = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.IndPropiedadPrincipal == true).TelefonoContactoTasacion;

            if (NegPropiedades.lstTasaciones.FirstOrDefault(t => t.IndPropiedadPrincipal == true).FechaSolicitudTasacion != null)
            {
                var FechaSolicitud = NegPropiedades.lstTasaciones.FirstOrDefault(t => t.IndPropiedadPrincipal == true).FechaSolicitudTasacion.Value;
                CultureInfo culture = new CultureInfo("es-CL");
                lblFechaSolicitudTasacion.Text = string.Format("Santiago de Chile a, {0} de {1} de {2}", FechaSolicitud.Day, FechaSolicitud.ToString("MMMM", culture), FechaSolicitud.Year);
            }
            lblNombreSolicitante.Text = NegSolicitudes.objSolicitudInfo.NombreCliente;
            lblRutSolicitante.Text = NegSolicitudes.objSolicitudInfo.RutCliente;




        }
    }
}