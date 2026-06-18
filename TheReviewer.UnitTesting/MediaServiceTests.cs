using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Services;
using Xunit;

namespace TheReviewer.UnitTesting;

public class MediaServiceTests
{
    [Fact]
    public void GetByType_WithFilms_ReturnsFilms()
    {
        var repository = new MockMediaRepository();
        var service = new MediaService(repository);

        var films = service.GetByType(1);

        Assert.Single(films);
        Assert.Equal(1, repository.LastRequestedTypeId);
        Assert.Equal("Inception", films[0].Name);
        Assert.Equal("Warner Bros.", films[0].Publisher);
        Assert.Equal(1, films[0].TypeId);
        Assert.Null(films[0].Episodes);
    }

    [Fact]
    public void GetByType_WithGames_ReturnsGames()
    {
        var repository = new MockMediaRepository();
        var service = new MediaService(repository);

        var games = service.GetByType(2);

        Assert.Single(games);
        Assert.Equal(2, repository.LastRequestedTypeId);
        Assert.Equal("The Legend of Zelda: Breath of the Wild", games[0].Name);
        Assert.Equal("Nintendo", games[0].Publisher);
        Assert.Equal(2, games[0].TypeId);
        Assert.Null(games[0].Episodes);
    }

    [Fact]
    public void GetByType_WithShows_ReturnsShows()
    {
        var repository = new MockMediaRepository();
        var service = new MediaService(repository);

        var shows = service.GetByType(3);

        Assert.Single(shows);
        Assert.Equal(3, repository.LastRequestedTypeId);
        Assert.Equal("Breaking Bad", shows[0].Name);
        Assert.Equal("AMC", shows[0].Publisher);
        Assert.Equal(3, shows[0].TypeId);
        Assert.Equal(62, shows[0].Episodes);
    }

    private class MockMediaRepository : IMediaRepository
    {
        private readonly DateTime _createdAt = new(2026, 6, 18);
        private readonly DateTime _updatedAt = new(2026, 6, 18);

        public int LastRequestedTypeId { get; private set; }

        public List<GetMediaDTO> GetByType(int typeId)
        {
            LastRequestedTypeId = typeId;

            return typeId switch
            {
                1 =>
                [
                    new GetMediaDTO(1, "Inception", "Warner Bros.", 9, 1, null, _createdAt, _updatedAt)
                ],
                2 =>
                [
                    new GetMediaDTO(2, "The Legend of Zelda: Breath of the Wild", "Nintendo", 10, 2, null, _createdAt, _updatedAt)
                ],
                3 =>
                [
                    new GetMediaDTO(3, "Breaking Bad", "AMC", 10, 3, 62, _createdAt, _updatedAt)
                ],
                _ => []
            };
        }
    }
}
