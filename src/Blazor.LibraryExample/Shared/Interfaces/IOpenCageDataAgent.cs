using System.Threading.Tasks;

namespace Blazor.LibraryExample.Shared.Interfaces
{
	public interface IOpenCageDataAgent
	{
		
		Task<string> GetFormatedLocationAsync(double latitude, double longitude);
	}
}