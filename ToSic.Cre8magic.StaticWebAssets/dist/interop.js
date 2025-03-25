function m() {
  var e = document.querySelector("body");
  e == null || e.removeAttribute("class");
}
function d(e) {
  var t = document.body;
  t.className = e;
}
function l(e) {
  console.log("2dm test activate GTM"), function(t, n, r, a, s) {
    t[a] = t[a] || [], t[a].push({ "gtm.start": (/* @__PURE__ */ new Date()).getTime(), event: "gtm.js" });
    var g = n.getElementsByTagName(r)[0], o = n.createElement(r), c = "";
    o.async = !0, o.src = "https://www.googletagmanager.com/gtm.js?id=" + s + c, g.parentNode.insertBefore(o, g);
  }(window, document, "script", "dataLayer", e);
}
function u() {
  console.log("gtm-interop - track page view"), i("event", "blazor_page_view");
}
function i(e, t) {
  console.log("gtm - gtag"), window.dataLayer = window.dataLayer || [], window.dataLayer.push(arguments);
}
export {
  m as clearBodyClasses,
  i as gtag,
  l as gtmActivate,
  u as gtmPageView,
  d as setBodyClass
};
//# sourceMappingURL=interop.js.map
