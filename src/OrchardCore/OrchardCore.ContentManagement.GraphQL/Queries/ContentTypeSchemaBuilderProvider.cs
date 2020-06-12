using System.Threading.Tasks;
using HotChocolate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using OrchardCore.Apis.GraphQL;
using OrchardCore.ContentManagement.GraphQL.Queries.Types;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Environment.Shell.Scope;

namespace OrchardCore.ContentManagement.GraphQL.Queries
{
    /// <summary>
    /// Registers all Content Types as queries.
    /// </summary>
    public class ContentTypeSchemaBuilderProvider : ISchemaBuilderProvider
    {

        public Task<IChangeToken> BuildAsync(ISchemaBuilder schema)
        {
            var serviceProvider = ShellScope.Services;

            var contentDefinitionManager = serviceProvider.GetService<IContentDefinitionManager>();
            schema.AddType<ContentTypeQueryTypeExtension>();
            //            var contentTypeBuilders = serviceProvider.GetServices<IContentTypeBuilder>().ToList();

            var changeToken = contentDefinitionManager.ChangeToken;

            //foreach (var typeDefinition in contentDefinitionManager.ListTypeDefinitions())
            //{
            //    //var typeType = new ContentItemType(_contentOptionsAccessor)
            //    //{
            //    //    Name = typeDefinition.Name,
            //    //    Description = S["Represents a {0}.", typeDefinition.DisplayName]
            //    //};

            //    //var query = new ContentItemsFieldType(typeDefinition.Name, schema, _contentOptionsAccessor, _settingsAccessor)
            //    //{
            //    //    Name = typeDefinition.Name,
            //    //    Description = S["Represents a {0}.", typeDefinition.DisplayName],
            //    //    ResolvedType = new ListGraphType(typeType)
            //    //};

            //    //query.RequirePermission(CommonPermissions.ViewContent, typeDefinition.Name);

            //    //foreach (var builder in contentTypeBuilders)
            //    //{
            //    //    builder.Build(query, typeDefinition, typeType);
            //    //}

            //    //var settings = typeDefinition.GetSettings<ContentTypeSettings>();

            //    //// Only add queries over standard content types
            //    //if (settings == null || String.IsNullOrWhiteSpace(settings.Stereotype))
            //    //{
            //    //    schema.Query.AddField(query);
            //    //}
            //    //else
            //    //{
            //    //    // Register the content item type explicitly since it won't be discovered from the root 'query' type.
            //    //    schema.RegisterType(typeType);
            //  //  }
            //}

            //foreach (var builder in contentTypeBuilders)
            //{
            //    builder.Clear();
            //}

            return Task.FromResult(changeToken);
        }
    }
}
