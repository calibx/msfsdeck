namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ToggleMuteCommand : PluginDynamicCommand
    {
        public ToggleMuteCommand() : base("Toggle Mute", "Toggles audio mute state", "Audio")
        {
        }

        protected override void RunCommand(String actionParameter)
        {
            this.Plugin.ClientApplication.SendKeyboardShortcut(VirtualKeyCode.VolumeMute);
        }
    }
}
