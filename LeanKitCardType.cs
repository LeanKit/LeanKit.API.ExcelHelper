using System.Runtime.InteropServices;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class LeanKitCardType
	{
		public long Id { get; set; }
		public bool IsDefault { get; set; }
		public string Name { get; set; }
	}
}
