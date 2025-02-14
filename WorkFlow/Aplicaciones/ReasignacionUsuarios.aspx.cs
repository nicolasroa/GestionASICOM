using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Aplicaciones
{
    public partial class ReasignacionUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Controles.EjecutarJavaScript("CargarInicioReasignacion();");
            if (!Page.IsPostBack)
            {
                CargaComboSucursal();
                CargarFabricas();
            }
        }

        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUsuario.SelectedValue == "-1")
            {
                ddlRol.ClearSelection();
            }
            else
            {
                CargarRoles(int.Parse(ddlUsuario.SelectedValue));
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }
        private void CargarFabricas()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oFabrica = new FabricaInfo();
                var NegFabrica = new NegFabricas();
                var oResultado = new Resultado<FabricaInfo>();

                //Asignación de Variables de Búsqueda
                oFabrica.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                //Ejecución del Proceso de Búsqueda
                oResultado = NegFabrica.BuscarFabrica(oFabrica);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<FabricaInfo>(ref ddlFabrica, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todas --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Fábricas");
                }
            }
        }
        private void CargaComboSucursal()
        {
            try
            {
                var oSucursalInfo = new SucursalInfo();
                var oNegSucursal = new NegSucursales();
                var oResultado = new Resultado<SucursalInfo>();
                oSucursalInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oResultado = oNegSucursal.Buscar(oSucursalInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SucursalInfo>(ref ddlSucursal, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todas --", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Sucursal");
                }
            }
        }
        private void CargarUsuarios()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoUsuario = new UsuarioInfo();
                var NegUsuarios = new NegUsuarios();
                var ObjetoResultado = new Resultado<UsuarioInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoUsuario.Sucursal_Id = int.Parse(ddlSucursal.SelectedValue);
                ObjetoUsuario.Fabrica_Id = int.Parse(ddlFabrica.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegUsuarios.Buscar(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref ddlUsuario, ObjetoResultado.Lista.OrderBy(u => u.Nombre).ToList(), "Rut", "Nombre", "-- Usuario a Reasignar --", "-1");
                    Controles.EjecutarJavaScript("CargarInicioReasignacion();");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Usuarios");
                }
            }
        }
        private void CargarNuevoUsuario(int Rol_Id)
        {
            try
            {

                ////Declaracion de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();
                int Usuario_Id = 0;
                Usuario_Id = int.Parse(ddlUsuario.SelectedValue);
                ////Asignacion de Variables
                ObjetoUsuario.Rol_Id = Rol_Id;
                ObjetoUsuario.Sucursal_Id = int.Parse(ddlSucursal.SelectedValue);
                ObjetoUsuario.Fabrica_Id = int.Parse(ddlFabrica.SelectedValue);
                ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjetoResultado = NegUsuario.BuscarUsuariosPorRol(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<UsuarioInfo>(ref ddlNuevoUsuario, ObjetoResultado.Lista.Where(u => u.Rut != Usuario_Id).OrderBy(u => u.Nombre).ToList(), "Rut", "Nombre", "-- Nuevo Usuario --", "-1");
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
        private void CargarRoles(int Usuario_Id)
        {
            try
            {
                //Declaracion de Variables
                var ObjetoUsuarioRol = new UsuarioRolInfo();
                var ObjetoResultado = new Resultado<UsuarioRolInfo>();
                var NegUsuario = new NegUsuarios();

                //Asignacion de Variables
                ObjetoUsuarioRol.Usuario_Id = Usuario_Id;
                ObjetoResultado = NegUsuario.BuscarUsuarioRol(ObjetoUsuarioRol);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (ObjetoResultado.Lista.Count > 1)
                        Controles.CargarCombo<UsuarioRolInfo>(ref ddlRol, ObjetoResultado.Lista, "Rol_Id", "DescripcionRol", "-- Rol del Usuario --", "-1");
                    else
                    {
                        Controles.CargarCombo<UsuarioRolInfo>(ref ddlRol, ObjetoResultado.Lista, "Rol_Id", "DescripcionRol", "", "");
                        CargarNuevoUsuario(ObjetoResultado.Lista.FirstOrDefault().Rol_Id);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRol.SelectedValue != "-1")
            {
                CargarNuevoUsuario(int.Parse(ddlRol.SelectedValue));
            }
        }
        private void CargarSolicitudes()
        {
            try
            {
                NegSolicitudes nSolicitudes = new NegSolicitudes();
                AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
                Resultado<AsignacionSolicitudInfo> rAsignacion = new Resultado<AsignacionSolicitudInfo>();
                if (ddlUsuario.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Usuario a Reasignar");
                    return;
                }
                if (ddlRol.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Rol");
                    return;
                }

                oAsignacion.Responsable_Id = int.Parse(ddlUsuario.SelectedValue);
                oAsignacion.Rol_Id = int.Parse(ddlRol.SelectedValue);

                rAsignacion = nSolicitudes.BuscarSolicitudesAsignadas(oAsignacion);

                if (rAsignacion.ResultadoGeneral)
                {
                    Controles.CargarGrid<AsignacionSolicitudInfo>(ref gvSolicitudesAsignadas, rAsignacion.Lista, new string[] { "Solicitud_Id" });

                }
                else
                {
                    Controles.MostrarMensajeError(rAsignacion.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar las Solicitudes Asignadas");
                }

            }
        }
        private void ProcesarReasignacion()
        {
            try
            {
                NegSolicitudes nSolicitudes = new NegSolicitudes();
                AsignacionSolicitudInfo oAsignacion = new AsignacionSolicitudInfo();
                Resultado<AsignacionSolicitudInfo> rAsignacion = new Resultado<AsignacionSolicitudInfo>();
                if (ddlUsuario.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Usuario a Reasignar");
                    return;
                }
                if (ddlNuevoUsuario.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar el Nuevo Usuario Responsable");
                    return;
                }
                if (ddlRol.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Rol");
                    return;
                }
                if (gvSolicitudesAsignadas.Rows.Count == 0)
                {
                    Controles.MostrarMensajeAlerta("No Hay Solicitudes para Reasignar");
                    return;
                }
                int SolicitudesSeleccionadas = 0;
                foreach (GridViewRow Row in gvSolicitudesAsignadas.Rows)
                {
                    CheckBox chkReasignar = (CheckBox)Row.FindControl("chkReasignar");
                    if (chkReasignar.Checked)
                    {
                        SolicitudesSeleccionadas++;
                    }
                }
                if (SolicitudesSeleccionadas == 0)
                {
                    Controles.MostrarMensajeAlerta("No Hay Solicitudes Seleccionadas para la Reasignación");
                    return;
                }


                oAsignacion.Responsable_Id = int.Parse(ddlUsuario.SelectedValue);
                oAsignacion.NuevoResponsable_Id = int.Parse(ddlNuevoUsuario.SelectedValue);
                oAsignacion.Rol_Id = int.Parse(ddlRol.SelectedValue);

                foreach (GridViewRow Row in gvSolicitudesAsignadas.Rows)
                {
                    CheckBox chkReasignar = (CheckBox)Row.FindControl("chkReasignar");
                    if (chkReasignar.Checked)
                    {
                        oAsignacion.Solicitud_Id = int.Parse(gvSolicitudesAsignadas.DataKeys[Row.RowIndex].Values["Solicitud_Id"].ToString());


                        rAsignacion = nSolicitudes.ProcesarReasignacion(oAsignacion);
                        if (rAsignacion.ResultadoGeneral == false)
                        {
                            Controles.MostrarMensajeError(rAsignacion.Mensaje);
                            return;
                        }
                    }
                }
                Controles.MostrarMensajeExito("Reasignación Procesada Correctamente");


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Procesar la Reasignación");
                }

            }
        }
        protected void btnSeleccionarTodas_Click(object sender, EventArgs e)
        {
            SeleccionarSolicitudes(1);
        }
        protected void btnSeleccionarMitad_Click(object sender, EventArgs e)
        {
            SeleccionarSolicitudes(2);
        }
        private void SeleccionarSolicitudes(int Porcentaje)
        {
            try
            {
                foreach (GridViewRow Row in gvSolicitudesAsignadas.Rows)
                {
                    CheckBox chkReasignar = (CheckBox)Row.FindControl("chkReasignar");
                    chkReasignar.Checked = false;
                }

                for (int i = 0; i < gvSolicitudesAsignadas.Rows.Count / Porcentaje; i++)
                {
                    CheckBox chkReasignar = (CheckBox)gvSolicitudesAsignadas.Rows[i].FindControl("chkReasignar");
                    chkReasignar.Checked = true;
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
                    Controles.MostrarMensajeError("Error al Seleccionar Solicitudes");
                }

            }
        }
        protected void btnProcesarReasignacion_Click(object sender, EventArgs e)
        {
            ProcesarReasignacion();
        }
        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
    }
}