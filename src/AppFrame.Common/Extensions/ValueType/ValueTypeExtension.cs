using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.Extensions.ValueType
{
    public static class ValueTypeExtension
    {
        /// <summary>
        /// 获取uint数据的某一位
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetBitValue(this uint value, ushort index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return (value & val) == val;
        }
        /// <summary>
        /// 获取int数据的某一位
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetBitValue(this int value, ushort index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return (value & val) == val;
        }
        /// <summary>
        /// 设置uint数据的某一位
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static uint SetBitValue(this uint value, ushort index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return (uint)(bitValue ? (value | val) : (value & ~val));
        }
        /// <summary>
        /// 设置int数据的某一位
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int SetBitValue(this int value, ushort index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return (bitValue ? (value | val) : (value & ~val));
        }
    }
}
