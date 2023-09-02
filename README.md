# Misq
Misq is an official .NET Misskey library.

## Install
available on nuget

## Usage


### case 1.Use app creation method (for older than Ver.12.27.0)
``` Csharp
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
user.Request("notes/create", new Dictionary<string, object> {
  { "text", "yee haw!" }
});
```
Learn more at https://misskey-hub.net/docs/api/app.html

### case 2.Manually issue your own access token
``` Csharp
// Generate a your access token on the web client.

// Authorize user
var user = new Misq.Me("https://misskey.io", "your access token here");

// Let's post a message to Misskey
await user.Request("notes/create", new Dictionary<string, object> {
    { "text", "yee haw?" }
});
```

Learn more at https://misskey-hub.net/docs/api/

### case 3.Use MiAuth
``` Csharp
// Create your app instance
var app = new Misq.App("https://misskey.io");
// Authorize user
var user = await app.Authorize(
	"your app's name",
    new string[] { "write:notes" });

// Let's post a message to Misskey
await user.Request("notes/create", new Dictionary<string, object> {
    { "text", "yee haw?" }
});
```

Learn more at https://misskey-hub.net/docs/api/
