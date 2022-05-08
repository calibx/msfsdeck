namespace Loupedeck.MsfsPlugin.encoder
{
    using System;
    using System.Collections.Generic;

    public abstract class DefaultEncoder : PluginDynamicAdjustment, Notifiable
    {
        protected Int32 min;
        protected Int32 max;
        protected Int32 step;
        protected readonly List<Binding> _bindings = new List<Binding>();

        public DefaultEncoder(String name, String desc, String category, Boolean resettable, Int32 min, Int32 max, Int32 step) : base(name, desc, category, resettable)
        {
            this.min = min;
            this.max = max;
            this.step = step;
            MsfsData.Instance.Register(this);
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
            this.ActionImageChanged();
        }
        protected override String GetAdjustmentValue(String actionParameter)
        {
            return this.GetDisplayValue();
        }
        public void Notify()
        {
            foreach (Binding binding in this._bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    MsfsData.Instance.refreshLimiter++;
                    binding.Reset();
                    this.AdjustmentValueChanged();
                }
            }
        }
        protected virtual String GetDisplayValue() => this.GetValue().ToString();
        protected virtual Int64 GetValue() => 0;
        protected abstract void SetValue(Int64 value);
    }
}
