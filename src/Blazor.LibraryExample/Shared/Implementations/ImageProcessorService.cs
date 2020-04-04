using Blazor.LibraryExample.Shared.Entities;
using Blazor.LibraryExample.Shared.Interfaces;
using MetadataExtractor;
using MetadataExtractor.Formats.QuickTime;
using MetadataExtractor.Formats.Xmp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.Shared.Implementation
{
	public class ImageProcessorService : IImageProcessorService
	{

		public async Task<Photo> GetImageMetadataAsync(byte[] imageInBytes, string fileName)
		{
			List<string> labels = new List<string>();
			IReadOnlyList<MetadataExtractor.Directory> data = null;
			try
			{
				data = await Task.Run(() => ImageMetadataReader.ReadMetadata(new MemoryStream(imageInBytes)));
			}
			catch
			{
				return null;
			}
			if (data == null)
			{
				return null;
			}

			var photo = new Photo { FileName = fileName};
			data.Where(d => d.Name == "XMP").ToList().ForEach(xmpData =>
			{
				AddLabelFromXmpDescription(labels, xmpData);
				AddDates(xmpData, photo);
			});

			var exifDir = data.SingleOrDefault(i => i.Name == "Exif IFD0");
			if (exifDir != null)
			{
				AddMetadataLabel(labels, exifDir, "Image Description");
				AddMetadataLabel(labels, exifDir, "Windows XP Subject");
				AddMetadataLabel(labels, exifDir, "Windows XP Title");
				AddMetadataLabel(labels, exifDir, "Windows XP Comment");
				if (!photo.TakenDate.HasValue)
				{
					AddExifDates(exifDir, photo);
				}
			}

			var quickTimeMeta = data.SingleOrDefault(d => d.Name == "QuickTimeMeta");
			if (quickTimeMeta != null)
			{
				var descriptionTag = quickTimeMeta.Tags.SingleOrDefault(t => t.Name == "mdta.com.apple.quicktime.description");
				if (descriptionTag != null)
				{
					var value = quickTimeMeta.GetObject(descriptionTag.Type) as QuickTimeMetadataValue;
					labels.Add(value.Value);
				}
				if (!photo.TakenDate.HasValue)
				{
					AddQuickTimeMetaCreationDate(quickTimeMeta, photo);
				}
			}

			var gpsMeta = data.SingleOrDefault(d => d.Name == "GPS");
			if (gpsMeta != null)
			{
				AddGpsMeta(photo, gpsMeta);
			}
			labels = labels.Distinct().ToList();
			if (labels.Any())
			{
				photo.Label = String.Join(Environment.NewLine, labels);
			}
			return photo;
		}

		private void AddGpsMeta(Photo photo, MetadataExtractor.Directory gpsMeta)
		{
			var latitudeTag = gpsMeta.Tags.SingleOrDefault(t => t.Name == "GPS Latitude");
			var longitudeTag = gpsMeta.Tags.SingleOrDefault(t => t.Name == "GPS Longitude");
			var altitudeTag = gpsMeta.Tags.SingleOrDefault(t => t.Name == "GPS Altitude");
			if (latitudeTag != null)
			{
				photo.Latitude = GetCoordinateFromDegrees(latitudeTag);
			}

			if (longitudeTag != null)
			{
				photo.Longitude = GetCoordinateFromDegrees(longitudeTag);
			}

			if (altitudeTag != null)
			{
				photo.Altitude = altitudeTag.Description;
			}
		}

		private static void AddQuickTimeMetaCreationDate(MetadataExtractor.Directory quickTimeMeta, Photo photo)
		{
			var tag = quickTimeMeta.Tags.SingleOrDefault(t => t.Name == "mdta.com.apple.quicktime.creationdate");
			if (tag != null)
			{
				var tagValue = quickTimeMeta.GetObject(tag.Type) as QuickTimeMetadataValue;
				if (DateTime.TryParse(tagValue.Value, out DateTime creationDate))
				{
					photo.TakenDate = creationDate;
				}
			}
		}

		private void AddExifDates(MetadataExtractor.Directory directory, Photo photo)
		{
			var tag = directory.Tags.SingleOrDefault(t => t.Name == "Date/Time");
			if (!string.IsNullOrWhiteSpace(tag?.Description))
			{
				if (DateTime.TryParse(tag.Description, out DateTime creationDate))
				{
					photo.TakenDate = creationDate;
				}
			}
		}

		private void AddDates(MetadataExtractor.Directory xmpData, Photo photo)
		{
			var xmpDirectory = xmpData as XmpDirectory;
			var dateCreated = xmpDirectory.XmpMeta.Properties.SingleOrDefault(p => p.Path == "xmp:CreateDate");
			var photoshopDateCreated = xmpDirectory.XmpMeta.Properties.SingleOrDefault(p => p.Path == "photoshop:DateCreated");
			var dateModified = xmpDirectory.XmpMeta.Properties.SingleOrDefault(p => p.Path == "xmp:ModifyDate");
			if (!String.IsNullOrWhiteSpace(dateCreated?.Value))
			{
				if (DateTime.TryParse(dateCreated.Value, out DateTime takenDate))
				{
					photo.TakenDate = takenDate;
				}
			}
			if (!photo.TakenDate.HasValue && !String.IsNullOrWhiteSpace(photoshopDateCreated?.Value))
			{
				if (DateTime.TryParse(photoshopDateCreated.Value, out DateTime photoshopTakenDate))
				{
					photo.TakenDate = photoshopTakenDate;
				}
			}
			if (!String.IsNullOrWhiteSpace(dateModified?.Value))
			{
				if (DateTime.TryParse(dateCreated.Value, out DateTime modifiedDate))
				{
					photo.ModifiedDate = modifiedDate;
				}
			}
		}

		private void AddLabelFromXmpDescription(List<string> labels, MetadataExtractor.Directory xmpData)
		{
			var xmpDirectory = xmpData as XmpDirectory;
			var artworkDesc = xmpDirectory.XmpMeta.Properties.SingleOrDefault(p => p.Path == "Iptc4xmpExt:ArtworkContentDescription");
			if (artworkDesc != null)
			{
				labels.Add(artworkDesc.Value);
			}
		}

		private void AddMetadataLabel(List<string> labels, MetadataExtractor.Directory exifDir, string tagName)
		{
			var tag = exifDir.Tags.SingleOrDefault(t => t.Name == tagName);
			if (!string.IsNullOrWhiteSpace(tag?.Description))
			{
				labels.Add(tag.Description);
			}
		}

		private double GetCoordinateFromDegrees(Tag tag)
		{
			var regexDegrees = new Regex(@"^(?<hours>\-?\d+)°\s(?<minutes>[\d,]+?)'\s(?<seconds>[\d\,]+)", RegexOptions.Compiled);
			var cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
			var fmt = new NumberFormatInfo();
			fmt.NegativeSign = "-";
			fmt.NumberDecimalSeparator = ",";
			var tagString = tag.Description;
			var result = regexDegrees.Match(tagString);
			var hours = double.Parse(result.Groups["hours"].Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign, fmt);
			var minutes = double.Parse(result.Groups["minutes"].Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign, fmt);
			var seconds = double.Parse(result.Groups["seconds"].Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign, fmt);
			return hours + (minutes / 60) + (seconds / 3600);
		}


	}
}