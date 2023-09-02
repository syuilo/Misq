using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace Misq
{
	/// <summary>
	/// Misskeyアプリクラス
	/// </summary>
	public class App
	{
		/// <summary>
		/// アプリケーションの属するインスタンスURLを取得または設定します。
		/// </summary>
		public string Host
		{
			get;
			set;
		}
	
		/// <summary>
		/// アプリケーションのシークレット・キーを取得または設定します。
		/// </summary>
		public string Secret
		{
			get;
			set;
		}

		/// <summary>
		/// アプリケーションのセッションIDを取得または設定します。
		/// </summary>
		public string SessionID 
		{
			get;
			set;
		}


		/// <summary>
		/// シークレットキーを使ってAppインスタンスを初期化します。
		/// </summary>
		/// <param name="host">アプリケーションの属するインスタンスURL</param>
		/// <param name="secret">アプリケーションのシークレットキー</param>
		public App(string host, string secret)
		{
			this.Host = host;
			this.Secret = secret;
			this.SessionID = null;
		}

		/// <summary>
		/// MiAuthを使ってAppインスタンスを初期化します。
		/// </summary>
		/// <param name="host">アプリケーションの属するインスタンスURL</param>
		public App(string host)
		{
			this.Host = host;
			this.SessionID = Guid.NewGuid().ToString();
			this.Secret = null;
		}

		/// <summary>
		/// シークレットキーを使って認証セッションを開始し、フォームを既定のブラウザーで表示します。
		/// </summary>
		/// <returns>ユーザー</returns>
		public async Task<Me> Authorize()
		{
			var obj = await this.Request("auth/session/generate");

			var token = obj.token.Value;
			var url = obj.url.Value;

			// 規定のブラウザで表示
			var info = new ProcessStartInfo(url);
			info.UseShellExecute = true;
			System.Diagnostics.Process.Start(info);

			Func<Task<dynamic>> check = async () => {
				var a = await this.Request("auth/session/userkey", new Dictionary<string, object> {
					{ "token", token }
				});

				return a.accessToken != null ? a : null;
			};

			dynamic x = null;

			while (x == null)
			{
				x = await check();
				await Task.Delay(1000);
			}

			var accessToken = x.accessToken.Value as string;
			var userData = x.user;

			return new Me(this.Host, accessToken, this.Secret, userData);
		}
		/// <summary>
		/// MiAuthを使って認証セッションを開始し、フォームを既定のブラウザーで表示します。
		/// </summary>
		/// <param name="appName">アプリの名前</param>
		/// <param name="permissions">アプリから利用可能な権限</param>
		/// <returns>ユーザー</returns>
		public async Task<Me> Authorize(string appName, IEnumerable<string> permissions)
		{
			// URLを作成
			var permissions_str = String.Join(",", permissions);
			HttpUtility.UrlEncode($"name={appName}&permission={permissions_str}");
			var name_query = HttpUtility.UrlEncode(appName);
			var parmissons_query = HttpUtility.UrlEncode(permissions_str);
			var url = $"{Host}/miauth/{SessionID}?name={name_query}&permission={parmissons_query}";

			// 規定のブラウザで表示
			var info = new ProcessStartInfo(url);
			info.UseShellExecute = true;
			System.Diagnostics.Process.Start(info);

			// ユーザーの認証を待つ
			Func<Task<dynamic>> check = async () =>
			{
				var a = await this.Request(
					$"miauth/{SessionID}/check", new Dictionary<string, object>());
				return a.token != null ? a : null;
			};

			dynamic x = null;

			while (x == null) {
				x = await check();
				await Task.Delay(1000);
			}

			// 取得したデータをMeにして返す
			var accessToken = x.token.Value as string;
			var userData = x.user;

			return new Me(this.Host, accessToken, userData);
		}

		/// <summary>
		/// このアプリからAPIにリクエストします。
		/// </summary>
		/// <param name="endpoint">エンドポイント名</param>
		/// <returns>レスポンス</returns>
		public async Task<dynamic> Request(string endpoint)
		{
			return await Core.Request(this.Host, endpoint, new Dictionary<string, object> {
				{ "appSecret", this.Secret }
			});
		}

		/// <summary>
		/// このアプリからAPIにリクエストします。
		/// </summary>
		/// <param name="endpoint">エンドポイント名</param>
		/// <param name="ps">パラメーター</param>
		/// <returns>レスポンス</returns>
		public async Task<dynamic> Request(string endpoint, Dictionary<string, object> ps)
		{
			ps.Add("appSecret", this.Secret);
			return await Core.Request(this.Host, endpoint, ps);
		}

		/// <summary>
		/// Misskeyアプリを登録します。
		/// </summary>
		/// <param name="host">アプリの属するインスタンスURL</param>
		/// <param name="appName">アプリの名前</param>
		/// <param name="appDescription">アプリの説明文</param>
		/// <param name="permissions">アプリから利用可能な権限</param>
		/// <returns>登録されたアプリ</returns>
		public static async Task<App> Register(string host, string appName, string appDescription, IEnumerable<string> permissions)
		{
			var ps = new Dictionary<string, object>
			{
				["name"] = appName,
				["description"] = appDescription,
				["permission"] = permissions
			};
			var app = await Core.Request(host, "app/create", ps);

			return new App(host, app.secret.Value);
		}
	}
}
