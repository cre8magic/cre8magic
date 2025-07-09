---
uid: OqtaneThemes.ThemeSettings.Index
---

# Theme Settings in Oqtane

In diesem Abschnitt wird erklärt, was Theme Settings in Oqtane sind, wo sie sich befinden, wie sie gespeichert und geladen werden und wie sie sich auf Site- und Page-Ebene unterscheiden.

---

## Was sind Theme Settings?

Theme Settings sind konfigurierbare Einstellungen, die es erlauben, das Aussehen und Verhalten eines Themes in Oqtane individuell anzupassen. Sie ermöglichen es Administratoren, Design- und Layout-Optionen zentral oder seitenbezogen zu steuern, ohne direkt in den Code eingreifen zu müssen.

---

## Theme Settings im Überblick

**Wo befinden sich Theme Settings im UI?**

- Theme Settings sind über das Oqtane-Admin-Panel erreichbar.
- Navigiere zu „Site Management“ → „Themes“.
- Bei Auswahl eines aktiven Themes findest du einen Bereich für Theme-spezifische Einstellungen.
- Zusätzlich können Theme Settings oft auch direkt auf einer Seite im „Page Settings“-Dialog angepasst werden, sofern das Theme dies unterstützt.
 
 **Wo befinden sich Theme Settings im Code?**

- Theme Settings werden in der `Theme.razor` oder einer spezifischen `Settings.razor`-Datei des Themes definiert.
- Die Einstellungen werden häufig über Klassen wie `ThemeSettings`, ein POCO-Objekt, im Theme-Projekt modelliert.
- Zugriff auf die Settings erfolgt im Theme-Code über Dependency Injection oder über das Settings-API von Oqtane.

**Beispiel:**

```csharp
public class ThemeSettings
{
    public string PrimaryColor { get; set; }
    public bool ShowLogo { get; set; }
}
```

**Was kann man mit Theme Settings alles machen?**

- Farben, Schriftarten, Abstände und weitere Styles anpassen.
- Sichtbarkeit von Komponenten (z.B. Logo, Navigation) steuern.
- Custom CSS/JS einbinden.
- Individuelle Optionen für spezielle Theme-Funktionalität bereitstellen.

---

## Speicherung von Theme Settings

**UI-Änderungen**

- Änderungen werden direkt im UI vorgenommen (z.B. Farbauswahl, Checkboxen).
- Nach einer Änderung muss auf „Speichern“ geklickt werden.

**Speicherung in der Datenbank**

- Theme Settings werden nach dem Speichern in der Oqtane-Datenbank abgelegt.
- Die Datenbank speichert die Einstellungen entweder auf Site- oder Page-Ebene, je nach Kontext.

**Namespace – keine Duplikate**

- Jede Theme Setting erhält einen eindeutigen Namespace (z.B. `Theme.MyTheme.SettingName`), um Kollisionen mit anderen Modulen oder Themes zu vermeiden.
- Dies gewährleistet, dass die Einstellungen eindeutig zugeordnet werden können.

---

## Wie und wo werden Theme Settings geladen?

- Beim Laden einer Seite werden die Theme Settings zunächst aus der Datenbank gelesen.
- Die Werte werden anhand des Namespaces dem korrekten Theme zugeordnet.
- Im Theme-Code können die Einstellungen über das Settings-API oder per Injection geladen und genutzt werden.

**Beispiel:**

```csharp
// Zugriff auf Theme Settings
var themeSettings = Settings.Get<ThemeSettings>("Theme.MyTheme");
```

---

## Site vs. Page Settings

**Unterschied zwischen Site Settings und Page Settings**

- **Site Settings**: Gelten global für die gesamte Website. Änderungen wirken sich auf alle Seiten aus.
- **Page Settings**: Gelten nur für eine bestimmte Seite. Überschreiben ggf. die Site Settings für diese Seite.

**Wie werden diese gemerged?**

- Beim Rendern einer Seite werden zunächst die Site Settings geladen.
- Falls für die aktuelle Seite Page Settings existieren, überschreiben diese die entsprechenden Werte aus den Site Settings (Merge-Mechanismus).
- Das resultierende Setting-Objekt kombiniert also globale und seitenbezogene Einstellungen.

---

## Beispiel: Custom Theme Settings im Oqtane Theme Basic

Im Oqtane Theme Basic haben wir den Code refactored und eigene Custom Settings eingeführt. Beispiel:

```csharp
public class BasicThemeSettings
{
    public string BackgroundImageUrl { get; set; }
    public bool EnableDarkMode { get; set; }
}
```

Im UI können nun ein Hintergrundbild und ein Dark Mode für das Theme aktiviert werden. Die Speicherung erfolgt wie oben beschrieben, mit einem Namespace wie `Theme.Basic.BackgroundImageUrl`.

---

**Zusammenfassung:**  
Theme Settings in Oqtane ermöglichen eine flexible Anpassung des Aussehens und Verhaltens eines Themes – global oder seitenbezogen. Sie sind sowohl im UI als auch im Code konfigurierbar und werden eindeutig per Namespace identifiziert und verwaltet.

## Example: Refaktorisierte Custom Settings im Oqtane Basic Theme

Im Oqtane Basic Theme wurde der Umgang mit Theme Settings überarbeitet und um eigene Custom Settings erweitert. Dies bietet eine flexible Möglichkeit, Design-Optionen und spezielle Features direkt über das Oqtane-UI zu steuern.

[Oqtane Basic](xref:Cre8magic.MagicThemes.Settings.Index)