using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Britehouse.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Britehouse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class IdsController: ControllerBase
	{
		[HttpPost]
		public void Post([FromBody]List<IdModel> idModel)
		{
			try
			{
				//TODO:::extract the birthdate,extract the gender and extract citizenship
				//TODO::: of valid save in the valid file else save in the non valid

			}
			catch (Exception exception)
			{
				throw;
			}
		}

	}
}
