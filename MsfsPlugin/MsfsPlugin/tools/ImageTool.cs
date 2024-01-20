namespace Loupedeck.MsfsPlugin.tools
{
    public static class ImageTool
    {
        static BitmapImage imageOff = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.off.png");

        static BitmapImage imageOn = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.on.png");

        static BitmapImage imageWait = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.wait.png");

        static BitmapImage imageAvailable = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.available.png");

        static BitmapImage imageDisable = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disable.png");

        static BitmapImage imageDisconnect = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disconnect.png");

        static BitmapImage imageTrying = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.trying.png");

        static readonly BitmapImage imageAvailableBorder = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.availableBorder.png");

        static readonly BitmapImage imageDisableBorder = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disableBorder.png");

        public static BitmapImage GetOnOffImage(long value) =>
            IsConnected()
            ? (value == 1 ? imageOn : imageOff)
            : IsTryingToConnect() ? imageTrying : imageDisconnect;

        public static BitmapImage GetAvailableDisableImage(long value) =>
            IsConnected()
            ? (value == 1 ? imageAvailableBorder : imageDisableBorder)
            : (IsTryingToConnect() ? imageTrying : imageDisconnect);

        public static BitmapImage GetOnAvailableWaitDisableImage(long value)
        {
            BitmapImage state;
            if (IsTryingToConnect())
            {
                state = imageTrying;
            }
            else if (!IsConnected())
            {
                state = imageDisconnect;
            }
            else
            {
                switch (value)
                {
                    case 1:
                        state = imageAvailable;
                        break;
                    case 2:
                        state = imageOn;
                        break;
                    case 3:
                        state = imageWait;
                        break;
                    case 4:
                        state = imageOff;
                        break;
                    default:
                        state = imageDisable;
                        break;
                }
            }
            return state;
        }

        public static void Refresh(bool toFull)
        {
            imageOff = GetImage("off", toFull);
            imageOn = GetImage("on", toFull);
            imageWait = GetImage("wait", toFull);
            imageAvailable = GetImage("available", toFull);
            imageDisable = GetImage("disable", toFull);
            imageDisconnect = GetImage("disconnect", toFull);
            imageTrying = GetImage("trying", toFull);

            if (!IsConnected())
            {
                MsfsData.Instance.Changed();
            }
        }

        public static BitmapColor Red = new BitmapColor(255, 0, 0);
        public static BitmapColor Green = new BitmapColor(0, 255, 0);
        public static BitmapColor Blue = new BitmapColor(0, 0, 255);
        public static BitmapColor Yellow = new BitmapColor(255, 255, 0);
        public static BitmapColor Grey = new BitmapColor(128, 128, 128);
        public static BitmapColor LightGrey = new BitmapColor(192, 192, 192);

        private static BitmapImage GetImage(string name, bool full) => EmbeddedResources.ReadImage($"Loupedeck.MsfsPlugin.Resources.{name}{(full ? "Full" : "")}.png");

        private static bool IsConnected() => MsfsData.Instance.bindings[BindingKeys.CONNECTION].MsfsValue == 1;
        private static bool IsTryingToConnect() => MsfsData.Instance.bindings[BindingKeys.CONNECTION].MsfsValue == 2;
    }
}
