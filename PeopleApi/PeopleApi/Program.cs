
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

app.MapPost("/newcharacter", (NewMarverlCharacter input) =>
{
    // Load existing characters
    var characters = LoadMarvelCharacters();

    // Create a new character with sample data
    var newCharacter = new MarvelCharacter
    {
        //input from the request body
        Id = Guid.NewGuid(),
        Name = input.Name,
        Role = input.Role,
        Description = input.Description
    };
    // Add the new character to the list
    characters.Add(newCharacter);
    SaveMarvelCharacters(characters);
    // Return the created character with a 201 status code
    // (.Created method makes the 201 status code)
    return Results.Created($"/marvelcharacter/{newCharacter.Id}", newCharacter);
});

app.MapDelete("/deletecharacter/{name}", (string name) =>
{
    // Load existing characters
    var characters = LoadMarvelCharacters();

    // Find the character to delete
    // FirstOrDefault returns the first matching element or null if none found
    // StringComparison.OrdinalIgnoreCase makes the comparison case-insensitive (Lambda expression)
    var characterToDelete = characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    if (characterToDelete == null)
    {
        return Results.NotFound("Character not found.");
    }

    // Remove the character from the list
    characters.Remove(characterToDelete);
    SaveMarvelCharacters(characters);
    // Return a 204 No Content response no body
    return Results.NoContent();
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

    var format = new JsonSerializerOptions { WriteIndented = true };

    var json = JsonSerializer.Serialize(characters, format);
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