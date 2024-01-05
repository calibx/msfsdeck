namespace Loupedeck.MsfsPlugin.folder
{
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class NavDynamicFolder : DefaultFolder
    {
        public NavDynamicFolder() : base("NAV")
        {
            bindings.Add(Nav1ActiveFreq = Register(BindingKeys.NAV1_ACTIVE_FREQUENCY));
            bindings.Add(Nav2ActiveFreq = Register(BindingKeys.NAV2_ACTIVE_FREQUENCY));
            bindings.Add(Nav1Available = Register(BindingKeys.NAV1_AVAILABLE));
            bindings.Add(Nav2Available = Register(BindingKeys.NAV2_AVAILABLE));
            bindings.Add(Nav1StandbyFreq = Register(BindingKeys.NAV1_STBY_FREQUENCY));
            bindings.Add(Nav2StandbyFreq = Register(BindingKeys.NAV2_STBY_FREQUENCY));
            bindings.Add(Nav1Swap = Register(BindingKeys.NAV1_RADIO_SWAP));
            bindings.Add(Nav2Swap = Register(BindingKeys.NAV2_RADIO_SWAP));
            bindings.Add(AdfActiveFreq = Register(BindingKeys.ADF_ACTIVE_FREQUENCY));
            bindings.Add(AdfStandbyFreq = Register(BindingKeys.ADF_STBY_FREQUENCY));
            bindings.Add(AdfAvail = Register(BindingKeys.ADF1_AVAILABLE));
            bindings.Add(AdfStbyAvail = Register(BindingKeys.ADF1_STBY_AVAILABLE));
            bindings.Add(AdfSwap = Register(BindingKeys.ADF_RADIO_SWAP));
        }

        // Plane                      Variable containing active frequency       Variable containing "other" freq     "other" frequency shown?  Swapping event
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        // ATR-42/72                        ADF ACTIVE FREQUENCY:1                   ADF STANDBY FREQUENCY:1                     yes            ADF1_RADIO_SWAP
        //                                  ADF ACTIVE FREQUENCY:2                   ADF STANDBY FREQUENCY:2                     yes            ADF2_RADIO_SWAP .. not yet implemented
        // Britten Norman Islander          ADF ACTIVE FREQUENCY:1                                                               no             
        // B737                             ADF ACTIVE FREQUENCY:1                   ??                                          yes            Must be using non-standard variable for "other" frequency
        // Carenado C182 RG II              ADF ACTIVE FREQUENCY:1                   ADF STANDBY FREQUENCY:1                     yes            ADF1_RADIO_SWAP
        // DC3                              ADF ACTIVE FREQUENCY:1                                                               no             
        // DC6                              ADF ACTIVE FREQUENCY:1                   ??                                          yes            Has two sets of active/standby but must be using non-standard variables
        // DHC-4 Caribou                    ADF ACTIVE FREQUENCY:1                   ADF ACTIVE FREQUENCY:2                      yes            Could be implemented because 'ADF AVAILABLE:2' is true
        // DHC-6 Twin Otter                 ADF ACTIVE FREQUENCY:1                   ADF STANDBY FREQUENCY:1                     yes            ADF1_RADIO_SWAP
        // JF Piper Arrow III               ADF ACTIVE FREQUENCY:1                                                               no
        // JP Logistics C152                ADF ACTIVE FREQUENCY:1                   ADF ACTIVE FREQUENCY:2                      yes            Can not be implemented because 'ADF AVAILABLE:2' is false
        // Milviz 310R                      ADF ACTIVE FREQUENCY:1                   ADF STANDBY FREQUENCY:1                     yes            ADF1_RADIO_SWAP
        // Textron C152                     ADF ACTIVE FREQUENCY:1                                                               no                            
        // Textron C172                     ADF ACTIVE FREQUENCY:1                   ADF STANDBY FREQUENCY:1                     yes            ADF1_RADIO_SWAP

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            //DebugTracing.Trace($"deviceType '{deviceType}'");
            return new[]
            {
                CreateCommandName(Nav1ActIntAction),
                CreateCommandName(Nav1ActDecAction),
                CreateCommandName(Nav1StbyIntAction),
                CreateCommandName(Nav1StbyDecAction),
                CreateCommandName(Nav2ActIntAction),
                CreateCommandName(Nav2ActDecAction),
                CreateCommandName(Nav2StbyIntAction),
                CreateCommandName(Nav2StbyDecAction),
                CreateCommandName(AdfActIntAction),
                CreateCommandName(AdfActDecAction),
                CreateCommandName(AdfStbyIntAction),
                CreateCommandName(AdfStbyDecAction),
            };
        }

        public override IEnumerable<string> GetEncoderPressActionNames(DeviceType deviceType)
        {
            //DebugTracing.Trace($"deviceType '{deviceType}'");
            return new[]
            {
                CreateCommandName(Nav1SwapAction),
                CreateCommandName(Nav2SwapAction),
                CreateCommandName(AdfSwapAction),
                NavigateUpActionName,
                CreateCommandName(NoAction),
                CreateCommandName(AdfOneTenSwap)
            };
        }

        public override IEnumerable<string> GetEncoderRotateActionNames(DeviceType deviceType)
        {
            //DebugTracing.Trace($"deviceType '{deviceType}'");
            return new[]
            {
                CreateAdjustmentName(Nav1IntEncAction),
                CreateAdjustmentName(Nav2IntEncAction),
                CreateAdjustmentName(AdfHundredsEncAction),
                CreateAdjustmentName(Nav1DecEncAction),
                CreateAdjustmentName(Nav2DecEncAction),
                CreateAdjustmentName(AdfOnesEncAction),
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
                    return "Up";
                case Nav2IntEncAction:
                    return "NAV2\nActive\n<-->";
                case Nav2DecEncAction:
                    return " ";
                case AdfHundredsEncAction:
                    // It could have been nice if we were able to document that this encoder changes the hundreds value
                    return "ADF\nActive" + (CanSwapAdf ? "\n<-->" : "");
                case AdfOnesEncAction:
                    return "ADF\nStandby\n" + (OnesMultiplier == 10 ? ">10/1" : "10/>1");
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
                        SetBackgroundImage(bitmapBuilder, Nav1Available);
                        bitmapBuilder.DrawText(ControllerNavIntValueText(Nav1ActiveFreq), ImageTool.Green, 40);
                        break;
                    case Nav1ActDecAction:
                        SetBackgroundImage(bitmapBuilder, Nav1Available);
                        bitmapBuilder.DrawText(ControllerNavDecValueText(Nav1ActiveFreq), ImageTool.Green, 40);
                        break;
                    case Nav1StbyIntAction:
                        SetBackgroundImage(bitmapBuilder, Nav1Available);
                        bitmapBuilder.DrawText(ControllerNavIntValueText(Nav1StandbyFreq), ImageTool.Yellow, 40);
                        break;
                    case Nav1StbyDecAction:
                        SetBackgroundImage(bitmapBuilder, Nav1Available);
                        bitmapBuilder.DrawText(ControllerNavDecValueText(Nav1StandbyFreq), ImageTool.Yellow, 40);
                        break;
                    case Nav2ActIntAction:
                        SetBackgroundImage(bitmapBuilder, Nav2Available);
                        bitmapBuilder.DrawText(ControllerNavIntValueText(Nav2ActiveFreq), ImageTool.Green, 40);
                        break;
                    case Nav2ActDecAction:
                        SetBackgroundImage(bitmapBuilder, Nav2Available);
                        bitmapBuilder.DrawText(ControllerNavDecValueText(Nav2ActiveFreq), ImageTool.Green, 40);
                        break;
                    case Nav2StbyIntAction:
                        SetBackgroundImage(bitmapBuilder, Nav2Available);
                        bitmapBuilder.DrawText(ControllerNavIntValueText(Nav2StandbyFreq), ImageTool.Yellow, 40);
                        break;
                    case Nav2StbyDecAction:
                        SetBackgroundImage(bitmapBuilder, Nav2Available);
                        bitmapBuilder.DrawText(ControllerNavDecValueText(Nav2StandbyFreq), ImageTool.Yellow, 40);
                        break;
                    case AdfActIntAction:
                        SetBackgroundImage(bitmapBuilder, AdfAvail);
                        bitmapBuilder.DrawText(ControllerAdfIntValueText(AdfActiveFreq, AdfAvail), ImageTool.Green, 30);
                        break;
                    case AdfActDecAction:
                        SetBackgroundImage(bitmapBuilder, AdfAvail);
                        bitmapBuilder.DrawText(ControllerAdfDecValueText(AdfActiveFreq, AdfAvail), ImageTool.Green, 30);
                        break;
                    case AdfStbyIntAction:
                        SetBackgroundImage(bitmapBuilder, AdfStbyAvail);
                        bitmapBuilder.DrawText(ControllerAdfIntValueText(AdfStandbyFreq, AdfStbyAvail), ImageTool.Green, 30);
                        break;
                    case AdfStbyDecAction:
                        SetBackgroundImage(bitmapBuilder, AdfStbyAvail);
                        bitmapBuilder.DrawText(ControllerAdfDecValueText(AdfStandbyFreq, AdfStbyAvail), ImageTool.Green, 30);
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
                case Nav1StbyIntAction:
                case Nav1StbyDecAction:
                case Nav1SwapAction:
                    Nav1Swap.SetControllerValue(1);
                    break;
                case Nav2ActIntAction:
                case Nav2ActDecAction:
                case Nav2StbyIntAction:
                case Nav2StbyDecAction:
                case Nav2SwapAction:
                    Nav2Swap.SetControllerValue(1);
                    break;
                case AdfActIntAction:
                case AdfActDecAction:
                case AdfStbyIntAction:
                case AdfStbyDecAction:
                case AdfSwapAction:
                    if (CanSwapAdf)
                        AdfSwap.SetControllerValue(1);
                    break;
                case AdfOneTenSwap:
                    OnesMultiplier = OnesMultiplier == 1 ? 10 : 1;
                    break;
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            DebugTracing.Trace($"actionParameter '{actionParameter}', ticks '{ticks}'");
            switch (actionParameter)
            {
                case Nav1IntEncAction:
                    Nav1StandbyFreq.SetControllerValue(navAdjuster.IncrIntValue(Nav1StandbyFreq.ControllerValue, ticks));
                    break;
                case Nav1DecEncAction:
                    Nav1StandbyFreq.SetControllerValue(navAdjuster.IncrDecimalValue(Nav1StandbyFreq.ControllerValue, ticks));
                    break;
                case Nav2IntEncAction:
                    Nav2StandbyFreq.SetControllerValue(navAdjuster.IncrIntValue(Nav2StandbyFreq.ControllerValue, ticks));
                    break;
                case Nav2DecEncAction:
                    Nav2StandbyFreq.SetControllerValue(navAdjuster.IncrDecimalValue(Nav2StandbyFreq.ControllerValue, ticks));
                    break;
                case AdfHundredsEncAction:
                    AdjustAdf(AdfAdjustmentFrequency, ticks * 100);
                    break;
                case AdfOnesEncAction:
                    AdjustAdf(AdfAdjustmentFrequency, ticks * OnesMultiplier);
                    break;
            }
        }

        void AdjustAdf(Binding bindingFreq, int ticks) => bindingFreq.SetControllerValue(adfAdjuster.IncrIntValue(bindingFreq.ControllerValue, ticks));

        void SetBackgroundImage(BitmapBuilder builder, Binding binding) => builder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(binding.MsfsValue));

        string ControllerNavIntValueText(Binding binding) => (binding.ControllerValue == 0 ? "0" : ControllerValueSubstring(binding, 0, 3)) + ".";
        string ControllerAdfIntValueText(Binding adfFreq, Binding adfAvailability) => adfAvailability.GetBool() ? ControllerAdfIntValueText(adfFreq) : string.Empty;
        string ControllerAdfDecValueText(Binding adfFreq, Binding adfAvailability) => adfAvailability.GetBool() ? ControllerAdfDecValueText(adfFreq) : string.Empty;
        string ControllerAdfIntValueText(Binding binding) => $"{binding.ControllerValue / 10}.";
        string ControllerAdfDecValueText(Binding binding) => (binding.ControllerValue % 10).ToString();
        string ControllerNavDecValueText(Binding binding) => binding.ControllerValue == 0 ? "0" : ControllerValueSubstring(binding, 3, 2);
        string ControllerValueSubstring(Binding binding, int startIndex, int length) => binding.ControllerValue.ToString().Substring(startIndex, length);

        Binding AdfAdjustmentFrequency => CanSwapAdf ? AdfStandbyFreq : AdfActiveFreq;

        bool CanSwapAdf => AdfStbyAvail.GetBool();

        readonly Binding Nav1ActiveFreq;
        readonly Binding Nav2ActiveFreq;
        readonly Binding Nav1Available;
        readonly Binding Nav2Available;
        readonly Binding Nav1StandbyFreq;
        readonly Binding Nav2StandbyFreq;
        readonly Binding Nav1Swap;
        readonly Binding Nav2Swap;
        readonly Binding AdfSwap;
        readonly Binding AdfActiveFreq;
        readonly Binding AdfStandbyFreq;
        readonly Binding AdfAvail;
        readonly Binding AdfStbyAvail;

        const string Nav1ActIntAction = "NAV1 Active Int";
        const string Nav1ActDecAction = "NAV1 Active Float";
        const string Nav1StbyIntAction = "NAV1 Standby Int";
        const string Nav1StbyDecAction = "NAV1 Standby Float";
        const string Nav2ActIntAction = "NAV2 Active Int";
        const string Nav2ActDecAction = "NAV2 Active Float";
        const string Nav2StbyIntAction = "NAV2 Standby Int";
        const string Nav2StbyDecAction = "NAV2 Standby Float";
        const string AdfActIntAction = "ADF Active Int";
        const string AdfActDecAction = "ADF Active Dec";
        const string AdfStbyIntAction = "ADF Standby Int";
        const string AdfStbyDecAction = "ADF Standby Dec";

        const string Nav1IntEncAction = "NAV1 Int Encoder";
        const string Nav1DecEncAction = "NAV1 Dec Encoder";
        const string Nav2IntEncAction = "NAV2 Int Encoder";
        const string Nav2DecEncAction = "NAV2 Dec Encoder";
        const string AdfHundredsEncAction = "ADF Int Encoder";
        const string AdfOnesEncAction = "ADF Dec Encoder";

        const string Nav1SwapAction = "NAV1 freq swap";
        const string Nav2SwapAction = "NAV2 freq swap";
        const string AdfSwapAction = "ADF freq swap";
        const string AdfOneTenSwap = "ADF one/ten swap";
        const string NoAction = "";

        int OnesMultiplier = 1;

        readonly DecimalValueAdjuster navAdjuster = new DecimalValueAdjuster(108, 117, 0, 95, 5, 1000000);
        readonly DecimalValueAdjuster adfAdjuster = new DecimalValueAdjuster(100, 1799, 0, 0, 1, 10);
    }
}
