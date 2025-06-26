class d {
  constructor() {
    this.pageViewCounter = 0;
  }
  /**
   * Activate Google Tag Manager
   * @param gtmId GTM container ID, like 'GTM-XXXXXXX'
   */
  addToPage(e) {
    console.log("2dm test activate GTM"), function(a, g, s, o, i) {
      a[o] = a[o] || [], a[o].push({ "gtm.start": (/* @__PURE__ */ new Date()).getTime(), event: "gtm.js" });
      var r = g.getElementsByTagName(s)[0], n = g.createElement(s), c = "";
      n.async = !0, n.src = "https://www.googletagmanager.com/gtm.js?id=" + i + c, r.parentNode.insertBefore(n, r);
    }(window, document, "script", "dataLayer", e);
  }
  /**
   * Track a page view in Google Tag Manager
   */
  pageView(e) {
    this.pageViewCounter++, console.log(`gtm-interop - track page view: ${this.pageViewCounter}`), this.gtag({ event: e || "Pageview" });
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
  gtag(e, a) {
    console.log("gtm - gtag", e, a, window.dataLayer), window.dataLayer = window.dataLayer || [], window.dataLayer.push(arguments);
  }
}
function m() {
  var t = document.querySelector("body");
  t == null || t.removeAttribute("class");
}
function w(t) {
  var e = document.body;
  e.className = t;
}
const l = new d();
export {
  d as GoogleTagManager,
  m as clearBodyClasses,
  l as gtm,
  w as setBodyClass
};
//# sourceMappingURL=interop.js.map
