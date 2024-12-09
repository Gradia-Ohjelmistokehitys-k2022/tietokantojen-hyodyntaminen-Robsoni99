using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading.Channels;
using System.Data.Common;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net;


namespace Tietokantojen_hyödyntäminen.Models
{
    internal class DataBaseRepository
    {

        private string _connectionString;

        public DataBaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string IsDbConnectionEstablished()
        {
            using var connection = new SqlConnection(_connectionString);

            try
            {
                connection.Open();
                return "Connection established!";
            }
            catch (SqlException ex)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> Books = new();

            using var dbConnection = new SqlConnection(_connectionString); // uusi tapa käyttää using-ominaisuutta. Tämä huolehtii yhteyden sulkemisesta.

            dbConnection.Open(); //avataan yhteys tietokantaan

            using var command = new SqlCommand("SELECT * FROM Book WHERE PublicationYear >= 2019", dbConnection); // kysely ja tietokannan osoite
            using var reader = command.ExecuteReader(); // olio, jolla luetaan tietoja kannasta
            while (reader.Read()) // silmukka, joka lukee kantaa niin kauan kuin siellä on rivejä, joital lukea
            {
                Book book = new() // jokaiselle riville luodaan uusi olio, johon tiedot tallennetaan
                {
                    BookId = Convert.ToInt32(reader["BookId"]),
                    Title = reader["Title"].ToString(),
                    ISBN = reader["ISBN"].ToString(),
                    PublicationYear = Convert.ToInt32(reader["PublicationYear"]),
                    AvailableCopies = Convert.ToInt32(reader["AvailableCopies"])
                };
                Books.Add(book);

            }



            return Books;
        }

        public int GetAverageAge()
        {
            List<User> Member = new();

            using var dbConnection = new SqlConnection(_connectionString); // uusi tapa käyttää using-ominaisuutta. Tämä huolehtii yhteyden sulkemisesta.

            dbConnection.Open();

            using var command = new SqlCommand("SELECT AVG(BirthDate) AS AverageAge From Member", dbConnection);
            using var reader = command.ExecuteReader();
            int BirthDate = 0;
            while (reader.Read()) // silmukka, joka lukee kantaa niin kauan kuin siellä on rivejä, joital lukea
            {
                BirthDate = Convert.ToInt32(reader["AverageAge"]);
            }
            int year = System.DateTime.Now.Year;
            int age = year - BirthDate;

            return age;
        }
        public Book GetMostAvailableCopies()
        {
            List<User> Member = new();

            using var dbConnection = new SqlConnection(_connectionString); // uusi tapa käyttää using-ominaisuutta. Tämä huolehtii yhteyden sulkemisesta.

            dbConnection.Open();

            using var command = new SqlCommand("SELECT * FROM BOOK WHERE AvailableCopies = (SELECT MAX(AvailableCopies) FROM BOOK);", dbConnection);
            using var reader = command.ExecuteReader();
            reader.Read();
            Book book = new()
            {
                BookId = Convert.ToInt32(reader["BookId"]),
                Title = reader["Title"].ToString(),
                ISBN = reader["ISBN"].ToString(),
                PublicationYear = Convert.ToInt32(reader["PublicationYear"]),
                AvailableCopies = Convert.ToInt32(reader["AvailableCopies"]),
            };

            return book;
        }

        public List<string> GetActiveUsers()
        {
            List<string> ActiveUsers = new();

            using var dbConnection = new SqlConnection(_connectionString); // uusi tapa käyttää using-ominaisuutta. Tämä huolehtii yhteyden sulkemisesta.

            dbConnection.Open(); //avataan yhteys tietokantaan

            using var command = new SqlCommand("select member.FirstName, member.LastName, Book.ISBN from Loan INNER JOIN member on loan.MemberId=Member.MemberId INNER JOIN Book on book.BookId=Loan.BookId;", dbConnection); // kysely ja tietokannan osoite
            using var reader = command.ExecuteReader(); // olio, jolla luetaan tietoja kannasta
            while (reader.Read()) // silmukka, joka lukee kantaa niin kauan kuin siellä on rivejä, joital lukea
            {
                string activeuser = reader["FirstName"].ToString() + " " + reader["LastName"].ToString() + ", " + reader["ISBN"].ToString();
                ActiveUsers.Add(activeuser);
            }
            

            return ActiveUsers;
        }
    }
}

