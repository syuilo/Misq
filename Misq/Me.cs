using System.Collections.Generic;
using System.Threading.Tasks;

namespace Misq
{
	public class Me : Entities.User
	{
		/// <summary>
		/// このユーザーのユーザーキー
		/// </summary>
		public string Userkey
		{
			get;
		}

		public Me(string userkey) : base(null)
		{
			this.Userkey = userkey;
		}

		public Me(string userkey, dynamic user) : base((object)user)
		{
			this.Userkey = userkey;
		}

		/// <summary>
		/// このユーザーからAPIにリクエストします。
		/// </summary>
		/// <param name="endpoint">エンドポイント名</param>
		/// <returns>レスポンス</returns>
		public async Task<dynamic> Request(string endpoint)
		{
			return await Core.Request(endpoint, new Dictionary<string, string> {
				{ "_userkey", this.Userkey }
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
			ps.Add("_userkey", this.Userkey);
			return await Core.Request(endpoint, ps);
		}

	}
}
