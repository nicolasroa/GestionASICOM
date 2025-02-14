using WorkFlow.AccesoDatos;
using WorkFlow.Entidades;
using WorkFlow.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkFlow.Negocio.Administracion
{
    public class NegTablas
    {
        public static string BaseSQL = ConfigBase.ConexionSQL_SEG;
        public NegTablas()
        {

        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Tabla según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Tabla</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<TablaInfo> Guardar(TablaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TablaInfo>();
            try
            {
                Entidad.Usuario_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.AccesoDatos.Operacion<TablaInfo>(Entidad, GlobalDA.SP.Tablas_GRB, GlobalDA.Accion.Guardar, BaseSQL);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tabla";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Tablas
        /// </summary>
        /// <param name="Entidad">Objeto Tabla con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TablaInfo> Buscar(TablaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TablaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TablaInfo, TablaInfo>(Entidad, GlobalDA.SP.Tablas_QRY, BaseSQL);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tabla";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Metodo que busca el Id de un registro de la Entidad Tabla
        /// </summary>
        /// <param name="Entidad">Objeto Tabla con los Parámetros de busqueda</param>
        /// <returns>Retorna un valor de tipo int</returns>
        public static int? IdentificadorMaestro(string Concepto, string Codigo)
        {

            Tabla Entidad = new Tabla { Concepto = Concepto, Codigo = Codigo };
            var ObjetoResultado = new Resultado<TablaInfo>();
            var negTablas = new NegTablas();
            try
            {
                int ID = new int();
                if (negTablas.ConceptosRegistrados == null)
                    negTablas.ConceptosRegistrados = new List<TablaInfo>();
                if (negTablas.ConceptosRegistrados.Count(c => c.ConceptoPadre == Concepto && c.Codigo == Codigo) != 0)
                {
                    ID = (int)negTablas.ConceptosRegistrados.FirstOrDefault(c => c.ConceptoPadre == Concepto && c.Codigo == Codigo).CodigoInterno;
                    return ID;
                }
                else
                {
                    ObjetoResultado = AccesoDatos.AccesoDatos.Buscar<TablaInfo, Tabla>(Entidad, GlobalDA.SP.Tablas_QRY, BaseSQL);
                    if (ObjetoResultado.Lista == null)
                    {
                        return null;
                    }
                    else
                    {
                        if (ObjetoResultado.Lista.Count() == 0)
                        {
                            return null;
                        }
                        else
                        {
                            ID = (int)ObjetoResultado.Lista.FirstOrDefault().CodigoInterno;
                            negTablas.ConceptosRegistrados.Add(ObjetoResultado.Lista.FirstOrDefault());
                            return ID;
                        }

                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }



        public static List<TablaInfo> BuscarCatalogo(string Concepto)
        {

            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoTabla = new TablaInfo();
                var NegTablas = new NegTablas();
                var ObjetoResultado = new Resultado<TablaInfo>();
                int? Estado_Id = new int();
                Estado_Id = NegTablas.IdentificadorMaestro(ConfigBase.TablaEstado, ConfigBase.CodigoActivo);

                ObjetoTabla.Concepto = Concepto;
                if (Estado_Id != null) { ObjetoTabla.Estado_Id = (int)Estado_Id; }
                if (CatalogosRegistrados == null)
                    CatalogosRegistrados = new List<TablaInfo>();
                if (CatalogosRegistrados.Count(c => c.ConceptoPadre == Concepto && c.Estado_Id == Estado_Id) > 0)
                {
                    ObjetoResultado.Lista = CatalogosRegistrados.Where(c => c.ConceptoPadre == Concepto && c.Estado_Id == Estado_Id).ToList<TablaInfo>();
                    return ObjetoResultado.Lista;
                }

                ObjetoResultado = NegTablas.Buscar(ObjetoTabla);

                if (ObjetoResultado.ResultadoGeneral)
                {

                    foreach (var dato in ObjetoResultado.Lista)
                    {
                        CatalogosRegistrados.Add(dato);
                    }

                    return ObjetoResultado.Lista;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }



        }

        #region PROPIEDADES


        public List<TablaInfo> ConceptosRegistrados
        {
            get { return (List<TablaInfo>)HttpContext.Current.Session[ISesiones.ConceptosRegistrados]; }
            set { HttpContext.Current.Session.Add(ISesiones.ConceptosRegistrados, value); }
        }

        public static List<TablaInfo> CatalogosRegistrados
        {
            get { return (List<TablaInfo>)HttpContext.Current.Session[ISesiones.CatalogosRegistrados]; }
            set { HttpContext.Current.Session.Add(ISesiones.CatalogosRegistrados, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {

            public const string ConceptosRegistrados = "ConceptosRegistrados";
            public const string CatalogosRegistrados = "CatalogosRegistrados";
        }
        #endregion

    }
}
