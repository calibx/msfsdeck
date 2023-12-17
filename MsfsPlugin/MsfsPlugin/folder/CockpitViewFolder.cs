namespace Loupedeck.MsfsPlugin.folder
{
    using System.Collections.Generic;
    using Loupedeck.MsfsPlugin.tools;

    internal class CockpitViewFolder : PluginDynamicFolder
    {
        public CockpitViewFolder()
        {
            DisplayName = "Cockpit Views";
            GroupName = "Folder";

            InSetMode = false;
            InCustomMode = false;
            SetModifierKey();
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.EncoderArea;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType) => new[]
            {
                CreateCommandName("1"),
                CreateCommandName("2"),
                CreateCommandName("3"),
                CreateCommandName("4"),
                CreateCommandName("5"),
                CreateCommandName("6"),
                CreateCommandName("7"),
                CreateCommandName("8"),
                CreateCommandName("9"),
                CreateCommandName("0"),
                CreateCommandName(FixedCustom),
                CreateCommandName(SetMode),
            };

        public override string GetCommandDisplayName(string actionParameter, PluginImageSize imageSize) => actionParameter;

        public override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            if (actionParameter == FixedCustom)
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    bitmapBuilder.Translate(0, -15);
                    bitmapBuilder.DrawText("Fixed", GetTextColor(!InCustomMode), 20);
                    bitmapBuilder.Translate(0, 25);
                    bitmapBuilder.DrawText("Custom", GetTextColor(InCustomMode), 20);
                    return bitmapBuilder.ToImage();
                }
            }
            else if (actionParameter == SetMode)
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    bitmapBuilder.DrawText("Set mode", GetTextColor(InSetMode), 18);
                    return bitmapBuilder.ToImage();
                }
            }
            else
            {
                return null;
            }
        }

        public override void RunCommand(string actionParameter)
        {
            switch (actionParameter)
            {
                case "0":
                    SendKey(VirtualKeyCode.Key0);
                    break;
                case "1":
                    SendKey(VirtualKeyCode.Key1);
                    break;
                case "2":
                    SendKey(VirtualKeyCode.Key2);
                    break;
                case "3":
                    SendKey(VirtualKeyCode.Key3);
                    break;
                case "4":
                    SendKey(VirtualKeyCode.Key4);
                    break;
                case "5":
                    SendKey(VirtualKeyCode.Key5);
                    break;
                case "6":
                    SendKey(VirtualKeyCode.Key6);
                    break;
                case "7":
                    SendKey(VirtualKeyCode.Key7);
                    break;
                case "8":
                    SendKey(VirtualKeyCode.Key8);
                    break;
                case "9":
                    SendKey(VirtualKeyCode.Key9);
                    break;
                case FixedCustom:
                    InCustomMode = !InCustomMode;
                    InSetMode = false;
                    SetModifierKey();
                    break;
                case SetMode:
                    InSetMode = !InSetMode;
                    SetModifierKey();
                    break;
            }
        }

        void SendKey(VirtualKeyCode key) => Plugin.KeyboardApi.SendShortcut(key, CurrentModifier);

        BitmapColor GetTextColor(bool emphasize) => emphasize ? ImageTool.Green : ImageTool.Grey;

        void SetModifierKey() => CurrentModifier =
            InSetMode
            ? ModifierKey.Control | ModifierKey.Alt
            : (InCustomMode ? ModifierKey.Alt : ModifierKey.Control);

        ModifierKey CurrentModifier { get; set; }

        bool InCustomMode { get; set; }
        bool InSetMode { get; set; }

        const string FixedCustom = "Fixed/Custom";
        const string SetMode = "Set mode";
    }
}
