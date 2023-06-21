using EnvDTE;
using EnvDTE80;

namespace EventLogVS.Providers
{
    internal class ShellProvider : BaseProvider<ShellProvider>
    {
        private DTEEvents _events;

        public override string Name => "Shell";

        public override bool IsEnabled => General.Instance.EnableShellProvider;

        protected override async Task InitializeAsync()
        {
            VS.Events.ShellEvents.EnvironmentColorChanged += () => { Write($"Environment color changed"); };

            // None of these events seem to fire
            VS.Events.ShellEvents.MainWindowVisibilityChanged += (visible) => { Write($"Main window visibility changed to {Result(visible)}"); };
            VS.Events.ShellEvents.ShellAvailable += () => { Write($"Shell available"); };
            VS.Events.ShellEvents.ShutdownStarted += () => { Write($"Shutdown started"); };

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DTE2 dte = await VS.GetServiceAsync<DTE, DTE2>();
            _events = (dte.Events as Events2).DTEEvents;
            
            _events.ModeChanged += (reason) => { Write($"Mode changed to {reason}"); };
            _events.OnStartupComplete += () => { Write($"Startup complete"); };
            _events.OnMacrosRuntimeReset += () => { Write($"Macro runtime reset"); };
        }

        private static string Result(bool visible)
        {
            return visible ? "visible" : "hidden";
        }
    }
}
