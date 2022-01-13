using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DevExpress17EmptyWebForms.BLL
{
    public class DigitosMinistracion : BasicAccess
    {
        /// <summary>
        /// Obtiene el identificador seleccionado
        /// </summary>
        /// <param name="idDigitoMinistracion">Identidicador</param>
        /// <returns></returns>
        public DAL.DigitosMinistracion Load(int idDigitoMinistracion)
        {
            try
            {
                InitAction(enumBasicCurrentAction.Load);
                return Context.DigitosMinistracions.Single(m => m.idDigitoMinistracion == idDigitoMinistracion);
            }
            catch (Exception ex)
            {
                SetError(ex);
                return null;
            }
        }

        public bool AgregarDigitoMisnistracion(string Codigo, string Descripcion)
        {
            // Instancia que establece valores a los campos que no se insertan desde la vista
            var EntidadDigitosMinistracion = new DAL.DigitosMinistracion
            {
                RegFecha = DateTime.Now,
                Codigo = Codigo,
                Descripcion = Descripcion,
                idUsuario = 1
            };

            SqlParameter[] parametros = new SqlParameter[5];

            try
            {
                InitAction(enumBasicCurrentAction.Add);
                BeginProcess();

                parametros[0] = CreateParameter("@Op", SqlDbType.VarChar, "Nuevo");
                parametros[1] = CreateParameter("@RegFecha", SqlDbType.DateTime, EntidadDigitosMinistracion.RegFecha);
                parametros[2] = CreateParameter("@Codigo", SqlDbType.VarChar, EntidadDigitosMinistracion.Codigo);
                parametros[3] = CreateParameter("@Descripcion", SqlDbType.VarChar, EntidadDigitosMinistracion.Descripcion);
                parametros[4] = CreateParameter("@idUsuario", SqlDbType.Int, EntidadDigitosMinistracion.idUsuario);

                // Parámetros de salida
                //parametros[7] = CreateParameter("@Exito", SqlDbType.Bit, ParameterDirection.Output);
                //parametros[8] = CreateParameter("@Mensaje", SqlDbType.VarChar, ParameterDirection.Output);

                ExecuteSp("[dbo].[sp_DigitosMinistracion]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                SetError(ex);
                return false;
            }
            finally
            {
                EndProcess();
            }
        }
    }
}