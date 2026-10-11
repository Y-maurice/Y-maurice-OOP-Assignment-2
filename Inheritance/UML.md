# Library System: Class Diagram


```mermaid
classDiagram
    direction TB

    class Person {
        +string PersonId
        +string FullName
        +string Phone
        #Person(id, fullName, phone)
    }

    class Member {
        -List~Loan~ loans
        +IReadOnlyList~Loan~ Loans
        +int MaxLoans
        +decimal LateFeeDiscount
        #Member(id, fullName, phone, maxLoans, discount)
        +Borrow(LibraryItem item, string loanId, DateTime date) Loan
    }

    class StudentMember {
        +StudentMember(id, fullName, phone)
    }

    class PremiumMember {
        +int ReadingPoints
        +PremiumMember(id, fullName, phone, discount)
    }

    class Staff {
        +DateTime HireDate
        +decimal MonthlySalary
        -decimal allowance
        #Staff(id, fullName, phone, hireDate, salary, allowance)
        +GiveRaise(decimal percentage)
        +GetMonthlyPay() decimal
    }

    class Librarian {
        +Librarian(id, fullName, phone, hireDate, salary)
        +ProcessReturn(Loan loan, DateTime returnDate)
        +MarkLost(Loan loan)
    }

    class Shelver {
        +string Section
        +Shelver(id, fullName, phone, hireDate, salary, section)
        +Reassign(string newSection)
    }

    class HeadLibrarian {
        +HeadLibrarian(id, fullName, phone, hireDate, salary)
        +ChangeLateFee(LibraryItem item, decimal newFee)
        +Withdraw(LibraryItem item)
        +Restore(LibraryItem item)
    }

    class LibraryItem {
        +string CatalogNumber
        +string Title
        +decimal BaseLateFee
        +bool IsWithdrawn
        +bool IsOnLoan
        +int LoanPeriodDays
        -decimal lateFeeMultiplier
        #LibraryItem(catalogNumber, title, baseLateFee, loanPeriodDays, multiplier)
        +GetDailyLateFee() decimal
        ~SetBaseLateFee(decimal fee)
        ~Withdraw()
        ~Restore()
        ~MarkBorrowed()
        ~MarkeReturned()
    }

    class Book {
        +Book(catalogNumber, title, baseLateFee)
    }

    class DVD {
        +DVD(catalogNumber, title, baseLateFee)
    }

    class Magazine {
        +Magazine(catalogNumber, title, baseLateFee)
    }

    class Loan {
        +string LoanId
        +DateTime BorrowDate
        +Member Member
        +LibraryItem Item
        +LoanStatus Status
        +DateTime? ReturnDate
        +DateTime DueDate
        +decimal LateFee
        ~Loan(loanId, member, item, borrowDate)
        ~Return(DateTime returnDate)
        ~MarkLost()
    }

    class LoanStatus {
        <<enumeration>>
        Borrowed
        Returned
        Lost
    }

    class LibrarySystem {
        -List~Member~ members
        -List~Staff~ staff
        -List~LibraryItem~ items
        -List~Loan~ loans
        +RegisterMember(Member member)
        +HireStaff(Staff staff)
        +AddItem(LibraryItem item)
        +BorrowItem(string memberId, string catalogNumber, DateTime date) Loan
        +ReturnItem(string staffId, string loanId, DateTime date)
        +MarkLoanLost(string staffId, string loanId)
        +FindMember(string id) Member
        +FindStaff(string id) Staff
        +FindItem(string catalogNumber) LibraryItem
        +FindLoan(string loanId) Loan
    }

    class Menu {
        -LibrarySystem system
        +Menu(LibrarySystem system)
        +Run()
    }

    Person <|-- Member
    Person <|-- Staff
    Member <|-- StudentMember
    Member <|-- PremiumMember
    Staff <|-- Librarian
    Staff <|-- Shelver
    Staff <|-- HeadLibrarian
    LibraryItem <|-- Book
    LibraryItem <|-- DVD
    LibraryItem <|-- Magazine

    Member "1" --> "0..*" Loan : has
    Loan "*" --> "1" LibraryItem : for
    Loan --> LoanStatus : Status
    Librarian ..> Loan : processes
    HeadLibrarian ..> LibraryItem : manages

    LibrarySystem "1" o-- "0..*" Member
    LibrarySystem "1" o-- "0..*" Staff
    LibrarySystem "1" o-- "0..*" LibraryItem
    LibrarySystem "1" o-- "0..*" Loan
    Menu "1" --> "1" LibrarySystem : uses

    note for Person "PersonId, FullName, Phone: get-only, set once in ctor, null/empty rejected"
    note for Member "loans is private; Loans is a read-only view. Borrow checks limit, withdrawn, already on loan"
    note for Staff "MonthlySalary: private set, only via GiveRaise (zero or negative rejected)"
    note for Shelver "Section: private set, only via Reassign"
    note for LibraryItem "BaseLateFee, IsWithdrawn: private set. IsOnLoan: internal set. One GetDailyLateFee for all kinds"
    note for Loan "Status: private set. DueDate, LateFee computed. Return date cannot be before BorrowDate"
    note for PremiumMember "ReadingPoints is computed: 5 x returned loans"
    note for LibrarySystem "Rejects duplicate PersonId, CatalogNumber, LoanId. Delegates all other rules to the owning class"
```

## Values each child passes up through `base(...)`

| Class | Values |
|---|---|
| StudentMember | maxLoans 3, discount 0 |
| PremiumMember | maxLoans 10, discount from constructor |
| Librarian / Shelver | allowance 0 |
| HeadLibrarian | allowance 400 |
| Book | loan period 21 days, fee multiplier 1 |
| DVD | loan period 7 days, fee multiplier 2 |
| Magazine | loan period 4 days, fee multiplier 0.5 |

