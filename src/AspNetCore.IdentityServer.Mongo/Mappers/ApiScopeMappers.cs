namespace AspNetCore.IdentityServer.Mongo.Mappers
{
    using Amazon.Runtime.Internal.Util;

    using AutoMapper;
    using Microsoft.Extensions.Logging;


    /// <summary>
    /// Extension methods to map to/from entity/model for api scopes.
    /// </summary>
    public static class ScopeMappers
    {
        // static ScopeMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiScopeMapperProfile>(),null).CreateMapper(null);

        // internal static IMapper Mapper { get; }

        static IMapper GetMapper(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ApiScopeMapperProfile>(), loggerFactory);
            return new Mapper(config);
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Duende.IdentityServer.Models.ApiScope? ToModel(this Entities.ApiScope entity, ILoggerFactory loggerFactory)
            => entity == null ? null : GetMapper(loggerFactory).Map<Duende.IdentityServer.Models.ApiScope>(entity);

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Entities.ApiScope? ToEntity(this Duende.IdentityServer.Models.ApiScope model, ILoggerFactory loggerFactory)
            => model == null ? null : GetMapper(loggerFactory).Map<Entities.ApiScope>(model);
    }
}
