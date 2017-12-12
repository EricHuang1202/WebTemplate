using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTemplate.Models.ViewModels
{
	public class LoginViewModel
	{
		public string AccountId { set; get; }
		public string Password { set; get; }
		public string AccName { set; get; }
		public int IsSuccess { set; get; }
		public string Message { set; get; }
	}
}