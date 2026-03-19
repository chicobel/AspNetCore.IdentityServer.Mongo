namespace AspNetCore.IdentityServer.Mongo.Mappers
{
    using Amazon.Runtime.Internal.Util;

    using AutoMapper;
    using Microsoft.Extensions.Logging;


    /// <summary>
    /// Extension methods to map to/from entity/model for identity resources.
    /// </summary>
    public static class IdentityResourceMappers
    {
        // static IdentityResourceMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>(),null).CreateMapper();

        // internal static IMapper Mapper { get; }

        static IMapper GetMapper(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>(), loggerFactory);
            return new Mapper(config);
        }

         /// <summary>

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Duende.IdentityServer.Models.IdentityResource? ToModel(this Entities.IdentityResource entity, ILoggerFactory loggerFactory)
            => entity == null ? null : GetMapper(loggerFactory).Map<Duende.IdentityServer.Models.IdentityResource>(entity);

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Entities.IdentityResource? ToEntity(this Duende.IdentityServer.Models.IdentityResource model, ILoggerFactory loggerFactory)
            => model == null ? null : GetMapper(loggerFactory).Map<Entities.IdentityResource>(model);
    }
}
