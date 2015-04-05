using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ApiDocumenter.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo;
using Voodoo.Messages;

namespace ApiDocumenter.Tests
{
    [TestClass]
    public class ModelQueryTests
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
        public void GenerateModel()
        {
            var request = new EmptyRequest();
            var response = new ModelQuery(GetModelRequest()).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreNotEqual(0, response.NameSpaces.Count);
            Debug.WriteLine(response.ToDebugString());

        }
        [TestMethod]
        public void WriteOutputCommand()
        {
            var response = new WriteOutputCommand(GetModelRequest()).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);          
        }

        [TestMethod]
        public void Demo()
        {
            var request =  new ModelRequest()
                {
                Assembly = typeof(Request).Assembly,
                OutputPath = @"C:\FolderWhereDocument-BootstrapIs\temp.html"
                };
            var response = new WriteOutputCommand(GetModelRequest()).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
        }

        
    }
}
