using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.DatosGenerales
{
    public partial class DatosObservaciones : System.Web.UI.UserControl
    {


        private List<TablaInfo> lstEstadosObservacion = new List<TablaInfo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarEstadoObservacion();
                CargarTipoObservacion();
                CargaObservaciones();

            }
        }
        private void CargarEstadoObservacion()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("ESTAOBSERVACION");
                if (Lista != null)
                {
                    lstEstadosObservacion = Lista;

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablaEstado + " Sin Datos");
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
                    Controles.MostrarMensajeError("Error al Cargar Estados de la Observación");
                }
            }
        }
        private void CargarTipoObservacion()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("TIPOOBSERVACION");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlTipoObservacion, Lista, "CodigoInterno", "Nombre", "--Tipo de Observación", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablaEstado + " Sin Datos");
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
                    Controles.MostrarMensajeError("Error al Cargar Estados de la Observación");
                }
            }
        }
        protected void btnAgregaComentario_Click(object sender, EventArgs e)
        {
            AgregaComentario();
        }

        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gdvComentarios.DataKeys[row.RowIndex].Values["Id"].ToString());
            int UsuarioIngreso_Id = int.Parse(gdvComentarios.DataKeys[row.RowIndex].Values["UsuarioIngreso_Id"].ToString());
            string ResponsableIngreso = gdvComentarios.DataKeys[row.RowIndex].Values["ResponsableIngreso"].ToString();
            int EventoIngreso_Id = int.Parse(gdvComentarios.DataKeys[row.RowIndex].Values["Evento_Id"].ToString());
            string DescripcionEvento = gdvComentarios.DataKeys[row.RowIndex].Values["DescripcionEvento"].ToString();

            if (NegBandejaEntrada.oBandejaEntrada != null)
            {
                if (NegUsuarios.Usuario.Rut != UsuarioIngreso_Id && NegBandejaEntrada.oBandejaEntrada.Evento_Id != EventoIngreso_Id)
                {
                    Controles.MostrarMensajeAlerta("Sòlo se permite que el Usuario " + ResponsableIngreso + " en el evento " + DescripcionEvento + " modifique esta Observación");
                    return;
                }
            }
            else
            {
                if (!NegObservaciones.indModificaObservacion)
                {
                    Controles.MostrarMensajeAlerta("Solo se permite modificar Observaciones desde el Módulo Mantenedor de Solicitudes");
                    return;
                }
            }
            ObtenerObservacion(Id);
        }

        protected void gdvComentarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvComentarios.PageIndex = e.NewPageIndex;
            CargaObservaciones();
        }

        private void CargaObservaciones()
        {
            try
            {
                var oInfo = new ObservacionInfo
                {
                    Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id,
                };

                var ObjResultado = new Resultado<ObservacionInfo>();
                var objNegocio = new NegObservaciones();

                ////Asignacion de Variables
                ObjResultado = objNegocio.Buscar(oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gdvComentarios, ObjResultado.Lista, new string[] { "Id", "Estado_Id", "UsuarioIngreso_Id", "TipoObservacion_Id", "ResponsableIngreso", "Evento_Id", "DescripcionEvento" });

                    if (NegSolicitudes.IndComentario == true)
                    {
                        lblTituloObservacion.Text = "Observaciones";  // SUJETO A
                        gdvComentarios.Columns[0].Visible = false;
                    }
                    else
                    {
                        lblTituloObservacion.Text = "Sujeto a Revisión";  // SUJETO A
                        gdvComentarios.Columns[0].Visible = true;
                    }
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

        private void AgregaComentario()
        {
            try
            {
                if (!ValidarFormulario()) { return; }

                var oInfo = new ObservacionInfo();

                if (NegObservaciones.objObservacionInfo != null)
                {
                    oInfo = NegObservaciones.objObservacionInfo;
                }
                oInfo.UsuarioIngreso_Id = NegUsuarios.Usuario.Rut.Value;
                oInfo.Descripcion = txtObservacion.Text;
                oInfo.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oInfo.Evento_Id = NegBandejaEntrada.oBandejaEntrada == null ? -1 : NegBandejaEntrada.oBandejaEntrada.Evento_Id;
                oInfo.TipoObservacion_Id = int.Parse(ddlTipoObservacion.SelectedValue);

                if (ddlTipoObservacion.SelectedValue == NegTablas.IdentificadorMaestro("TIPOOBSERVACION", "REPA").ToString()) //TIPO Reparo/Subsano
                {
                    oInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTAOBSERVACION", "P");//ESTADO PENDIENTE
                }
                else
                {
                    oInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTAOBSERVACION", "I"); //ESTADO INGRESADA
                }



                var ObjResultado = new Resultado<ObservacionInfo>();
                var objNegocio = new NegObservaciones();

                ////Asignacion de Variables
                ObjResultado = objNegocio.Guardar(ref oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                    CargaObservaciones();
                    txtObservacion.Text = "";
                    hfIdObservacion.Value = "";
                    ddlTipoObservacion.ClearSelection();
                    NegObservaciones.objObservacionInfo = null;
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

        private void ActualizarEstadoObservacion(int Id, int NuevoEstado_Id)
        {
            try
            {
                var oInfo = new ObservacionInfo();

                oInfo = NegObservaciones.lstObservacionInfo.FirstOrDefault(o => o.Id == Id);
                oInfo.Estado_Id = NuevoEstado_Id;
                oInfo.UsuarioSubsano_Id = NegUsuarios.Usuario.Rut.Value;
                oInfo.FechaSubsano = DateTime.Now;
                var ObjResultado = new Resultado<ObservacionInfo>();
                var objNegocio = new NegObservaciones();

                ////Asignacion de Variables
                ObjResultado = objNegocio.Guardar(ref oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Estado de Observación Actualizado");
                    CargaObservaciones();
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


        private bool ValidarFormulario()
        {
            if (txtObservacion.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtObservacion.ClientID);
                return false;
            }
            if (ddlTipoObservacion.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlTipoObservacion.ClientID);
                return false;
            }
            return true;
        }


        private void ObtenerObservacion(int id)
        {
            try
            {
                var oInfo = new ObservacionInfo
                {
                    Id = id,
                    UsuarioIngreso_Id = NegUsuarios.Usuario.Rut.Value,
                    Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id
                };

                var ObjResultado = new Resultado<ObservacionInfo>();
                var objNegocio = new NegObservaciones();

                ////Asignacion de Variables
                ObjResultado = objNegocio.Buscar(oInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    NegObservaciones.objObservacionInfo = ObjResultado.Lista[0];
                    txtObservacion.Text = NegObservaciones.objObservacionInfo.Descripcion;
                    ddlTipoObservacion.SelectedValue = NegObservaciones.objObservacionInfo.TipoObservacion_Id.ToString();
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

        protected void gdvComentarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (lstEstadosObservacion.Count == 0)
                        CargarEstadoObservacion();
                    if (NegObservaciones.lstObservacionInfo.Count > 0 && e.Row.RowIndex >= 0)
                    {
                        Anthem.DropDownList ddlEstado = (e.Row.FindControl("ddlEstadoObservacion") as Anthem.DropDownList);
                        int Estado_Id = int.Parse(gdvComentarios.DataKeys[e.Row.RowIndex].Values["Estado_Id"].ToString());
                        int TipoObservacion_Id = int.Parse(gdvComentarios.DataKeys[e.Row.RowIndex].Values["TipoObservacion_Id"].ToString());

                        if (TipoObservacion_Id == (int)NegTablas.IdentificadorMaestro("TIPOOBSERVACION", "REPA"))
                            Controles.CargarCombo<TablaInfo>(ref ddlEstado, lstEstadosObservacion.Where(es => es.CodigoInterno != (int)NegTablas.IdentificadorMaestro("ESTAOBSERVACION", "I")).ToList<TablaInfo>(), Constantes.StringCodigoInterno, Constantes.StringNombre, "", "");
                        else
                            Controles.CargarCombo<TablaInfo>(ref ddlEstado, lstEstadosObservacion.Where(es => es.CodigoInterno == (int)NegTablas.IdentificadorMaestro("ESTAOBSERVACION", "I")).ToList<TablaInfo>(), Constantes.StringCodigoInterno, Constantes.StringNombre, "", "");

                        ddlEstado.SelectedValue = Estado_Id.ToString();

                        if (TipoObservacion_Id == (int)NegTablas.IdentificadorMaestro("TIPOOBSERVACION", "REPA"))
                            ddlEstado.Enabled = true;
                        else
                            ddlEstado.Enabled = false;
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
                    Controles.MostrarMensajeError("Error al Cargar Estados de la Observación en la Lista");
                }
            }
        }

        protected void ddlEstadoObservacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((Anthem.DropDownList)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gdvComentarios.DataKeys[row.RowIndex].Values["Id"].ToString());
            int UsuarioIngreso_Id = int.Parse(gdvComentarios.DataKeys[row.RowIndex].Values["UsuarioIngreso_Id"].ToString());
            Anthem.DropDownList ddlEstado = (row.FindControl("ddlEstadoObservacion") as Anthem.DropDownList);
            ActualizarEstadoObservacion(Id, int.Parse(ddlEstado.SelectedValue));
        }
    }
}