using GraphQL.Types;
using SitecoreWarriors.DevEx.GraphQL.Jobs.GraphQL.Mutations;
using SitecoreWarriors.DevEx.GraphQL.Jobs.GraphQL.Queries;
using Sitecore.Services.GraphQL.Schemas;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SitecoreWarriors.DevEx.GraphQL.Jobs
{
    [ExcludeFromCodeCoverage]
    internal class JobSchemaProvider : SchemaProviderBase
    {
        public override IEnumerable<FieldType> CreateRootQueries()
        {
            yield return new JobsListQuery();
        }

        public override IEnumerable<FieldType> CreateRootMutations()
        {            
            yield return new StartJobMutation();
            yield return new RebuildLinkDbMutation();
        }
    }
}
