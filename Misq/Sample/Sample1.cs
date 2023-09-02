
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Misq.Sample
{
	internal class Sample1
	{
		static bool FirstTime => false;

		static async Task Main(string[] args)
		{
			Misq.App app;
			if (FirstTime) {
				// Register your app for the first time
				app = await Misq.App.Register(
						"https://misskey.io",
						"your app's name",
						"your app's description",
						new string[] { "write:notes" }
					);

				// You MUST record your app's secret key
				Console.WriteLine($"your app's secret key is [{app.Secret}]");
			} else {
				// From next time onwards, create your app instance
				app = new Misq.App("https://misskey.io", "your app's secret key here");
			}

			// Authorize user
			var user = await app.Authorize();

			// Let's post a message to Misskey
			await user.Request("notes/create",
				new Dictionary<string, object> {
				  { "text", "yee haw!" }
				});
		}
	}
}
