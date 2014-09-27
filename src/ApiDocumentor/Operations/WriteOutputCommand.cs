using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiDocumenter.Models;
using Voodoo;
using Voodoo.Messages;
using Voodoo.Operations;

namespace ApiDocumenter.Operations
{
    public class WriteOutputCommand : Command<ModelRequest, Response>
    {
        public WriteOutputCommand(ModelRequest request) : base(request)
        {
        }

        protected override Response ProcessRequest()
        {

            var model = new ModelQuery(request).Execute();
            var paths = new Dictionary<Type, string>();
            foreach (var type in model.AllTypes.Distinct().ToArray())
            {
                var path = type.GetPath();
                var typeName = type.FixUpTypeName();
                if (type.GetGenericArguments().Any())
                    typeName = type.GetTypeNameWithoutGenericArguments();
                var link = string.Format("<a href=\"#{0}\">{1}</a>", path, HttpUtility.HtmlEncode(typeName));
                paths.Add(type, link);
            }

            var body = new Body() { Model = model.NameSpaces };
            body.Paths = paths;
            var html = body.TransformText();
            IoNic.WriteFile(html, request.OutputPath);
            return response;

        }
    }
}
