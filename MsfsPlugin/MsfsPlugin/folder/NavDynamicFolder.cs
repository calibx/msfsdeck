namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class NavDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly List<Binding> _bindings = new List<Binding>();   //>> Remove underscore
        public NavDynamicFolder()
        {
            DisplayName = "NAV";
            GroupName = "Folder";

            //>> These should have names and all references should use the names rather than index into _bindings
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_ACTIVE_FREQUENCY)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_ACTIVE_FREQUENCY)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_AVAILABLE)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_AVAILABLE)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_STBY_FREQUENCY)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_STBY_FREQUENCY)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_RADIO_SWAP)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_RADIO_SWAP)));

            MsfsData.Instance.Register(this);
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;

        public override IEnumerable<String> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("NAV1 Active Int"),
                CreateCommandName("NAV1 Active Float"),
                CreateCommandName("NAV1 Standby Int"),
                CreateCommandName("NAV1 Standby Float"),
                CreateCommandName("NAV2 Active Int"),
                CreateCommandName("NAV2 Active Float"),
                CreateCommandName("NAV2 Standby Int"),
                CreateCommandName("NAV2 Standby Float"),
                NavigateUpActionName,
            };
        }

        public override IEnumerable<String> GetEncoderRotateActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateAdjustmentName ("NAV1 Int Encoder"),
                CreateAdjustmentName ("NAV2 Int Encoder"),
                CreateAdjustmentName (""),
                CreateAdjustmentName ("NAV1 Float Encoder"),
                CreateAdjustmentName ("NAV2 Float Encoder"),
            };
        }

        public override IEnumerable<String> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("NAV1 Int Reset"),
                CreateCommandName("NAV2 Int Reset"),
                CreateCommandName (""),
                CreateCommandName("NAV1 Float Reset"),
                CreateCommandName("NAV2 Float Reset"),
            };
        }

        public override String GetAdjustmentDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "NAV1 Int Encoder":
                    ret = "NAV1\n" + Math.Truncate(_bindings[4].ControllerValue / 1000000f) + ".";
                    break;
                case "NAV1 Float Encoder":
                    var com1dbl = Math.Round(_bindings[4].ControllerValue / 1000000f - Math.Truncate(_bindings[4].ControllerValue / 1000000f), 3).ToString();
                    ret = "NAV1\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(2, '0');
                    break;
                case "NAV2 Int Encoder":
                    ret = "NAV2\n" + Math.Truncate(_bindings[5].ControllerValue / 1000000f) + ".";
                    break;
                case "NAV2 Float Encoder":
                    var com2dbl = Math.Round(_bindings[5].ControllerValue / 1000000f - Math.Truncate(_bindings[5].ControllerValue / 1000000f), 3).ToString();
                    ret = "NAV2\n" + (com2dbl.Length > 2 ? com2dbl.Substring(2) : com2dbl).PadRight(2, '0');
                    break;
            }
            return ret;
        }

        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "NAV1 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(_bindings[2].MsfsValue));
                    bitmapBuilder.DrawText((_bindings[0].ControllerValue == 0 ? "0" : _bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV1 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(_bindings[2].MsfsValue));
                    bitmapBuilder.DrawText(_bindings[0].ControllerValue == 0 ? "0" : _bindings[0].ControllerValue.ToString().Substring(3, 2), new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV1 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(_bindings[2].MsfsValue));
                    bitmapBuilder.DrawText((_bindings[4].ControllerValue == 0 ? "0" : _bindings[4].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV1 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(_bindings[2].MsfsValue));
                    bitmapBuilder.DrawText(_bindings[4].ControllerValue == 0 ? "0" : _bindings[4].ControllerValue.ToString().Substring(3, 2), new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV2 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(_bindings[3].MsfsValue));
                    bitmapBuilder.DrawText((_bindings[1].ControllerValue == 0 ? "0" : _bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV2 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(_bindings[3].MsfsValue));
                    bitmapBuilder.DrawText(_bindings[1].ControllerValue == 0 ? "0" : _bindings[1].ControllerValue.ToString().Substring(3, 2), new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV2 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(_bindings[3].MsfsValue));
                    bitmapBuilder.DrawText((_bindings[5].ControllerValue == 0 ? "0" : _bindings[5].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV2 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(_bindings[3].MsfsValue));
                    bitmapBuilder.DrawText(_bindings[5].ControllerValue == 0 ? "0" : _bindings[5].ControllerValue.ToString().Substring(3, 2), new BitmapColor(255, 255, 0), 40);
                    break;

            }
            return bitmapBuilder.ToImage();
        }

        public override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "NAV1 Active":
                case "NAV1 Active Int":
                case "NAV1 Active Float":
                case "NAV1 Standby":
                case "NAV1 Standby Int":
                case "NAV1 Standby Float":
                case "NAV1 Int Reset":
                case "NAV1 Float Reset":
                    _bindings[6].SetControllerValue(1);
                    break;
                case "NAV2 Active":
                case "NAV2 Active Int":
                case "NAV2 Active Float":
                case "NAV2 Standby":
                case "NAV2 Standby Int":
                case "NAV2 Standby Float":
                case "NAV2 Int Reset":
                case "NAV2 Float Reset":
                    _bindings[7].SetControllerValue(1);
                    break;
            }
        }

        public override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            switch (actionParameter)
            {
                case "NAV1 Int Encoder":
                    _bindings[4].SetControllerValue(IncrIntValue(_bindings[4].ControllerValue, ticks));
                    break;
                case "NAV1 Float Encoder":
                    _bindings[4].SetControllerValue(IncrDecimalValue(_bindings[4].ControllerValue, ticks));
                    break;
                case "NAV2 Int Encoder":
                    _bindings[5].SetControllerValue(IncrIntValue(_bindings[5].ControllerValue, ticks));
                    break;
                case "NAV2 Float Encoder":
                    _bindings[5].SetControllerValue(IncrDecimalValue(_bindings[5].ControllerValue, ticks));
                    break;
            }
            EncoderActionNamesChanged();
            ButtonActionNamesChanged();
        }

        Int64 IncrIntValue(Int64 presentValue, Int32 ticks)
        {
            var actualValue = presentValue / 10000;
            var intValue = actualValue / 100;       // Truncate decimal part away
            var decimals = actualValue % 100;

            return EncodeValues(ConvertTool.ApplyAdjustment(intValue, ticks, 108, 117, 1, true), decimals);
        }

        Int64 IncrDecimalValue(Int64 presentValue, Int32 ticks)
        {
            var actualValue = presentValue / 10000;
            var intValue = actualValue / 100;       // Truncate decimal part away
            var decimals = actualValue % 100;

            return intValue == 0 && decimals == 0
                ? 108000000    // Recover if an illegal value crept into the system
                : EncodeValues(intValue, ConvertTool.ApplyAdjustment(decimals, ticks, 0, 95, 5, true));
        }

        Int64 EncodeValues(Int64 intValue, Int64 decimalValue) =>
            (intValue * 100 + decimalValue) * 10000;

        public void Notify()
        {
            foreach (Binding binding in _bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    binding.Reset();
                }
            }
        }
    }
}
