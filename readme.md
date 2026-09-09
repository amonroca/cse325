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
  "id": 4,
  "name": "Cheese",
  "isGlutenFree": false
}
```

The API assigns the next available identifier when this record is posted. The
controller returns `201 Created` and a link to the corresponding GET action.

### 1.1 Proof of Test

API response for the POST verb
![API Response for POST](ContosoPizza/images/Captura%20de%20tela%202026-09-09%20174341.png)

API response for the PUT verb
![API Response for PUT](ContosoPizza/images/create-new-item.png)

API response for the GET verb
![API Response for GET](ContosoPizza/images/Captura%20de%20tela%202026-09-09%20174224.png)

API response for the DELETE verb
![API Response for DELETE](ContosoPizza/images/Captura%20de%20tela%202026-09-07%20162843.png)

Additional GET response for the created pizza
![API Response for created pizza](ContosoPizza/images/Captura%20de%20tela%202026-09-07%20163026.png)

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

### 2.1 Proof of Test

Sales summary report
![Sales summary report](DotNetFiles/mslearn-dotnet-files/images/Captura%20de%20tela%202026-09-09%20175702.png)
