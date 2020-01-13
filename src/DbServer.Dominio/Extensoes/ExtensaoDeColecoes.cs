using System.Collections.Generic;

namespace DbServer.Dominio.Extensoes
{
    public static class ExtensaoDeColecoes
    {

        public static bool NuloOuVazio<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

    }
}
