using System.ComponentModel;
using System.Runtime.InteropServices;

namespace EventLogVS
{
    internal partial class OptionsProvider
    {
        [ComVisible(true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>
    {
        private const string _category = "Event Providers";

        [Category(_category)]
        [DisplayName("Build")]
        [Description("Show information from the build events provider.")]
        [DefaultValue(true)]
        public bool EnableBuildProvider { get; set; } = true;

        [Category(_category)]
        [DisplayName("Command")]
        [Description("Show information from the command execution provider.")]
        [DefaultValue(true)]
        public bool EnableCommandProvider { get; set; } = true;

        [Category(_category)]
        [DisplayName("Debugger")]
        [Description("Show information from the debugger provider.")]
        [DefaultValue(true)]
        public bool EnableDebugProvider { get; set; } = true;

        [Category(_category)]
        [DisplayName("Document")]
        [Description("Show information from the document events provider.")]
        [DefaultValue(true)]
        public bool EnableDocumentProvider { get; set; } = true;

        [Category(_category)]
        [DisplayName("Project")]
        [Description("Show information from the project events provider.")]
        [DefaultValue(true)]
        public bool EnableProjectProvider { get; set; } = true;

        [Category(_category)]
        [DisplayName("Publish")]
        [Description("Show information from the publish events provider.")]
        [DefaultValue(true)]
        public bool EnablePublishProvider { get; set; } = true;

        [Category(_category)]
        [DisplayName("Solution")]
        [Description("Show information from the solution events provider.")]
        [DefaultValue(true)]
        public bool EnableSolutionProvider { get; set; } = true;

        [Category(_category)]
        [DisplayName("Shell")]
        [Description("Show information from the shell events provider.")]
        [DefaultValue(false)]
        public bool EnableShellProvider { get; set; }

        [Category(_category)]
        [DisplayName("Tool windows")]
        [Description("Show information from the tool windows events provider.")]
        [DefaultValue(false)]
        public bool EnableWindowsProvider { get; set; }
    }
}
