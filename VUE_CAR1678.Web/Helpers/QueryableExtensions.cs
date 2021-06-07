using VUE_CAR1678.Web.Models.Almacen;
using System.Linq;

namespace VUE_CAR1678.Web.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, int pagina, int cantidadRegistroPorPagina)
        {
            return queryable
                .Skip((pagina - 1) * cantidadRegistroPorPagina)
                .Take(cantidadRegistroPorPagina);
        }
    }
}