using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Aplicaciones
{
    public partial class BandejaDesembolso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargaComboInmobiliaria();
            }
        }

        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            CargaEventosDesembolso();
        }
        protected void ddlInmobiliaria_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlInmobiliaria.SelectedValue == "-1")
            {
                Controles.CargarCombo<UsuarioInfo>(ref ddlProyecto, null, "Id", "Descripcion", "-- Todos --", "-1");
            }
            else
            {
                CargaComboProyecto(int.Parse(ddlInmobiliaria.SelectedValue));
            }




        }
        protected void btnBuscar_Click(object sender, EventArgs e) => CargaEventosDesembolso();

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
                    Controles.CargarCombo<InmobiliariaInfo>(ref ddlInmobiliaria, objResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                    Controles.CargarCombo<UsuarioInfo>(ref ddlProyecto, null, "Id", "Descripcion", "-- Todos --", "-1");
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
                    Controles.CargarCombo<ProyectoInfo>(ref ddlProyecto, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
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
        private void CargaEventosDesembolso()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                DesembolsoFiltro oFiltro = new DesembolsoFiltro();
                NegDesembolso oNeg = new NegDesembolso();
                Resultado<DesembolsoInfo> oResultado = new Resultado<DesembolsoInfo>();

                if (txtNumeroSolicitud.Text != "")
                    oFiltro.Solicitud_Id = int.Parse(txtNumeroSolicitud.Text);
                oFiltro.Nombre = txtNombreCliente.Text;
                oFiltro.ApellidoPaterno = txtApePatCliente.Text;
                oFiltro.ApellidoMaterno = txtApeMatCliente.Text;
                oFiltro.Inmobiliaria_Id = int.Parse(ddlInmobiliaria.SelectedValue);
                oFiltro.Proyecto_Id = int.Parse(ddlProyecto.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                oResultado = oNeg.CargarBandejaDesembolso(oFiltro);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<DesembolsoInfo>(ref gvBusqueda, oResultado.Lista, new string[] { "Id", "Solicitud_Id" });
                    lblContador.Text = oResultado.Lista.Count.ToString() + " Registro(s) Encontrado(s)";
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

        protected void chkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos();
            else
                DesmarcarTodos();
        }


        private void MarcarTodos()
        {
            int x = 0;

            for (x = 0; x <= gvBusqueda.Rows.Count - 1; x++)
            {
                ((CheckBox)gvBusqueda.Rows[x].FindControl("chkIniciarFlujo")).Checked = true;
            }
        }
        private void DesmarcarTodos()
        {
            int x = 0;

            for (x = 0; x <= gvBusqueda.Rows.Count - 1; x++)
            {
                ((CheckBox)gvBusqueda.Rows[x].FindControl("chkIniciarFlujo")).Checked = false;
            }
        }

        protected void btnIniciarFlujoPrepago_Click(object sender, EventArgs e)
        {
            InciarFlujoDesembolso();
        }

        private void InciarFlujoDesembolso()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                DesembolsoInfo oDesembolso = new DesembolsoInfo();
                NegDesembolso oNeg = new NegDesembolso();
                Resultado<DesembolsoInfo> oResultado = new Resultado<DesembolsoInfo>();


                foreach (GridViewRow Row in gvBusqueda.Rows)
                {
                    CheckBox chkIniciarFlujo = (CheckBox)Row.FindControl("chkIniciarFlujo");

                    int Solicitud_Id = int.Parse(gvBusqueda.DataKeys[Row.RowIndex].Values["Solicitud_Id"].ToString());
                    oDesembolso.Solicitud_Id = Solicitud_Id;


                    if (chkIniciarFlujo.Checked)
                        oResultado = oNeg.IniciarFlujo(oDesembolso);


                    if (oResultado.ResultadoGeneral)
                    {
                        if (!ActualizaSolicitud(Solicitud_Id)) return;
                    }
                    else
                    {
                        Controles.MostrarMensajeError(oResultado.Mensaje);
                        return;
                    }
                }
                Controles.MostrarMensajeExito("Flujo de Desembolso Inciado Correctamente para las Solicitudes Seleccionadas, Favor Consultar la Bandeja de Entrada");

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Iniciar el Flujo de Desembolso");
                }
            }
        }



        private bool ActualizaSolicitud(int Solicitud_Id)
        {

            try
            {
                Resultado<SolicitudInfo> rSolicitud = new Resultado<SolicitudInfo>();
                NegSolicitudes nSolicitud = new NegSolicitudes();
                SolicitudInfo oSolicitud = new SolicitudInfo();

                oSolicitud.Id = Solicitud_Id;

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

                NegSolicitudes.objSolicitudInfo.SubEstado_Id = (int)NegTablas.IdentificadorMaestro("SUBEST_SOLICITUD", "PD");
                NegSolicitudes.objSolicitudInfo.Usuario_Id = NegUsuarios.Usuario.Rut;

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
    }
}