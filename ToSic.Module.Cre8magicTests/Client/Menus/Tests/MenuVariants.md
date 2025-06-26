# Menu Variants
    
Goal is to make notes of ca. how we want the menu to be used.

## Just us the Service & Data

This would just be using the service, and looping etc.
Examples:

```razor
@inject IMagicMenuService MenuSvc
@{
    var menuSettings = new MagicMenuSettings
    {
        Tailor = basicDesigner,
        MenuItems = MenuTestData.dicTopLevelThreePage[currentEnv]
    };
    var menu = MenuSvc.GetMenu(PageState, menuSettings);
}
    
@foreach(var item in menu)
{
    <div>@item.Title</div>
}
```


## Just the service - using Configs from elsewhere...?

Not sure if this is a realistic scenario, but it would basically be the same thing,
but the settings etc. would come from a central place...
    
Not sure if this makes sense, because you could simply have a helper object which
has the code elsewhere, to generate the settings, which is similar but less predefined architecture...

```razor
@inject IMagicMenuService MenuSvc
@inject MyMenuSettingsProvider MenuSettingsProvider
@{
    // Named Settings with somehow injected Settings provider
    var menu2 = MenuSvc.GetMenu(PageState, new() { SettingsName = "footer" });
    
    // Same but "nicer"
    var menu2 = MenuSvc.GetMenu(PageState, "footer");

    // Named settings with manually specified settings provider
    var menu2 = MenuSvc.GetMenu(PageState, new() { SettingsProvider = MenuSettingsProvider, SettingsName = "footer" });

    // Named Settings with differing parameter
    var menu2 = MenuSvc.GetMenu(PageState, new() { SettingsProvider = MenuSettingsProvider, SettingsName = "footer", Start = "52, 41!" })
}
```

Note: some consequences would probably be that we would internall separate 

- settings
- runtime context containing logs, all-settings etc. ? - would make the settings less mutable in a good way...
- TODO: needs clarity as to how the new settings are mixed with provided settings


## Just the service and just a Designer

This scenario would be uncommon, but I guess would make sense architecturally.
Basically you would use the service and a designer, to simply organize the code better.

```razor
@{
    var myDesigner = new MyMenuTailor();
    var menu = MenuSvc.GetMenu(PageState, new() { Designer = myDesigner });
}

@foreach(var item in menu)
{
    <div class='item.Classes("div")'>@item.Title</div>
}
```

Notes:

- the designer could be preconfigured, in which case it doesn't need to know the menu settings
- in other cases the designer should know about the current settings - TODO: how?
- in other cases the designer should know about more global settings - TODO: how?

- We probably need to differentiate between Tailor vs. DesigerFactory - the factory would be used to create a designer, and the designer would be used to render the menu
- ...or maybe also DesignerName / DesignerType, which would then use DI?


## Using Pre-Built controls

This would use the pre-built controls, which are already there.

```razor
<MagicMenu Settings="new()"></MagicMenu>

<MagicMenu Settings="new() { Tailor = new MyMenuTailor() }"></MagicMenu>

<MagicMenu Settings="new() { DesignerProvider = injectedProvider }"></MagicMenu>

<!-- maybe with name...?-->
<MagicMenu SettingsName="footer"></MagicMenu>
```

!WARNING!

We have 2 scenarios

1. Settings Name - where it should look up this name in the list of settings for this name
1. Name - where we look up the parts configuration to possibly find another name!
1. Theme Settings Name - where we would first choose the theme configuration? and then the part?

