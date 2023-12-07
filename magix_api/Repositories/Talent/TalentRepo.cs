using magix_api.Data;
using magix_api.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace magix_api.Repositories
{
    public class TalentRepo : ITalentRepo
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MagixContext _context;

        public TalentRepo(IMemoryCache memoryCache, MagixContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public async Task<List<Talent>> GetAllTalents()
        {
            List<Talent>? result = _memoryCache.Get<List<Talent>>("talents");
            if (result is null)
            {
                try
                {
                    ServerResponse<List<Talent>> response = await GameServerAPI.CallApi<List<Talent>>("talents");
                    if (response.IsValid && response.Content != null)
                    {
                        result = response.Content.Select(talent =>
                        {
                            talent.ToFrontend();
                            var dbTalent = _context.Talents.SingleOrDefault(t => t.Name == talent.Name);
                            if (dbTalent is not null)
                            {
                                dbTalent.Description = talent.Description;
                                talent.Id = dbTalent.Id;
                            }
                            else
                            {
                                _context.Talents.Add(talent);
                            }
                            return talent;
                        }).ToList();
                        var input = _memoryCache.Set("talents", result, TimeSpan.FromDays(1));
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result ?? new List<Talent>();
        }

        public async Task<Talent?> GetTalentById(int id)
        {
            return await _context.Talents.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Talent?> GetTalentByName(string name)
        {
            return await _context.Talents.FirstOrDefaultAsync(t => t.Name == name);
        }
    }
}
