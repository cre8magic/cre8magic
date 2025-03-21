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
//export function gtmActivate(gtmId: string) {
//  window.cre8magic.gtm.activate(gtmId);
//}

/**
 * Track a page view in Google Tag Manager
 */
//export function gtmPageView() {
//  window.cre8magic.gtm.pageView();
//}
