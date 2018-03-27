namespace Microsoft.AspNetCore.Authentication
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System;

    public static class SlackAuthenticationExtensions
    {
        public static AuthenticationBuilder AddSlackToken(this AuthenticationBuilder builder, Action<SlackTokenAuthenticationOptions> configureOptions)
        {
            return AddSlackToken(builder, SlackTokenAuthenticationDefaults.AuthenticationScheme, configureOptions);
        }

        public static AuthenticationBuilder AddSlackToken(this AuthenticationBuilder builder, string authenticationScheme, Action<SlackTokenAuthenticationOptions> configureOptions)
        {
            builder.Services.AddSingleton<IPostConfigureOptions<SlackTokenAuthenticationOptions>, SlackTokenAuthenticationPostConfigureOptions>();
            
            return builder.AddScheme<SlackTokenAuthenticationOptions, SlackTokenAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }
}
