import axios from './index'

/**
 * 用户登录
 * @param { object } data 登录信息
 */
export function login(data) {
  return axios.request({
    url: '/api/token',
    method: 'get',
    params: data
  })
}

/**
 * 获取登录用户的个人信息
 */
export const getUserinfo = () => {
  return axios.request({
    url: '/api/user/current'
  })
}

/**
 * 获取用户绑定的角色
 */
export function getRole(id) {
  return axios.request({
    url: `/api/user/role/${id || 0}`
  })
}

/**
 * 获取用户列表（未做分页）
 */
export function getList() {
  return axios.request({
    url: '/api/user'
  })
}

/**
 * 用户绑定角色
 * @param { Array } arr 角色数组
 */
export function bindRoles(arr) {
  return axios.request({
    url: '/api/user/bindRoles',
    method: 'post',
    data: arr
  })
}

/**
 * 获取用户功能菜单
 */
export function getMenus() {
  return axios.request({
    url: `/api/user/menus`
  })
}
