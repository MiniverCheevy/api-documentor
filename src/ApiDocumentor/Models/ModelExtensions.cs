using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo;

namespace ApiDocumenter.Models
{
    public static class ModelExtensions
    {
        public static string GetPath(this Type type)
        {
            var result = type.FixUpTypeName().Replace("<", "|").Replace(">", "|");
            result = string.Format("{0}.{1}", type.Namespace, result);
            return result;
        }        

        public static string CleanPath(this string path)
        {
            return path.Replace("<", ".").Replace(">", ".").Replace(" ", "-");
        }
    }
}
