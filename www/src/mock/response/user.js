import { mock } from './util'

export function login() {
  return mock({
    token: 'token'
  })
}

export const getUserinfo = () => {
  return mock({
    name: '@name',
    'age|18-60': 0,
    email: '@email'
  })
}

export function getRole() {
  return mock({
    'home': true,
    'about|1': true,
    'good|1': true,
    'goodList|1': true,
    'goodDetail|1': true,
    'settings|1': true,
    'sys|1': true,
    'user|1': true,
    'all|1': true
  })
}
