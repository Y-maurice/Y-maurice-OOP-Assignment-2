## 1- GradeBook Responsibilities

| Method | Responsibility | Reason to Change | Belongs in `GradeBook`? |
|---|---|---|---|
| `Record()` | Store/record student scores | The way student scores are stored or recorded changes | Yes |
| `Average()` | Calculate student averages | The formula or rules for calculating the average change | No |
| `Letter()` | Determine the student's letter grade | The academic grading policy change | No |
| `MeetsHonorRoll()` | Determine whether a student qualifies for the honor roll | The academic rules for honor-roll eligibility change | No |
| `TranscriptPlain()` | Format a student's transcript as plain text | The transcript's text layout or presentation format changes | No |
| `ExportCsv()` | Export grade information as CSV | The CSV structure or export format changes | No |

## 2- WardBoard Reponsibilities

| Method | Responsibility | Reason to Change | Belongs in `WardBoard`? |
|---|---|---|---|
| `AssignBed()` | Assign a patient to a bed | The way patients are assigned or bed assignments are managed changes | Yes, but the acuity calculation and pager logging inside the method should be separated |
| `ScoreAcuity()` | Calculate the patient's clinical acuity score | The clinical scoring rules or thresholds change | No |
| `BuildHandoffNote()` | Format a nurse handoff note | The handoff note's presentation or narrative format changes | No |
| `DrainPagerLog()` | Manage and retrieve pager alerts/logs | The way pager alerts are stored, retrieved, or cleared changes | No |
| `ExportCensusCsv()` | Export ward census information as CSV | The census export format or CSV structure changes | No |

## 3- WarehousePickList Resonsibilities

| Method | Responsibility | Reason to Change | Belongs in `WarehousePickList`? |
|---|---|---|---|
| `AddNeed()` | Record items that need to be picked | The way pick-list requirements are recorded or managed changes | Yes |
| `Allocate()` | Determine how much stock can be allocated for each requested item | The inventory allocation or shortage policy changes | No |
| `WalkingOrder()` | Determine the order in which items should be picked through the warehouse | The warehouse layout or path/walking strategy changes | No |
| `PickerScript()` | Generate instructions and shortage warnings for the picker | The handheld device UX or picker instruction format changes | No |
| `WmsXmlBatch()` | Generate an XML batch for the WMS integration | The WMS integration contract or XML format changes | No |

## 4- CheckoutBasket Responsibilities

| Method | Responsibility | Reason to Change | Belongs in `CheckoutBasket`? |
|---|---|---|---|
| `AddLine()` | Add products/lines to the checkout basket | The way products are added or basket contents are managed changes | Yes |
| `ApplyCouponText()` | Store/interpret coupon information | The coupon format or marketing rules change | No |
| `EnableGiftWrap()` | Record that gift wrapping was requested | The way gift-wrap selection is represented or managed changes | Yes |
| `SubTotal()` | Calculate the subtotal of basket items | The pricing or subtotal calculation rules change | No |
| `DiscountAmount()` | Calculate the discount based on coupon rules | Coupon/discount policies or coupon formats change | No |
| `GrandTotal()` | Calculate the final amount after discounts and additional fees | Overall pricing, discount, or gift-wrap pricing policies change | No |
| `GiftMessageCard()` | Generate customer-facing gift message content | The gift-message wording or presentation changes | No |
| `AuthorizePaymentStub()` | Authorize the customer's payment | The payment gateway or payment authorization mechanism changes | No |

## 5- AppointmentDesk Responsibilities

| Method | Responsibility | Reason to Change | Belongs in `AppointmentDesk`? |
|---|---|---|---|
| `IsWithinBusinessHours()` | Determine whether a time falls within the clinic's business hours | The clinic's opening-hours or working-day policy changes | No |
| `FindNextSlot()` | Find the next available appointment slot | The slot-search or appointment scheduling logic changes | Yes |
| `TryBook()` | Book an available appointment slot | The way appointments are booked or tracked changes | Yes |
| `ToIcs()` | Generate an ICS calendar representation of an appointment | The calendar/ICS serialization format or interoperability requirements change | No |
| `SmsReminder()` | Generate an SMS appointment reminder | The messaging channel or reminder wording changes | No |
| `Align()` | Align a time to the appointment slot interval | The internal slot-alignment calculation changes | Yes (supporting logic) |


