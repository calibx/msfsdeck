namespace Loupedeck.MsfsPlugin.encoder
{
    using Loupedeck.MsfsPlugin.tools;

    internal class VorObiEncoder : DefaultEncoder
    {
        public VorObiEncoder() : base("VOR OBS", "VOR OBS encoder", "Nav", true, 0, 359, 1)
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.VOR1_OBS));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.VOR2_OBS));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.VOR1_SET));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.VOR2_SET));
        }

        protected override void RunCommand(string actionParameter) => controlsVor2 = !controlsVor2;

        protected override string GetDisplayValue() => $"{Vor1Value}\n {Vor2Value}";

        protected override long GetValue() => bindings[currentIndex + 2].ControllerValue;

        protected override void SetValue(long newValue) => bindings[currentIndex + 2].SetControllerValue(newValue);

        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            var presentValue = initialized[currentIndex] ? GetValue() : bindings[currentIndex].ControllerValue;
            SetValue(ConvertTool.ApplyAdjustment(presentValue, ticks, min, max, step, true));
            initialized[currentIndex] = true;
            ActionImageChanged();
        }

        string Vor1Value => VorValue(0);
        string Vor2Value => VorValue(1);

        private string VorValue(int index) => $"{(index == currentIndex ? ">" : "")}{bindings[index].ControllerValue}";

        int currentIndex => controlsVor2 ? 1 : 0;

        bool controlsVor2 = false;
        bool[] initialized = new bool[2] { false, false };
    }
}
