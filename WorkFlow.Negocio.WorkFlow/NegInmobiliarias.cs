using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegInmobiliarias
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;


        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Inmobiliarias
        /// </summary>
        /// <param name="Entidad">Objeto InmobiliariaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad InmobiliariaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<InmobiliariaInfo> Buscar(InmobiliariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<InmobiliariaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<InmobiliariaInfo, InmobiliariaInfo>(Entidad, GlobalDA.SP.Inmobiliarias_QRY, BaseSQL);
                lstInmobiliarias = new List<InmobiliariaInfo>();
                lstInmobiliarias = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Inmobiliarias";
                return ObjetoResultado;
            }
        }

        public Resultado<InmobiliariaInfo> Guardar(InmobiliariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<InmobiliariaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<InmobiliariaInfo>(Entidad, GlobalDA.SP.Inmobiliarias_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Inmobiliaria";
                return ObjetoResultado;
            }
        }

        public Resultado<InmobiliariaInfo> Guardar(ref InmobiliariaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<InmobiliariaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<InmobiliariaInfo>(ref Entidad, GlobalDA.SP.Inmobiliarias_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Inmobiliaria";
                return ObjetoResultado;
            }
        }
        #region PROPIEDADES

       
        public static List<InmobiliariaInfo> lstInmobiliarias
        {
            get { return (List<InmobiliariaInfo>)HttpContext.Current.Session[ISesiones.lstInmobiliarias]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstInmobiliarias, value); }
        }


        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string lstInmobiliarias = "lstInmobiliarias";
          
        }
        #endregion

    }
}
