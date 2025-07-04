---
 uid: OqtaneThemes.Installation.Index
---

# Install Theme Guide

This guide will walk you through the two different methods of installing a custom theme in Oqtane: via the official Marketplace or by uploading a NuGet package manually. Both options are suitable depending on whether the theme is published online or developed locally.

> [!TIP]
> Make sure your theme is built and packaged correctly before installation. You will need a .nupkg file to upload it manually or a published release to use the marketplace..

---

## Install from Marketplace

<div gallery="gallery01">
  <img src="./assets/shared-install-theme_1.webp" data-caption="Step 1:<br />Open the Admin Dashboard <br /> Go to Theme Management">
  <img src="./assets/shared-install-theme_2.webp" data-caption="Step 2:<br />Click Install Theme">
  <img src="./assets/marketplace-install-theme_1.webp" data-caption="Step 3:<br />Select the Marketplace tab">
  <img src="./assets/marketplace-install-theme_2.webp" data-caption="Step 4:<br />Download and Accept the theme">
  <img src="./assets/marketplace-install-theme_3.webp" data-caption="Step 5:<br />Download Successfully, Restart the Application to activate the theme">
  <img src="./assets/shared-install-theme_3.webp" data-caption="Step 6:<br />Restart the Applicatctio">
</div>

To install a theme from the **Oqtane Marketplace**:

1. Open your **Admin Dashboard** and Go to **Theme Management**
2. Click **Install Theme**
3. Switch to the **Marketplace** tab
4. Click **Download** next to the desired theme and **Accept**
5. Finally, click **Restart Application** to activate it

**Note:**  

* On a **local environment**, you can restart directly from **Visual Studio**.  
* On a **server**, go to **System Info > Restart Application**.

---

Don’t have a `.nupkg` file yet?
Learn how to build one in [How to Build a NuGet Theme](xref:OqtaneThemes.PublishTheme.Index)

## Install from NuGet Package

<div gallery="gallery02">
  <img src="./assets/nuget-install-theme_1.webp" data-caption="Step 1:<br />Open Theme Management">
  <img src="./assets/nuget-install-theme_2.webp" data-caption="Step 2:<br />Upload your .nupkg file">
  <img src="./assets/shared-install-theme_3.webp" data-caption="Step 3:<br />Restart the application">
</div>

To manually install a theme using a `.nupkg` file:

Don’t have a `.nupkg` file yet?
Learn how to build one in [How to Build a NuGet Theme](xref:OqtaneThemes.PublishTheme.Index)

1. Go to **Admin Dashboard** → **Theme Management**
2. Click **Install Theme** and switch to the **Upload** tab
3. Select your `.nupkg` file and upload it
4. After upload, click **Restart Application**

**Note:**  

* On a **local environment**, you can restart directly from **Visual Studio**.  
* On a **server**, go to **System Info > Restart Application**.