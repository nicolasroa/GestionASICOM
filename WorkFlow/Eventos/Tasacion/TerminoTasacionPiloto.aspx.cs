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
using WorkFlow.Entidades.Documental;
using System.Web;
using System.IO;

namespace WorkFlow.Eventos.Tasacion
{
    public partial class TerminoTasacionPiloto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioSolicitudTasaciones();");

            if (!Page.IsPostBack)
            {

                CargarAcciones();
                CargarDocumentosSolicitud();


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
                    if (gvDocumentosSolicitud.Rows.Count == 0)
                    {
                        Controles.MostrarMensajeAlerta("Debe Subir el Informe de Tasación");
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
        private void CargarDocumentosSolicitud()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoArchRep = new ArchivosRepositoriosInfo();
                var NegArchRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoArchRep.NumeroSolicitud = NegSolicitudes.objSolicitudInfo.Id.ToString();
                ObjetoArchRep.IdGrupoDocumento = 7;// Grupo Tasación
                ObjetoArchRep.IdTipoDocumento = 124; //Documento Informe Tasación

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegArchRep.Buscar(ObjetoArchRep);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    NegArchivosRepositorios.getsetlstArchivoRepositorio = ObjetoResultado.Lista;
                    Controles.CargarGrid<ArchivosRepositoriosInfo>(ref gvDocumentosSolicitud, ObjetoResultado.Lista, new string[] { Constantes.StringId, "IdArchivo" });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "ArchivosRepositorios");
                }
            }
        }
        protected void gvDocumentosSolicitud_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*Imagen dependiendo de la Extension*/
                Anthem.Image imgTipoDocumento = (Anthem.Image)e.Row.FindControl("imgTipoDocumento");
                Anthem.HiddenField hdnTipoDoc = (Anthem.HiddenField)e.Row.FindControl("hdnTipoDocumento");
                imgTipoDocumento.ImageUrl = ArchivoRecursos.ObtenerValorNodo(hdnTipoDoc.Value.ToLower());

            }
        }
        protected void btnGuardarDocSolicitud_Click(object sender, EventArgs e)
        {
            GuardarDocumentoSolicitud();
        }
        private void GuardarDocumentoSolicitud()
        {
            try
            {
                //Declaración de Variables
                var ObjetoArchivoRep = new ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();

                if (!fileDocSolicitud.HasFile)
                {
                    Controles.MensajeEnControl(fileDocSolicitud.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarArchivo.ToString()));
                    return;
                }

                var ObjetoConfiguracion = NegConfiguracionGeneral.Obtener();
                if (fileDocSolicitud.PostedFile.ContentLength > ObjetoConfiguracion.TamanioArchivoBytes)
                {
                    fileDocSolicitud.Attributes.Clear();
                    Controles.MensajeEnControl(fileDocSolicitud.ClientID, "Archivo Excede el tamaño Máximo Permitido (" + (ObjetoConfiguracion.TamanioArchivoBytes / 1000).ToString() + " KB)");
                    return;
                }


                if (fileDocSolicitud.HasFiles)
                {
                    foreach (HttpPostedFile uploadedFile in fileDocSolicitud.PostedFiles)
                    {

                        //Agregar documento
                        string filePath = uploadedFile.FileName;
                        string filename = Path.GetFileName(filePath);
                        string ext = Path.GetExtension(filename);
                        string contenttype = String.Empty;

                        Stream fs = uploadedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        if (hfIdArchivoSolicitud.Value == "")
                            hfIdArchivoSolicitud.Value = "-1";
                        ObjetoArchivoRep.IdArchivo = int.Parse(hfIdArchivoSolicitud.Value);
                        ObjetoArchivoRep.IdGrupoDocumento = 7;// Grupo Tasación
                        ObjetoArchivoRep.IdTipoDocumento = 124; //Documento Informe Tasación
                        ObjetoArchivoRep.FechaEmision = DateTime.Parse(txtFechaEmisionDocSolicitud.Text);
                        ObjetoArchivoRep.NombreArchivo = System.IO.Path.GetFileName(uploadedFile.FileName);
                        ObjetoArchivoRep.ExtensionArchivo = System.IO.Path.GetExtension(uploadedFile.FileName);
                        ObjetoArchivoRep.SizeKB = uploadedFile.ContentLength;
                        ObjetoArchivoRep.ContentType = uploadedFile.ContentType;
                        ObjetoArchivoRep.ArchivoVB = bytes;
                        ObjetoArchivoRep.Estado_Id = 1;
                        ObjetoArchivoRep.Rut = -1; //NegPortal.getsetDatosWorkFlow.Rut;
                        ObjetoArchivoRep.NumeroSolicitud = NegSolicitudes.objSolicitudInfo.Id.ToString();
                        ObjetoArchivoRep.SistemaWF = "61";
                        ObjetoArchivoRep.UsuarioCreacion_Id = (int)NegUsuarios.Usuario.Rut;
                        ObjetoArchivoRep.UsuarioModificacion_Id = (int)NegUsuarios.Usuario.Rut;
                        ObjetoArchivoRep.IdUsuarioWF = (int)NegUsuarios.Usuario.Rut;


                        //Ejecucion del procedo para Guardar
                        ObjetoResultado = NegArchivoRep.Guardar(ObjetoArchivoRep);

                        if (!ObjetoResultado.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                            return;
                        }
                    }
                    txtFechaEmisionDocSolicitud.Text = "";
                    fileDocSolicitud.AllowMultiple = true;
                    hfIdArchivoSolicitud.Value = "";
                    CargarDocumentosSolicitud();

                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Portal");
                }
            }
        }
        protected void lnkNombreDocSolicitud_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            DescargarDocumento(NegArchivosRepositorios.ObtenerArchivoRepositorio(Id));
        }

        private void DescargarDocumento(ArchivosRepositoriosInfo oInfo)
        {
            try
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + oInfo.NombreArchivo);
                Response.ContentType = oInfo.ContentType;
                Response.BinaryWrite(oInfo.ArchivoVB);
                Response.End();
                Response.Flush();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.DescargarArchivo.ToString()) + "Portal");
                }
            }
        }

        protected void btnDescargarDocSolicitud_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            DescargarDocumento(NegArchivosRepositorios.ObtenerArchivoRepositorio(Id));
        }

        protected void btnEditarDocSolicitud_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());

            ObtenerDatos(Id, 1);
        }

        protected void btnEliminarDocSolicitud_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            int IdArchivo = int.Parse(gvDocumentosSolicitud.DataKeys[row.RowIndex].Values["IdArchivo"].ToString());
            EliminarArchivoRepositorio(Id, IdArchivo);
        }

        private void ObtenerDatos(int Id, int flagTab)
        {
            try
            {
                var ObjetoArchivoRep = new ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();

                ObjetoArchivoRep.Id = Id;
                ObjetoResultado = NegArchivoRep.Buscar(ObjetoArchivoRep);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoArchivoRep = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoArchivoRep != null)
                    {
                        if (flagTab == 1)
                            LlenarFormularioDocSolicitud(ObjetoArchivoRep);
                        // else if (flagTab == 3)
                        //LlenarFormularioRep(ObjetoArchivoRep);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Portal");
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "GrupoDocumento");
                }
            }

        }

        private void LlenarFormularioDocSolicitud(ArchivosRepositoriosInfo oArchivoRepositorio)
        {
            try
            {
                hfIdArchivoSolicitud.Value = oArchivoRepositorio.IdArchivo.ToString();
                txtFechaEmisionDocSolicitud.Text = oArchivoRepositorio.FechaEmision.ToShortDateString();
                fileDocSolicitud.AllowMultiple = false;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Portal");
                }
            }
        }

        private void EliminarArchivoRepositorio(int Id, int IdArchivo)
        {
            try
            {
                //Declaración de Variables
                var ObjetoArchivoRep = new ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();

                ObjetoArchivoRep.Id = Id;
                ObjetoArchivoRep.IdArchivo = IdArchivo;

                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegArchivoRep.Eliminar(ObjetoArchivoRep);

                if (ObjetoResultado.ResultadoGeneral)
                {

                    CargarDocumentosSolicitud();
                    Controles.MostrarMensajeExito("Archivo Eliminado Correctamente");
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
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Portal");
                }
            }
        }
        protected void btnDocumental_Click(object sender, EventArgs e)
        {
            NegPortal.getsetDatosWorkFlow = new Entidades.Documental.DatosWorkflow();
            NegPortal.getsetDatosWorkFlow.Rut = -1;
            Controles.AbrirPopup("~/Aplicaciones/Documental.aspx", "Carpeta Digital");
        }
    }
}