// -----------------------------------------------------------------------
// <copyright file="IAuthProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace SportsStore.WebUI.Infrastructure.Abstract
{
    using DataAccess.EntityFramework.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);

        IEnumerable<string> RegisterUser(ApplicationUser user, string Password);
    }
}
