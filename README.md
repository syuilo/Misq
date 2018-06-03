# Misq
.NET Misskey liblary

## Install
available on nuget

## Usage

### 理想
``` Csharp
var app = new Misq.App("your app's secret key here");

// Authorize user
var user = await app.Authorize();

// Let's post a message to Misskey
user.Request("posts/create", new Dictionary<string, object> {
  { "text", "yee haw!" }
});
```

### 現状
``` Csharp
var app = new Misq.App("your app's secret key here");

// Begin authorize session
var done = await app.Authorize();

myAnyUIEvent += async () => {
  // Get authorized user
  var me = await done();
  
  // Let's post a message to Misskey
  me.Request("posts/create", new Dictionary<string, object> {
    { "text", "yee haw!" }
  });
};
```
