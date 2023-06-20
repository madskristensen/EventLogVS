namespace EventLogVS.Providers
{
    internal class DebugProvider : BaseProvider<DebugProvider>
    {
        public override string Name => "Debugger";

        public override bool IsEnabled => General.Instance.EnableDebugProvider;

        protected override Task InitializeAsync()
        {
            VS.Events.DebuggerEvents.EnterBreakMode += () => { Write($"Enter break mode"); };
            VS.Events.DebuggerEvents.EnterDesignMode += () => { Write($"Enter design mode"); };
            VS.Events.DebuggerEvents.EnterEditAndContinueMode += () => { Write($"Enter edit and continue mode"); };
            VS.Events.DebuggerEvents.EnterRunMode += () => { Write($"Enter run mode"); };

            return Task.CompletedTask;
        }
    }
}
