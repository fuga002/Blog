namespace Blog.Common.Models.JwtOptions;

public class JwtOption
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigninKey { get; set; }
    public double  Minute { get; set; }
}