using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.Autoroute.Models;

namespace OrchardCore.Autoroute.GraphQL
{
    public class PagePartQueryObjectType : ObjectGraphType<PageTypePart>
    {
        public PagePartQueryObjectType(IStringLocalizer<PagePartQueryObjectType> S)
        {
            Name = "PageTypePart";
            Description = S["Custom URLs (permalinks) for your content item."];

            Field(x => x.Type).Description(S["The permalinks for your content item."]);
        }
    }
}
