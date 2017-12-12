using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTemplate.Models.ViewModels
{
	public class SignUpViewModel
	{
		public string AccId { set; get; }
		public string AccName { set; get; }
		public string AccPwd { set; get; }
		public string Email { set; get; }
		public int IsSuccess { set; get; }
		public string Message { set; get; }
	}
}