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

namespace WebSite.Administracion
{
    public partial class UsuarioB : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarRoles();
                CargarSucursales();
                CargarFabricas();
                CargarInversionista();
                CargaComboInmobiliaria();
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var Permisos = (RolMenu)NegMenus.Permisos;

            if (Permisos.PermisoCrear == false && hfId.Value == "")
            {
                MostrarMensajeValidacion(Constantes.MensajesUsuario.SinPermisoCrear.ToString());
                return;
            }
            if (Permisos.PermisoModificar == false && hfId.Value != "")
            {
                MostrarMensajeValidacion(Constantes.MensajesUsuario.SinPermisoModificar.ToString());
                return;
            }
            GuardarEntidad();
        }
        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            CargarGrid();
        }
        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values["Rut"].ToString());
            ObtenerDatos(Id);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }
        #endregion
        #region Metodos

        //Carga Inicial
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
                    Controles.CargarCombo<FabricaInfo>(ref ddlFabrica, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                    Controles.CargarCombo<FabricaInfo>(ref ddlFormFabrica, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Fábrica --", "-1");
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
        private void CargarInversionista()
        {
            try
            {
                var oInfo = new InversionistaInfo();
                var oNegocio = new NegInversionistas();
                var oResultado = new Resultado<InversionistaInfo>();
                oInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oResultado = oNegocio.Buscar(oInfo);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<InversionistaInfo>(ref ddlFormInversionista, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Inversionista --", "-1");
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
                    Controles.CargarCombo<InmobiliariaInfo>(ref ddlFormInmobiliaria, objResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");
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
        private void CargarEstados()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigBase.TablaEstado);
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlEstado, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Todos --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlFormEstado, Lista, Constantes.StringCodigoInterno, Constantes.StringNombre, "-- Estado --", "-1");

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado");
                }
            }
        }
        private void MostrarMensajeValidacion(string Validacion)
        {
            Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Validacion));
        }
        private void CargarRoles()
        {
            try
            {
                var oNegRoles = new NegRoles();
                var oRoles = new RolesInfo();
                var oResultado = new Resultado<RolesInfo>();

                oRoles.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                oRoles.Sistema_Id = (int)NegTablas.IdentificadorMaestro("SISTEMAS", "WF");

                oResultado = oNegRoles.Buscar(oRoles);
                if (oResultado.ResultadoGeneral)
                {
                    Controles.CargarListBox<RolesInfo>(ref lstbFormRoles, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "Tablas Estado");
                }
            }
        }
        private void CargarSucursales()
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
                    Controles.CargarCombo<SucursalInfo>(ref ddlSucursal, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Todas--", "-1");
                    Controles.CargarCombo<SucursalInfo>(ref ddlFormSucursal, oResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Sin Sucursal--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + " Sucursales");
                }
            }
        }


        //Metodos Generales
        private bool ValidarFormulario()
        {
            int Seleccionados = 0;
            if (txtFormNombre.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormNombre.ClientID);
                return false;
            }

            if (txtFormMail.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormMail.ClientID);
                return false;
            }
            if (txtFormRut.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormRut.ClientID);
                return false;
            }
            if (ddlFormEstado.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEstado.ClientID);
                return false;
            }

            for (int index = 0; index <= lstbFormRoles.Items.Count - 1; index++)
            {
                if (lstbFormRoles.Items[index].Selected == true)
                    Seleccionados++;
            }

            if (Seleccionados == 0)
            {
                Controles.MensajeEnControl(lstbFormRoles.ClientID);
                return false;
            }


            return true;

        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    ObjetoUsuario.Rut = int.Parse(hfId.Value.ToString().Split('-')[0]);
                    ObjetoUsuario = DatosEntidad(ObjetoUsuario);
                }
                else
                {
                    ObjetoUsuario.IntentosFallidos = 0;
                    ObjetoUsuario.FechaUltimoCambioClave = DateTime.Now;
                    ObjetoUsuario.Contraseña = NegUsuarios.GenerarClave();
                    ObjetoUsuario.PrimerInicio = false;
                }
               
                ObjetoUsuario.NombreUsuario = txtFormNombreUsuario.Text;
                ObjetoUsuario.Nombre = txtFormNombre.Text;
                ObjetoUsuario.Telefono = txtFormTelefono.Text;
                ObjetoUsuario.Email = txtFormMail.Text;
                ObjetoUsuario.Rut = int.Parse(txtFormRut.Text.Split('-')[0].ToString());
                ObjetoUsuario.Dv = txtFormRut.Text.Split('-')[1].ToString();
                ObjetoUsuario.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);

                if (ObjetoUsuario.Estado_Id == NegTablas.IdentificadorMaestro("ESTADOS", "A"))
                    ObjetoUsuario.IntentosFallidos = 0;

                ObjetoUsuario.Sucursal_Id = int.Parse(ddlFormSucursal.SelectedValue);
                ObjetoUsuario.Fabrica_Id = int.Parse(ddlFormFabrica.SelectedValue);
                ObjetoUsuario.Inmobiliaria_Id = int.Parse(ddlFormInmobiliaria.SelectedValue);
                ObjetoUsuario.Inversionista_Id = int.Parse(ddlFormInversionista.SelectedValue);
                if (txtMontoMaximoAprobacion.Text == "")
                    ObjetoUsuario.MontoMaximoAprobacion = null;
                else
                    ObjetoUsuario.MontoMaximoAprobacion = decimal.Parse(txtMontoMaximoAprobacion.Text);


                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegUsuario.Guardar(ref ObjetoUsuario);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (!AsignarRoles(ObjetoUsuario)) return;
                    if (hfId.Value.Length == 0)
                    {
                        if (!EnviarMail(ObjetoUsuario)) { return; }
                        
                        //Mail.EnviarMensajeNuevoUsuario(ObjetoUsuario, NegConfiguracionGeneral.Obtener());
                    }

                    LimpiarFormulario();
                    txtRut.Text = ObjetoUsuario.Rut.ToString() + "-" + ObjetoUsuario.Dv;
                    CargarGrid();
                    txtRut.Text = "";
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                    Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Usuario");
                }
            }
        }

        private bool EnviarMail(UsuarioInfo oUsuario)
        {
            try
            {
                Mail.MailInfo oMail = new Mail.MailInfo();
                Resultado<Mail.MailInfo> oResultado = new Resultado<Mail.MailInfo>();
                NegUsuarios.NuevoUsuario = new UsuarioInfo();
                NegUsuarios.NuevoUsuario = oUsuario;

                oMail.Asunto = "Nuevo Usuario WorkFLow Hipotecario";
                oMail.Destinatarios = oUsuario.Email;

                string urlPlantillaMail = "~/Reportes/PlantillasMail/NuevoUsuario.aspx";
                string ContenidMail = "";
                StringWriter htmlStringWriter = new StringWriter();
                Server.Execute(urlPlantillaMail, htmlStringWriter);
                ContenidMail = htmlStringWriter.GetStringBuilder().ToString();
                htmlStringWriter.Close();
                oMail.Texto = ContenidMail;
                oResultado = Mail.EnviarMail(oMail, NegConfiguracionGeneral.Obtener());
                if (!oResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeError(oResultado.Mensaje);
                    return false;
                }
                else
                {
                    return true;
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
                    Controles.MostrarMensajeError("Error al Enviar Mail");
                }
                return false;
            }
        }


        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoUsuario = new UsuarioInfo();
                var NegUsuarios = new NegUsuarios();
                var ObjetoResultado = new Resultado<UsuarioInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoUsuario.Nombre = txtNombre.Text;
                ObjetoUsuario.Sucursal_Id = int.Parse(ddlSucursal.SelectedValue);
                ObjetoUsuario.Rut = txtRut.Text.Length > 0 ? int.Parse(txtRut.Text.Split('-')[0].ToString()) : -1;
                ObjetoUsuario.Estado_Id = int.Parse(ddlEstado.SelectedValue);
                ObjetoUsuario.Fabrica_Id = int.Parse(ddlFabrica.SelectedValue);
                

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegUsuarios.Buscar(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<UsuarioInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { "Rut", Constantes.StringNombre });
                    lblContador.Text = ObjetoResultado.ValorDecimal.ToString() + " Registro(s) Encontrado(s)";
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
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var ObjetoUsuario = new UsuarioInfo();
                var NegUsuario = new NegUsuarios();

                ObjetoUsuario.Rut = Id;
                ObjetoResultado = NegUsuario.Buscar(ObjetoUsuario);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoUsuario = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoUsuario != null)
                    {
                        LlenarFormulario(ObjetoUsuario);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Usuario");
                        }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Usuario");
                }
            }
        }
        private UsuarioInfo DatosEntidad(UsuarioInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var ObjetoUsuario = new UsuarioInfo();
                var NegUsuario = new NegUsuarios();

                ObjetoResultado = NegUsuario.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoUsuario = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoUsuario != null)
                    {
                        return ObjetoUsuario;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Usuario");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Usuario");

                }
                return null;
            }
        }
        private void LlenarFormulario(UsuarioInfo ObjetoUsuario)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = ObjetoUsuario.Rut.ToString();
                txtFormNombreUsuario.Text = ObjetoUsuario.NombreUsuario;
                txtFormNombre.Text = ObjetoUsuario.Nombre;
                txtFormMail.Text = ObjetoUsuario.Email;
                txtFormTelefono.Text = ObjetoUsuario.Telefono;
                txtFormRut.Text = ObjetoUsuario.Rut.ToString() + "-" + ObjetoUsuario.Dv;
                txtFormRut.Enabled = false;
                ddlFormEstado.SelectedValue = ObjetoUsuario.Estado_Id.ToString();
                ddlFormSucursal.SelectedValue = ObjetoUsuario.Sucursal_Id.ToString();
                ddlFormFabrica.SelectedValue = ObjetoUsuario.Fabrica_Id.ToString();
                ddlFormInversionista.SelectedValue = ObjetoUsuario.Inversionista_Id.ToString();
                ddlFormInmobiliaria.SelectedValue = ObjetoUsuario.Inmobiliaria_Id.ToString();
                txtClave.Text = Seguridad.Desencriptar(ObjetoUsuario.Contraseña);
                txtMontoMaximoAprobacion.Text = ObjetoUsuario.MontoMaximoAprobacion.ToString();
                CargarRolesAsignados(ObjetoUsuario.Rut);


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Usuarios");
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormNombreUsuario.Text = "";
            txtFormNombre.Text = "";
            txtFormMail.Text = "";
            txtFormTelefono.Text = "";
            txtFormRut.Text = "";
            txtFormRut.Enabled = true;
            ddlFormEstado.ClearSelection();
            ddlFormSucursal.ClearSelection();
            ddlFormFabrica.ClearSelection();
            ddlFormInversionista.ClearSelection();
            ddlFormInmobiliaria.ClearSelection();
            lstbFormRoles.ClearSelection();
            txtMontoMaximoAprobacion.Text = "";
            txtClave.Text = "";
            hfId.Value = "";
        }
        private bool AsignarRoles(UsuarioInfo Entidad)
        {
            //Declaracion de Variables
            var ObjetoUsuarioRol = new UsuarioRolInfo();
            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioRolInfo>();
            //Variables de Busqueda de Usuario para la Asignación
            var ObjetoResultadoUsuario = new Resultado<UsuarioInfo>();


            ObjetoUsuarioRol.Usuario_Id = Entidad.Rut;

            foreach (ListItem item in lstbFormRoles.Items)
            {
                if (item.Selected == true)
                {
                    ObjetoUsuarioRol.Rol_Id = int.Parse(item.Value.ToString());

                    ObjetoResultado = NegUsuario.AsignarUsuarioRol(ObjetoUsuarioRol);
                    if (ObjetoResultado.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        return false;
                    }
                }
                else
                {
                    ObjetoUsuarioRol.Rol_Id = int.Parse(item.Value.ToString());

                    ObjetoResultado = NegUsuario.DesasignarUsuarioRol(ObjetoUsuarioRol);
                    if (ObjetoResultado.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        return false;
                    }
                }

            }


            return true;


        }
        private void CargarRolesAsignados(int? Usuario_Id)
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
                lstbFormRoles.ClearSelection();
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (ObjetoResultado.Lista != null)
                    {
                        for (int x = 0; x <= ObjetoResultado.Lista.Count - 1; x++)
                        {
                            for (int y = 0; y <= lstbFormRoles.Items.Count - 1; y++)
                            {
                                if (int.Parse(lstbFormRoles.Items[y].Value) == ObjetoResultado.Lista[x].Rol_Id)
                                    lstbFormRoles.Items[y].Selected = true;
                            }
                        }
                    }
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
        #endregion
    }
}