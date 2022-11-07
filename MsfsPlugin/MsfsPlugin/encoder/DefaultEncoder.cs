namespace Loupedeck.MsfsPlugin.encoder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

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
            this.SetValue(ConvertTool.ApplyAdjustment(this.GetValue(), ticks, this.min, this.max, this.step));
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
                    binding.Reset();
                }
            }
        }
        protected virtual String GetDisplayValue() => this.GetValue().ToString();
        protected virtual Int64 GetValue() => 0;
        protected abstract void SetValue(Int64 value);
    }
}
