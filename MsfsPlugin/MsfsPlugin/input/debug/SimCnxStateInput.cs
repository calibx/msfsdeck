namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    class SimCnxStateInput : DefaultInput
    {
        public SimCnxStateInput() : base("ConnectionSimConnect", "Display SimConnect connection state", "Debug")
        {
            bindings.Add(binding = Register(BindingKeys.CONNECTION));
        }

        protected override string GetValue() => binding.MsfsValue == 1 ? "connected" : binding.MsfsValue == 2 ? "trying to\nconnect" : "not\nconnected";

        protected override void RunCommand(string actionParameter)
        {
            var curValue = bindings[0].MsfsValue;
            if ( curValue == 1 || curValue == 2)
                SimConnectDAO.Instance.Disconnect();
            else
                SimConnectDAO.Instance.Connect();
        }

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(binding.MsfsValue));
                bitmapBuilder.DrawText(GetValue());
                return bitmapBuilder.ToImage();
            }
        }

        readonly Binding binding;
    }
}
