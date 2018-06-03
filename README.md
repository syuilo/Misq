# Misq
.NET Misskey liblary

## Install
available on nuget

## Usage

``` Csharp
var app = new Misq.App("https://misskey.xyz", "your app's secret key here");

// Authorize user
var user = await app.Authorize();

// Let's post a message to Misskey
user.Request("notes/create", new Dictionary<string, object> {
  { "text", "yee haw!" }
});
```
