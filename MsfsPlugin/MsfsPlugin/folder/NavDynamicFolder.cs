namespace Loupedeck.MsfsPlugin.folder
{
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class NavDynamicFolder : PluginDynamicFolder, INotifiable
    {
        public NavDynamicFolder()
        {
            DisplayName = "NAV";
            GroupName = "Folder";

            bindings.Add(nav1ActiveFreq = MsfsData.Instance.Register(BindingKeys.NAV1_ACTIVE_FREQUENCY));
            bindings.Add(nav2ActiveFreq = MsfsData.Instance.Register(BindingKeys.NAV2_ACTIVE_FREQUENCY));
            bindings.Add(nav1Available = MsfsData.Instance.Register(BindingKeys.NAV1_AVAILABLE));
            bindings.Add(nav2Available = MsfsData.Instance.Register(BindingKeys.NAV2_AVAILABLE));
            bindings.Add(nav1StandbyFreq = MsfsData.Instance.Register(BindingKeys.NAV1_STBY_FREQUENCY));
            bindings.Add(nav2StandbyFreq = MsfsData.Instance.Register(BindingKeys.NAV2_STBY_FREQUENCY));
            bindings.Add(nav1Swap = MsfsData.Instance.Register(BindingKeys.NAV1_RADIO_SWAP));
            bindings.Add(nav2Swap = MsfsData.Instance.Register(BindingKeys.NAV2_RADIO_SWAP));

            MsfsData.Instance.Register(this);
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            //DebugTracing.Trace($"deviceType '{deviceType}'");
            return new[]
            {
                CreateCommandName(Nav1ActIntAction),
                CreateCommandName(Nav1ActDecAction),
                CreateCommandName(Nav1StdbyIntAction),
                CreateCommandName(Nav1StdbyDecAction),
                CreateCommandName(Nav2ActIntAction),
                CreateCommandName(Nav2ActDecAction),
                CreateCommandName(Nav2StdbyIntAction),
                CreateCommandName(Nav2StdbyDecAction),
            };
        }

        public override IEnumerable<string> GetEncoderPressActionNames(DeviceType deviceType)
        {
            //DebugTracing.Trace($"deviceType '{deviceType}'");
            var nav1Swap = CreateCommandName(Nav1Swap);
            var nav2Swap = CreateCommandName(Nav2Swap);
            return new[]
            {
                nav1Swap,
                nav2Swap,
                NavigateUpActionName,
                nav1Swap,
                nav2Swap,
                //>> ADF swap here
            };
        }

        public override IEnumerable<string> GetEncoderRotateActionNames(DeviceType deviceType)
        {
            //DebugTracing.Trace($"deviceType '{deviceType}'");
            return new[]
            {
                CreateAdjustmentName(Nav1IntEncAction),
                CreateAdjustmentName(Nav2IntEncAction),
                CreateAdjustmentName(NoAction),
                CreateAdjustmentName(Nav1DecEncAction),
                CreateAdjustmentName(Nav2DecEncAction),
            };
        }

        public override string GetAdjustmentDisplayName(string actionParameter, PluginImageSize imageSize)
        {
            //DebugTracing.Trace($"actionParameter '{actionParameter}', imageSize '{imageSize}'");
            switch (actionParameter)
            {
                // In this implementation we actually return explanation of the encoder button press, because
                // the effect of using the encoder rotation is self-explanatory.
                case Nav1IntEncAction:
                    return "NAV1\nActive\n<-->";
                case Nav1DecEncAction:
                    return "NAV1\nStandby\n<-->";
                case Nav2IntEncAction:
                    return "NAV2\nActive\n<-->";
                case Nav2DecEncAction:
                    return "NAV2\nStandby\n<-->";
            }
            return NoAction;
        }

        public override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            //DebugTracing.Trace($"actionParameter '{actionParameter}', imageSize '{imageSize}'");
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                switch (actionParameter)
                {
                    case Nav1ActIntAction:
                        SetBackgroundImage(bitmapBuilder, nav1Available);
                        bitmapBuilder.DrawText(ControllerIntValueText(nav1ActiveFreq), ImageTool.Green, 40);
                        break;
                    case Nav1ActDecAction:
                        SetBackgroundImage(bitmapBuilder, nav1Available);
                        bitmapBuilder.DrawText(ControllerDecValueText(nav1ActiveFreq), ImageTool.Green, 40);
                        break;
                    case Nav1StdbyIntAction:
                        SetBackgroundImage(bitmapBuilder, nav1Available);
                        bitmapBuilder.DrawText(ControllerIntValueText(nav1StandbyFreq), ImageTool.Yellow, 40);
                        break;
                    case Nav1StdbyDecAction:
                        SetBackgroundImage(bitmapBuilder, nav1Available);
                        bitmapBuilder.DrawText(ControllerDecValueText(nav1StandbyFreq), ImageTool.Yellow, 40);
                        break;
                    case Nav2ActIntAction:
                        SetBackgroundImage(bitmapBuilder, nav2Available);
                        bitmapBuilder.DrawText(ControllerIntValueText(nav2ActiveFreq), ImageTool.Green, 40);
                        break;
                    case Nav2ActDecAction:
                        SetBackgroundImage(bitmapBuilder, nav2Available);
                        bitmapBuilder.DrawText(ControllerDecValueText(nav2ActiveFreq), ImageTool.Green, 40);
                        break;
                    case Nav2StdbyIntAction:
                        SetBackgroundImage(bitmapBuilder, nav2Available);
                        bitmapBuilder.DrawText(ControllerIntValueText(nav2StandbyFreq), ImageTool.Yellow, 40);
                        break;
                    case Nav2StdbyDecAction:
                        SetBackgroundImage(bitmapBuilder, nav2Available);
                        bitmapBuilder.DrawText(ControllerDecValueText(nav2StandbyFreq), ImageTool.Yellow, 40);
                        break;
                }
                return bitmapBuilder.ToImage();
            }
        }

        public override void RunCommand(string actionParameter)
        {
            DebugTracing.Trace($"actionParameter '{actionParameter}'");
            switch (actionParameter)
            {
                case Nav1ActIntAction:
                case Nav1ActDecAction:
                case Nav1StdbyIntAction:
                case Nav1StdbyDecAction:
                case Nav1Swap:
                    nav1Swap.SetControllerValue(1);
                    break;
                case Nav2ActIntAction:
                case Nav2ActDecAction:
                case Nav2StdbyIntAction:
                case Nav2StdbyDecAction:
                case Nav2Swap:
                    nav2Swap.SetControllerValue(1);
                    break;
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            DebugTracing.Trace($"actionParameter '{actionParameter}', ticks '{ticks}'");
            switch (actionParameter)
            {
                case Nav1IntEncAction:
                    nav1StandbyFreq.SetControllerValue(navAdjuster.IncrIntValue(nav1StandbyFreq.ControllerValue, ticks));
                    break;
                case Nav1DecEncAction:
                    nav1StandbyFreq.SetControllerValue(navAdjuster.IncrDecimalValue(nav1StandbyFreq.ControllerValue, ticks));
                    break;
                case Nav2IntEncAction:
                    nav2StandbyFreq.SetControllerValue(navAdjuster.IncrIntValue(nav2StandbyFreq.ControllerValue, ticks));
                    break;
                case Nav2DecEncAction:
                    nav2StandbyFreq.SetControllerValue(navAdjuster.IncrDecimalValue(nav2StandbyFreq.ControllerValue, ticks));
                    break;
            }
            EncoderActionNamesChanged();
            ButtonActionNamesChanged();
        }

        public void Notify()
        {
            foreach (Binding binding in bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    binding.Reset();
                }
            }
        }

        void SetBackgroundImage(BitmapBuilder builder, Binding binding) => builder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(binding.MsfsValue));
        string ControllerIntValueText(Binding binding) => (binding.ControllerValue == 0 ? "0" : ControllerValueSubstring(binding, 0, 3)) + ".";
        string ControllerDecValueText(Binding binding) => binding.ControllerValue == 0 ? "0" : ControllerValueSubstring(binding, 3, 2);
        string ControllerValueSubstring(Binding binding, int startIndex, int length) => binding.ControllerValue.ToString().Substring(startIndex, length);

        readonly Binding nav1ActiveFreq;
        readonly Binding nav2ActiveFreq;
        readonly Binding nav1Available;
        readonly Binding nav2Available;
        readonly Binding nav1StandbyFreq;
        readonly Binding nav2StandbyFreq;
        readonly Binding nav1Swap;
        readonly Binding nav2Swap;

        private const string Nav1ActIntAction = "NAV1 Active Int";
        private const string Nav1ActDecAction = "NAV1 Active Float";
        private const string Nav1StdbyIntAction = "NAV1 Standby Int";
        private const string Nav1StdbyDecAction = "NAV1 Standby Float";
        private const string Nav2ActIntAction = "NAV2 Active Int";
        private const string Nav2ActDecAction = "NAV2 Active Float";
        private const string Nav2StdbyIntAction = "NAV2 Standby Int";
        private const string Nav2StdbyDecAction = "NAV2 Standby Float";

        private const string Nav1IntEncAction = "NAV1 Int Encoder";
        private const string Nav1DecEncAction = "NAV1 Float Encoder";
        private const string Nav2IntEncAction = "NAV2 Int Encoder";
        private const string Nav2DecEncAction = "NAV2 Float Encoder";

        private const string Nav1Swap = "NAV1 freq swap";
        private const string Nav2Swap = "NAV2 freq swap";
        private const string NoAction = "";

        readonly List<Binding> bindings = new List<Binding>();
        readonly DecimalValueAdjuster navAdjuster = new DecimalValueAdjuster(108, 117, 0, 95, 5);
        //>> Here we want an adfAdjuster
    }
}
