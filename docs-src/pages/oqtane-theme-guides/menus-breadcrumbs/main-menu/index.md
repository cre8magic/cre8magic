---
  uid: OqtaneThemes.MainMenu.Index
---

# Was möchte ich dir hier zeigen

In dieser Sektion möchten wir dir zeigen, wie du das Hauptmenü in Oqtane anpassen kannst.

In diesem Fall, möchte ich die HorizontaleItems anpassen, das ich auch auf das Parent Url clicken kann, wenn es ein child hat.

<div gallery="gallery01">
  <img src="./assets/main-menu-issue.webp" data-caption="Show Problem">
</div>

Ziel ist es, die Darstellung zu verbessern.

## Welche Schritte sind dafür nötig

Bevor wir ins Detail gehen, hier ein kurzer Überblick über den Ablauf:

1. Kopieren der Basis-Dateien des Oqtane-Menüs
2. Einfügen der Dateien in deinem Theme
3. Verlinken der neuen Dateien und Prüfen
4. Anpassen der Menüstruktur
5. Überprüfen des Outputs

---

### 1. Kopiere die Menü-Dateien vom Oqtane-Projekt in dein Theme

- Öffne das Oqtane-Hauptprojekt
- Navigiere zu `Client -> Theme`
- Kopiere die folgenden drei Dateien:
    1. Datei 1: `Menu.razor` – enthält die Menüstruktur
    2. Datei 2: `MenuHorizontal.razor` – enthält die Struktur des Horizontal
    3. Datei 3: `MenuItemsHorizontal.razor` – enthält die Logik der Items (Child und Parentes)

Um das Menü vollständig anzupassen, müssen wir die kopierten Dateien an einem neuen Speicherort einfügen.

Damit die Änderungen greifen, musst du die neue Menü-Datei in Default.razor referenzieren. Du kannst zur Überprüfung zunächst einen Test-Header einfügen:

#### Fangen wir mit der Menu.razor an

- Kopieren sie die Menu.razor in ihre Theme und erstellen sie ein Folder unter Theme und bennen sie es um z.b. CmMenu (Cre8magicMenu)
- Stellen sie sicher, das der Namespace stimmt. 
- Fügen sie ihr Menu unter Default.razor ein. Dafür benötigen sie den Namespaces ihres CmMenu 
- Anschliessen sollten sie eine kleine änderung vornemmen und das Projekt Bilden z.b. mit einem Hilfe Text, damit sie sehen können, ob es lädt 

<div gallery="gallery02">
  <img src="./assets/main-menu-cmMenu_1.webp" data-caption="Copy the Menu">
  <img src="./assets/main-menu-cmMenu_2.webp" data-caption="Add to your Theme code an rename with Prefix">
  <img src="./assets/main-menu-cmMenu_3.webp" data-caption="Use the new CmMenu">
  <img src="./assets/main-menu-cmMenu_4.webp" data-caption="Add some mini Changes">
  <img src="./assets/main-menu-cmMenu_5.webp" data-caption="Show the mini Changes">
</div>

#### Fügen wir nun das MenuHorizontal.razor ein

Dies brauchen wir, da wir hier auf das MenuItemsHorizontal verweisen, welches wir erweitern werden. 

- Kopieren des Files
- Im Menu Ordner einfügen, umbennen und den Namespace richtig setzen 
- Im CmMenu.razor nicht mehr auf das Oqtane MenuHorizontal sondern auf unser CmMenuHorizontal verweisen
- Builden und testen

<div gallery="gallery03">
  <img src="./assets/main-menu-cmMenuHorizontal_1.webp" data-caption="Copy the MenuHorizontal.razor">
  <img src="./assets/main-menu-cmMenuHorizontal_2.webp" data-caption="Add to your Theme code an rename with Prefix">
  <img src="./assets/main-menu-cmMenuHorizontal_3.webp" data-caption="Use the new CmMenuHorizontal">
</div>

#### Fügen wir nun das MenuItemsHorizontal.razor ein

- Kopieren des Files
- Im Menu Ordner einfügen, umbennen und den Namespace richtig setzen 
- Im CmMenuItemsHorizontal.razor nicht mehr auf das Oqtane MenuItemsHorizontal sondern auf unser CmMenuItemsHorizontal.razor verweisen
- Builden und testen
- Nun können wir unsere änderung vornhemen

<div gallery="gallery04">
  <img src="./assets/main-menu-cmMenuHorizontalItems_1.webp" data-caption="Copy the MenuItemsHorizontal.razor">
  <img src="./assets/main-menu-cmMenuHorizontalItems_2.webp" data-caption="Add to your Theme code an rename with Prefix">
  <img src="./assets/main-menu-cmMenuHorizontalItems_3.webp" data-caption="Use the new MenuItemsHorizontal">
</div>

---

### MenuItemsHorizontal anpassen, damit wir einen Hover Effect und kein click mehr haben

Jetzt kannst du das Menü nach deinen Anforderungen anpassen.
Beispielsweise:

- Änderung der Menüstruktur

Aufbau MenuItemsHorizontal angepasst.
Styles überarbeitet

---

### 5. Überprüfe die Änderungen

Nachdem du deine Anpassungen vorgenommen hast:

- Baue das Projekt erneut
- Teste im UI, ob die Änderungen wie gewünscht übernommen wurden

<div gallery="gallery06">
  <img src="./assets/main-menu-final.webp" data-caption="Show Final">
</div>

