<!-- 系统角色页面 -->
<template>
  <div class="app-container">
    <el-button type="primary" @click="handleAddRole">新增角色</el-button>

    <el-table :data="rolesList" style="width: 100%;margin-top:30px;" border>
      <el-table-column align="center" label="角色编码" width="220">
        <template slot-scope="scope">{{ scope.row.key }}</template>
      </el-table-column>
      <el-table-column align="center" label="角色名称" width="220">
        <template slot-scope="scope">{{ scope.row.name }}</template>
      </el-table-column>
      <el-table-column align="header-center" label="描述">
        <template slot-scope="scope">{{ scope.row.description }}</template>
      </el-table-column>
      <el-table-column align="center" label="操作">
        <template slot-scope="scope">
          <el-button type="primary" size="small" @click="handleEdit(scope)">编辑</el-button>
          <el-button type="danger" size="small" @click="handleDelete(scope)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog :visible.sync="dialogVisible" :title="dialogType==='edit'?'编辑角色':'新增角色'">
      <el-form :model="role" label-width="80px" label-position="left">
        <el-form-item label="编码">
          <el-input v-model.trim="role.key" :disabled="dialogType === 'edit'" placeholder="角色编码" />
        </el-form-item>
        <el-form-item label="名称">
          <el-input v-model.trim="role.name" placeholder="角色名称" />
        </el-form-item>
        <el-form-item label="描述">
          <el-input
            v-model.trim="role.description"
            :autosize="{ minRows: 2, maxRows: 4}"
            type="textarea"
            placeholder="角色描述"
          />
        </el-form-item>
        <el-form-item label="功能">
          <el-tree
            ref="tree"
            :check-strictly="checkStrictly"
            :data="routesData"
            :props="defaultProps"
            show-checkbox
            node-key="name"
            default-expand-all
            check-on-click-node
            :expand-on-click-node="false"
            class="permission-tree"
          />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="confirmRole">确定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import path from 'path'
import { deepClone } from '@/lib/util'
import { getRoles, addRole, updateRole, deleteRole } from '@/api/role'
import { mapState } from 'vuex'
const defaultRole = {
  key: '',
  name: '',
  description: '',
  routes: []
}

export default {
  name: 'role',
  data() {
    //这里存放数据
    return {
      role: Object.assign({}, defaultRole), // 新增/编辑角色实体对象
      rolesList: [], // 所有角色列表
      routesData: [], // 所有可见的路由
      dialogVisible: false,
      checkStrictly: false,
      dialogType: 'new', // 标识新增还是编辑
      defaultProps: {
        children: 'children', // 树形结构中标识子对象的属性
        label: 'title' // 树形结构中显示名称的属性
      }
    }
  },
  computed: {
    ...mapState('app', ['accessRoutes']) // 可访问的路由
  },
  async created() {
    let { data } = await getRoles()
    this.rolesList = data
    this.routesData = this.generateRoutes(this.accessRoutes)
  },
  methods: {
    handleAddRole() {
      this.role = Object.assign({}, defaultRole)
      if (this.$refs.tree) {
        this.$refs.tree.setCheckedNodes([])
      }
      this.dialogType = 'new'
      this.dialogVisible = true
    },
    handleEdit(scope) {
      this.dialogType = 'edit'
      this.dialogVisible = true
      this.checkStrictly = true
      this.role = deepClone(scope.row)
      this.$nextTick(() => {
        this.$refs.tree.setCheckedKeys(
          this.role.routes.map(route => route.name)
        )
        this.checkStrictly = false
      })
    },
    handleDelete({ $index, row }) {
      this.$confirm(`确定删除角色【${row.name}】吗?`, '警告', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        await deleteRole(row.id)
        this.rolesList.splice($index, 1)
        this.$message({
          type: 'success',
          message: '删除成功!'
        })
      })
    },
    async confirmRole() {
      const isEdit = this.dialogType === 'edit'
      const checkedKeys = this.$refs.tree.getCheckedKeys()

      if (!this.role.key) {
        this.$message.error('请输入角色编码')
        return
      }
      if (!this.role.name) {
        this.$message.error('请输入角色名称')
        return
      }
      if (
        this.rolesList.some(
          role =>
            role.id !== this.role.id &&
            (role.key === this.role.key || role.name === this.role.name)
        )
      ) {
        this.$message.error('已存在相同名称的角色')
        return
      }
      const loading = this.$$loading()
      this.dialogVisible = false
      this.role.routes = checkedKeys.map(name => ({ name }))
      if (isEdit) {
        let { data } = await updateRole(this.role.id, this.role)
        this.rolesList.some((role, index) => {
          if (role.key !== data.key) return false
          this.rolesList.splice(index, 1, data)
          return true
        })
      } else {
        let { data } = await addRole(this.role)
        this.rolesList.push(data)
      }
      loading.close()

      const { description, key, name } = this.role
      this.$notify({
        title: '操作成功',
        dangerouslyUseHTMLString: true,
        message: `
            <div>角色编码: ${key}</div>
            <div>角色名称: ${name}</div>
            <div>角色描述: ${description}</div>
        `,
        type: 'success'
      })
    },
    /**
     * 生成树形结构可读取的数据结构
     */
    generateRoutes(routes) {
      const res = []

      for (let route of routes) {
        const onlyOneShowingChild = this.onlyOneShowingChild(
          route.children,
          route
        )

        if (route.children && onlyOneShowingChild && !route.always) {
          route = onlyOneShowingChild
        }

        const data = {
          name: route.name,
          title: route.meta && route.meta.title
        }

        if (route.children) {
          data.children = this.generateRoutes(route.children, data.path)
        }
        res.push(data)
      }
      return res
    },
    onlyOneShowingChild(children = [], parent) {
      let onlyOneChild = null
      const showingChildren = children.filter(item => !item.hidden)
      if (showingChildren.length === 1) {
        onlyOneChild = showingChildren[0]
        onlyOneChild.path = path.resolve(parent.path, onlyOneChild.path)
        return onlyOneChild
      }

      if (showingChildren.length === 0) {
        onlyOneChild = { ...parent, path: '', noShowingChildren: true }
        return onlyOneChild
      }

      return false
    }
  }
}
</script>
<style lang="scss" scoped>
.app-container {
  .roles-table {
    margin-top: 30px;
  }
  .permission-tree {
    margin-bottom: 30px;
  }
}
</style>