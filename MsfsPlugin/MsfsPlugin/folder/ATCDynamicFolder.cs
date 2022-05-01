namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;
    using Loupedeck.MsfsPlugin.msfs;

    public class ATCDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly List<Binding> _bindings = new List<Binding>();
        public ATCDynamicFolder()
        {
            this.DisplayName = "ATC";
            this.GroupName = "Folder";
            this.Navigation = PluginDynamicFolderNavigation.EncoderArea;
            MsfsData.Instance.Register(this);
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_0_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_1_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_2_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_3_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_4_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_5_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_6_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_7_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_8_ATC_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ATC_9_ATC_FOLDER)));
        }
        public override IEnumerable<String> GetButtonPressActionNames()
        {
            return new[]
            {
                this.CreateCommandName("Open/Close"),
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
            SimConnectDAO.Instance.setPlugin(this.Plugin);
            switch (actionParameter)
            {
                case "Open/Close":
                    this._bindings[0].SetControllerValue(1);
                    break;
                case "0":
                    this._bindings[1].SetControllerValue(1);
                    break;
                case "1":
                    this._bindings[2].SetControllerValue(1);
                    break;
                case "2":
                    this._bindings[3].SetControllerValue(1);
                    break;
                case "3":
                    this._bindings[4].SetControllerValue(1);
                    break;
                case "4":
                    this._bindings[5].SetControllerValue(1);
                    break;
                case "5":
                    this._bindings[6].SetControllerValue(1);
                    break;
                case "6":
                    this._bindings[7].SetControllerValue(1);
                    break;
                case "7":
                    this._bindings[8].SetControllerValue(1);
                    break;
                case "8":
                    this._bindings[9].SetControllerValue(1);
                    break;
                case "9":
                    this._bindings[10].SetControllerValue(1);
                    break;
            }
        }
        public void Notify() {}

    }
}
