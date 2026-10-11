using Library_System;
using Library_System.Items;
using Library_System.Members;
using Library_System.Staff;

namespace LibraryApp
{
    public class LibrarySystem
    {
        private readonly List<Member> members = new List<Member>();
        private readonly List<Staff> staff = new List<Staff>();
        private readonly List<LibraryItem> items = new List<LibraryItem>();
        private readonly List<Loan> loans = new List<Loan>();
        private int nextLoanNumber = 1;

        public void RegisterMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            EnsurePersonIdIsFree(member.PersonId);
            members.Add(member);
        }

        public void HireStaff(Staff newStaff)
        {
            if (newStaff == null)
                throw new ArgumentNullException(nameof(newStaff));
            EnsurePersonIdIsFree(newStaff.PersonId);
            staff.Add(newStaff);
        }

        public void AddItem(LibraryItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            foreach (LibraryItem existing in items)
            {
                if (existing.CatalogNumber == item.CatalogNumber)
                    throw new InvalidOperationException(
                        "An item with catalog number " + item.CatalogNumber + " already exists");
            }
            items.Add(item);
        }

        public Loan BorrowItem(string memberId, string catalogNumber, DateTime date)
        {
            Member member = FindMember(memberId);
            LibraryItem item = FindItem(catalogNumber);


            string loanId = "L" + nextLoanNumber.ToString("D3");
            Loan loan = member.Borrow(item, loanId, date);

            nextLoanNumber++;
            loans.Add(loan);
            return loan;
        }

        public void ReturnItem(string staffId, string loanId, DateTime date)
        {
            Librarian librarian = GetLibrarian(staffId);
            librarian.ProcessReturn(FindLoan(loanId), date);
        }

        public void MarkLoanLost(string staffId, string loanId)
        {
            Librarian librarian = GetLibrarian(staffId);
            librarian.MarkLost(FindLoan(loanId));
        }

        public Member FindMember(string id)
        {
            foreach (Member m in members)
                if (m.PersonId == id) return m;
            throw new InvalidOperationException("No member found with ID " + id);
        }

        public Staff FindStaff(string id)
        {
            foreach (Staff s in staff)
                if (s.PersonId == id) return s;
            throw new InvalidOperationException("No staff member found with ID " + id);
        }

        public LibraryItem FindItem(string catalogNumber)
        {
            foreach (LibraryItem i in items)
                if (i.CatalogNumber == catalogNumber) return i;
            throw new InvalidOperationException("No item found with catalog number " + catalogNumber);
        }

        public Loan FindLoan(string loanId)
        {
            foreach (Loan l in loans)
                if (l.LoanId == loanId) return l;
            throw new InvalidOperationException("No loan found with ID " + loanId);
        }


        private void EnsurePersonIdIsFree(string personId)
        {
            foreach (Member m in members)
                if (m.PersonId == personId)
                    throw new InvalidOperationException("ID " + personId + " is already registered");
            foreach (Staff s in staff)
                if (s.PersonId == personId)
                    throw new InvalidOperationException("ID " + personId + " is already registered");
        }

        private Librarian GetLibrarian(string staffId)
        {
            Librarian librarian = FindStaff(staffId) as Librarian;
            if (librarian == null)
                throw new InvalidOperationException("Only a librarian can do this action");
            return librarian;
        }

        public IReadOnlyList<Member> GetAllMembers()
        {
            return members.AsReadOnly();
        }

        public IReadOnlyList<Staff> GetAllStaff()
        {
            return staff.AsReadOnly();
        }

        public IReadOnlyList<Loan> GetAllLoans()
        {
            return loans.AsReadOnly();
        }
    }
}