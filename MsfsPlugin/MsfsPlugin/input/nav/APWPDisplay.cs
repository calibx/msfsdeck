namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class APWPDisplay : DefaultInput
    {
        public APWPDisplay() : base("WP", "Display next WP data", "Nav")
        {
            wpId = Bind(BindingKeys.AP_NEXT_WP_ID);
            wpDist = Bind(BindingKeys.AP_NEXT_WP_DIST);
            wpETE = Bind(BindingKeys.AP_NEXT_WP_ETE);
            wpHdg = Bind(BindingKeys.AP_NEXT_WP_HEADING);
        }

        protected override string GetValue() =>
            "WP " + wpId.ControllerValue + "\n" +
            ConvertTool.Round(wpDist.ControllerValue / 10.0, 1) + " nm \n" +
            TimeSpan.FromSeconds(wpETE.ControllerValue).ToString() + "\n" +
            wpHdg.ControllerValue + " °";

        readonly Binding wpId;
        readonly Binding wpDist;
        readonly Binding wpETE;
        readonly Binding wpHdg;
    }
}

