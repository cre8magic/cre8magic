import { defineConfig } from 'vite';
import { resolve } from 'path';

export default defineConfig({
  build: {
    // Set sourcemap here per Vite 6.1.0 recommendations
    sourcemap: true,
    outDir: 'dist',
    rollupOptions: {
      input: {
        ambient: resolve(__dirname, 'src/scripts/ambient/index.ts')
        // interop: resolve(__dirname, 'src/scripts/interop/interop.ts')
      },
      output: {
        // Set output names to match the entry keys
        entryFileNames: '[name].js'
      },
      treeshake: false
    }
  }
});
