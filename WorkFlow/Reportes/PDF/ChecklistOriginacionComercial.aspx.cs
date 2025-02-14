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
    public partial class ChecklistOriginacionComercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNombreDeudor.Text = NegSolicitudes.objSolicitudInfo.NombreCliente;
            lblNombreEjecutivo.Text = NegSolicitudes.objSolicitudInfo.DescripcionEjecutivoComercial + " (" + NegSolicitudes.objSolicitudInfo.MailEjecutivoComercial + ")";
           
            CargarDocumentos();
        }

        private void CargarDocumentos()
        {
            try
            {
                NegDocumentacion nDocumentos = new NegDocumentacion();
                RegistroDocumentosOriginacionComercialInfo oDocumentos = new RegistroDocumentosOriginacionComercialInfo();
                Resultado<RegistroDocumentosOriginacionComercialInfo> rDocumentos = new Resultado<RegistroDocumentosOriginacionComercialInfo>();

                oDocumentos.Solicitud_Id = NegSolicitudes.objSolicitudInfo.Id;
               

                //lblParticipanteAntecedentes.Text = oParticipante.NombreCliente;
                rDocumentos = nDocumentos.BuscarRegistroDocumentosOriginacionComercial(oDocumentos);

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
    }
}