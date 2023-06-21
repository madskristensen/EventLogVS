using EnvDTE;
using EnvDTE80;

namespace EventLogVS.Providers
{
    internal class PublishProvider : BaseProvider<PublishProvider>
    {
        private PublishEvents _events;

        public override string Name => "Publish";

        public override bool IsEnabled => General.Instance.EnablePublishProvider;

        protected override async Task InitializeAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DTE2 dte = await VS.GetServiceAsync<DTE, DTE2>();
            _events = (dte.Events as Events2).PublishEvents;

            _events.OnPublishBegin += (ref bool Continue) => { Write("Publish begin"); };
            _events.OnPublishDone += (success) => { Write($"Publish done: {Result(success)}"); };
        }

        private static string Result(bool success)
        {
            return success ? "success" : "failed";
        }
    }
}
