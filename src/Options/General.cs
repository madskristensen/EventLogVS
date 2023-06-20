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
        [Category("Providers")]
        [DisplayName("Build")]
        [Description("Show information from the build events provider.")]
        [DefaultValue(true)]
        public bool EnableBuildProvider { get; set; } = true;

        [Category("Providers")]
        [DisplayName("Command")]
        [Description("Show information from the command execution provider.")]
        [DefaultValue(true)]
        public bool EnableCommandProvider { get; set; } = true;

        [Category("Providers")]
        [DisplayName("Debugger")]
        [Description("Show information from the debugger provider.")]
        [DefaultValue(true)]
        public bool EnableDebugProvider { get; set; } = true;

        [Category("Providers")]
        [DisplayName("Document")]
        [Description("Show information from the document events provider.")]
        [DefaultValue(true)]
        public bool EnableDocumentProvider { get; set; } = true;

        [Category("Providers")]
        [DisplayName("Project items")]
        [Description("Show information from the project items events provider.")]
        [DefaultValue(true)]
        public bool EnableProjectItemsProvider { get; set; } = true;

        [Category("Providers")]
        [DisplayName("Solution")]
        [Description("Show information from the solution events provider.")]
        [DefaultValue(true)]
        public bool EnableSolutionProvider { get; set; } = true;

        [Category("Providers")]
        [DisplayName("Tool windows")]
        [Description("Show information from the tool windows events provider.")]
        [DefaultValue(false)]
        public bool EnableWindowsProvider { get; set; }

        [Category("Providers")]
        [DisplayName("Shell")]
        [Description("Show information from the shell events provider.")]
        [DefaultValue(false)]
        public bool EnableShellProvider { get; set; }
    }
}
