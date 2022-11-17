namespace Loupedeck.MsfsPlugin.tools
{
    using System;

    public class ImageTool
    {
        public static BitmapImage _imageOff = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.off.png");

        public static BitmapImage _imageOn = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.on.png");

        public static BitmapImage _imageWait = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.wait.png");

        public static BitmapImage _imageAvailable = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.available.png");

        public static BitmapImage _imageAvailableBorder = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.availableBorder.png");

        public static BitmapImage _imageDisable = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disable.png");

        public static BitmapImage _imageDisableBorder = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disable.png");

        public static BitmapImage _imageDisconnect = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disconnect.png");

        public static BitmapImage _imageTrying = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.trying.png");

        public static BitmapImage GetOnOffImage(Int64 value) => ImageTool.IsConnected() ? value == 1 ? _imageOn : _imageOff : ImageTool.IsTryingToConnect() ? _imageTrying : _imageDisconnect;
        public static BitmapImage GetAvailableDisableImage(Int64 value) => ImageTool.IsConnected() ? value == 1 ? _imageAvailableBorder : _imageDisableBorder : ImageTool.IsTryingToConnect() ? _imageTrying : _imageDisconnect;

        public static BitmapImage GetOnAvailableWaitDisableImage(Int64 value)
        {
            BitmapImage state;
            if (ImageTool.IsTryingToConnect()) {
                state = _imageTrying;
            } else if (!ImageTool.IsConnected())
            {
                state = _imageDisconnect;
            } else
            { 
                switch (value)
                {
                    case 1:
                        state = ImageTool._imageAvailable;
                        break;
                    case 2:
                        state = ImageTool._imageOn;
                        break;
                    case 3:
                        state = ImageTool._imageWait;
                        break;
                    case 4:
                        state = ImageTool._imageOff;
                        break;
                    default:
                        state = ImageTool._imageDisable;
                        break;
                }
            }
            return state;
        }

        private static Boolean IsConnected() => MsfsData.Instance.bindings[BindingKeys.CONNECTION].MsfsValue == 1;
        private static Boolean IsTryingToConnect() => MsfsData.Instance.bindings[BindingKeys.CONNECTION].MsfsValue == 2;

        public static void Refresh()
        {
            _imageOff = MsfsData.Instance.bindings[BindingKeys.CONNECTION].ControllerValue == 1 ? EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.offFull.png") : EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.off.png");
            _imageOn = MsfsData.Instance.bindings[BindingKeys.CONNECTION].ControllerValue == 1 ? EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.onFull.png") : EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.on.png");
            _imageWait = MsfsData.Instance.bindings[BindingKeys.CONNECTION].ControllerValue == 1 ? EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.waitFull.png") : EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.wait.png");
            _imageAvailable = MsfsData.Instance.bindings[BindingKeys.CONNECTION].ControllerValue == 1 ? EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.availableFull.png") : EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.available.png");
            _imageDisable = MsfsData.Instance.bindings[BindingKeys.CONNECTION].ControllerValue == 1 ? EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disableFull.png") : EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disable.png");
            _imageDisconnect = MsfsData.Instance.bindings[BindingKeys.CONNECTION].ControllerValue == 1 ? EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disconnectFull.png") : EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disconnect.png");
            _imageTrying = MsfsData.Instance.bindings[BindingKeys.CONNECTION].ControllerValue == 1 ? EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.tryingFull.png") : EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.trying.png");
        }
}
}
