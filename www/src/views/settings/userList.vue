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
      <el-table-column label="性别" prop="gender">
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
  </div>
</template>

<script>
import { getList, getRoles } from '@/api/user'
export default {
  data() {
    return {
      tableData: []
    }
  },
  async created() {
    let { data } = await getList()
    this.tableData = data
  },
  methods: {
    bindRole($index, user) {
      console.log($index, user)
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
</style>