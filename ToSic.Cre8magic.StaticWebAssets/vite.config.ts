import { defineConfig } from 'vite';
import { resolve } from 'path';
import copy from 'rollup-plugin-copy';

export default defineConfig({
  build: {
    sourcemap: true,
    outDir: 'dist',
    minify: 'esbuild',
    cssCodeSplit: false,
    lib: {
      entry: {
        interop: resolve(__dirname, 'src/scripts/interop/interop.ts'),
        ambient: resolve(__dirname, 'src/scripts/ambient/index.ts'),
        style: resolve(__dirname, 'src/styles/style.scss')
      },
      formats: ['es']
    },
    rollupOptions: {
      output: {
        assetFileNames: (assetInfo) => {
          if (assetInfo.name?.endsWith('.css')) return 'style.min.css';
          if (assetInfo.name?.endsWith('.css.map')) return 'style.min.css.map';
          return '[name][extname]';
        },
      },
      plugins: [
        // Copy all files in dist to the OqtaneStaticAssetsPath after build
        copy({
          targets: [
            {
              src: 'dist/**/*',
              dest: '../../oqtane.framework/Oqtane.Server/wwwroot/_content/ToSic.Cre8magic.Oqtane'
            }
          ],
          hook: 'writeBundle'
        })
      ]
    }
  },
  // css: {
  //   devSourcemap: true,
  //   postcss: {
  //     map: { inline: false },
  //     plugins: []
  //   }
  // },
});

