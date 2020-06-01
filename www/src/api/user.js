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
    url: '/api/user/role'
  })
}

export function getList() {
  return axios.request({
    url: '/api/user'
  })
}

export function getRoles() {
  return axios.request({
    url: '/api/user/role'
  })
}