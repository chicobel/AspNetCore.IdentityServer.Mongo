namespace AspNetCore.IdentityServer.Mongo.Mappers
{
    using AutoMapper;

    /// <summary>
    /// Extension methods to map to/from entity/model for clients.
    /// </summary>
    public static class ClientMappers
    {
        // static ClientMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>()).CreateMapper();

        // internal static IMapper Mapper { get; }

        static Mapper GetMapper(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>(), loggerFactory);
            return new Mapper(config);
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Duende.IdentityServer.Models.Client? ToModel(this Entities.Client? entity, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
            => entity == null ? null : GetMapper(loggerFactory).Map<Duende.IdentityServer.Models.Client>(entity);

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Entities.Client? ToEntity(this Duende.IdentityServer.Models.Client? model, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
            => model == null ? null : GetMapper(loggerFactory).Map<Entities.Client>(model);
    }
}
