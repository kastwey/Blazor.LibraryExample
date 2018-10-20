using EpubReader.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.Client.Extensions
{
	public static class ChapterListExtensions
	{

		public static List<EpubChapterRef> Flatten(this List<EpubChapterRef> chapters)
		{
			List<EpubChapterRef> allChapters = new List<EpubChapterRef>();


			List<EpubChapterRef> GetChaptersAndSubChapters(EpubChapterRef chapter)
			{
				List<EpubChapterRef> flattenedChapters = new List<EpubChapterRef>();
				flattenedChapters.Add(chapter);
				if (chapter.SubChapters != null && chapter.SubChapters.Any())
				{
					foreach (var subChapter in chapter.SubChapters)
					{
						flattenedChapters.AddRange(GetChaptersAndSubChapters(subChapter));
					}
				}
				return flattenedChapters;
			}

			foreach (var chapter in chapters)
			{
				allChapters.AddRange(GetChaptersAndSubChapters(chapter));
			}
			return allChapters;
		}

	}
}
