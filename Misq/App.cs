using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
		/// Appインスタンスを初期化します。
		/// </summary>
		/// <param name="host">アプリケーションの属するインスタンスURL</param>
		/// <param name="secret">アプリケーションのシークレットキー</param>
		public App(string host, string secret)
		{
			this.Host = host;
			this.Secret = secret;
		}

		/// <summary>
		/// 認証セッションを開始し、フォームを既定のブラウザーで表示します。
		/// </summary>
		/// <returns>ユーザー</returns>
		public async Task<Me> Authorize()
		{
			var obj = await this.Request("auth/session/generate");

			var token = obj.token.Value;
			var url = obj.url.Value;

			// 規定のブラウザで表示
			System.Diagnostics.Process.Start(url);

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

			var accessToken = x.accessToken.Value;
			var userData = x.user;

			return new Me(this.Host, accessToken, this.Secret, userData);
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
