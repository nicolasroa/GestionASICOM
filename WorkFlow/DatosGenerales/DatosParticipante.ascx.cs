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
    public partial class DatosParticipante : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioParticipante();");
            if (!Page.IsPostBack)
            {
                CargarParticipacion();
                CargarTipoPersona();

                CargarTipoDireccion();
                CargarEstadoCivil();
                CargarRegimenMatrimonial();
                CargarNacionalidad();
                CargarSexo();
                CargarNivelEducacional();
                CargarResidencia();
                CargarTipoActividad();
                CargarSituacionLaboral();
                CargarRegion(ref ddlRegion);
                CargarRegion(ref ddlRegionLab);
                CargarProfesiones();
                Controles.CargarCombo<ProvinciaInfo>(ref ddlProvincia, null, "Id", "Descripcion", "-- Provincia--", "-1");
                Controles.CargarCombo<ComunaInfo>(ref ddlComuna, null, "Id", "Descripcion", "-- Comuna--", "-1");
                Controles.CargarCombo<ProvinciaInfo>(ref ddlProvinciaLab, null, "Id", "Descripcion", "-- Provincia--", "-1");
                Controles.CargarCombo<ComunaInfo>(ref ddlComunaLab, null, "Id", "Descripcion", "-- Comuna--", "-1");
                CargaSegDesgravamen();
                CargaSegCesantia();
                CargarParticipantes();
                CargarTipoRelacion();
                CargarNotariasPersoneria();





                if (NegBandejaEntrada.oBandejaEntrada == null || !NegBandejaEntrada.oBandejaEntrada.IndModificaDatosParticipantes)
                {
                    Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.Button>(this).ForEach(c => c.Enabled = false);
                    Controles.ModificarControles<Anthem.LinkButton>(this).ForEach(c => c.Visible = false);
                    btnCancelarDireccion.Visible = true;
                    btnCancelarDireccion.Text = "<span class='glyphicon glyphicon-trash'></span> Limpiar";
                    gvBusqueda.Columns[gvBusqueda.Columns.Count - 1].Visible = false;
                    gvDirecciones.Columns[gvDirecciones.Columns.Count - 1].Visible = false;
                    gvDatosLaborales.Columns[gvDatosLaborales.Columns.Count - 1].Visible = false;
                }
            }



        }

        private void CargarParticipacion()
        {

            try
            {
                NegParticipante oNeg = new NegParticipante();
                TipoParticipanteInfo oFiltro = new TipoParticipanteInfo();
                Resultado<TipoParticipanteInfo> rParticipante = new Resultado<TipoParticipanteInfo>();
                oFiltro.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                rParticipante = oNeg.BuscarTipoParticipante(oFiltro);
                if (rParticipante.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoParticipanteInfo>(ref ddlTipoParticipacion, rParticipante.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Participación --", "-1");
                    txtPorcentajeDeuda.Text = "0";
                    txtPorcentajeDeuda.Enabled = false;
                    txtPorcentajeDominio.Text = "0";
                    txtPorcentajeDominio.Enabled = false;
                    txtPorcentajeDesgravamen.Text = "0";
                    txtPorcentajeDesgravamen.Enabled = false;
                    ddlSeguroDesgravamen.ClearSelection();
                    ddlSeguroDesgravamen.Enabled = false;
                    txtTasaSeguroDesgravamen.Text = "0";
                    txtPrimaSeguroDesgravamen.Text = "0";

                    ddlSeguroCesantia.ClearSelection();
                    ddlSeguroCesantia.Enabled = false;
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                }
                else
                {
                    Controles.MostrarMensajeError(rParticipante.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar el Combo Tipo de Participantes");
                }
            }

        }

        private void CargarTipoPersona()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("TIPO_PERSONA");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlTipoPersona, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Tipo de Persona --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Tipo de Persona Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Tipo Persona");
                }
            }
        }

        private void CargarTipoDireccion()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("TIPO_DIRECCIONES");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlTipoDireccion, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Tipo de Dirección --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Tipo de Dirección Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Tipo Persona");
                }
            }
        }
        private void CargarEstadoCivil()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("ESTADO_CIVIL");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlEstadoCivil, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Estado Civil --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlEstadoCivilRelacionado, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Estado Civil --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Estado Civil Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado Civil");
                }
            }
        }
        private void CargarRegimenMatrimonial()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("REGIMEN_MATRI");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlRegimenMatrimonial, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Regimen Matrimonial --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlRegimenMatrimonialRelacionado, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Regimen Matrimonial --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Regimen Matrimonial Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Regiman matrimonial");
                }
            }
        }
        private void CargarNacionalidad()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("NACIONALIDAD");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlNacionalidad, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Nacionalidad --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlNacionalidadRelacionado, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Nacionalidad --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Nacionalidad Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Nacionalidad");
                }
            }
        }
        private void CargarSexo()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("SEXO");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlSexo, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Sexo --", "-1");

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
        private void CargarNivelEducacional()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("NIVEL_EDUCACION");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlNivelEducacional, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Nivel Educacacional --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Nivel Educacional Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Nivel Eduacional");
                }
            }
        }
        private void CargarResidencia()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("RESIDENCIA");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlResidencia, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Residencia --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlResidenciaRelacionado, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Residencia --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Residencia Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Residencia");
                }
            }
        }
        private void CargarRegion(ref Anthem.DropDownList Combo)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new RegionInfo();
                var ObjetoResultado = new Resultado<RegionInfo>();
                var NegComuna = new NegRegion();

                ////Asignacion de Variables
                ObjetoResultado = NegComuna.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<RegionInfo>(ref Combo, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Región--", "-1");

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Comuna");
                }
            }
        }
        private void CargarProvincia(ref Anthem.DropDownList Combo, int Region_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ProvinciaInfo();
                var ObjetoResultado = new Resultado<ProvinciaInfo>();
                var NegComuna = new NegProvincia();

                ////Asignacion de Variables
                ObjInfo.Region_Id = Region_Id;
                ObjetoResultado = NegComuna.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProvinciaInfo>(ref Combo, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Provincia--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Comuna");
                }
            }
        }
        private void CargarComuna(ref Anthem.DropDownList Combo, int Provincia_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ComunaInfo();
                var ObjetoResultado = new Resultado<ComunaInfo>();
                var NegComuna = new NegComunas();

                ////Asignacion de Variables
                ObjInfo.Provincia_Id = Provincia_Id;
                ObjetoResultado = NegComuna.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ComunaInfo>(ref Combo, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Comuna--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Comuna");
                }
            }
        }
        private void CargarTipoActividad()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("TIPO_ACTIVIDAD");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlTipoActividad, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Tipo de Actividad --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Tipo de Actividad Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Residencia");
                }
            }
        }

        private void CargarSituacionLaboral()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("SITUACION_LABORAL");
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlSituacionLaboral, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Tipo de Actividad --", "-1");

                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Tipo de Actividad Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Residencia");
                }
            }
        }
        private void CargaSegDesgravamen()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                ////Asignacion de Variables
                ObjInfo.GrupoSeguro_Id = 2;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroDesgravamen, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Seguro Desgravamen--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Seguro Desgravamen");
                }
            }
        }
        private void CargaSegCesantia()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                ////Asignacion de Variables
                ObjInfo.GrupoSeguro_Id = 3;
                ObjInfo.Subsidio_Id = NegSolicitudes.objSolicitudInfo.Subsidio_Id;
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjResultado = objNegocio.Buscar(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<SeguroInfo>(ref ddlSeguroCesantia, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Seguro Cesantía--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Seguro Cesantía");
                }
            }
        }
        public void CargarParticipantes()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var oParticipante = new ParticipanteInfo();
                var NegParticipantes = new NegParticipante();
                var oResultado = new Resultado<ParticipanteInfo>();

                //Asignación de Variables de Búsqueda
                oParticipante.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                //Ejecución del Proceso de Búsqueda
                oResultado = NegParticipantes.BuscarParticipante(oParticipante);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ParticipanteInfo>(ref gvBusqueda, oResultado.Lista, new string[] { Constantes.StringId, "Rut" });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Participantes");
                }
            }
        }
        //Datos Personales
        //######################################################################################################
        private bool GrabarDatosPersonales()
        {
            try
            {
                if (!ValidarDatosPersonales()) return false;
                NegClientes nCliente = new NegClientes();
                ClientesInfo oCliente = new ClientesInfo();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();
                if (hfCliente.Value.Length != 0)
                {
                    oCliente.Rut = int.Parse(hfCliente.Value);
                    oCliente = DatosEntidadCliente(oCliente);
                }
                else
                {
                    oCliente.Rut = int.Parse(txtRutCliente.Text.Split('-')[0].ToString());
                    oCliente.Dv = txtRutCliente.Text.Split('-')[1].ToString();
                }
                oCliente.TipoPersona_Id = int.Parse(ddlTipoPersona.SelectedValue);
                oCliente.Nombre = txtNombre.Text;
                oCliente.Paterno = txtPaterno.Text;
                oCliente.Materno = txtMaterno.Text;
                oCliente.EstadoCivil_Id = int.Parse(ddlEstadoCivil.SelectedValue);
                oCliente.RegimenMatrimonial_Id = int.Parse(ddlRegimenMatrimonial.SelectedValue);
                oCliente.Nacionalidad_Id = int.Parse(ddlNacionalidad.SelectedValue);
                oCliente.Sexo_Id = int.Parse(ddlSexo.SelectedValue);
                oCliente.NivelEducacional_Id = int.Parse(ddlNivelEducacional.SelectedValue);
                oCliente.Profesion_Id = int.Parse(ddlProfesion.SelectedValue);
                oCliente.Residencia_Id = int.Parse(ddlResidencia.SelectedValue);
                DateTime Fecha = DateTime.Now;
                if (DateTime.TryParse(txtFechaNacimiento.Text, out Fecha))
                    oCliente.FechaNacimiento = Fecha;
                oCliente.NumeroHijos = txtNumeroHijos.Text == "" ? 0 : int.Parse(txtNumeroHijos.Text);
                oCliente.CargasFamiliares = txtCargasFamiliares.Text == "" ? 0 : int.Parse(txtCargasFamiliares.Text);

                oCliente.Mail = txtMail.Text;
                oCliente.TelefonoMovil = txtCelular.Text;
                oCliente.TelefonoFijo = txtFono.Text;

                rCliente = nCliente.Guardar(ref oCliente);
                if (rCliente.ResultadoGeneral)
                {
                    if (!ProcesarDirecciones(oCliente.Rut)) return false;
                    NegClientes.objClienteInfo = oCliente;
                    Controles.MostrarMensajeExito("Datos Guardados Correctamente");
                }
                else
                {
                    Controles.MostrarMensajeError(rCliente.Mensaje);
                    return false;
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
                    Controles.MostrarMensajeError("Error al Grabar Datos Personales");
                }
                return false;
            }
        }
        private ClientesInfo DatosEntidadCliente(ClientesInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<ClientesInfo>();
                var oCliente = new ClientesInfo();
                var oNegCliente = new NegClientes();

                oResultado = oNegCliente.Buscar(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oCliente = oResultado.Lista.FirstOrDefault();

                    if (oCliente != null)
                    {
                        return oCliente;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Cliente");
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Accion");

                }
                return null;
            }
        }
        private void LlenarFormularioDatosPersonales(ClientesInfo oCliente)
        {
            try
            {
                LimpiarDatosCliente();
                hfCliente.Value = oCliente.Rut.ToString();
                txtRutCliente.Text = oCliente.Rut.ToString() + '-' + oCliente.Dv;
                txtRutCliente.Enabled = false;
                ddlTipoPersona.SelectedValue = oCliente.TipoPersona_Id.ToString();
                SeleccionaTipoPersona();
                txtNombre.Text = oCliente.Nombre;
                txtPaterno.Text = oCliente.Paterno;
                txtMaterno.Text = oCliente.Materno;
                ddlEstadoCivil.SelectedValue = oCliente.EstadoCivil_Id.ToString();
                ddlRegimenMatrimonial.SelectedValue = oCliente.RegimenMatrimonial_Id.ToString();
                ddlNacionalidad.SelectedValue = oCliente.Nacionalidad_Id.ToString();
                ddlSexo.SelectedValue = oCliente.Sexo_Id.ToString();
                ddlNivelEducacional.SelectedValue = oCliente.NivelEducacional_Id.ToString();
                ddlProfesion.SelectedValue = oCliente.Profesion_Id.ToString();
                ddlResidencia.SelectedValue = oCliente.Residencia_Id.ToString();
                txtFechaNacimiento.Text = oCliente.FechaNacimiento.ToShortDateString();
                txtNumeroHijos.Text = oCliente.NumeroHijos.ToString();
                txtCargasFamiliares.Text = oCliente.CargasFamiliares.ToString();

                CargarRegion(ref ddlRegion);

                txtMail.Text = oCliente.Mail;
                txtCelular.Text = oCliente.TelefonoMovil;
                txtFono.Text = oCliente.TelefonoFijo;

                NegClientes.objClienteInfo = new ClientesInfo();
                NegClientes.objClienteInfo = oCliente;
                CargarAntecedentesLaborales(oCliente.Rut);
                CargarDirecciones(oCliente.Rut);
                if (oCliente.TipoPersona_Id == 2 || oCliente.RegimenMatrimonial_Id == 3)//Persona Jurídica o Con Regimen Matrimonial Sociedad Conyugal
                {
                    CargarClientesRelacionados(oCliente.Rut);
                    Controles.EjecutarJavaScript("DesplegarDatosRelacionados('true');");
                }
                else
                {
                    Controles.EjecutarJavaScript("DesplegarDatosRelacionados('false');");
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
                    Controles.MostrarMensajeError("Error al Cargar Datos Personales");
                }
            }

        }
        private void LimpiarDatosCliente(bool LimpiaRut = true)
        {
            try
            {
                btnCancelar.Text = "Cancelar Ingreso de Participante";
                hfCliente.Value = "";
                if (LimpiaRut)
                    txtRutCliente.Text = "";
                txtRutCliente.Enabled = true;
                ddlTipoPersona.ClearSelection();
                SeleccionaTipoPersona();
                ddlTipoParticipacion.ClearSelection();
                txtNombre.Text = "";
                txtPaterno.Text = "";
                txtMaterno.Text = "";
                ddlEstadoCivil.ClearSelection();
                ddlRegimenMatrimonial.ClearSelection();
                ddlNacionalidad.ClearSelection();
                ddlSexo.ClearSelection();
                ddlNivelEducacional.ClearSelection();
                ddlProfesion.ClearSelection();
                ddlResidencia.ClearSelection();
                txtFechaNacimiento.Text = "";
                txtNumeroHijos.Text = "";
                txtCargasFamiliares.Text = "";
                ddlRegion.ClearSelection();
                Controles.CargarCombo<ProvinciaInfo>(ref ddlProvincia, null, "Id", "Descripcion", "-- Provincia--", "-1");
                Controles.CargarCombo<ComunaInfo>(ref ddlComuna, null, "Id", "Descripcion", "-- Comuna--", "-1");
                txtDireccion.Text = "";
                txtNumeroDireccion.Text = "";
                txtNroDpto.Text = "";
                txtMail.Text = "";
                txtCelular.Text = "";
                txtFono.Text = "";
                NegClientes.lstDireccionesCliente = new List<DireccionClienteInfo>();
                Controles.CargarGrid<ParticipanteInfo>(ref gvDirecciones, new List<ParticipanteInfo>(), new string[] { });
                Controles.CargarGrid<AntecedentesLaboralesInfo>(ref gvDatosLaborales, new List<AntecedentesLaboralesInfo>(), new string[] { });
                LimpiarDatosDireccion();
                LimpiarDatosLaborales();
                LimpiarDatosGenerales();
                LimpiarClienteRelacionado();
                Controles.EjecutarJavaScript("DesplegarDatosPersonales('false');");
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Limpiar Datos Personales");
                }
            }
        }
        private void LimpiarDatosDireccion()
        {
            ddlRegion.ClearSelection();
            Controles.CargarCombo<ProvinciaInfo>(ref ddlProvincia, null, "Id", "Descripcion", "-- Provincia--", "-1");
            Controles.CargarCombo<ComunaInfo>(ref ddlComuna, null, "Id", "Descripcion", "-- Comuna--", "-1");
            txtDireccion.Text = "";
            txtNumeroDireccion.Text = "";
            txtNroDpto.Text = "";
            ddlTipoDireccion.ClearSelection();
            //NegClientes.objDireccionesCliente = null;
        }
        private bool ValidarDatosPersonales()
        {
            try
            {
                if (txtRutCliente.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Rut del Participante");
                    return false;
                }
                if (ddlTipoPersona.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar el Tipo de Persona del Participante");
                    return false;
                }
                if (txtNombre.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Nombre Cliente/Razón Social del Participante");
                    return false;
                }
                if (txtPaterno.Text.Length == 0 && txtPaterno.Enabled)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Apellido Paterno del Participante");
                    return false;
                }
                if (txtMaterno.Text.Length == 0 && txtMaterno.Enabled)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Apellido Materno del Participante");
                    return false;
                }
                if (txtMail.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Mail del Participante");
                    return false;
                }
                //if (ddlEstadoCivil.SelectedValue == "-1" && ddlEstadoCivil.Enabled)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Seleccionar el Estado Civil del Participante");
                //    return false;
                //}
                //if (ddlRegimenMatrimonial.SelectedValue == "-1" && ddlRegimenMatrimonial.Enabled && ddlEstadoCivil.SelectedValue == "1")//CASADO-SOC.CONYUGAL 
                //{
                //    Controles.MostrarMensajeAlerta("Debe Seleccionar el Regimen Matrimonial del Participante");
                //    return false;
                //}
                //if (ddlNacionalidad.SelectedValue == "-1" && ddlNacionalidad.Enabled)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Seleccionar la Nacionalidad del Participante");
                //    return false;
                //}
                //if (ddlSexo.SelectedValue == "-1" && ddlSexo.Enabled)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Seleccionar el Sexo del Participante");
                //    return false;
                //}
                //if (ddlNivelEducacional.SelectedValue == "-1" && ddlNivelEducacional.Enabled)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Seleccionar el Nivel Educacional del Participante");
                //    return false;
                //}
                //if (ddlResidencia.SelectedValue == "-1" && ddlResidencia.Enabled)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Seleccionar la Residencia del Participante");
                //    return false;
                //}
                //if (txtFechaNacimiento.Text.Length == 0 && txtPaterno.Enabled)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar la Fecha de Nacimiento del Participante");
                //    return false;
                //}
                //if (txtFechaNacimiento.Text.Length == 0 && txtPaterno.Enabled == false)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar la Fecha de Inicio de Actividades del Participante");
                //    return false;
                //}

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
                    Controles.MostrarMensajeError("Error al Validar Datos Personales");
                }
                return false;
            }
        }
        private void CargarProfesiones()
        {
            try
            {
                ProfesionesInfo oProfesion = new ProfesionesInfo();
                NegClientes nClientes = new NegClientes();
                Resultado<ProfesionesInfo> rProfesion = new Resultado<ProfesionesInfo>();


                rProfesion = nClientes.BuscarProfesiones(oProfesion);

                if (rProfesion.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProfesionesInfo>(ref ddlProfesion, rProfesion.Lista, "Id", "Descripcion", "-- Profesión --", "-1");
                    Controles.CargarCombo<ProfesionesInfo>(ref ddlProfesionRelacionado, rProfesion.Lista, "Id", "Descripcion", "-- Profesión --", "-1");
                    Controles.EjecutarJavaScript("ActivaDrop();");
                }
                else
                {
                    Controles.MostrarMensajeError(rProfesion.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Profesiones");
                }
            }
        }
        private void CargarDirecciones(int Cliente_Id)
        {
            try
            {
                NegClientes nCliente = new NegClientes();
                Resultado<DireccionClienteInfo> rDireccion = new Resultado<DireccionClienteInfo>();
                DireccionClienteInfo oDireccion = new DireccionClienteInfo();

                oDireccion.Cliente_Id = Cliente_Id;
                rDireccion = nCliente.BuscarDireccion(oDireccion);
                if (rDireccion.ResultadoGeneral)
                {
                    Controles.CargarGrid<DireccionClienteInfo>(ref gvDirecciones, rDireccion.Lista.Where(d => d.Id != -1).ToList<DireccionClienteInfo>(), new string[] { "Id", "TipoDireccion_Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(rDireccion.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar las Direcciones");
                }
            }
        }
        private void GrabarDireccion()
        {
            try
            {
                DireccionClienteInfo oDireccion = new DireccionClienteInfo();
                if (NegClientes.lstDireccionesCliente == null)
                    NegClientes.lstDireccionesCliente = new List<DireccionClienteInfo>();


                if (ddlTipoDireccion.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Dirección");
                    return;
                }
                if (ddlRegion.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar la Región Correspondiente a la Dirección del Participante");
                    return;
                }
                if (ddlProvincia.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar la Provincia Correspondiente a la Dirección del Participante");
                    return;
                }
                if (ddlComuna.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar la Comuna Correspondiente a la Dirección del Participante");
                    return;
                }
                if (txtDireccion.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Dirección (Calle) del Participante");
                    return;
                }
                if (txtNumeroDireccion.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar el Número Correspondiente a la Dirección del Participante");
                    return;
                }

                if (NegClientes.objDireccionesCliente == null)
                {
                    oDireccion = new DireccionClienteInfo();
                    oDireccion.Id = 0;
                }
                else
                    oDireccion = NegClientes.objDireccionesCliente;

                oDireccion.Cliente_Id = NegClientes.objClienteInfo.Rut;
                oDireccion.TipoDireccion_Id = int.Parse(ddlTipoDireccion.SelectedValue);
                oDireccion.Comuna_Id = int.Parse(ddlComuna.SelectedValue);
                oDireccion.Provincia_Id = int.Parse(ddlProvincia.SelectedValue);
                oDireccion.Region_Id = int.Parse(ddlRegion.SelectedValue);
                oDireccion.Direccion = txtDireccion.Text;
                oDireccion.Numero = txtNumeroDireccion.Text;
                oDireccion.Departamento = txtNroDpto.Text;
                oDireccion.DescripcionTipoDireccion = ddlTipoDireccion.SelectedItem.Text;
                oDireccion.DireccionCompleta = oDireccion.Direccion + " " + oDireccion.Numero.ToString() + " " + oDireccion.Departamento.ToString() + ", " + ddlComuna.SelectedItem.Text + " " + ddlProvincia.SelectedItem.Text + " " + ddlRegion.SelectedItem.Text;

                if (NegClientes.lstDireccionesCliente.FirstOrDefault(d => d.TipoDireccion_Id == oDireccion.TipoDireccion_Id && d.Id == oDireccion.Id) != null)
                {
                    foreach (var direccion in NegClientes.lstDireccionesCliente.Where(d => d.TipoDireccion_Id == oDireccion.TipoDireccion_Id && d.Id == oDireccion.Id))
                    {
                        direccion.Comuna_Id = int.Parse(ddlComuna.SelectedValue);
                        direccion.Provincia_Id = int.Parse(ddlProvincia.SelectedValue);
                        direccion.Region_Id = int.Parse(ddlRegion.SelectedValue);
                        direccion.Direccion = txtDireccion.Text;
                        direccion.Numero = txtNumeroDireccion.Text;
                        direccion.Departamento = txtNroDpto.Text;
                        direccion.DireccionCompleta = oDireccion.DireccionCompleta;
                    }
                }
                else
                {
                    NegClientes.lstDireccionesCliente.Add(oDireccion);
                }
                Controles.CargarGrid<DireccionClienteInfo>(ref gvDirecciones, NegClientes.lstDireccionesCliente.Where(d => d.Id != -1 && d.Cliente_Id == NegClientes.objClienteInfo.Rut).ToList<DireccionClienteInfo>(), new string[] { "Id", "TipoDireccion_Id" });
                LimpiarDatosDireccion();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Grabar la Dirección");
                }
            }
        }
        private bool ProcesarDirecciones(int Cliente_Id)
        {
            try
            {
                NegClientes nCliente = new NegClientes();
                Resultado<DireccionClienteInfo> rDireccion = new Resultado<DireccionClienteInfo>();
                DireccionClienteInfo oDireccion = new DireccionClienteInfo();
                if (NegClientes.lstDireccionesCliente != null)
                {
                    foreach (var direccion in NegClientes.lstDireccionesCliente.Where(d => d.Id != -1))
                    {
                        direccion.Usuario_Id = NegUsuarios.Usuario.Rut;
                        direccion.Cliente_Id = Cliente_Id;
                        rDireccion = nCliente.GuardarDireccion(direccion);
                        if (!rDireccion.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rDireccion.Mensaje);
                            return false;
                        }
                    }
                    foreach (var direccion in NegClientes.lstDireccionesCliente.Where(d => d.Id == -1))
                    {
                        direccion.Usuario_Id = NegUsuarios.Usuario.Rut;
                        direccion.Cliente_Id = Cliente_Id;
                        rDireccion = nCliente.EliminarDireccion(direccion);
                        if (!rDireccion.ResultadoGeneral)
                        {
                            Controles.MostrarMensajeError(rDireccion.Mensaje);
                            return false;
                        }
                    }
                }
                LimpiarDatosDireccion();
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
                    Controles.MostrarMensajeError("Error al Procesar las Direcciones");
                }
                return false;

            }
        }
        private void CargarInfoDireccion(DireccionClienteInfo oDireccion)
        {
            ddlTipoDireccion.SelectedValue = oDireccion.TipoDireccion_Id.ToString();
            ddlRegion.SelectedValue = oDireccion.Region_Id.ToString();
            CargarProvincia(ref ddlProvincia, oDireccion.Region_Id);
            ddlProvincia.SelectedValue = oDireccion.Provincia_Id.ToString();
            CargarComuna(ref ddlComuna, oDireccion.Provincia_Id);
            ddlComuna.SelectedValue = oDireccion.Comuna_Id.ToString();
            txtDireccion.Text = oDireccion.Direccion;
            txtNumeroDireccion.Text = oDireccion.Numero;
            txtNroDpto.Text = oDireccion.Departamento;
            NegClientes.objDireccionesCliente = new DireccionClienteInfo();
            NegClientes.objDireccionesCliente = oDireccion;

        }
        private void EliminarDireccion(int TipoDireccion_Id)
        {
            try
            {
                foreach (var direccion in NegClientes.lstDireccionesCliente.Where(d => d.TipoDireccion_Id == TipoDireccion_Id))
                {
                    direccion.Id = -1;
                }
                Controles.CargarGrid<DireccionClienteInfo>(ref gvDirecciones, NegClientes.lstDireccionesCliente.Where(d => d.Id != -1).ToList<DireccionClienteInfo>(), new string[] { "Id", "TipoDireccion_Id" });
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Eliminar la Dirección");
                }
            }
        }
        //######################################################################################################
        //Datos Laborales
        //######################################################################################################
        private void GrabarDatosLaborales()
        {
            try
            {
                if (!ValidarFormularioDatosLaborales()) return;
                NegParticipante nLaborales = new NegParticipante();
                AntecedentesLaboralesInfo oLaborales = new AntecedentesLaboralesInfo();
                Resultado<AntecedentesLaboralesInfo> rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                if (hfLaborales.Value.Length != 0)
                {
                    oLaborales.Id = int.Parse(hfLaborales.Value);
                    oLaborales = DatosEntidadDatosLaborales(oLaborales);
                }
                oLaborales.RutCliente = NegClientes.objClienteInfo.Rut;
                oLaborales.RutEmpleador = txtRutEmpleador.Text;
                oLaborales.NombreEmpleador = txtEmpleador.Text;
                oLaborales.TipoActividad_Id = int.Parse(ddlTipoActividad.SelectedValue);
                oLaborales.SituacionLaboral_Id = int.Parse(ddlSituacionLaboral.SelectedValue);
                oLaborales.Cargo = txtCargo.Text;
                if (txtFechaInicioContrato.Text.Length != 0)
                    oLaborales.FechaInicioContrato = DateTime.Parse(txtFechaInicioContrato.Text);
                else
                    oLaborales.FechaInicioContrato = null;


                if (txtFechaTerminoContrato.Text.Length != 0)
                    oLaborales.FechaTerminoContrato = DateTime.Parse(txtFechaTerminoContrato.Text);
                else
                    oLaborales.FechaTerminoContrato = null;
                oLaborales.Region_Id = int.Parse(ddlRegionLab.SelectedValue);
                oLaborales.Provincia_Id = int.Parse(ddlProvinciaLab.SelectedValue);
                oLaborales.Comuna_Id = int.Parse(ddlComunaLab.SelectedValue);
                oLaborales.Direccion = txtDireccionLab.Text;
                oLaborales.Mail = txtMailLab.Text;
                oLaborales.Fono = txtFonoContacto.Text;

                rLaborales = nLaborales.GuardarAntecedentesLaborales(oLaborales);

                if (rLaborales.ResultadoGeneral)
                {
                    CargarAntecedentesLaborales(oLaborales.RutCliente);
                    LimpiarDatosLaborales();
                    Controles.MostrarMensajeExito("Datos Guardados Correctamente");
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
                    Controles.MostrarMensajeError("Error al Grabar Datos Personales");
                }
            }
        }
        private void CargarAntecedentesLaborales(int Rut)
        {
            try
            {
                AntecedentesLaboralesInfo oFiltro = new AntecedentesLaboralesInfo();
                NegParticipante nLaborales = new NegParticipante();
                Resultado<AntecedentesLaboralesInfo> rLaborales = new Resultado<AntecedentesLaboralesInfo>();

                oFiltro.RutCliente = Rut;
                rLaborales = nLaborales.BuscarAntecedentesLaborales(oFiltro);
                if (rLaborales.ResultadoGeneral)
                {
                    Controles.CargarGrid<AntecedentesLaboralesInfo>(ref gvDatosLaborales, rLaborales.Lista, new string[] { "Id", "RutCliente" });
                }
                else
                {
                    Controles.MostrarMensajeError(rLaborales.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Antecedentes Laborales");

                }
            }
        }
        private AntecedentesLaboralesInfo DatosEntidadDatosLaborales(AntecedentesLaboralesInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<AntecedentesLaboralesInfo>();
                var oLaboral = new AntecedentesLaboralesInfo();
                var oNeg = new NegParticipante();

                oResultado = oNeg.BuscarAntecedentesLaborales(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oLaboral = oResultado.Lista.FirstOrDefault();

                    if (oLaboral != null)
                    {
                        return oLaboral;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Antecedentes Laborales");
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Antecedentes Laborales");

                }
                return null;
            }
        }
        private void LlenaFormularioDatosLaborales(AntecedentesLaboralesInfo oLaborales)
        {
            try
            {
                hfLaborales.Visible = false;
                hfLaborales.Value = oLaborales.Id.ToString();
                txtRutEmpleador.Text = oLaborales.RutEmpleador;
                txtEmpleador.Text = oLaborales.NombreEmpleador;
                ddlTipoActividad.SelectedValue = oLaborales.TipoActividad_Id.ToString();
                ddlSituacionLaboral.SelectedValue = oLaborales.SituacionLaboral_Id.ToString();
                txtCargo.Text = oLaborales.Cargo;
                if (oLaborales.FechaInicioContrato != null)
                    txtFechaInicioContrato.Text = oLaborales.FechaInicioContrato.Value.ToShortDateString();
                if (oLaborales.FechaTerminoContrato != null)
                    txtFechaTerminoContrato.Text = DateTime.Parse(oLaborales.FechaTerminoContrato.ToString()).ToShortDateString();
                ddlRegionLab.SelectedValue = oLaborales.Region_Id.ToString();
                CargarProvincia(ref ddlProvinciaLab, oLaborales.Region_Id);
                ddlProvinciaLab.SelectedValue = oLaborales.Provincia_Id.ToString();
                CargarComuna(ref ddlComunaLab, oLaborales.Provincia_Id);
                ddlComunaLab.SelectedValue = oLaborales.Comuna_Id.ToString();
                txtDireccionLab.Text = oLaborales.Direccion;
                txtMailLab.Text = oLaborales.Mail;
                txtFonoContacto.Text = oLaborales.Fono;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Datos Laborales");
                }
            }
        }
        private void LimpiarDatosLaborales()
        {
            try
            {
                hfLaborales.Value = "";
                txtRutEmpleador.Text = "";
                txtEmpleador.Text = "";
                ddlTipoActividad.ClearSelection();
                ddlSituacionLaboral.ClearSelection();
                txtCargo.Text = "";
                txtFechaInicioContrato.Text = "";
                txtFechaTerminoContrato.Text = "";
                ddlRegionLab.ClearSelection();
                Controles.CargarCombo<ProvinciaInfo>(ref ddlProvinciaLab, null, "Id", "Descripcion", "-- Provincia--", "-1");
                Controles.CargarCombo<ComunaInfo>(ref ddlComunaLab, null, "Id", "Descripcion", "-- Comuna--", "-1");
                txtDireccionLab.Text = "";
                txtMailLab.Text = "";
                txtFonoContacto.Text = "";

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Limpiar Datos Laborales");
                }
            }
        }
        private bool ValidarFormularioDatosLaborales()
        {
            try
            {
                //if (txtRutEmpleador.Text.Length == 0)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar el Rut del Empleador");
                //    return false;
                //}
                //if (txtEmpleador.Text.Length == 0)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar el Nombre del Empleador");
                //    return false;
                //}
                if (ddlTipoActividad.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar el Tipo de Actividad");
                    return false;
                }
                if (ddlSituacionLaboral.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar la Situación Laboral");
                    return false;
                }
                //if (txtFechaInicioContrato.Text.Length == 0)
                //{
                //    Controles.MostrarMensajeAlerta("Debe Ingresar la Fecha de Inicio del Contrato/Actividad");
                //    return false;
                //}

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
                    Controles.MostrarMensajeError("Error al Validar Datos Laborales");
                }
                return false;
            }
        }

        //######################################################################################################
        //Datos Generales
        //######################################################################################################
        private void GrabarDatosGenerales()
        {
            try
            {
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                if (NegClientes.objClienteInfo == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Guardar los Datos del Cliente antes de Continuar");
                    return;
                }
                if (!ValidarFormularioDatosGenerales()) return;
                if (hfParticipante.Value != "")
                {
                    oParticipante.Id = int.Parse(hfParticipante.Value);
                    oParticipante = DatosEntidadDatosGenerales(oParticipante);
                }

                var PorcentajeDeuda = decimal.Zero;
                var PorcentajeDominio = decimal.Zero;
                var PorcentajeDesgravamen = decimal.Zero;
                var TasaSeguroDesgravamen = decimal.Zero;
                var PrimaSeguroDesgravamen = decimal.Zero;
                var TasaSeguroCesantia = decimal.Zero;
                var PrimaSeguroCesantia = decimal.Zero;


                if (!decimal.TryParse(txtPorcentajeDeuda.Text, out PorcentajeDeuda))
                    PorcentajeDeuda = 0;
                if (!decimal.TryParse(txtPorcentajeDominio.Text, out PorcentajeDominio))
                    PorcentajeDominio = 0;
                if (!decimal.TryParse(txtPorcentajeDesgravamen.Text, out PorcentajeDesgravamen))
                    PorcentajeDesgravamen = 0;
                if (!decimal.TryParse(txtTasaSeguroDesgravamen.Text, out TasaSeguroDesgravamen))
                    TasaSeguroDesgravamen = 0;
                if (!decimal.TryParse(txtPrimaSeguroDesgravamen.Text, out PrimaSeguroDesgravamen))
                    PrimaSeguroDesgravamen = 0;
                if (!decimal.TryParse(txtTasaSeguroCesantia.Text, out TasaSeguroCesantia))
                    TasaSeguroCesantia = 0;
                if (!decimal.TryParse(txtPrimaSeguroCesantia.Text, out PrimaSeguroCesantia))
                    PrimaSeguroCesantia = 0;


                oParticipante.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
                oParticipante.Rut = NegClientes.objClienteInfo.Rut;
                oParticipante.TipoParticipacion_Id = int.Parse(ddlTipoParticipacion.SelectedValue);
                oParticipante.PorcentajeDeuda = PorcentajeDeuda;
                oParticipante.PorcentajeDominio = PorcentajeDominio;
                oParticipante.PorcentajeDesgravamen = PorcentajeDesgravamen;
                oParticipante.SeguroDesgravamen_Id = int.Parse(ddlSeguroDesgravamen.SelectedValue);
                oParticipante.TasaSeguroDesgravamen = TasaSeguroDesgravamen / 100;
                oParticipante.PrimaSeguroDesgravamen = PrimaSeguroDesgravamen;
                oParticipante.SeguroCesantia_Id = int.Parse(ddlSeguroCesantia.SelectedValue);
                oParticipante.TasaSeguroCesantia = TasaSeguroCesantia / 100;
                oParticipante.PrimaSeguroCesantia = PrimaSeguroCesantia;

                NegParticipante.ObjParticipante = new ParticipanteInfo();
                NegParticipante.ObjParticipante = oParticipante;



                Resultado<ParticipanteInfo> rGenerales = new Resultado<ParticipanteInfo>();
                NegParticipante nGenerales = new NegParticipante();
                oParticipante = NegParticipante.ObjParticipante;

                rGenerales = nGenerales.GuardarParticipante(ref oParticipante);

                if (rGenerales.ResultadoGeneral)
                {
                    NegParticipante.ObjParticipante = oParticipante;
                    Controles.MostrarMensajeExito("Datos Guardados Correctamente");
                }
                else
                {
                    Controles.MostrarMensajeError(rGenerales.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Grabar Datos Generales");
                }
            }
        }
        private void LlenarDatosGenerales(ParticipanteInfo oParticipante)
        {
            try
            {
                LimpiarDatosGenerales();
                ddlTipoParticipacion.SelectedValue = oParticipante.TipoParticipacion_Id.ToString();
                SeleccionaTipoParticipacion();
                txtPorcentajeDeuda.Text = String.Format("{0:0.00}", oParticipante.PorcentajeDeuda);
                txtPorcentajeDominio.Text = String.Format("{0:0.00}", oParticipante.PorcentajeDominio);
                txtPorcentajeDesgravamen.Text = String.Format("{0:0.00}", oParticipante.PorcentajeDesgravamen);
                ddlSeguroDesgravamen.SelectedValue = oParticipante.SeguroDesgravamen_Id.ToString();
                ddlSeguroCesantia.SelectedValue = oParticipante.SeguroCesantia_Id.ToString();

                if (ddlTipoPersona.SelectedValue != "1")
                {
                    ddlSeguroDesgravamen.ClearSelection();
                    SeleccionarSeguroDesgravamen();

                    ddlSeguroCesantia.ClearSelection();
                    SeleccionarSeguroCesantia();

                    ddlSeguroDesgravamen.Enabled = false;
                    ddlSeguroCesantia.Enabled = false;
                }


                SeleccionarSeguroCesantia();
                SeleccionarSeguroDesgravamen();
                hfParticipante.Value = oParticipante.Id.ToString();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Datos Generales");
                }
            }
        }
        private ParticipanteInfo DatosEntidadDatosGenerales(ParticipanteInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<ParticipanteInfo>();
                var oInfo = new ParticipanteInfo();
                var oNeg = new NegParticipante();

                oResultado = oNeg.BuscarParticipante(Entidad);

                if (oResultado.ResultadoGeneral == true)
                {
                    oInfo = oResultado.Lista.FirstOrDefault();

                    if (oInfo != null)
                    {
                        NegParticipante.ObjParticipante = new ParticipanteInfo();
                        NegParticipante.ObjParticipante = oInfo;
                        return oInfo;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(oResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Participante");
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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Antecedentes Laborales");

                }
                return null;
            }
        }
        private void LimpiarDatosGenerales()
        {
            try
            {
                hfParticipante.Value = "";
                ddlTipoParticipacion.ClearSelection();
                txtPorcentajeDeuda.Text = "";
                txtPorcentajeDominio.Text = "";
                txtPorcentajeDesgravamen.Text = "";
                ddlSeguroDesgravamen.ClearSelection();
                txtTasaSeguroDesgravamen.Text = "";
                txtPrimaSeguroDesgravamen.Text = "";
                ddlSeguroCesantia.ClearSelection();
                txtTasaSeguroCesantia.Text = "";
                txtPrimaSeguroCesantia.Text = "";
                NegParticipante.ObjParticipante = new ParticipanteInfo();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Limpiar Datos Generales");
                }
            }
        }
        private bool ValidarFormularioDatosGenerales()
        {
            try
            {
                if (ddlTipoParticipacion.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Participación");
                    return false;
                }
                if (txtPorcentajeDeuda.Enabled && txtPorcentajeDeuda.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Porcentaje de Deuda");
                    return false;
                }


                if (txtPorcentajeDominio.Enabled && txtPorcentajeDominio.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Porcentaje de Dominio");
                    return false;
                }

                if (txtPorcentajeDesgravamen.Enabled && txtPorcentajeDesgravamen.Text.Length == 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar un Porcentaje de Desgravamen");
                    return false;
                }

                if (ddlSeguroDesgravamen.Enabled && ddlSeguroDesgravamen.SelectedValue == "-1" && txtTasaSeguroDesgravamen.Text.Length > 0)
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar un Seguro de Desgravamen");
                    return false;
                }

                if (ddlSeguroCesantia.Enabled && ddlSeguroCesantia.SelectedValue == "-1")
                {
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                }
                if (NegSolicitudes.objSolicitudInfo.Subsidio_Id != -1 && ddlSeguroCesantia.Enabled && ddlSeguroCesantia.SelectedValue == "-1" && ddlTipoParticipacion.SelectedValue == "1")// Creditos con Subsidio y solo para el Solicitante Principal
                {
                    Controles.MostrarMensajeAlerta("El seguro de Cesantía es Obligatorio para los Créditos con Subsidio");
                    return false;
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
                    Controles.MostrarMensajeError("Error al Validar Datos Generales");
                }
                return false;
            }
        }
        //######################################################################################################
        private void ConfirmarInformacion()
        {
            try
            {
                if (!ValidarDatosPersonales()) return;
                if (!ValidarFormularioDatosGenerales()) return;

                var PorcentajeDeuda = decimal.Zero;

                if (!decimal.TryParse(txtPorcentajeDeuda.Text, out PorcentajeDeuda))
                    PorcentajeDeuda = 0;
                if (NegParticipante.ObjParticipante.Id == -1)
                {
                    Controles.MostrarMensajeAlerta("Debe Grabar los Datos Generales del Participante");
                    return;
                }

                if (ddlTipoPersona.SelectedValue == "1" && gvDatosLaborales.Rows.Count == 0 && PorcentajeDeuda > 0)//Persona Natural con % de Deuda Mayo a 0
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresal los Datos Laborales del Participante");
                    return;
                }

                NegParticipante nGenerales = new NegParticipante();
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                Resultado<ParticipanteInfo> rGenerales = new Resultado<ParticipanteInfo>();

                oParticipante = NegParticipante.ObjParticipante;

                rGenerales = nGenerales.GuardarParticipante(ref oParticipante);

                if (rGenerales.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Datos Guardados Correctamente");
                    CargarParticipantes();
                    ActualizarSegurosContratados(oParticipante);
                    LimpiarFormulario();
                    Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusquedaPart();", true);

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
                    Controles.MostrarMensajeError("Error al Confirmar la Información");
                }

            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ConfirmarInformacion();
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusquedaPart();", true);
        }
        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormularioPart();", true);
        }
        protected void gvBusqueda_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }
        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Rut = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Rut"].ToString());
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
            ClientesInfo oCliente = new ClientesInfo();
            ParticipanteInfo oParticipante = new ParticipanteInfo();
            oParticipante = NegParticipante.lstParticipantes.FirstOrDefault(p => p.Id == Id);
            oCliente.Rut = Rut;
            oCliente = DatosEntidadCliente(oCliente);
            NegClientes.objClienteInfo = oCliente;
            LlenarFormularioDatosPersonales(oCliente);
            Controles.EjecutarJavaScript("DesplegarDatosPersonales('true');");
            LlenarDatosGenerales(oParticipante);
            NegParticipante.ObjParticipante = oParticipante;
            CargarAntecedentesLaborales(oCliente.Rut);

            if (NegBandejaEntrada.oBandejaEntrada == null || !NegBandejaEntrada.oBandejaEntrada.IndModificaDatosParticipantes)
            {
                Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Enabled = false);
                Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.Enabled = false);
                Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Enabled = false);
                Controles.ModificarControles<Anthem.Button>(this).ForEach(c => c.Enabled = false);

                btnCancelar.Text = "Volver a Listado de Participantes";
                btnCancelar.Enabled = true;

            }
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormularioPart();", true);
        }
        private void LimpiarFormulario(bool LimpiarRut = true)
        {
            Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Text = "");
            Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.ClearSelection());
            Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Checked = false);
            LimpiarDatosCliente(LimpiarRut);
            LimpiarDatosGenerales();
            LimpiarDatosDireccion();
            LimpiarDatosLaborales();
            NegClientes.lstDireccionesCliente = new List<DireccionClienteInfo>();
            Controles.CargarGrid<ParticipanteInfo>(ref gvDirecciones, new List<ParticipanteInfo>(), new string[] { });
            hfId.Value = "";
            hfCliente.Value = "";
            hfLaborales.Value = "";
            hfParticipante.Value = "";
            NegClientes.objClienteInfo = new ClientesInfo();
            NegParticipante.ObjParticipante = new ParticipanteInfo();
        }
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedValue == "-1")
            {
                Controles.CargarCombo<ProvinciaInfo>(ref ddlProvincia, null, "Id", "Descripcion", "-- Provincia--", "-1");
            }
            else
            {
                CargarProvincia(ref ddlProvincia, int.Parse(ddlRegion.SelectedValue));

            }
        }
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvincia.SelectedValue == "-1")
            {
                Controles.CargarCombo<ComunaInfo>(ref ddlComuna, null, "Id", "Descripcion", "-- Comuna--", "-1");
            }
            else
            {
                CargarComuna(ref ddlComuna, int.Parse(ddlProvincia.SelectedValue));

            }
        }
        protected void ddlRegionLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegionLab.SelectedValue == "-1")
            {
                Controles.CargarCombo<ProvinciaInfo>(ref ddlProvinciaLab, null, "Id", "Descripcion", "-- Provincia--", "-1");
            }
            else
            {
                CargarProvincia(ref ddlProvinciaLab, int.Parse(ddlRegionLab.SelectedValue));

            }
        }
        protected void ddlProvinciaLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvinciaLab.SelectedValue == "-1")
            {
                Controles.CargarCombo<ComunaInfo>(ref ddlComunaLab, null, "Id", "Descripcion", "-- Comuna--", "-1");
            }
            else
            {
                CargarComuna(ref ddlComunaLab, int.Parse(ddlProvinciaLab.SelectedValue));

            }
        }
        private void SeleccionaTipoPersona()
        {
            try
            {
                if (ddlTipoPersona.SelectedValue == "2")//Jurídica
                {
                    lblNombreRazonSocial.Text = "Razón Social";
                    lblPaternoGiro.Text = "Giro";
                    txtMaterno.Text = "";
                    txtMaterno.Enabled = false;
                    ddlEstadoCivil.ClearSelection();
                    ddlEstadoCivil.Enabled = false;
                    ddlRegimenMatrimonial.ClearSelection();
                    ddlRegimenMatrimonial.Enabled = false;
                    ddlNacionalidad.ClearSelection();
                    ddlNacionalidad.Enabled = false;
                    ddlSexo.ClearSelection();
                    ddlSexo.Enabled = false;
                    ddlNivelEducacional.ClearSelection();
                    ddlNivelEducacional.Enabled = false;
                    ddlProfesion.ClearSelection();
                    ddlProfesion.Enabled = false;
                    ddlResidencia.ClearSelection();
                    ddlResidencia.Enabled = false;
                    lblFechaNacimiento.Text = "Inicio de Actividades";
                    Controles.EjecutarJavaScript("DesplegarDatosLaborales('false');");
                    ddlSeguroDesgravamen.ClearSelection();
                    SeleccionarSeguroDesgravamen();

                    ddlSeguroCesantia.ClearSelection();
                    SeleccionarSeguroCesantia();

                    ddlSeguroDesgravamen.Enabled = false;
                    ddlSeguroCesantia.Enabled = false;
                    txtPorcentajeDesgravamen.Text = "0";
                    txtNumeroHijos.Text = "";
                    txtCargasFamiliares.Text = "";
                    txtNumeroHijos.Enabled = false;
                    txtCargasFamiliares.Enabled = false;

                    ddlNotariaPersoneria.ClearSelection();
                    ddlNotariaPersoneria.Enabled = true;
                    txtFechaPersoneria.Text = "";
                    txtFechaPersoneria.Enabled = true;
                    Controles.EjecutarJavaScript("DesplegarDatosRelacionados('true');");

                }
                else
                {
                    lblNombreRazonSocial.Text = "Nombre";
                    lblPaternoGiro.Text = "Apellido Paterno";
                    ddlSeguroDesgravamen.Enabled = true;
                    ddlSeguroCesantia.Enabled = true;
                    txtMaterno.Text = "";
                    txtMaterno.Enabled = true;
                    ddlEstadoCivil.Enabled = true;
                    ddlRegimenMatrimonial.Enabled = true;
                    ddlNacionalidad.Enabled = true;
                    ddlSexo.Enabled = true;
                    ddlNivelEducacional.Enabled = true;
                    ddlProfesion.Enabled = true;
                    ddlResidencia.Enabled = true;
                    lblFechaNacimiento.Text = "Fecha de Nacimiento";
                    txtNumeroHijos.Enabled = true;
                    txtCargasFamiliares.Enabled = true;
                    if (NegClientes.objClienteInfo != null)
                    {
                        txtPaterno.Text = NegClientes.objClienteInfo.Paterno;
                        txtMaterno.Text = NegClientes.objClienteInfo.Materno;

                    }
                    ddlNotariaPersoneria.Enabled = false;
                    txtFechaPersoneria.Enabled = false;

                    Controles.EjecutarJavaScript("DesplegarDatosLaborales('true');");

                }

                CargarTipoRelacion();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Seleccionar Tipo de Persona");
                }
            }
        }
        protected void ddlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionaTipoPersona();
        }
        protected void btnModificarDatoLaboral_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvDatosLaborales.DataKeys[row.RowIndex].Values["Id"].ToString());
            AntecedentesLaboralesInfo obj = new AntecedentesLaboralesInfo();
            obj.Id = Id;
            obj = DatosEntidadDatosLaborales(obj);
            LlenaFormularioDatosLaborales(obj);
        }
        protected void gvDatosLaborales_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }
        protected void btnGuardarDatosLaborales_Click(object sender, EventArgs e)
        {
            GrabarDatosLaborales();
        }
        protected void btnCancelarDatosLaborales_Click(object sender, EventArgs e)
        {
            LimpiarDatosLaborales();
        }
        protected void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            GrabarDatosPersonales();
        }
        protected void btnCancelarCliente_Click(object sender, EventArgs e)
        {
            LimpiarDatosCliente();


        }
        protected void btnNuevoDatoLaboral_Click(object sender, EventArgs e)
        {
            LimpiarDatosLaborales();
        }
        protected void ddlTipoParticipacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionaTipoParticipacion();
        }
        private void SeleccionaTipoParticipacion()
        {
            try
            {
                if (NegClientes.objClienteInfo == null)
                {
                    Controles.MostrarMensajeAlerta("Debe Grabar los datos de la Personal ANtes de Continuar");
                    txtPorcentajeDeuda.Text = "0";
                    txtPorcentajeDeuda.Enabled = false;
                    txtPorcentajeDominio.Text = "0";
                    txtPorcentajeDominio.Enabled = false;
                    txtPorcentajeDesgravamen.Text = "0";
                    txtPorcentajeDesgravamen.Enabled = false;
                    ddlSeguroDesgravamen.ClearSelection();
                    ddlSeguroDesgravamen.Enabled = false;
                    txtTasaSeguroDesgravamen.Text = "0";
                    txtPrimaSeguroDesgravamen.Text = "0";

                    ddlSeguroCesantia.ClearSelection();
                    ddlSeguroCesantia.Enabled = false;
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                    return;

                }

                if (ddlTipoParticipacion.SelectedValue == "-1")
                {
                    txtPorcentajeDeuda.Text = "0";
                    txtPorcentajeDeuda.Enabled = false;
                    txtPorcentajeDominio.Text = "0";
                    txtPorcentajeDominio.Enabled = false;
                    txtPorcentajeDesgravamen.Text = "0";
                    txtPorcentajeDesgravamen.Enabled = false;
                    ddlSeguroDesgravamen.ClearSelection();
                    ddlSeguroDesgravamen.Enabled = false;
                    txtTasaSeguroDesgravamen.Text = "0";
                    txtPrimaSeguroDesgravamen.Text = "0";

                    ddlSeguroCesantia.ClearSelection();
                    ddlSeguroCesantia.Enabled = false;
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                }
                else
                {
                    TipoParticipanteInfo oParticipacion = new TipoParticipanteInfo();
                    oParticipacion = NegParticipante.lstTipoParticipante.FirstOrDefault(tp => tp.Id == int.Parse(ddlTipoParticipacion.SelectedValue));
                    if (oParticipacion != null)
                    {
                        txtPorcentajeDeuda.Enabled = oParticipacion.IndDeudor == true ? true : false;
                        txtPorcentajeDominio.Enabled = oParticipacion.IndDominio == true ? true : false;
                        txtPorcentajeDesgravamen.Enabled = oParticipacion.IndDesgravamen == true ? true : false;
                        ddlSeguroDesgravamen.Enabled = oParticipacion.IndDesgravamen == true ? true : false;
                        ddlSeguroCesantia.Enabled = oParticipacion.IndCesantia == true ? true : false;





                    }

                    if (NegClientes.objClienteInfo.TipoPersona_Id == 2)//Persona Juridica
                    {
                        txtPorcentajeDesgravamen.Text = "0";
                        txtPorcentajeDesgravamen.Enabled = false;
                        ddlSeguroDesgravamen.ClearSelection();
                        ddlSeguroDesgravamen.Enabled = false;
                        txtTasaSeguroDesgravamen.Text = "0";
                        txtPrimaSeguroDesgravamen.Text = "0";

                        ddlSeguroCesantia.ClearSelection();
                        ddlSeguroCesantia.Enabled = false;
                        txtTasaSeguroCesantia.Text = "0";
                        txtPrimaSeguroCesantia.Text = "0";
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
                    Controles.MostrarMensajeError("Error al Seleccionar Tipo de Participación");
                }
            }
        }
        protected void ddlSeguroDesgravamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSeguroDesgravamen();

        }
        protected void ddlSeguroCesantia_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarSeguroCesantia();
        }
        private void SeleccionarSeguroDesgravamen()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                if (ddlSeguroDesgravamen.SelectedValue == "-1")
                {
                    txtTasaSeguroDesgravamen.Text = "0";
                    txtPrimaSeguroDesgravamen.Text = "0";
                }
                else
                {
                    decimal PorcentajeSeguro = decimal.Zero;

                    if (!decimal.TryParse(txtPorcentajeDesgravamen.Text, out PorcentajeSeguro))
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar un % de Seguro de Desgravamen");
                        return;
                    }
                    if (PorcentajeSeguro < 0 || PorcentajeSeguro > 100)
                    {
                        Controles.MostrarMensajeAlerta("Debe Ingresar un valor entre 0 y 100");
                        txtPorcentajeDesgravamen.Text = "0";
                        return;
                    }

                    ////Asignacion de Variables
                    ObjInfo.Id = int.Parse(ddlSeguroDesgravamen.SelectedValue);
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        PrimaSeguro = (ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.MontoCredito * PorcentajeSeguro) / 100;
                        txtPrimaSeguroDesgravamen.Text = string.Format("{0:0.00000000}", PrimaSeguro);
                        txtTasaSeguroDesgravamen.Text = string.Format("{0:0.00000000}", ObjInfo.TasaMensual * 100);
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Seleccionar el Seguro de Desgravamen");
                }
            }
        }
        private void SeleccionarSeguroCesantia()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new SeguroInfo();
                var ObjResultado = new Resultado<SeguroInfo>();
                var objNegocio = new NegSeguros();

                if (ddlSeguroCesantia.SelectedValue == "-1")
                {
                    txtTasaSeguroCesantia.Text = "0";
                    txtPrimaSeguroCesantia.Text = "0";
                }
                else
                {
                    ////Asignacion de Variables
                    ObjInfo.Id = int.Parse(ddlSeguroCesantia.SelectedValue);
                    ObjResultado = objNegocio.Buscar(ObjInfo);
                    if (ObjResultado.ResultadoGeneral)
                    {
                        ObjInfo = ObjResultado.Lista.FirstOrDefault(s => s.Id == ObjInfo.Id);
                        decimal PrimaSeguro = decimal.Zero;
                        if (ObjInfo.PorValorCuota)
                        {
                            if (!NegSolicitudes.RecalcularDividendo()) return;
                            PrimaSeguro = ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.ValorDividendoSinCesantiaUF;
                        }
                        else
                            PrimaSeguro = ObjInfo.TasaMensual * NegSolicitudes.objSolicitudInfo.MontoCredito;
                        txtPrimaSeguroCesantia.Text = String.Format("{0:0.00000000}", PrimaSeguro);
                        txtTasaSeguroCesantia.Text = String.Format("{0:0.00000000}", ObjInfo.TasaMensual * 100);
                    }
                    else
                    {
                        Controles.MostrarMensajeError(ObjResultado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Seleccionar el Seguro de Desgravamen");
                }
            }
        }
        protected void txtPorcentajeDesgravamen_TextChanged(object sender, EventArgs e)
        {

            SeleccionarSeguroDesgravamen();
        }
        protected void btnGuardarDatosGenerales_Click(object sender, EventArgs e)
        {
            GrabarDatosGenerales();
        }
        protected void btnCancelarDatosGenerales_Click(object sender, EventArgs e)
        {
            LimpiarDatosGenerales();
        }
        private void ActualizarSegurosContratados(ParticipanteInfo oParticipante)
        {
            try
            {
                NegSeguros nSeguros = new NegSeguros();
                Resultado<SegurosContratadosInfo> rSeguros = new Resultado<SegurosContratadosInfo>();
                SegurosContratadosInfo oSegurosContratados = new SegurosContratadosInfo();





                //Actualizacion del Seguro de Desgravamen

                if (oParticipante.PorcentajeDesgravamen > 0)
                {
                    oSegurosContratados = new SegurosContratadosInfo();
                    oSegurosContratados.TipoSeguro_Id = 2;//Desgravamen
                    oSegurosContratados.Solicitud_Id = oParticipante.Solicitud_Id;
                    oSegurosContratados.RutCliente = oParticipante.Rut;
                    oSegurosContratados.Seguro_Id = oParticipante.SeguroDesgravamen_Id;
                    if (oParticipante.MontoAseguradoDPS > 0)
                        oSegurosContratados.MontoAsegurado = oParticipante.MontoAseguradoDPS;
                    else
                        oSegurosContratados.MontoAsegurado = NegSolicitudes.objSolicitudInfo.MontoCredito * oParticipante.PorcentajeDesgravamen / 100;
                    oSegurosContratados.TasaMensual = oParticipante.TasaSeguroDesgravamen;
                    oSegurosContratados.PrimaMensual = oParticipante.PrimaSeguroDesgravamen;

                    rSeguros = nSeguros.GuardarSegurosContratados(oSegurosContratados);
                    if (rSeguros.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(rSeguros.Mensaje);
                        return;
                    }
                }

                //Actualizacion del Seguro de Cesantía


                if (oParticipante.SeguroCesantia_Id != -1)
                {
                    var oSeguro = new SeguroInfo();
                    var oResultadoSeguro = new Resultado<SeguroInfo>();
                    oSeguro.Id = oParticipante.SeguroCesantia_Id;
                    oResultadoSeguro = nSeguros.Buscar(oSeguro);
                    if (oResultadoSeguro.ResultadoGeneral)
                    {
                        oSeguro = oResultadoSeguro.Lista.FirstOrDefault(s => s.Id == oSeguro.Id);
                        decimal MontoAsegurado = decimal.Zero;
                        if (oSeguro.PorValorCuota)
                            MontoAsegurado = NegSolicitudes.objSolicitudInfo.ValorDividendoSinCesantiaUF;
                        else
                            MontoAsegurado = NegSolicitudes.objSolicitudInfo.MontoCredito;


                        oSegurosContratados = new SegurosContratadosInfo();
                        oSegurosContratados.TipoSeguro_Id = 3;//Cesantia
                        oSegurosContratados.Solicitud_Id = oParticipante.Solicitud_Id;
                        oSegurosContratados.RutCliente = oParticipante.Rut;
                        oSegurosContratados.Seguro_Id = oParticipante.SeguroCesantia_Id;
                        oSegurosContratados.MontoAsegurado = MontoAsegurado;
                        oSegurosContratados.TasaMensual = oParticipante.TasaSeguroCesantia;
                        oSegurosContratados.PrimaMensual = oParticipante.PrimaSeguroCesantia;

                    }
                    else
                    {
                        Controles.MostrarMensajeError(oResultadoSeguro.Mensaje);
                        return;
                    }

                }
                else
                {
                    oSegurosContratados = new SegurosContratadosInfo();
                    oSegurosContratados.TipoSeguro_Id = 3;//Cesantia
                    oSegurosContratados.Solicitud_Id = oParticipante.Solicitud_Id;
                    oSegurosContratados.RutCliente = oParticipante.Rut;
                    oSegurosContratados.Seguro_Id = oParticipante.SeguroCesantia_Id;
                }
                rSeguros = nSeguros.GuardarSegurosContratados(oSegurosContratados);
                if (rSeguros.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(rSeguros.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Actualizar los Seguros Contratados");
                }
            }
        }
        protected void btnValidarRutCliente_Click(object sender, EventArgs e)
        {
            if (txtRutCliente.Text.Length != 0)
            {
                int Rut = int.Parse(txtRutCliente.Text.Split('-')[0].ToString());
                ClientesInfo oCliente = new ClientesInfo();
                oCliente.Rut = Rut;
                NegClientes.objClienteInfo = new ClientesInfo();
                oCliente = DatosEntidadCliente(oCliente);
                if (oCliente != null)
                {
                    NegClientes.objClienteInfo = oCliente;
                    LlenarFormularioDatosPersonales(oCliente);
                }
                else
                    LimpiarDatosCliente(LimpiaRut: false);


                Controles.EjecutarJavaScript("DesplegarDatosPersonales('true');");
            }
            else
            {
                Controles.MostrarMensajeAlerta("Debe Ingresar un Rut");
            }
        }
        protected void btnEliminarParticipante_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
                int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Id"].ToString());
                NegParticipante nParticipante = new NegParticipante();
                ParticipanteInfo oParticipante = new ParticipanteInfo();
                Resultado<ParticipanteInfo> rParticipante = new Resultado<ParticipanteInfo>();

                oParticipante.Id = Id;


                rParticipante = nParticipante.EliminarParticipante(oParticipante);
                if (rParticipante.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    CargarParticipantes();
                }
                else
                {
                    Controles.MostrarMensajeError(rParticipante.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Eliminar el Participante");
                }
            }
        }

        protected void btnModificarDireccion_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int TipoDireccion_Id = int.Parse(gvDirecciones.DataKeys[row.RowIndex].Values["TipoDireccion_Id"].ToString());
            DireccionClienteInfo oDireccion = new DireccionClienteInfo();
            oDireccion = NegClientes.lstDireccionesCliente.FirstOrDefault(d => d.TipoDireccion_Id == TipoDireccion_Id);
            CargarInfoDireccion(oDireccion);
        }

        protected void btnEliminarDireccion_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int TipoDireccion_Id = int.Parse(gvDirecciones.DataKeys[row.RowIndex].Values["TipoDireccion_Id"].ToString());
            EliminarDireccion(TipoDireccion_Id);
        }

        protected void btnGrabarDireccion_Click(object sender, EventArgs e)
        {
            GrabarDireccion();
        }

        protected void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            LimpiarDatosDireccion();
        }

        protected void btnEliminarDatoLaboral_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
                int Id = int.Parse(gvDatosLaborales.DataKeys[row.RowIndex].Values["Id"].ToString());
                int RutCliente = int.Parse(gvDatosLaborales.DataKeys[row.RowIndex].Values["RutCliente"].ToString());
                NegParticipante nParticipante = new NegParticipante();
                AntecedentesLaboralesInfo oAntecedente = new AntecedentesLaboralesInfo();
                Resultado<AntecedentesLaboralesInfo> rParticipante = new Resultado<AntecedentesLaboralesInfo>();
                oAntecedente.Id = Id;
                rParticipante = nParticipante.EliminarAntecedentesLaborales(oAntecedente);
                if (rParticipante.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    CargarAntecedentesLaborales(RutCliente);
                }
                else
                {
                    Controles.MostrarMensajeError(rParticipante.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Eliminar el Antecedente Laboral");
                }
            }
        }




        #region Clientes Relacionados
        private void CargarTipoRelacion()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo("TIPO_RELACION");
                if (Lista != null)
                {
                    if (ddlRegimenMatrimonial.Enabled)
                        Controles.CargarCombo<TablaInfo>(ref ddlTipoRelacion, Lista.Where(t => t.CodigoInterno == 1).ToList(), Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Tipo de Relación --", "-1");
                    else
                        Controles.CargarCombo<TablaInfo>(ref ddlTipoRelacion, Lista.Where(t => t.CodigoInterno != 1).ToList(), Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Tipo de Relación --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo Tipo de Dirección Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Tipo Persona");
                }
            }
        }

        private void CargarNotariasPersoneria()
        {
            try
            {
                NegNotarias nNotaria = new NegNotarias();
                NotariasInfo oNotaria = new NotariasInfo();
                Resultado<NotariasInfo> rNotaria = new Resultado<NotariasInfo>();


                oNotaria.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");

                rNotaria = nNotaria.Buscar(oNotaria);

                if (rNotaria.ResultadoGeneral)
                {
                    Controles.CargarCombo<NotariasInfo>(ref ddlNotariaPersoneria, rNotaria.Lista, "Id", "Descripcion", "-- Notaría Personería --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeError(rNotaria.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar las Notarias");
                }
            }
        }
        private void CargarClientesRelacionados(int Cliente_Id)
        {
            try
            {
                NegClientes nCliente = new NegClientes();
                ClienteRelacionadoInfo oRelacionado = new ClienteRelacionadoInfo();
                Resultado<ClienteRelacionadoInfo> rRelacionado = new Resultado<ClienteRelacionadoInfo>();

                oRelacionado.Cliente_Id = Cliente_Id;
                rRelacionado = nCliente.BuscarClienteRelacionado(oRelacionado);
                if (rRelacionado.ResultadoGeneral)
                {
                    Controles.CargarGrid<ClienteRelacionadoInfo>(ref gvRelacionados, rRelacionado.Lista, new string[] { "Id", "ClienteRelacionado_Id", "TipoRelacion_Id", "Cliente_Id" });
                }
                else
                {
                    Controles.MostrarMensajeError(rRelacionado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Tipo Persona");
                }
            }
        }
        protected void btnValidarRutRelacionado_Click(object sender, EventArgs e)
        {

            if (ddlTipoRelacion.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Relación");
                return;
            }

            if (txtRutClienteRelacionado.Text.Length != 0)
            {

                ClientesInfo oCliente = new ClientesInfo();
                ClienteRelacionadoInfo oRelcionado = new ClienteRelacionadoInfo();

                oCliente.Rut = int.Parse(txtRutClienteRelacionado.Text.Split('-')[0].ToString());
                oCliente.Dv = txtRutClienteRelacionado.Text.Split('-')[1].ToString();
                oCliente.RutCompleto = txtRutClienteRelacionado.Text;

                NegClientes.objClienteInfo = new ClientesInfo();
                oCliente = DatosEntidadClienteRelacionado(oCliente);
                if (oCliente != null)
                {
                    NegClientes.objClienteInfo = oCliente;
                    LlenarFormularioDatosClienteRelacionado(oCliente, null);
                }
                else
                {
                    LimpiarDatosClienteRelacionado();
                }
            }
            else
            {
                Controles.MostrarMensajeAlerta("Debe Ingresar un Rut");
            }
        }
        private void LimpiarDatosClienteRelacionado(bool LimpiaTipoRelacion = false)
        {
            try
            {
                if (LimpiaTipoRelacion)
                {
                    ddlTipoRelacion.ClearSelection();
                    txtRutClienteRelacionado.Text = "";
                }
                txtNombreRelacionado.Text = "";
                txtPaternoRelacionado.Text = "";
                txtMaternoRelacionado.Text = "";

                ddlEstadoCivilRelacionado.ClearSelection();
                ddlRegimenMatrimonialRelacionado.ClearSelection();
                ddlNacionalidadRelacionado.ClearSelection();
                ddlNacionalidadRelacionado.ClearSelection();
                ddlResidenciaRelacionado.ClearSelection();
                ddlProfesionRelacionado.ClearSelection();
                ddlNotariaPersoneria.ClearSelection();
                txtFechaPersoneria.Text = "";

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Limpiar Datos Personales");
                }
            }
        }
        private void LimpiarClienteRelacionado()
        {
            Controles.LimpiarGrid(ref gvRelacionados);
            Controles.EjecutarJavaScript("DesplegarDatosRelacionados('false');");
            LimpiarDatosClienteRelacionado();
        }
        private void LlenarFormularioDatosClienteRelacionado(ClientesInfo oClienteRelacionado, ClienteRelacionadoInfo oRelacionado)
        {
            try
            {
                if (oRelacionado != null)
                {
                    ddlTipoRelacion.SelectedValue = oRelacionado.TipoRelacion_Id.ToString();
                    ddlNotariaPersoneria.SelectedValue = oRelacionado.NotariaPersoneria_Id.ToString();
                    if (oRelacionado.FechaPersoneria != null)
                        txtFechaPersoneria.Text = oRelacionado.FechaPersoneria.Value.ToShortDateString();
                }

                txtNombreRelacionado.Text = oClienteRelacionado.Nombre;
                txtPaternoRelacionado.Text = oClienteRelacionado.Paterno;
                txtMaternoRelacionado.Text = oClienteRelacionado.Materno;
                txtRutClienteRelacionado.Text = oClienteRelacionado.RutCompleto;
                ddlEstadoCivilRelacionado.SelectedValue = oClienteRelacionado.EstadoCivil_Id.ToString();
                ddlRegimenMatrimonialRelacionado.SelectedValue = oClienteRelacionado.RegimenMatrimonial_Id.ToString();
                ddlNacionalidadRelacionado.SelectedValue = oClienteRelacionado.Nacionalidad_Id.ToString();
                ddlResidenciaRelacionado.SelectedValue = oClienteRelacionado.Residencia_Id.ToString();
                ddlProfesionRelacionado.SelectedValue = oClienteRelacionado.Profesion_Id.ToString();



            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar Datos Personales");
                }
            }

        }
        private ClientesInfo DatosEntidadClienteRelacionado(ClientesInfo Entidad)
        {
            try
            {
                var oResultado = new Resultado<ClientesInfo>();
                var oCliente = new ClientesInfo();
                var oNegCliente = new NegClientes();

                oResultado = oNegCliente.Buscar(Entidad, false);

                if (oResultado.ResultadoGeneral == true)
                {
                    oCliente = oResultado.Lista.FirstOrDefault();

                    if (oCliente != null)
                    {
                        return oCliente;
                    }
                    else
                    {

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
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Accion");

                }
                return null;
            }
        }
        protected void btnModificarRelacionado_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Rut = int.Parse(gvRelacionados.DataKeys[row.RowIndex].Values["ClienteRelacionado_Id"].ToString());
            int Id = int.Parse(gvRelacionados.DataKeys[row.RowIndex].Values["Id"].ToString());
            ClientesInfo oCliente = new ClientesInfo();
            ClienteRelacionadoInfo oRelacionado = new ClienteRelacionadoInfo();

            oRelacionado = NegClientes.lstClientesRelacionados.FirstOrDefault(cr => cr.Id == Id);
            oCliente.Rut = Rut;
            oCliente = DatosEntidadClienteRelacionado(oCliente);
            LlenarFormularioDatosClienteRelacionado(oCliente, oRelacionado);
        }
        protected void btnEliminarRelacionado_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
                int Id = int.Parse(gvRelacionados.DataKeys[row.RowIndex].Values["Id"].ToString());
                int Rut = int.Parse(gvRelacionados.DataKeys[row.RowIndex].Values["Cliente_Id"].ToString());
                NegClientes nCliente = new NegClientes();
                ClienteRelacionadoInfo oRelacionado = new ClienteRelacionadoInfo();
                Resultado<ClienteRelacionadoInfo> rRelacionado = new Resultado<ClienteRelacionadoInfo>();
                oRelacionado.Id = Id;
                rRelacionado = nCliente.EliminarClienteRelacionado(oRelacionado);
                if (rRelacionado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito("Registro Eliminado Correctamente");
                    CargarClientesRelacionados(Rut);
                }
                else
                {
                    Controles.MostrarMensajeError(rRelacionado.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Eliminar el Cliente Relacionado");
                }
            }
        }
        protected void ddlRegimenMatrimonial_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionaRegimenMatrimonial();
        }
        private void SeleccionaRegimenMatrimonial()
        {
            if (ddlRegimenMatrimonial.SelectedValue == "3")
            {
                CargarClientesRelacionados(int.Parse(txtRutCliente.Text.Split('-')[0].ToString()));
                Controles.EjecutarJavaScript("DesplegarDatosRelacionados('true');");
            }
            else
            {
                Controles.EjecutarJavaScript("DesplegarDatosRelacionados('false');");
            }
        }
        private bool ValidarFormularioRelacionado()
        {

            if (txtRutCliente.Text == "")
            {
                Controles.MostrarMensajeAlerta("Debe Ingresar el Rut del Cliente al que se le agregara un Relacionado");
                return false;
            }
            if (int.Parse(txtRutClienteRelacionado.Text.Split('-')[0].ToString()) > 50000000)
            {
                Controles.MostrarMensajeAlerta("El Cliente Relacionado debe ser una persona Naturals");
                return false;
            }
            if (ddlTipoRelacion.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar un Tipo de Relación");
                return false;
            }
            if (txtNombreRelacionado.Text == "")
            {
                Controles.MostrarMensajeAlerta("Debe Ingresar el Nombre del Cliente Relacionado");
                return false;
            }
            if (txtPaterno.Text == "")
            {
                Controles.MostrarMensajeAlerta("Debe Ingresar el Apellido Paterno del Cliente Relacionado");
                return false;
            }
            if (ddlEstadoCivilRelacionado.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar un Estado Civil para el Cliente Relacionado");
                return false;
            }
            if (ddlRegimenMatrimonialRelacionado.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar un Régimen Matrimonial para el Cliente Relacionado");
                return false;
            }
            if (ddlNacionalidadRelacionado.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar una Nacionalidad para el Cliente Relacionado");
                return false;
            }
            if (ddlResidenciaRelacionado.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar una Residencia para el Cliente Relacionado");
                return false;
            }
            if (ddlProfesionRelacionado.SelectedValue == "-1")
            {
                Controles.MostrarMensajeAlerta("Debe Seleccionar una Profesión para el Cliente Relacionado");
                return false;
            }
            if (ddlTipoPersona.SelectedValue == "2")//Persona Jurídica
            {
                if (ddlNotariaPersoneria.SelectedValue == "-1")
                {
                    Controles.MostrarMensajeAlerta("Debe Seleccionar la Notaría de la Personería del Cliente Relacionado");
                    return false;
                }
                if (txtFechaPersoneria.Text == "")
                {
                    Controles.MostrarMensajeAlerta("Debe Ingresar la Fecha de la Personería");
                    return false;
                }
            }

            return true;
        }
        private void GrabarRelacionado()
        {
            try
            {
                NegClientes nCliente = new NegClientes();
                ClienteRelacionadoInfo oRelacionado = new ClienteRelacionadoInfo();
                ClientesInfo oClienteRelacionado = new ClientesInfo();
                Resultado<ClienteRelacionadoInfo> rRelacionado = new Resultado<ClienteRelacionadoInfo>();
                Resultado<ClientesInfo> rCliente = new Resultado<ClientesInfo>();

                if (!GrabarDatosPersonales()) return;
                if (!ValidarFormularioRelacionado()) return;


                oClienteRelacionado.Rut = int.Parse(txtRutClienteRelacionado.Text.Split('-')[0].ToString());



                oClienteRelacionado = DatosEntidadClienteRelacionado(oClienteRelacionado);
                if (oClienteRelacionado == null)
                {
                    oClienteRelacionado = new ClientesInfo();
                }
                oClienteRelacionado.Rut = int.Parse(txtRutClienteRelacionado.Text.Split('-')[0].ToString());
                oClienteRelacionado.Dv = txtRutClienteRelacionado.Text.Split('-')[1].ToString();
                oClienteRelacionado.Nombre = txtNombreRelacionado.Text;
                oClienteRelacionado.Paterno = txtPaternoRelacionado.Text;
                oClienteRelacionado.Materno = txtMaternoRelacionado.Text;
                oClienteRelacionado.TipoPersona_Id = 1;//Persona Natural
                oClienteRelacionado.EstadoCivil_Id = int.Parse(ddlEstadoCivilRelacionado.SelectedValue);
                oClienteRelacionado.RegimenMatrimonial_Id = int.Parse(ddlRegimenMatrimonialRelacionado.SelectedValue);
                oClienteRelacionado.Nacionalidad_Id = int.Parse(ddlNacionalidadRelacionado.SelectedValue);
                oClienteRelacionado.Residencia_Id = int.Parse(ddlResidenciaRelacionado.SelectedValue);
                oClienteRelacionado.Profesion_Id = int.Parse(ddlProfesionRelacionado.SelectedValue);

                rCliente = nCliente.Guardar(ref oClienteRelacionado);
                if (rCliente.ResultadoGeneral)
                {
                    if (oClienteRelacionado != null)
                    {
                        oRelacionado.Cliente_Id = int.Parse(txtRutCliente.Text.Split('-')[0].ToString());
                        oRelacionado.ClienteRelacionado_Id = oClienteRelacionado.Rut;
                        oRelacionado.TipoRelacion_Id = int.Parse(ddlTipoRelacion.SelectedValue);
                        oRelacionado.NotariaPersoneria_Id = int.Parse(ddlNotariaPersoneria.SelectedValue);
                        DateTime Fecha = DateTime.Now;
                        if (DateTime.TryParse(txtFechaPersoneria.Text, out Fecha))
                            oRelacionado.FechaPersoneria = Fecha;
                        else
                            oRelacionado.FechaPersoneria = null;


                        rRelacionado = nCliente.GuardarClienteRelacionado(oRelacionado);
                        if (rRelacionado.ResultadoGeneral)
                        {
                            CargarClientesRelacionados(oRelacionado.Cliente_Id);
                            Controles.MostrarMensajeExito("Cliente Relacionado Grabado Correctamente");
                        }
                        else
                        {
                            Controles.MostrarMensajeError(rRelacionado.Mensaje);
                            return;
                        }
                    }
                    else
                    {
                        Controles.MostrarMensajeError("Error al Grabar el Cliente Relacionado");
                        return;
                    }

                }
                else
                {
                    Controles.MostrarMensajeError(rCliente.Mensaje);
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
                    Controles.MostrarMensajeError("Error al Cargar Datos Personales");
                }
            }
        }

        protected void btnGrabarRelacionado_Click(object sender, EventArgs e)
        {
            GrabarRelacionado();
        }

        protected void btnCancelarRelacionado_Click(object sender, EventArgs e)
        {
            LimpiarDatosClienteRelacionado(true);
        }
        #endregion


    }
}