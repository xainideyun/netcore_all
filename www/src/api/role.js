import axios from './index'

export function getRoles() {
    return axios.request({
        url: '/api/role',
        method: 'get'
    })
}

export function addRole(data) {
    return axios.request({
        url: '/api/role',
        method: 'post',
        data
    })
}

export function updateRole(id, data) {
    return axios.request({
        url: `/api/role/${id}`,
        method: 'put',
        data
    })
}

export function deleteRole(id) {
    axios.request({
        url: `/api/role/${id}`,
        method: 'delete'
    })
}

/**
 * 用户绑定角色
 * @param { Array } arr 角色数组
 */
export function bindRoles(arr) {
    return axios.request({
        url: '/api/role/bind',
        method: 'post',
        data: arr
    })
}
