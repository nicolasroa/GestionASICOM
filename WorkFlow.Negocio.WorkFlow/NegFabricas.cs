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
    public class NegFabricas
    {

        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS
        public Resultado<AsignacionTipoFabricaInfo> GuardarAsignacionTipoFabrica(AsignacionTipoFabricaInfo Entidad)
        {
            Resultado<AsignacionTipoFabricaInfo> ObjetoResultado = new Resultado<AsignacionTipoFabricaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<AsignacionTipoFabricaInfo>(Entidad, GlobalDA.SP.AsignacionTipoFabrica_INS, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Asignacion Tipo Fabrica";
                return ObjetoResultado;
            }
        }
        public Resultado<AsignacionTipoFabricaInfo> EliminarAsignacionTipoFabrica(AsignacionTipoFabricaInfo Entidad)
        {
            Resultado<AsignacionTipoFabricaInfo> ObjetoResultado = new Resultado<AsignacionTipoFabricaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<AsignacionTipoFabricaInfo>(Entidad, GlobalDA.SP.AsignacionTipoFabrica_DEL, GlobalDA.Accion.Eliminar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Asignacion Tipo Fabrica";
                return ObjetoResultado;
            }
        }
        public Resultado<AsignacionTipoFabricaInfo> BuscarAsignacionTipoFabrica(AsignacionTipoFabricaInfo Entidad)
        {
            var ObjetoResultado = new Resultado<AsignacionTipoFabricaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<AsignacionTipoFabricaInfo, AsignacionTipoFabricaInfo>(Entidad, GlobalDA.SP.AsignacionTipoFabrica_QRY, BaseSQL);
                lstAsignacionTipoFabrica = new List<AsignacionTipoFabricaInfo>();
                lstAsignacionTipoFabrica = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Asignacion Tipo de Fabrica";
                return ObjetoResultado;
            }
        }
        public Resultado<FabricaInfo> GuardarFabrica(FabricaInfo Entidad)
        {
            Resultado<FabricaInfo> ObjetoResultado = new Resultado<FabricaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<FabricaInfo>(Entidad, GlobalDA.SP.Fabricas_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tipo Fabrica";
                return ObjetoResultado;
            }
        }
        public Resultado<FabricaInfo> GuardarFabrica(ref FabricaInfo Entidad)
        {
            var ObjetoResultado = new Resultado<FabricaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<FabricaInfo>(ref Entidad, GlobalDA.SP.Fabricas_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tipo Fabrica";
                return ObjetoResultado;
            }
        }
        public Resultado<FabricaInfo> BuscarFabrica(FabricaInfo Entidad)
        {
            var ObjetoResultado = new Resultado<FabricaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<FabricaInfo, FabricaInfo>(Entidad, GlobalDA.SP.Fabricas_QRY, BaseSQL);
                lstFabrica = new List<FabricaInfo>();
                lstFabrica = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tipo de Fabrica";
                return ObjetoResultado;
            }
        }
        #endregion

        #region PROPIEDADES

        public static List<AsignacionTipoFabricaInfo> lstAsignacionTipoFabrica
        {
            get { return (List<AsignacionTipoFabricaInfo>)HttpContext.Current.Session[ISesiones.lstAsignacionTipoFabrica]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstAsignacionTipoFabrica, value); }
        }
       
        public static List<FabricaInfo> lstFabrica
        {
            get { return (List<FabricaInfo>)HttpContext.Current.Session[ISesiones.lstFabrica]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstFabrica, value); }
        }

        #endregion


        #region SESIONES
        private class ISesiones
        {
            public const string lstAsignacionTipoFabrica = "lstAsignacionTipoFabrica";
            public const string lstFabrica = "lstFabrica";
            
        }
        #endregion

    }
}
