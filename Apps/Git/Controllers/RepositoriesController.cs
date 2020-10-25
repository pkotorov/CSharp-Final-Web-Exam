﻿using Git.Services;
using Git.ViewModels.Repositories;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            var viewModel = this.repositoriesService.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.Name))
            {
                return this.Error("Name is required!");
            }

            if (input.Name.Length < 3 || input.Name.Length > 10)
            {
                return this.Error("Name must be between 3 and 10 characters long!");
            }

            if (string.IsNullOrEmpty(input.RepositoryType))
            {
                return this.Error("Type must be selected!");
            }

            var userId = this.GetUserId();

            this.repositoriesService.CreateRepository(input.Name, input.RepositoryType, userId);

            return this.Redirect("/Repositories/All");
        }
    }
}
