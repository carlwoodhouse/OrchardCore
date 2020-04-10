using System.Threading.Tasks;
using HotChocolate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using OrchardCore.Apis.GraphQL;
using OrchardCore.ContentManagement.GraphQL.Queries.Types;

namespace OrchardCore.ContentManagement.GraphQL.Queries
{
    public class ContentItemProvider : ISchemaBuilderProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer S;

        public ContentItemProvider(IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<ContentItemProvider> localizer)
        {
            _httpContextAccessor = httpContextAccessor;

            S = localizer;
        }

        public Task<IChangeToken> BuildAsync(ISchemaBuilder schema)
        {
            schema.AddType<ContentItemInterface>();
            schema.AddType<ContentItemType>();
            schema.AddType<ContentItemQueryTypeExtension>();


            //var field = new FieldType
            //{
            //    Name = "ContentItem",
            //    Description = S["Content items are instances of content types, just like objects are instances of classes."],
            //    Type = typeof(ContentItemInterface),
            //    Arguments = new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>>
            //        {
            //            Name = "contentItemId",
            //            Description = S["Content item id"]
            //        }
            //    ),
            //    Resolver = new AsyncFieldResolver<ContentItem>(ResolveAsync)
            //};

            //schema.Query.AddField(field);

            return Task.FromResult<IChangeToken>(null);
        }
    }
}
