import axios from './index'

export function login(data) {
  return axios.request({
    url: '/api/token',
    method: 'get',
    params: data
  })
}

export const getUserinfo = () => {
  return axios.request({
    url: '/api/user/current'
  })
}

export function getRole() {
  return axios.request({
    url: '/api/role'
  })
}
