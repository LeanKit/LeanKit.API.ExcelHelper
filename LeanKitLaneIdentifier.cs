using System.Runtime.InteropServices;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class LeanKitLaneIdentifier : LeanKitIdentifier
	{
		public long ParentLaneId { get; set; }
		public long TopLevelParentLaneId { get; set; }
		public int CardLimit { get; set; }
		public int Index { get; set; }
	}
}
