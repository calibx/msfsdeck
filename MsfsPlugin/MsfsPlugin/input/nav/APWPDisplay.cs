namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class APWPDisplay : DefaultInput
    {
        public APWPDisplay() : base("WP", "Display next WP data", "Nav")
        {
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NEXT_WP_ID)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NEXT_WP_DIST)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NEXT_WP_ETE)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NEXT_WP_HEADING)));
        }
        protected override String GetValue() => "POI " + this.bindings[0].ControllerValue + "\n" + this.bindings[1].ControllerValue + " mn \n" + TimeSpan.FromSeconds(this.bindings[2].ControllerValue).ToString() + "\n" + this.bindings[3].ControllerValue + " °";
    }
}

