using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Commands
{
    [ExcludeFromCodeCoverage]
    public class StartJobArgs : JobTaskOptions, IVerbosityArgs
    {
        public string JobName { get; set; }

        public override void Validate()
        {
            Require("JobName");
            Require("Config");
            Default("EnvironmentName", "default");
        }
    }
}
