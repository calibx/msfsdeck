namespace Loupedeck.MsfsPlugin.folder
{
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin;

    public abstract class DefaultFolder : PluginDynamicFolder, INotifiable
    {
        protected readonly List<Binding> bindings = new List<Binding>();

        protected DefaultFolder(string displayName)
        {
            DisplayName = displayName;
            GroupName = "Folder";
            MsfsData.Instance.Register(this);
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;

        public void Notify()
        {
            foreach (Binding binding in bindings)
            {
                if (binding.HasMSFSChanged())
                    binding.Reset();
            }
        }

        protected static Binding Register(BindingKeys key) => MsfsData.Instance.Register(key);
    }
}
