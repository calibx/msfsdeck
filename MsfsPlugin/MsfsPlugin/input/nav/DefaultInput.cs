namespace Loupedeck.MsfsPlugin.input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class DefaultInput : PluginDynamicCommand, Notifiable
    {
        public DefaultInput(String name, String desc, String category) : base(name, desc, category) => MsfsData.Instance.register(this);

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return this.GetValue();
        }

        protected override void RunCommand(String actionParameter) => this.ChangeValue();
        protected abstract String GetValue();
        protected abstract void ChangeValue();
    }
}

