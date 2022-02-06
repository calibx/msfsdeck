namespace Loupedeck.MsfsPlugin.input
{
    using System;

    public abstract class DefaultInput : PluginDynamicCommand, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        public DefaultInput(String name, String desc, String category) : base(name, desc, category) => MsfsData.Instance.Register(this);

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return this.GetValue();
        }
        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return this.GetImage(imageSize);
        }

        protected override void RunCommand(String actionParameter) => this.ChangeValue();
        protected virtual String GetValue() => null;
        protected virtual BitmapImage GetImage(PluginImageSize imageSize) => null;
        protected virtual void ChangeValue() { }
    }
}

