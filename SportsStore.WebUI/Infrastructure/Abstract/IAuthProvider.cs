// -----------------------------------------------------------------------
// <copyright file="IAuthProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.WebUI.Infrastructure.Abstract
{
    using System;
    using System.Collections.Generic;
    using SportsStore.WebUI.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IAuthProvider
    {
        Boolean Authenticate(String username, String password);

        IEnumerable<String> RegisterUser(ApplicationUser user, String Password);
    }
}
