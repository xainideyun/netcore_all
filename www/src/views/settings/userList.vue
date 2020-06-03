<!--  -->
<template>
  <div class="container">
    <el-table :data="tableData" border style="width: 100%">
      <el-table-column type="index" label="#" align="center"></el-table-column>
      <el-table-column type="expand">
        <template slot-scope="props">
          <el-form label-position="left" inline class="demo-table-expand">
            <el-form-item label="姓名：">
              <span>{{ props.row.name }}</span>
            </el-form-item>
            <el-form-item label="性别：">
              <span>{{ props.row.gender }}</span>
            </el-form-item>
            <el-form-item label="登录名称：">
              <span>{{ props.row.userName }}</span>
            </el-form-item>
            <el-form-item label="手机号码：">
              <span>{{ props.row.phone }}</span>
            </el-form-item>
            <el-form-item label="年龄：">
              <span>{{ props.row.age }}</span>
            </el-form-item>
            <el-form-item label="邮箱：">
              <span>{{ props.row.email }}</span>
            </el-form-item>
            <el-form-item label="最后登录时间：">
              <span>{{ props.row.lastLoginTime }}</span>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>
      <el-table-column label="姓名" prop="name"></el-table-column>
      <el-table-column label="登陆名称" prop="userName"></el-table-column>
      <el-table-column label="性别" prop="gender" width="80" align="center">
        <template slot-scope="scope">
          <el-tag size="medium">{{ scope.row.gender }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column fixed="right" label="操作" width="120">
        <template slot-scope="scope">
          <el-button type="text" size="small">编辑</el-button>
          <el-button
            @click.native.prevent="bindRole(scope.$index, scope.row)"
            type="text"
            size="small"
          >绑定角色</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog :visible.sync="roleDialogVisible" title="绑定角色" :close-on-click-modal="false">
      <el-form label-width="100px" label-position="left">
        <el-form-item label="已绑定角色">
          <el-tag
            v-for="tag in userRoles"
            :key="tag.id"
            closable
            effect="light"
            type="success"
            @close="removeRole(tag)"
          >{{tag.name}}</el-tag>
        </el-form-item>
        <hr style="opacity: .3;" />
        <el-form-item label="系统角色">
          <el-tag
            v-for="tag in remaindRoles"
            @click="addRole(tag)"
            :key="tag.name"
            effect="plain"
          >{{tag.name}}</el-tag>
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="roleDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="confirmRole">确定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getList, getRole } from '@/api/user'
import { getRoles, bindRoles } from '@/api/role'
import { deepClone, getArrIndex } from '@/lib/util'
export default {
  data() {
    return {
      tableData: [],
      roleDialogVisible: false,
      selectedUser: undefined,
      userRoles: [], // 用户已绑定角色
      sysRoles: undefined // 系统角色
    }
  },
  async created() {
    let { data } = await getList()
    this.tableData = data
  },
  computed: {
    /**
     * 系统角色中移除已绑定角色
     */
    remaindRoles() {
      if (!this.sysRoles) return []
      return this.sysRoles.filter(
        a => this.userRoles.filter(b => b.id === a.id).length === 0
      )
    }
  },
  methods: {
    /**
     * 为用户绑定角色
     */
    async bindRole($index, user) {
      if (!this.sysRoles) {
        await this.getSysRoles()
      }
      if (!user.roles) {
        let { data } = await getRole(user.user_id)
        user.roles = data
      }
      this.userRoles = deepClone(user.roles)
      this.selectedUser = user
      this.roleDialogVisible = true
    },
    removeRole(role) {
      let index = getArrIndex(this.userRoles, role)
      this.userRoles.splice(index, 1)
    },
    addRole(role) {
      this.userRoles.push(deepClone(role))
    },
    async confirmRole() {
      let ids = this.userRoles.map(a => ({
        roleId: a.id,
        userId: this.selectedUser.user_id
      }))
      await bindRoles(ids)
      this.selectedUser.roles = deepClone(this.userRoles)
      this.roleDialogVisible = false
      let str = this.userRoles.map(a => a.name).join(',')
      this.$notify({
        title: '绑定成功',
        dangerouslyUseHTMLString: true,
        message: `
            <div>已绑定角色: ${str}</div>
        `,
        type: 'success'
      })
    },
    /**
     * 获取系统角色
     */
    async getSysRoles() {
      let { data } = await getRoles()
      this.sysRoles = data || []
    }
  }
}
</script>
<style lang='scss' scoped>
.container {
  padding: 20px;
}
.demo-table-expand {
  font-size: 0;
}
.demo-table-expand label {
  width: 90px;
  color: #99a9bf;
}
.demo-table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 50%;
}
.el-form-item {
  text-align: left;
}
.el-tag {
  margin-right: 10px;
  cursor: pointer;
}
.el-tag--plain:hover {
  background-color: #a3d3ff;
  color: #fff;
}
</style>