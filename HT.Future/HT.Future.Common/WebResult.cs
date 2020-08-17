using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Common
{
    /// <summary>
    /// api调用返回结果
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class WebResult<TResult>
    {
        /// <summary>
        /// 结果码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 结果数据
        /// </summary>
        public TResult Result { get; set; }
    }
}
