namespace Loupedeck.MsfsPlugin.encoder
{
    using System.Collections.Generic;
    using Loupedeck.MsfsPlugin.tools;

    public abstract class DefaultEncoder : PluginDynamicAdjustment, INotifiable
    {
        protected int min;
        protected int max;
        protected int step;
        protected readonly List<Binding> bindings = new List<Binding>();

        public DefaultEncoder(string name, string desc, string category, bool resettable, int min, int max, int step) : base(name, desc, category, resettable)
        {
            this.min = min;
            this.max = max;
            this.step = step;
            MsfsData.Instance.Register(this);
        }

        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            SetValue(ConvertTool.ApplyAdjustment(GetValue(), ticks, min, max, step));
            ActionImageChanged();
        }
        protected override string GetAdjustmentValue(string actionParameter) => GetDisplayValue();

        public void Notify()
        {
            foreach (Binding binding in bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    binding.Reset();
                }
            }
        }

        protected static Binding Register(BindingKeys key) => MsfsData.Instance.Register(key);
        protected virtual string GetDisplayValue() => GetValue().ToString();
        protected virtual long GetValue() => 0;
        protected abstract void SetValue(long value);
    }
}
