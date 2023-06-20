using System.IO;

namespace EventLogVS.Providers
{
    internal class DocumentProvider : BaseProvider<DocumentProvider>
    {
        public override string Name => "Document";

        public override bool IsEnabled => General.Instance.EnableDocumentProvider;

        protected override Task InitializeAsync()
        {
            VS.Events.DocumentEvents.AfterDocumentWindowHide += (document) => { Write($"Document window hidden: {document.WindowFrame.Caption}"); };
            VS.Events.DocumentEvents.BeforeDocumentWindowShow += (document) => { Write($"Document window shown: {document.WindowFrame.Caption}"); };
            VS.Events.DocumentEvents.Closed += (document) => { Write($"Document closing: {Path.GetFileName(document)}"); };
            VS.Events.DocumentEvents.Opened += (document) => { Write($"Document opened: {Path.GetFileName(document)}"); };
            VS.Events.DocumentEvents.Saved += (document) => { Write($"Document saved: {Path.GetFileName(document)}"); };
            
            return Task.CompletedTask;
        }
    }
}
