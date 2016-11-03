namespace Misq.Entities
{
	public class User
	{
		/// <summary>
		/// ID
		/// </summary>
		public string ID
		{
			get;
		}

		/// <summary>
		/// 名前
		/// </summary>
		public string Name
		{
			get;
		}

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string Username
		{
			get;
		}

		/// <summary>
		/// データソースを与えてユーザーを初期化します。
		/// </summary>
		/// <param name="source">データソース</param>
		public User(dynamic source)
		{
			if (source != null)
			{
				this.ID = source.id;
				this.Name = source.name;
				this.Username = source.username;
			}
		}
	}
}
