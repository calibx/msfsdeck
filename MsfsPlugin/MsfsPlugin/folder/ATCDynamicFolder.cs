namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    public class ATCDynamicFolder : PluginDynamicFolder, Notifiable
    {
        public ATCDynamicFolder()
        {
            this.DisplayName = "ATC";
            this.GroupName = "Folder";
            this.Navigation = PluginDynamicFolderNavigation.EncoderArea;
            MsfsData.Instance.Register(this);

        }
        public override IEnumerable<String> GetButtonPressActionNames()
        {
            return new[]
            {
                this.CreateCommandName("Open/Close"),
                //this.CreateCommandName("Close"),
                this.CreateCommandName("0"),
                this.CreateCommandName("1"),
                this.CreateCommandName("2"),
                this.CreateCommandName("3"),
                this.CreateCommandName("4"),
                this.CreateCommandName("5"),
                this.CreateCommandName("6"),
                this.CreateCommandName("7"),
                this.CreateCommandName("8"),
                this.CreateCommandName("9")
            };
        }
        public override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) => actionParameter;
        public override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "Open/Close":
                    MsfsData.Instance.ATC = true;
                    break;
                case "Close":
                    MsfsData.Instance.ATCClose = true;
                    break;
                case "0":
                    MsfsData.Instance.ATC0 = true;
                    break;
                case "1":
                    MsfsData.Instance.ATC1 = true;
                    break;
                case "2":
                    MsfsData.Instance.ATC2 = true;
                    break;
                case "3":
                    MsfsData.Instance.ATC3 = true;
                    break;
                case "4":
                    MsfsData.Instance.ATC4 = true;
                    break;
                case "5":
                    MsfsData.Instance.ATC5 = true;
                    break;
                case "6":
                    MsfsData.Instance.ATC6 = true;
                    break;
                case "7":
                    MsfsData.Instance.ATC7 = true;
                    break;
                case "8":
                    MsfsData.Instance.ATC8 = true;
                    break;
                case "9":
                    MsfsData.Instance.ATC9 = true;
                    break;
            }
        }
        public void Notify()
        {
        }

    }
}
