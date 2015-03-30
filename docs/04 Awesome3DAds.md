The Unity SDK allows you to create in game ads for the user to interact with. These can be walls or posters or shirts or any other GameObject that allows a texture to be rendered.

http://www.youtube.com/embed/6TtnqCP6pOE

To do this, create a GameObject and attach the "ad.cs" script as with a Display Ad. Then in the inspector menu input your App_id and Placement_id.

Note: the "Closable" and "Fix Placement" options have no effect on Awesome3D Ads.

There are a few things to be considered when using Awesome3D Ads:
- The GameObject will be disabled until the ad is loaded (unless "Hide" is set to true, in which case the ad will only show when called from a script). This means that if you attach the ad to a wall for example, the wall will not exist in game until the ad has loaded. There are 2 solutions if you want to change this:
	- Put a quad in front of the wall and attach the ad to that - this has other advantages (such as the wall itself will not be resized when the ad is loaded, and the ad will only show on one face of the wall, instead of all of them).
	- You can copy the ad.cs file and comment out the following lines which can be found at the start of the ```Start()``` function:
	```
	if (!gameObject.guiTexture) {
		renderer.enabled = false;
	} else {
		guiTexture.enabled = false;
	}
	```
	You will need to proved a default texture with this method however.
- The GameObject that "ad.cs" is attached to will be resized when the ad is loaded to maintain the correct aspect ratio of the ad.
	- Only the "x" and "y" dimensions are changed, the "z" dimension will remain as set by you.
	- The GameObject will never be resized to be bigger than any dimension you set.
- The "ad.cs" script will add a Box Collider component to the GameObject. This is required for user clicks to be registered.
- Planes are currently not supported. It is recommended to use a Quad instead.