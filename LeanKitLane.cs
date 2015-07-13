using System.Runtime.InteropServices;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class LeanKitLane
	{
		public long Id { get; set; }
		public int Index { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public short Width { get; set; }
		public long ParentLaneId { get; set; }
		public LeanKitCard[] Cards { get; set; }
		public long[] ChildLaneIds { get; set; }
		public long[] SiblingLaneIds { get; set; }
	}
}
