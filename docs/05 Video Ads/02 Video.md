
To display a video ad you just have to call `SuperAwesome.VideoAd.open` with your placement ID.

```
SuperAwesome.VideoAd.open ("__YOUR_PLACEMENT_ID__");
```

You can optionally pass a second boolean parameter `testMode` - if set to true, the SDK will display a demo ad instead of fetching a real ad. This is recommended while you are still in development.