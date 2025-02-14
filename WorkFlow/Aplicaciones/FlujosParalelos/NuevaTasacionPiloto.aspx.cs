using WorkFlow.Entidades;
using WorkFlow.Entidades.Documental;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.Documental;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace WorkFlow.Aplicaciones.FlujosParalelos
{
    public partial class NuevaTasacionPiloto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                DatosInmobiliariaProyecto.CargaComboTipoInmueble();
                DatosInmobiliariaProyecto.CargaComboRegion();
                DatosInmobiliariaProyecto.CargaComboInmobiliaria();
                DatosInmobiliariaProyecto.PermiteActualizacion(true);
            }
        }
        protected void btnProcesarTasacion_Click(object sender, EventArgs e)
        {
            if (!DatosInmobiliariaProyecto.GuardarEntidadInmobiliaria()) return;
            if (!DatosInmobiliariaProyecto.GuardarEntidadProyecto()) return;
            ProcesarFlujo();
        }

        protected void ProcesarFlujo()
        {
            try
            {


                var oSolicitud = new SolicitudInfo();
                var rSolicitud = new Resultado<SolicitudInfo>();
                var nSolicitud = new NegSolicitudes();


                oSolicitud.Estado_Id = 6;//Tasacion Piloto en Curso
                oSolicitud.SubEstado_Id = (int)NegTablas.IdentificadorMaestro("SUBEST_SOLICITUD", "ING");
                oSolicitud.FechaIngreso = DateTime.Now;
                oSolicitud.Producto_Id = 9;// Producto Tasacion Piloto
                oSolicitud.Destino_Id = 10; //Destino Tasación Piloto
                oSolicitud.Sucursal_Id = 3;// Casa Matriz
                oSolicitud.EjecutivoComercial_Id = (int)NegUsuarios.Usuario.Rut;
                oSolicitud.Inmobiliaria_Id = int.Parse(DatosInmobiliariaProyecto.ddlInmobiliaria.SelectedValue);
                oSolicitud.Proyecto_Id = int.Parse(DatosInmobiliariaProyecto.ddlProyecto.SelectedValue);
                rSolicitud = nSolicitud.Guardar(ref oSolicitud);
                if (rSolicitud.ResultadoGeneral)
                {
                    IniciarFlujoTasacion(oSolicitud.Id, 3);//Se inicia Flujo de Tasación Piloto
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
                    Controles.MostrarMensajeError("Error al Procesar Tasación");
                }
            }

        }
        private void IniciarFlujoTasacion(int Solicitud_Id, int TipoTasacion_Id)
        {
            try
            {
                NegFlujo nFlujo = new NegFlujo();
                FlujoInfo oFlujo = new FlujoInfo();
                Resultado<FlujoInfo> rFlujo = new Resultado<FlujoInfo>();

                oFlujo.Solicitud_Id = Solicitud_Id;
                oFlujo.TipoFlujo_Id = TipoTasacion_Id;
                rFlujo = nFlujo.IniciarFlujoTasacion(oFlujo);
                if (rFlujo.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Flujo Iniciado Correctamente bajo la Solicitud: " + Solicitud_Id.ToString() + ", Consulta la Bandeja de Entrada");
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
                    Controles.MostrarMensajeError("Error al Iniciar el Flujo de Tasación.");
                }
            }
        }
    }
}