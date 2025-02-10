require("jsonc-require");
const path = require("path");
const webpack = require("webpack");

const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const RemoveEmptyScriptsPlugin = require("webpack-remove-empty-scripts");
const CopyPlugin = require("copy-webpack-plugin");
const FileManagerPlugin = require("filemanager-webpack-plugin");

const glob = require("glob");
const exec = require("child_process").exec;
const { merge } = require("webpack-merge");

// DEV ENV - for local development
const oqtaneRoot = "../../oqtane.framework/Oqtane.Server"; // Oqtane.Server folder relative to this file
const staticWebAssets = `../ToSic.cre8magic.client/wwwroot`;
const staticWebAssetsInOqtane = `${oqtaneRoot}/wwwroot/_content/ToSic.Cre8magic.Oqtane`;
const distFolder = `dist`;

const commonConfig = {
  mode: "production",
  entry: {
    ambient: glob.sync("./src/scripts/ambient/*.ts"),
    // interop: glob.sync("./src/scripts/interop/*.ts"),
  },
  output: {
    path: path.resolve(__dirname, distFolder),
    filename: "[name].js",
  },
  devtool: "source-map",
  performance: {
    assetFilter: function (assetFilename) {
      return assetFilename.endsWith(".js");
    },
  },
  stats: {
    warnings: false,
    cachedModules: false,
    groupModulesByCacheStatus: false,
  },
  cache: {
    type: "filesystem",
    cacheDirectory: path.resolve(__dirname, ".temp_cache"),
    compression: "gzip",
  },
  resolve: {
    extensions: [".scss", ".ts", ".js"],
  },
  plugins: [
    new RemoveEmptyScriptsPlugin(), // prevent empty styles.js from being created :( https://www.npmjs.com/package/webpack-remove-empty-scripts
    new webpack.ProgressPlugin(),
    {
      // triggers tsc build the interop independently from before webpack compile
      // needed for /interop js-files
      // these basically bypass webpack and directly let TSC compile this
      // note: this is not watching the files, so you need to run webpack again to get the latest interop files
      apply: (compiler) => {
        compiler.hooks.beforeCompile.tap("BeforeCompilePlugin", () => {
          exec(
            `npx tsc -p ./src/scripts/interop/tsconfig.json --outDir ${distFolder}/interop`,
            (err, stdout, stderr) => {
              if (stdout) process.stdout.write(stdout);
              if (stderr) process.stderr.write(stderr);
            }
          ).on("exit", () => {});
        });
      },
    },
    new FileManagerPlugin({
        events: {
            onEnd: {
                copy: [
                    {
                        source: distFolder,
                        destination: path.resolve(__dirname, staticWebAssets),
                    },
                ],
            },
        },
    }),
    new FileManagerPlugin({
        events: {
            onEnd: {
                copy: [
                    {
                        source: distFolder,
                        destination: path.resolve(__dirname, staticWebAssetsInOqtane),
                    },
                ],
            },
        },
    }),
  ],
  module: {
    rules: [
      {
        test: /\.woff|woff2/,
        type: "asset/resource",
        generator: {
          filename: "Fonts/[hash][ext][query]",
        },
      },
      {
        test: /\.s[ac]ss$/i,
        use: [
          MiniCssExtractPlugin.loader,
          {
            loader: "css-loader",
            options: {
              sourceMap: true,
            },
          },
          {
            loader: "postcss-loader",
            options: {
              sourceMap: true,
              postcssOptions: {
                plugins: function () {
                  return [require("autoprefixer")];
                },
              },
            },
          },
          {
            loader: "sass-loader",
            options: {
              sourceMap: true,
            },
          },
        ],
      },
      {
        test: /\.ts$/,
        exclude: [/node_modules/, /scripts\/interop/],
        use: {
          loader: "ts-loader",
          options: {
            transpileOnly: true,
          },
        },
      },
    ],
  },
};

const watchConfig = {
  watch: true,
  plugins: [
    // copy dist to to oqtane web dir
    new FileManagerPlugin({
      events: {
        onEnd: {
          copy: [
            {
              source: distFolder,
              destination: path.resolve(__dirname, staticWebAssetsInOqtane),
            },
          ],
        },
      },
    }),
  ],
};

const buildConfig = {
  watch: false,
};

module.exports = (env, args) => {
  const config = env.watch
    ? merge(commonConfig, watchConfig)
    : merge(commonConfig, buildConfig);

  return config;
};

new webpack.ProgressPlugin((percentage, message) => {
  console.log(`${(percentage * 100).toFixed()}% ${message}`);
});
