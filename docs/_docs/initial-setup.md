---
title: Initialise the SDK
description: Initialise the SDK
---

# Initialise the SDK

The first thing you’ll need to do after adding the SDK is to initialise it in a custom <strong>MonoBehaviour</strong> in your app.

```c#
using tv.superawesome.sdk.publisher;

public class MainScript : MonoBehaviour {

  // initialization
  void Start () {
    AwesomeAds.init(true);
  }
}
```

Where the <strong>initSDK</strong> method takes a boolean parameter indicating whether logging is enabled or not. For production environments logging should be <strong>off</strong>.