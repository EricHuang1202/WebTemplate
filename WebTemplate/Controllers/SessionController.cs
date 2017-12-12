using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebTemplate.Models.ViewModels;
using WebTemplate.Util;

namespace WebTemplate.Controllers
{
    public class SessionController : ApiController
    {
		[HttpPost]
		public IHttpActionResult SetSession(SessionViewModel model)
		{
			var session = HttpContext.Current.Session;
			if (session != null)
			{
				if (session[model.Param] == null)
					session[model.Param] = model.Value;
				return Ok();
			}
			else
			{
				return BadRequest("Set Session失敗:");
			}
		}

		[HttpPost]
		public SessionViewModel GetSession(SessionViewModel model)
		{
			var session = HttpContext.Current.Session;
			if (session != null)
			{
				if (session[model.Param] != null)
					model.Value = session[model.Param].ToString();
			}

			return model;
		}

		[HttpPost]
		public SessionViewModel DelSession(SessionViewModel model)
		{
			var session = HttpContext.Current.Session;
			if (session != null)
			{
				if (session[model.Param] != null)
					session.Remove(model.Param);
			}

			return model;
		}

		[HttpPost]
		public IHttpActionResult TokenEncrypt(SessionViewModel model)
		{
			try
			{
				model.Value = UtilFunction.securityEncrypt(model.Param);
				return Ok(model);
			}
			catch (Exception e)
			{
				return BadRequest("Token加密失敗:" + e.Message);
			}
		}
	}
}
