import { defineConfig } from 'vite';
import { resolve } from 'path';
import { viteStaticCopy } from 'vite-plugin-static-copy';
import autoprefixer from 'autoprefixer';
import fs from 'fs';

// This is ugly, but ATM how it's done in Oqtane
const themeName = 'ToSic.Cre8magic.TestTheme.Client';
const distFolder = `wwwroot/Themes/${themeName}`;
// const oqtaneTarget = resolve(__dirname, `../../oqtane.framework/Oqtane.Server/wwwroot/Themes`);

// // Plugin to clean Oqtane destination before copying
// function deleteFolderPlugin(destPath) {
//     return {
//         name: 'delete-folder',
//         apply: 'build',
//         buildStart() {
//             console.log(`[delete-folder] "${destPath}"`);
//             if (fs.existsSync(destPath)) {
//                 fs.rmSync(destPath, { recursive: true, force: true });
//             }
//         }
//     };
// }

export default defineConfig(({ mode }) => {
  const isDebug = (mode == 'dev');
  return {
    build: {
      sourcemap: true,
      minify: isDebug ? false : 'esbuild',
      cssCodeSplit: false,
      outDir: distFolder,
      rollupOptions: {
        input: {
          theme: './src/styles/theme.scss'
        },
        output: {
          assetFileNames: (assetInfo) => {
            if (assetInfo.name === 'style.css') return 'theme.min.css';
            return '[name][extname]'; // default for everything else
          },
        },
      },
    },

    css: {
      preprocessorOptions: {
        scss: {
          silenceDeprecations: ['mixed-decls', 'color-functions', 'global-builtin', 'import']
        },
      },
      postcss: {
        plugins: [autoprefixer()]
      },
    },

    // Handle copying of files
    plugins: [
      // deleteFolderPlugin(`${oqtaneTarget}\\${themeName}`), // Clean the Oqtane destination folder before copying
      viteStaticCopy({
        targets: [
          {
            src: ['./src/*.json', './src/assets', './node_modules/bootstrap/dist/js/bootstrap.bundle.min.*'],
            dest: './'
          },
          // {
          //     src: distFolder,
          //     dest: oqtaneTarget,
          // }
        ],
        hook: 'writeBundle',
      }),
    ]
  };
});