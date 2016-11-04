using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Misq
{
	public static class Core
	{
		public const string api = "https://api.misskey.xyz";
		public const string auth = "https://auth.misskey.xyz";

		/// <summary>
		/// APIにリクエストします。
		/// </summary>
		/// <param name="endpoint">エンドポイント名</param>
		/// <param name="ps">パラメーター</param>
		/// <returns>レスポンス</returns>
		public static async Task<dynamic> Request(string endpoint, Dictionary<string, string> ps)
		{
			var client = new HttpClient();

			if (endpoint[0] != '/') endpoint = '/' + endpoint;

			var res = await client.PostAsync(api + endpoint,
				new FormUrlEncodedContent(ps));

			var obj = JsonConvert.DeserializeObject<dynamic>(
				await res.Content.ReadAsStringAsync());

			return obj;
		}
	}
}
