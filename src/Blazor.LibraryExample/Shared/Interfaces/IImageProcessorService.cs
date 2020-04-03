using Blazor.LibraryExample.Shared.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.Shared.Interfaces
{
	
	public interface IImageProcessorService
	{
		Task<Photo> GetImageMetadataAsync(byte[] imageInBytes, string fileName);
	}
}