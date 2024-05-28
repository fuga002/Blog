using System.Security.Claims;
using System.Text.Json;

namespace Blog.Client.AuthActions;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs!.TryGetValue(ClaimTypes.Name, out object name);

        if (name != null)
        {
            claims.Add(new Claim(ClaimTypes.Name,name.ToString()!));
        }

        foreach (var key in keyValuePairs)
        {
            claims.Add(new Claim(key.Key,key.Value.ToString()!));
        }

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length%4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Convert.FromBase64String(base64);
    }
}