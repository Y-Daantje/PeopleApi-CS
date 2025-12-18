using System.Reflection.PortableExecutable;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
internal class Program
{
    private static void Main(string[] args)
    {
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


        // List<MarvelCharacter> LoadMarvelCharacters()
        // {
        //     var filePath = "marvel.json";

        //     // Check if the file exists
        //     if (!File.Exists(filePath))
        //     {
        //         // If the file doesn't exist, return an empty list
        //         return new List<MarvelCharacter>();
        //     }
        //     // Read the JSON file content
        //     var json = File.ReadAllText(filePath);
        //     Console.WriteLine(json);


        //     // If the file is empty, return an empty list
        //     if (string.IsNullOrWhiteSpace(json))
        //     {
        //         return new List<MarvelCharacter>();
        //     }

        //     // Deserialize the JSON content to a list of MarvelCharacter
        //     // If deserialization fails, return an empty list
        //     var result = JsonSerializer.Deserialize<List<MarvelCharacter>>(json);

        //     if (result != null && result.Count > 0) // checks if result is null(if its there) or checks if it has more than one character in the list.
        //     {
        //         ChangeCharacterRole(result[1]); // Change the role of the second character as a test
        //         CheckRole(result[1]);
        //     }
        //     return result ?? new List<MarvelCharacter>();

        // }

        //list test
        List<MarvelCharacter> MarvelCharacters = new List<MarvelCharacter>();
        {
            MarvelCharacters.Add(new MarvelCharacter
            {
                Id = 1,
                Name = "Iron Man",
                Role = "Damage",
                Description = "Armed with superior intellect and a nanotech battlesuit of his own design, Tony Stark stands alongside gods as the Invincible Iron Man. His state of the art armor turns any battlefield into his personal playground, allowing him to steal the spotlight he so desperately desires."
            });
            MarvelCharacters.Add(new MarvelCharacter
            {
                Id = 2,
                Name = "Captain America",
                Role = "Tank",
                Description = "Enhanced to the peak of human physicality by an experimental serum, World War II hero Steve Rogers fights for American ideals as the Avengers' iconic leader, Captain America. Armed with his indestructible shield and unyielding spirit, he stands as a symbol of freedom and justice."
            });
            MarvelCharacters.Add(new MarvelCharacter
            {
                Id = 3,
                Name = "Thor",
                Role = "Bruiser",
                Description = "As the Norse God of Thunder, Thor wields the enchanted hammer Mjolnir to protect both Asgard and Earth. With his godly strength, weather manipulation, and indomitable spirit, Thor stands as a mighty warrior and a key member of the Avengers."
            });
            Console.WriteLine();

            foreach (var character in MarvelCharacters)
            {
                Console.WriteLine($"Character: {character.Name}, Role: {character.Role}");
            }
            
            Console.WriteLine();
        }

        ///
        // app.MapGet("/listmarvelcharacters", () =>
        // {
        //     var characters = LoadMarvelCharacters();

        //     return Results.Ok(characters);
        // });



        void ChangeCharacterRole(MarvelCharacter character)
        {
            character.Role = "test";
            Console.WriteLine(character.Name);// Output: name
            Console.WriteLine(character.Role); // Output: test
        }

        void CheckRole(MarvelCharacter character)
        {
            if (character.Role == "test")
            {
                Console.WriteLine("Role has been changed to test.");
            }
            else
            {
                Console.WriteLine("Role is unchanged.");
            }

        }

        app.Run();
    }
}

// Models
public class MarvelCharacter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Description { get; set; }
}

// public class UpdateMarvelCharacter
// {
//     public string? Name { get; set; }
//     public string? Role { get; set; }
//     public string? Description { get; set; }
// }
// public class NewMarverlCharacter
// {
//     public string Name { get; set; }
//     public string Role { get; set; }
//     public string Description { get; set; }
// }