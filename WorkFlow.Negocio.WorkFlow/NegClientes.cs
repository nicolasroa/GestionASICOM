using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegClientes
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;

        #region METODOS

        /// <summary>
        /// Método que realiza una consulta de clientes
        /// </summary>
        /// <param name="Entidad">Objeto ClientesInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad CLientesInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ClientesInfo> Buscar(ClientesInfo Entidad, bool conSesion = true)
        {

            var ObjetoResultado = new Resultado<ClientesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ClientesInfo, ClientesInfo>(Entidad, GlobalDA.SP.Clientes_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                if (conSesion)
                    objClienteInfo = ObjetoResultado.Lista[0];
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Clientes.";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que guarda un objeto de cliente
        /// </summary>
        /// <param name="Entidad">Objeto ClientesInfo con el objeto Info correspondiente</param>
        /// <returns>Lista de la Entidad CLientesInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ClientesInfo> Guardar(ClientesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ClientesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.Clientes_GRB, GlobalDA.Accion.Guardar, BaseSQL);


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Clientes.";
                return ObjetoResultado;
            }
        }
        public Resultado<ClientesInfo> Guardar(ref ClientesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ClientesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(ref Entidad, GlobalDA.SP.Clientes_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                objClienteInfo = Entidad;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Clientes.";
                return ObjetoResultado;
            }
        }


        public Resultado<CategoriaSiiInfo> BuscarCategoriaSii(CategoriaSiiInfo Entidad)
        {

            var ObjetoResultado = new Resultado<CategoriaSiiInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<CategoriaSiiInfo, CategoriaSiiInfo>(Entidad, GlobalDA.SP.CategoriasSii_QRY, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ex.Message;
                return ObjetoResultado;
            }
        }

        public Resultado<SubCategoriaSiiInfo> BuscarSubCategoriaSii(SubCategoriaSiiInfo Entidad)
        {

            var ObjetoResultado = new Resultado<SubCategoriaSiiInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<SubCategoriaSiiInfo, SubCategoriaSiiInfo>(Entidad, GlobalDA.SP.SubCategoriasSii_QRY, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ex.Message;
                return ObjetoResultado;
            }
        }

        public Resultado<ActividadSiiInfo> BuscarActividadSii(ActividadSiiInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ActividadSiiInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ActividadSiiInfo, ActividadSiiInfo>(Entidad, GlobalDA.SP.ActividadesSii_QRY, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ex.Message;
                return ObjetoResultado;
            }
        }
        public Resultado<ProfesionesInfo> BuscarProfesiones(ProfesionesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ProfesionesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ProfesionesInfo, ProfesionesInfo>(Entidad, GlobalDA.SP.Profesiones_QRY, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception ex)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ex.Message;
                return ObjetoResultado;
            }
        }
        public Resultado<DireccionClienteInfo> BuscarDireccion(DireccionClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DireccionClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<DireccionClienteInfo, DireccionClienteInfo>(Entidad, GlobalDA.SP.DireccionesCliente_QRY, BaseSQL);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                    lstDireccionesCliente = new List<DireccionClienteInfo>();
                    lstDireccionesCliente = ObjetoResultado.Lista;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Direccion Cliente.";
                return ObjetoResultado;
            }
        }
        public Resultado<DireccionClienteInfo> GuardarDireccion(DireccionClienteInfo Entidad)
        {
            var ObjetoResultado = new Resultado<DireccionClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.DireccionesCliente_GRB, GlobalDA.Accion.Guardar, BaseSQL);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Direccion Cliente.";
                return ObjetoResultado;
            }
        }
        public Resultado<DireccionClienteInfo> EliminarDireccion(DireccionClienteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<DireccionClienteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.DireccionesCliente_DEL, GlobalDA.Accion.Guardar, BaseSQL);


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Direccion Cliente.";
                return ObjetoResultado;
            }
        }
        public Resultado<ClienteRelacionadoInfo> GuardarClienteRelacionado(ClienteRelacionadoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ClienteRelacionadoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ClienteRelacionadoInfo>(Entidad, GlobalDA.SP.ClientesRelacionados_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ClienteRelacionado";
                return ObjetoResultado;
            }
        }
        public Resultado<ClienteRelacionadoInfo> BuscarClienteRelacionado(ClienteRelacionadoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ClienteRelacionadoInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ClienteRelacionadoInfo, ClienteRelacionadoInfo>(Entidad, GlobalDA.SP.ClientesRelacionados_QRY, BaseSQL);
                lstClientesRelacionados = new List<ClienteRelacionadoInfo>();
                lstClientesRelacionados = ObjetoResultado.Lista;
                
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " ClienteRelacionado";
                return ObjetoResultado;
            }
        }
        public Resultado<ClienteRelacionadoInfo> EliminarClienteRelacionado(ClienteRelacionadoInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ClienteRelacionadoInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.ClientesRelacionados_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ClienteRelacionado";
                return ObjetoResultado;
            }
        }


        #endregion

        #region PROPIEDADES

        public static ClientesInfo objClienteInfo
        {
            get { return (ClientesInfo)HttpContext.Current.Session[ISesiones.objClienteInfo]; }
            set { HttpContext.Current.Session.Add(ISesiones.objClienteInfo, value); }
        }
        public static List<DireccionClienteInfo> lstDireccionesCliente
        {
            get { return (List<DireccionClienteInfo>)HttpContext.Current.Session[ISesiones.lstDireccionesCliente]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstDireccionesCliente, value); }
        }

        public static List<ClienteRelacionadoInfo> lstClientesRelacionados
        {
            get { return (List<ClienteRelacionadoInfo>)HttpContext.Current.Session[ISesiones.lstClientesRelacionados]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstClientesRelacionados, value); }
        }


        public static DireccionClienteInfo objDireccionesCliente
        {
            get { return (DireccionClienteInfo)HttpContext.Current.Session[ISesiones.objDireccionesCliente]; }
            set { HttpContext.Current.Session.Add(ISesiones.objDireccionesCliente, value); }
        }
        public static bool? IndNuevoCliente
        {
            get { return (bool?)HttpContext.Current.Session[ISesiones.IndNuevoCliente]; }
            set { HttpContext.Current.Session.Add(ISesiones.IndNuevoCliente, value); }
        }
        public static bool? IndClienteModal
        {
            get { return (bool?)HttpContext.Current.Session[ISesiones.IndClienteModal]; }
            set { HttpContext.Current.Session.Add(ISesiones.IndClienteModal, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public const string objClienteInfo = "objClienteInfo";
            public const string IndNuevoCliente = "IndNuevoCliente";
            public const string IndClienteModal = "IndClienteModal";
            public const string lstDireccionesCliente = "lstDireccionesCliente";
            public const string objDireccionesCliente = "objDireccionesCliente";
            public const string lstClientesRelacionados = "lstClientesRelacionados";
        }
        #endregion



    }
}
