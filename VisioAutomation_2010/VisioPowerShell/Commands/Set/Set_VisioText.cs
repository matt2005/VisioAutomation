using System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioPowerShell.Commands.Set
{
    [Cmdlet(VerbsCommon.Set, "VisioText")]
    public class Set_VisioText : VisioCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string[] Text { get; set; }

        [Parameter(Mandatory = false)]
        public IVisio.Shape[] Shapes;

        protected override void ProcessRecord()
        {
            this.client.Text.Set(this.Shapes, this.Text);
        }
    }
}
