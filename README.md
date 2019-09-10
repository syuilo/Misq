# Misq
Misq is an official .NET Misskey library.

## Install
available on nuget

## Usage

``` Csharp
// Create your app instance
var app = new Misq.App("https://misskey.io", "your app's secret key here");

// Authorize user
var user = await app.Authorize();

// Let's post a message to Misskey
user.Request("notes/create", new Dictionary<string, object> {
  { "text", "yee haw!" }
});
```
