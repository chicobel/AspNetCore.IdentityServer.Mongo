namespace AspNetCore.IdentityServer.Mongo.Mappers
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;


    /// <summary>
    /// Extension methods to map to/from entity/model for persisted grants.
    /// </summary>
    public static class PersistedGrantMappers
    {
        // static PersistedGrantMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PersistedGrantMapperProfile>()).CreateMapper(null);

        // internal static IMapper Mapper { get; }

        static Mapper GetMapper(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PersistedGrantMapperProfile>(), loggerFactory);
            return new Mapper(config);
        }

         /// <summary>
         /// Maps an entity to a model.
         /// </summary>
         /// <param name="entity">The entity.</param>
         /// <param name="loggerFactory">The logger factory.</param>
         /// <returns></returns>

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Duende.IdentityServer.Models.PersistedGrant? ToModel(this Entities.PersistedGrant entity, ILoggerFactory loggerFactory)
            => entity == null ? null : GetMapper(loggerFactory).Map<Duende.IdentityServer.Models.PersistedGrant>(entity);

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Entities.PersistedGrant? ToEntity(this Duende.IdentityServer.Models.PersistedGrant model, ILoggerFactory loggerFactory)
            => model == null ? null : GetMapper(loggerFactory).Map<Entities.PersistedGrant>(model);

        /// <summary>
        /// Updates an entity from a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public static void UpdateEntity(this Duende.IdentityServer.Models.PersistedGrant model, Entities.PersistedGrant entity, ILoggerFactory loggerFactory)
            => GetMapper(loggerFactory).Map(model, entity);
    }
}
