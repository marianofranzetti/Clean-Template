using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext streamerDbContext = new();

//await AddNewRecords();
//QueryStreaming();
//await QueryMethods();
//await QueryLinq();
//await TrackingAndNotTracking();
//await AddNewStreamerWithVideo();
//await AddVideoWithActor();
//await AddNewDirectorWithVideo();
await MultipleEntitiesQuery();

async Task MultipleEntitiesQuery()
{
    var videoWithActores = await streamerDbContext.videos.Include(x => x.Actores)
    .Include(x => x.Streamer)
    .Include(x => x.Director)/*.Select(x => 
        new
        {
            Nombre = x.Nombre,
            
        })*/
    .FirstOrDefaultAsync(q => q.Id == 1);
    Console.WriteLine("la pelicula: " + videoWithActores.Nombre + 
        " tiene a los actores");

    if (videoWithActores is not null && videoWithActores.Actores is not null
        && videoWithActores.Actores.Count > 0)
    {
        foreach (var actor in videoWithActores.Actores)
        {
            Console.WriteLine(actor.Nombre + ", ");
        }
    }
    if (videoWithActores.Director is not null
        && videoWithActores.Director.Nombre is not null)
    {
        Console.WriteLine("con el director: " + videoWithActores.Director.Nombre);
    }

    if (videoWithActores.Streamer is not null
       && videoWithActores.Streamer.Nombre is not null)
    {
        Console.WriteLine("con el streamer: " + videoWithActores.Streamer.Nombre);
    }

   
}

async Task AddNewDirectorWithVideo()
{
    var director = new Director()
    {
        Nombre = "Lorenzo",
        Apellido = "Basteri",
        VideoId = 1
    };

    await streamerDbContext.AddAsync(director);
    await streamerDbContext.SaveChangesAsync();
}

async Task AddVideoWithActor()
{
    var actor = new Actor()
    {
        Nombre = "Brad",
        Apellido = "Pitt"
    };

    await streamerDbContext.AddAsync(actor);
    await streamerDbContext.SaveChangesAsync();

    var video = new VideoActor()
    {
        ActorId = actor.Id,
        VideoId = 1
    };

    await streamerDbContext.AddAsync(video);
    await streamerDbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideo()
{
    
    var batmanForever = new Video()
    {
        Nombre = "Batman Forever",
        StreamerId = 1002
    };
    await streamerDbContext.AddAsync(batmanForever);
    await streamerDbContext.SaveChangesAsync();
}
async Task TrackingAndNotTracking()
{
    var streamerWithoutTracking = await streamerDbContext.Streamers.FirstOrDefaultAsync(x => x.Nombre.Contains("Dis"));
    var streamerWithTracking = await streamerDbContext.Streamers.AsNoTracking().FirstOrDefaultAsync(x => x.Nombre.Contains("Net"));
    //without tracking the object can be modificated
    //with tracking the object can´t be modificated
    Console.WriteLine(streamerWithoutTracking.Nombre);
    Console.WriteLine(streamerWithTracking.Nombre);

}
async Task QueryLinq()
{
    Console.WriteLine("Escriba el nombre de la compañia");
    var streamingNombre = Console.ReadLine();

    var streamers = await (from i in streamerDbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamingNombre}%")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}
async Task QueryMethods()
{
    var streamer =  streamerDbContext.Streamers;

    var firstAsync = streamer.Where(x => x.Nombre.Contains("N")).FirstAsync();
    var firstOrDefaultAsync = streamer.Where(x => x.Nombre.Contains("N")).FirstOrDefaultAsync();
    var firstOrDefaultAsync_2 = streamer.FirstOrDefaultAsync(x => x.Nombre.Contains("N"));
    var resultado = await streamer.FindAsync(1);
}
await QueryFilter();
async Task QueryFilter()
{
    Console.WriteLine("Escriba el nombre de la compañia");
    var streamingNombre = Console.ReadLine();

    // var streamers = await streamerDbContext.Streamers.Where(x => x.Nombre!.Equals("Netflix")).AsNoTracking().ToListAsync();
    //EF FUNCTION 
    var streamers = await streamerDbContext.Streamers.Where(x => EF.Functions.Like(x.Nombre, $"%{streamingNombre}%")).AsNoTracking().ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}
void QueryStreaming()
{
    var streamers = streamerDbContext.Streamers.ToList();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }

    var movies = streamerDbContext.videos.Where(x => x.StreamerId == 2).ToList();

    foreach (var movie in movies)
    {
        Console.WriteLine($"{movie.Id} - {movie.Nombre}");
    }

}
async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Nombre = "Disney",
        Url = "https://disney.com"
    };

    await streamerDbContext.Streamers.AddAsync(streamer);
    await streamerDbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video {
        Nombre = "La Cenicienta",
        StreamerId = streamer.Id
    },
     new Video {
        Nombre = "1001 Dalmatas",
        StreamerId = streamer.Id
    },
     new Video {
        Nombre = "El Jorobado De Notredame",
        StreamerId = streamer.Id
    },
     new Video {
        Nombre = "Star Wars",
        StreamerId = streamer.Id
    }
};

    await streamerDbContext.videos.AddRangeAsync(movies);
    await streamerDbContext.SaveChangesAsync();
}
