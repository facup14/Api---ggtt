using DATA.DTOS;
using DATA.Extensions;
using DATA.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Service.Queries
{
    public interface ICambiosCentroDeCostoQueryService
    {
        List<CambiosCentroDeCostoDTO> GetHistoricosCambioCentroDeCosto(long idUnidad);
    }
    public class CambiosCentroDeCostoQueryService : ICambiosCentroDeCostoQueryService
    {
        private readonly Context _context;

        public CambiosCentroDeCostoQueryService(Context context)
        {
            _context = context;
        }

        public List<CambiosCentroDeCostoDTO> GetHistoricosCambioCentroDeCosto(long idUnidad)
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
            cmd.CommandText = "sp_CambiosCentroDeCostoDatos";
            cmd.Parameters.Add("@IdUnidad", System.Data.SqlDbType.BigInt).Value = idUnidad;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();

            var listNotifications = (from row in dt.AsEnumerable()
                                     select new CambiosCentroDeCostoDTO()
                                     {
                                         IdCcorigen = row.Field<long>("IdCCOrigen"),
                                         idCcdestino = row.Field<long>("IdCCDestino"),
                                         CCOrigen = row.Field<string>("CCOrigen"),
                                         CCDestino = row.Field<string>("CCDestino"),
                                         Fecha = row.Field<DateTime>("Fecha"),
                                         Motivo = row.Field<string>("Motivo"),
                                         IdCambioCentroDeCosto = row.Field<long>("IdCambioCentroDeCosto"),
                                     }).ToList();
            return listNotifications;
        }
    }
}
