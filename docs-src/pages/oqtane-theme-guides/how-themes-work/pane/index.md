---
 uid: OqtaneThemes.WhatArePanes.Index
---

# What is a Pane

A **Pane** is a designated area or placeholder within a page layout where modules can be placed.
Common examples include DefaultPane, HeaderPane, and others.

Each theme or layout can define its own set of panes. The names and number of panes depend on how the layout was built by the designer.

<div gallery="gallery01">
  <img src="./assets/oqtane-setting-pane_1.webp" data-caption="Cre8magic">
  <img src="./assets/oqtane-setting-pane_2.webp" data-caption="Default Theme with many Panes">
</div>

> [!TIP]
> We recommend using one, or at most two, panes to make it easy for users to understand and work with.

Show a short example in `Themes > Default.razor` to demonstrate how it works in the code.

```csharp
public override string Panes => PaneNames.Default + ",Header"; // Add for Dropdown Menu
```

 ```html
<div data-bs-theme ="default-theme" data-theme-variant="@Variant">
 @* Pane for Header content *@
  <div id="theme-page-header-pane" class="container-xxl px-0">
    <Pane Name="Header" />  
  </div>

  @* Default pane for main content *@
  <main role="main" id="theme-page-main">
    <Pane Name="@PaneNames.Default" /> 
  </main>
</div>
 ```


## Why Panes Matter

Using panes allows you to control the placement of your modules without writing any code.
You can visually arrange content to match your site's design, whether you're adding navigation, banners, or content blocks.

**For example:**

- You might place a Menu module in the HeaderPane
- A News module in the DefaultPane

This flexible structure helps keep your site organized and easy to manage.

