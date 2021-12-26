namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class LightsInput : PluginDynamicCommand, Notifiable
    {
        public LightsInput() : base()
        {
            MsfsData.Instance.register(this);
            this.AddParameter("Navigation", "Toogle navigation light", "Lights");
            this.AddParameter("Beacon", "Toogle Beacon light", "Lights");
            this.AddParameter("Landing", "Toogle Landing light", "Lights");
            this.AddParameter("Taxi", "Toogle Taxi light", "Lights");
            this.AddParameter("Strobes", "Toogle Strobes light", "Lights");
            this.AddParameter("Instruments", "Toogle Instruments light", "Lights");
            this.AddParameter("Recognition", "Toogle Recognition light", "Lights");
            this.AddParameter("Wing", "Toogle Wing light", "Lights");
            this.AddParameter("Logo", "Toogle Logo light", "Lights");
            this.AddParameter("Cabin", "Toogle Cabin light", "Lights");
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.CurrentSpeed + "\n" + MsfsData.Instance.CurrentAPSpeed;
        }

        protected override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "Navigation":
                    MsfsData.Instance.NavigationLight = !MsfsData.Instance.NavigationLight;
                    break;
                case "Beacon":
                    MsfsData.Instance.BeaconLight = !MsfsData.Instance.BeaconLight;
                    break;
                case "Landing":
                    MsfsData.Instance.LandingLight = !MsfsData.Instance.LandingLight;
                    break;
                case "Taxi":
                    MsfsData.Instance.NavigationLight = !MsfsData.Instance.NavigationLight;
                    break;
                case "Strobes":
                    MsfsData.Instance.TaxiLight = !MsfsData.Instance.TaxiLight;
                    break;
                case "Instruments":
                    MsfsData.Instance.InstrumentsLight = !MsfsData.Instance.InstrumentsLight;
                    break;
                case "Recognition":
                    MsfsData.Instance.RecognitionLight = !MsfsData.Instance.RecognitionLight;
                    break;
                case "Wing":
                    MsfsData.Instance.WingLight = !MsfsData.Instance.WingLight;
                    break;
                case "Logo":
                    MsfsData.Instance.LogoLight = !MsfsData.Instance.LogoLight;
                    break;
                case "Cabin":
                    MsfsData.Instance.CabinLight = !MsfsData.Instance.CabinLight;
                    break;
            }

        }


    }
}

