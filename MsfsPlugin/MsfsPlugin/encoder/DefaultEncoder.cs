namespace Loupedeck.MsfsPlugin.encoder
{
    using System;
    using System.Diagnostics;

    public abstract class DefaultEncoder : PluginDynamicAdjustment, Notifiable
    {
        protected Int32 min;
        protected Int32 max;
        protected Int32 step;
        private static int count;
        public DefaultEncoder(String name, String desc, String category, Boolean resettable, Int32 min, Int32 max, Int32 step) : base(name, desc, category, resettable)
        {
                //MsfsData.Instance.Register(this);
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
        protected override String GetAdjustmentValue(String actionParameter)
        {
            return this.GetDisplayValue();
        }
        public void Notify() => this.AdjustmentValueChanged();
        protected virtual String GetDisplayValue() => this.GetValue().ToString();
        protected virtual Int32 GetValue() => 0;
        protected abstract void SetValue(Int32 value);

        protected override bool OnUnload()
        {
            Debug.WriteLine("unload");
            return true;
        }

    }
}
