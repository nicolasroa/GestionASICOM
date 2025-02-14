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

namespace WorkFlow.DatosGenerales
{
    public partial class DatosInmobiliariaProyecto : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
               
            }
        }


        public void PermiteActualizacion(bool Actualiza)
        {
            if (Actualiza)
            {
                Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Enabled = true);
                Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.Enabled = true);
                Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Enabled = true);
                Controles.ModificarControles<Anthem.Button>(this).ForEach(c => c.Enabled = true);
                Controles.ModificarControles<Anthem.LinkButton>(this).ForEach(c => c.Visible = true);
            }
            else
            {
                Controles.ModificarControles<Anthem.TextBox>(this).ForEach(c => c.Enabled = false);
                Controles.ModificarControles<Anthem.DropDownList>(this).ForEach(c => c.Enabled = false);
                Controles.ModificarControles<Anthem.CheckBox>(this).ForEach(c => c.Enabled = false);
                Controles.ModificarControles<Anthem.Button>(this).ForEach(c => c.Enabled = false);
                Controles.ModificarControles<Anthem.LinkButton>(this).ForEach(c => c.Visible = false);
            }
        }

        protected void ddlInmobiliaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInmobiliaria.SelectedValue == "-1")
            {
                Controles.CargarCombo<ProyectoInfo>(ref ddlProyecto, null, "Id", "Descripcion", "-- Seleccione una Inmobiliaria", "-1");
                LimpiarFormularioInmobiliaria();
            }
            else
            {
                CargaComboProyecto(int.Parse(ddlInmobiliaria.SelectedValue));
                ObtenerDatosInmobiliaria(int.Parse(ddlInmobiliaria.SelectedValue));
            }
        }

        protected void ddlProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProyecto.SelectedValue == "-1")
            {
                LimpiarFormularioProyecto();
            }
            else
            {

                ObtenerDatosProyecto(int.Parse(ddlProyecto.SelectedValue));
            }
        }


        public void CargaComboTipoInmueble()
        {
            try
            {
                ////Declaracion de VariablesS
                var ObjInfo = new TipoInmuebleInfo();
                var ObjResultado = new Resultado<TipoInmuebleInfo>();
                var objNegocio = new NegPropiedades();

                ////Asignacion de Variables
                ObjInfo.Estado_Id = (int)NegTablas.IdentificadorMaestro("ESTADOS", "A");
                ObjInfo.IndPropiedadPrincipal = true;
                ObjResultado = objNegocio.BuscarTipoInmueble(ObjInfo);
                if (ObjResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<TipoInmuebleInfo>(ref ddlFormTipoInmueble, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Tipo Inmueble--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Tipo Inmueble");
                }
            }
        }
        public void CargaComboInmobiliaria()
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
                    Controles.CargarCombo<InmobiliariaInfo>(ref ddlInmobiliaria, objResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");

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
        public void CargaComboProyecto(int Inmbobiliaria_Id)
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
                    Controles.CargarCombo<ProyectoInfo>(ref ddlProyecto, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--No Aplica--", "-1");
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
        public void ObtenerDatosInmobiliaria(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<InmobiliariaInfo>();
                var oEvento = new InmobiliariaInfo();
                var oNegInmobiliaria = new NegInmobiliarias();

                oEvento.Id = Id;
                ObjetoResultado = oNegInmobiliaria.Buscar(oEvento);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    oEvento = ObjetoResultado.Lista.FirstOrDefault();

                    if (oEvento != null)
                    {
                        LlenarFormularioInmobiliaria(oEvento);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Inmobiliaria");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Inmobiliaria");
                }
            }
        }
        private void LlenarFormularioInmobiliaria(InmobiliariaInfo oInmobiliaria)
        {
            try
            {
                LimpiarFormularioInmobiliaria();
                txtFormDescripcion.Text = oInmobiliaria.Descripcion;
                txtFormRut.Text = oInmobiliaria.RutInmobiliaria;
                txtFormContacto.Text = oInmobiliaria.Contacto;
                txtFormCargoContacto.Text = oInmobiliaria.CargoContacto;
                txtFormMailContacto.Text = oInmobiliaria.MailContacto;
                txtFormFonoFijoContacto.Text = oInmobiliaria.FonoFijoContacto;
                txtFormCelularContacto.Text = oInmobiliaria.CelularContacto;






            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Inmobiliaria");
                }
            }
        }
        private void LimpiarFormularioInmobiliaria()
        {
            txtFormDescripcion.Text = "";
            txtFormRut.Text = "";
            txtFormContacto.Text = "";
            txtFormCargoContacto.Text = "";
            txtFormMailContacto.Text = "";
            txtFormFonoFijoContacto.Text = "";
            txtFormCelularContacto.Text = "";
        }



        public void ObtenerDatosProyecto(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<ProyectoInfo>();
                var oEvento = new ProyectoInfo();
                var oNegProyecto = new NegProyectos();

                oEvento.Id = Id;
                ObjetoResultado = oNegProyecto.Buscar(oEvento);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    oEvento = ObjetoResultado.Lista.FirstOrDefault();

                    if (oEvento != null)
                    {
                        LlenarFormularioProyecto(oEvento);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Proyecto");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Proyecto");
                }
            }
        }
        private void LlenarFormularioProyecto(ProyectoInfo oProyecto)
        {
            try
            {
                LimpiarFormularioProyecto();

                txtFormDescripcionProyecto.Text = oProyecto.Descripcion;
                CargaComboRegion();
                ddlFormRegion.SelectedValue = oProyecto.Region_Id.ToString();
                CargaComboProvincia(int.Parse(ddlFormRegion.SelectedValue));
                ddlFormProvincia.SelectedValue = oProyecto.Provincia_Id.ToString();
                CargaComboComuna(int.Parse(ddlFormProvincia.SelectedValue));
                ddlFormComuna.SelectedValue = oProyecto.Comuna_Id.ToString();
                txtFormDireccion.Text = oProyecto.Direccion;
                ddlFormTipoInmueble.SelectedValue = oProyecto.TipoInmueble_Id.ToString();

                NegComunas nComuna = new NegComunas();
                Resultado<ComunaSiiInfo> rComuna = new Resultado<ComunaSiiInfo>();
                ComunaSiiInfo oComuna = new ComunaSiiInfo();

                oComuna.Descripcion = oProyecto.DescripcionComuna;
                rComuna = nComuna.BuscarComunaSii(oComuna);
                if (rComuna.ResultadoGeneral)
                {
                    if (rComuna.Lista.Count == 1)
                        Controles.CargarCombo<ComunaSiiInfo>(ref ddlFormComunaSII, rComuna.Lista, "Id", "Descripcion", "", "");
                    else
                        Controles.CargarCombo<ComunaSiiInfo>(ref ddlFormComunaSII, rComuna.Lista, "Id", "Descripcion", "-- Seleccione una Comuna --", "-1");
                }
                ddlFormComunaSII.SelectedValue = oProyecto.ComunaSII_Id.ToString();
                txtFormRolMatriz.Text = oProyecto.RolMatriz;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + " Proyecto");
                }
            }
        }
        private void LimpiarFormularioProyecto()
        {
            txtFormDescripcionProyecto.Text = "";

            ddlFormRegion.ClearSelection();
            ddlFormProvincia.ClearSelection();
            ddlFormComuna.ClearSelection();
            ddlFormComunaSII.ClearSelection();
            txtFormRolMatriz.Text = "";
            txtFormDireccion.Text = "";
            ddlFormTipoInmueble.ClearSelection();

        }



        protected void ddlFormRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboProvincia(int.Parse(ddlFormRegion.SelectedValue));
        }

        protected void ddlFormProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaComboComuna(int.Parse(ddlFormProvincia.SelectedValue));
        }
        public void CargaComboRegion()
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new RegionInfo();
                var ObjetoResultado = new Resultado<RegionInfo>();
                var NegRegion = new NegRegion();

                ////Asignacion de Variables
                ObjetoResultado = NegRegion.Buscar(ObjInfo);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<RegionInfo>(ref ddlFormRegion, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Región--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Región");
                }
            }
        }
        private void CargaComboProvincia(int Region_Id)
        {
            try
            {
                ////Declaracion de Variables
                var ObjInfo = new ProvinciaInfo();
                var ObjetoResultado = new Resultado<ProvinciaInfo>();
                var NegProvincia = new NegProvincia();

                ////Asignacion de Variables
                ObjInfo.Region_Id = Region_Id;
                ObjetoResultado = NegProvincia.Buscar(ObjInfo);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<ProvinciaInfo>(ref ddlFormProvincia, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Provincia--", "-1");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + " Provincia");
                }
            }
        }
        private void CargaComboComuna(int Provincia_Id)
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
                    Controles.CargarCombo<ComunaInfo>(ref ddlFormComuna, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "--Comuna--", "-1");





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

        public bool GuardarEntidadInmobiliaria()
        {
            try
            {
                //Declaración de Variables
                var oInmobiliaria = new InmobiliariaInfo();
                var ObjetoResultado = new Resultado<InmobiliariaInfo>();
                var oNegInmobiliaria = new NegInmobiliarias();

                if (!ValidarFormularioInmobiliaria()) { return false; }

                //Asignacion de Variales

                oInmobiliaria.Id = int.Parse(ddlInmobiliaria.SelectedValue);
                oInmobiliaria = DatosEntidadInmobiliaria(oInmobiliaria);

                oInmobiliaria.Descripcion = txtFormDescripcion.Text;
                oInmobiliaria.RutInmobiliaria = txtFormRut.Text;
                oInmobiliaria.Contacto = txtFormContacto.Text;
                oInmobiliaria.CargoContacto = txtFormCargoContacto.Text;
                oInmobiliaria.MailContacto = txtFormMailContacto.Text;
                oInmobiliaria.FonoFijoContacto = txtFormFonoFijoContacto.Text;
                oInmobiliaria.CelularContacto = txtFormCelularContacto.Text;



                //Ejecucion del procedo para Guardar
                ObjetoResultado = oNegInmobiliaria.Guardar(oInmobiliaria);

                if (!ObjetoResultado.ResultadoGeneral)
                {

                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Eventos");
                }
                return false;
            }
        }
        private bool ValidarFormularioInmobiliaria()
        {
            if (txtFormDescripcion.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormDescripcion.ClientID);
                return false;
            }



            if (txtFormRut.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormRut.ClientID);
                return false;
            }
            if (txtFormContacto.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormContacto.ClientID);
                return false;
            }
            if (txtFormCelularContacto.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormCelularContacto.ClientID);
                return false;
            }
            if (txtFormFonoFijoContacto.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormFonoFijoContacto.ClientID);
                return false;
            }
            if (txtFormMailContacto.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormMailContacto.ClientID);
                return false;
            }

            return true;

        }
        private InmobiliariaInfo DatosEntidadInmobiliaria(InmobiliariaInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<InmobiliariaInfo>();
                var oEvento = new InmobiliariaInfo();
                var oNegInmobiliaria = new NegInmobiliarias();

                ObjetoResultado = oNegInmobiliaria.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    oEvento = ObjetoResultado.Lista.FirstOrDefault();

                    if (oEvento != null)
                    {
                        return oEvento;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Inmobiliaria");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Inmobiliaria");

                }
                return null;
            }
        }

        protected void ddlFormComuna_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormComuna.SelectedValue == "-1")
                CargarComboComunaSII();
            else
                CargarComboComunaSII(ddlFormComuna.SelectedItem.Text);
        }

        private void CargarComboComunaSII(string DescripcionComuna = "")
        {
            try
            {
                NegComunas nComuna = new NegComunas();
                Resultado<ComunaSiiInfo> rComuna = new Resultado<ComunaSiiInfo>();
                ComunaSiiInfo oComuna = new ComunaSiiInfo();

                oComuna.Descripcion = DescripcionComuna;
                rComuna = nComuna.BuscarComunaSii(oComuna);
                if (rComuna.ResultadoGeneral)
                {
                    if (rComuna.Lista.Count == 1)
                        Controles.CargarCombo<ComunaSiiInfo>(ref ddlFormComunaSII, rComuna.Lista, "Id", "Descripcion", "", "");
                    else
                        Controles.CargarCombo<ComunaSiiInfo>(ref ddlFormComunaSII, rComuna.Lista, "Id", "Descripcion", "-- Seleccione una Comuna --", "-1");
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
                    Controles.MostrarMensajeError("Error al Cargar Comuna SII");
                }
            }
        }







        private bool ValidarFormularioProyecto()
        {

            if (ddlInmobiliaria.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlInmobiliaria.ClientID);
                return false;
            }

            if (txtFormDescripcion.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormDescripcion.ClientID);
                return false;
            }

            return true;

        }
        public bool GuardarEntidadProyecto()
        {
            try
            {
                //Declaración de Variables
                var oProyecto = new ProyectoInfo();
                var ObjetoResultado = new Resultado<ProyectoInfo>();
                var oNegProyecto = new NegProyectos();

                if (!ValidarFormularioProyecto()) { return false; }
                //Asignacion de Variales
                oProyecto.Id = int.Parse(ddlProyecto.SelectedValue);
                oProyecto = DatosEntidadProyecto(oProyecto);
                oProyecto.Descripcion = txtFormDescripcion.Text;
                oProyecto.Inmobiliaria_Id = int.Parse(ddlInmobiliaria.SelectedValue);
                oProyecto.Comuna_Id = int.Parse(ddlFormComuna.SelectedValue);
                oProyecto.Direccion = txtFormDireccion.Text;
                oProyecto.RolMatriz = txtFormRolMatriz.Text;
                oProyecto.ComunaSII_Id = int.Parse(ddlFormComunaSII.SelectedValue);
                oProyecto.TipoInmueble_Id = int.Parse(ddlFormTipoInmueble.SelectedValue);

                //Ejecucion del procedo para Guardar
                ObjetoResultado = oNegProyecto.Guardar(oProyecto);

                if (!ObjetoResultado.ResultadoGeneral)
                {

                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + "Eventos");
                }
                return false;
            }
        }


        private ProyectoInfo DatosEntidadProyecto(ProyectoInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<ProyectoInfo>();
                var oEvento = new ProyectoInfo();
                var oNegProyecto = new NegProyectos();

                ObjetoResultado = oNegProyecto.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    oEvento = ObjetoResultado.Lista.FirstOrDefault();

                    if (oEvento != null)
                    {
                        return oEvento;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Proyecto");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + " Proyecto");

                }
                return null;
            }
        }






    }
}