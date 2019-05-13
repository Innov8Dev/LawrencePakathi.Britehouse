using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Britehouse.App.Model
{
	public class RawFileModel
	{
		public int LastModified { get; set; }
		public DateTime LastModifiedDate { get; set; }
		public string Name { get; set; }
		public int Size { get; set; }
		public string Type { get; set; }
		public string WebkitRelativePath { get; set; }
	}
}
