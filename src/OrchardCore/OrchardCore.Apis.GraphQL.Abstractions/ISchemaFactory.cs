using System.Threading.Tasks;
using HotChocolate;

namespace OrchardCore.Apis.GraphQL
{
    /// <summary>
    /// Represents a service that provides the <see cref="ISchema"/> instance that is used in a GraphQL request.
    /// The result should be cached and reused when possible.
    /// </summary>
    public interface ISchemaFactory
    {
        Task<ISchema> GetSchemaAsync();
    }
}
