using Git.Data;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateRepository(string name, string type, string userId)
        {
            bool isPublic;

            if (type == "Private")
            {
                isPublic = false;
            }
            else
            {
                isPublic = true;
            }

            var repository = new Repository
            {
                Name = name, 
                OwnerId = userId,
                IsPublic = isPublic,
                CreatedOn = DateTime.UtcNow
            };

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public IEnumerable<AllRepositoriesViewModel> GetAll()
        {
            return this.db.Repositories
                .Where(x => x.IsPublic == true)
                .Select(x => new AllRepositoriesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    CreatedOn = x.CreatedOn,
                    CommitsCount = x.Commits.Count()
                }).ToList();
        }


    }
}
