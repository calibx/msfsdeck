namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class CounterAdjustment : PluginDynamicAdjustment
    {
        private Int32 _counter = 0;
        public CounterAdjustment() : base("Counter", "Counts rotation ticks", "Tools", true)
        {
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            this._counter += ticks; // increase or decrease counter on the number of ticks
           this.AdjustmentValueChanged(); // adjustment value has changed
        }
        protected override void RunCommand(String actionParameter)
        {
            this._counter = 0; // reset counter to 0
            this.AdjustmentValueChanged(); // adjustment value has changed
        }
        protected override String GetAdjustmentValue(String actionParameter)
        {
            return this._counter.ToString();
        }
    }
}
