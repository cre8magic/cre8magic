import { defineConfig } from 'vite';
import { resolve } from 'path';
import copy from 'rollup-plugin-copy';

export default defineConfig({
    build: {
        sourcemap: true,
        outDir: 'dist',
        minify: 'esbuild',
        lib: {
            entry: {
                interop: resolve(__dirname, 'src/scripts/interop/interop.ts'),
                ambient: resolve(__dirname, 'src/scripts/ambient/index.ts')
            },
            formats: ['es']
        },
        rollupOptions: {
            output: {
                entryFileNames: '[name].js',
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
    }
});

