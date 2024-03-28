namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.tools;

    public abstract class DefaultEncoder : PluginDynamicAdjustment, INotifiable
    {
        protected int min;
        protected int max;
        protected int step;

        protected DefaultEncoder(string name, string desc, string category, bool resettable, int min, int max, int step) : base(name, desc, category, resettable)
        {
            this.min = min;
            this.max = max;
            this.step = step;
            entity = new CommonEntity();
            MsfsData.Instance.Register(this);
        }

        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            SetValue(ConvertTool.ApplyAdjustment(GetValue(), ticks, min, max, step));
            ActionImageChanged();
        }

        protected Binding Bind(BindingKeys key) => entity.Bind(key);

        public void Notify() => entity.Notify();

        protected override string GetAdjustmentValue(string actionParameter) => GetDisplayValue();

        protected virtual string GetDisplayValue() => GetValue().ToString();

        protected virtual long GetValue() => 0;

        protected abstract void SetValue(long value);

        readonly CommonEntity entity;
    }
}
