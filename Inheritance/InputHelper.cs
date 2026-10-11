using System.Globalization;

namespace LibraryApp
{

    public static class InputHelper
    {

        public static string AskText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(text))
                    return text.Trim();

                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }


        public static int AskInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();
                int value;

                if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                {
                    Console.WriteLine("Please enter a whole number.");
                    continue;
                }
                if (value < min || value > max)
                {
                    Console.WriteLine("The number must be between " + min + " and " + max + ".");
                    continue;
                }
                return value;
            }
        }

        public static decimal AskPositiveDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();
                decimal value;

                if (!decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                {
                    Console.WriteLine("Please enter a valid number (for example 2 or 2.5).");
                    continue;
                }
                if (value <= 0)
                {
                    Console.WriteLine("The number must be greater than zero.");
                    continue;
                }
                return value;
            }
        }

        public static decimal AskDecimalInRange(string prompt, decimal min, decimal max)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();
                decimal value;

                if (!decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                {
                    Console.WriteLine("Please enter a valid number (for example 10 or 12.5).");
                    continue;
                }
                if (value < min || value > max)
                {
                    Console.WriteLine("The number must be between " + min + " and " + max + ".");
                    continue;
                }
                return value;
            }
        }

        public static DateTime AskDate(string prompt, bool allowTodayOnEnter)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();

                if (allowTodayOnEnter && string.IsNullOrWhiteSpace(text))
                    return DateTime.Today;

                DateTime value;
                if (DateTime.TryParseExact(text == null ? "" : text.Trim(), "yyyy-MM-dd",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out value))
                    return value;

                Console.WriteLine("Please enter a valid date as yyyy-MM-dd (for example 2026-10-25).");
            }
        }

        public static string AskChoice(string prompt, string[] options)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(text))
                {
                    foreach (string option in options)
                    {
                        if (string.Equals(option, text.Trim(), StringComparison.OrdinalIgnoreCase))
                            return option;
                    }
                }

                Console.WriteLine("Please choose one of: " + string.Join(", ", options) + ".");
            }
        }
    }
}