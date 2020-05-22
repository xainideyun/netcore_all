const path = require('path')
const BASE_URL = process.env.NODE_ENV === 'production' ? '/' : '/'
const resolve = dir => path.join(__dirname, dir)

module.exports = {
  lintOnSave: false,
  publicPath: BASE_URL,
  outputDir: '../dist/wwwroot',
  chainWebpack: config => {
    config.resolve.alias
      .set('_c', resolve('src/components'))
  },
  // 打包时不生成.map文件
  productionSourceMap: false,
  devServer: {
    proxy: 'http://localhost:5000'
  }
}
