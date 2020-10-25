using Microsoft.EntityFrameworkCore.Migrations;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;

namespace Git.Data
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Repositories = new HashSet<Repository>();
            this.Commits = new HashSet<Commit>();
            this.Role = IdentityRole.User;
        }

        public virtual ICollection<Repository> Repositories { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
    }
}
