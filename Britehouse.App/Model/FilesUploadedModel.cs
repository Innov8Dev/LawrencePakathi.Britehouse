using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Britehouse.App.Model
{
	public class FilesUploadedModel
	{
		public string Name { get; set; }
		public RawFileModel RawFile { get; set; }
		public int Size { get; set; }
		public string Type { get; set; }
		public IFormFile FileToUpload { get; set; }
	}
}
