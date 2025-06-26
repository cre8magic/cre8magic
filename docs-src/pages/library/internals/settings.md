---
uid: Cre8magic.Library.Internals.Settings
title: Magic Languages
---

# cre8magic – Internals of Settings

These documents should explain the internal workings / architecture of **cre8magic**.

The purpose is to ensure that we have clear conventions and that everybody who works on this does it the same way.

## Overview

Settings have a few important challenges, such as:

1. They should be optional in their entirety - so no settings should still result in a error free system.
1. Every setting should be optional - so if a setting is not there, the system should still work.
1. Settings should allow merging with other settings, so that they can be combined / enhanced step by step.
1. When a service needs a setting, it should be able to just access it, and not worry about the setting not being there.

## Basic Architecture

* Settings are all **record** objects
* Settings are all **read-only** objects
* All properties are init-only
* All properties should be nullable, to clearly indicate that they have not been specified (WIP)
* settings should be constructable through JSON (so they must have an empty public constructor)

## Reading Stabilized Settings

* Reading the settings for final use should happen through a `Stabilized` class, which provides defaults where necessary
* Every settings record must have a `GetStable()` method that returns a `Stabilized` object
  * it must be a method, not a property, to ensure that it's not used as a record property, incl. `GetHashCode()` / `Equality(...)` access and that it won't be serialized.
  * because of this, it may also _not_ have a backing `_stable` field, but must create a new instance every time

## Merging Settings

Settings should be able to be merged with other settings.
In general, this means that there are these concepts:

1. There is a **primary** settings object, whose properties have precedence
1. There is a **secondary** settings object, whose properties are used if the primary does not have them
1. If neither of the settings has a specific property, the resulting setting should be `null`

The way this is implemented is like this:

1. Every settings object has an internal constructor that takes two settings objects and will merge their properties
1. This `private protected` constructor will do the property merging
1. Since most settings inherit from `MagicSettings` and from `MagicInheritsBase`, they will call their base constructors to handle all their properties
1. For triggering a merge, every merge happens from an instance of the settings object and will call `CloneUnder(other)` to return a new merged object
1. To ensure that all settings have such a CloneUnder, they must all implement `ICanClone<[OwnType]>`
1. The `ICanClone<T>` is done as an explicit implementation, so that it does not appear in the docs and is "harder" to access

## Sample Implementation

As of now, the latest implementation of this best-practices is the `MagicAnalyticsSettings` - use this as a reference.

### MagicAnalyticsSettings

[!code-csharp[](../../../../ToSic.Cre8Magic.Client/Analytics/MagicAnalyticsSettings.cs)]

### Base Class MagicSettings

[!code-csharp[](../../../../ToSic.Cre8Magic.Client/Settings/MagicSettings.cs)]

### Root Base Class MagicInheritsBase

[!code-csharp[](../../../../ToSic.Cre8Magic.Client/Settings/MagicInheritsBase.cs)]

