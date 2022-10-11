namespace Loupedeck.MsfsPlugin
{
    using System;
    using Loupedeck.MsfsPlugin.encoder;
    using Loupedeck.MsfsPlugin.tools;

    class HeadignAPEncoder : DefaultEncoder
    {
        public HeadignAPEncoder() : base("Head", "Autopilot heading encoder", "AP", true, 0, 360, 1)
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_HEADING)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.HEADING)));
        }
        protected override void RunCommand(String actionParameter) => this.SetValue(this._bindings[1].ControllerValue);
        protected override String GetDisplayValue() => "[" + this._bindings[0].ControllerValue + "]\n" + this._bindings[1].ControllerValue;
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            this.SetValue(ConvertTool.ApplyAdjustment(this.GetValue(), ticks, this.min, this.max, this.step, true));
            this.ActionImageChanged();
        }

    }
}
