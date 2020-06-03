/**
 * 应用store
 */
import Cookies from 'js-cookie'
import config from '@/config'
import {
  asyncRoutes,
  constantRoutes
} from '@/router/router'

/**
 * 过滤动态路由表
 * @param routes 动态路由
 * @param roles 角色权限
 */
export function filterAsyncRoutes(routes, roles) {
  const res = []
  routes.forEach(route => {
    const tmp = { // 拷贝一份路由对象
      ...route
    }

    if (tmp.children && tmp.children.length > 0) {
      tmp.children = filterAsyncRoutes(tmp.children, roles)
      if (tmp.children.length > 0) {  // 如果此菜单存在有权限的子菜单，则加入路由表
        res.push(tmp)
      }
    } else {
      // 没有子菜单，则判断用户是否对此菜单拥有权限
      if (hasPermission(roles, tmp)) {
        res.push(tmp)
      }
    }
  })
  return res
}

/**
 * 判断该角色是否有权限
 * @param { Object } roles 角色权限
 * @param { Object } route 路由
 */
function hasPermission(roles, route) {
  if (!route.name) return false // 如果路由不存在name，则直接返回无权限
  if (route.always) return true // 如果路由always为true，则返回有权限
  return roles.indexOf(route.name) > -1
}

/**
 * 筛选无需隐藏的路由
 */
function filterNotHiddenRoutes(routes) {
  let arr = []
  routes.forEach(route => {
    if (route.hidden) return    // 如果设置为隐藏，则直接返回
    let tmp = {
      ...route
    }
    if (tmp.children) {
      tmp.children = filterNotHiddenRoutes(route.children)
    }
    arr.push(tmp)
  })
  return arr
}

export default {
  namespaced: true,
  state: {
    sidebar: { // 布局页侧边栏状态
      opened: Cookies.get('sidebarStatus') ? !!+Cookies.get('sidebarStatus') : true,
      withoutAnimation: false
    },
    device: 'desktop', // 登录设备
    size: Cookies.get('size') || 'medium', // 尺寸
    routes: [], // 当前正在使用的路由表
    addRoutes: [], // 新增的路由表
    accessRoutes: []  // 可访问的路由
  },
  mutations: {
    /**
     * 侧边栏状态切换
     */
    TOGGLE_SIDEBAR: state => {
      state.sidebar.opened = !state.sidebar.opened
      state.sidebar.withoutAnimation = false
      if (state.sidebar.opened) {
        Cookies.set('sidebarStatus', 1)
      } else {
        Cookies.set('sidebarStatus', 0)
      }
    },
    /**
     * 关闭侧边栏
     */
    CLOSE_SIDEBAR: (state, withoutAnimation) => {
      Cookies.set('sidebarStatus', 0)
      state.sidebar.opened = false
      state.sidebar.withoutAnimation = withoutAnimation
    },
    /**
     * 更改登录设备
     */
    TOGGLE_DEVICE: (state, device) => {
      state.device = device
    },
    /**
     * 更改尺寸
     */
    SET_SIZE: (state, size) => {
      state.size = size
      Cookies.set('size', size)
    },
    /**
     * 设置系统路由表
     */
    SET_ROUTES: (state, routes) => {
      state.addRoutes = routes
      state.routes = [...constantRoutes, ...routes]
    },
    SET_ACCESS_ROUTES(state, routes) {
      state.accessRoutes = routes
    }
  },
  actions: {
    toggleSideBar({
      commit
    }) {
      commit('TOGGLE_SIDEBAR')
    },
    closeSideBar({
      commit
    }, {
      withoutAnimation
    }) {
      commit('CLOSE_SIDEBAR', withoutAnimation)
    },
    toggleDevice({
      commit
    }, device) {
      commit('TOGGLE_DEVICE', device)
    },
    setSize({
      commit
    }, size) {
      commit('SET_SIZE', size)
    },
    /**
     * 生成路由表
     * @param { Object } store
     * @param { Object} roles
     */
    generateRoutes({
      commit
    }, auth) {
      return new Promise(resolve => { // 此处根据角色信息筛选可用的路由表
        let accessedRoutes
        if (config.openRoleSystem && !auth.all) {  // roles.all：如果返回的角色信息all为true，则加载所有动态路由
          accessedRoutes = filterAsyncRoutes(asyncRoutes, auth.menus)
        } else {
          accessedRoutes = asyncRoutes
        }
        commit('SET_ROUTES', accessedRoutes)  // 设置有权限的路由
        commit('SET_ACCESS_ROUTES', [ ...filterNotHiddenRoutes(constantRoutes), ...filterNotHiddenRoutes(asyncRoutes) ])  // 设置所有可访问路由
        resolve(accessedRoutes)
      })
      // return new Promise(resolve => {
      //   let routes = asyncRoutes || []
      //   commit('SET_ROUTES', routes)
      //   resolve(routes)
      // })
    }
  }
}
