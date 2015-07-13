using System.Runtime.InteropServices;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class LeanKitCard
	{
		public long BoardId { get; set; }
		public string BoardTitle { get; set; }
		public long Id { get; set; }
		public long LaneId { get; set; }
		public string LaneTitle { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string CreateDate { get; set; }
		public int Index { get; set; }
		public long TypeId { get; set; }
		public string TypeName { get; set; }
		public int Size { get; set; }
		public long Version { get; set; }
		public bool IsBlocked { get; set; }
		public string BlockReason { get; set; }
		public string StartDate { get; set; }
		public string DueDate { get; set; }
		public string ExternalSystemName { get; set; }
		public string ExternalSystemUrl { get; set; }
		public string ExternalCardID { get; set; }
		public string Tags { get; set; }
		public int Priority { get; set; }
		public string PriorityText { get; set; }
		public string CustomIcon { get; set; }
		public string DateArchived { get; set; }
		public string LastComment { get; set; }
		public int CommentsCount { get; set; }
		public int AttachmentsCount { get; set; }
		public string[] AssignedUsers { get; set; }
		public long ParentCardId { get; set; }
		public long ParentBoardId { get; set; }
		public string ExternalCardIdPrefix { get; set; }
		public string ActualFinishDate { get; set; }
		public string ActualStartDate { get; set; }
		public string BlockStateChangeDate { get; set; }
	}
}
