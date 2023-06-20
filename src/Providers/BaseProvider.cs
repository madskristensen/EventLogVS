namespace EventLogVS.Providers
{
    internal abstract class BaseProvider<T> where T : BaseProvider<T>, new()
    {
        private OutputWindowPane Pane { get; set; }

        public abstract string Name { get; }

        public abstract bool IsEnabled { get; }

        public static async Task InitializeAsync(OutputWindowPane pane)
        {
            T provider = new()
            {
                Pane = pane
            };

            await provider.InitializeAsync();
        }

        protected abstract Task InitializeAsync();

        protected void Write(string text)
        {
            WriteAsync(text).FireAndForget();
        }

        protected async Task WriteAsync(string text)
        {
            if (IsEnabled)
            {
                await Pane.WriteLineAsync($"{DateTime.Now:T}\t[{Name}] {text}");
            }
        }
    }
}
