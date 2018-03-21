using ShortUrlNet.Models;

namespace ShortUrlNet.Services
{
    public class TestDataService
    {
        private IRepository _repository;

        public TestDataService(IRepository repository)
        {
            _repository = repository;
        }
    }
}
