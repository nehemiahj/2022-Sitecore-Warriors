﻿using GraphQL.Common.Request;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Datasources.Sc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Jobs.Client.Services
{
    public class JobListService : IJobListService
    {
        private readonly ISitecoreApiClient _apiClient;

        public JobListService(ISitecoreApiClient apiClient) => _apiClient = apiClient;

        public Task<IEnumerable<string>> GetJobListAsync(
          EnvironmentConfiguration configuration,
          CancellationToken cancellationToken = default(CancellationToken))
        {
            ISitecoreApiClient apiClient = _apiClient;
            if (apiClient.Endpoint == null)
            {
                apiClient.Endpoint = configuration;
            }
            return _apiClient.RunQuery<IEnumerable<string>>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "\nquery{\n  jobsList\n}",
                Variables = new Dictionary<string, object>()
            }, "jobsList", cancellationToken);
        }
    }
}
