
using System.Text.Json;
var builder = WebApplication.CreateBuilder();

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Marvel endpoint
/// <summary>
/// Gets a list of Marvel characters from a JSON file.
/// </summary> 
app.MapGet("/listmarvelcharacters", () =>
{
    var characters = LoadMarvelCharacters();
    return Results.Ok(characters);
});

app.MapPost("/newcharacter", () =>
{
    var characters = LoadMarvelCharacters();

    var nextId =  
});


List<MarvelCharacter> LoadMarvelCharacters()
{
    var filePath = "marvel.json";

    if (!File.Exists(filePath))
    {
        return new List<MarvelCharacter>();
    }

    var json = File.ReadAllText(filePath);
    Console.WriteLine(json);
    if (string.IsNullOrWhiteSpace(json))
    {
        return new List<MarvelCharacter>();
    }


    var result = JsonSerializer.Deserialize<List<MarvelCharacter>>(json);
    return result ?? new List<MarvelCharacter>();
}

void SaveMarvelCharacters(List<MarvelCharacter> characters)
{
    var filePath = "marvel.json";

    var options = new JsonSerializerOptions();

    var json = JsonSerializer.Serialize(characters, options);
    File.WriteAllText(filePath, json);
}


app.Run();

// Models
public class MarvelCharacter
{
    public Guid Id { get; set; } // Unique identifier // GUID generate e random number for every character unique and safe.
    public string Name { get; set; }
    public string Role { get; set; }
    public string Description { get; set; }
}

public class NewMarverlCharacter
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string Description { get; set; }
}