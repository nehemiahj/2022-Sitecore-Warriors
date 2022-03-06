using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Commands
{
    [ExcludeFromCodeCoverage]
    public class RebuildLinkDbArgs : JobTaskOptions, IVerbosityArgs
    {
        public string Database { get; set; }

        public override void Validate()
        {
            Require("Config");            
            Default("EnvironmentName", "default");
        }
    }
}
