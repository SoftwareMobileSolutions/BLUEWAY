using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BLUEWAY.Data;
using BLUEWAY.Interfaces;
using BLUEWAY.Models;
using Dapper;

namespace BLUEWAY.Services
{
    public class LoginService: ILogin
    {
		private readonly SqlCnConfigMain _context;
		public LoginService(SqlCnConfigMain context)
		{
			_context = context;
		}
		public async Task<IEnumerable<LoginModel>> authlogin(string usuario, string password)
		{

			IEnumerable<LoginModel> data;
			var conn = _context.CreateConnection();
			using (conn)
			{
				string query = @"exec mBlueWay_AuthBCR @usuario, @password";
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}
				data = await conn.QueryAsync<LoginModel>(query, new { usuario, password }, commandType: CommandType.Text);
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
			return data;
		}
	}
}
