namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.msfs;
    public class MSFSPlugin : Plugin
    {
        public override void Load()
        {
            Info.Icon16x16 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.16.png");
            Info.Icon32x32 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.32.png");
            Info.Icon48x48 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.48.png");
            Info.Icon256x256 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.256.png");
            MsfsData.Instance.plugin = this;
        }

        public override void Unload()
        {
            SimConnectDAO.Instance.Disconnect(true);
        }


        public override void RunCommand(string commandName, string parameter)
        { }

        public override void ApplyAdjustment(string adjustmentName, string parameter, int diff)
        { }

        public override bool UsesApplicationApiOnly => true;
    }
}
