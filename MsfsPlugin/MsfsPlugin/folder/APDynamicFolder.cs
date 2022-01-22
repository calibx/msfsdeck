namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    public class APDynamicFolder : PluginDynamicFolder, Notifiable
    {
        public APDynamicFolder()
        {
            this.DisplayName = "AP";
            this.GroupName = "Folder";
            this.Navigation = PluginDynamicFolderNavigation.None;
            MsfsData.Instance.Register(this);

        }

        public override IEnumerable<String> GetEncoderRotateActionNames()
        {
            return new[]
            {
                this.CreateAdjustmentName ("Altitude Encoder"),
                this.CreateAdjustmentName ("Heading Encoder"),
                this.CreateAdjustmentName ("Speed Encoder"),
                this.CreateAdjustmentName ("VS Speed Encoder"),
            };
        }

        public override IEnumerable<String> GetEncoderPressActionNames()
        {
            return new[]
            {
                this.CreateCommandName("Altitude Reset"),
                this.CreateCommandName("Heading Reset"),
                this.CreateCommandName("Speed Reset"),
                this.CreateCommandName("VS Speed Reset"),
            };
        }
        public override IEnumerable<String> GetButtonPressActionNames()
        {
            return new[]
            {
                PluginDynamicFolder.NavigateUpActionName,
                this.CreateCommandName("Altitude"),
                this.CreateCommandName("Heading"),
                this.CreateCommandName("GPS"),
                this.CreateCommandName("Speed"),
                this.CreateCommandName("AP"),
                this.CreateCommandName("Throttle"),
                this.CreateCommandName("VS Speed"),
            };
        }
        public override String GetAdjustmentDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            lock (this)
            { 
                switch (actionParameter)
                {
                    case "Altitude Encoder":
                        ret = "Alt\n[" + MsfsData.Instance.CurrentAPAltitude + "]\n" + MsfsData.Instance.CurrentAltitude;
                        break;
                    case "Heading Encoder":
                        ret = "Head\n[" + MsfsData.Instance.CurrentAPHeading + "]\n" + MsfsData.Instance.CurrentHeading;
                        break;
                    case "Speed Encoder":
                        ret = "Speed\n[" + MsfsData.Instance.CurrentAPSpeed + "]\n" + MsfsData.Instance.CurrentSpeed;
                        break;
                    case "VS Speed Encoder":
                        ret = "VS\n[" + MsfsData.Instance.CurrentAPVerticalSpeed + "]\n" + MsfsData.Instance.CurrentVerticalSpeed;
                        break;
                }
            }
            return ret;
        }
        public override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "Altitude":
                    ret += MsfsData.Instance.ApAltHoldSwitch == 1 ? "Disable" : "Enable";
                    break;
                case "Heading":
                    ret += MsfsData.Instance.ApHeadHoldSwitch == 1 ? "Disable" : "Enable";
                    break;
                case "GPS":
                    ret += MsfsData.Instance.ApNavHoldSwitch == 1 ? "Disable" : "Enable";
                    break;
                case "Speed":
                    ret += MsfsData.Instance.ApSpeedHoldSwitch == 1 ? "Disable" : "Enable";
                    break;
                case "AP":
                    ret += MsfsData.Instance.ApSwitch == 1 ? "Disable" : "Enable";
                    break;
                case "Throttle":
                    ret += MsfsData.Instance.ApThrottleSwitch == 1 ? "Disable" : "Enable";
                    break;
                case "VS Speed":
                    ret += MsfsData.Instance.ApVSHoldSwitch == 1 ? "Disable" : "Enable";
                    break;
            }
            ret += " " + actionParameter;
            return ret;
        }

        public override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            lock (this)
            {
                switch (actionParameter)
                {
                    case "Altitude Encoder":
                        MsfsData.Instance.CurrentAPAltitude = this.ApplyAdjustment(MsfsData.Instance.CurrentAPAltitude, -10000, 20000, 100, ticks);
                        break;
                    case "Heading Encoder":
                        MsfsData.Instance.CurrentAPHeading = this.ApplyAdjustment(MsfsData.Instance.CurrentAPHeading, 0, 360, 1, ticks);
                        break;
                    case "Speed Encoder":
                        MsfsData.Instance.CurrentAPSpeed = this.ApplyAdjustment(MsfsData.Instance.CurrentAPSpeed, 0, 2000, 1, ticks);
                        break;
                    case "VS Speed Encoder":
                        MsfsData.Instance.CurrentAPVerticalSpeed = this.ApplyAdjustment(MsfsData.Instance.CurrentAPVerticalSpeed, -10000, 10000, 100, ticks);
                        break;
                }
            }
        }
        public override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "Altitude":
                    MsfsData.Instance.ApAltHoldSwitch = (MsfsData.Instance.ApAltHoldSwitch + 1) % 2;
                    break;
                case "Heading":
                    MsfsData.Instance.ApHeadHoldSwitch = (MsfsData.Instance.ApHeadHoldSwitch + 1) % 2;
                    break;
                case "GPS":
                    MsfsData.Instance.ApNavHoldSwitch = (MsfsData.Instance.ApNavHoldSwitch + 1) % 2;
                    break;
                case "Speed":
                    MsfsData.Instance.ApSpeedHoldSwitch = (MsfsData.Instance.ApSpeedHoldSwitch + 1) % 2;
                    break;
                case "AP":
                    MsfsData.Instance.ApSwitch = (MsfsData.Instance.ApSwitch + 1) % 2;
                    break;
                case "Throttle":
                    MsfsData.Instance.ApThrottleSwitch = (MsfsData.Instance.ApThrottleSwitch + 1) % 2;
                    break;
                case "VS Speed":
                    MsfsData.Instance.ApVSHoldSwitch = (MsfsData.Instance.ApVSHoldSwitch + 1) % 2;
                    break;
                case "Altitude Reset":
                    MsfsData.Instance.CurrentAPAltitude = (Int32)(Math.Round(MsfsData.Instance.CurrentAltitude / 100d, 0) * 100);
                    break;
                case "Heading Reset":
                    MsfsData.Instance.CurrentAPHeading = MsfsData.Instance.CurrentHeading;
                    break;
                case "Speed Reset":
                    MsfsData.Instance.CurrentAPSpeed = (Int32)(Math.Round(MsfsData.Instance.CurrentSpeed / 100d, 0) * 100);
                    break;
                case "VS Speed Reset":
                    MsfsData.Instance.CurrentAPVerticalSpeed = (Int32)(Math.Round(MsfsData.Instance.CurrentVerticalSpeed / 100d, 0) * 100);
                    break;
            }
        }

        public void Notify()
        {
            this.ButtonActionNamesChanged();
            this.EncoderActionNamesChanged();
        }

        private Int32 ApplyAdjustment(Int32 value, Int32 min, Int32 max, Int32 steps, Int32 ticks)
        {
            value += ticks * steps;
            if (value < min)
            { value = min; }
            else if (value > max)
            { value = max; }
            return value;

        }
    }

}
