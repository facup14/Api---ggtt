using Common.Collection;
using PERSISTENCE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Filter
{
    public static class Filters
    {
        public static async Task<ICollection<T>> Filters<T>(
            this IQueryable<T> query, string filtro)
        {
            var entity = Context
            switch (filtro)
            {
                case "Obs":
                    return await query;
                case IQueryable<T> queryFilter:
                    return await queryFilter.ToListAsync();
                default:
                    return await queryFilter.ToListAsync();
            }

            var result = new DataCollection<T>
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalPages
            };
            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            }
            if (result.Pages < page)
            {
                throw new EmptyCollectionException("La colección no poseé mas de" + " " + result.Pages + " " + "pagina/as");
            }

            return result;
        }        
    }
}
