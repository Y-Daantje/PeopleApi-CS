
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Marvel endpoint
app.MapGet("/marvel", () =>
{
    var characters = LoadMarvelCharacters();
    return Results.Ok(characters);
})
.WithName("GetMarvelCharacters");

List<MarvelCharacter> LoadMarvelCharacters()
{
    const string filePath = "marvel.json"; 

    if (!File.Exists(filePath))
    {
        return new List<MarvelCharacter>();
    }

    var json = File.ReadAllText(filePath);
    if (string.IsNullOrWhiteSpace(json))
    {
        return new List<MarvelCharacter>();
    }

    var result = JsonSerializer.Deserialize<List<MarvelCharacter>>(json);
    return new List<MarvelCharacter>();
}

app.Run();

// Models
public class MarvelCharacter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Description { get; set; }
}
