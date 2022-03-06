using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using Sitecore.DevEx.Configuration.Models;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Commands;
using SitecoreWarriors.DevEx.Jobs.Client.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks
{
    public class RebuildLinkDbTask
    {
        private readonly IRebuildLinkDbService _rebuildLinkDbService;
        private readonly ILogger<RebuildLinkDbTask> _logger;
        private readonly IEnvironmentConfigurationProvider _configurationProvider;

        public RebuildLinkDbTask(
          ILogger<RebuildLinkDbTask> logger,
          IEnvironmentConfigurationProvider configurationProvider,
          IRebuildLinkDbService startJobService)
        {
            _rebuildLinkDbService = startJobService;
            _logger = logger;
            _configurationProvider = configurationProvider;
        }

        public async Task Execute(RebuildLinkDbArgs args)
        {
            EnvironmentConfiguration configurationAsync = await _configurationProvider.GetEnvironmentConfigurationAsync(args.Config, args.EnvironmentName);
            Stopwatch stopwatch = Stopwatch.StartNew();
            string result = (await _rebuildLinkDbService.RebuildLinkDbStartJobAsync(configurationAsync, args.Database).ConfigureAwait(false)).ToString();
            _logger.LogTrace(string.Format("Rebuild Link DB Response: Loaded in {0}ms ({1}).", stopwatch.ElapsedMilliseconds, args.Database));
            ColorLogExtensions.LogConsoleInformation(_logger, $"Rebuild Link DB:", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
            ColorLogExtensions.LogConsoleInformation(_logger, result, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
            stopwatch = null;
        }
    }
}
