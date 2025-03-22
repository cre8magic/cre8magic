const d = "theme", r = `${d}-breadcrumbs`;
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
  t && e.forEach((o) => v(o, t));
}
function v(e, t) {
  console.log("hideNavbarAfterClick", e, t), e.addEventListener("click", () => {
    new window.bootstrap.Offcanvas(t).hide();
  });
}
function w() {
  console.log("init breadcrumbs"), s(), window.addEventListener("resize", s), document.addEventListener("scroll", s);
  var e = document.querySelector(`.${r}-trigger`);
  e != null && e.addEventListener("click", () => {
    const t = document.querySelector(`.${r}`);
    console.log(t), t == null || t.classList.toggle(`${r}-open`);
  });
}
function s() {
  console.log("test 2dm 2024-11-25b");
  var e = document.getElementById(`${d}-page-navigation`);
  if (e != null) {
    var t = e.offsetHeight, o = document.querySelector(`.${r}`);
    if (!o)
      return;
    o.style.top = t + "px";
    var n = o.getBoundingClientRect().top;
    n <= t && scrollY != 0 ? o.classList.add("bg-light", "shadow") : o.classList.remove("bg-light", "shadow");
  }
}
function p() {
  document.querySelectorAll("a").forEach((e, t) => {
    var o;
    e.hasAttribute("href") && ((o = e.getAttribute("href")) != null && o.endsWith(".pdf")) && e.setAttribute("target", "_blank");
  });
}
const b = `${d}-to-top`, h = "visible";
function y() {
  console.log("init to top"), E();
}
function E() {
  console.log("toTopShowHideOnScroll"), document.addEventListener("scroll", L);
}
function L() {
  var e = document.getElementById(b);
  if (!e) return;
  e.addEventListener("click", T);
  const t = document.body.scrollTop > 200 || document.documentElement.scrollTop > 200;
  e.classList.toggle(h, t);
}
function T() {
  window.scrollTo({ top: 0, behavior: "smooth" });
}
function A(e) {
  console.log("2dm test activate GTM"), function(t, o, n, i, a) {
    t[i] = t[i] || [], t[i].push({ "gtm.start": (/* @__PURE__ */ new Date()).getTime(), event: "gtm.js" });
    var l = o.getElementsByTagName(n)[0], c = o.createElement(n), u = "";
    c.async = !0, c.src = "https://www.googletagmanager.com/gtm.js?id=" + a + u, l.parentNode.insertBefore(c, l);
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
p();
m();
w();
f();
S();
//# sourceMappingURL=ambient.js.map
