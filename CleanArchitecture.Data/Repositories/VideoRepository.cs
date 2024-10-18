using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Video>> GetByName(string Name)
        {
           return await _context.videos.Where(x => x.Nombre!.Equals(Name)).ToListAsync();  
        }

        public async Task<IEnumerable<Video>> GetByUsername(string Username)
        {
            return await _context.videos.Where(x => x.CreatedBy!.Equals(Username)).ToListAsync();
        }

        Task<Video> IVideoRepository.GetByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
