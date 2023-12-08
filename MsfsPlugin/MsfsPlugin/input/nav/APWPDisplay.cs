namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class APWPDisplay : DefaultInput
    {
        public APWPDisplay() : base("WP", "Display next WP data", "Nav")
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_NEXT_WP_ID));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_NEXT_WP_DIST));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_NEXT_WP_ETE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_NEXT_WP_HEADING));
        }
        protected override string GetValue() => "WP " + bindings[0].ControllerValue + "\n" + bindings[1].ControllerValue + " mn \n" + TimeSpan.FromSeconds(bindings[2].ControllerValue).ToString() + "\n" + bindings[3].ControllerValue + " °";
    }
}

