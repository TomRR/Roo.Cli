namespace Roo.Cli.Tests;

using System.Collections.Generic;
using Xunit;
using Roo.Cli.Common.Models;

public class MapperExtensionsTests
{
    [Fact]
    public void Repository_ToDto_MapsCorrectly()
    {
        // Arrange
        var repo = new Repository
        {
            Name = "Repo1",
            Path = "/path",
            Url = "http://url",
            Tags = "tag1,tag2",
            Description = "desc"
        };

        // Act
        var dto = repo.MapTo();

        // Assert
        Assert.Equal(repo.Name, dto.Name);
        Assert.Equal(repo.Path, dto.Path);
        Assert.Equal(repo.Url, dto.Url);
        Assert.Equal(repo.Tags, dto.Tags);
        Assert.Equal(repo.Description, dto.Description);
        Assert.Equal($"{repo.Path}/{repo.Name}", dto.GetLocalRepoPath());
    }

    [Fact]
    public void RepositoryDto_ToDomain_MapsCorrectly()
    {
        // Arrange
        var dto = new RepositoryDto("Repo1", "http://url", "/path", "tag1,tag2", "desc");

        // Act
        var repo = dto.MapTo();

        // Assert
        Assert.Equal(dto.Name, repo.Name);
        Assert.Equal(dto.Path, repo.Path);
        Assert.Equal(dto.Url, repo.Url);
        Assert.Equal(dto.Tags, repo.Tags);
        Assert.Equal(dto.Description, repo.Description);
    }

    [Fact]
    public void Owner_ToDtoAndBack_MapsCorrectly()
    {
        var owner = new Owner { Name = "Tom" };
        var dto = owner.MapTo();
        var owner2 = dto.MapTo();

        Assert.Equal(owner.Name, dto.Name);
        Assert.Equal(owner.Name, owner2.Name);
    }

    [Fact]
    public void RooConfig_ToDtoAndBack_MapsCorrectly()
    {
        // Arrange
        var config = new RooConfig
        {
            Title = "Config1",
            Owner = new Owner { Name = "Alice" },
            Repositories = new List<Repository>
            {
                new() { Name = "Repo1", Path = "/p1", Url = "url1" },
                new() { Name = "Repo2", Path = "/p2", Url = "url2" }
            }
        };

        // Act
        var dto = config.MapTo();
        var config2 = dto.MapTo();

        // Assert
        Assert.Equal(config.Title, dto.Title);
        Assert.Equal(config.Owner?.Name, dto.Owner?.Name);
        Assert.Equal(config.Repositories.Count, dto.Repositories.Count);

        for (int i = 0; i < config.Repositories.Count; i++)
        {
            Assert.Equal(config.Repositories[i].Name, dto.Repositories[i].Name);
            Assert.Equal(config.Repositories[i].Path, dto.Repositories[i].Path);
            Assert.Equal(config.Repositories[i].Url, dto.Repositories[i].Url);

            Assert.Equal(config.Repositories[i].Name, config2.Repositories[i].Name);
            Assert.Equal(config.Repositories[i].Path, config2.Repositories[i].Path);
            Assert.Equal(config.Repositories[i].Url, config2.Repositories[i].Url);
        }
    }

    [Fact]
    public void RooConfigDto_WithNulls_MapsSafely()
    {
        // Arrange
        var dto = new RooConfigDto(null, null, null!);

        // Act
        var config = dto.MapTo();

        // Assert
        Assert.Null(config.Title);
        Assert.Null(config.Owner);
        Assert.NotNull(config.Repositories);
        Assert.Empty(config.Repositories);
    }

    [Fact]
    public void RepositoryList_Mapping_Works()
    {
        // Arrange
        var list = new List<Repository>
        {
            new() { Name = "A", Path = "/a", Url = "urlA" },
            new() { Name = "B", Path = "/b", Url = "urlB" }
        };

        // Act
        var dtoList = list.MapTo();
        var list2 = dtoList.MapTo();

        // Assert
        Assert.Equal(list.Count, dtoList.Count);
        Assert.Equal(list.Count, list2.Count);
        for (int i = 0; i < list.Count; i++)
        {
            Assert.Equal(list[i].Name, dtoList[i].Name);
            Assert.Equal(list[i].Path, dtoList[i].Path);
            Assert.Equal(list[i].Url, dtoList[i].Url);

            Assert.Equal(list[i].Name, list2[i].Name);
            Assert.Equal(list[i].Path, list2[i].Path);
            Assert.Equal(list[i].Url, list2[i].Url);
        }
    }
}
