using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiDocumenter.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Voodoo;
using Voodoo.Messages;

namespace ApiDocumenter.Tests
{
    [TestClass]
    public class JsonTests
    {
        public ModelRequest GetModelRequest()
        {
            return new ModelRequest()
            {
                Assembly = typeof(Request).Assembly,
                OutputPath = @"Z:\Dropbox\Lib\Projects\voodoo.gh-pages\voodoo\temp.html"
            };
        }
        [TestMethod]
       public void GenerateJson ()
       {
           var request = new EmptyRequest();
           var response = new ModelQuery(GetModelRequest()).Execute();
           Assert.AreEqual(null, response.Message);
           Assert.AreEqual(true, response.IsOk);
           Assert.AreNotEqual(0, response.NameSpaces.Count);
           Debug.WriteLine(response.ToDebugString());

           IoNic.WriteFile(JsonConvert.SerializeObject(response), @"c:\temp.json");

       }
    }
}
