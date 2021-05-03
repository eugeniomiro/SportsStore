// -----------------------------------------------------------------------
// <copyright file="IAuthProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.WebUI.Infrastructure.Abstract
{
    using System.Collections.Generic;
    using SportsStore.WebUI.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);

        IEnumerable<string> RegisterUser(ApplicationUser user, string Password);
    }
}
