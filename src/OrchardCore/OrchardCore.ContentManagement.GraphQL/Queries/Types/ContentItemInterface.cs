using HotChocolate.Types;

namespace OrchardCore.ContentManagement.GraphQL.Queries.Types
{
    public class ContentItemInterface : InterfaceType<ContentItem>
    {
     //   private readonly GraphQLContentOptions _options;

        //public ContentItemInterface(IOptions<GraphQLContentOptions> optionsAccessor)
        //{
        //    _options = optionsAccessor.Value;

        //    Name = "ContentItem";

        //    Field(ci => ci.ContentItemId);
        //    Field(ci => ci.ContentItemVersionId);
        //    Field(ci => ci.ContentType);
        //    Field(ci => ci.DisplayText, nullable: true);
        //    Field(ci => ci.Published);
        //    Field(ci => ci.Latest);
        //    Field<DateTimeGraphType>("modifiedUtc", resolve: ci => ci.Source.ModifiedUtc);
        //    Field<DateTimeGraphType>("publishedUtc", resolve: ci => ci.Source.PublishedUtc);
        //    Field<DateTimeGraphType>("createdUtc", resolve: ci => ci.Source.CreatedUtc);
        //    Field(ci => ci.Owner);
        //    Field(ci => ci.Author);
        //}

        protected override void Configure(IInterfaceTypeDescriptor<ContentItem> descriptor)
        {
            descriptor.Name("ContentItemInterface");
            descriptor.Ignore(ci => ci.Has(null));
        }

        //public override FieldType AddField(FieldType fieldType)
        //{
        //    if (!_options.ShouldSkip(typeof(ContentItemType), fieldType.Name))
        //    {
        //        return base.AddField(fieldType);
        //    }

        //    return null;
        //}
    }
}
