using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Voodoo;
using Voodoo.Messages;

namespace ApiDocumenter.Models
{
    public class ClassNode : Node
    {
        private List<string> gettersAndSetters;
        public List<Type> Parents { get; set; }
        public List<Type> Interfaces { get; set; }
        public List<INameValuePair> Descriptions { get; set; }
        public List<INameValuePair> Addendums { get; set; }

        public ClassNode()
        {
            Parents = new List<Type>();
            Interfaces = new List<Type>();
            Properties = new List<PropertyNode>();
            Methods = new List<MethodNode>();
            gettersAndSetters = new List<string>();
        }

        public ClassNode(Type @class) : this()
        {
            Name = @class.FixUpTypeName();
            Path = @class.GetPath();
            FullName = string.Format("{0}.{1}", @class.Namespace, Name);
            if (@class.Name == "ILog")
            {
            }
            getProperties(@class);
            getMethods(@class);
            var parent = @class.BaseType;
            while (parent != null)
            {
                if (parent != typeof (object))
                    Parents.Add(parent);

                parent = parent.BaseType;
            }
            foreach (var face in @class.GetInterfaces().ToList())
            {
                if (@class.DoesImplementInterfaceOf(face))
                    Interfaces.Add(face);
            }

        }

        private void getMethods(Type @class)
        {
            var noiseMethods = new string[] {"ToString", "Equals", "GetHashCode", "GetType"};
            foreach (
                var method in
                    @class.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static |
                                      BindingFlags.DeclaredOnly )
                          .Where(c => ! noiseMethods.Contains(c.Name)
                                      && c.GetCustomAttribute<CompilerGeneratedAttribute>() == null
                                       && (c.GetCustomAttribute<ObsoleteAttribute>() == null)
                                      && ! gettersAndSetters.Contains(c.Name)))
            {
               
                var methodNode = new MethodNode(method);
                this.Methods.Add(methodNode);
            }
        }

        private void getProperties(Type @class)
        {
            foreach (
                var property in
                    @class.GetProperties(BindingFlags.Public |
                        BindingFlags.Instance | BindingFlags.Static |
                        BindingFlags.DeclaredOnly )
                        .Where(c=>c.GetCustomAttribute<ObsoleteAttribute>() == null))
                        
            {
                if (property.GetGetMethod() !=null)
                    gettersAndSetters.Add(property.GetGetMethod().Name);
                if (property.GetSetMethod() != null)
                    gettersAndSetters.Add(property.GetSetMethod().Name);
               
                var propertyNode = new PropertyNode(property);
                this.Properties.Add(propertyNode);
            }
        }

        public List<PropertyNode> Properties { get; set; }
        public List<MethodNode> Methods { get; set; }
    }
}