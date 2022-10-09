namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.msfs;
    public class MSFSPlugin : Plugin
    {
        public override void Load()
        {
            this.Info.Icon16x16 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.16.png");
            this.Info.Icon32x32 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.32.png");
            this.Info.Icon48x48 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.48.png");
            this.Info.Icon256x256 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.256.png");
        }

        public override void Unload()
        {
            SimConnectDAO.Instance.Disconnect();
        }


        public override void RunCommand(String commandName, String parameter)
        {

        }

        public override void ApplyAdjustment(String adjustmentName, String parameter, Int32 diff)
        {
        }

    }
}
