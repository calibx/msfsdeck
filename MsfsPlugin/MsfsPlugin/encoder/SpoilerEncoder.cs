namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class SpoilerEncoder : PluginDynamicAdjustment, Notifiable
    {
        public SpoilerEncoder() : base("Spoiler", "Spoiler position", "Misc", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            MsfsData.Instance.CurrentSpoiler += ticks;
            if (MsfsData.Instance.CurrentSpoiler < -1)
            { MsfsData.Instance.CurrentSpoiler = -1; }
            else if (MsfsData.Instance.CurrentSpoiler > 10)
            { MsfsData.Instance.CurrentSpoiler = 10; }
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentSpoiler = 0;
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {

            return MsfsData.Instance.CurrentSpoiler == -1 ? "Auto" : MsfsData.Instance.CurrentSpoiler.ToString();

        }
        public void Notify() => this.AdjustmentValueChanged();
    }
}
