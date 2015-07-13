using System.Runtime.InteropServices;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class LeanKitBoard
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public long Version { get; set; }
		public bool Active { get; set; }
		public LeanKitLane[] Lanes { get; set; }
		public LeanKitLane[] Backlog { get; set; }
		public LeanKitLane[] Archive { get; set; }
		public LeanKitCardType[] CardTypes { get; set; }
		public bool ClassOfServiceEnabled { get; set; }
		public long[] TopLevelLaneIds { get; set; }
		public long BacklogTopLevelLaneId { get; set; }
		public long ArchiveTopLevelLaneId { get; set; }
	}
}
