using System.Collections.Generic;
using EPiServer.ServiceLocation;

namespace Alloy.Mvc.Setup
{
    /// <summary>
    /// Defines the settings for the admin user registration feature.
    /// </summary>
    [Options(ConfigurationSection = ConfigurationSectionConstants.Cms)]
    public class RegisterAdminUserOptions
    {
        /// <summary>
        /// Gets or sets the roles the admin user should be assigned to.
        /// </summary>
        public ICollection<string> Roles { get; set; } = new HashSet<string>(new[] { EPiServer.Authorization.Roles.WebAdmins });

        /// <summary>
        /// Gets or sets how the registration should behave.
        /// </summary>
        /// <remarks>
        /// Default is <see cref="RegisterAdminUserBehaviors.Enabled"/> | <see cref="RegisterAdminUserBehaviors.LocalRequestsOnly"/> | <see cref="RegisterAdminUserBehaviors.SingleUser"/>
        /// </remarks>
        public RegisterAdminUserBehaviors Behavior { get; set; } = RegisterAdminUserBehaviors.Enabled |
                                                                    RegisterAdminUserBehaviors.LocalRequestsOnly |
                                                                    RegisterAdminUserBehaviors.SingleUser;
    }
}