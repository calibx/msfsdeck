namespace Loupedeck.MsfsPlugin.input
{
    using System;
    using System.Diagnostics;
    public abstract class DefaultInput : PluginDynamicCommand, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        protected readonly String _imageAvailableResourcePath = "Loupedeck.MsfsPlugin.Resources.available.png";
        protected readonly String _imageDisableResourcePath = "Loupedeck.MsfsPlugin.Resources.disable.png";
        protected Binding _binding;
        public DefaultInput(String name, String desc, String category) : base(name, desc, category) => MsfsData.Instance.Register(this);

        public void Notify()
        {
            if (this._binding != null && this._binding.Key != null)
            {
                if (this._binding.HasMSFSChanged())
                {
                    Debug.WriteLine("Refesh " + this._binding.Key);
                    this.ActionImageChanged();
                    this._binding.SetMsfsValue(this._binding.MsfsValue);
                }
                else
                {
                    Debug.WriteLine("Skipping " + this._binding.Key);
                }
            }
            else
            {
                this.ActionImageChanged();
            }
        }

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) => this.GetValue();
        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize) => this.GetImage(imageSize);
        protected override void RunCommand(String actionParameter) => this.ChangeValue();
        protected virtual String GetValue() => null;
        protected virtual BitmapImage GetImage(PluginImageSize imageSize) => null;
        protected virtual void ChangeValue() { }
    }
}

