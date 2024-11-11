import { prefix, debug, prefixBreadcrumbs } from '../shared/constants';

export function initBreadcrumb() {
  if (debug) console.log("init breadcrumbs");

  setBreadcrumbsStyling() 
  window.addEventListener("resize", setBreadcrumbsStyling);
  document.addEventListener('scroll', setBreadcrumbsStyling);

  var breadCrumbTrigger = document.querySelector(`.${prefixBreadcrumbs}-trigger`);
  if(breadCrumbTrigger != null) {
    breadCrumbTrigger.addEventListener('click', () => {
      if (debug) console.log(document.querySelector(`.${prefixBreadcrumbs}`))
      document.querySelector(`.${prefixBreadcrumbs}`).classList.toggle(`${prefixBreadcrumbs}-open`)
    })
  }
}

function setBreadcrumbsStyling() {
  var header = document.getElementById(`${prefix}-page-navigation`);
  if(header != null) {
    var headerHeight = header.offsetHeight;

    var breadcrumb = document.querySelector(`.${prefixBreadcrumbs}`) as HTMLElement;
    breadcrumb.style.top = headerHeight + "px";
    var breadcrumbOffsetTop = breadcrumb.getBoundingClientRect().top;

    if (breadcrumbOffsetTop <= headerHeight && scrollY != 0) {
      breadcrumb.classList.add("bg-light", "shadow");
    } else {
      breadcrumb.classList.remove("bg-light", "shadow");
    }
  }
}
