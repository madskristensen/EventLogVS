namespace EventLogVS.Providers
{
    internal class ShellProvider : BaseProvider<ShellProvider>
    {
        public override string Name => "Shell";

        public override bool IsEnabled => General.Instance.EnableShellProvider;

        protected override Task InitializeAsync()
        {
            VS.Events.ShellEvents.EnvironmentColorChanged += () => { Write($"Environment color changed"); };

            // None of these events seem to fire
            VS.Events.ShellEvents.MainWindowVisibilityChanged += (visible) => { Write($"Main window visibility changed to {Result(visible)}"); };
            VS.Events.ShellEvents.ShellAvailable += () => { Write($"Shell available"); };
            VS.Events.ShellEvents.ShutdownStarted += () => { Write($"Shutdown started"); };

            return Task.CompletedTask;
        }

        private static string Result(bool visible)
        {
            return visible ? "visible" : "hidden";
        }
    }
}
