namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class BarometerEncoder : PluginDynamicAdjustment, Notifiable
    {
        protected Double min;
        protected Double max;
        protected Double step;
        protected Binding _binding;

        public BarometerEncoder() : base("Baro", "Barometer encoder", "Nav", true) {
            this._binding = new Binding(BindingKeys.KOHLSMAN);
            MsfsData.Instance.Register(this._binding);
            MsfsData.Instance.Register(this);
            this.min = 27.99;
            this.max = 30.81;
            this.step = 0.01;
       }

        protected override void RunCommand(String actionParameter) => this._binding.SetControllerValue("1");
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            var value = Double.Parse(this._binding.ControllerValue);
            value += ticks * this.step;
            if (value < this.min)
            { value = this.min; }
            else if (value > this.max)
            { value = this.max; }
            this._binding.SetControllerValue(value.ToString());
            this.ActionImageChanged();
        }
        protected override String GetAdjustmentValue(String actionParameter) => this._binding.ControllerValue == null ? "0" : Math.Round(Double.Parse(this._binding.ControllerValue), 2).ToString();
        public void Notify()
        {
            if (this._binding != null && this._binding.Key != null)
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
