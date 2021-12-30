namespace Loupedeck.MsfsPlugin
{
    using System;

    class APWPDisplay : PluginDynamicCommand, Notifiable
    {
        public APWPDisplay() : base("WP", "Display new WP data", "AP")
        {
            MsfsData.Instance.register(this);
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            var timeSpan = TimeSpan.FromSeconds(MsfsData.Instance.ApNextWPETE);

            return "POI " + MsfsData.Instance.ApNextWPID + "\n" + Math.Round(MsfsData.Instance.ApNextWPDist) + " mn \n" + timeSpan.ToString() + "\n" + Math.Round(MsfsData.Instance.ApNextWPHeading) + " °";
        }

        public void Notify() => this.AdjustmentValueChanged();

    }
}

