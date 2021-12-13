namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class SpoilerEncoder : PluginDynamicAdjustment, Notifiable
    {
        private Int32 value;

        public SpoilerEncoder() : base("Spoiler", "Spoiler position", "Misc", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            this.value = +ticks;
            if (this.value < 0)
            { this.value = 0; } 
            else if (this.value > 100)
            { this.value = 100; }
            MsfsData.Instance.CurrentSpoiler = this.value;
        }
        protected override void RunCommand(String actionParameter)
        {
            this.value = 1;
            MsfsData.Instance.CurrentSpoiler = this.value;
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {
            var state = "Off";
            if (MsfsData.Instance.CurrentSpoiler == 1)
            { state = "Auto"; } else
            { state = MsfsData.Instance.CurrentSpoiler.ToString(); }
            return state;
        }
        public void Notify() => this.AdjustmentValueChanged();
    }
}
