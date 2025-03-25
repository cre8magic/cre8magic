import { debug } from '../shared/constants';

// Debug setting for GTM functionality
const debugGtm = debug || true;

/**
 * Activate Google Tag Manager
 * @param gtmId GTM container ID, like 'GTM-XXXXXXX'
 */
export function gtmActivate(gtmId: string) {
  if (debugGtm) console.log('2dm test activate GTM');
  // <!-- Google Tag Manager -->
  (function (w, d, s, l, i) {
    (w as any)[l] = (w as any)[l] || [];
    (w as any)[l].push({ 'gtm.start': new Date().getTime(), event: 'gtm.js' });
    var f = d.getElementsByTagName(s)[0],
      j = d.createElement(s) as HTMLScriptElement, dl = l != 'dataLayer' ? '&l=' + l : '';
    j.async = true; j.src = 'https://www.googletagmanager.com/gtm.js?id=' + i + dl;
    f.parentNode!.insertBefore(j, f);
  })(window, document, 'script', 'dataLayer', gtmId);
}

/**
 * Track a page view in Google Tag Manager
 */
export function gtmPageView() {
  if (debugGtm) console.log('gtm-interop - track page view');
  gtag('event', 'blazor_page_view');
}

/**
 * Track a custom event in Google Tag Manager
 * @param target Usually 'event'
 * @param more Event data
 */
export function gtag(target: 'event', more: unknown) {
  if (debugGtm) console.log('gtm - gtag');
  window.dataLayer = window.dataLayer || [];
  window.dataLayer.push(arguments);
}