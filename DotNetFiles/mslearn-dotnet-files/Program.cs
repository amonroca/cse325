using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);
var salesFiles = FindFiles(storesDirectory);
var salesTotal = CalculateSalesTotal(salesFiles);
var totalsFile = Path.Combine(salesTotalDir, "totals.txt");
File.WriteAllText(totalsFile, $"{salesTotal.ToString(CultureInfo.InvariantCulture)}{Environment.NewLine}");
GenerateSalesReport(Path.Combine(salesTotalDir, "sales-report.txt"), totalsFile, storesDirectory, salesFiles);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        // The file name will contain the full path, so only check the end of it
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

void GenerateSalesReport(string reportFile, string totalsFile, string storesDirectory, IEnumerable<string> salesFiles)
{
    var report = new StringBuilder();
    var actualSalesTotal = double.Parse(File.ReadAllText(totalsFile).Trim(), CultureInfo.InvariantCulture);

    report.AppendLine("Sales Summary");
    report.AppendLine("------------------------");
    report.AppendLine();
    report.AppendLine($"  Total Sales: ${actualSalesTotal.ToString("N2", CultureInfo.InvariantCulture)}");
    report.AppendLine();
    report.AppendLine("Details:");

    var fileDetails = new List<(string FilePath, double Total)>();
    var maxTotalWidth = 0;

    foreach (var file in salesFiles)
    {
        var salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        var fileSalesTotal = data?.Total ?? data?.OverallTotal ?? 0;
        var relativeFilePath = Path.GetRelativePath(storesDirectory, file);
        var formattedTotal = $"${fileSalesTotal.ToString("N2", CultureInfo.InvariantCulture)}";
        maxTotalWidth = 35 - (relativeFilePath.Length + formattedTotal.Length);
        // The next two lines aren't very professional, but work well for a school project
        var spaces = new string(' ', maxTotalWidth);
        fileDetails.Add((relativeFilePath + ":" + spaces, fileSalesTotal));
    }

    foreach (var detail in fileDetails)
    {
        var formattedTotal = $"${detail.Total.ToString("N2", CultureInfo.InvariantCulture)}";
        report.AppendLine($"  {detail.FilePath} {formattedTotal}");
    }

    File.WriteAllText(reportFile, report.ToString());
}

record SalesData(double? Total, double? OverallTotal);
