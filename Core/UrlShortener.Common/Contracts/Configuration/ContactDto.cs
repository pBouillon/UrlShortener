namespace UrlShortener.Common.Contracts.Configuration
{
    /// <summary>
    /// References Swagger contact header's data
    /// </summary>
    public class ContactDto
    {
        /// <summary>
        /// Author's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Author's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author's website URL
        /// </summary>
        public string Url { get; set; }
    }
}
