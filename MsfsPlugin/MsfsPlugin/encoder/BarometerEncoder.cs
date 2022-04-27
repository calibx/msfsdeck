namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class BarometerEncoder : PluginDynamicAdjustment, Notifiable
    {
        protected Int64 min;
        protected Int64 max;
        protected Int64 step;
        protected Binding _binding;

        public BarometerEncoder() : base("Baro", "Barometer encoder", "Nav", true) {
            this._binding = new Binding(BindingKeys.KOHLSMAN);
            MsfsData.Instance.Register(this._binding);
            MsfsData.Instance.Register(this);
            this.min = 2799;
            this.max = 3081;
            this.step = 1;
       }

        protected override void RunCommand(String actionParameter) => this._binding.SetControllerValue(1);
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            var value = this._binding.ControllerValue;
            value += ticks * this.step;
            if (value < this.min)
            { value = this.min; }
            else if (value > this.max)
            { value = this.max; }
            this._binding.SetControllerValue(value);
            this.ActionImageChanged();
        }
        protected override String GetAdjustmentValue(String actionParameter) => this._binding.ControllerValue == null ? "0" : (this._binding.ControllerValue / 100).ToString();
        public void Notify()
        {
            if (this._binding != null)
            {
                if (this._binding.HasMSFSChanged())
                {
                    this._binding.Reset();
                    this.AdjustmentValueChanged();
                }
            }
            else
            {
                this.ActionImageChanged();
            }
        }
    }
}
