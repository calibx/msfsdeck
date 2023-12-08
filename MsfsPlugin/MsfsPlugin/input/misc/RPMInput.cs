namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class RPMInput : DefaultInput
    {
        public RPMInput() : base("RPM/N1", "Display RPM for or N1", "Misc")
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.ENGINE_TYPE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.ENGINE_NUMBER));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.E1RPM));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.E2RPM));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.E1N1));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.E2N1));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.E3N1));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.E4N1));
        }

        protected override string GetValue()
        {
            switch (bindings[0].MsfsValue)
            {
                case 0:
                    return "RPM\n" + (bindings[1].MsfsValue == 2
                                    ? bindings[2].MsfsValue.ToString() + "\n" + bindings[3].MsfsValue.ToString()
                                    : bindings[2].MsfsValue.ToString());
                case 1:
                case 5:
                    return "N1\n" + (bindings[1].MsfsValue == 4
                    ? bindings[4].MsfsValue.ToString() + "\n" + bindings[5].MsfsValue.ToString() + "\n" + bindings[6].MsfsValue.ToString() + "\n" + bindings[7].MsfsValue.ToString()
                    : bindings[1].MsfsValue == 2
                                    ? bindings[4].MsfsValue.ToString() + "\n" + bindings[5].MsfsValue.ToString()
                                    : bindings[4].MsfsValue.ToString());
                default:
                    return "N/A";
            }
        }
    }
}
