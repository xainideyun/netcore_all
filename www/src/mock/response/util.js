import Mock from 'mockjs'
export function mock(data) {
    return Mock.mock({
        isSuccess: true,
        message: '成功',
        statusCode: 200,
        data
    })
}