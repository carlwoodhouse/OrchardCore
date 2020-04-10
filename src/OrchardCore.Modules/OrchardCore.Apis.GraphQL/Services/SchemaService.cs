using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using OrchardCore.Environment.Shell.Scope;

namespace OrchardCore.Apis.GraphQL.Services
{
    public class SchemaService : ISchemaFactory
    {
        private readonly IEnumerable<ISchemaBuilderProvider> _schemaBuilders;
        private readonly SemaphoreSlim _schemaGenerationSemaphore = new SemaphoreSlim(1, 1);
        private readonly ConcurrentDictionary<ISchemaBuilderProvider, IChangeToken> _changeTokens = new ConcurrentDictionary<ISchemaBuilderProvider, IChangeToken>();

        private ISchema _schema;

        public SchemaService(IEnumerable<ISchemaBuilderProvider> schemaBuilders)
        {
            _schemaBuilders = schemaBuilders;
        }

        public async Task<ISchema> GetSchemaAsync()
        {
            if (_schema is object && !_changeTokens.Values.Any(x => x.HasChanged))
            {
                return _schema;
            }

            await _schemaGenerationSemaphore.WaitAsync();

            try
            {
                if (_schema is object && !_changeTokens.Values.Any(x => x.HasChanged))
                {
                    return _schema;
                }

                var serviceProvider = ShellScope.Services;

                var schema = SchemaBuilder.New().AddQueryType(d => d.Name("Query"));

                //var schema = new Schema
                //{
                //    Query = new ObjectGraphType { Name = "Query" },
                //    Mutation = new ObjectGraphType { Name = "Mutation" },
                //    Subscription = new ObjectGraphType { Name = "Subscription" },
                //    FieldNameConverter = new OrchardFieldNameConverter(),
                //    DependencyResolver = serviceProvider.GetService<IDependencyResolver>()
                //};

                foreach (var builder in _schemaBuilders)
                {
                    var token = await builder.BuildAsync(schema);

                    if (token is object)
                    {
                        _changeTokens[builder] = token;
                    }
                }

                //foreach (var type in serviceProvider.GetServices<IInputObjectGraphType>())
                //{
                //    schema.RegisterType(type);
                //}

                //foreach (var type in serviceProvider.GetServices<IObjectGraphType>())
                //{
                //    schema.RegisterType(type);
                //}

                // Clean Query, Mutation and Subscription if they have no fields
                // to prevent GraphQL configuration errors.

                //if (!schema.Query.Fields.Any())
                //{
                //    schema.Query = null;
                //}

                //if (!schema.Mutation.Fields.Any())
                //{
                //    schema.Mutation = null;
                //}

                //if (!schema.Subscription.Fields.Any())
                //{
                //    schema.Subscription = null;
                //}

                return _schema = schema.Create();
            }
            finally
            {
                _schemaGenerationSemaphore.Release();
            }
        }
    }
}
