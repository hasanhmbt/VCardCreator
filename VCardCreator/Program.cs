using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
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
        else
        {
            try
            {
                for (int i = 0; i < vCardCount; i++)
                {

                    var vCard = await VCardCreate.GenerateRandomVCard();
                    VCardCreate.SaveCardToFile(vCard);
                    Console.WriteLine(vCard.Id);
                }
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error saving VCard to file: {ioEx.Message}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error occured:{ex}");
            }

        }

        Console.Write("All vCards have been created.\n\n\n");



    }
}




public class VCardCreate
{


    private static readonly HttpClient client = new HttpClient();


    public static async Task<VCard> GenerateRandomVCard()
    {

        string apiUrl = $"https://randomuser.me/api/";

        try
        {



            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();


            var randomUser = JsonDocument.Parse(responseBody).RootElement.GetProperty("results")[0];





            return new VCard
            {

                Id = randomUser.GetProperty("login").GetProperty("uuid").GetString(),
                Firstname = randomUser.GetProperty("name").GetProperty("first").GetString(),
                Surname = randomUser.GetProperty("name").GetProperty("last").GetString(),
                Email = randomUser.GetProperty("email").GetString(),
                Phone = randomUser.GetProperty("phone").GetString(),
                Country = randomUser.GetProperty("location").GetProperty("country").GetString(),
                City = randomUser.GetProperty("location").GetProperty("city").GetString(),


            };
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to fetch data from the API. Please check your network connection or try again later. Error details: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred while generating the VCard. Error details: {ex.Message}");
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
