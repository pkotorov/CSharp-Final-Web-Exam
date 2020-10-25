using Git.ViewModels.Repositories;
using System.Collections;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        void CreateRepository(string name, string type, string userId);

        IEnumerable<AllRepositoriesViewModel> GetAll();
    }
}
