namespace Loupedeck.MsfsPlugin
{
    using System;

    class ThrottleEncoder : PluginDynamicAdjustment, Notifiable
    {

        public ThrottleEncoder() : base("Throttle", "Current throttle", "Misc", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            var newThrottle = MsfsData.Instance.CurrentThrottle + ticks * 1;
            if (MsfsData.Instance.ThrottleLowerFromMSFS < 0)
            {
                if (newThrottle <= -100)
                {
                    newThrottle = -100;
                }
                else if (newThrottle > 100)
                {
                    newThrottle = 100;
                }
            }
            else
            {
                if (newThrottle <= 0)
                {
                    newThrottle = 0;
                }
                else if (newThrottle > 100)
                {
                    newThrottle = 100;
                }
            }
            MsfsData.Instance.CurrentThrottle = (Int16)newThrottle;
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentThrottle = MsfsData.Instance.ThrottleLowerFromMSFS < 0 ? (Int16)(-100) : (Int16)0;
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.CurrentThrottle.ToString();
        }


        public void Notify() => this.AdjustmentValueChanged();
    }
}
