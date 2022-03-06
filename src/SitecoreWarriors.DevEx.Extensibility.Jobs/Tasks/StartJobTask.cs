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
    public class StartJobTask
    {
        private readonly IStartJobService _startJobService;
        private readonly ILogger<StartJobTask> _logger;
        private readonly IEnvironmentConfigurationProvider _configurationProvider;

        public StartJobTask(
          ILogger<StartJobTask> logger,
          IEnvironmentConfigurationProvider configurationProvider,
          IStartJobService startJobService)
        {
            _startJobService = startJobService;
            _logger = logger;
            _configurationProvider = configurationProvider;
        }

        public async Task Execute(StartJobArgs args)
        {
            EnvironmentConfiguration configurationAsync = await _configurationProvider.GetEnvironmentConfigurationAsync(args.Config, args.EnvironmentName);
            Stopwatch stopwatch = Stopwatch.StartNew();
            string result = (await _startJobService.StartJobAsync(configurationAsync, args.JobName).ConfigureAwait(false)).ToString();
            _logger.LogTrace(string.Format("Start Job Response: Loaded in {0}ms ({1}).", stopwatch.ElapsedMilliseconds, args.JobName));
            ColorLogExtensions.LogConsoleInformation(_logger, $"Start Job:", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
            ColorLogExtensions.LogConsoleInformation(_logger, result, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
            stopwatch = null;
        }
    }
}
