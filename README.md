# Misq
.NET Misskey liblary

## Install
nuget coming soon

## Usage
``` Csharp
var app = new Misq.App("your app's secret key here");

// Begin authorize session
var done = await app.Authorize();

myAnyUIEvent += async () => {
  // Get authorized user
  var me = await done();
  
  // Let's post a message to Misskey
  me.Request("/posts/create", new Dictionary<string, string> {
    { "text", "yee haw!" }
  });
};
```
