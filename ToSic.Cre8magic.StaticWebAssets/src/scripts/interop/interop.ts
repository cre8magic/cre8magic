import { debug } from '../shared/constants';

// Temporary solution to add GTM, not final!
const debugGtm = debug || true;
/**
 * Function to clear the <body> tag of all classes
 */
export function clearBodyClasses() {
  var body = document.querySelector("body");
  body?.removeAttribute("class");
}

/**
 * Function to set the body class
 * @param bodyClass Classes to add to the body
 */
export function setBodyClass(bodyClass: string) {
  var body = document.body;
  body.className = bodyClass;
}

/**
 * Activate Google Tag Manager
 * @param gtmId GTM container ID, like 'GTM-XXXXXXX'
 */
export function gtmActivate(gtmId: string) {
  if (debugGtm) console.log('2dm test activate GTM');
  // <!-- Google Tag Manager -->
  // <script>
  (function (w, d, s, l, i) {
    (w as any)[l] = (w as any)[l] || [];
    (w as any)[l].push({ 'gtm.start': new Date().getTime(), event: 'gtm.js' });
    var f = d.getElementsByTagName(s)[0],
      j = d.createElement(s) as HTMLScriptElement, dl = l != 'dataLayer' ? '&l=' + l : '';
    j.async = true; j.src = 'https://www.googletagmanager.com/gtm.js?id=' + i + dl;
    f.parentNode!.insertBefore(j, f);
  })(window, document, 'script', 'dataLayer', gtmId);
  // </script>
  // <!-- End Google Tag Manager -->
}

/**
 * Track a page view in Google Tag Manager
 */
export function gtmPageView() {
  // window.cre8magic.gtm.pageView();
  if (debugGtm) console.log('gtm-interop - track page view');
  gtag('event', 'blazor_page_view');//
}

/**
 * Track a custom event in Google Tag Manager
 * @param eventName Name of the event to track
 */
export function gtag(target: 'event', more: unknown) {
  if (debugGtm) console.log('gtm - gtag');
  window.dataLayer = window.dataLayer || [];
  window.dataLayer.push(arguments);
}
