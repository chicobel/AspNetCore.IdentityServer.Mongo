namespace AspNetCore.IdentityServer.Mongo.Mappers
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;


    /// <summary>
    /// Extension methods to map to/from entity/model for API resources.
    /// </summary>
    public static class ApiResourceMappers
    {
        // static ApiResourceMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>()).CreateMapper(null);

        // internal static IMapper Mapper { get; }

        static Mapper GetMapper(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>(), loggerFactory);
            return new Mapper(config);
        }

         /// <summary>

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Duende.IdentityServer.Models.ApiResource? ToModel(this Entities.ApiResource? entity, ILoggerFactory loggerFactory)
            => entity == null ? null : GetMapper(loggerFactory).Map<Duende.IdentityServer.Models.ApiResource>(entity);

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static Entities.ApiResource? ToEntity(this Duende.IdentityServer.Models.ApiResource? model, ILoggerFactory loggerFactory)
            => model == null ? null : GetMapper(loggerFactory).Map<Entities.ApiResource>(model);
    }
}
