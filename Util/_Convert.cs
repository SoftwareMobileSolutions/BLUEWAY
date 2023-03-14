using System.Security.Cryptography;
using System.Text;

namespace BLUEWAY.Util
{
	public class _Convert
	{
		public string SHA1HashStringForUTF8String(string s)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);

			var sha1 = SHA1.Create();
			byte[] hashBytes = sha1.ComputeHash(bytes);

			return HexStringFromBytes(hashBytes);
		}
		public string HexStringFromBytes(byte[] bytes)
		{
			var sb = new StringBuilder();
			foreach (byte b in bytes)
			{
				var hex = b.ToString("x2");
				sb.Append(hex);
			}
			return sb.ToString();
		}

		public string Base64Encode(string plainText)
		{
			if (plainText != null)
			{
				var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
				return System.Convert.ToBase64String(plainTextBytes);
			}
			else
			{
				return plainText;
			}
		}
		public static string Base64Decode(string base64EncodedData)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
	}
}
