const path = require('path');
const glob = require('glob');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CssMinimizerPlugin = require('css-minimizer-webpack-plugin');
const TerserPlugin = require('terser-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

module.exports = (env, argv) => {
	const isDevMode = argv.mode === 'development';
	
	return {
		entry: {
			vendor: './webpack.vendor.js',
			app: './webpack.app.js'
		},
		output: {
			path: path.resolve(__dirname, 'wwwroot'),
			filename: 'js/[name].min.js'
		},
		module: {
			rules: [
				{
					test: /\.hbs$/,
					loader: 'handlebars-loader',
				},
				{
					test: /\.css$/,
					use: [
						MiniCssExtractPlugin.loader, 
						'css-loader', 
						{
							loader: 'postcss-loader',
							options: {
								postcssOptions: {
									plugins: [
										require('autoprefixer')()
									]
								}
							}
						}
					]
				},
				{
					test: /\.scss$/,
					use: [
						MiniCssExtractPlugin.loader, 
						'css-loader', 
						{
							loader: 'postcss-loader',
							options: {
								postcssOptions: {
									plugins: [
										require('autoprefixer')()
									]
								}
							}
						},
						'sass-loader']
				},
				{
					test: /\.(png|svg|jpg|jpeg|gif)$/,
					type: 'asset/resource',
					generator: {
						filename: 'img/[name][ext][query]'
					}
				},
				{
					test: /\.(woff|woff2|eot|ttf|otf)$/,
					type: 'asset/resource',
					generator: {
						filename: 'fonts/[name][ext][query]'
					}
				}
			]
		},
		plugins: [
			new CleanWebpackPlugin(),
			new MiniCssExtractPlugin({
				filename: 'css/[name].min.css'
			}),
			new CopyWebpackPlugin({
				patterns: [
					{ from: 'src/img', to: 'img' },
					{ from: 'src/data', to: 'data' },
					{ from: 'src/js/demo', to: 'js/demo' },
					{ from: 'node_modules/bootstrap-icons/font/fonts/', to: 'css/fonts/' },
					{ from: 'node_modules/@fortawesome/fontawesome-free/webfonts', to: 'webfonts' },
					{ from: 'node_modules/@fortawesome/', to: 'lib/@fortawesome/' },
					{ from: 'node_modules/@fullcalendar/', to: 'lib/@fullcalendar/' },
					{ from: 'node_modules/@highlightjs/cdn-assets/', to: 'lib/@highlightjs/cdn-assets/' },
					{ from: 'node_modules/apexcharts/', to: 'lib/apexcharts/' },
					{ from: 'node_modules/blueimp-canvas-to-blob/', to: 'lib/blueimp-canvas-to-blob/' },
					{ from: 'node_modules/blueimp-file-upload/', to: 'lib/blueimp-file-upload/' },
					{ from: 'node_modules/blueimp-gallery/', to: 'lib/blueimp-gallery/' },
					{ from: 'node_modules/blueimp-load-image/', to: 'lib/blueimp-load-image/' },
					{ from: 'node_modules/blueimp-tmpl/', to: 'lib/blueimp-tmpl/' },
					{ from: 'node_modules/bootstrap/', to: 'lib/bootstrap/' },
					{ from: 'node_modules/bootstrap-datepicker/', to: 'lib/bootstrap-datepicker/' },
					{ from: 'node_modules/bootstrap-daterangepicker/', to: 'lib/bootstrap-daterangepicker/' },
					{ from: 'node_modules/bootstrap-icons/', to: 'lib/bootstrap-icons/' },
					{ from: 'node_modules/bootstrap-slider/', to: 'lib/bootstrap-slider/' },
					{ from: 'node_modules/bootstrap-table/', to: 'lib/bootstrap-table/' },
					{ from: 'node_modules/bootstrap-timepicker/', to: 'lib/bootstrap-timepicker/' },
					{ from: 'node_modules/chart.js/', to: 'lib/chart.js/' },
					{ from: 'node_modules/datatables.net/', to: 'lib/datatables.net/' },
					{ from: 'node_modules/datatables.net-bs5/', to: 'lib/datatables.net-bs5/' },
					{ from: 'node_modules/datatables.net-autofill/', to: 'lib/datatables.net-autofill/' },
					{ from: 'node_modules/datatables.net-autofill-bs5/', to: 'lib/datatables.net-autofill-bs5/' },
					{ from: 'node_modules/datatables.net-buttons/', to: 'lib/datatables.net-buttons/' },
					{ from: 'node_modules/datatables.net-buttons-bs5/', to: 'lib/datatables.net-buttons-bs5/' },
					{ from: 'node_modules/datatables.net-colreorder/', to: 'lib/datatables.net-colreorder/' },
					{ from: 'node_modules/datatables.net-colreorder-bs5/', to: 'lib/datatables.net-colreorder-bs5/' },
					{ from: 'node_modules/datatables.net-fixedcolumns/', to: 'lib/datatables.net-fixedcolumns/' },
					{ from: 'node_modules/datatables.net-fixedcolumns-bs5/', to: 'lib/datatables.net-fixedcolumns-bs5/' },
					{ from: 'node_modules/datatables.net-fixedheader/', to: 'lib/datatables.net-fixedheader/' },
					{ from: 'node_modules/datatables.net-fixedheader-bs5/', to: 'lib/datatables.net-fixedheader-bs5/' },
					{ from: 'node_modules/datatables.net-keytable/', to: 'lib/datatables.net-keytable/' },
					{ from: 'node_modules/datatables.net-keytable-bs5/', to: 'lib/datatables.net-keytable-bs5/' },
					{ from: 'node_modules/datatables.net-responsive/', to: 'lib/datatables.net-responsive/' },
					{ from: 'node_modules/datatables.net-responsive-bs5/', to: 'lib/datatables.net-responsive-bs5/' },
					{ from: 'node_modules/datatables.net-rowgroup/', to: 'lib/datatables.net-rowgroup/' },
					{ from: 'node_modules/datatables.net-rowgroup-bs5/', to: 'lib/datatables.net-rowgroup-bs5/' },
					{ from: 'node_modules/datatables.net-rowreorder-bs5/', to: 'lib/datatables.net-rowreorder-bs5/' },
					{ from: 'node_modules/datatables.net-scroller/', to: 'lib/datatables.net-scroller/' },
					{ from: 'node_modules/datatables.net-scroller-bs5/', to: 'lib/datatables.net-scroller-bs5/' },
					{ from: 'node_modules/datatables.net-select/', to: 'lib/datatables.net-select/' },
					{ from: 'node_modules/datatables.net-select-bs5/', to: 'lib/datatables.net-select-bs5/' },
					{ from: 'node_modules/jquery/', to: 'lib/jquery/' },
					{ from: 'node_modules/jquery-migrate/', to: 'lib/jquery-migrate/' },
					{ from: 'node_modules/jquery-typeahead/', to: 'lib/jquery-typeahead/' },
					{ from: 'node_modules/jquery-ui-dist/', to: 'lib/jquery-ui-dist/' },
					{ from: 'node_modules/jquery.maskedinput/', to: 'lib/jquery.maskedinput/' },
					{ from: 'node_modules/js-cookie/', to: 'lib/js-cookie/' },
					{ from: 'node_modules/jszip/', to: 'lib/jszip/' },
					{ from: 'node_modules/jvectormap-content/', to: 'lib/jvectormap-content/' },
					{ from: 'node_modules/jvectormap-next/', to: 'lib/jvectormap-next/' },
					{ from: 'node_modules/lity/', to: 'lib/lity/' },
					{ from: 'node_modules/kbw-countdown/', to: 'lib/kbw-countdown/' },
					{ from: 'node_modules/masonry-layout/', to: 'lib/masonry-layout/' },
					{ from: 'node_modules/moment/', to: 'lib/moment/' },
					{ from: 'node_modules/perfect-scrollbar/', to: 'lib/perfect-scrollbar/' },
					{ from: 'node_modules/photoswipe/', to: 'lib/photoswipe/' },
					{ from: 'node_modules/select-picker/', to: 'lib/select-picker/' },
					{ from: 'node_modules/spectrum-colorpicker2/', to: 'lib/spectrum-colorpicker2/' },
					{ from: 'node_modules/summernote/', to: 'lib/summernote/' },
					{ from: 'node_modules/tag-it/', to: 'lib/tag-it' }
				]
			})
		],
		optimization: {
			minimizer: [
				new TerserPlugin({
					exclude: /assets\/plugins\/jquery-migrate\/test\/runner\/command\.js/,
				}),
				new CssMinimizerPlugin()
			],
			splitChunks: {
				cacheGroups: {
					vendorStyles: {
						type: 'css/mini-extract',
						test: /\.css$/,
						name: 'vendor',
						chunks: 'all',
						enforce: true,
					}
				}
			},
		},
		performance: {
			maxEntrypointSize: 1024000,
			maxAssetSize: 1024000,
		}
	}
};