using System;
using System.Runtime.InteropServices;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class LeanKitBoardListItem
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string IsArchived { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
