namespace Loupedeck.MsfsPlugin.input
{
    using System;

    public abstract class DefaultInput : PluginDynamicCommand, Notifiable
    {
        public DefaultInput(String name, String desc, String category) : base(name, desc, category) => MsfsData.Instance.Register(this);

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return this.GetValue();
        }

        protected override void RunCommand(String actionParameter) => this.ChangeValue();
        protected abstract String GetValue();
        protected virtual void ChangeValue() { }
    }
}

