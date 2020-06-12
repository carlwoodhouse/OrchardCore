using HotChocolate.Types;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Scope;

namespace OrchardCore.ContentManagement.GraphQL.Queries.Types
{
    public class ContentItemQueryTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            var S = ShellScope.Services.GetService<IStringLocalizer<ContentItemQueryTypeExtension>>();

            descriptor.Name("Query");
            descriptor.Field("contentItem")
                .Description(S["Content items are instances of content types, just like objects are instances of classes."])
                .Type<ContentItemInterface>()
                .Argument("contentItemId", a => a.Type<NonNullType<StringType>>().Description(S["A content item id to query for"]))
                .Resolver(context => context.Service<IContentManager>().GetAsync(context.Argument<string>("contentItemId")));
        }
    }
}
