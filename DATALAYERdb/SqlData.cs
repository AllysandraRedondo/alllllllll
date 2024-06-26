using System.Collections.Generic;
using System.Data.SqlClient;
using MODELSdb;

namespace DATALAYERdb
{
    public class SqlData
    {
        string connectionString = "Data Source=LAPTOP-JGL8F6OD\\SQLEXPRESS;Initial Catalog=AnimeList;Integrated Security=True;";
        SqlConnection sqlConnection;

        public SqlData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<AnimeContent> GetAnime()
        {
            string selectStatement = "SELECT Title, Studio, ReleaseDate, Status, Genre, Episodes, Summary FROM AnimeListTable";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            List<AnimeContent> animeList = new List<AnimeContent>();
            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                AnimeContent anime = new AnimeContent
                {
                    title = reader["Title"].ToString(),
                    studio = reader["Studio"].ToString(),
                    releasedate = reader["ReleaseDate"].ToString(),
                    status = reader["Status"].ToString(),
                    genre = reader["Genre"].ToString(),
                    episodes = reader["Episodes"].ToString(),
                    summary = reader["Summary"].ToString()
                };
                animeList.Add(anime);
            }

            sqlConnection.Close();
            return animeList;
        }

        public int AddAnime(string title, string studio, string releasedate, string status, string genre, string episodes, string summary)
        {
            string insertStatement = "INSERT INTO AnimeListTable (Title, Studio, ReleaseDate, Status, Genre, Episodes, Summary) VALUES (@title, @studio, @releasedate, @status, @genre, @episodes, @summary)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@title", title);
            insertCommand.Parameters.AddWithValue("@studio", studio);
            insertCommand.Parameters.AddWithValue("@releasedate", releasedate);
            insertCommand.Parameters.AddWithValue("@status", status);
            insertCommand.Parameters.AddWithValue("@genre", genre);
            insertCommand.Parameters.AddWithValue("@episodes", episodes);
            insertCommand.Parameters.AddWithValue("@summary", summary);

            sqlConnection.Open();
            int success = insertCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return success;
        }

        public int RemoveAnime(string title)
        {
            string deleteStatement = "DELETE FROM AnimeListTable WHERE Title = @title";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);

            deleteCommand.Parameters.AddWithValue("@title", title);

            sqlConnection.Open();
            int success = deleteCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return success;
        }

        public List<AnimeContent> SearchAnime(string keyword)
        {
            string searchStatement = "SELECT Title, Studio, ReleaseDate, Status, Genre, Episodes, Summary FROM AnimeListTable WHERE Title LIKE @keyword OR Studio LIKE @keyword OR Genre LIKE @keyword";
            SqlCommand searchCommand = new SqlCommand(searchStatement, sqlConnection);

            searchCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

            sqlConnection.Open();
            List<AnimeContent> searchResults = new List<AnimeContent>();
            SqlDataReader reader = searchCommand.ExecuteReader();

            while (reader.Read())
            {
                AnimeContent anime = new AnimeContent
                {
                    title = reader["Title"].ToString(),
                    studio = reader["Studio"].ToString(),
                    releasedate = reader["ReleaseDate"].ToString(),
                    status = reader["Status"].ToString(),
                    genre = reader["Genre"].ToString(),
                    episodes = reader["Episodes"].ToString(),
                    summary = reader["Summary"].ToString()
                };
                searchResults.Add(anime);
            }

            sqlConnection.Close();
            return searchResults;
        }

        
    }
}
