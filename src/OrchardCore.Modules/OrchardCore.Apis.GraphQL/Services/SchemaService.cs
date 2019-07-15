using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace OrchardCore.Apis.GraphQL.Services
{
    public class SchemaService : ISchemaFactory
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IEnumerable<ISchemaBuilder> _schemaBuilders;
        private readonly IServiceProvider _serviceProvider;
        private static SemaphoreSlim _schemaGenerationSemaphore = new SemaphoreSlim(1, 1);

        public SchemaService(
            IMemoryCache memoryCache,
            IEnumerable<ISchemaBuilder> schemaBuilders,
            IServiceProvider serviceProvider)
        {
            _memoryCache = memoryCache;
            _schemaBuilders = schemaBuilders;
            _serviceProvider = serviceProvider;
        }

        public async Task<ISchema> GetSchema()
        {
            if (_memoryCache.TryGetValue<ISchema>("GraphQLSchema", out var schema)) {
                return schema;
            }

            await _schemaGenerationSemaphore.WaitAsync();

            if (_memoryCache.TryGetValue("GraphQLSchema", out schema))
            {
                return schema;
            }

            try
            {
                schema = await _memoryCache.GetOrCreateAsync("GraphQLSchema", async f =>
                {
                    f.SetSlidingExpiration(TimeSpan.FromHours(1));

                    schema = new Schema
                    {
                        Query = new ObjectGraphType { Name = "Query" },
                        Mutation = new ObjectGraphType { Name = "Mutation" },
                        Subscription = new ObjectGraphType { Name = "Subscription" },
                        FieldNameConverter = new OrchardFieldNameConverter(),

                        DependencyResolver = _serviceProvider.GetService<IDependencyResolver>()
                    };

                    foreach (var builder in _schemaBuilders)
                    {
                        var token = await builder.BuildAsync(schema);

                        if (token != null)
                        {
                            f.AddExpirationToken(token);
                        }
                    }

                    foreach (var type in _serviceProvider.GetServices<IInputObjectGraphType>())
                    {
                        schema.RegisterType(type);
                    }

                    foreach (var type in _serviceProvider.GetServices<IObjectGraphType>())
                    {
                        schema.RegisterType(type);
                    }

                // Clean Query, Mutation and Subscription if they have no fields
                // to prevent GraphQL configuration errors.

                if (!schema.Query.Fields.Any())
                    {
                        schema.Query = null;
                    }

                    if (!schema.Mutation.Fields.Any())
                    {
                        schema.Mutation = null;
                    }

                    if (!schema.Subscription.Fields.Any())
                    {
                        schema.Subscription = null;
                    }

                    schema.Initialize();

                    return schema;
                });
            }
            finally
            {
                _schemaGenerationSemaphore.Release();
            }

            return schema;
        }
    }
}
