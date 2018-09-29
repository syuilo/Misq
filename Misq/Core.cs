using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Misq
{
	public static class Core
	{
		static Lazy<HttpClient> _client = new Lazy<HttpClient>();
		/// <summary>
		/// APIにリクエストします。
		/// </summary>
		/// <param name="host">MisskeyインスタンスのURL</param>
		/// <param name="endpoint">エンドポイント名</param>
		/// <param name="ps">パラメーター</param>
		/// <returns>レスポンス</returns>
		public static async Task<dynamic> Request(string host, string endpoint, Dictionary<string, object> ps)
		{
			var client = _client.Value;

			var ep = $"{host}/api/{endpoint}";

			var content = new StringContent(JsonConvert.SerializeObject(ps),
				Encoding.UTF8, "application/json");

			var res = await client.PostAsync(ep, content);

			var obj = JsonConvert.DeserializeObject<dynamic>(
				await res.Content.ReadAsStringAsync());

			return obj;
		}

		/// <summary>
		/// APIにリクエストします。
		/// </summary>
		/// <param name="host">MisskeyインスタンスのURL</param>
		/// <param name="endpoint">エンドポイント名</param>
		/// <param name="ps">パラメーター</param>
		/// <returns>レスポンス</returns>
		public static async Task<dynamic> RequestWithBinary(string host, string endpoint, MultipartFormDataContent ps)
		{
			var client = _client.Value;

			var ep = $"{host}/api/{endpoint}";

			var res = await client.PostAsync(ep, ps);

			var obj = JsonConvert.DeserializeObject<dynamic>(
				await res.Content.ReadAsStringAsync());

			return obj;
		}
	}
}
