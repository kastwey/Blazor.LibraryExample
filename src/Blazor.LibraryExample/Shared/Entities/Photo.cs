using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.LibraryExample.Shared.Entities
{
	public class Photo
	{

		public string FileName { get; set; }

		public string Label { get; set; }

		public DateTime? TakenDate { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public double Altitude { get; set; }

		public string LocationDesc { get; set; }


	}
}