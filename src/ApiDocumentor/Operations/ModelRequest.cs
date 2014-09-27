using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApiDocumenter.Operations
{
    public class ModelRequest
    {
        public Type[] ExcludedTypes { get; set; }
        public Assembly Assembly { get; set; }
        public string OutputPath { get; set; }

        public ModelRequest()
        {
            ExcludedTypes = new Type[]{};
        }
    }
}
