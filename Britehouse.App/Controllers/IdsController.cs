using Britehouse.App.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Britehouse.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdsController : ControllerBase
	{
		[HttpPost]
		public ActionResult Post([FromBody]List<IdModel> idModel)
		{
			try
			{
				foreach (var ids in idModel)
				{
					if (ids.IdNumber.Length == 13)
					{
						//valid id numbers
						//extract the birthdate,extract the gender and extract citizenship
						//valid save in the valid file else save in the non valid

						string birthdate = GetBirthdateFromId(ids.IdNumber);
						if (birthdate != "Error")
						{
							string gender = GetGender(ids.IdNumber);
							if (gender != "Error")
							{
								string citizenship = GetCitizenship(ids.IdNumber);
								if (citizenship != "Error")
								{
									SaveInValid(ids.IdNumber, birthdate, gender, citizenship);
								}
								else
								{
									SaveInUnValid(ids.IdNumber, "Citizenship Error.");
								}
							}
							else
							{
								SaveInUnValid(ids.IdNumber, "Gender Error.");
							}
						}
						else
						{
							SaveInUnValid(ids.IdNumber, "Birthday Error.");
						}
					}
					else
					{
						//invalid
						// save in non valid
						SaveInUnValid(ids.IdNumber, "Id number not 13 numbers");
						return Ok();
					}

				}
				return Ok();
			}
			catch (Exception exception)
			{
				return BadRequest(exception);
			}
		}
		[HttpPost("postfiles")]
		[Consumes("")]
		public ActionResult PostFiles([FromBody]FilesUploadedModel filesUploadedModel)
		{
			try
			{
				var files = Request.Form.Files;
				   var formData = HttpContext.Request.Form;
             				//Create Path of not exists
				if (!Directory.Exists(storeLocation))
					Directory.CreateDirectory(storeLocation);
				if (filesUploadedModel == null)
					return Content("file not selected");

				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filesUploadedModel.Name);

				using (var stream = new FileStream(path, FileMode.Create))
				{
					filesUploadedModel.FileToUpload.CopyTo(stream);
				}

				var column1 = new List<string>();
					var column2 = new List<string>();
					using (var rd = new StreamReader(filesUploadedModel.Name))
					{
						while (!rd.EndOfStream)
						{
							var splits = rd.ReadLine().Split(';');
							column1.Add(splits[0]);
							column2.Add(splits[1]);
						}
					} 
				//}

				return Ok();
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
		string storeLocation = @"C:\IdNumbers";

		private void SaveInValid(string idNumber, string birthdate, string gender, string citizenship)
		{
			//initialize the path to get the file.
			IdModel idsModels = new IdModel();

			if (!Directory.Exists(storeLocation))
			{
				//create directory if it does not exist
				Directory.CreateDirectory(storeLocation);
				//create headers
				idsModels .IdNumber = "IdentityNumber ";
				idsModels .Birthdate = "BirthDate";
				idsModels .Gender = "Gender";
				idsModels .Citizenship = "Citizenship";

				//create file
				WriteCSV(idsModels , storeLocation, "Valid");
			}
			idsModels .IdNumber = idNumber;
			idsModels .Birthdate = birthdate;
			idsModels .Gender = gender;
			idsModels .Citizenship = citizenship;

			//write id into file
			WriteCSV(idsModels , storeLocation, "Valid");
		}
		public void WriteCSV<HeadersModel>(HeadersModel item, string storeLocation,string fileType)
		{
			Type itemType = typeof(HeadersModel);
			var props = itemType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
			using (StreamWriter streamWriter = new StreamWriter(storeLocation + "\\"+fileType+".csv", true))
			{
				streamWriter.WriteLine(string.Join(", ", props.Select(y => y.GetValue(item, null))));
			}
		}
		private void SaveInUnValid(string idNumber, string error)
		{
			try
			{
				string storeLocation = @"C:\IdNumbers";
				IdModel idsModels = new IdModel();

				if (!Directory.Exists(storeLocation))
				{
					//create directory if it does not exist
					Directory.CreateDirectory(storeLocation);
					//create headers
					idsModels.IdNumber = "IdentityNumber ";
					idsModels.Error = "Error";

					//create file
					WriteCSV(idsModels, storeLocation, "InValid");
				}
				idsModels.IdNumber = idNumber;
				idsModels.Error = error;

				//write id into file
				WriteCSV(idsModels, storeLocation, "InValid");

			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		private string GetCitizenship(string idNumber)
		{
			try
			{
				int citizenship = Convert.ToInt32(idNumber.Substring(10, 1));
				if (citizenship == 0)
				{
					return "South African";
				}
				else if (citizenship > 0)
				{
					return "Other";
				}
				else
				{
					return "Error";
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		private string GetGender(string idNumber)
		{
			try
			{
				int gender = Convert.ToInt32(idNumber.Substring(6, 1));
				if (gender <= 4)
				{
					return "Female";
				}
				else if (gender > 4)
				{
					return "Male";
				}
				else
				{
					return "Error";
				}
			}
			catch (Exception exeption)
			{
				throw exeption;
			}
		}

		private string GetBirthdateFromId(string idNumber)
		{
			try
			{
				string idNumberSubString = idNumber.Substring(0, 6);
				DateTime date;
				if (DateTime.TryParseExact(idNumberSubString, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
				{
					return date.ToString("dd MMMM yyyy");
				}
				else
				{
					return "Error";
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
	}
}