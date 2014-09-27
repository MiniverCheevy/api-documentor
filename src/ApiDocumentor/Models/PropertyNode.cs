using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo;

namespace ApiDocumenter.Models
{
    public class PropertyNode : Node
    {
        public bool IsStatic { get; set; }
        public string TypeName { get; set; }
        public Type ReturnType { get; set; }

        public PropertyNode(PropertyInfo property)
        {
            this.Name = property.Name;
            ReturnType = property.PropertyType;
            this.FullName = string.Format("{0}.{1}", ReturnType.FullName, property.Name);
            this.TypeName = property.PropertyType.FixUpTypeName();
            this.IsStatic = property.CanRead && property.GetSetMethod() !=null ? 
                property.GetGetMethod().IsStatic : property.GetSetMethod() != null 
                && property.GetSetMethod().IsStatic;
        }
    }
}