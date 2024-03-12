namespace Loupedeck.MsfsPlugin.tools
{
    using System.Collections.Generic;

    internal class CommonEntity
    {
        public readonly IList<Binding> bindings;   //>> Can be made private when no longer referenced from DefaultEncoder and DefaultInput.

        public CommonEntity()
        {
            bindings = new List<Binding>();
        }

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

        public Binding Bind(BindingKeys key, long? value = null)
        {
            Binding binding = MsfsData.Instance.Register(key, value);
            bindings.Add(binding);
            return binding;
        }
    }
}
