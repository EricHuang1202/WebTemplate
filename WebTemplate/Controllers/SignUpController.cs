using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebTemplate.Models.ViewModels;
using WebTemplate.Models.Repository;

namespace WebTemplate.Controllers
{
    public class SignUpController : ApiController
    {
		[HttpPost]
		public IHttpActionResult SignUp(SignUpViewModel model)
		{
			model = SignUpRepository.SignUp(model);
			if (model.IsSuccess == 1)
			{
				return Ok(model);
			}
			else
			{
				return BadRequest(model.Message);
			}
		}
	}
}
