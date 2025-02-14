/**
 * Function to clear the <body> tag of all classes
 */
export function clearBodyClasses() {
    var body = document.querySelector("body");
    body === null || body === void 0 ? void 0 : body.removeAttribute("class");
}
/**
 * Function to set the body class
 * @param bodyClass Classes to add to the body
 */
export function setBodyClass(bodyClass) {
    var body = document.body;
    body.className = bodyClass;
}
//# sourceMappingURL=interop.js.map