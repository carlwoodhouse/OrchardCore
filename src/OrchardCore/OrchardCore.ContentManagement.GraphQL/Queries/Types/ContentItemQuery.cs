using System;
using System.Collections.Generic;
using System.Text;
using HotChocolate.Types;

namespace OrchardCore.ContentManagement.GraphQL.Queries.Types
{

    public class ContentItemQueryTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("Query");
            descriptor.Field("contentItem")
                .Type<ContentItemInterface>()
                .Argument("contentItemId", a => a.Type<NonNullType<StringType>>().Description("ContentItemId"))
                .Resolver(context => context.Service<IContentManager>().GetAsync(context.Argument<string>("contentItemId")));
        }
    }

    public class ContentItemType : ObjectType<ContentItem>
    {
        protected override void Configure(IObjectTypeDescriptor<ContentItem> descriptor)
        {
            descriptor.Implements<ContentItemInterface>();
        }
    }
}
