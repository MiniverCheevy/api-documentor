using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApiDocumenter.Models
{
    public class Node
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public NodeType Type { get; set; }
        public string Path { get; set; }
        public TypeInformation TypeInfo { get; set; }
    }
}