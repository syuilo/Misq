# Misq
.NET Misskey liblary

## Install
nuget coming soon

## Usage
``` Csharp
var app = new Misq.App("your app's secret key here");

// Authorize user
var user = await app.Authorize();

// Let's post a message to Misskey
user.Request("posts/create", new Dictionary<string, string> {
  { "text", "yee haw!" }
});
```
