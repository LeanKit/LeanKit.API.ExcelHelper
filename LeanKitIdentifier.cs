using System.Runtime.InteropServices;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class LeanKitIdentifier
	{
		public long Id { get; set; }
		public string Name { get; set; }
	}
}
