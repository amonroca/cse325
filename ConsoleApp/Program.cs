var date = DateTime.Now;
var daysUntilChristmas = (new DateTime(date.Year, 12, 25) - date).Days;

Console.WriteLine("Hello, World!");
Console.WriteLine($"Current date and time is {date.ToString("MM/dd/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture)}");
Console.WriteLine($"There are {daysUntilChristmas} days until the next Christmas.");
