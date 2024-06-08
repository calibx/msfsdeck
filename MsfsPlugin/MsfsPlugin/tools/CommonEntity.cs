namespace Loupedeck.MsfsPlugin.tools
{
    using System.Collections.Generic;

    internal class CommonEntity
    {
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

        private readonly IList<Binding> bindings;
    }
}
