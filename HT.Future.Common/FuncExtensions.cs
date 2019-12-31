using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HT.Future.Common
{
    public static class FuncExtensions
    {


        /// <summary>
        /// 获取枚举类型的所有项列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        /// <summary>
        /// 获取枚举类型的显示值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        /// <summary>
        /// 获取枚举类型的具体值与显示值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
        }


        /// <summary>
        /// 动态加载所有继承于指定基类的类型
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="baseType"></param>
        /// <param name="assemblies"></param>
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (Type type in types)
                modelBuilder.Entity(type);
        }
        /// <summary>
        /// 通过反射动态加载所有的IEntityTypeConfiguration配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assemblies"></param>
        public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic);

            foreach (Type type in types)
            {
                foreach (Type iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        MethodInfo applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
                    }
                }
            }
        }

        /// <summary>
        /// 获取byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.Default;
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// 将二进制数组转化为字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToStr(this byte[] buffer, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.Default;
            return encoding.GetString(buffer);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string RandomNumber(int len = 6)
        {
            var arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var num = new StringBuilder();
            var rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < len; i++)
            {
                num.Append(arrChar[rnd.Next(0, 9)].ToString());
            }
            return num.ToString();
        }

        /// <summary>
        /// 将对象转化为json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将json字符串转化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

        /// <summary>
        /// 遍历列表，执行函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> predicate)
        {
            foreach (var item in list)
            {
                predicate(item);
            }
            return list;
        }

        /// <summary>
        /// 遍历列表，执行函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> predicate)
        {
            foreach (var item in list)
            {
                await predicate(item);
            }
            return list;
        }

        /// <summary>
        /// 将字符串转化为MD5码
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="encode">编码方式</param>
        /// <returns></returns>
        public static string ToMd5(this string input, Encoding encode = null)
        {
            if (encode == null) encode = Encoding.UTF8;
            var md5 = new MD5CryptoServiceProvider();
            var bytResult = md5.ComputeHash(encode.GetBytes(input));
            var strResult = BitConverter.ToString(bytResult);
            strResult = strResult.Replace("-", "");
            return strResult.ToLower();
        }

        /// <summary>
        /// 将时间转化为时间戳（毫秒数）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            var ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

    }




    /// <summary>
    /// 显示属性
    /// </summary>
    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }

}
