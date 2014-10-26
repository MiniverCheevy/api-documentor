using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Voodoo;

namespace ApiDocumenter.Models
{
    public class MethodNode : Node
    {
        public string Signature { get; set; }
        public string TypeArguments { get; set; }
        public string Source { get; set; }
        public Type SourceType { get; set; }
        public string ReturnTypeName { get; set; }
        public Type ReturnType { get; set; }

        public MethodNode(MethodInfo method)
        {
            var path = method.DeclaringType.GetPath();
            
            ReturnType = method.ReturnType;
            ReturnTypeName = ReturnType.FixUpTypeName();
            buildTypeArguments(method);
            buildSignature(method);
            Path = string.Format("{0}.{1}.{2}", path, TypeArguments, Signature).CleanPath();
            this.Name = method.Name;
            this.FullName = string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
        }

        private void buildSignature(MethodInfo method)
        {
            this.IsStatic = method.IsStatic;
            var isExtension = method.IsDefined(typeof (ExtensionAttribute), false);
            const string space = " ";
            const string comma = ",";
            var signatureBuilder = new StringBuilder();
            var parameters = method.GetParameters();
            if (parameters.Any())
            {
                if (isExtension)
                {
                    this.SourceType = parameters.First().ParameterType;
                    this.Source = SourceType.FixUpTypeName();
                    
                }
                var lastParameter = parameters.Last();
                var counter = isExtension ? 1 : 0;
                for (var i = counter; i < parameters.Count(); i++)
                {
                    signatureBuilder.Append(parameters[i].ParameterType.FixUpTypeName());
                    signatureBuilder.Append(space);
                    signatureBuilder.Append(parameters[i].Name);
                    if (parameters[i] == lastParameter)
                        continue;
                    signatureBuilder.Append(comma);
                    signatureBuilder.Append(space);
                }
            }

            this.Signature = signatureBuilder.ToString();
        }

        private void buildTypeArguments(MethodInfo method)
        {
            var typeArguments = new StringBuilder();            
            var arguments = method.GetGenericArguments();
            if (arguments.Any())
            {
                typeArguments.Append("<");
                var lastArgument = arguments.Last();
                foreach (var argument in arguments)
                {
                    typeArguments.Append(argument.Name);
                    if (argument != lastArgument)
                        typeArguments.Append(",");
                }

                typeArguments.Append(">");
            }
            this.TypeArguments = typeArguments.ToString();
        }

        public bool IsStatic { get; set; }
    }
}