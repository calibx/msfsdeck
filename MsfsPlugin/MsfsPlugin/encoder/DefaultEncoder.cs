namespace Loupedeck.MsfsPlugin.encoder
{
    using System;

    public abstract class DefaultEncoder : PluginDynamicAdjustment, Notifiable
    {
        protected Int32 min;
        protected Int32 max;
        protected Int32 step;
        public DefaultEncoder(String name, String desc, String category, Boolean resettable, Int32 min, Int32 max, Int32 step) : base(name, desc, category, resettable)
        {
            MsfsData.Instance.register(this);
            this.min = min;
            this.max = max;
            this.step = step;
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            var value = this.GetValue();
            value += ticks * this.step;
            if (value < this.min)
            { value = this.min; }
            else if (value > this.max)
            { value = this.max; }
            this.SetValue(value);
        }


        protected override String GetAdjustmentValue(String actionParameter) => this.GetValue().ToString();
        public void Notify() => this.AdjustmentValueChanged();

        protected abstract Int32 GetValue();
        protected abstract Int32 SetValue(Int32 value);
    }
}
