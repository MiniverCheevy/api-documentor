api-documentor
==============

uses reflection to walk a .Net assembly and generate psuedo msdn style documentation, does not read xml comments.


Download the <a href="http://visualstudiogallery.msdn.microsoft.com/1f6ec6ff-e89b-4c47-8e79-d2d68df894ec">Razor Generator</a> and install it.  Download the latest version of the excellent <a href="https://github.com/gfranko/Document-Bootstrap">Document-Bootstrap</a>.  Note:  this will only provide the structure and display classes and public instance properties and methods.  You will add the remarks and explanations.

<pre>
<code>
    var request =  new ModelRequest()
    {
        Assembly = typeof(Request).Assembly,
        OutputPath = @"C:\FolderWhereDocument-BootstrapIs\temp.html"
    };
    var response = new WriteOutputCommand(GetModelRequest()).Execute();
</code>
<pre>


