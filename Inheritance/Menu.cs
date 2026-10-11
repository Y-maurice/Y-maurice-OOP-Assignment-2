using Library_System;
using Library_System.Items;
using Library_System.Members;
using Library_System.Staff;

namespace LibraryApp
{
    public class Menu
    {
        private readonly LibrarySystem system;

        public Menu(LibrarySystem system)
        {
            if (system == null)
                throw new ArgumentNullException(nameof(system));
            this.system = system;
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Library System ===");
                Console.WriteLine("1. Register student member");
                Console.WriteLine("2. Register premium member");
                Console.WriteLine("3. Hire librarian");
                Console.WriteLine("4. Add item (book / DVD / magazine)");
                Console.WriteLine("5. Borrow item");
                Console.WriteLine("6. Return item");
                Console.WriteLine("7. Mark loan as lost");
                Console.WriteLine("8. Show member's loans");
                Console.WriteLine("9. Show all members");
                Console.WriteLine("10. Show all staff");
                Console.WriteLine("11. Show all loans");
                Console.WriteLine("0. Exit");

                int choice = InputHelper.AskInt("Choose: ", 0, 11);

                try
                {
                    switch (choice)
                    {
                        case 1: RegisterStudent(); break;
                        case 2: RegisterPremium(); break;
                        case 3: HireLibrarian(); break;
                        case 4: AddItem(); break;
                        case 5: Borrow(); break;
                        case 6: Return(); break;
                        case 7: MarkLost(); break;
                        case 8: ShowLoans(); break;
                        case 9: ShowAllMembers(); break;
                        case 10: ShowAllStaff(); break;
                        case 11: ShowAllLoans(); break;
                        case 0: running = false; break;
                    }
                }
                catch (Exception ex)
                {
                    // Business rule violations (loan limit, withdrawn item, unknown ID, ...)
                    // arrive here as exceptions with a clear message.
                    Console.WriteLine("Rejected: " + ex.Message);
                }
            }
        }

        private void RegisterStudent()
        {
            string id = InputHelper.AskText("ID: ");
            string name = InputHelper.AskText("Full name: ");
            string phone = InputHelper.AskText("Phone: ");

            system.RegisterMember(new StudentMember(id, name, phone));
            Console.WriteLine("Student member registered.");
        }

        private void RegisterPremium()
        {
            string id = InputHelper.AskText("ID: ");
            string name = InputHelper.AskText("Full name: ");
            string phone = InputHelper.AskText("Phone: ");
            decimal discount = InputHelper.AskDecimalInRange("Late-fee discount % (0-100): ", 0m, 100m);

            system.RegisterMember(new PremiumMember(id, name, phone, discount));
            Console.WriteLine("Premium member registered.");
        }

        private void HireLibrarian()
        {
            string id = InputHelper.AskText("ID: ");
            string name = InputHelper.AskText("Full name: ");
            string phone = InputHelper.AskText("Phone: ");
            decimal salary = InputHelper.AskPositiveDecimal("Monthly salary: ");

            system.HireStaff(new Librarian(id, name, phone, DateTime.Today, salary));
            Console.WriteLine("Librarian hired.");
        }

        private void AddItem()
        {
            string kind = InputHelper.AskChoice("Kind (book / dvd / magazine): ",
                                                new string[] { "book", "dvd", "magazine" });
            string number = InputHelper.AskText("Catalog number: ");
            string title = InputHelper.AskText("Title: ");
            decimal fee = InputHelper.AskPositiveDecimal("Base late fee: ");

            LibraryItem item;
            if (kind == "book") item = new Book(number, title, fee);
            else if (kind == "dvd") item = new DVD(number, title, fee);
            else item = new Magazine(number, title, fee);

            system.AddItem(item);
            Console.WriteLine("Item added.");
        }

        private void Borrow()
        {
            string memberId = InputHelper.AskText("Member ID: ");
            string catalogNumber = InputHelper.AskText("Catalog number: ");

            Loan loan = system.BorrowItem(memberId, catalogNumber, DateTime.Today);
            Console.WriteLine("Borrowed. Loan " + loan.LoanId + ", due " + loan.DueDate.ToShortDateString() + ".");
        }

        private void Return()
        {
            string staffId = InputHelper.AskText("Librarian ID: ");
            string loanId = InputHelper.AskText("Loan ID: ");
            DateTime date = InputHelper.AskDate("Return date (yyyy-MM-dd, Enter for today): ", true);

            system.ReturnItem(staffId, loanId, date);

            Loan loan = system.FindLoan(loanId);
            Console.WriteLine("Returned. Due date: " + loan.DueDate.ToShortDateString()
                              + ", late fee: " + loan.LateFee + ".");
        }

        private void MarkLost()
        {
            string staffId = InputHelper.AskText("Librarian ID: ");
            string loanId = InputHelper.AskText("Loan ID: ");

            system.MarkLoanLost(staffId, loanId);
            Console.WriteLine("Loan marked as lost.");
        }

        private void ShowLoans()
        {
            Member member = system.FindMember(InputHelper.AskText("Member ID: "));

            if (member.Loans.Count == 0)
                Console.WriteLine("No loans yet.");

            foreach (Loan loan in member.Loans)
            {
                Console.WriteLine(loan.LoanId + " | " + loan.Item.Title + " | " + loan.Status
                                  + " | due " + loan.DueDate.ToShortDateString());
            }

            PremiumMember premium = member as PremiumMember;
            if (premium != null)
                Console.WriteLine("Reading points: " + premium.ReadingPoints);
        }

        private void ShowAllMembers()
        {
            if (system.GetAllMembers().Count == 0)
            {
                Console.WriteLine("No members registered.");
                return;
            }

            foreach (Member member in system.GetAllMembers())
            {
                Console.WriteLine(member.PersonId + " | " + member.FullName
                                  + " | " + member.GetType().Name
                                  + " | phone " + member.Phone
                                  + " | max loans " + member.MaxLoans
                                  + " | discount " + member.LateFeeDiscount + "%"
                                  + " | total loans " + member.Loans.Count);
            }
        }

        private void ShowAllStaff()
        {
            if (system.GetAllStaff().Count == 0)
            {
                Console.WriteLine("No staff hired.");
                return;
            }

            foreach (Staff person in system.GetAllStaff())
            {
                Console.WriteLine(person.PersonId + " | " + person.FullName
                                  + " | " + person.GetType().Name
                                  + " | phone " + person.Phone
                                  + " | hired " + person.HireDate.ToShortDateString()
                                  + " | salary " + person.MonthlySalary
                                  + " | monthly pay " + person.MonthlyPay);
            }
        }

        private void ShowAllLoans()
        {
            if (system.GetAllLoans().Count == 0)
            {
                Console.WriteLine("No loans yet.");
                return;
            }

            foreach (Loan loan in system.GetAllLoans())
            {
                string returned = loan.ReturnDate == null
                    ? "not returned"
                    : "returned " + loan.ReturnDate.Value.ToShortDateString();

                Console.WriteLine(loan.LoanId + " | " + loan.Member.FullName
                                  + " | " + loan.Item.Title
                                  + " | " + loan.Status
                                  + " | borrowed " + loan.BorrowDate.ToShortDateString()
                                  + " | due " + loan.DueDate.ToShortDateString()
                                  + " | " + returned
                                  + " | late fee " + loan.LateFee);
            }
        }
    }
}