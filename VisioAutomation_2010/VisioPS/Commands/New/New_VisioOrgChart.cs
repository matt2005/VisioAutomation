using VA = VisioAutomation;
using SMA = System.Management.Automation;

namespace VisioPS.Commands
{
    [SMA.Cmdlet(SMA.VerbsCommon.New, "VisioOrgChart")]
    public class New_VisioOrgChart : VisioPS.VisioPSCmdlet
    {
        protected override void ProcessRecord()
        {
            var orgchart = new VA.Layout.Models.OrgChart.Drawing();
            this.WriteObject(orgchart);
        }
    }
}