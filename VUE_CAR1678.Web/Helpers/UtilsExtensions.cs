using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VUE_CAR1678.Web.Helpers
{
    public static class UtilsExtensions
    {
        public static object ValorNombrePropiedad<T>(this IEnumerable<T> data, string nombreColumna)
        {
            object resp = null;

            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

                foreach (T item in data)
                {
                    foreach (PropertyDescriptor prop in properties)
                    {
                        if (prop.Name == nombreColumna)
                        {
                            if (prop.GetValue(item) != null)
                            {
                                resp = prop.GetValue(item) ?? DBNull.Value;
                                return resp;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resp;
        }

        public static Dictionary<string, string> obtenerListaPorColumnas<T>(this T data)
        {
            Dictionary<string, string> resp = new Dictionary<string, string>();

            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    var valor = (string)(prop.GetValue(data) ?? DBNull.Value);
                    resp.Add(prop.Name, valor);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resp;
        }

        public static string obtenerValorPorColumnas<T>(this T data, string nombreColumna)
        {
            string resp = "";

            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.Name == nombreColumna)
                    {
                        return (string)(prop.GetValue(data) ?? DBNull.Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resp;
        }

        public static string ObtenerCodrep(ClaimsIdentity identity) {
            var codrep = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return codrep;
            //if (identity!=null) {
            //    var claims = identity.Claims;
            //}
            //return "";
        }
    }
}