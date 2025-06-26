# Styling with Bootstrap 5, SASS and Variables

## This Setup Uses Bootstrap 5 Best Practices

The styles for this theme are generated using SASS. 

This is best practice and allows you to manage a code file which in turn generates CSS. 

If you're not familiar with SASS, best check out the [Bootstrap SASS tutorial](https://getbootstrap.com/docs/5.3/customize/sass/).

## How SCSS Variables work

1. The `theme.scss` file is the entry point. Vite takes this and generates the `dist/theme.min.css`
1. The `_variables.scss` is the file that prepares all the variables. This is where you will usually make adjustments or override Bootstrap 5 variables

## How to use

1. First make sure you can build. That means make sure you already did `npm install` 
1. Vite will always be run on build of the solution 
1. You can also run Vite with `npm run build`for production (or use the dev version with `npm run dev`).

Then make changes to the `_variables.scss` as you need. In case you need more Bootstrap components, import them in the `theme.scss`.