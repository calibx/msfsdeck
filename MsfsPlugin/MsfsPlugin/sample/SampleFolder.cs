namespace Loupedeck.MsfsPlugin.sample
{
    using System;
    using System.Collections.Generic;


    public class SampleFolder : PluginDynamicFolder
    {
        private bool state;
        public SampleFolder()
        {
            this.DisplayName = "SampleFolder";
            this.GroupName = "Sample";
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.EncoderArea;
        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[] 
            {
                this.CreateCommandName("AutoChangeImage"),
                this.CreateCommandName("ChangeOnClick"),
            };
        }

        public override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            PluginLog.Info("GetCommandDisplayName " + actionParameter + " state " + state);
            return state.ToString();
        }
        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            PluginLog.Info("GetCommandImage " + actionParameter + " state " + state );
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                switch (actionParameter)
                {
                    case "AutoChangeImage":
                    case "ChangeOnClick":
                        if (state)
                        {
                            bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.on.png"));
                        } 
                        else
                        {
                            bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage("Loupedeck.MsfsPlugin.Resources.off.png"));
                        }
                        bitmapBuilder.DrawText(state.ToString());
                        break;

                }
                return null;
                //return bitmapBuilder.ToImage();
            }
        }
        public override void RunCommand(String actionParameter)
        {
            state = !state;
            //ButtonActionNamesChanged(); //=> no need to manual call
        }
    }
}
