using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Commands
{
    [ExcludeFromCodeCoverage]
    public class RebuildLinkDbCommand : SubcommandBase<RebuildLinkDbTask, RebuildLinkDbArgs>
    {
        public RebuildLinkDbCommand(IServiceProvider container)
          : base("rebuildlinkdb", "Start rebuilding a link db.", container)
        {
            AddOption(ArgOptions.Config);
            AddOption(ArgOptions.Database);
            AddOption(ArgOptions.Verbose);
            AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(RebuildLinkDbTask task, RebuildLinkDbArgs args)
        {
            args.Validate();
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}
