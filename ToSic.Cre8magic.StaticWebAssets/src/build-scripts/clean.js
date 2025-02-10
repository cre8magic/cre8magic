require("jsonc-require");
const shell = require("shelljs");

console.log(`cleaning dist directory`);
shell.rm("-rf", `dist/*`);