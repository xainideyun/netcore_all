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
        method:'delete'
    })
}
