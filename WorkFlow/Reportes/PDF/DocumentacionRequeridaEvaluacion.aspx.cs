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
    public partial class DocumentacionRequeridaEvaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ParticipanteInfo oInfo = new ParticipanteInfo();
            oInfo = NegParticipante.ObjParticipantePDF;

            if (oInfo.flagPDF != 1)
            {
                divContenido.Style.Add("page-break-after", "always");
            }

            lblNombreDeudor.Text = oInfo.NombreCliente;
            lblTipoDeudor.Text = oInfo.DescripcionTipoParticipante;
            lblNombreEjecutivo.Text = NegSolicitudes.objSolicitudInfo.DescripcionEjecutivoComercial + " (" + NegSolicitudes.objSolicitudInfo.MailEjecutivoComercial + ")";
          
            CargarDocumentos(oInfo);

        }

        #region METODOS

        private void CargarDocumentos(ParticipanteInfo oParticipante)
        {
            try
            {
                NegDocumentacion nDocumentos = new NegDocumentacion();
                RegistroDocumentosPersonalesInfo oDocumentos = new RegistroDocumentosPersonalesInfo();
                Resultado<RegistroDocumentosPersonalesInfo> rDocumentos = new Resultado<RegistroDocumentosPersonalesInfo>();

                oDocumentos.Solicitud_Id = oParticipante.Solicitud_Id;
                oDocumentos.RutCliente = oParticipante.Rut;

                //lblParticipanteAntecedentes.Text = oParticipante.NombreCliente;
                rDocumentos = nDocumentos.BuscarRegistroDocumentosPersonalesSolicitados(oDocumentos);

                if (rDocumentos.ResultadoGeneral)
                {
                    Controles.CargarGrid(ref gvDocumentosRequeridos, rDocumentos.Lista, new string[] { "Documento_Id"});

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