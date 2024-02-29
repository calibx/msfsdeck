namespace Loupedeck.MsfsPlugin.folder
{
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.msfs;

    public class ATCDynamicFolder : DefaultFolder
    {
        public ATCDynamicFolder() : base("ATC")
        {
            bindings.Add(Register(BindingKeys.ATC_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_0_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_1_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_2_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_3_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_4_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_5_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_6_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_7_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_8_ATC_FOLDER));
            bindings.Add(Register(BindingKeys.ATC_9_ATC_FOLDER));
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.EncoderArea;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("Open/Close"),
                CreateCommandName("0"),
                CreateCommandName("1"),
                CreateCommandName("2"),
                CreateCommandName("3"),
                CreateCommandName("4"),
                CreateCommandName("5"),
                CreateCommandName("6"),
                CreateCommandName("7"),
                CreateCommandName("8"),
                CreateCommandName("9")
            };
        }

        public override string GetCommandDisplayName(string actionParameter, PluginImageSize imageSize) => actionParameter;

        public override void RunCommand(string actionParameter)
        {
            SimConnectDAO.Instance.setPlugin(Plugin);
            switch (actionParameter)
            {
                case "Open/Close":
                    bindings[0].SetControllerValue(1);
                    break;
                case "0":
                    bindings[1].SetControllerValue(1);
                    break;
                case "1":
                    bindings[2].SetControllerValue(1);
                    break;
                case "2":
                    bindings[3].SetControllerValue(1);
                    break;
                case "3":
                    bindings[4].SetControllerValue(1);
                    break;
                case "4":
                    bindings[5].SetControllerValue(1);
                    break;
                case "5":
                    bindings[6].SetControllerValue(1);
                    break;
                case "6":
                    bindings[7].SetControllerValue(1);
                    break;
                case "7":
                    bindings[8].SetControllerValue(1);
                    break;
                case "8":
                    bindings[9].SetControllerValue(1);
                    break;
                case "9":
                    bindings[10].SetControllerValue(1);
                    break;
            }
        }
    }
}
