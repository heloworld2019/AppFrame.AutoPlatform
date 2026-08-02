using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppAttribute.Validate
{
    public class AppValueRangeAttribute : ValidateBaseAttribute
    {
        private readonly ValidateType _validateType = ValidateType.None;
        public float MinFloat { get; private set; }
        public float MaxFloat { get; private set; }
        public double MinDouble { get; private set; }
        public double MaxDouble { get; private set; }
        public short MinShort { get; private set; }
        public short MaxShort { get; private set; }
        public ushort MinUShort { get; private set; }
        public ushort MaxUShort { get; private set; }
        public int MinInt { get; private set; }
        public int MaxInt { get; private set; }
        public uint MinUInt { get; private set; }
        public uint MaxUInt { get; private set; }
        public long MinLong { get; private set; }
        public long MaxLong { get; private set; }
        public ulong MinULong { get; private set; }
        public ulong MaxULong { get; private set; }

        public AppValueRangeAttribute(double minValue, double maxValue)
        {
            _validateType = ValidateType.Double;
            MinDouble = minValue;
            MaxDouble = maxValue;
            ErrorInfo = $"值不在{minValue}~{maxValue}之间";
        }
        public AppValueRangeAttribute(float minValue, float maxValue)
        {
            _validateType = ValidateType.Float;
            MinFloat = minValue;
            MaxFloat = maxValue;
            ErrorInfo = $"值不在{minValue}~{maxValue}之间";
        }
        public AppValueRangeAttribute(ushort minValue, ushort maxValue)
        {
            _validateType = ValidateType.UShort;
            MinUShort = minValue;
            MaxUShort = maxValue;
            ErrorInfo = $"值不在{minValue}~{maxValue}之间";
        }
        public AppValueRangeAttribute(short minValue, short maxValue)
        {
            _validateType = ValidateType.Short;
            MinShort = minValue;
            MaxShort = maxValue;
            ErrorInfo = $"值不在{minValue}~{maxValue}之间";
        }
        public AppValueRangeAttribute(int minValue, int maxValue)
        {
            _validateType = ValidateType.Int;
            MinInt = minValue;
            MaxInt = maxValue;
            ErrorInfo = $"值不在{minValue}~{maxValue}之间";
        }
        public AppValueRangeAttribute(uint minValue, uint maxValue)
        {
            _validateType = ValidateType.UInt;
            MinUInt = minValue;
            MaxUInt = maxValue;
            ErrorInfo = $"值不在{minValue}~{maxValue}之间";
        }
        public AppValueRangeAttribute(long minValue, long maxValue)
        {
            _validateType = ValidateType.Long;
            MinLong = minValue;
            MaxLong = maxValue;
            ErrorInfo = $"值不在{minValue}~{maxValue}之间";
        }
        public AppValueRangeAttribute(ulong minValue, ulong maxValue)
        {
            _validateType = ValidateType.Long;
            MinULong = minValue;
            MaxULong = maxValue;
            ErrorInfo = $"值不在{minValue}~{maxValue}之间";
        }
        public override bool Validate(object value)
        {
            switch (_validateType)
            {
                case ValidateType.None:
                    return false;
                case ValidateType.Float:
                    return (ushort)value >= MinFloat && (ushort)value <= MaxFloat;
                case ValidateType.Double:
                    return (double)value >= MinDouble && (double)value <= MaxDouble;
                case ValidateType.Short:
                    return (ushort)value >= MinShort && (ushort)value <= MaxShort;
                case ValidateType.UShort:
                    return (ushort)value >= MinUShort && (ushort)value <= MaxUShort;
                case ValidateType.Int:
                    return (ushort)value >= MinInt && (ushort)value <= MaxInt;
                case ValidateType.UInt:
                    return (ushort)value >= MinUInt && (ushort)value <= MaxUInt;
                case ValidateType.Long:
                    return (ushort)value >= MinLong && (ushort)value <= MaxLong;
                case ValidateType.ULong:
                    return (ushort)value >= MinULong && (ushort)value <= MaxULong;
                default:
                    return false;
            }
        }

        private enum ValidateType
        {
            None,
            Float,
            Double,
            Short,
            UShort,
            Int,
            UInt,
            Long,
            ULong,
        }
    }
}
