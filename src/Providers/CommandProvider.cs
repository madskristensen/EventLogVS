using System.Linq;
using EnvDTE;
using EnvDTE80;

namespace EventLogVS.Providers
{
    internal class CommandProvider : BaseProvider<CommandProvider>
    {
        private DTE2 _dte;
        private CommandEvents _events;

        private static readonly string[] _ignoreCmd =
        {
            "Edit.GoToFindCombo",
            "Debug.LocationToolbar.ProcessCombo",
            "Debug.LocationToolbar.ThreadCombo",
            "Debug.LocationToolbar.StackFrameCombo",
            "Build.SolutionPlatforms",
            "Build.SolutionConfigurations",
            "Edit.Delete",
            "Edit.DeleteBackwards",
            "Edit.Undo",
            "Edit.Redo",
            "Edit.Duplicate",
            "Edit.Copy",
            "Edit.Paste",
            "Edit.Cut",
            "Edit.CharLeft",
            "Edit.CharLeftExtend",
            "Edit.CharRight",
            "Edit.CharRightExtend",
            "Edit.SelectionCancel",
            "Edit.LineDown",
            "Edit.LineUp",
            "Edit.LineEnd",
            "Edit.LineUpExtend",
            "Edit.LineStartExtend",
            "Edit.LineStart",
            "Edit.WordNext",
            "Edit.WordNextExtend",
            "Edit.WordPrevious",
            "Edit.WordPreviousExtend",
            "Edit.MoveSelectedLinesDown",
            "Edit.MoveSelectedLinesUp",
            "Edit.PageUp",
            "Edit.PageDown",
        };

        public override string Name => "Command";

        public override bool IsEnabled => General.Instance.EnableCommandProvider;

        protected override async Task InitializeAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            _dte = await VS.GetServiceAsync<DTE, DTE2>();
            _events = _dte.Events.CommandEvents;
            _events.BeforeExecute += OnBeforeExecute;
        }

        private void OnBeforeExecute(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (CustomIn is not null || CustomOut is not null)
            {
                return;
            }

            try
            {
                Command cmd = _dte.Commands.Item(Guid, ID);

                if (!string.IsNullOrWhiteSpace(cmd?.Name) && !ShouldCommandBeIgnored(cmd))
                {
                    WriteAsync("'" + cmd.Name + "' invoked").FireAndForget();
                }
            }
            catch (ArgumentException)
            {
            }
        }

        private static bool ShouldCommandBeIgnored(Command cmd)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            return _ignoreCmd.Contains(cmd.Name, StringComparer.OrdinalIgnoreCase);
        }
    }
}
