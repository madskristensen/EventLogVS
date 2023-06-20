using System.Collections.Generic;

namespace EventLogVS.Providers
{
    internal class StatusbarProvider : BaseProvider<StatusbarProvider>
    {
        private static string _lastStatusMessage;
        private static readonly List<string> _ignore = new()
        { "Ready" };

        public override string Name => "Status bar";

        protected override async Task InitializeAsync()
        {
            await Task.Yield();

            while (true)
            {
                await CheckStatusMessageAsync();
                await Task.Delay(1000);
            }
        }

        private async Task CheckStatusMessageAsync()
        {
            string text = (await VS.StatusBar.GetMessageAsync())?.Trim();

            if (!string.IsNullOrEmpty(text) &&
                !_ignore.Contains(text) &&
                text != _lastStatusMessage)
            {
                _lastStatusMessage = text;
                await WriteAsync(text);
            }
        }
    }
}
