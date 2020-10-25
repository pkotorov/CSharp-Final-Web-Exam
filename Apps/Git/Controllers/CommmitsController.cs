using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.commitsService.GetRepoId(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string description, string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(description))
            {
                return this.Error("Description is required!");
            }

            if (description.Length < 5)
            {
                return this.Error("Description must be at least 5 characters long!");
            }

            var creatorId = this.GetUserId();

            this.commitsService.CreateCommit(description, creatorId, id);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            var viewModel = this.commitsService.GetMyCommits(userId);

            return this.View(viewModel);
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (this.commitsService.Delete(id, userId))
            {
                return this.Redirect("/Commits/All");
            }

            return this.Error("You are not the owner of this commit!");
        }
    }
}
