namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Loupedeck.MsfsPlugin.input;

    class RPMInput : DefaultInput
    {
        public RPMInput() : base("RPM/N1", "Display RPM for or N1", "Misc")
        {
            bindings.Add(engineType = Register(BindingKeys.ENGINE_TYPE));
            bindings.Add(engineNumber = Register(BindingKeys.ENGINE_NUMBER));
            bindings.Add(eng1Rpm = Register(BindingKeys.E1RPM));
            bindings.Add(eng2Rpm = Register(BindingKeys.E2RPM));
            bindings.Add(eng3Rpm = Register(BindingKeys.E3RPM));
            bindings.Add(eng4Rpm = Register(BindingKeys.E4RPM));
            bindings.Add(eng1N1 = Register(BindingKeys.E1N1));
            bindings.Add(eng2N1 = Register(BindingKeys.E2N1));
            bindings.Add(eng3N1 = Register(BindingKeys.E3N1));
            bindings.Add(eng4N1 = Register(BindingKeys.E4N1));
            bindings.Add(eng1N2 = Register(BindingKeys.E1N2));
            bindings.Add(eng2N2 = Register(BindingKeys.E2N2));
            bindings.Add(eng3N2 = Register(BindingKeys.E3N2));
            bindings.Add(eng4N2 = Register(BindingKeys.E4N2));

            RpmBindings = new List<Binding>() { eng1Rpm, eng2Rpm, eng3Rpm, eng4Rpm };
            N1Bindings = new List<Binding>() { eng1N1, eng2N1, eng3N1, eng4N1 };
            N2Bindings = new List<Binding>() { eng1N2, eng2N2, eng3N2, eng4N2 };
        }

        protected override string GetValue()
        {
            switch (engineType.MsfsValue)
            {
                case 0:   // Prop
                case 5:   // Turbo-prop
                    return Join(Prepend("RPM", RpmValues));
                case 1:   // Jet
                    return Join(Prepend("N1" + columnSeparator + "N2", Join(N1Values, N2Values)));
                default:
                    return "N/A";
            }
        }

        IList<string> Prepend(string val, IList<string> list) => list.Prepend(val).ToList();
        string Join(IList<string> values) => string.Join("\n", values);
        IList<string> Join(IList<string> values1, IList<string> values2)
        {
            var result = new List<string>();
            for (int i = 0; i < values1.Count; i++)
                result.Add(values1[i] + columnSeparator + values2[i]);
            return result;
        }
        IList<string> RpmValues => GetValues(RpmBindings, engineCount);
        IList<string> N1Values => GetValues(N1Bindings, engineCount);
        IList<string> N2Values => GetValues(N2Bindings, engineCount);
        IList<string> GetValues(IList<Binding> bindings, int count) => bindings.Take(count).Select(x => x.MsfsValue.ToString()).ToList();

        int engineCount => (int)engineNumber.MsfsValue;

        readonly IList<Binding> N1Bindings;
        readonly IList<Binding> N2Bindings;
        readonly IList<Binding> RpmBindings;

        readonly Binding engineType;
        readonly Binding engineNumber;
        readonly Binding eng1Rpm;
        readonly Binding eng2Rpm;
        readonly Binding eng3Rpm;
        readonly Binding eng4Rpm;
        readonly Binding eng1N1;
        readonly Binding eng2N1;
        readonly Binding eng3N1;
        readonly Binding eng4N1;
        readonly Binding eng1N2;
        readonly Binding eng2N2;
        readonly Binding eng3N2;
        readonly Binding eng4N2;

        const string columnSeparator = ", \t";
    }
}
