using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Web.UI;
using System.Collections.Generic;

namespace WorkFlow.Mantenedores
{
    public partial class Clientes : System.Web.UI.Page
    {

        #region PROPIEDADES


        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Controles.EjecutarJavaScript("InicioCliente();");
            if (!Page.IsPostBack)
            {
                CargarTipoPersona();
                CargaFormulario();

            }
        }

        protected void btnGrabarCliente_Click(object sender, EventArgs e)
        {
            GrabarCliente();
        }
        protected void ddlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionaTipoPersona();
        }
        protected void btnCerrarCliente_Click(object sender, EventArgs e)
        {
            Controles.CerrarModal();
        }
        protected void btnValidarRutCliente_Click(object sender, EventArgs e)
        {
            if (txtRutCliente.Text.Length != 0)
            {
                int Rut = int.Parse(txtRutCliente.Text.Split('-')[0].ToString());
                ClientesInfo oCliente = new ClientesInfo();
                oCliente.Rut = Rut;
                NegClientes.objClienteInfo = new ClientesInfo();
                SeleccionaTipoPersona();



            }
            else
            {
                Controles.MostrarMensajeAlerta("Debe Ingresar un Rut");
            }
        }

        #endregion

        #region METODOS
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
                    lblFechaNacimiento.Text = "Inicio de Actividades";
                }
                else
                {
                    lblNombreRazonSocial.Text = "Nombre";
                    lblPaternoGiro.Text = "Apellido Paterno";
                    txtMaterno.Text = "";
                    txtMaterno.Enabled = true;
                    lblFechaNacimiento.Text = "Fecha de Nacimiento";
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
                    Controles.MostrarMensajeError("Error al Seleccionar Tipo de Persona");
                }
            }
        }
        private void GrabarCliente()
        {
            try
            {
                var oCliente = new ClientesInfo();
                if (!ValidarFormulario()) { return; }

                oCliente.Rut = NegClientes.objClienteInfo.Rut;
                oCliente.Dv = NegClientes.objClienteInfo.Dv;
                oCliente.Nombre = txtNombre.Text;
                oCliente.Paterno = txtPaterno.Text;
                oCliente.Materno = txtMaterno.Text;
                oCliente.Mail = txtEmail.Text;
                oCliente.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                oCliente.TelefonoMovil = txtCelular.Text;
                oCliente.TelefonoFijo = txtFono.Text;
                oCliente.NombreCompleto = txtNombre.Text + " " + txtPaterno.Text + " " + txtMaterno.Text;
                if (oCliente.Rut >= 50000000)//Persona Juridica
                    oCliente.TipoPersona_Id = (int)NegTablas.IdentificadorMaestro("TIPO_PERSONA", "J");
                else
                    oCliente.TipoPersona_Id = (int)NegTablas.IdentificadorMaestro("TIPO_PERSONA", "N");

                var ObjResultado = new Resultado<ClientesInfo>();
                var objNegocio = new NegClientes();

                ////Asignacion de Variables
                ObjResultado = objNegocio.Guardar(ref oCliente);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                    NegClientes.objClienteInfo = new ClientesInfo();
                    NegClientes.objClienteInfo = oCliente;
                    NegClientes.IndClienteModal = true;
                    Controles.CerrarModal(1);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Cliente");
                }
            }
        }

        private void CargaFormulario()
        {
            int Rut = int.Parse(NegClientes.objClienteInfo.RutCompleto.Split('-')[0].ToString());
            ////MODIFICACION
            if (NegClientes.IndNuevoCliente == false)
            {

                txtNombre.Text = NegClientes.objClienteInfo.Nombre;
                txtPaterno.Text = NegClientes.objClienteInfo.Paterno;
                txtMaterno.Text = NegClientes.objClienteInfo.Materno;
                txtEmail.Text = NegClientes.objClienteInfo.Mail;
                txtFechaNacimiento.Text = ((DateTime)NegClientes.objClienteInfo.FechaNacimiento).ToShortDateString();
                txtCelular.Text = NegClientes.objClienteInfo.TelefonoMovil;
                txtFono.Text = NegClientes.objClienteInfo.TelefonoFijo;
                txtRutCliente.Text = NegClientes.objClienteInfo.RutCompleto;
                txtNombre.Enabled = false;
                txtPaterno.Enabled = false;
                txtMaterno.Enabled = false;
                txtFechaNacimiento.Enabled = false;
                txtRutCliente.Enabled = false;
              
                if (Rut >= 50000000)//Persona Juridica
                    ddlTipoPersona.SelectedValue = ((int)NegTablas.IdentificadorMaestro("TIPO_PERSONA", "J")).ToString();
                else
                    ddlTipoPersona.SelectedValue = ((int)NegTablas.IdentificadorMaestro("TIPO_PERSONA", "N")).ToString();

                ddlTipoPersona.SelectedValue = NegClientes.objClienteInfo.TipoPersona_Id.ToString();
                SeleccionaTipoPersona();
            }
            else
            {
                txtRutCliente.Text = NegClientes.objClienteInfo.RutCompleto;
                txtRutCliente.Enabled = false;
               
                if (Rut >= 50000000)//Persona Juridica
                    ddlTipoPersona.SelectedValue = ((int)NegTablas.IdentificadorMaestro("TIPO_PERSONA", "J")).ToString();
                else
                    ddlTipoPersona.SelectedValue = ((int)NegTablas.IdentificadorMaestro("TIPO_PERSONA", "N")).ToString();

                ddlTipoPersona.SelectedValue = NegClientes.objClienteInfo.TipoPersona_Id.ToString();
                SeleccionaTipoPersona();
            }


        }

        private bool ValidarFormulario()
        {
            if (ddlTipoPersona.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlTipoPersona.ClientID);
                return false;
            }
            if (txtNombre.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtNombre.ClientID);
                return false;
            }
            if (txtPaterno.Text.Length == 0 && txtMaterno.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtPaterno.ClientID);
                return false;
            }
            if (txtFechaNacimiento.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFechaNacimiento.ClientID);
                return false;
            }
            if (txtEmail.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtEmail.ClientID);
                return false;
            }
            if (txtCelular.Text.Length == 0 && txtFono.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtCelular.ClientID);
                return false;
            }
            return true;
        }
        #endregion


    }
}