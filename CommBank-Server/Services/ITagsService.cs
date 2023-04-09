using CommBank.Models;

namespace CommBank.Services;

public interface ITagsService
{
    Task CreateAsync(Tag newTag);
    Task<List<Tag>> GetAsync();
    Task<Tag?> GetAsync(string id);
    Task RemoveAsync(string id);
    Task UpdateAsync(string id, Tag updatedTag);
}
