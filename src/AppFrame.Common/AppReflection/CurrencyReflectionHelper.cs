using AppFrame.Common.AppFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppReflection  
{
    public static class CurrencyReflectionHelper  // 插件框架，动态外部 dll 模块加载，不需要硬引用程序集；适合插件式工控框架。
    {
        /// <summary>
        /// 加载目录下实现指定接口的dll
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<T> LoadDll<T>(string folderPath)
        {
            CurrencyFileHelper.DirectoryExist(folderPath);
            string[] files = Directory.GetFiles(folderPath);
            if (files?.Length <= 0) return null;
            foreach (var file in files)
            {
                if (!file.ToUpper().EndsWith(".DLL")) continue;
                try
                {
                    var interfaces = new List<T>();
                    Assembly ab = Assembly.LoadFrom(file); //加载程序集
                    Type[] types = ab.GetTypes();
                    foreach (Type t in types)
                    {
                        if (t.GetInterface(typeof(T).Name) != null)
                        {
                            interfaces.Add((T)ab.CreateInstance(t.FullName));
                        }
                    }
                    return interfaces;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }
    }
}
