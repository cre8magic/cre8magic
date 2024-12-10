// WIP
// From https://github.com/dotnet/docfx/blob/main/templates/default/styles/docfx.js

const debug = false;

function setupEverything() {
  if (debug)
    console.log('setupEverything');

  inlineSvgs();

};

export default {
  start: () => {
    setupEverything();
  }
}

if (debug)
  console.log('main.js loaded');

export function inlineSvgs() {
  // For LOGO SVG
  // Replace SVG with inline SVG
  // http://stackoverflow.com/questions/11978995/how-to-change-color-of-svg-image-using-css-jquery-svg-image-replacement

  const svgs = document.querySelectorAll('img.svg') as NodeListOf<HTMLImageElement>;
  svgs.forEach((svg) => {
    const img = svg;
    const imgID = img.id;
    const imgClass = img.className;
    const imgURL = img.src;
    const imgWidth = img.getAttribute('width');
    const imgHeight = img.getAttribute('height');

    fetch(imgURL)
      .then((response) => {
        return response.text();
      })
      .then((data) => {
        // Get the SVG tag, ignore the rest
        const parser = new DOMParser();
        const xmlDoc = parser.parseFromString(data, 'text/xml');
        const $svg = xmlDoc.getElementsByTagName('svg')[0];

        // Add replaced image's ID to the new SVG
        if (imgID != null)
          $svg.id = imgID;

        // Add replaced image's classes to the new SVG
        if (imgClass != null)
          $svg.setAttribute('class', imgClass + ' replaced-svg');

        // Add replaced image's width to the new SVG
        if (imgWidth != null)
          $svg.setAttribute('width', imgWidth);

        // Add replaced image's height to the new SVG
        if (imgHeight != null)
          $svg.setAttribute('height', imgHeight);

        // Remove any invalid XML tags as per http://validator.w3.org
        $svg.removeAttribute('xmlns:a');

        // Replace image with new SVG
        img.parentNode?.replaceChild($svg, img);
      });
  });

}
