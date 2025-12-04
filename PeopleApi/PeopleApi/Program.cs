
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

List<MarvelCharacter> LoadMarvelCharacters()
{
    var filePath = "marvel.json";

    // Check if the file exists
    if (!File.Exists(filePath))
    {
        // If the file doesn't exist, return an empty list
        return new List<MarvelCharacter>();
    }
    // Read the JSON file content
    var json = File.ReadAllText(filePath);
    Console.WriteLine(json);

    // If the file is empty, return an empty list
    if (string.IsNullOrWhiteSpace(json))
    {
        return new List<MarvelCharacter>();
    }

    // Deserialize the JSON content to a list of MarvelCharacter
    // If deserialization fails, return an empty list
    var result = JsonSerializer.Deserialize<List<MarvelCharacter>>(json);
    return result ?? new List<MarvelCharacter>();
}


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
    // Return the created character with a 201 status code.
    // (.Created method makes the 201 status code) --> input successful and return the created resource.
    return Results.Created($"/{newCharacter.Id}", newCharacter);
});





app.MapPatch("/updatecharacter/{name}", (string name, UpdateMarvelCharacter updatedData) =>
{
    // Load existing characters.
    var characters = LoadMarvelCharacters();

    // Find the character to update.
    //characterToUpdate.Role → the old value in the JSON file.
    var characterToUpdate = characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    if (characterToUpdate == null)
    {
        return Results.NotFound("Character not found.");
    }

    // Update the character's details.
    // If the updatedData properties are null, keep the existing values.
    // If the user provides a new value, update it if not keep the old one.
    characterToUpdate.Name = updatedData.Name ?? characterToUpdate.Name;
    characterToUpdate.Role = updatedData.Role ?? characterToUpdate.Role;
    characterToUpdate.Description = updatedData.Description ?? characterToUpdate.Description;

    // Save the updated list back to the JSON file.
    SaveMarvelCharacters(characters);

    // Return the updated character.
    return Results.Ok(characterToUpdate);
});




app.MapDelete("/deletecharacter/{name}", (string name) =>
{
    // Load existing characters
    var characters = LoadMarvelCharacters();


    // StringComparison.OrdinalIgnoreCase makes the comparison case-insensitive (Lambda expression)
    // Find the character to delete
    // FirstOrDefault returns the first matching element or null if none found
    var characterToDelete = characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    if (characterToDelete == null)
    {
        return Results.NotFound("Character not found.");
    }

    //Load → Save → Delete → (but delete is never saved!) --> wrong order if you flip it.

    // Remove the character from the list
    characters.Remove(characterToDelete);

    // this line need to come after removing the character because we need to remover first then save it to the list.
    // Save the updated list back to the JSON file.
    // Load → Delete → Save → Return 204 (exmaple).
    SaveMarvelCharacters(characters);

    // Return a 204 No Content response no body
    return Results.NoContent();
});






// Save the list of Marvel characters to the JSON file
void SaveMarvelCharacters(List<MarvelCharacter> characters)
{
    var filePath = "marvel.json";
    // serialize the list to JSON format for better readability.
    var format = new JsonSerializerOptions { WriteIndented = true };
    // change C# objects to JSON string
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

public class UpdateMarvelCharacter
{
    public string? Name { get; set; }
    public string? Role { get; set; }
    public string? Description { get; set; }
}
public class NewMarverlCharacter
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string Description { get; set; }
}