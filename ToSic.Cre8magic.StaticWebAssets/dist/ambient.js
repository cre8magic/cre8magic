const d = "theme", r = `${d}-breadcrumbs`;
function m() {
  console.log("initMailDecrypt"), setTimeout(function() {
    const e = document.querySelectorAll("[data-madr1]:not(.madr-done)");
    Array.from(e).forEach((t, n) => {
      const o = t.getAttribute("data-madr1") + "@" + t.getAttribute("data-madr2") + "." + t.getAttribute("data-madr3"), i = t.getAttribute("data-linktext") ? t.getAttribute("data-linktext") : o, a = document.createElement("a");
      a.setAttribute("href", `mailto:${o}`), a.innerHTML = i || "", t.parentElement && t.parentElement.appendChild(a), t.classList.add("madr-done"), t.style.display = "none";
    });
  }, 500);
}
function f() {
  console.log("initOffCanvasEvents");
  var e = document.querySelectorAll(".mobile-navigation-link"), t = document.getElementById("offcanvasNavbar");
  if (t) {
    var n = new bootstrap.Offcanvas(t);
    e.forEach((o) => {
      o.addEventListener("click", () => {
        n.hide();
      });
    });
  }
}
function v() {
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
    var t = e.offsetHeight, n = document.querySelector(`.${r}`);
    if (!n)
      return;
    n.style.top = t + "px";
    var o = n.getBoundingClientRect().top;
    o <= t && scrollY != 0 ? n.classList.add("bg-light", "shadow") : n.classList.remove("bg-light", "shadow");
  }
}
function w() {
  document.querySelectorAll("a").forEach((e, t) => {
    var n;
    e.hasAttribute("href") && ((n = e.getAttribute("href")) != null && n.endsWith(".pdf")) && e.setAttribute("target", "_blank");
  });
}
const p = `${d}-to-top`, b = "visible";
function h() {
  console.log("init to top"), y();
}
function y() {
  console.log("toTopShowHideOnScroll"), document.addEventListener("scroll", E);
}
function E() {
  var e = document.getElementById(p);
  if (!e) return;
  e.addEventListener("click", T);
  const t = document.body.scrollTop > 200 || document.documentElement.scrollTop > 200;
  e.classList.toggle(b, t);
}
function T() {
  window.scrollTo({ top: 0, behavior: "smooth" });
}
function L(e) {
  console.log("2dm test activate GTM"), function(t, n, o, i, a) {
    t[i] = t[i] || [], t[i].push({ "gtm.start": (/* @__PURE__ */ new Date()).getTime(), event: "gtm.js" });
    var l = n.getElementsByTagName(o)[0], c = n.createElement(o), u = "";
    c.async = !0, c.src = "https://www.googletagmanager.com/gtm.js?id=" + a + u, l.parentNode.insertBefore(c, l);
  }(window, document, "script", "dataLayer", e);
}
class A {
  constructor() {
    this.activate = L;
  }
  pageView() {
    console.log("gtm-interop - track page view"), g("event", "blazor_page_view");
  }
}
window.dataLayer = window.dataLayer || [];
function g(e, t) {
  console.log("gtm - gtag"), window.dataLayer.push(arguments);
}
function B() {
  window.cre8magic = window.cre8magic || {}, window.cre8magic.gtm = window.cre8magic.gtm || new A(), window.gtag = window.gtag || g;
}
h();
w();
m();
v();
f();
B();
window.cre8magic.gtm.activate("GTM-T8W5TBL");
//# sourceMappingURL=ambient.js.map
