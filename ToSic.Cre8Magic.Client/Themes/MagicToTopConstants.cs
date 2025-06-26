namespace ToSic.Cre8magic.Themes;

/// <summary>
/// Constants for the ToTop component - a floating button that scrolls the page to the top.
/// </summary>
public class MagicToTopConstants
{
    /// <summary>
    /// The ID of the link to top element.
    /// </summary>
    public const string LinkToTopNameId = "linkToTop";

    /// <summary>
    /// The default name of the link to top element.
    /// This is the default ID used by the JS to enable the show/hide behavior when the user scrolls down.
    /// </summary>
    public const string LinkToTopHtmlId = "theme-to-top";

    public const string LinkToTopImage = "toTopImage";

    // TODO: make sure the Link-To-Top uses the currentColor
    // and make sure that it's set by CSS

    /// <summary>
    /// Default SVG for the link to top element.
    /// </summary>
    public const string LinkToTopDefaultSvg = """
<svg xmlns="http://www.w3.org/2000/svg" width="19.032" height="20.034" viewBox="0 0 19.032 20.034">
  <g id="Group_2" data-name="Group 2" transform="translate(-1055.984 -551.276)">
      <path id="Path_2" data-name="Path 2" d="M8.1,16.2,0,8.1,8.1,0" transform="translate(1073.602 552.69) rotate(90)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2" />
      <line id="Line_1" data-name="Line 1" y2="17.599" transform="translate(1065.481 552.711)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2" />
  </g>
</svg>
""";

}