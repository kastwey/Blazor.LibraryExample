using Blazor.LibraryExample.Shared.INterfaces;
using OpenCage.Geocode;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Blazor.LibraryExample.Shared.Implementations
{
	public class OpenCageDataAgent : IOpenCageDataAgent
	{
		private readonly IGeocoder _geoCoderClient;

		public OpenCageDataAgent(IGeocoder geocoderClient)
		{
			_geoCoderClient = geocoderClient;
		}

		public object GetFormatedLocation(double latitude, double longitude)
		{
			var cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
			var response = _geoCoderClient.Geocode($"{latitude.ToString(cultureInfo)},{longitude.ToString(cultureInfo)}");
			if (response.Results.Length == 0)
			{
				return "No info";
			}
			return response.Results[0].Formatted;
		}
	}
}