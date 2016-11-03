# Misq
.NET Misskey liblary

## Install
nuget coming soon

## Usage
``` Csharp
var app = new Misq.App("your app's secret key here");
var done = await app.Authorize();

this.button.Click += async (_1, _2) => {
  var me = await done();
  
  me.Request("/posts/create", new Dictionary<string, string> {
    { "text", "yee haw!" }
  });
};
```
