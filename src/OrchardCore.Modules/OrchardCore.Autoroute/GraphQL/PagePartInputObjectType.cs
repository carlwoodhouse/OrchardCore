using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.Apis.GraphQL.Queries;
using OrchardCore.Autoroute.Models;

namespace OrchardCore.Autoroute.GraphQL
{
    public class PagePartInputObjectType : WhereInputObjectGraphType<PageTypePart>
    {
        public PagePartInputObjectType(IStringLocalizer<PagePartInputObjectType> S)
        {
            Name = "PageTypePartInput";
            Description = S["the me"];

            AddScalarFilterFields<StringGraphType>("type", S["the page type of the content item to filter"]);
        }
    }
}
