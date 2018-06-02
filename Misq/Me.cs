using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Linq;

namespace Misq
{
	public class Me : Entities.User
	{
		/// <summary>
		/// アクセストークン
		/// </summary>
		private string Token
		{
			get;
		}

		/// <summary>
		/// ユーザートークン
		/// </summary>
		public string UserToken
		{
			get;
		}

		/// <summary>
		/// インスタンスURL
		/// </summary>
		public string Host
		{
			get;
		}

		public Me(string Host, string userToken, string appSecret) : base(null)
		{
			this.Host = Host;
			this.UserToken = userToken;
			this.Token = this.GenerateAccessToken(userToken, appSecret);
		}

		public Me(string Host, string userToken, string appSecret, dynamic user) : base((object)user)
		{
			this.Host = Host;
			this.UserToken = userToken;
			this.Token = this.GenerateAccessToken(userToken, appSecret);
		}

		private string GenerateAccessToken(string userToken, string appSecret)
		{
			using (var hash = SHA256Managed.Create())
			{
				return String.Concat(hash
					.ComputeHash(Encoding.UTF8.GetBytes(userToken + appSecret))
					.Select(item => item.ToString("x2")));
			}
		}

		/// <summary>
		/// このユーザーからAPIにリクエストします。
		/// </summary>
		/// <param name="endpoint">エンドポイント名</param>
		/// <returns>レスポンス</returns>
		public async Task<dynamic> Request(string endpoint)
		{
			return await Core.Request(this.Host, endpoint, new Dictionary<string, string> {
				{ "i", this.Token }
			});
		}

		/// <summary>
		/// このユーザーからAPIにリクエストします。
		/// </summary>
		/// <param name="endpoint">エンドポイント名</param>
		/// <param name="ps">パラメーター</param>
		/// <returns>レスポンス</returns>
		public async Task<dynamic> Request(string endpoint, Dictionary<string, string> ps)
		{
			ps.Add("i", this.Token);
			return await Core.Request(this.Host, endpoint, ps);
		}

	}
}
