namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class FPSInput : PluginDynamicCommand, Notifiable
    {
        public FPSInput() : base("FPS", "Display current FPS", "Debug")

        {
            MsfsData.Instance.register(this);
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            return "\u001b[31m" + MsfsData.Instance.fps + "\nFPS\u001b[0m33";
        }

        protected override void RunCommand(String actionParameter)
        {
        }
    }
}

