namespace RuichenShuxin.AbpPro.Core;

public class AuthServerOptions
{
    public string Authority { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public string SwaggerClientId { get; set; }
    public string CertificatePassPhrase { get; set; }

    public string[] Scopes { get; set; } = Array.Empty<string>();
}
