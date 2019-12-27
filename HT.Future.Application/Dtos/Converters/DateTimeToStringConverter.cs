using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Application
{
    /// <summary>
    /// 映射时间转化为字符串
    /// </summary>
    public class DateTimeToStringConverter : IValueConverter<DateTime, string>
    {
        public static DateTimeToStringConverter Instance { get; }
        static DateTimeToStringConverter()
        {
            Instance = new DateTimeToStringConverter();
        }
        public string Convert(DateTime sourceMember, ResolutionContext context)
            => sourceMember.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
