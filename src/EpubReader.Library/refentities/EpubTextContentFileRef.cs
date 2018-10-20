﻿using System.Threading.Tasks;

namespace EpubReader.Library
{
    public class EpubTextContentFileRef : EpubContentFileRef
    {
        public EpubTextContentFileRef(EpubBookRef epubBookRef)
            : base(epubBookRef)
        {
        }

        public string ReadContent()
        {
            return ReadContentAsText();
        }

        public Task<string> ReadContentAsync()
        {
            return ReadContentAsTextAsync();
        }
    }
}
