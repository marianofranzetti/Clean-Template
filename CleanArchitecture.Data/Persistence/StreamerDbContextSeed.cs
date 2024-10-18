using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers.Any())
            {
                context.Streamers.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("se inserto los recors al db {context}", typeof(StreamerDbContext).Name);
            }
        }

        public static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>()
            {
                new Streamer () {  CreatedBy = "mariano", Nombre = "mariano andres", Url = "http://www.prueba.com"},
                new Streamer () {  CreatedBy = "juan", Nombre = "juan martin", Url = "http://www.pruebajuan.com"},
            };


        }
    }
}
