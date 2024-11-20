namespace Base.EndPoints.Web.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LocalAuthorizeAttribute(string? roles = null) : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// Gets or sets a comma delimited list of roles that are allowed to access the resource.
    /// </summary>
    private string? Roles { get; set; } = roles;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if genericAction is decorated with [LocalAllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<LocalAllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var user = context.HttpContext.User.Identity?.IsAuthenticated;
        if (user == false)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" })
            { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else
        {
            if (!context.HttpContext.ValidationToken())
            {
                throw new UnauthorizedException();
            }

            if (Roles == null) return;
            if (Roles.Split(',').Any(role => !context.HttpContext.User.IsInRole(role.ToLower())))
            {
                throw new UnauthorizedException();
            }
        }
    }
}

public static class UserTokenExtension
{
    public static bool ValidationToken(this HttpContext claimsPrincipal)
    {
        claimsPrincipal.Request.Headers.TryGetValue("Authorization", out StringValues token);
        token = token.ToString().Replace("Bearer ", "");
        if (string.IsNullOrWhiteSpace(token.ToString()))
        {
            throw new SecurityTokenException("Unauthorized");
        }

        try
        {
            var secretKey = Encoding.UTF8.GetBytes("5238d9bd03d543e62fff76cbea6a2392a95f42110e36c282e4c919991f607daf");
            var encryptionKey = Encoding.UTF8.GetBytes("35b780710e4a81ae");
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token.ToString(),
                    new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero, // default: 5 min
                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateAudience = true, //default : false
                        ValidAudience = "Job",
                        ValidateIssuer = true, //default : false
                        ValidIssuer = "Job",

                        TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                    },
                    out var validatedToken);
            return true;
        }
        catch (Exception e)
        {
            throw new SecurityTokenException("Unauthorized");
        }
    }
}