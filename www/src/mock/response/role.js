import { mock } from './util'

export function getMenus() {
  return mock([
    { id: 1, name: 'good', title: '商品管理', parentId: null },
    { id: 2, name: 'settings', title: '系统设置', parentId: null },
    { id: 3, name: 'goodList', title: '商品列表', parentId: 1 },
    { id: 4, name: 'user', title: '个人中心', parentId: 2 },
    { id: 5, name: 'order', title: '订单管理', parentId: null },
    { id: 6, name: 'sys', title: '配置', parentId: 2 },
    { id: 7, name: 'myOrder', title: '我的订单', parentId: 5 }
  ])
}

export function getRoles() {
  return mock({
    
  })
}

