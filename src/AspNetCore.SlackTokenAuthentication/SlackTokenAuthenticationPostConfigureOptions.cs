namespace Microsoft.AspNetCore.Authentication
{
    using Microsoft.Extensions.Options;
    using System;

    public class SlackTokenAuthenticationPostConfigureOptions : IPostConfigureOptions<SlackTokenAuthenticationOptions>
    {
        public void PostConfigure(string name, SlackTokenAuthenticationOptions options)
        {
            if (options.Tokens?.Length == 0)
            {
                throw new InvalidOperationException("At least one Slack token must be provided in options");
            }
        }
    }
}
