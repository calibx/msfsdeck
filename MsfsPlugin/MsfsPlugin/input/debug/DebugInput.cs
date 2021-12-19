namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DebugInput : PluginDynamicCommand, Notifiable
    {
  
        public DebugInput() : base("Debug", "Display debugged value ", "Debug")

        {
            MsfsData.Instance.ValuesDisplayed = true;
            MsfsData.Instance.register(this);
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {

            return MsfsData.Instance.DebugValue + "\nDebug";
        }

        protected override void RunCommand(String actionParameter)
        {
        }
    }
}

