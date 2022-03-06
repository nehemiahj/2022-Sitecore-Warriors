using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using Sitecore.DevEx.Configuration.Models;
using SitecoreWarriors.DevEx.Jobs.Client.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks
{
    public class JobListTask
    {
        private readonly ILogger<JobListTask> _logger;
        private readonly IJobListService _jobListService;
        private readonly IEnvironmentConfigurationProvider _configurationProvider;

        public JobListTask(
          IJobListService jobListService,
          ILogger<JobListTask> logger,
          IEnvironmentConfigurationProvider configurationProvider)
        {
            _jobListService = jobListService;
            _logger = logger;
            _configurationProvider = configurationProvider;
        }

        public async Task Execute(JobTaskOptions args)
        {
            EnvironmentConfiguration configurationAsync = await _configurationProvider.GetEnvironmentConfigurationAsync(args.Config, args.EnvironmentName);
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<string> list = (await _jobListService.GetJobListAsync(configurationAsync).ConfigureAwait(false)).ToList<string>();
            _logger.LogTrace(string.Format("Jobs List: Loaded in {0}ms ({1} jobs).", stopwatch.ElapsedMilliseconds, list.Count));
            ColorLogExtensions.LogConsoleInformation(_logger, $"Jobs list: (Count:{list.Count})", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
            foreach (string str in list)
                ColorLogExtensions.LogConsoleInformation(_logger, str, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
            stopwatch = null;
        }
    }
}
