using CommBank.Models;
using CommBank.Services;

namespace CommBank.Tests.Fake;

public class FakeTagsService : ITagsService
{
    List<Tag> _tags;
    Tag _tag;

    public FakeTagsService(List<Tag> tags, Tag tag)
    {
        _tags = tags;
        _tag = tag;
    }

    public async Task<List<Tag>> GetAsync() =>
        await Task.FromResult(_tags);

    public async Task<Tag?> GetAsync(string id) =>
        await Task.FromResult(_tag);

    public async Task CreateAsync(Tag newTag) =>
        await Task.FromResult(true);

    public async Task UpdateAsync(string id, Tag updatedTag) =>
        await Task.FromResult(true);

    public async Task RemoveAsync(string id) =>
        await Task.FromResult(true);
}