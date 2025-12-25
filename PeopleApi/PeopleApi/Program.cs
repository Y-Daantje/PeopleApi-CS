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

        //simple list Marvel characters
        List<MarvelCharacter> MarvelCharacters = new List<MarvelCharacter>();
        {
            //add marvel character using the .add method "not from JSON file "(with the MavelCharacter type defined below)
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

            //print all marvel characters from the list
            foreach (var character in MarvelCharacters)
            {
                Console.WriteLine($"Character: {character.Name}, Role: {character.Role}");
            }
            //change role based on its id and verify the change
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

        app.MapDelete("/deletemarvelcharacter", (int id) =>
        {

            var character = MarvelCharacters.Find(m => m.Id == id);
            if (character == null)
            {
                return Results.NotFound($"Marvel character with ID {id} not found.");
            }
            MarvelCharacters.Remove(character);
            Console.WriteLine($"Character name: {character.Name}, with ID: {id} has been deleted.");
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

public class Rekenmachine
{
    // Variabele om het resultaat op te slaan
    private double resultaat;

    // Optellen
    public void optellen(double a, double b)
    {
        resultaat = a + b;
        System.out.println("Resultaat: " + resultaat);
    }

    // Aftrekken
    public void aftrekken(double a, double b)
    {
        resultaat = a - b;
        System.out.println("Resultaat: " + resultaat);
    }

    // Vermenigvuldigen
    public void vermenigvuldigen(double a, double b)
    {
        resultaat = a * b;
        System.out.println("Resultaat: " + resultaat);
    }

    // Getter voor het resultaat
    public double getResultaat()
    {
        return resultaat;
    }

    // Testen van de rekenmachine
    public static void main(String[] args)
    {
        Rekenmachine r = new Rekenmachine();

        r.optellen(5, 3);        // Resultaat: 8.0
        r.aftrekken(10, 4);      // Resultaat: 6.0
        r.vermenigvuldigen(6, 7); // Resultaat: 42.0

        // Toegang tot het opgeslagen resultaat
        System.out.println("Laatste resultaat opgeslagen in de class: " + r.getResultaat());
    }
}

public class Rekenmachine
{
    // Huidig resultaat
    private double resultaat;

    // Constructor: start met 0
    public Rekenmachine()
    {
        resultaat = 0;
    }

    // Optellen bij huidig resultaat
    public void optellen(double getal)
    {
        resultaat += getal;
        System.out.println("Resultaat: " + resultaat);
    }

    // Aftrekken van huidig resultaat
    public void aftrekken(double getal)
    {
        resultaat -= getal;
        System.out.println("Resultaat: " + resultaat);
    }

    // Vermenigvuldigen met huidig resultaat
    public void vermenigvuldigen(double getal)
    {
        resultaat *= getal;
        System.out.println("Resultaat: " + resultaat);
    }

    // Delen door huidig resultaat
    public void delen(double getal)
    {
        if (getal != 0)
        {
            resultaat /= getal;
            System.out.println("Resultaat: " + resultaat);
        }
        else
        {
            System.out.println("Fout: deling door nul!");
        }
    }

    // Haal huidig resultaat op
    public double getResultaat()
    {
        return resultaat;
    }

    // Testen
    public static void main(String[] args)
    {
        Rekenmachine r = new Rekenmachine();

        r.optellen(5);        // 0 + 5 = 5
        r.aftrekken(2);       // 5 - 2 = 3
        r.vermenigvuldigen(4); // 3 * 4 = 12
        r.delen(3);           // 12 / 3 = 4

        System.out.println("Laatste resultaat opgeslagen: " + r.getResultaat()); // 4
    }
}