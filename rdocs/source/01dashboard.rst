Getting started
===============

The first step in integrating the SuperAwesome SDK is to register on the `SuperAwesome Dashboard <http://dashboard.superawesome.tv>`_.

.. image:: img/IMG_01_Dashboard_1.png

From here you'll be able to create apps and placements, obtain performance reports (number of impressions, clicks), view current revenue, etc.

Creating Apps
^^^^^^^^^^^^^

When first logging in to the dashboard, the first thing you'll want to do is create one or more **Apps**,
with each app in the dashboard representing one of your own apps.

Each app has a number of associated parameters such as ID, name, domain (or iTunes URL), etc.
The name and domain can be configured when you create the app, while the ID is automatically assigned so as to be unique to each app.

.. image:: img/IMG_01_Dashboard_2.png

In turn, each app can have multiple placements.

Adding Placements
^^^^^^^^^^^^^^^^^

Placements represent ad units where creatives will be served. Creatives are the actual ad data that gets shown in your app,
like images, videos, interactive rich media content, etc.

.. image:: img/IMG_01_Dashboard_3.png

Each placement has a number of associated parameters like ID, name, format, dimension.

    * The placement's ID is a unique identifier associated with it. In the SDK it will be used to load ad data.
    * The name is a human readable identifier. It's good practice to name your placements something easy to remember or suggestive.
    * The format can be video, display, etc, and informs you of what type of ads should be run on the placement.
    * The dimensions give you an idea of where you should try to place the ad in your app.

.. image:: img/IMG_01_Dashboard_4.png

Below you can find a description of all mobile placement types, as well as test IDs, that have a 100% fill rate.

==================  ==========  =============================   ==============
Name                Size        Description                     Test Placement
==================  ==========  =============================   ==============
Standard Mobile     320x50px    Mobile banner                   30471
SM Mobile           300x50px    Small mobile banner             30476
Interstitial        320x480px   Mobile fullscreen (portrait)    30473
Interstitial LS     480x320px   Mobile fullscreen (landscape)   30474
Leaderboard         728x90px    Tablet banner                   30475
MPU                 300x250px   Smaller tablet banner           30472
LG Interstitial     768x1024px  Tablet fullscreen (portrait)    30477
LG Interstitial LS  1024x768px  Tablet fullscreen (landscape)   30478
Video Preroll       Flexible    Mobile & tablet video           30479
Gamewall            Flexible    Gamewall                        N/A
==================  ==========  =============================   ==============
