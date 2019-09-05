using Npgsql;
using NpgsqlTypes;
using System.Data;
using UrlShortener.Service.Url.Constants;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Service.Url
{
    /// <summary>
    /// References the concrete implementation of the data access layer
    /// </summary>
    public class Dal : IDal
    {
        /// <summary>
        /// Connection string to the database
        /// </summary>
        /// <see cref="Database"/>
        private readonly string _connectionString;

        /// <summary>
        /// Default constructor
        /// Build the connection string
        /// </summary>
        public Dal()
        {
            _connectionString = $"Host={Database.Host};" +
                                $"Port={Database.Port};" +
                                $"Username={Database.Username};" +
                                $"Password={Database.Password};" +
                                $"Database={Database.DatabaseName}";
        }

        /// <inheritdoc/>
        /// <summary>
        /// Get the original URL from its shortened version
        /// </summary>
        /// <param name="shortUrl">The short URL's code</param>
        /// <returns>The original URL</returns>
        public string GetOriginalFrom(string shortUrl)
        {
            string original;

            const string shortParam = "short";

            var query = $"SELECT {Database.Columns.LongUrl} " +
                        $"FROM {Database.TableName} " +
                        $"WHERE {Database.Columns.ShortUrl} LIKE @{shortParam};";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Connection = connection;

                    command.CommandText = query;
                    command.Parameters.AddWithValue(shortParam, shortUrl);

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        reader.Read();

                        original = (string)reader[0];
                    }
                }
            }

            return original;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Get the shortened URL from its long version
        /// </summary>
        /// <param name="originUrl">The original URL</param>
        /// <returns>The shortened URL code</returns>
        public string GetShortenedFor(string originUrl)
        {
            string shortened;

            const string urlParam = "raw";

            var query = $"SELECT {Database.Columns.ShortUrl} " +
                        $"FROM {Database.TableName} " +
                        $"WHERE {Database.Columns.LongUrl} LIKE @{urlParam};";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Connection = connection;

                    command.CommandText = query;
                    command.Parameters.AddWithValue(urlParam, originUrl);

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        reader.Read();

                        shortened = (string) reader[0];
                    }
                }
            }

            return shortened;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Check if the original URL is currently tracked by the system
        /// </summary>
        /// <param name="originUrl">Original URL to check</param>
        /// <returns>True if stored; false otherwise</returns>
        public bool IsUrlStored(string originUrl)
        {
            long records;

            const string urlParam = "raw";

            var query = "SELECT COUNT(*) " +
                        $"FROM {Database.TableName} " +
                        $"WHERE {Database.Columns.LongUrl} LIKE @{urlParam};";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = query;
                    command.Parameters.AddWithValue(urlParam, NpgsqlDbType.Varchar, originUrl);

                    // ReSharper disable once PossibleNullReferenceException
                    records = (long) command.ExecuteScalar();
                }
            }

            return records != 0;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Check if the shortened URL code is currently stored in the database
        /// </summary>
        /// <param name="shortUrl">The short URL's code</param>
        /// <returns>True if stored; false otherwise</returns>
        public bool IsShortCodeStored(string shortUrl)
        {
            long records;

            const string urlParam = "short";

            var query = "SELECT COUNT(*) " +
                        $"FROM {Database.TableName} " +
                        $"WHERE {Database.Columns.ShortUrl} LIKE @{urlParam};";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = query;
                    command.Parameters.AddWithValue(urlParam, NpgsqlDbType.Varchar, shortUrl);

                    // ReSharper disable once PossibleNullReferenceException
                    records = (long)command.ExecuteScalar();
                }
            }

            return records != 0;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Store the original URL and its associated short code
        /// </summary>
        /// <param name="originUrl">Original URL</param>
        /// <param name="shortened">Associated short code</param>
        public void StoreShortened(string originUrl, string shortened)
        {
            const string shortParam = "shortened";
            const string urlParam = "raw";

            var query = $"INSERT INTO {Database.TableName} " +
                        $"({Database.Columns.LongUrl}, {Database.Columns.ShortUrl})" +
                        $"VALUES (@{urlParam}, @{shortParam});";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = query;
                    command.Parameters.AddWithValue(shortParam, shortened);
                    command.Parameters.AddWithValue(urlParam, originUrl);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
