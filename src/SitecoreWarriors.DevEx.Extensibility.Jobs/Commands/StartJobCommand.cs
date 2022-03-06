using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Commands
{
    [ExcludeFromCodeCoverage]
    public class StartJobCommand : SubcommandBase<StartJobTask, StartJobArgs>
    {
        public StartJobCommand(IServiceProvider container)
          : base("start", "Start a db task.", container)
        {
            AddOption(ArgOptions.Config);
            AddOption(ArgOptions.JobName);
            AddOption(ArgOptions.Verbose);
            AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(StartJobTask task, StartJobArgs args)
        {
            args.Validate();
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}
