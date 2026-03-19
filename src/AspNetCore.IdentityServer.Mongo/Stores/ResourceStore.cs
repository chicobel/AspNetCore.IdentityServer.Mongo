namespace AspNetCore.IdentityServer.Mongo.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Duende.IdentityServer.Stores;
    using Microsoft.Extensions.Logging;
    using MongoDB.Driver;

    using AspNetCore.IdentityServer.Mongo.Interfaces;
    using AspNetCore.IdentityServer.Mongo.Mappers;

    /// <summary>
    /// Implementation of IResourceStore thats uses MongoDB.
    /// </summary>
    /// <seealso cref="IResourceStore" />
    public class ResourceStore : IResourceStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceStore"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public ResourceStore(IConfigurationDbContext context, ILogger<ResourceStore> logger, ILoggerFactory loggerFactory)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        protected ILoggerFactory LoggerFactory { get; private set; }

        /// <summary>
        /// The DbContext.
        /// </summary>
        protected IConfigurationDbContext Context { get; private set; }

        /// <summary>
        /// The logger.
        /// </summary>
        protected ILogger<ResourceStore> Logger { get; private set; }

        /// <summary>
        /// Finds the API resources by name.
        /// </summary>
        /// <param name="apiResourceNames">The names.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Duende.IdentityServer.Models.ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var names = apiResourceNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.Ordinal)
                .ToArray();

            if (names.Length == 0)
            {
                Logger.LogDebug("Did not find {Apis} API resource in database", apiResourceNames);
                return Array.Empty<Duende.IdentityServer.Models.ApiResource>();
            }

            var filter = Builders<Entities.ApiResource>.Filter.In(x => x.Name, names);

            var results = await Context.ApiResources
                .Find(filter)
                .ToListAsync()
                .ConfigureAwait(false);

            var models = results.Select(x => x.ToModel(LoggerFactory)).ToArray();

            if (models.Length > 0)
                Logger.LogDebug("Found {Apis} API resource in database", results.Select(x => x.Name));
            else
                Logger.LogDebug("Did not find {Apis} API resource in database", names);

            return models as IEnumerable<Duende.IdentityServer.Models.ApiResource>;
        }

        /// <summary>
        /// Gets API resources by scope name.
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Duende.IdentityServer.Models.ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            ArgumentNullException.ThrowIfNull(scopeNames);

            var names = scopeNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.Ordinal)
                .ToArray();

            if (names.Length == 0)
            {
                Logger.LogDebug("Found {Apis} API resources in database", Array.Empty<string>());
                return Array.Empty<Duende.IdentityServer.Models.ApiResource>();
            }

            // ApiResource.Scopes is List<ApiResourceScope> and ApiResourceScope has property Scope (string)
            var filter = Builders<Entities.ApiResource>.Filter.ElemMatch(
                x => x.Scopes,
                Builders<Entities.ApiResourceScope>.Filter.In(s => s.Scope, names)
            );

            var results = await Context.ApiResources
                .Find(filter)
                .ToListAsync()
                .ConfigureAwait(false);

            var models = results.Select(x => x.ToModel(LoggerFactory)).ToArray();

            Logger.LogDebug("Found {Apis} API resources in database", models.Select(x => x?.Name));


            return models as IEnumerable<Duende.IdentityServer.Models.ApiResource>;
        }

        /// <summary>
        /// Gets identity resources by scope name.
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Duende.IdentityServer.Models.IdentityResource>> FindIdentityResourcesByScopeNameAsyncOld(IEnumerable<string> scopeNames)
        {
            var scopes = scopeNames.ToArray();

            // var query = await Context.IdentityResources.FindAsync(x => scopes.Contains(x.Name));
            // var results = await query.ToListAsync();

            var filter = Builders<Entities.IdentityResource>
                .Filter
                .In(x => x.Name, scopes);

            var results = await Context.IdentityResources
                .Find(filter)
                .ToListAsync();

            Logger.LogDebug("Found {IdentityResources} identity scopes in database", results.Select(x => x.Name));

            return results.Select(x => x.ToModel(LoggerFactory)).ToArray() as IEnumerable<Duende.IdentityServer.Models.IdentityResource>;
        }

        public virtual async Task<IEnumerable<Duende.IdentityServer.Models.IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames is null) throw new ArgumentNullException(nameof(scopeNames));

            var scopes = scopeNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.Ordinal)
                .ToArray();

            if (scopes.Length == 0)
            {
                Logger.LogDebug("Found {IdentityResources} identity scopes in database", Array.Empty<string>());
                return Array.Empty<Duende.IdentityServer.Models.IdentityResource>();
            }

            var filter = Builders<Entities.IdentityResource>.Filter.In(x => x.Name, scopes);

            var results = await Context.IdentityResources
                .Find(filter)
                .ToListAsync()
                .ConfigureAwait(false);

            Logger.LogDebug("Found {IdentityResources} identity scopes in database", results.Select(x => x.Name));

            return results.Select(x => x.ToModel(LoggerFactory)).ToArray() as IEnumerable<Duende.IdentityServer.Models.IdentityResource>;
        }

        /// <summary>
        /// Gets scopes by scope name.
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Duende.IdentityServer.Models.ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames is null) throw new ArgumentNullException(nameof(scopeNames));

            var scopes = scopeNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.Ordinal)
                .ToArray();

            if (scopes.Length == 0)
            {
                Logger.LogDebug("Found {Scopes} scopes in database", Array.Empty<string>());
                return Array.Empty<Duende.IdentityServer.Models.ApiScope>();
            }

            var filter = Builders<Entities.ApiScope>.Filter.In(x => x.Name, scopes);

            var results = await Context.ApiScopes
                .Find(filter)
                .ToListAsync()
                .ConfigureAwait(false);

            Logger.LogDebug("Found {Scopes} scopes in database", results.Select(x => x.Name));


            return results.Select(x => x.ToModel(LoggerFactory)).ToArray() as IEnumerable<Duende.IdentityServer.Models.ApiScope>;
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Duende.IdentityServer.Models.Resources> GetAllResourcesAsync()
        {
            var identity = await (await Context.IdentityResources.FindAsync(_ => true)).ToListAsync();
            var apis = await (await Context.ApiResources.FindAsync(_ => true)).ToListAsync();
            var scopes = await (await Context.ApiScopes.FindAsync(_ => true)).ToListAsync();

            var result = new Duende.IdentityServer.Models.Resources(
                identity.Select(x => x.ToModel(LoggerFactory)) as IEnumerable<Duende.IdentityServer.Models.IdentityResource>,
                apis.Select(x => x.ToModel(LoggerFactory)) as IEnumerable<Duende.IdentityServer.Models.ApiResource>,
                scopes.Select(x => x.ToModel(LoggerFactory)) as IEnumerable<Duende.IdentityServer.Models.ApiScope>
            );

            Logger.LogDebug("Found {Scopes} as all scopes, and {Apis} as API resources",
                result.IdentityResources.Select(x => x.Name).Union(result.ApiScopes.Select(x => x.Name)),
                result.ApiResources.Select(x => x.Name));

            return result;
        }
    }
}
