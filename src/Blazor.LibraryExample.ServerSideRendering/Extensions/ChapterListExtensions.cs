using EpubReader.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.ServerSideRendering.Extensions
{
	public static class ChapterListExtensions
	{

		public static List<EpubChapterRef> Flatten(this List<EpubChapterRef> chapters)
		{
			List<EpubChapterRef> allChapters = new List<EpubChapterRef>();


			static List<EpubChapterRef> GetChaptersAndSubChapters(EpubChapterRef chapter)
			{
				var flattenChapters = new List<EpubChapterRef>
				{
					chapter
				};
				if (chapter.SubChapters != null && chapter.SubChapters.Any())
				{
					foreach (var subChapter in chapter.SubChapters)
					{
						flattenChapters.AddRange(GetChaptersAndSubChapters(subChapter));
					}
				}
				return flattenChapters;
			}

			foreach (var chapter in chapters)
			{
				allChapters.AddRange(GetChaptersAndSubChapters(chapter));
			}
			return allChapters;
		}

	}
}
