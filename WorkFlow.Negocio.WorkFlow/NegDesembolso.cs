using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegDesembolso
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        public Resultado<DesembolsoInfo> CargarBandejaDesembolso(DesembolsoFiltro Filtro)
        {

            var ObjetoResultado = new Resultado<DesembolsoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DesembolsoInfo, DesembolsoFiltro>(Filtro, GlobalDA.SP.BandejaDesembolso_QRY, BaseSQL);
                if (ObjetoResultado.ResultadoGeneral)
                    ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ex.Message + " Bandeja de Desembolso";
                return ObjetoResultado;
            }
        }


        public Resultado<DesembolsoInfo> IniciarFlujo(DesembolsoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DesembolsoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<DesembolsoInfo>(Entidad, GlobalDA.SP.IniciarFlujoDesembolso_PRC, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ex.Message + " Flujo de Desembolso";
                return ObjetoResultado;
            }
        }



        #region PROPIEDADES

        public static List<BandejaEntradaInfo> lstBandejaDesembolso
        {
            get { return (List<BandejaEntradaInfo>)HttpContext.Current.Session[ISesiones.lstBandejaDesembolso]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstBandejaDesembolso, value); }
        }
        public static BandejaEntradaInfo oBandejaDesembolso
        {
            get { return (BandejaEntradaInfo)HttpContext.Current.Session[ISesiones.oBandejaDesembolso]; }
            set { HttpContext.Current.Session.Add(ISesiones.oBandejaDesembolso, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstBandejaDesembolso = "lstBandejaDesembolso";
            public static string oBandejaDesembolso = "oBandejaDesembolso";
        }
        #endregion

    }
}
