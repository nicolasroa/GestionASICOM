using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Web;

namespace WorkFlow.Negocio.WorkFlow
{
    public class NegParticipante
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_WF;
        #region METODOS

        public Resultado<TipoParticipanteInfo> GuardarTipoParticipante(TipoParticipanteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoParticipanteInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoParticipanteInfo>(Entidad, GlobalDA.SP.TipoParticipante_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tipo Participante";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoParticipanteInfo> GuardarTipoParticipante(ref TipoParticipanteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoParticipanteInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TipoParticipanteInfo>(ref Entidad, GlobalDA.SP.TipoParticipante_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tipo Participante";
                return ObjetoResultado;
            }
        }
        public Resultado<TipoParticipanteInfo> BuscarTipoParticipante(TipoParticipanteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TipoParticipanteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TipoParticipanteInfo, TipoParticipanteInfo>(Entidad, GlobalDA.SP.TipoParticipante_QRY, BaseSQL);
                lstTipoParticipante = new List<TipoParticipanteInfo>();
                lstTipoParticipante = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tipo Participante";
                return ObjetoResultado;
            }
        }
        public Resultado<ParticipanteInfo> GuardarParticipante(ParticipanteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ParticipanteInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ParticipanteInfo>(Entidad, GlobalDA.SP.Participante_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Participante";
                return ObjetoResultado;
            }
        }
        public Resultado<ParticipanteInfo> GuardarParticipante(ref ParticipanteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ParticipanteInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ParticipanteInfo>(ref Entidad, GlobalDA.SP.Participante_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Participante";
                return ObjetoResultado;
            }
        }
        public Resultado<ParticipanteInfo> BuscarParticipante(ParticipanteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ParticipanteInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<ParticipanteInfo, ParticipanteInfo>(Entidad, GlobalDA.SP.Participante_QRY, BaseSQL);
                lstParticipantes = new List<ParticipanteInfo>();
                lstParticipantes = ObjetoResultado.Lista;
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Participante";
                return ObjetoResultado;
            }
        }
        public Resultado<ParticipanteInfo> EliminarParticipante(ParticipanteInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ParticipanteInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<ParticipanteInfo>(Entidad, GlobalDA.SP.Participante_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + " Participante";
                return ObjetoResultado;
            }
        }


        public Resultado<AntecedentesLaboralesInfo> GuardarAntecedentesLaborales(AntecedentesLaboralesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AntecedentesLaboralesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<AntecedentesLaboralesInfo>(Entidad, GlobalDA.SP.AntecedentesLaborales_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " AntecedentesLaborales";
                return ObjetoResultado;
            }
        }
        public Resultado<AntecedentesLaboralesInfo> GuardarAntecedentesLaborales(ref AntecedentesLaboralesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AntecedentesLaboralesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<AntecedentesLaboralesInfo>(ref Entidad, GlobalDA.SP.AntecedentesLaborales_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " AntecedentesLaborales";
                return ObjetoResultado;
            }
        }
        public Resultado<AntecedentesLaboralesInfo> BuscarAntecedentesLaborales(AntecedentesLaboralesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AntecedentesLaboralesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<AntecedentesLaboralesInfo, AntecedentesLaboralesInfo>(Entidad, GlobalDA.SP.AntecedentesLaborales_QRY, BaseSQL);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " AntecedentesLaborales";
                return ObjetoResultado;
            }
        }
        public Resultado<AntecedentesLaboralesInfo> EliminarAntecedentesLaborales(AntecedentesLaboralesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AntecedentesLaboralesInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.Usuario.Rut;

                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion(Entidad, GlobalDA.SP.AntecedentesLaborales_DEL, GlobalDA.Accion.Eliminar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " AntecedentesLaborales";
                return ObjetoResultado;
            }
        }



       




        #endregion

        #region PROPIEDADES
        public static List<TipoParticipanteInfo> lstTipoParticipante
        {
            get { return (List<TipoParticipanteInfo>)HttpContext.Current.Session[ISesiones.lstTipoParticipante]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTipoParticipante, value); }
        }
        public static List<ParticipanteInfo> lstParticipantes
        {
            get { return (List<ParticipanteInfo>)HttpContext.Current.Session[ISesiones.lstParticipante]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstParticipante, value); }
        }
        public static List<ParticipanteInfo> lstParticipantesDPS
        {
            get { return (List<ParticipanteInfo>)HttpContext.Current.Session[ISesiones.lstParticipantesDPS]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstParticipantesDPS, value); }
        }

        public static ParticipanteInfo ObjParticipante
        {
            get { return (ParticipanteInfo)HttpContext.Current.Session[ISesiones.ObjParticipante]; }
            set { HttpContext.Current.Session.Add(ISesiones.ObjParticipante, value); }
        }

        public static ParticipanteInfo ObjParticipantePDF
        {
            get { return (ParticipanteInfo)HttpContext.Current.Session[ISesiones.ObjParticipantePDF]; }
            set { HttpContext.Current.Session.Add(ISesiones.ObjParticipantePDF, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string lstParticipante = "lstParticipante";
            public static string lstParticipantesDPS = "lstParticipantesDPS";
            public static string lstTipoParticipante = "lstTipoParticipante";
            public static string ObjParticipante = "ObjParticipante";
            public static string ObjParticipantePDF = "ObjParticipantePDF";
        }
        #endregion




    }
}
