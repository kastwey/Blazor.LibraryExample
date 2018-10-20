using System;
using System.IO;
using System.Threading.Tasks;
using EpubReader.Library.Utils;
using ICSharpCode.SharpZipLib.Zip;

namespace EpubReader.Library
{
	public abstract class EpubContentFileRef
	{
		private readonly EpubBookRef epubBookRef;

		public EpubContentFileRef(EpubBookRef epubBookRef)
		{
			this.epubBookRef = epubBookRef;
		}

		public string FileName { get; set; }
		public EpubContentType ContentType { get; set; }
		public string ContentMimeType { get; set; }

		public byte[] ReadContentAsBytes()
		{
			return ReadContentAsBytesAsync().Result;
		}

		public async Task<byte[]> ReadContentAsBytesAsync()
		{
			var contentFileEntry = GetContentFileEntry();
			byte[] content = new byte[(int)contentFileEntry.Size];
			using (Stream contentStream = OpenContentStream(contentFileEntry))
			using (MemoryStream memoryStream = new MemoryStream(content))
				await contentStream.CopyToAsync(memoryStream).ConfigureAwait(false);
			return content;
		}

		public string ReadContentAsText()
		{
			return ReadContentAsTextAsync().Result;
		}

		public async Task<string> ReadContentAsTextAsync()
		{
			using (Stream contentStream = GetContentStream())
			using (StreamReader streamReader = new StreamReader(contentStream))
				return await streamReader.ReadToEndAsync().ConfigureAwait(false);
		}

		public Stream GetContentStream()
		{
			return OpenContentStream(GetContentFileEntry());
		}

		private ZipEntry GetContentFileEntry()
		{
			string contentFilePath = ZipPathUtils.Combine(epubBookRef.Schema.ContentDirectoryPath, FileName);
			ZipEntry contentFileEntry = epubBookRef.EpubArchive.GetEntry(contentFilePath);
			if (contentFileEntry == null)
				throw new Exception(String.Format("EPUB parsing error: file {0} not found in archive.", contentFilePath));
			if (contentFileEntry.Size > Int32.MaxValue)
				throw new Exception(String.Format("EPUB parsing error: file {0} is bigger than 2 Gb.", contentFilePath));
			return contentFileEntry;
		}

		private Stream OpenContentStream(ZipEntry contentFileEntry)
		{
			Stream contentStream = this.epubBookRef.EpubArchive.GetInputStream(contentFileEntry);
			if (contentStream == null)
				throw new Exception(String.Format("Incorrect EPUB file: content file \"{0}\" specified in manifest is not found.", FileName));
			return contentStream;
		}
	}
}
