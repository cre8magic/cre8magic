import { debug } from '../shared/constants';

// Debug setting for GTM functionality
const debugGtm = debug || true;

export class GoogleTagManager {

  /**
   * Activate Google Tag Manager
   * @param gtmId GTM container ID, like 'GTM-XXXXXXX'
   */
  addToPage(gtmId: string) {
    if (debugGtm)
      console.log('2dm test activate GTM');
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

  pageViewCounter = 0;

  /**
   * Track a page view in Google Tag Manager
   */
  pageView(verb?: string) {
    this.pageViewCounter++;
    if (debugGtm)
      console.log(`gtm-interop - track page view: ${this.pageViewCounter}`);
    // this.gtag('event', verb || 'blazor_page_view');
    this.gtag({ event: verb || 'Pageview' });
  }

  // /**
  //  * Track a custom event in Google Tag Manager
  //  * @param target Usually 'event'
  //  * @param more Event data
  //  */
  // gtagCorrected(data: { event: string | 'Pageview' }) {
  //   window.dataLayer = window.dataLayer || [];
  //   if (debugGtm)
  //     console.log('gtm - gtag corrected', data, window.dataLayer);
  //   window.dataLayer.push(data);
  // }

  /**
   * Track a custom event in Google Tag Manager
   * @param dataOrEvent Usually 'event'
   * @param more Event data
   */
  gtag(dataOrEvent: 'event' | Record<string, unknown>, more?: unknown) {
    if (debugGtm)
      console.log('gtm - gtag', dataOrEvent, more, window.dataLayer);
    window.dataLayer = window.dataLayer || [];
    window.dataLayer.push(arguments);
  }
}
