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

            if (MarvelCharacters.Count > 0)
            {
                ChangeCharacterRole(MarvelCharacters[0]); // Change the role of the first character as a test
                CheckRole(MarvelCharacters[0]);
            }


            Console.WriteLine();
        }


        app.MapGet("/listmarvelcharacters", () =>
        {
            // get Marvel characters from the list and not from JSON file
            return Results.Ok(MarvelCharacters);
        });

        app.MapDelete("/deletemarvelcharacter/{id}", (int id) =>
        {

            var character = MarvelCharacters.Find(m => m.Id == id);
            if (character == null)
            {
                return Results.NotFound($"Marvel character with ID {id} not found.");
            }
            MarvelCharacters.Remove(character);
            return Results.Ok($"Marvel character with ID {id} has been deleted.");

        });



        void ChangeCharacterRole(MarvelCharacter character)
        {
            character.Role = "test";
            Console.WriteLine($"Character: {MarvelCharacters[0].Name}, Role: {MarvelCharacters[0].Role}"); // Output: name
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

public class UpdateMarvelCharacter
{
    public string? Name { get; set; }
    public string? Role { get; set; }
    public string? Description { get; set; }
}
// public class NewMarverlCharacter
// {
//     public string Name { get; set; }
//     public string Role { get; set; }
//     public string Description { get; set; }
// }