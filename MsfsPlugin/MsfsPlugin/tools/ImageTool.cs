namespace Loupedeck.MsfsPlugin.tools
{
    using System;

    public class ImageTool
    {
        public static readonly BitmapImage _imageOff = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.off.png");

        public static readonly BitmapImage _imageOn = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.on.png");

        public static readonly BitmapImage _imageWait = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.wait.png");

        public static readonly BitmapImage _imageAvailable = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.available.png");

        public static readonly BitmapImage _imageDisable = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disable.png");

        public static readonly BitmapImage _imageDisconnect = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.disconnect.png");

        public static readonly BitmapImage _imageTrying = EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.trying.png");

        public static BitmapImage GetOnOffImage(Int64 value) => value == 1 ? _imageOn : _imageOff;
        public static BitmapImage GetAvailableDisableImage(Int64 value) => value == 1 ? _imageAvailable : _imageDisable;

        public static BitmapImage GetOnAvailableWaitDisableImage(Int64 value)
        {
            BitmapImage state;
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
            return state;
        } 
    }
}
