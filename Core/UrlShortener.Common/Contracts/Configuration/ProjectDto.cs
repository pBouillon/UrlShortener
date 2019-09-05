namespace UrlShortener.Common.Contracts.Configuration
{
    public class ProjectDto
    {
        public string ApiVersion { get; set; }
        public ContactDto Contact { get; set; }
        public string Description { get; set; }
        public LicenseDto License { get; set; }
        public string TermsOfService { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
    }
}
