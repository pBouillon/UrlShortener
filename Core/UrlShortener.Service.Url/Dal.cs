using Npgsql;
using System;
using System.Data;
using UrlShortener.Service.Url.Constants;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Service.Url
{
    public class Dal : IDal
    {
        private readonly string _connectionString;

        public Dal()
        {
            _connectionString = $"Host={Database.Host};" +
                                $"Port={Database.Port};" +
                                $"Username={Database.Username};" +
                                $"Password={Database.Password};" +
                                $"Database={Database.DatabaseName}";
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="originUrl"></param>
        /// <returns></returns>
        public string GetShortenedFor(string originUrl)
        {
            string shortened;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                const string urlParam = "raw";

                var query = "SELECT raw" +
                            $"FROM {Database.TableName}" +
                            $"WHERE {Database.Columns.LongUrl} LIKE @{urlParam};";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(urlParam, originUrl);

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        shortened = (string) reader[0];
                    }
                }
            }

            return shortened;
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="originUrl"></param>
        /// <returns></returns>
        public bool IsUrlStored(string originUrl)
        {
            long records;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                const string urlParam = "raw";

                var query = "SELECT COUNT(*) " +
                            $"FROM {Database.TableName} " +
                            $"WHERE {Database.Columns.LongUrl} LIKE @{urlParam};";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(urlParam, originUrl);

                    // ReSharper disable once PossibleNullReferenceException
                    records = (long) command.ExecuteScalar();
                }
            }

            return records != 0;
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="originUrl"></param>
        /// <param name="shortened"></param>
        public void StoreShortened(string originUrl, string shortened)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                const string shortParam = "shortened";
                const string urlParam = "raw";

                var query = $"INSERT INTO {Database.TableName} " +
                            $"({Database.Columns.LongUrl}, {Database.Columns.ShortUrl})" +
                            $"VALUES (@{urlParam}, @{shortParam});";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(shortParam, shortened);
                    command.Parameters.AddWithValue(urlParam, originUrl);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
