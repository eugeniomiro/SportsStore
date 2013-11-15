// -----------------------------------------------------------------------
// <copyright file="IAuthProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SportsStore.WebUI.Infrastructure.Abstract
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IAuthProvider
    {
        Boolean Authenticate(String username, String password);
    }
}
