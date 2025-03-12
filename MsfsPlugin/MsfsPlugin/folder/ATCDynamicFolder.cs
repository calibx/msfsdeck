namespace Loupedeck.MsfsPlugin.folder
{
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.msfs;

    public class ATCDynamicFolder : DefaultFolder
    {
        public ATCDynamicFolder() : base("ATC")
        {
            OpenClose = Bind(BindingKeys.ATC_ATC_FOLDER);
            Choice1 = Bind(BindingKeys.ATC_1_ATC_FOLDER);
            Choice2 = Bind(BindingKeys.ATC_2_ATC_FOLDER);
            Choice3 = Bind(BindingKeys.ATC_3_ATC_FOLDER);
            Choice4 = Bind(BindingKeys.ATC_4_ATC_FOLDER);
            Choice5 = Bind(BindingKeys.ATC_5_ATC_FOLDER);
            Choice6 = Bind(BindingKeys.ATC_6_ATC_FOLDER);
            Choice7 = Bind(BindingKeys.ATC_7_ATC_FOLDER);
            Choice8 = Bind(BindingKeys.ATC_8_ATC_FOLDER);
            Choice9 = Bind(BindingKeys.ATC_9_ATC_FOLDER);
            Choice0 = Bind(BindingKeys.ATC_0_ATC_FOLDER);
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.EncoderArea;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("Open/Close"),
                CreateCommandName("1"),
                CreateCommandName("2"),
                CreateCommandName("3"),
                CreateCommandName("4"),
                CreateCommandName("5"),
                CreateCommandName("6"),
                CreateCommandName("7"),
                CreateCommandName("8"),
                CreateCommandName("9"),
                CreateCommandName("0")
            };
        }

        public override string GetCommandDisplayName(string actionParameter, PluginImageSize imageSize) => actionParameter;

        public override void RunCommand(string actionParameter)
        {
            switch (actionParameter)
            {
                case "Open/Close":
                    OpenClose.SetControllerValue(1);
                    break;
                case "1":
                    Choice1.SetControllerValue(1);
                    break;
                case "2":
                    Choice2.SetControllerValue(1);
                    break;
                case "3":
                    Choice3.SetControllerValue(1);
                    break;
                case "4":
                    Choice4.SetControllerValue(1);
                    break;
                case "5":
                    Choice5.SetControllerValue(1);
                    break;
                case "6":
                    Choice6.SetControllerValue(1);
                    break;
                case "7":
                    Choice7.SetControllerValue(1);
                    break;
                case "8":
                    Choice8.SetControllerValue(1);
                    break;
                case "9":
                    Choice9.SetControllerValue(1);
                    break;
                case "0":
                    Choice0.SetControllerValue(1);
                    break;
            }
        }

        readonly Binding OpenClose;
        readonly Binding Choice1;
        readonly Binding Choice2;
        readonly Binding Choice3;
        readonly Binding Choice4;
        readonly Binding Choice5;
        readonly Binding Choice6;
        readonly Binding Choice7;
        readonly Binding Choice8;
        readonly Binding Choice9;
        readonly Binding Choice0;
    }
}
