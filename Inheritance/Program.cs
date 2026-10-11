using Library_System.Items;
using Library_System.Members;
using Library_System.Staff;

namespace LibraryApp
{
    class Program
    {
        static void Main()
        {
            LibrarySystem system = new LibrarySystem();
            Seed(system);
            PrintSeededData();

            new Menu(system).Run();
        }

        static void Seed(LibrarySystem system)
        {
            // Members
            system.RegisterMember(new StudentMember("MEM01", "Ali Hassan", "0100000001"));
            system.RegisterMember(new PremiumMember("MEM02", "Sara Adel", "0100000002", 10m));
            system.RegisterMember(new StudentMember("MEM03", "Nour Ahmed", "0100000003"));

            // Staff
            system.HireStaff(new Librarian("ST01", "Omar Samy", "0100000011", new DateTime(2024, 1, 1), 3000m));
            system.HireStaff(new HeadLibrarian("ST02", "Mona Khaled", "0100000012", new DateTime(2020, 5, 1), 4000m));
            system.HireStaff(new Shelver("ST03", "Hany Nabil", "0100000013", new DateTime(2025, 3, 1), 2500m, "Fiction"));

            // Items
            system.AddItem(new Book("BK-1", "Clean Code", 2m));
            system.AddItem(new Book("BK-2", "The Pragmatic Programmer", 2m));
            system.AddItem(new Book("BK-3", "Refactoring", 2m));
            system.AddItem(new DVD("DVD-1", "The Matrix", 2m));
            system.AddItem(new DVD("DVD-2", "Inception", 2m));
            system.AddItem(new Magazine("MAG-1", "Tech Monthly", 2m));
            system.AddItem(new Magazine("MAG-2", "Science Today", 2m));

            //loans
            system.BorrowItem("MEM01", "BK-1", DateTime.Today.AddDays(-10));  // book: due in 11 days
            system.BorrowItem("MEM02", "DVD-1", DateTime.Today.AddDays(-10)); // DVD: due 3 days ago
        }

        static void PrintSeededData()
        {
            Console.WriteLine("Sample data loaded:");
            Console.WriteLine("  Members:  MEM01 Ali (student), MEM02 Sara (premium, 10% discount), MEM03 Nour (student)");
            Console.WriteLine("  Staff:    ST01 Omar (librarian), ST02 Mona (head librarian), ST03 Hany (shelver)");
            Console.WriteLine("  Items:    BK-1, BK-2, BK-3 (books), DVD-1, DVD-2 (DVDs), MAG-1, MAG-2 (magazines)");
            Console.WriteLine("  Loans:    L001 (MEM01, BK-1, due in 11 days), L002 (MEM02, DVD-1, 3 days overdue)");
            Console.WriteLine("  Only ST01 can return items or mark them lost, because only a librarian may do that.");
        }
    }
}