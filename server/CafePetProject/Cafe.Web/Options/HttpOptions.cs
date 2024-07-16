namespace Cafe.Web.Options;

public static class HttpOptions
{
    public static CookieOptions RefreshTokenCookieOptions()
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
        };
        return cookieOptions;
    }
}