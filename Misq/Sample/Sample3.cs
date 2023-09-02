
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Misq.Sample
{
	internal class Sample3
	{
		static async Task Main(string[] args)
		{
			// Create your app instance
			var app = new Misq.App("https://misskey.io");
			// Authorize user
			var user = await app.Authorize(
				"your app's name",
				new string[] { "write:notes" });

			// Let's post a message to Misskey
			await user.Request("notes/create",
				new Dictionary<string, object> {
				  { "text", "yee haw!?" }
				});
		}
	}
}
