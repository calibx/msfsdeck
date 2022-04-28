namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class APWPDisplay : DefaultInput
    {
        public APWPDisplay() : base("WP", "Display next WP data", "Nav") { }
        protected override String GetValue() => "POI " + MsfsData.Instance.ApNextWPID + "\n" + Math.Round(MsfsData.Instance.ApNextWPDist, 1) + " mn \n" + TimeSpan.FromSeconds(MsfsData.Instance.ApNextWPETE).ToString() + "\n" + Math.Round(MsfsData.Instance.ApNextWPHeading) + " °";
    }
}

