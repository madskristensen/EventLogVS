using System.IO;

namespace EventLogVS.Providers
{
    internal class SolutionProvider : BaseProvider<SolutionProvider>
    {
        public override string Name => "Solution";

        public override bool IsEnabled => General.Instance.EnableSolutionProvider;

        protected override Task InitializeAsync()
        {
            VS.Events.SolutionEvents.OnAfterBackgroundSolutionLoadComplete += () => { Write($"Solution loaded"); };
            VS.Events.SolutionEvents.OnAfterCloseFolder += (folder) => { Write($"Project closed: {Path.GetDirectoryName(folder)}"); };
            VS.Events.SolutionEvents.OnAfterCloseSolution += () => { Write($"Solution closed"); };
            VS.Events.SolutionEvents.OnAfterLoadProject += (project) => { Write($"Project loaded: {project.Text}"); };
            VS.Events.SolutionEvents.OnAfterMergeSolution += () => { Write($"Solution merged"); };
            VS.Events.SolutionEvents.OnAfterOpenFolder += (folder) => { Write($"Project opened: {Path.GetDirectoryName(folder)}"); };
            VS.Events.SolutionEvents.OnAfterOpenProject += (project) => { Write($"Project opened: {project.Text}"); };
            VS.Events.SolutionEvents.OnAfterOpenSolution += (solution) => { Write($"Solution {solution.Text} opened"); };
            VS.Events.SolutionEvents.OnAfterRenameProject += (project) => { Write($"Project renamed: {project.Text}"); };
            VS.Events.SolutionEvents.OnBeforeCloseFolder += (folder) => { Write($"Project closing: {Path.GetDirectoryName(folder)}"); };
            VS.Events.SolutionEvents.OnBeforeCloseProject += (project) => { Write($"Project closing: {project.Text}"); };
            VS.Events.SolutionEvents.OnBeforeCloseSolution += () => { Write($"Solution closing"); };
            VS.Events.SolutionEvents.OnBeforeOpenProject += (project) => { Write($"Project opening: {Path.GetFileName(project)}"); };
            VS.Events.SolutionEvents.OnBeforeOpenSolution += (solution) => { Write($"Solution opening: {Path.GetFileName(solution)}"); };
            VS.Events.SolutionEvents.OnBeforeUnloadProject += (project) => { Write($"Project unloading: {project.Text}"); };

            return Task.CompletedTask;
        }
    }
}
