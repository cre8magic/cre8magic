const l = "theme", c = `${l}-breadcrumbs`;
function m() {
  console.log("initMailDecrypt"), setTimeout(function() {
    const e = document.querySelectorAll("[data-madr1]:not(.madr-done)");
    Array.from(e).forEach((t, o) => {
      const n = t.getAttribute("data-madr1") + "@" + t.getAttribute("data-madr2") + "." + t.getAttribute("data-madr3"), i = t.getAttribute("data-linktext") ? t.getAttribute("data-linktext") : n, a = document.createElement("a");
      a.setAttribute("href", `mailto:${n}`), a.innerHTML = i || "", t.parentElement && t.parentElement.appendChild(a), t.classList.add("madr-done"), t.style.display = "none";
    });
  }, 500);
}
function f() {
  console.log("initOffCanvasEvents");
  const e = document.querySelectorAll(".mobile-navigation-link"), t = document.getElementById("offcanvasNavbar");
  t && e.forEach((o) => p(o, t));
}
function p(e, t) {
  console.log("hideNavbarAfterClick", e, t), e.addEventListener("click", () => {
    new window.bootstrap.Offcanvas(t).hide();
  });
}
function v() {
  console.log("init breadcrumbs"), s(), window.addEventListener("resize", s), document.addEventListener("scroll", s);
  var e = document.querySelector(`.${c}-trigger`);
  e != null && e.addEventListener("click", () => {
    const t = document.querySelector(`.${c}`);
    console.log(t), t == null || t.classList.toggle(`${c}-open`);
  });
}
function s() {
  console.log("test 2dm 2024-11-25b");
  var e = document.getElementById(`${l}-page-navigation`);
  if (e != null) {
    var t = e.offsetHeight, o = document.querySelector(`.${c}`);
    if (!o)
      return;
    o.style.top = t + "px";
    var n = o.getBoundingClientRect().top;
    n <= t && scrollY != 0 ? o.classList.add("bg-light", "shadow") : o.classList.remove("bg-light", "shadow");
  }
}
function w() {
  document.querySelectorAll("a").forEach((e, t) => {
    var o;
    e.hasAttribute("href") && ((o = e.getAttribute("href")) != null && o.endsWith(".pdf")) && e.setAttribute("target", "_blank");
  });
}
const b = `${l}-to-top`, h = "visible";
function y() {
  console.log("init to top"), E();
}
function E() {
  console.log("toTopShowHideOnScroll"), document.addEventListener("scroll", T);
}
function T() {
  console.log("toTopButtonVisibility");
  var e = document.getElementById(b);
  if (!e)
    return;
  e.addEventListener("click", L);
  const t = document.body.scrollTop > 200 || document.documentElement.scrollTop > 200;
  e.classList.toggle(h, t);
}
function L() {
  console.log("scrollTop"), window.scrollTo({ top: 0, behavior: "smooth" });
}
function A(e) {
  console.log("2dm test activate GTM"), function(t, o, n, i, a) {
    t[i] = t[i] || [], t[i].push({ "gtm.start": (/* @__PURE__ */ new Date()).getTime(), event: "gtm.js" });
    var d = o.getElementsByTagName(n)[0], r = o.createElement(n), u = "";
    r.async = !0, r.src = "https://www.googletagmanager.com/gtm.js?id=" + a + u, d.parentNode.insertBefore(r, d);
  }(window, document, "script", "dataLayer", e);
}
class B {
  constructor() {
    this.activate = A;
  }
  pageView() {
    console.log("gtm-interop - track page view"), g("event", "blazor_page_view");
  }
}
window.dataLayer = window.dataLayer || [];
function g(e, t) {
  console.log("gtm - gtag"), window.dataLayer.push(arguments);
}
function S() {
  window.cre8magic = window.cre8magic || {}, window.cre8magic.gtm = window.cre8magic.gtm || new B(), window.gtag = window.gtag || g;
}
y();
w();
m();
v();
f();
S();
//# sourceMappingURL=ambient.js.map
