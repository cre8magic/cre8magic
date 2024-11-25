import { prefix, debug, prefixBreadcrumbs } from '../shared/constants';

export function initBreadcrumb() {
  if (debug) console.log("init breadcrumbs");

  setBreadcrumbsStyling() 
  window.addEventListener("resize", setBreadcrumbsStyling);
  document.addEventListener('scroll', setBreadcrumbsStyling);

  var breadCrumbTrigger = document.querySelector(`.${prefixBreadcrumbs}-trigger`);
  if (breadCrumbTrigger != null) {
    breadCrumbTrigger.addEventListener('click', () => {
      const selected = document.querySelector(`.${prefixBreadcrumbs}`);
      if (debug) console.log(selected)
      selected?.classList.toggle(`${prefixBreadcrumbs}-open`)
    })
  }
}

function setBreadcrumbsStyling() {
  console.log("test 2dm 2024-11-25b");

  var header = document.getElementById(`${prefix}-page-navigation`);
  if (header != null) {
    var headerHeight = header.offsetHeight;

    var breadcrumb = document.querySelector(`.${prefixBreadcrumbs}`) as HTMLElement;

    if (!breadcrumb)
      return;
    breadcrumb.style.top = headerHeight + "px";
    var breadcrumbOffsetTop = breadcrumb.getBoundingClientRect().top;

    if (breadcrumbOffsetTop <= headerHeight && scrollY != 0) {
      breadcrumb.classList.add("bg-light", "shadow");
    } else {
      breadcrumb.classList.remove("bg-light", "shadow");
    }
  }
}
