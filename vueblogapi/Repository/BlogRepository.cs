using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using vueblogapi.Model;

namespace vueblogapi.Services
{
    public class BlogRepository
    {
        private readonly IMongoCollection<Blog> _blogCollection;
        private readonly IOptions<DatabaseSetting> _dbSetting;

        public BlogRepository(IOptions<DatabaseSetting> dbSetting)
        {
            _dbSetting = dbSetting;
            var client = new MongoClient(_dbSetting.Value.ConnectionString);
            var db = client.GetDatabase(_dbSetting.Value.BlogCollection);
            _blogCollection = db.GetCollection<Blog>(_dbSetting.Value.BlogCollection);
        }

        public async Task<IEnumerable<Blog>> GetBlogs()
        {
            return await _blogCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Blog> GetById(string id)
        {
            return await _blogCollection.Find(blog => blog.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateBlog(Blog blog)
        {
            await _blogCollection.InsertOneAsync(blog);
        }

        public async Task UpdateBlog(Blog blog)
        {
            await _blogCollection.ReplaceOneAsync(i => i.Id == blog.Id, blog);
        }
    }
}
