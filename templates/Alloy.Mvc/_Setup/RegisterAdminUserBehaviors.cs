using System;

namespace Alloy.Mvc.Setup
{
    /// <summary>
    /// Defines the behavior of the admin user registration feature.
    /// </summary>
    [Flags]
    public enum RegisterAdminUserBehaviors
    {
        /// <summary>
        /// Disables the registration page and automatic redirect on
        /// first request.
        /// </summary>
        Disabled = 1,

        /// <summary>
        /// Enables the registration page and automatic redirect on
        /// first request.
        /// </summary>
        Enabled = 2,

        /// <summary>
        /// Disables the registration page and automatic redirect on
        /// first request on non-local URLs.
        /// </summary>
        LocalRequestsOnly = 4,

        /// <summary>
        /// Disables the registration page and automatic redirect on
        /// first request when a user has been created.
        /// </summary>
        SingleUser = 8
    }
}
