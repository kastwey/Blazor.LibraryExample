﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.Client.Events
{
	public class BooksTableHeaderClickedEventArgs : EventArgs
	{

		public BookOrder BookOrder { get; set; }

		public string LinkId { get; set; }

	}
}
