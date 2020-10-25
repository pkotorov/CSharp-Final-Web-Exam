using Git.Data;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateCommit(string description, string creatorId, string repositoryId)
        {
            var commit = new Commit
            {
                Description = description,
                CreatorId = creatorId,
                RepositoryId = repositoryId,
                CreatedOn = DateTime.UtcNow
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public bool Delete(string commitId, string userId)
        {
            var commit = this.db.Commits
                .Where(x => x.Id == commitId
                && x.CreatorId == userId)
                .FirstOrDefault();

            if (commit != null)
            {
                this.db.Commits.Remove(commit);
                this.db.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<MyCommitsViewModel> GetMyCommits(string id)
        {
            return this.db.Commits
                .Where(x => x.CreatorId == id)
                .Select(x => new MyCommitsViewModel
                {
                    Id = x.Id,
                    Repository = x.Repository.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn
                }).ToList();
        }

        public CommitToRepoViewModel GetRepoId(string id)
        {
            return this.db.Repositories
                .Where(x => x.Id == id)
                .Select(x => new CommitToRepoViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefault();
        }
    }
}
