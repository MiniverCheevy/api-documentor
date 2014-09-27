using System;
using System.Collections.Generic;
using System.Linq;
using ApiDocumenter.Models;
using Voodoo.Messages;

namespace ApiDocumenter.Operations
{
    public class ModelResponse : Response
    {
        public List<NamespaceNode> NameSpaces { get; set; }
        
        public ModelResponse()
        {
            NameSpaces = new List<NamespaceNode>();
        }

        public Type[] AllTypes { get; set; }
    }
}
