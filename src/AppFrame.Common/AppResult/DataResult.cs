using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppResult
{
    public class DataResult<T>
    {
        public DataResult()
        {

        }

        public DataResult(T data, bool statu = true)
        {
            Data = data;
            Statu = statu;
            Message = "返回结果成功";
        }

        public DataResult(string message = "返回结果失败", bool statu = false)
        {
            Statu = statu;
            Message = message;
        }

        public T Data { get; set; }
        public bool Statu { get; set; }
        public string Message { get; set; }
    }
    public class DataResult: DataResult<string>
    {

    }
}
