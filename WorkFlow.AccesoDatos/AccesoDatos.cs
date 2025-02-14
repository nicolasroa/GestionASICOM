using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;

namespace WorkFlow.AccesoDatos
{
    public static class AccesoDatos
    {
        /// <summary>
        /// Metodo que Ejecuta un Procedimiento almacenado de Búsqueda
        /// </summary>
        /// <typeparam name="T">Tipo de Dato de la Lista Resultante</typeparam>
        /// <typeparam name="X">Tipo de Dato del Filtro</typeparam>
        /// <param name="Filtro">Objeto Fitro</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns></returns>
        public static Resultado<T> Buscar<T, X>(X Filtro, GlobalDA.SP Procedimiento, string BaseSQL)
        {
            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = GlobalDA.EjecutarProcedimientoBusqueda<T, X>(Filtro, Procedimiento.ToString(), BaseSQL);

                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase;
                    }
                }
            }
            catch (Exception Ex)
            {

                if (!Constantes.ModoDebug)
                {
                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase;
                }
                else
                {
                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                }
            }

            return ObjResultado;
        }
        /// <summary>
        /// Metodo que Ejecuta un Procedimiento almacenado de Búsqueda con Coenxion variable
        /// </summary>
        /// <typeparam name="T">Tipo de Dato de la Lista Resultante</typeparam>
        /// <typeparam name="X">Tipo de Dato del Filtro</typeparam>
        /// <param name="Filtro">Objeto Fitro</param>
        /// <param name="Conexion">Nombre de la Conexion</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns></returns>
        public static Resultado<T> Operacion<T>(T Entidad, GlobalDA.SP Procedimiento, GlobalDA.Accion Accion, string BaseSQL)
        {

            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = GlobalDA.EjecutarProcedimientoOperacional<T>(Entidad, Procedimiento.ToString(), BaseSQL);
                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        switch (Accion)
                        {
                            case GlobalDA.Accion.Eliminar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                                break;
                            case GlobalDA.Accion.Guardar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                            default:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (!Constantes.ModoDebug)
                {
                    switch (Accion)
                    {
                        case GlobalDA.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                            break;
                        case GlobalDA.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                    }
                }
                else
                {
                    switch (Accion)
                    {
                        case GlobalDA.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        case GlobalDA.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                    }
                }
            }
            return ObjResultado;
        }

        //public static Resultado<T> Operacion<T>(List<T> lstEntidad, string NombreTabla, GlobalDA.SP Procedimiento, GlobalDA.Accion Accion, String BaseSQL)
        //{

        //    var NombreClase = "";
        //    T obj = default(T);
        //    obj = Activator.CreateInstance<T>();
        //    NombreClase = obj.GetType().Name.Replace("Info", "");
        //    var ObjResultado = new Resultado<T>();
        //    try
        //    {
        //        ObjResultado = GlobalDA.EjecutarProcedimientoOperacional<T>(lstEntidad, NombreTabla, Procedimiento.ToString(), BaseSQL);
        //        if (!ObjResultado.ResultadoGeneral)
        //        {
        //            if (!Constantes.ModoDebug)
        //            {
        //                switch (Accion)
        //                {
        //                    case GlobalDA.Accion.Eliminar:
        //                        ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
        //                        break;
        //                    case GlobalDA.Accion.Guardar:
        //                        ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
        //                        break;
        //                    default:
        //                        ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        if (!Constantes.ModoDebug)
        //        {
        //            switch (Accion)
        //            {
        //                case GlobalDA.Accion.Eliminar:
        //                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
        //                    break;
        //                case GlobalDA.Accion.Guardar:
        //                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
        //                    break;
        //                default:
        //                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            switch (Accion)
        //            {
        //                case GlobalDA.Accion.Eliminar:
        //                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase + " Exception: " + Ex.Message;
        //                    break;
        //                case GlobalDA.Accion.Guardar:
        //                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
        //                    break;
        //                default:
        //                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
        //                    break;
        //            }
        //        }
        //    }
        //    return ObjResultado;
        //}


        public static Resultado<T> Operacion<T>(ref T Entidad, GlobalDA.SP Procedimiento, GlobalDA.Accion Accion, string BaseSQL)
        {

            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = GlobalDA.EjecutarProcedimientoOperacional(ref Entidad, Procedimiento.ToString(), BaseSQL);
                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        switch (Accion)
                        {
                            case GlobalDA.Accion.Eliminar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                                break;
                            case GlobalDA.Accion.Guardar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                            default:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (!Constantes.ModoDebug)
                {
                    switch (Accion)
                    {
                        case GlobalDA.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                            break;
                        case GlobalDA.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                    }
                }
                else
                {
                    switch (Accion)
                    {
                        case GlobalDA.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        case GlobalDA.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                    }
                }
            }
            return ObjResultado;
        }

        public static Resultado<T> OperacionLog<T>(T Entidad, GlobalDA.SP Procedimiento)
        {
            return GlobalDA.EjecutarProcedimientoLog<T>(Entidad, Procedimiento.ToString());
        }

    }
}
