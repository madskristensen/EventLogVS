namespace EventLogVS.Providers
{
    internal class BuildProvider : BaseProvider<BuildProvider>
    {
        public override string Name => "Build";

        public override bool IsEnabled => General.Instance.EnableBuildProvider;

        protected override Task InitializeAsync()
        {
            VS.Events.BuildEvents.ProjectBuildDone += (args) => { Write($"Project build done: {args.Project.Name} ({Result(args.IsSuccessful)})"); };
            VS.Events.BuildEvents.ProjectBuildStarted += (project) => { Write($"Project build started: {project.Name}"); };
            VS.Events.BuildEvents.ProjectCleanDone += (args) => { Write($"Project clean done: {args.Project.Name} ({Result(args.IsSuccessful)})"); };
            VS.Events.BuildEvents.ProjectCleanStarted += (project) => { Write($"Project clean started: {project.Name}"); };
            VS.Events.BuildEvents.ProjectConfigurationChanged += (project) => { Write($"Project configuration changed: {project.Name}"); };

            VS.Events.BuildEvents.SolutionBuildCancelled += () => { Write("Solution build cancelled"); };
            VS.Events.BuildEvents.SolutionBuildStarted += (s, e) => { Write("Solution build started"); };
            VS.Events.BuildEvents.SolutionBuildDone += (success) => { Write($"Solution build done: ({Result(success)})"); };
            VS.Events.BuildEvents.SolutionConfigurationChanged += () => { Write("Solution configuration changed"); };
            
            return Task.CompletedTask;
        }

        private static string Result(bool success)
        {
            return success ? "success" : "failed";
        }
    }
}
