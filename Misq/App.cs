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
		/// <returns>ユーザーが認証を終えたことを通知するハンドラ</returns>
		public async Task<Func<Task<Me>>> Authorize()
		{
			var obj = await this.Request("auth/session/generate");

			var token = obj.token.Value;
			var url = obj.url.Value;

			// 規定のブラウザで表示
			System.Diagnostics.Process.Start(url);

			return async () =>
			{
				var obj2 = await this.Request("auth/session/userkey", new Dictionary<string, object> {
					{ "token", token }
				});

				var accessToken = obj2.accessToken.Value;
				var userData = obj2.user;

				return new Me(this.Host, accessToken, this.Secret, userData);
			};
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


	}
}
