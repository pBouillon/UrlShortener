namespace UrlShortener.Common.Contracts.Configuration
{
    /// <summary>
    /// References Swagger project header's data
    /// </summary>
    public class ProjectDto
    {
        /// <summary>
        /// API version
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// Author's contact
        /// </summary>
        public ContactDto Contact { get; set; }

        /// <summary>
        /// Project's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// License information
        /// </summary>
        public LicenseDto License { get; set; }

        /// <summary>
        /// Terms of service of the API
        /// </summary>
        public string TermsOfService { get; set; }

        /// <summary>
        /// API's title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// API's version
        /// </summary>
        public string Version { get; set; }
    }
}
