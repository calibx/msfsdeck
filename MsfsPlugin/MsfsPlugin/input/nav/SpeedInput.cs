namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class SpeedInput : PluginDynamicCommand, Notifiable
    {
        public SpeedInput() : base("Speed", "Display current and AP speed", "Nav") => MsfsData.Instance.register(this);

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.CurrentSpeed + "\n" + MsfsData.Instance.CurrentAPSpeed;
        }

        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentAPSpeed = (Int32)(Math.Round(MsfsData.Instance.CurrentSpeed / 100d, 0) * 100);


    }
}

