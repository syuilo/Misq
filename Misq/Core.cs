using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Misq
{
	public static class Core
	{
		/// <summary>
		/// APIにリクエストします。
		/// </summary>
		/// <param name="host">MisskeyインスタンスのURL</param>
		/// <param name="endpoint">エンドポイント名</param>
		/// <param name="ps">パラメーター</param>
		/// <returns>レスポンス</returns>
		public static async Task<dynamic> Request(string host, string endpoint, Dictionary<string, string> ps)
		{
			var client = new HttpClient();

			var ep = $"{host}/api/{endpoint}";

			var res = await client.PostAsync(ep,
				new FormUrlEncodedContent(ps));

			var obj = JsonConvert.DeserializeObject<dynamic>(
				await res.Content.ReadAsStringAsync());

			return obj;
		}
	}
}
