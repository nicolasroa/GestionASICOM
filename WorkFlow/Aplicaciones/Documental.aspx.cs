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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Aplicaciones
{
    public partial class Documental : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioDocumental();");
            if (!Page.IsPostBack)
            {
                Controles.EjecutarJavaScript("MostrarFormularioSubirDocSolicitud();");

                if (!ObtenerDatosWorkflow())
                {
                    return;
                }
                CargarGrupoDocumento();
                CargarTipoDocumentos(-1, 0);
                CargarDocumentosSolicitud();
                CargarResumenDocumentosSolicitud();
                CargarGrillaBitacora();

            }
        }

        private bool ObtenerDatosWorkflow()
        {
            try
            {
                txtFechaEmisionDocSolicitud.Text = DateTime.Now.ToShortDateString();
                //txtFechaEmisionRep.Text = DateTime.Now.ToShortDateString();

                //Declaración de Variables de Búsqueda
                var ObjetoDatosWF = new DatosWorkflow();
                var NegDatosWF = new NegPortal();

                var ObjResultadoWF = new Resultado<DatosWorkflow>();
                //Asignación datos del WF
                if (NegPortal.getsetDatosWorkFlow.Solicitud_Id > 0)
                    ObjetoDatosWF.Solicitud_Id = NegPortal.getsetDatosWorkFlow.Solicitud_Id;
                else
                    ObjetoDatosWF.Solicitud_Id = NegBandejaEntrada.oBandejaEntrada.Solicitud_Id;
                ObjetoDatosWF.UsuarioWF = (int)NegUsuarios.Usuario.Rut;
                ObjetoDatosWF.Evento_Id = NegBandejaEntrada.oBandejaEntrada.Evento_Id;
                if (NegPortal.getsetDatosWorkFlow != null)
                {
                    if (NegPortal.getsetDatosWorkFlow.Rut != -1)
                    {
                        ObjetoDatosWF.Rut = NegPortal.getsetDatosWorkFlow.Rut;

                    }
                }


                //Ejecución del Proceso de Búsqueda
                ObjResultadoWF = NegDatosWF.Buscar(ObjetoDatosWF);

                if (ObjResultadoWF.ResultadoGeneral == true)
                {
                    ObjetoDatosWF = ObjResultadoWF.Lista.FirstOrDefault();

                    if (ObjetoDatosWF != null)
                    {

                        //ObjetoDatosWF.IdUsuario = int.Parse(lblUsuarioWF.Text);
                        NegPortal.getsetDatosWorkFlow = ObjetoDatosWF;
                        NegPortal.getsetDatosWorkFlow.lstParticipantes = new List<Participantes>();
                        if (ObjetoDatosWF.NombreSolicitante != null)
                        {
                            lblNombreSolicitante.Text = ObjetoDatosWF.NombreSolicitante;
                            lblRutSolicitante.Text = ObjetoDatosWF.Rut.ToString() + '-' + ObjetoDatosWF.Dv;
                            lblSolicitud.Text = ObjetoDatosWF.Solicitud_Id.ToString();
                            lblProceso.Text = ObjetoDatosWF.DescripcionMalla;

                            if (ObjetoDatosWF.Solicitud_Id == 0)
                            {
                                Controles.EjecutarJavaScript("InhabilitarSolicitudes();");


                            }
                            foreach (var participante in ObjResultadoWF.Lista)
                            {
                                NegPortal.getsetDatosWorkFlow.lstParticipantes.Add(new Participantes
                                {
                                    RutParticipante = participante.Rut,
                                    DescripcionTipoParticipacion = participante.DescripcionTipoParticipacion,
                                    TipoParticipacion_Id = participante.TipoParticipacion_Id,
                                    NombreParticipante = participante.NombreSolicitante
                                });
                            }

                            //Controles.CargarCombo<Participantes>(ref ddlParticipantesRep, NegPortal.getsetDatosWorkFlow.lstParticipantes, "RutParticipante", "NombreParticipante", "-- Participantes --", "-1");

                        }
                        else
                        {
                            lblNombreSolicitante.Text = "Sin Participante Ingresado";
                            lblRutSolicitante.Text = "Sin Participante Ingresado";
                            lblSolicitud.Text = ObjetoDatosWF.Solicitud_Id.ToString();
                            lblProceso.Text = ObjetoDatosWF.DescripcionMalla;
                            //tabParticipantes.Visible = false;
                        }
                        return true;

                    }
                    else
                    {

                    }
                }
                else
                {
                    if (Constantes.ModoDebug == true)
                    {
                        Controles.MostrarMensajeError(ObjResultadoWF.Mensaje);

                    }
                    else
                    {
                        Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "GrupoDocumento");
                    }
                    return false;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "GrupoDocumento");
                }
                return false;
            }
            return false;
        }

        private void CargarGrupoDocumento()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoGrupoDoc = new GruposDocumentos();
                var NegGrupoDoc = new NegGrupoDocumentos();
                var ObjResultadoGD = new Resultado<GruposDocumentos>();

                //Asignación Grupos de Documentos
                ObjetoGrupoDoc.Id = -1;
                ObjetoGrupoDoc.GrupoDocumento = "";

                //Ejecución del Proceso de Búsqueda
                ObjResultadoGD = NegGrupoDoc.ObtenerGrupoDocumentos(ObjetoGrupoDoc);

                if (ObjResultadoGD.ResultadoGeneral)
                {
                    Controles.CargarCombo<GruposDocumentos>(ref ddlGrupoDocSolicitud, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Seleccione --", "-1");
                    Controles.CargarCombo<GruposDocumentos>(ref ddlBusqGrupoSolicitud, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Todos --", "-1");
                    //Controles.CargarCombo<GruposDocumentos>(ref ddlGrupoDocumentoRep, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Seleccione --", "-1");
                    //Controles.CargarCombo<GruposDocumentos>(ref ddlGrupoDocumentoRepFil, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Todos --", "-1");
                }
                else
                {
                    //Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    Controles.MostrarMensajeAlerta("Grupos de Documentos Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "GrupoDocumento");
                }
            }
        }

        private void CargarTipoDocumentos(int IdGrupoDocumento, int TipoControl)
        {
            try
            {




                var ObjetoTipoDoc = new TiposDocumentosInfo();
                var NegTipoDoc = new NegTiposDocumentos();
                var ObjResultadoTD = new Resultado<TiposDocumentosInfo>();

                //Asignación Tipos de Documentos
                ObjetoTipoDoc.Id = -1;
                ObjetoTipoDoc.IdGrupoDocumento = IdGrupoDocumento;

                ObjResultadoTD = NegTipoDoc.ObtenerTipoDocumentos(ObjetoTipoDoc);

                if (ObjResultadoTD.ResultadoGeneral)
                {
                    NegTiposDocumentos.getLstTipoDoc = ObjResultadoTD.Lista;

                    switch (TipoControl)
                    {
                        case 1:
                            if (IdGrupoDocumento == -1)
                                Controles.CargarCombo<TiposDocumentosInfo>(ref ddlTipoDocumentoDocSolicitud, new List<TiposDocumentosInfo>(), Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            else
                                Controles.CargarCombo<TiposDocumentosInfo>(ref ddlTipoDocumentoDocSolicitud, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            break;
                        case 2:
                            if (IdGrupoDocumento == -1)
                                Controles.CargarCombo<TiposDocumentosInfo>(ref ddlBusqTipoDocumentoSolicitud, new List<TiposDocumentosInfo>(), Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            else
                                Controles.CargarCombo<TiposDocumentosInfo>(ref ddlBusqTipoDocumentoSolicitud, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            break;
                        case 3:
                            //Controles.CargarCombo<TiposDocumentosInfo>(ref ddlTipoDocumentoRep, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            break;
                        case 4:
                            //Controles.CargarCombo<TiposDocumentosInfo>(ref ddlTipoDocumentoRepFil, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Todos --", "-1");
                            break;
                        default:
                            Controles.CargarCombo<TiposDocumentosInfo>(ref ddlTipoDocumentoDocSolicitud, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            Controles.CargarCombo<TiposDocumentosInfo>(ref ddlBusqTipoDocumentoSolicitud, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Todos --", "-1");
                            //Controles.CargarCombo<TiposDocumentosInfo>(ref ddlTipoDocumentoRep, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            //Controles.CargarCombo<TiposDocumentosInfo>(ref ddlTipoDocumentoRepFil, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Todos --", "-1");
                            break;
                    }

                }
                else
                {
                    //Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    Controles.MostrarMensajeAlerta("Grupos de Documentos Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "TipoDocumento");
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
                ObjetoArchRep.NumeroSolicitud = NegPortal.getsetDatosWorkFlow.Solicitud_Id.ToString();
                ObjetoArchRep.IdGrupoDocumento = int.Parse(ddlBusqGrupoSolicitud.SelectedValue);
                ObjetoArchRep.IdTipoDocumento = int.Parse(ddlBusqTipoDocumentoSolicitud.SelectedValue);
                //ObjetoArchRep.IdRolWF = NegPortal.getsetDatosWorkFlow.IdRolWF;


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

        private void CargarResumenDocumentosSolicitud()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoArchivoRep = new ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoArchivoRep.NumeroSolicitud = NegPortal.getsetDatosWorkFlow.Solicitud_Id.ToString();
                ObjetoArchivoRep.IdUsuarioWF = NegUsuarios.Usuario.Rut;

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegArchivoRep.BuscarBandejaGrupoDocumento(ObjetoArchivoRep);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ArchivosRepositoriosInfo>(ref gvResumenDocumentosSolicitud, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Tablas");
                }
            }
        }

        private void GuardarDocumentoSolicitud()
        {
            try
            {
                //Declaración de Variables
                var ObjetoArchivoRep = new ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();

                if (!ValidarFormularioDocSolicitud())
                {
                    return;
                }


                if (fileDocSolicitud.HasFiles)
                {
                    foreach (HttpPostedFile uploadedFile in fileDocSolicitud.PostedFiles)
                    {
                        //Asignacion de Variales
                        if (hfIdDocSolicitud.Value.Length != 0)
                        {
                            ObjetoArchivoRep.Id = int.Parse(hfIdDocSolicitud.Value.ToString());
                            ObjetoArchivoRep = DatosDocSolicitud(ObjetoArchivoRep);
                        }

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
                        ObjetoArchivoRep.IdGrupoDocumento = int.Parse(ddlGrupoDocSolicitud.SelectedValue);
                        ObjetoArchivoRep.IdTipoDocumento = int.Parse(ddlTipoDocumentoDocSolicitud.SelectedValue);
                        ObjetoArchivoRep.FechaEmision = DateTime.Parse(txtFechaEmisionDocSolicitud.Text);
                        ObjetoArchivoRep.Descripcion = txtObservacionDocSolicitud.Text;
                        ObjetoArchivoRep.NombreArchivo = System.IO.Path.GetFileName(uploadedFile.FileName);
                        ObjetoArchivoRep.ExtensionArchivo = System.IO.Path.GetExtension(uploadedFile.FileName);
                        ObjetoArchivoRep.SizeKB = uploadedFile.ContentLength;
                        ObjetoArchivoRep.ContentType = uploadedFile.ContentType;
                        ObjetoArchivoRep.ArchivoVB = bytes;
                        ObjetoArchivoRep.Estado_Id = 1;
                        ObjetoArchivoRep.Rut = -1; //NegPortal.getsetDatosWorkFlow.Rut;
                        ObjetoArchivoRep.NumeroSolicitud = NegPortal.getsetDatosWorkFlow.Solicitud_Id.ToString();
                        ObjetoArchivoRep.SistemaWF = NegPortal.getsetDatosWorkFlow.SistemaWF;
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
                    LimpiarFormularioDocSolicitud();
                    CargarDocumentosSolicitud();
                    CargarResumenDocumentosSolicitud();
                    CargarGrillaBitacora();
                    /*CargarBandejaGrupoDocumento();                    
                    CargarGrillaRepositorioDocumento();
                    CargarBandejaGrupoDocumentoRepositorio();*/

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

        private bool ValidarFormularioDocSolicitud()
        {
            if (ddlGrupoDocSolicitud.SelectedIndex == 0)
            {

                Controles.MensajeEnControl(ddlGrupoDocSolicitud.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarGrupoDocumento.ToString()));
                return false;
            }

            if (ddlTipoDocumentoDocSolicitud.SelectedIndex == 0)
            {

                Controles.MensajeEnControl(ddlTipoDocumentoDocSolicitud.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarTipoDocumento.ToString()));
                return false;
            }

            if (txtFechaEmisionDocSolicitud.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFechaEmisionDocSolicitud.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.IngresarFechaEmision.ToString()));
                return false;
            }

            if (Convert.ToDateTime(txtFechaEmisionDocSolicitud.Text) > DateTime.Now)
            {
                Controles.MensajeEnControl(txtFechaEmisionDocSolicitud.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarFechaEmision.ToString()));
                return false;
            }

            if (!fileDocSolicitud.HasFile)
            {
                Controles.MensajeEnControl(fileDocSolicitud.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarArchivo.ToString()));
                return false;
            }

            var ObjetoConfiguracion = NegConfiguracionGeneral.Obtener();
            if (fileDocSolicitud.PostedFile.ContentLength > ObjetoConfiguracion.TamanioArchivoBytes)
            {
                fileDocSolicitud.Attributes.Clear();
                Controles.MensajeEnControl(fileDocSolicitud.ClientID, "Archivo Excede el tamaño Máximo Permitido (" + (ObjetoConfiguracion.TamanioArchivoBytes / 1000).ToString() + " KB)");
                return false;
            }


            var ObjetoTD = NegTiposDocumentos.ObtenerTipoDocumento(int.Parse(ddlTipoDocumentoDocSolicitud.SelectedValue));
            if (!ObjetoTD.PermisoSubir)
            {
                Controles.MensajeEnControl(ddlTipoDocumentoDocSolicitud.ClientID, "Usted no Tiene Permiso para procesar este Tipo de Documento, Favor contactar al Administrador");
                return false;
            }

            return true;
        }

        private ArchivosRepositoriosInfo DatosDocSolicitud(ArchivosRepositoriosInfo Entidad)
        {
            try
            {
                var ObjetoArchivoRep = new ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();

                ObjetoResultado = NegArchivoRep.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoArchivoRep = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoArchivoRep != null)
                    {
                        return ObjetoArchivoRep;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "ArchivoRepositorio");
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
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "ArchivoRepositorio");
                }
                return null;
            }
        }

        private void LimpiarFormularioDocSolicitud()
        {
            hfIdDocSolicitud.Value = "";
            hfIdArchivoSolicitud.Value = "";
            ddlGrupoDocSolicitud.SelectedIndex = 0;
            CargarTipoDocumentos(-2, 1);
            //ddlTipoDocumento.SelectedIndex = 0;
            txtFechaEmisionDocSolicitud.Text = DateTime.Now.ToShortDateString();
            fileDocSolicitud.Attributes.Clear();
            txtObservacionDocSolicitud.Text = "";
            Controles.EjecutarJavaScript("MostrarFormularioSubirDocSolicitud();");
            fileDocSolicitud.AllowMultiple = true;

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
                hfIdDocSolicitud.Value = oArchivoRepositorio.Id.ToString();
                hfIdArchivoSolicitud.Value = oArchivoRepositorio.IdArchivo.ToString();
                ddlGrupoDocSolicitud.SelectedValue = oArchivoRepositorio.IdGrupoDocumento.ToString();
                CargarTipoDocumentos(oArchivoRepositorio.IdGrupoDocumento, 1);
                ddlTipoDocumentoDocSolicitud.SelectedValue = oArchivoRepositorio.IdTipoDocumento.ToString();
                txtFechaEmisionDocSolicitud.Text = oArchivoRepositorio.FechaEmision.ToShortDateString();
                txtObservacionDocSolicitud.Text = oArchivoRepositorio.Descripcion;
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
                    CargarResumenDocumentosSolicitud();
                    CargarGrillaBitacora();
                    //CargarBandejaGrupoDocumento();
                    //CargarBandejaGrupoDocumentoRepositorio();
                    //CargarGrillaRepositorioDocumento();

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

        protected void btnGuardarDocSolicitud_Click(object sender, EventArgs e)
        {
            GuardarDocumentoSolicitud();
        }

        protected void btnCancelarDocSolicitud_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDocSolicitud();
        }

        protected void btnBuscarDocSolicitud_Click(object sender, EventArgs e)
        {
            CargarDocumentosSolicitud();
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
            Controles.EjecutarJavaScript(" MostrarFormularioSubirDocSolicitudlEditar();");
            ObtenerDatos(Id, 1);
        }

        protected void btnEliminarDocSolicitud_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            int IdArchivo = int.Parse(gvDocumentosSolicitud.DataKeys[row.RowIndex].Values["IdArchivo"].ToString());
            EliminarArchivoRepositorio(Id, IdArchivo);
        }

        protected void ddlGrupoDocSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTipoDocumentos(int.Parse(ddlGrupoDocSolicitud.SelectedValue), 1);
        }

        protected void gvDocumentosSolicitud_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*Imagen dependiendo de la Extension*/
                Anthem.Image imgTipoDocumento = (Anthem.Image)e.Row.FindControl("imgTipoDocumento");
                Anthem.HiddenField hdnTipoDoc = (Anthem.HiddenField)e.Row.FindControl("hdnTipoDocumento");
                imgTipoDocumento.ImageUrl = ArchivoRecursos.ObtenerValorNodo(hdnTipoDoc.Value.ToLower());

                /*Ocultar controles*/
                Anthem.HiddenField hdnId = (Anthem.HiddenField)e.Row.FindControl("IdSolDoc");

                if (string.IsNullOrEmpty(hdnId.Value) || hdnId.Value == "-1")
                {
                    e.Row.Cells[3].Text = "";
                    ((Anthem.ImageButton)e.Row.FindControl("btnDescargarDocSolicitud")).Visible = false;
                    ((Anthem.ImageButton)e.Row.FindControl("btnEditarDocSolicitud")).Visible = false;
                    ((Anthem.ImageButton)e.Row.FindControl("btnEliminarDocSolicitud")).Visible = false;
                    ((Anthem.LinkButton)e.Row.FindControl("lnkNombreDocSolicitud")).Enabled = false;
                    imgTipoDocumento.Visible = false;
                }
                else
                {
                    ((Anthem.ImageButton)e.Row.FindControl("btnDescargarDocSolicitud")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoDescargar")).Value);
                    ((Anthem.ImageButton)e.Row.FindControl("btnEditarDocSolicitud")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoModificar")).Value);
                    ((Anthem.ImageButton)e.Row.FindControl("btnEliminarDocSolicitud")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoEliminar")).Value);
                    ((Anthem.LinkButton)e.Row.FindControl("lnkNombreDocSolicitud")).Enabled = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoDescargar")).Value);
                }
            }
        }

        private void CargarGrillaBitacora()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoBita = new BitacoraInfo();
                var NegBita = new NegBitacora();
                var ObjetoResultado = new Resultado<BitacoraInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoBita.NumeroSolicitud = NegPortal.getsetDatosWorkFlow.Solicitud_Id.ToString();
                ObjetoBita.strRutParticipantes = NegUsuarios.Usuario.Rut.ToString();

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegBita.Buscar(ObjetoBita);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    NegBitacora.getLstBitacora = ObjetoResultado.Lista;

                    if (ObjetoResultado.Lista.Count == 0)
                    {

                        ObjetoBita.DescripcionEvento = " ";
                        ObjetoBita.NombreArchivo = " ";
                        ObjetoBita.TipoDocumento = " ";
                        ObjetoBita.Usuario = " ";
                        ObjetoResultado.Lista.Add(ObjetoBita);
                    }

                    Controles.CargarGrid<BitacoraInfo>(ref gvHistorial, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString() + "Bitacora"));
                }
            }
        }

        //protected void btnExportar_Click(object sender, EventArgs e)
        //{
        //    ExportarAExcel();
        //}

        protected void gvHistorial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHistorial.PageIndex = e.NewPageIndex;
            CargarGrillaBitacora();
        }

        protected void gvHistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == " " && e.Row.Cells[2].Text == " " && e.Row.Cells[3].Text == " ")
                {
                    e.Row.Cells[0].Text = " ";
                }
            }
        }

        private void ExportarAExcel()
        {
            var Resultado = "";
            if (NegBitacora.getLstBitacora == null || NegBitacora.getLstBitacora.Count == 0)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros para Generar el Documento");
                return;
            }
            Resultado = Excel.ExportarGrid<BitacoraInfo>(gvHistorial, NegBitacora.getLstBitacora, "Bitacora", "Reporte de Bitacora");

            if (Resultado != "")
            {
                Controles.MostrarMensajeError(Resultado);
            }

        }

        protected void gvDocumentosSolicitud_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocumentosSolicitud.PageIndex = e.NewPageIndex;
            Controles.CargarGrid<ArchivosRepositoriosInfo>(ref gvDocumentosSolicitud, NegArchivosRepositorios.getsetlstArchivoRepositorio, new string[] { Constantes.StringId, "IdArchivo" });
        }
    }


}