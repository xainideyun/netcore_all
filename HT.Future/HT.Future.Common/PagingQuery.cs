﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Common
{
    /// <summary>
    /// 分页类型
    /// </summary>
    public class PagingQuery
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public double PageCount
        {
            get
            {
                return Math.Ceiling(Convert.ToDouble(RecordCount) / PageSize);
            }
        }
        /// <summary>
        /// 跳过的记录数
        /// </summary>
        public int Skip
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
    }
}
