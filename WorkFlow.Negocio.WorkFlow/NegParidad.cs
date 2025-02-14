using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Linq;

namespace WorkFlow.Negocio.WorkFlow
{
    public static class NegParidad
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        public static decimal ObtenerParidad(int CodigoMoneda_Id, DateTime Fecha)
        {
            var ObjetoResultado = new Resultado<ParidadInfo>();
            try
            {
                var Entidad = new ParidadInfo();
                Entidad.CodigoMoneda_Id = CodigoMoneda_Id;
                Entidad.Fecha = Fecha;
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ParidadInfo, ParidadInfo>(Entidad, GlobalDA.SP.Paridad_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.FirstOrDefault().Valor;
                return (decimal)ObjetoResultado.ValorDecimal;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Paridad";
                return 0;
            }
        }

        public static decimal ObtenerTMC(int CodigoMoneda_Id, DateTime Fecha, int Plazo,decimal MontoCredito)
        {
            var ObjetoResultado = new Resultado<ParidadInfo>();
            try
            {
                var Entidad = new ParidadInfo();
                Entidad.CodigoMoneda_Id = CodigoMoneda_Id;
                Entidad.Fecha = Fecha;
                Entidad.Plazo = Plazo;
                Entidad.MontoCredito = MontoCredito;
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ParidadInfo, ParidadInfo>(Entidad, GlobalDA.SP.TMC_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.FirstOrDefault().Valor;
                return (decimal)ObjetoResultado.ValorDecimal;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Paridad";
                return 0;
            }
        }
    }
}
