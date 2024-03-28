namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.tools;

    internal class VorObiEncoder : DefaultEncoder
    {
        public VorObiEncoder() : base("VOR OBS", "VOR OBS encoder", "Nav", true, 0, 359, 1)
        {
            vor1Obs = Bind(BindingKeys.VOR1_OBS);
            vor2Obs = Bind(BindingKeys.VOR2_OBS);
            vor1Set = Bind(BindingKeys.VOR1_SET);
            vor2Set = Bind(BindingKeys.VOR2_SET);
        }

        protected override void RunCommand(string actionParameter) => controlsVor2 = !controlsVor2;

        protected override string GetDisplayValue() => $"{Vor1Value}\n {Vor2Value}";

        protected override long GetValue() => currentSet.ControllerValue;

        protected override void SetValue(long newValue) => currentSet.SetControllerValue(newValue);

        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            var presentValue = initialized[currentIndex] ? GetValue() : currentObs.ControllerValue;
            SetValue(ConvertTool.ApplyAdjustment(presentValue, ticks, min, max, step, true));
            initialized[currentIndex] = true;
            ActionImageChanged();
        }

        string Vor1Value => VorValue(0);
        string Vor2Value => VorValue(1);

        private string VorValue(int index) => $"{(index == currentIndex ? ">" : "")}{(index == 0 ? vor1Obs : vor2Obs).ControllerValue}";

        bool controlsVor2 = false;
        Binding currentObs => controlsVor2 ? vor2Obs : vor1Obs;
        Binding currentSet => controlsVor2 ? vor2Set : vor1Set;
        int currentIndex => controlsVor2 ? 1 : 0;
        bool[] initialized = new bool[2] { false, false };

        readonly Binding vor1Obs;
        readonly Binding vor2Obs;
        readonly Binding vor1Set;
        readonly Binding vor2Set;
    }
}
