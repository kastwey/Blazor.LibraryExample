using Blazor.LibraryExample.Shared.Entities.OpenCageData;
using Blazor.LibraryExample.Shared.Interfaces;

using Newtonsoft.Json;

using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.Shared.Implementations
{
	public class OpenCageDataAgent : IOpenCageDataAgent
	{
		private readonly HttpClient _httpClient;

		public OpenCageDataAgent(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<string> GetFormatedLocationAsync(double latitude, double longitude)
		{
			var cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
			var uri = $"https://api.opencagedata.com/geocode/v1/json?key=c2f2802b34c44e2b87d508cd6e3d523a&q={latitude.ToString(cultureInfo)}%2C{longitude.ToString(cultureInfo)}&pretty=1&no_annotations=1";
			var response = await _httpClient.GetAsync(uri);
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			var apiResponse = JsonConvert.DeserializeObject<GeocodeResponse>(content);
			if (apiResponse.results == null || !apiResponse.results.Any())
			{
				return "No info.";
			}
			return apiResponse.results[0].formatted;
		}
	}
}