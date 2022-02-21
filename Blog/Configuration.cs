namespace Blog;

public static class Configuration
{
    public static string JwtKey  = "FO2lfYZdTgWj2i0Kbw0DBA";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_EbLAc6fHQhuJ8YQjQm7lOw";
    public static SmtpConfiguration Smtp = new ();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}