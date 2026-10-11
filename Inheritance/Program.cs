using Library_System.Items;
using Library_System.Members;
using Library_System.Staff;

namespace LibraryApp
{
    class Program
    {
        static void Main()
        {
            // Task 3.3: prove each rule (uses its own LibrarySystem, so the menu data stays untouched)
            RunRulesDemo();

            Console.WriteLine();
            Console.WriteLine("Press Enter to start the menu...");
            Console.ReadLine();

            LibrarySystem system = new LibrarySystem();
            Seed(system);
            PrintSeededData();

            new Menu(system).Run();
        }

        static void RunRulesDemo()
        {
            LibrarySystem demo = new LibrarySystem();

            // ---------- Setup ----------
            StudentMember student = new StudentMember("MEM01", "Ali Hassan", "0100000001");
            PremiumMember premium = new PremiumMember("MEM02", "Sara Adel", "0100000002", 10m);
            Librarian librarian = new Librarian("ST01", "Omar Samy", "0100000003", new DateTime(2024, 1, 1), 3000m);
            HeadLibrarian head = new HeadLibrarian("ST02", "Mona Khaled", "0100000004", new DateTime(2020, 5, 1), 4000m);
            Shelver shelver = new Shelver("ST03", "Hany Nabil", "0100000005", new DateTime(2025, 3, 1), 2500m, "Fiction");

            demo.RegisterMember(student);
            demo.RegisterMember(premium);
            demo.HireStaff(librarian);
            demo.HireStaff(head);
            demo.HireStaff(shelver);

            Book book1 = new Book("BK-1", "Clean Code", 2m);
            Book book2 = new Book("BK-2", "The Pragmatic Programmer", 2m);
            Book book3 = new Book("BK-3", "Refactoring", 2m);
            DVD dvd1 = new DVD("DVD-1", "The Matrix", 2m);
            DVD dvd2 = new DVD("DVD-2", "Inception", 2m);
            Magazine mag1 = new Magazine("MAG-1", "Tech Monthly", 2m);

            demo.AddItem(book1);
            demo.AddItem(book2);
            demo.AddItem(book3);
            demo.AddItem(dvd1);
            demo.AddItem(dvd2);
            demo.AddItem(mag1);

            Section("Must NOT compile (uncomment one at a time to see the error)");

            // Person p = new Person("X1", "Test", "010");                          // must NOT compile: protected constructor
            // Member m = new Member("X2", "Test", "010", 3, 0m);                   // must NOT compile: protected constructor
            // Staff s = new Staff("X3", "Test", "010", DateTime.Today, 1000m, 0m); // must NOT compile: protected constructor
            // LibraryItem i = new LibraryItem("X4", "Test", 1m, 7, 1m);            // must NOT compile: protected constructor
            // student.FullName = "Someone Else";                                   // must NOT compile: get-only
            // student.Loans.Add(null);                                             // must NOT compile: IReadOnlyList has no Add
            // book1.IsOnLoan = true;                                               // must NOT compile: private set
            // librarian.MonthlySalary = 99999m;                                    // must NOT compile: private set
            // shelver.Section = "Children";                                        // must NOT compile: private set

            Section("Validation at creation");

            try
            {
                StudentMember bad = new StudentMember("", "No Id", "010");
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected: " + ex.Message); }

            try
            {
                StudentMember bad = new StudentMember("MEM09", "No Phone", null);
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected: " + ex.Message); }

            try
            {
                demo.RegisterMember(new StudentMember("MEM01", "Duplicate Id", "010"));
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected: " + ex.Message); }

            Section("Raises");

            try
            {
                librarian.GiveRaise(0m);
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected zero raise: " + ex.Message); }

            try
            {
                librarian.GiveRaise(-5m);
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected negative raise: " + ex.Message); }

            librarian.GiveRaise(10m);
            Console.WriteLine("Librarian salary after a 10% raise: " + librarian.MonthlySalary);

            Section("Borrowing rules");

            // Withdrawn item
            head.Withdraw(dvd2);
            try
            {
                demo.BorrowItem("MEM02", "DVD-2", new DateTime(2026, 10, 1));
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected (withdrawn): " + ex.Message); }
            head.Restore(dvd2);

            // Student borrows 3 items the 4th is rejected
            demo.BorrowItem("MEM01", "BK-1", new DateTime(2026, 10, 1));
            demo.BorrowItem("MEM01", "BK-2", new DateTime(2026, 10, 1));
            var lostCandidate = demo.BorrowItem("MEM01", "BK-3", new DateTime(2026, 10, 1));
            Console.WriteLine("Student now has " + student.Loans.Count + " loans.");

            try
            {
                demo.BorrowItem("MEM01", "MAG-1", new DateTime(2026, 10, 1));
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected (4th loan): " + ex.Message); }

            // Item already on loan
            try
            {
                demo.BorrowItem("MEM02", "BK-1", new DateTime(2026, 10, 2));
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected (already on loan): " + ex.Message); }

            Section("Monthly pay for each kind of staff");

            List<Staff> allStaff = new List<Staff>();
            allStaff.Add(librarian);
            allStaff.Add(head);
            allStaff.Add(shelver);

            foreach (Staff person in allStaff)
            {
                Console.WriteLine(person.FullName + " (" + person.GetType().Name + "): monthly pay = " + person.MonthlyPay);
            }

            Section("Loan period and daily late fee for each kind of item");

            List<LibraryItem> allItems = new List<LibraryItem>();
            allItems.Add(book1);
            allItems.Add(dvd1);
            allItems.Add(mag1);

            foreach (LibraryItem item in allItems)
            {
                Console.WriteLine(item.Title + " (" + item.GetType().Name + "): loan period = "
                                  + item.LoanPeriodDays + " days, daily late fee = " + item.GetDailyLateFee());
            }

            Section("Premium member returns a DVD 5 days late");

            var dvdLoan = demo.BorrowItem("MEM02", "DVD-1", new DateTime(2026, 10, 1));
            Console.WriteLine("Borrowed on " + dvdLoan.BorrowDate.ToShortDateString()
                              + ", due " + dvdLoan.DueDate.ToShortDateString());

            DateTime returnDate = dvdLoan.DueDate.AddDays(5);
            demo.ReturnItem("ST01", dvdLoan.LoanId, returnDate);

            Console.WriteLine("Returned on " + returnDate.ToShortDateString());
            Console.WriteLine("Due date: " + dvdLoan.DueDate.ToShortDateString());
            Console.WriteLine("Late fee: " + dvdLoan.LateFee);
            Console.WriteLine("Reading points: " + premium.ReadingPoints);

            Section(" Illegal status changes");

            try
            {
                demo.ReturnItem("ST01", dvdLoan.LoanId, returnDate);
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected (returned twice): " + ex.Message); }

            try
            {
                demo.MarkLoanLost("ST01", dvdLoan.LoanId);
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected (returned loan marked lost): " + ex.Message); }

            try
            {
                demo.ReturnItem("ST01", lostCandidate.LoanId, new DateTime(2026, 9, 1));
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected (return date before borrow date): " + ex.Message); }

            demo.MarkLoanLost("ST01", lostCandidate.LoanId);
            Console.WriteLine("Loan " + lostCandidate.LoanId + " status: " + lostCandidate.Status);

            try
            {
                demo.ReturnItem("ST01", lostCandidate.LoanId, new DateTime(2026, 10, 20));
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected (lost loan returned): " + ex.Message); }

            try
            {
                demo.ReturnItem("ST03", dvdLoan.LoanId, returnDate);
                Console.WriteLine("ERROR: this should have been rejected.");
            }
            catch (Exception ex) { Console.WriteLine("Rejected (shelver is not a librarian): " + ex.Message); }
        }

        static void Section(string title)
        {
            Console.WriteLine();
            Console.WriteLine("=== " + title + " ===");
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