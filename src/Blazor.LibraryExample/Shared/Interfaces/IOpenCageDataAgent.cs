namespace Blazor.LibraryExample.Shared.INterfaces
{
	public interface IOpenCageDataAgent
	{
		object GetFormatedLocation(double latitude, double longitude);
	}
}