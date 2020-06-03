import Layout from '@/views/layout'

/**
 * hidden：左侧菜单不显示
 * icon：左侧菜单的显示图标
 * affix：在选项卡中固定
 * always：是否总是有权限访问（不受权限设置影响）
 */

export const constantRoutes = [{
    path: '/redirect',  // 重定向路由，用于选项卡的刷新
    component: Layout,
    hidden: true,
    children: [{
      path: '/redirect/:path(.*)',
      component: () => import('@/views/redirect')
    }]
  },
  {
    path: '/',
    redirect: '/home',
    component: Layout,
    children: [{
      path: 'home',
      name: 'home',
      component: () => import(`@/views/home`),
      meta: {
        title: '首页',
        icon: 'Homehomepagemenu',   // 左侧菜单图标
        affix: true   // 固定在选项卡
      }
    }]
  },
  {
    path: '/about',
    component: Layout,
    children: [{
      path: 'index',
      name: 'about',
      component: () => import('@/views/about'),
      meta: {
        title: '关于我们'
      }
    }]
  },
  {
    path: '/login',
    name: 'login',
    component: () => import('@/views/login'),
    hidden: true,   // 左侧菜单不显示
    meta: {
      title: '登录'
    }
  },
  {
    path: '/404',
    component: () => import('@/views/error_404'),
    hidden: true,
    meta: {
      title: '未找到页面'
    }
  },
  {
    path: '/401',
    component: () => import('@/views/error_401'),
    hidden: true,
    meta: {
      title: '无权限'
    }
  }
]

export const asyncRoutes = [{
    path: '/good',
    name: 'good',
    component: Layout,
    redirect: '/good/list',
    always: true,
    meta: {
      title: '商品管理'
    },
    children: [{
        path: 'list',
        name: 'goodList',
        always: true,
        component: () => import('@/views/good/list'),
        meta: {
          title: '商品列表'
        }
      },
      {
        path: 'detail',
        name: 'goodDetail',
        component: () => import('@/views/good/detail'),
        always: true,
        // hidden: true,
        meta: {
          title: '商品详情'
        }
      }
    ]
  },
  {
    path: '/settings',
    name: 'settings',
    redirect: '/settings/user',
    component: Layout,
    meta: {
      title: '系统设置'
    },
    children: [
      {
        path: 'user',
        name: 'user',
        component: () => import('@/views/settings/user.vue'),
        meta: {
          title: '个人中心'
        }
      },
      {
        path: 'sys',
        name: 'sys',
        component: () => import('@/views/settings/sys.vue'),
        meta: {
          title: '配置'
        }
      },
      {
        path: 'role',
        name: 'role',
        component: () => import('@/views/settings/role.vue'),
        meta: {
          title: '角色列表'
        }
      },
      {
        path: 'userList',
        name: 'userList',
        component: () => import('@/views/settings/userList.vue'),
        meta: {
          title: '用户列表'
        }
      }
    ]
  },
  {
    path: '*',
    name: 'error_404',
    redirect: '/404',
    always: true,
    hidden: true,
    meta: {
    }
  }
]

export default constantRoutes
