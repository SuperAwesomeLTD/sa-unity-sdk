After importing the SuperAwesome Unity package, navigate to the Assets/SuperAwesome directory in the Project tab, locate the 'SuperAwesome Interstitial' prefab:

![](img/sa_interstitial_prefab.png "SuperAwesome Interstitial Prefab")

Drag the prefab into your scene; you should see a canvas containing the interstitial ad. Select the interstitial ad in order to modify its options in the Inspector tab.

![](img/interstitial_inspector.png "Interstitial Inspector")

Note: Make sure you select the Interstitial object, not the 'SuperAwesome Interstitial' parent, as seen in the hierarchy tab:

![](img/interstitial_hierarchy.png "Interstitial Object in Hierarchy")


In the inspector tab you can change the options for your interstitial ad. Possible options include:

| Option                | Description                                                                                                                                                                          |
|-----------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Placement ID          | This is your placement ID, as found on the SuperAwesome Dashboard.                                                                                                                   |
| Test Mode             | If selected, the interstitial will only load test ads from the server. Use this option while developing, and turn it off when your app is ready for production.                      |
| Open Instantly		| If selected, the interstitial will load and show instantly. If not, you will have to manually play it using the Show() function |
| is Parental Gate Enabled | When set to true, a parental gate will appear when users press on an ad. If users can solve a basic math question, they will be allowed to pass through. Else they will remain in the application | 