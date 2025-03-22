import { debug } from '../shared/constants';

/**
 * Global bootstrap variable on window.
 * Usually not available until later.
 */
// declare const bootstrap: bootstrap;

// Takes care of closing the offcanvas when clicking on a navigation link
// The page is not reloaded, so the offcanvas does not close
export function initOffCanvasEvents() {

  if (debug)
    console.log('initOffCanvasEvents');

  //Offcanvas close on link click
  const navLinks = document.querySelectorAll(".mobile-navigation-link");
  const navOffcanvas = document.getElementById('offcanvasNavbar') as HTMLElement;
  if (navOffcanvas) {
    navLinks.forEach(element => hideNavbarAfterClick(element, navOffcanvas));
  }
}

function hideNavbarAfterClick(element: Element, navOffcanvas: HTMLElement) {
  if (debug)
    console.log('hideNavbarAfterClick', element, navOffcanvas);

  element.addEventListener('click', () => {
    const bsNavOffcanvas = new window.bootstrap.Offcanvas(navOffcanvas)
    bsNavOffcanvas.hide();
  });
}