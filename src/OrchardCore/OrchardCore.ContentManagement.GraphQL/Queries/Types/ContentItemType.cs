using System.IO;
using System.Text.Encodings.Web;
using GraphQL.Types;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Apis.GraphQL;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.GraphQL.Options;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;


namespace OrchardCore.ContentManagement.GraphQL.Queries.Types { 
    public class ContentItemType : ObjectType<ContentItem>
    {
        private readonly GraphQLContentOptions _options;
    
      //  public string Name { get; set; }

        protected override void Configure(IObjectTypeDescriptor<ContentItem> descriptor)
        {
      //      descriptor.Name("ContentItemType");
            descriptor.Field(ci => ci.ContentItemId).Description("Content item id");
            descriptor.Field(ci => ci.ContentItemVersionId).Description("The content item version id");
            descriptor.Field(ci => ci.ContentType).Description("Type of content");
            descriptor.Field(ci => ci.DisplayText).Description("The display text of the content item");
            descriptor.Field(ci => ci.Published).Description("Is the published version");
            descriptor.Field(ci => ci.Latest).Description("Is the latest version");
            descriptor.Field(ci => ci.ModifiedUtc).Description("The date and time of modification");
            //Field<DateTimeGraphType>("publishedUtc", resolve: ci => ci.Source.PublishedUtc, description: "The date and time of publication");
            //Field<DateTimeGraphType>("createdUtc", resolve: ci => ci.Source.CreatedUtc, description: "The date and time of creation");
            //Field(ci => ci.Owner).Description("The owner of the content item");
            //Field(ci => ci.Author).Description("The author of the content item");

            //Field<StringGraphType>()
            //    .Name("render")
            //    .ResolveAsync(async context =>
            //    {
            //        var userContext = (GraphQLContext)context.UserContext;
            //        var serviceProvider = userContext.ServiceProvider;

            //        // Build shape
            //        var displayManager = serviceProvider.GetRequiredService<IContentItemDisplayManager>();
            //        var updateModelAccessor = serviceProvider.GetRequiredService<IUpdateModelAccessor>();
            //        var model = await displayManager.BuildDisplayAsync(context.Source, updateModelAccessor.ModelUpdater);

            //        var displayHelper = serviceProvider.GetRequiredService<IDisplayHelper>();
            //        var htmlEncoder = serviceProvider.GetRequiredService<HtmlEncoder>();

            //        using (var sb = StringBuilderPool.GetInstance())
            //        {
            //            using (var sw = new StringWriter(sb.Builder))
            //            {
            //                var htmlContent = await displayHelper.ShapeExecuteAsync(model);
            //                htmlContent.WriteTo(sw, htmlEncoder);

            //                await sw.FlushAsync();
            //                return sw.ToString();
            //            }
            //        }
            //    });

        }


        public ContentItemType(IOptions<GraphQLContentOptions> optionsAccessor, string name, string description)
        {
            _options = optionsAccessor.Value;

            Name = name;
            Description = description;

            //Interface<ContentItemInterface>();

            //IsTypeOf = IsContentType;
        }

        private bool IsContentType(object obj)
        {
            return obj is ContentItem item && item.ContentType == Name;
        }

        //public override FieldType AddField(FieldType fieldType)
        //{
        //    if (!_options.ShouldSkip(this.GetType(), fieldType.Name))
        //    {
        //        return base.AddField(fieldType);
        //    }

        //    return null;
        //}
    }
}
