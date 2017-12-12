using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTemplate.Util
{
	public class UtilFunction
	{
		#region 加解密功能副程式

		/// <summary>
		/// 將傳入的字串做加密。
		/// </summary>
		/// <param name="Src">欲加密字串</param>
		/// <returns>加密後的字串</returns>
		public static string securityEncrypt(string Src)
		{
			string Key, Dest, TDest;
			int KeyLen, KeyPos, SrcPos, SrcAsc, Offset;
			string returnMsg = "";
			try
			{
				if (Src == "")
					return "";

				Key = "ITCHR";
				KeyLen = Key.Length;
				KeyPos = 0;
				SrcPos = 0;
				SrcAsc = 0;

				Random random = new Random();
				Offset = random.Next(0, 256);
				Dest = Convert.ToString(Offset, 16);
				if (Dest.Length == 1)
					Dest = "0" + Dest;

				for (SrcPos = 0; SrcPos <= Src.Length - 1; SrcPos++)
				{
					SrcAsc = (Asc(Src[SrcPos]) + Offset) % 255;
					if ((KeyPos < KeyLen))
					{
						KeyPos = KeyPos + 1;
					}
					else
					{
						KeyPos = 1;
					}
					SrcAsc = (SrcAsc ^ Asc(Key[KeyPos - 1]));
					TDest = Convert.ToString(SrcAsc, 16);
					if (TDest.Length == 1)
						TDest = "0" + TDest;
					Dest = Dest + TDest;
					Offset = SrcAsc;
				}

				returnMsg = Dest;
			}
			catch (Exception Ex)
			{
				returnMsg = "【編碼加密錯誤】" + Ex.Message;
			}

			return returnMsg;
		}

		/// <summary>
		/// 解密字串。
		/// </summary>
		/// <param name="Src">欲解密字串</param>
		/// <returns>解密後的字串</returns>
		public static string securityDecrypt(string Src)
		{
			string Key, Dest, Str1;
			int KeyLen, KeyPos, SrcPos, SrcAsc, Offset, TmpSrcAsc;
			string returnMsg = "";

			try
			{
				if (Src == "")
					return "";

				Key = "ITCHR";
				KeyLen = Key.Length;
				KeyPos = 0;
				SrcPos = 0;
				SrcAsc = 0;
				Dest = "";

				Str1 = Src.Substring(0, 2);

				Offset = Convert.ToInt32(Str1, 16);
				SrcPos = 3;

				do
				{
					SrcAsc = Convert.ToInt32(Src.Substring(SrcPos - 1, 2), 16);
					if (KeyPos < KeyLen)
						KeyPos = KeyPos + 1;
					else
						KeyPos = 1;

					TmpSrcAsc = SrcAsc ^ Asc(Key[KeyPos - 1]);

					if (TmpSrcAsc <= Offset)
						TmpSrcAsc = 255 + TmpSrcAsc - Offset;
					else
						TmpSrcAsc = TmpSrcAsc - Offset;

					Dest = Dest + Chr(TmpSrcAsc);
					Offset = SrcAsc;
					SrcPos = SrcPos + 2;

				} while (SrcPos < Src.Length);

				returnMsg = Dest;
			}
			catch (Exception Ex)
			{
				returnMsg = "【編碼解密錯誤】" + Ex.Message;
			}

			return returnMsg;
		}

		private static int Asc(char c)
		{
			int converted = c;
			if (converted >= 0x80)
			{
				byte[] buffer = new byte[2];
				// if the resulting conversion is 1 byte in length, just use the value
				if (System.Text.Encoding.Default.GetBytes(new char[] { c }, 0, 1, buffer, 0) == 1)
				{
					converted = buffer[0];
				}
				else
				{
					// byte swap bytes 1 and 2;
					converted = buffer[0] << 16 | buffer[1];
				}
			}
			return converted;
		}

		private static string Chr(int asciiCode)
		{
			if (asciiCode >= 0 && asciiCode <= 255)
			{
				System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
				byte[] byteArray = new byte[] { (byte)asciiCode };
				string strCharacter = asciiEncoding.GetString(byteArray);
				return (strCharacter);
			}
			else
				throw new ApplicationException("ASCII Code is not valid.");
		}

		#endregion
	}
}