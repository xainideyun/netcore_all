import variables from '@/styles/element-variables.scss'

const state = {
  theme: variables.theme,
  showSettings: true,
  tagsView: true,
  fixedHeader: true,
  sidebarLogo: true
}

const mutations = {
  CHANGE_SETTING: (state, {
    key,
    value
  }) => {
    if (state.hasOwnProperty(key)) {
      state[key] = value
    }
  }
}

const actions = {
  changeSetting({
    commit
  }, data) {
    commit('CHANGE_SETTING', data)
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
