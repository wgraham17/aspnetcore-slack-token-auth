namespace Microsoft.AspNetCore.Authentication
{
    public class SlackTokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string[] Tokens { get; set; }
    }
}
