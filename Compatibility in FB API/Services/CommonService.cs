namespace MatchUp.Services
{
    public static class CommonService
    {
        public static string CreateGoogleUrl(string name)
        {
            string url = "https://www.google.com/search?&rls=en&q=" + name.Replace(" ", "+");

            return url;
        }
    }
}