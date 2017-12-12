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
	public class SignUpRepository
	{
		public static string strStockConnection = ConfigurationManager.ConnectionStrings["StockConnection"].ToString();
		public static SignUpViewModel SignUp(SignUpViewModel model)
		{
			using (MySqlConnection connection = new MySqlConnection(strStockConnection))
			{
				MySqlCommand Cmd = connection.CreateCommand();
				connection.Open();
				try
				{
					Cmd.CommandText = "SELECT * From Account WHERE AccId = @AccId ";
					Cmd.Parameters.Clear();
					Cmd.Parameters.AddWithValue("@AccId", model.AccId);

					MySqlDataAdapter daData = new MySqlDataAdapter(Cmd);
					DataTable dtDataResult = new DataTable();
					daData.Fill(dtDataResult);
					if (dtDataResult.Rows.Count > 0)
					{
						model.IsSuccess = 0;
						model.Message = "此帳號已存在！";
					}
					else
					{
						Cmd.CommandText = @"INSERT INTO Account(AccId, AccName, AccPwd, Email) 
                                    VALUES (@AccId, @AccName, @AccPwd, @Email)";

						Cmd.Parameters.Clear();
						Cmd.Parameters.AddWithValue("@AccId", model.AccId);
						Cmd.Parameters.AddWithValue("@AccName", model.AccName);
						Cmd.Parameters.AddWithValue("@AccPwd", UtilFunction.securityEncrypt(model.AccPwd));
						Cmd.Parameters.AddWithValue("@Email", model.Email);
						Cmd.ExecuteNonQuery();

						model.IsSuccess = 1;
						model.Message = "註冊帳號成功！";
					}
				}
				catch (Exception e)
				{
					model.IsSuccess = 0;
					model.Message = e.Message;
				}
			}
			return model;
		}
	}
}