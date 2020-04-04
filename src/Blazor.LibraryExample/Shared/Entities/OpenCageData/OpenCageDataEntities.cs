using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.LibraryExample.Shared.Entities.OpenCageData
{
	public class License
	{
		public string name { get; set; }
		public string url { get; set; }
	}

	public class Rate
	{
		public int limit { get; set; }
		public int remaining { get; set; }
		public int reset { get; set; }
	}

	public class Northeast
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Southwest
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Bounds
	{
		public Northeast northeast { get; set; }
		public Southwest southwest { get; set; }
	}

	public class Components
	{

	}

	public class Geometry
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Result
	{
		public Bounds bounds { get; set; }
		public Components components { get; set; }
		public int confidence { get; set; }
		public string formatted { get; set; }
		public Geometry geometry { get; set; }
	}

	public class Status
	{
		public int code { get; set; }
		public string message { get; set; }
	}

	public class StayInformed
	{
		public string blog { get; set; }
		public string twitter { get; set; }
	}

	public class Timestamp
	{
		public string created_http { get; set; }
		public int created_unix { get; set; }
	}

	public class GeocodeResponse
	{
		public string documentation { get; set; }
		public List<License> licenses { get; set; }
		public Rate rate { get; set; }
		public List<Result> results { get; set; }
		public Status status { get; set; }
		public StayInformed stay_informed { get; set; }
		public string thanks { get; set; }
		public Timestamp timestamp { get; set; }
		public int total_results { get; set; }
	}
}