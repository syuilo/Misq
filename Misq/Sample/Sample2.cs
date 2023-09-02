
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Misq.Sample
{
	internal class Sample2
	{
		static async Task Main(string[] args)
		{
			// Generate a your access token on the web client.

			// Authorize user
			var user = new Misq.Me("https://misskey.io", "your access token here");

			// Let's post a message to Misskey
			await user.Request("notes/create",
				new Dictionary<string, object> {
				  { "text", "yee haw?" }
				});
		}
	}
}
