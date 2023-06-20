namespace EventLogVS.Providers
{
    internal class WindowProvider : BaseProvider<WindowProvider>
    {
        public override string Name => "Tool windows";

        public override bool IsEnabled => General.Instance.EnableWindowsProvider;

        protected override Task InitializeAsync()
        {
            VS.Events.WindowEvents.ActiveFrameChanged += (window) =>
            {
                if (window.OldFrame.Caption != window.NewFrame.Caption)
                {
                    Write($"Active frame changed from {window.OldFrame.Caption} to {window.NewFrame.Caption}");
                }
            };

            VS.Events.WindowEvents.Destroyed += (window) => { Write($"Window closed: {window.Caption}"); };
            VS.Events.WindowEvents.FrameIsOnScreenChanged += (window) => { Write($"Frame is on screen changed: {window.Frame.Caption}"); };

            // These events are noisy and not very useful
            //VS.Events.WindowEvents.Created += (window) => { Write($"Window created: {window.Caption}"); };
            //VS.Events.WindowEvents.FrameIsVisibleChanged += (window) => { Write($"Frame is on screen changed. Is visible: {window.IsNewVisible}"); };

            return Task.CompletedTask;
        }
    }
}
