Please remember that in Unity, click events are not triggered at all unless there is an EventSystem UI object. If this doesn't exist in the Hierarchy, add one from the `GameObject > UI` menu.

Also, since the Unity SDK uses the iOS / Android native SDK, testing your app in Unity won't show ads. Only by playing the app on a simulator or device will the whole ad process be triggered.