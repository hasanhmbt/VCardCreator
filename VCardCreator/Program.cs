using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;
using VCardCreator.Models;
using static System.Net.WebRequestMethods;

namespace VCardCreator;


class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter the number of VCards you want to create:");

        if (!int.TryParse(Console.ReadLine(), out int vCardCount))
        {
            Console.WriteLine("Enter a valid number.");
            return;
        }
        try
        {
            var vCards = await VCardCreate.GenerateRandomVCards(vCardCount);
            foreach (var vCard in vCards)
            {
                VCardCreate.SaveCardToFile(vCard);
                Console.WriteLine($"vCard with ID: {vCard.Id} created.");
            }
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"Error saving VCard to file: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex}");
        }

        Console.WriteLine("All vCards have been created.");
    }



}




public class VCardCreate
{


    private static readonly HttpClient client = new HttpClient();


    public static async Task<List<VCard>> GenerateRandomVCards(int number)
    {
        string apiUrl = $"https://randomuser.me/api/?results={number}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var rootObject = JsonSerializer.Deserialize<Rootobject>(responseBody);

            if (rootObject?.results == null || rootObject.results.Length == 0)
            {
                throw new Exception("Invalid API response");
            }

            var vCards = new List<VCard>();
            foreach (var result in rootObject.results)
            {
                var vCard = new VCard
                {
                    Id = result.login.uuid,
                    Firstname = result.name.first,
                    Surname = result.name.last,
                    Email = result.email,
                    Phone = result.phone,
                    Country = result.location.country,
                    City = result.location.city
                };
                vCards.Add(vCard);
            }

            return vCards;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to fetch data from the API. Please check your network connection or try again later. Error details: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred while generating the vCards. Error details: {ex.Message}");
        }
    }



    public static void SaveCardToFile(VCard vCard)
    {
        try
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "vCards");
            Directory.CreateDirectory(filePath);

            string fileName = Path.Combine(filePath, $"{vCard.Firstname}-{vCard.Surname}-{Guid.NewGuid()}.vcf");
            string vCardContent = vCard.ToVCardFormat();

            System.IO.File.WriteAllText(fileName, vCardContent);
            Console.WriteLine($"VCard saved to: {fileName}");
        }
        catch (IOException ioEx)
        {
            throw new Exception($"Failed to save the VCard to file. Error details: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred while saving the VCard. Error details: {ex.Message}");
        }
    }



}
