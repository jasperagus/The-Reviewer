using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;

namespace TheReviewer.UnitTesting.Repositories;

public class MockMediaRepository : IMediaRepository
{
    public List<GetMediaDTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public List<GetMediaDTO> GetByType(int typeId)
    {
        throw new NotImplementedException();
    }
}