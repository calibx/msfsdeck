namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class APWPDisplay : DefaultInput
    {
        public APWPDisplay() : base("WP", "Display next WP data", "Nav") {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NEXT_WP_ID)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NEXT_WP_DIST)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NEXT_WP_ETE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NEXT_WP_HEADING)));
        }
        protected override String GetValue() => "POI " + this._bindings[0].ControllerValue + "\n" + this._bindings[1].ControllerValue + " mn \n" + TimeSpan.FromSeconds(this._bindings[2].ControllerValue).ToString() + "\n" + this._bindings[3].ControllerValue + " °";
    }
}

