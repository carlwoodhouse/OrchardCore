using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Environment.Shell.Scope;

namespace OrchardCore.ContentManagement.GraphQL.Queries.Types
{
    public class ContentTypeQueryTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            var serviceProvider = ShellScope.Services;

            var S = serviceProvider.GetService<IStringLocalizer<ContentItemQueryTypeExtension>>();
            var contentDefinitionManager = serviceProvider.GetService<IContentDefinitionManager>(); 

            descriptor.Name("Query");

            foreach(var contentDefinition in contentDefinitionManager.ListTypeDefinitions())
            {
                descriptor.Field(contentDefinition.Name)
                  .Description(S["Content items are instances of content types, just like objects are instances of classes."])
                  .Type<ContentItemInterface>()
                  .Argument("contentItemId", a => a.Type<NonNullType<StringType>>().Description(S["A content item id to query for"]))
                  .Resolver(context => context.Service<IContentManager>().GetAsync(context.Argument<string>("contentItemId")));
            }     
        }
    }
}
