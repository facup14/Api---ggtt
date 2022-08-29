using DATA.DTOS;
using DATA.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Service.Queries
{
    public interface IUnidadesChoferesQueryService
    {
        List<UnidadesChoferesDTO> GetUnidadesChoferes(long idUnidad);
    }

    public class UnidadesChoferesQueryService : IUnidadesChoferesQueryService
    {
        private readonly Context _context;

        public UnidadesChoferesQueryService(Context context)
        {
            _context = context;
        }
        public List<UnidadesChoferesDTO> GetUnidadesChoferes(long idUnidad)
        {

            UnidadesQueryService unidadesQuery = new UnidadesQueryService(_context);

            var unidad = unidadesQuery.GetAsync(idUnidad);

            if (unidad.Result is null)
            {
                throw new EmptyCollectionException("La Unidad ingresada no Existe");
            }
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_UnidadesChoferesTabla";
            cmd.Parameters.Add("@IdUnidad", System.Data.SqlDbType.BigInt).Value = idUnidad;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();

            var listNotifications = (from row in dt.AsEnumerable()
                                     select new UnidadesChoferesDTO()
                                     {
                                         IdChofer = row.Field<long>("IdChofer"),
                                         apellidoynombres = row.Field<string>("apellidoynombres"),
                                         legajo = row.Field<string>("legajo"),
                                         carnetvence = row.Field<DateTime>("carnetvence"),
                                         obs = row.Field<string>("obs"),
                                         Fecha = row.Field<string>("Fecha"),
                                         Hasta = row.Field<string>("Hasta"),
                                         actual = row.Field<bool>("actual"),
                                     }).ToList();
            return listNotifications;
        }
    }
}
