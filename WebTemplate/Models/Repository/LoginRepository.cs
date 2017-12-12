using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebTemplate.Models.ViewModels;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using WebTemplate.Util;

namespace WebTemplate.Models.Repository
{
	public class LoginRepository
	{
		public static string strStockConnection = ConfigurationManager.ConnectionStrings["StockConnection"].ToString();
		public static LoginViewModel ChkLogin(LoginViewModel model)
		{
			using (MySqlConnection connection = new MySqlConnection(strStockConnection))
			{
				MySqlCommand Cmd = connection.CreateCommand();
				connection.Open();
				try
				{
					Cmd.CommandText = "SELECT * From Account WHERE AccId = @AccId ";
					Cmd.Parameters.Clear();
					Cmd.Parameters.AddWithValue("@AccId", model.AccountId);

					MySqlDataAdapter daData = new MySqlDataAdapter(Cmd);
					DataTable dtDataResult = new DataTable();
					daData.Fill(dtDataResult);
					foreach (DataRow tRowData in dtDataResult.Rows)
					{
						if (model.Password == UtilFunction.securityDecrypt(tRowData["AccPwd"].ToString()))
						{
							model.AccName = tRowData["AccName"].ToString();
							model.IsSuccess = 1;
							model.Message = "登入成功！";
						}
						else
						{
							model.IsSuccess = 0;
							model.Message = "登入密碼錯誤！";
						}
					}

					if (dtDataResult.Rows.Count == 0)
					{
						model.IsSuccess = 0;
						model.Message = "無此帳號！";
					}

				}
				catch (Exception)
				{
					throw;
				}
			}
			return model;
		}
	}
}