## 6- CourseEnrollmentDesk Responsibilities

| Method | Responsibility | Reason to Change | Belongs in `CourseEnrollmentDesk`? |
|---|---|---|---|
| `Register()` | Register a student by assigning a seat or adding them to the waitlist | Enrollment and capacity rules change | Yes |
| `WaitlistPosition()` | Determine a student's position in the waitlist | Waitlist management or positioning rules change | Yes |
| `WelcomePacketMarkdown()` | Generate a Markdown welcome packet for a student | Academy marketing content or document formatting changes | No |
| `TuitionInvoiceLine()` | Generate a tuition invoice line including VAT | Finance, tax, or invoice formatting rules change | No |
| `PromoteFromWaitlist()` | Promote students from the waitlist when seats become available | Waitlist promotion or enrollment policy changes | Yes |

## 7- LoanDesk Responsibilities

| Method | Responsibility | Reason to Change | Belongs in `LoanDesk`? |
|---|---|---|---|
| `RiskScore()` | Calculate the applicant's risk score | The risk model or scoring rules change | Yes |
| `IsEligible()` | Determine whether the applicant is eligible for the loan | Loan eligibility or underwriting rules change | Yes |
| `RequiredDocuments()` | Determine which documents the applicant must provide | Compliance or documentation requirements change | No |
| `DecisionLetter()` | Generate the loan decision letter | Legal/communication wording or letter format changes | No |
| `UnderwriterCsvRow()` | Generate a CSV row for underwriter/analytics systems | Analytics or export schema changes | No |


## 8- SubscriptionBilling Responsibilities

| Method | Responsibility | Reason to Change | Belongs in `SubscriptionBilling`? |
|---|---|---|---|
| `Prorate()` | Calculate the prorated subscription amount | Billing/proration rules or finance calendar rules change | Yes |
| `NextInvoiceNumber()` | Generate the next invoice number | Invoice numbering or fiscal-prefix rules change | No |
| `RegisterFailedPayment()` | Record a failed payment attempt | The way failed payment attempts are tracked changes | Yes |
| `DunningEmail()` | Generate a payment-failure/dunning email | Collections messaging, tone, or legal wording changes | No |
| `LedgerJournalLine()` | Generate an accounting ledger journal line | Accounting export format or ledger integration requirements change | No |

## 9- SupportTicket Responsibilities

| Method | Responsibility | Reason to Change | Belongs in `SupportTicket`? |
|---|---|---|---|
| `AppendCustomerMessage()` | Add a new customer message to the ticket | The way customer messages are stored or appended changes | Yes |
| `RecalculatePriorityFromText()` | Determine ticket priority from the subject and body using keyword rules | Support priority classification rules or keyword heuristics change | No |
| `SlaDeadline()` | Calculate the SLA deadline based on ticket priority | SLA policies or response-time rules change | No |
| `IsBreached()` | Determine whether the ticket has exceeded its SLA deadline | SLA breach rules change | No |
| `DraftPublicReply()` | Generate a customer-facing support reply | Customer communication templates or tone change | No |
| `InternalEscalationBlurb()` | Generate an internal escalation message | Internal escalation message format or content changes | No |

## 10- KitchenTicket

| Method | Responsibility | Reason to Change | Belongs in `KitchenTicket`? |
|---|---|---|---|
| `AddItem()` | Add an item and its preparation information to the kitchen ticket | The way kitchen items or ingredients are recorded changes | Yes |
| `DetectAllergens()` | Detect allergens from the ingredients of the ticket items | Allergen definitions or regulatory allergen rules change | No |
| `EstimatedReadyMinutes()` | Estimate when the order will be ready | Kitchen scheduling, preparation-time, or station-capacity rules change | No |
| `RenderThermalTicket()` | Generate the formatted thermal-printer ticket | Printer hardware, layout, or formatting requirements change | No |
| `ExpoLaneHint()` | Determine which kitchen/expo lane should handle the order | Expo routing or kitchen operational rules change | No |