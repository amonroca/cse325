# Module Evidence and Part 2 Function

## 1. Evidence from `Create a web API with ASP.NET Core controllers`

The `ContosoPizza` Web API registers controller support in `Program.cs` and maps
controller routes with `app.MapControllers()`.

The existing pizza content in `ContosoPizza/Services/PizzaService.cs` is:

```json
[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  }
]
```

The additional record for the POST request is:

```json
{
  "id": 3,
  "name": "Hawaiian",
  "isGlutenFree": false
}
```

The API assigns the next available identifier when this record is posted. The
controller returns `201 Created` and a link to the corresponding GET action.

## 2. Working sales summary function for Part 2

The following is the working `CalculateSalesTotal` function from
`DotNetFiles/mslearn-dotnet-files/Program.cs`:

```csharp
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
```
