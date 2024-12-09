using Tietokantojen_hyödyntäminen.Models;

namespace Tietokantojen_hyödyntäminen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var DataBaseRepository = new DataBaseRepository("server=(localdb)\\MSSQLLocalDB; Trusted_Connection=true; database=Library;");
            Console.WriteLine(DataBaseRepository.IsDbConnectionEstablished());
            var Books = DataBaseRepository.GetAllBooks();
            foreach (var book in Books)
            {
                Console.WriteLine(book.Title);
            }

            Console.WriteLine(DataBaseRepository.GetAverageAge());

            Console.WriteLine(DataBaseRepository.GetMostAvailableCopies().Title);

            var ActiveUsers = DataBaseRepository.GetActiveUsers();
            foreach (var activeuser in ActiveUsers)
            {
                Console.WriteLine(activeuser);
            }
        }
    }
}
