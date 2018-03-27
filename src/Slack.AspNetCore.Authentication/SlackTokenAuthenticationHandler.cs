namespace Microsoft.AspNetCore.Authentication
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    public class SlackTokenAuthenticationHandler : AuthenticationHandler<SlackTokenAuthenticationOptions>
    {
        protected SlackTokenAuthenticationHandler(IOptionsMonitor<SlackTokenAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (this.Request.Method != "POST" || !this.Request.HasFormContentType)
            {
                return AuthenticateResult.NoResult();
            }

            var formData = await this.Request.ReadFormAsync();

            if (!formData.ContainsKey("token"))
            {
                return AuthenticateResult.Fail("Missing token in POST request");
            }

            if (!this.Options.Tokens.Contains(formData["token"].ToString()))
            {
                return AuthenticateResult.Fail("Invalid token");
            }

            var claims = new[] 
            {
                new Claim(ClaimTypes.Name, "SlackWebAPI"),
                new Claim("Team", formData["team_id"]),
                new Claim("User", formData["user_id"])
            };

            var identity = new ClaimsIdentity(claims, this.Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
