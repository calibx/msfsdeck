namespace Loupedeck.MsfsPlugin.folder
{
    using Loupedeck.MsfsPlugin;
    using Loupedeck.MsfsPlugin.tools;

    public abstract class DefaultFolder : PluginDynamicFolder, INotifiable
    {
        protected DefaultFolder(string displayName)
        {
            DisplayName = displayName;
            GroupName = "Folder";
            entity = new CommonEntity();
            MsfsData.Instance.Register(this);
        }

        protected Binding Bind(BindingKeys key) => entity.Bind(key);

        public void Notify(bool force) => entity.Notify(force);

        readonly CommonEntity entity;
    }
}
