using WorkFlow.Entidades;
using WorkFlow.General;
using WorkFlow.Negocio.Administracion;
using WorkFlow.Negocio.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlow.Reportes.PDF
{
    public partial class DocumentacionEstudioTitulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            ParticipanteInfo oInfo = new ParticipanteInfo();
            oInfo = NegParticipante.ObjParticipantePDF;

            if (NegPropiedades.objEETT.flagPDF != 1)
            {
                divContenido.Style.Add("page-break-after", "always");
            }
            lblNombreDeudor.Text = NegSolicitudes.objSolicitudInfo.NombreCliente;
            lblSolicitud.Text = NegSolicitudes.objSolicitudInfo.Id.ToString();
            lblNombreEjecutivo.Text = NegSolicitudes.objSolicitudInfo.DescripcionEjecutivoComercial;
            CargarDocumentos();

        }

        #region METODOS

        private void CargarDocumentos()
        {
            try
            {
                NegDocumentosEstudioTitulo nDocumentos = new NegDocumentosEstudioTitulo();
                RegistroDocumentosEstudioTituloInfo oDocumentos = new RegistroDocumentosEstudioTituloInfo();
                Resultado<RegistroDocumentosEstudioTituloInfo> rDocumentos = new Resultado<RegistroDocumentosEstudioTituloInfo>();

                oDocumentos.Solicitud_Id = NegPropiedades.objEETT.Solicitud_Id;
                oDocumentos.Tasacion_Id = NegPropiedades.objEETT.Id;
              
                //lblParticipanteAntecedentes.Text = oParticipante.NombreCliente;
                rDocumentos = nDocumentos.BuscarRegistroDocumentosEstudioTitulo(oDocumentos);

                if (rDocumentos.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvDocumentosRequeridos, rDocumentos.Lista, new string[] { "Documento_Id" });

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
                    Controles.MostrarMensajeError("Error al Cargar los Documentos A Validar");
                }
            }
        }

        #endregion
    }
}