using Git.ViewModels.Commits;
using System.Collections.Generic;

namespace Git.Services
{
    public interface ICommitsService
    {
        CommitToRepoViewModel GetRepoId(string id);

        void CreateCommit(string description, string creatorId, string repositoryId);

        IEnumerable<MyCommitsViewModel> GetMyCommits(string id);

        bool Delete(string commitId, string userId);
    }
}
