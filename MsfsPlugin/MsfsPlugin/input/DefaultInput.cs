namespace Loupedeck.MsfsPlugin.input
{
    using System;
    using System.Collections.Generic;
    public abstract class DefaultInput : PluginDynamicCommand, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        protected readonly String _imageWaitResourcePath = "Loupedeck.MsfsPlugin.Resources.wait.png";
        protected readonly String _imageAvailableResourcePath = "Loupedeck.MsfsPlugin.Resources.available.png";
        protected readonly String _imageDisableResourcePath = "Loupedeck.MsfsPlugin.Resources.disable.png";
        protected readonly String _imageDisconnectResourcePath = "Loupedeck.MsfsPlugin.Resources.disconnect.png";
        protected readonly String _imageTryingResourcePath = "Loupedeck.MsfsPlugin.Resources.trying.png";
        protected readonly List<Binding> _bindings = new List<Binding>();
        protected readonly Binding _bindingCnx = MsfsData.Instance.Register(new Binding(BindingKeys.CONNECTION));
        public DefaultInput(String name, String desc, String category) : base(name, desc, category) => MsfsData.Instance.Register(this);
        public void Notify()
        {
            foreach (Binding binding in this._bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    MsfsData.Instance.refreshLimiter++;
                    binding.Reset();
                    this.ActionImageChanged();
                }
            }
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) => this.GetValue();
        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            if (this._bindingCnx.MsfsValue == 0)
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageDisconnectResourcePath));
                    bitmapBuilder.DrawText(this.DisplayName);
                    return bitmapBuilder.ToImage();
                }
            } else if (this._bindingCnx.MsfsValue == 2)
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageTryingResourcePath));
                    bitmapBuilder.DrawText(this.DisplayName);
                    return bitmapBuilder.ToImage();
                }
            }
            else
            {
                return this.GetImage(imageSize);
            }
        }
        
        protected override void RunCommand(String actionParameter) => this.ChangeValue();
        protected virtual String GetValue() => null;
        protected virtual BitmapImage GetImage(PluginImageSize imageSize) => null;
        protected virtual void ChangeValue() { }
    }
}

