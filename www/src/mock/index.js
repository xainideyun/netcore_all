import Mock from 'mockjs'
// import { baseURL } from '../config'
import { login, getUserinfo, getRole } from './response/user'

function mock(pattern, method, func) {
    if (typeof method === 'function') {
        func = method
        method = 'get'
    }
    Mock.mock(pattern, method, func)
}

mock(/\/api\/token?/, login)
mock(/\/api\/user\/current/, getUserinfo)
mock(/\/api\/user\/role/, getRole)

export default Mock
