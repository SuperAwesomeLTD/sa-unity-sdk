---
title: Verifying a user’s age
description: Verifying a user’s age
---

# Verifying a user’s age

A new feature in the SDK is the ability to verify a user’s age, given a date of birth.

An example below:

{% highlight java %}
// a date of birth in YYYY-MM-DD format
final String dateOfBirth = "2012-02-02";
Context context = this;

AwesomeAds.triggerAgeCheck(context, dateOfBirth, new GetIsMinorInterface() {
  @Override
  public void getIsMinorData(GetIsMinorModel isMinorModel) {

    if (isMinorModel != null) {
      // relevant values in the model
      String country = isMinorModel.getCountry()
      int consentAge = isMinorModel.getConsentAgeForCountry()
      int userAge = isMinorModel.getAge()
      boolean isMinor = isMinorModel.isMinor()
    }
  }
});
{% endhighlight %}