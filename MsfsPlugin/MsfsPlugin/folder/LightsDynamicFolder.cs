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
        this.CreateCommandName("7"),
        this.CreateCommandName("8"),
        this.CreateCommandName("9"),
        this.CreateCommandName("."),
        this.CreateCommandName("4"),
        this.CreateCommandName("5"),
        this.CreateCommandName("6"),
        this.CreateCommandName("0"),
        this.CreateCommandName("1"),
        this.CreateCommandName("2"),
        this.CreateCommandName("3")
    };
        }
    }

}
