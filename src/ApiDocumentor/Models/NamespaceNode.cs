using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo;

namespace ApiDocumenter.Models
{
    public class NamespaceNode : Node
    {
        public NamespaceNode()
        {
            Classes = new List<ClassNode>();
        }

        public NamespaceNode(string @namespace) : this()
        {
            Name = @namespace;            
            FullName = @namespace;
            Type = NodeType.Namespace;
        }

        public List<ClassNode> Classes { get; set; }

        internal void AddClass(Type @class)
        {
            var node = new ClassNode(@class);
            this.Classes.Add(node);
        }
    }
}