using Microsoft.Extensions.Options;
using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public class TagsService : ITagsService
{
    private readonly IMongoCollection<Models.Tag> _tagsCollection;

    public TagsService(IMongoDatabase mongoDatabase)
    {
        _tagsCollection = mongoDatabase.GetCollection<Models.Tag>("Tags");
    }

    public async Task<List<Models.Tag>> GetAsync() =>
        await _tagsCollection.Find(_ => true).ToListAsync();

    public async Task<Models.Tag?> GetAsync(string id) =>
        await _tagsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Models.Tag newTag) =>
        await _tagsCollection.InsertOneAsync(newTag);

    public async Task UpdateAsync(string id, Models.Tag updatedTag) =>
        await _tagsCollection.ReplaceOneAsync(x => x.Id == id, updatedTag);

    public async Task RemoveAsync(string id) =>
        await _tagsCollection.DeleteOneAsync(x => x.Id == id);
}