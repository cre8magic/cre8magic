# ToSic.Cre8magic.StaticWebAssets

This project contains the source (and compiled dist) of the JS used by cre8magic.
It is a TypeScript project, which is compiled to JavaScript.

For the built it uses vite, which also handles typescript compilation.

- use the npm package to add TypeScript support!
- do not use TypeScript nuget package!

## Good to know

1. `npm install` happens automatically when the project is opened in Visual Studio (setting in the .esproj file)
1. Building project will build scripts in `/dist/` folder.
1. after build it will copy it to the development Oqtane (specified in the .esproj file - by default it's `../../oqtane.framework/Oqtane.Server/wwwroot/_content/ToSic.Cre8magic.Oqtane`)
1. for distribution in nuget, see project `ToSic.Cre8magic.Package` which will pick it up from the dist folder
1. vite is configured to not tree-shake using the syntax `const modules = import...`

## Contents

1. `/src/scripts/ambient` contains JS for the ambient environment - so scripts that are "always loaded / always there" such as breadcrumb helper JS
1. `/src/scripts/interop` contains JS for Oqtane interop to call

## Develop JS while Oqtane is Running in Visual Studio

When Visual Studio is running Oqtane, you cannot run the watcher at the same time.
But for developing the JS, you will want to do this.
The solution is to open this folder in a separate VS Code, and run the `serve` command and edit the code from there.

## INFO:

- [JavaScript and TypeScript in Visual Studio](https://learn.microsoft.com/en-us/visualstudio/javascript/javascript-in-visual-studio?view=vs-2022)
- [tsconfig.json](https://www.typescriptlang.org/docs/handbook/tsconfig-json.html)
- [Compile TypeScript code (Node.js)](https://learn.microsoft.com/en-us/visualstudio/javascript/compile-typescript-code-npm?view=vs-2022)
- [MSBuild reference for the JavaScript Project System](https://learn.microsoft.com/en-us/visualstudio/javascript/javascript-project-system-msbuild-reference?view=vs-2022)