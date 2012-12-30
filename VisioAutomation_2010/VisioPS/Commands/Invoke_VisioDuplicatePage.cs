using SMA = System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioPS.Commands
{
    [SMA.Cmdlet(SMA.VerbsLifecycle.Invoke, "VisioDuplicatePage")]
    public class Invoke_VisioDuplicatePage : VisioPS.VisioPSCmdlet
    {
        [SMA.Parameter(Position = 0, Mandatory = false)]
        public string Name = null;

        [SMA.Parameter(Position = 1, Mandatory = false)]
        public IVisio.Document ToDocument=null;

        protected override void ProcessRecord()
        {
            IVisio.Page newpage;
            var scriptingsession = this.ScriptingSession;
            if (this.ToDocument == null)
            {
                newpage = scriptingsession.Page.Duplicate();
            }
            else
            {
                newpage = scriptingsession.Page.Duplicate(this.ToDocument);
            }

            this.WriteObject(newpage);            
        }
    }
}