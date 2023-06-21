using System.IO;
using System.Linq;

namespace EventLogVS.Providers
{
    internal class ProjectProvider : BaseProvider<ProjectProvider>
    {
        public override string Name => "Project";

        public override bool IsEnabled => General.Instance.EnableProjectProvider;

        protected override Task InitializeAsync()
        {
            VS.Events.ProjectItemsEvents.AfterAddProjectItems += (items) =>
            {
                Write($"Project item(s) added: ({string.Join(",", items.Select(i => Path.GetFileName(i.Name)))})");
            };

            VS.Events.ProjectItemsEvents.AfterRemoveProjectItems += (items) =>
            {
                Write($"Project item(s) removed: ({string.Join(",", items.ProjectItemRemoves.Select(i => Path.GetFileName(i.RemovedItemName)))})");
            };

            VS.Events.ProjectItemsEvents.AfterRenameProjectItems += (items) =>
            {
                Write($"Project item {Path.GetFileName(items.ProjectItemRenames[0].OldName)} renamed to {Path.GetFileName(items.ProjectItemRenames[0].SolutionItem.Name)}");
            };

            return Task.CompletedTask;
        }
    }
}
