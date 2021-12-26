namespace Loupedeck.MsfsPlugin.encoder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class DefaultEncoder : PluginDynamicAdjustment, Notifiable
    {
        private readonly Int32 min;
        private readonly Int32 max;
        private readonly Int32 step;
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
            value += ticks*this.step;
            if (value < this.min)
            { value = this.min; }
            else if (value > this.max)
            { value = this.max; }
            this.SetValue(value);
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentZoom = 0;
        }

        protected override String GetAdjustmentValue(String actionParameter) => this.GetValue().ToString();
        public void Notify() => this.AdjustmentValueChanged();

        protected abstract Int32 GetValue();
        protected abstract Int32 SetValue(Int32 value);
    }
}
