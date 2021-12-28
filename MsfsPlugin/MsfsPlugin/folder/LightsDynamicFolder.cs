namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LightsDynamicFolder : PluginDynamicFolder
    {
        public LightsDynamicFolder()
        {
            this.DisplayName = "Lights";
            this.GroupName = "System";
            this.Navigation = PluginDynamicFolderNavigation.EncoderArea;

        }
        public override IEnumerable<String> GetButtonPressActionNames()
        {
            return new[]
            {
        PluginDynamicFolder.NavigateUpActionName,
        this.CreateCommandName("Navigation"),
        this.CreateCommandName("Beacon"),
        this.CreateCommandName("Landing"),
        this.CreateCommandName("Taxi"),
        this.CreateCommandName("Strobes"),
        this.CreateCommandName("Instruments"),
        this.CreateCommandName("Recognition"),
        this.CreateCommandName("Wing"),
        this.CreateCommandName("Logo"),
        this.CreateCommandName("Cabin")
    };
        }
    }
}
