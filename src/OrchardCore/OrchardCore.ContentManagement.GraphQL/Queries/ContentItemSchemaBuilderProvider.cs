using System.Threading.Tasks;
using HotChocolate;
using Microsoft.Extensions.Primitives;
using OrchardCore.Apis.GraphQL;
using OrchardCore.ContentManagement.GraphQL.Queries.Types;

namespace OrchardCore.ContentManagement.GraphQL.Queries
{
    public class ContentItemSchemaBuilderProvider : ISchemaBuilderProvider
    {
        public Task<IChangeToken> BuildAsync(ISchemaBuilder schema)
        {
            schema.AddType<ContentItemInterface>();
            schema.AddType<ContentItemType>();
            schema.AddType<ContentItemQueryTypeExtension>();

            return Task.FromResult<IChangeToken>(null);
        }
    }
}
