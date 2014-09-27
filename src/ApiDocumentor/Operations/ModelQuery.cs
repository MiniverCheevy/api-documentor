using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ApiDocumenter.Models;
using Voodoo.Operations;

namespace ApiDocumenter.Operations
{
    public class ModelQuery : Query<ModelRequest, ModelResponse>
    {
        private Assembly assembly;

        public ModelQuery(ModelRequest request)
            : base(request)
        {
        }

        protected override ModelResponse ProcessRequest()
        {
            assembly = request.Assembly;
            buildNamespaces();
            return response;
        }

        private void buildNamespaces()
        {
            response.AllTypes = assembly.GetTypes().ToArray();
            var namespaces = response.AllTypes.Select(c => c.Namespace)
                                                .Distinct()
                                                .Where(c => c != null)
                                                .OrderBy(c => c).ToArray();
            foreach (var @namespace in namespaces)
            {
                buildNamespace(@namespace);
            }
        }

        private void buildNamespace(string @namespace)

        {
            var node = new NamespaceNode(@namespace);
            var classes = response.AllTypes
                .Where(c => c.Namespace == node.FullName 
                        && ! c.IsDefined(typeof (CompilerGeneratedAttribute), false)
                        && ! c.IsNested   
                        && ! request.ExcludedTypes.Contains(c)
                        && (c.GetCustomAttribute<ObsoleteAttribute>() == null)
                    )
                .OrderBy(c => c.Name).ToArray();

            foreach (var @class in classes)
                node.AddClass(@class);
            response.NameSpaces.Add(node);
        }
    }
}