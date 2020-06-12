---
title: Verifying a user’s age
description: Verifying a user’s age
---

# Verifying a user’s age

A new feature in the SDK is the ability to verify a user’s age, given a date of birth.

An example below:

```c#
// a date of birth in YYYY-MM-DD format
string dateOfBirth = "2012-02-02";

AwesomeAds.triggerAgeCheck(dateOfBirth, (isMinorModel) => {
  if (isMinorModel != null) {
    // relevant values in the model
    string country = isMinorModel.country;
    int consentAge = isMinorModel.consentAgeForCountry;
    int userAge = isMinorModel.age;
    boolean isMinor = isMinorModel.isMinor;
  }
});
```