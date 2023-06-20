global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using EventLogVS.Providers;
using Microsoft.VisualStudio;

namespace EventLogVS
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.EventLogVSString)]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), Vsix.Name, "General", 0, 0, true, SupportsProfiles = true)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class EventLogVSPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            OutputWindowPane pane = await VS.Windows.CreateOutputWindowPaneAsync("Event Log", true);

            await BuildProvider.InitializeAsync(pane);
            await CommandProvider.InitializeAsync(pane);
            await DebugProvider.InitializeAsync(pane);
            await DocumentProvider.InitializeAsync(pane);
            await ProjectItemProvider.InitializeAsync(pane);
            await SolutionProvider.InitializeAsync(pane);
            await WindowProvider.InitializeAsync(pane);
        }
    }
}