using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Encodings.Web;
using NaKolachServer.Presentation.Models;

namespace NaKolachServer.Presentation.Services;

public class FileService
{
	public FileService()
	{

	}

	public async Task SaveToFileAsync(RegistrationModel registrationData)
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true,

			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		};

		string? jsonString = JsonSerializer.Serialize(registrationData, options);
		await File.WriteAllTextAsync("RegistrationData.json", jsonString);
	}

	public async Task<RegistrationModel?> ReadFromFileAsync()
	{
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		string jsonString = await File.ReadAllTextAsync("RegistrationData.json");

		var storedUser = JsonSerializer.Deserialize<RegistrationModel>(jsonString, options);

		return storedUser;
	}
}