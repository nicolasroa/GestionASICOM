using Anthem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.General
{
    public static class Controles
    {
        public static void EjecutarJavaScript(string Script)
        {
            try
            {
                Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), Script, true);
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message, Ex);
                }
                else
                {
                    Controles.MostrarMensajeError("Error al Cargar datos de la Solicitud", null);
                }
            }

        }
        public static void AbrirPopup(string Url, int Ancho, int Alto, string Titulo)
        {
            Url = ObtenerUrlAbsoluta(Url);
            string _StrScript = "ModalTamanoVariable('@url','@Ancho', '@Alto', '@Titulo');";
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void AbrirPopup(string Url, string Titulo)
        {
            Url = ObtenerUrlAbsoluta(Url);
            string _StrScript = "ModalURL('@url','@Titulo');";
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }


        public static void AbrirEvento(string Url, string Titulo)
        {
            Url = ObtenerUrlAbsoluta(Url);
            string _StrScript = "OpenModalEvento('@url','@Titulo');";
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void AbrirEvento(string Url, string Evento,string Etapa, string Malla, string Solicitud,string Responsable,string FechaEstimada,string FechaAsignacion)
        {
            Url = ObtenerUrlAbsoluta(Url);
            string _StrScript = "OpenModalEventoConDetalle('@url','@Evento','@Etapa','@Malla','@Solicitud','@Responsable','@FechaEstimada','@FechaAsignacion');";
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Evento", Evento);
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Etapa", Etapa);
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Malla", Malla);
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Solicitud", Solicitud);
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Responsable", Responsable);
            _StrScript = _StrScript.Replace("@url", Url).Replace("@FechaEstimada", FechaEstimada);
            _StrScript = _StrScript.Replace("@url", Url).Replace("@FechaAsignacion", FechaAsignacion);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void AbrirPopupExterno(string Url, int Ancho, int Alto, string Titulo)
        {

            string _StrScript = "ModalURL('@url',@Ancho, @Alto, '@Titulo');";
            _StrScript = _StrScript.Replace("@url", Url).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void AbrirModal(string IdDiv, string Titulo)
        {
            string _StrScript = "ModalDiv('@IdDiv','@Titulo');";
            _StrScript = _StrScript.Replace("@IdDiv", IdDiv).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void CerrarModal(int Evento = 0)
        {
            string script = "CerrarDivModal(@Evento);";
            script = script.Replace("@Evento", Evento.ToString());
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), script, true);
        }

        public static string ObtenerUrlAbsoluta(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }

        public static void CerrarPopup()
        {
            bool Evento = true;
            string script = "CerrarDivModal();";
            script = script.Replace("@Evento", Evento.ToString().ToLower());
            Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), script, true);

        }
        public static void CerrarConCargaMenu()
        {
            bool Evento = true;
            string script = "CerrarVentanaMenu();";
            script = script.Replace("@Evento", Evento.ToString().ToLower());
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), script, true);

        }
        public static void MostrarMensajeError(string Texto, Exception ex = null)
        {
            if (ex != null)
            {
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                // Los datos separados de nuestra excepción 
                // se encuentran en la variable frame.

                //Obtener el nombre de archivo
                string fileName = frame.GetFileName();

                //Obtener el nombre del método
                string nombreMetodo = frame.GetMethod().DeclaringType.Name;

                //Obtener el número de línea
                int linea = frame.GetFileLineNumber();

                //Obtener el número de columna
                int columna = frame.GetFileColumnNumber();

                //Obtener código del error
                int codigo = frame.GetHashCode();

                Texto = Texto + " fileName:  " + fileName + " linea:" + linea;

            }

            string _StrScript = "MostrarMensajeError('@Texto');";
            _StrScript = _StrScript.Replace("@Texto", Texto.Replace("'", ""));

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void MostrarMensajeInfo(string Texto)
        {
            string _StrScript = "MostrarMensajeInfo('@Texto');";
            _StrScript = _StrScript.Replace("@Texto", Texto.Replace("'", ""));

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void MostrarMensajeExito(string Texto)
        {
            string _StrScript = "MostrarMensajeExito('@Texto');";
            _StrScript = _StrScript.Replace("@Texto", Texto.Replace("'", ""));

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void MostrarMensajeAlerta(string Texto)
        {
            string _StrScript = "MostrarMensajeAlerta('@Texto');";
            _StrScript = _StrScript.Replace("@Texto", Texto.Replace("'", ""));
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }
        public static void MensajeEnControl(string IdControl, string Texto)
        {
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MensajeEnControl('" + IdControl + "','" + Texto + "');", true);
        }
        public static void MensajeEnControl(string IdControl)
        {
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MensajeEnControl('" + IdControl + "','Dato Obligatorio');", true);
        }
        public static void CargarTabla(string Id)
        {
            string _StrScript = "CargarTabla('@Id');";
            _StrScript = _StrScript.Replace("@Id", Id.Replace("'", ""));

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _StrScript, true);
        }

        //CONTROLES
        public static void CargarCombo<T>(ref System.Web.UI.WebControls.DropDownList Combo, List<T> Lista, string Valor, string Texto, string TextoInicial, string valorInicial)
        {

            Combo.DataSource = Lista;
            Combo.DataTextField = Texto;
            Combo.DataValueField = Valor;

            Combo.DataBind();
            if (TextoInicial != "")
            {
                if (valorInicial == "")
                    valorInicial = Guid.Empty.ToString();
                Combo.Items.Insert(0, new ListItem(TextoInicial, valorInicial));
                Combo.SelectedIndex = 0;
            }
        }
        public static void CargarCombo<T>(ref Anthem.DropDownList Combo, List<T> Lista, string Valor, string Texto, string TextoInicial, string valorInicial)
        {
            Combo.SelectedValue = null;
            Combo.DataSource = null;
            Combo.DataBind();
            Combo.DataSource = Lista;
            Combo.DataTextField = Texto;
            Combo.DataValueField = Valor;

            Combo.DataBind();
            if (TextoInicial != "")
            {
                if (valorInicial == "")
                    valorInicial = Guid.Empty.ToString();
                Combo.Items.Insert(0, new ListItem(TextoInicial, valorInicial));
                Combo.SelectedIndex = 0;
            }
        }
        public static void CargarListBox<T>(ref System.Web.UI.WebControls.ListBox List, List<T> Lista, string Valor, string Texto)
        {

            List.DataSource = Lista;
            List.DataTextField = Texto;
            List.DataValueField = Valor;
            List.DataBind();

        }
        public static void CargarListBox<T>(ref Anthem.ListBox List, List<T> Lista, string Valor, string Texto)
        {

            List.DataSource = Lista;
            List.DataTextField = Texto;
            List.DataValueField = Valor;
            List.DataBind();
        }
        public static void CargarChekBoxList<T>(ref System.Web.UI.WebControls.CheckBoxList ChekBox, List<T> Lista, string Valor, string Texto)
        {

            ChekBox.DataSource = Lista;
            ChekBox.DataTextField = Texto;
            ChekBox.DataValueField = Valor;
            ChekBox.DataBind();

        }
        public static void CargarChekBoxList<T>(ref Anthem.CheckBoxList ChekBox, List<T> Lista, string Valor, string Texto)
        {

            ChekBox.DataSource = Lista;
            ChekBox.DataTextField = Texto;
            ChekBox.DataValueField = Valor;
            ChekBox.DataBind();

        }
        public static void CargarRadioButtonList<T>(ref System.Web.UI.WebControls.RadioButtonList RadioButtonList, List<T> Lista, string Valor, string Texto)
        {

            RadioButtonList.DataSource = Lista;
            RadioButtonList.DataTextField = Texto;
            RadioButtonList.DataValueField = Valor;
            RadioButtonList.DataBind();

        }
        public static void CargarRadioButtonList<T>(ref Anthem.RadioButtonList RadioButtonList, List<T> Lista, string Valor, string Texto)
        {

            RadioButtonList.DataSource = Lista;
            RadioButtonList.DataTextField = Texto;
            RadioButtonList.DataValueField = Valor;
            RadioButtonList.DataBind();

        }
        public static void CargarGrid<T>(ref System.Web.UI.WebControls.GridView Grid, List<T> Lista, string[] DataKey)
        {

            if (DataKey.Length != 0)
                Grid.DataKeyNames = DataKey;
            if (Lista.Count() != 0)
            {
                Grid.DataSource = Lista;
                Grid.DataBind();
                Grid.UseAccessibleHeader = true;
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                Grid.EmptyDataRowStyle.CssClass = "GridHeader";
                Grid.EmptyDataText = "No hay Registros para mostrar";
                Grid.DataBind();
            }


        }

        public static void CargarGrid<T>(ref Anthem.GridView Grid, List<T> Lista, string[] DataKey, int[] ColumnasAEliminar = null)
        {

            if (DataKey.Length != 0)
                Grid.DataKeyNames = DataKey;
            if (Lista.Count() != 0)
            {
                if (ColumnasAEliminar != null)
                {
                    foreach (var Columna in ColumnasAEliminar)
                    {
                        if (Columna <= Grid.Columns.Count && Columna >= 1)
                            Grid.Columns[Columna - 1].Visible = false;
                    }
                }
                Grid.DataBind();
                Grid.DataSource = Lista;
                Grid.DataBind();
                Grid.UseAccessibleHeader = true;
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                Grid.DataSource = null;
                Grid.EmptyDataRowStyle.CssClass = "GridHeader";
                Grid.EmptyDataText = "No hay Registros para mostrar";
                Grid.DataBind();
            }
            

        }
        public static void LimpiarGrid(ref Anthem.GridView Grid)
        {

  
                Grid.DataSource = null;
                Grid.EmptyDataRowStyle.CssClass = "GridHeader";
                Grid.EmptyDataText = "No hay Registros para mostrar";
                Grid.DataBind();
            


        }
        public static List<T> ModificarControles<T>(this Control container) where T : Control
        {
            List<T> controls = new List<T>();
            foreach (Control c in container.Controls)
            {
                if (c is T)
                    controls.Add((T)c);
                controls.AddRange(ModificarControles<T>(c));
            }
            return controls;
        }


        public static void AgregaFormato(ref Anthem.TextBox txtBox, FormatoControl formato)
        {
            switch (formato)
            {
                case FormatoControl.Fecha:
                    txtBox.Attributes.Add("onblur", "Formato(this);");
                    break;
                case FormatoControl.Rut:
                    txtBox.Attributes.Add("Formato", "10R;");
                    txtBox.Attributes.Add("OnFocus", "(this);");
                    break;
                case FormatoControl.Digitos:
                    txtBox.Attributes.Add("Formato", "+20E");
                    txtBox.Attributes.Add("OnFocus", "ObjIniciarVal(this);");
                    break;
                case FormatoControl.Caracteres:
                    txtBox.Attributes.Add("Formato", "A");
                    txtBox.Attributes.Add("OnFocus", "ObjIniciarVal(this);");
                    break;
                case FormatoControl.Hora:
                    txtBox.Attributes.Add("Formato", "H");
                    txtBox.Attributes.Add("OnFocus", "ObjIniciarVal(this);");
                    break;
                case FormatoControl.RutVal:
                    txtBox.Attributes.Add("Formato", "10R");
                    txtBox.Attributes.Add("OnFocus", "ObjIniciarVal(this);");
                    break;
            }
        }

        public static void AgregaFormato(ref System.Web.UI.WebControls.TextBox txtBox, FormatoControl formato)
        {
            switch (formato)
            {
                case FormatoControl.Fecha:
                    txtBox.Attributes.Add("onblur", "Formato(this);");
                    break;
                case FormatoControl.Rut:
                    txtBox.Attributes.Add("Formato", "10R;");
                    txtBox.Attributes.Add("OnFocus", "(this);");
                    break;
                case FormatoControl.Digitos:
                    txtBox.Attributes.Add("Formato", "+20E");
                    txtBox.Attributes.Add("OnFocus", "ObjIniciarVal(this);");
                    break;
                case FormatoControl.Caracteres:
                    txtBox.Attributes.Add("Formato", "A");
                    txtBox.Attributes.Add("OnFocus", "ObjIniciarVal(this);");
                    break;
                case FormatoControl.Hora:
                    txtBox.Attributes.Add("Formato", "H");
                    txtBox.Attributes.Add("OnFocus", "ObjIniciarVal(this);");
                    break;
                case FormatoControl.RutVal:
                    txtBox.Attributes.Add("Formato", "10R");
                    txtBox.Attributes.Add("OnFocus", "ObjIniciarVal(this);");
                    break;
            }
        }

        public enum FormatoControl
        {
            Fecha,
            Rut,
            Digitos,
            Numeros,
            Caracteres,
            Alfanumerico,
            Hora,
            RutVal
        }


    }
}
