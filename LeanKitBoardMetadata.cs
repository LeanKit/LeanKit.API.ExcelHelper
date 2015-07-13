using System.Runtime.InteropServices;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class LeanKitBoardMetadata
	{
		public LeanKitBoardMetadata()
		{
			PopulatePriorities();
		}

		public long BoardId { get; set; }
		public LeanKitIdentifier[] CardTypes { get; set; }
		public LeanKitIdentifier[] BoardUsers { get; set; }
		public LeanKitLaneIdentifier[] Lanes { get; set; }
		public LeanKitIdentifier[] ClassesOfService { get; set; }
		public LeanKitIdentifier[] Priorities { get; set; }
		// public List<TaskboardCategoryIdentifier> TaskboardCategories { get; set; }

		private void PopulatePriorities()
		{
			Priorities = new[]
			{
				new LeanKitIdentifier {Id = 0, Name = "Low"},
				new LeanKitIdentifier {Id = 1, Name = "Normal"},
				new LeanKitIdentifier {Id = 2, Name = "High"},
				new LeanKitIdentifier {Id = 3, Name = "Critical"}
			};
		}
	}
}
