using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppFiles
{
    public static class CurrencyFileHelper
    {
        /// <summary>
        /// 判断文件夹是否存在，不存在则创建
        /// </summary>
        /// <param name="path"></param>
        public static void DirectoryExist(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath)) throw new Exception("文件夹路径不能为空！");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}
