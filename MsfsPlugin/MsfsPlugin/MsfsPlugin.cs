namespace Loupedeck.MsfsPlugin
{
    using System.Reflection;
    using System.Runtime.Loader;

    using Loupedeck.MsfsPlugin.msfs;

    // This class contains the plugin-level logic of the Loupedeck plugin.

    public class MsfsPlugin : Plugin
    {
        // Gets a value indicating whether this is an API-only plugin.
        public override bool UsesApplicationApiOnly => true;

        // Gets a value indicating whether this is a Universal plugin or an Application plugin.
        public override bool HasNoApplication => true;

        // Initializes a new instance of the plugin class.
        public MsfsPlugin()
        {
            // Initialize the plugin log.
            PluginLog.Init(Log);

            // Initialize the plugin resources.
            PluginResources.Init(Assembly);
        }

        // This method is called when the plugin is loaded.
        public override void Load()
        {
            Info.Icon16x16 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.16.png");
            Info.Icon32x32 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.32.png");
            Info.Icon48x48 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.48.png");
            Info.Icon256x256 = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.256.png");
            MsfsData.Instance.plugin = this;
            DataTransferOut.loadEvents();
        }

        // This method is called when the plugin is unloaded.
        public override void Unload()
        {
            SimConnectWrapper.Instance.Disconnect();
        }
    }
}
