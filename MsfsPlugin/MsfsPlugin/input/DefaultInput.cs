namespace Loupedeck.MsfsPlugin.input
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    public abstract class DefaultInput : PluginDynamicCommand, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        protected readonly String _imageAvailableResourcePath = "Loupedeck.MsfsPlugin.Resources.available.png";
        protected readonly String _imageDisableResourcePath = "Loupedeck.MsfsPlugin.Resources.disable.png";
        protected readonly List<Binding> _bindings = new List<Binding>();
        public DefaultInput(String name, String desc, String category) : base(name, desc, category) => MsfsData.Instance.Register(this);
        public void Notify()
        {
            foreach (Binding binding in this._bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    Debug.WriteLine("Refresh " + binding.Key);
                    //binding.Reset();
                    this.ActionImageChanged();
                }
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

