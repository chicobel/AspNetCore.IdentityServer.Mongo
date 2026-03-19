namespace AspNetCore.IdentityServer.Mongo.Mappers
{
    using AutoMapper;

    /// <summary>
    /// Extension methods to map to/from entity/model for identity providers.
    /// </summary>
    public static class IdentityProviderMappers
    {
        // static IdentityProviderMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityProviderMapperProfile>(),null)
        //         .CreateMapper();

        // internal static IMapper Mapper { get; }

        static Mapper GetMapper(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<IdentityProviderMapperProfile>(), loggerFactory);
            return new Mapper(config);
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Duende.IdentityServer.Models.IdentityProvider? ToModel(this Entities.IdentityProvider entity, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
            => entity == null ? null : GetMapper(loggerFactory).Map<Duende.IdentityServer.Models.IdentityProvider>(entity);

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Entities.IdentityProvider? ToEntity(this Duende.IdentityServer.Models.IdentityProvider model, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
            => model == null ? null : GetMapper(loggerFactory).Map<Entities.IdentityProvider>(model);
    }
}
