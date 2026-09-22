using AppFrame.Common.AppAttribute.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.Dto
{
    public class AppSolenoidDto
    {
        public int Index { get; set; }                          //  序号，界面列表显示排序 ID
        public string ParameterName { get; set; }               //	参数唯一名称（程序内标识）
        public string ParameterDescription { get; set; }        //	中文描述，界面显示备注
        public bool Enable { get; set; }                        //	是否启用该气缸 / 电磁阀
        public string SolenoidName { get; set; }                //  气缸名称（界面展示名：移栽气缸、夹紧气缸等）
        public int GoOutputId { get; set; }                     //	前进 / 伸出输出点位 ID（PLC 输出地址）
        public int BackOutputId { get; set; }                   //	后退 / 缩回输出点位 ID
        public int GoSensorInputId { get; set; }                //	伸出到位传感器输入点位
        public int BackSensorInputId { get; set; }              //  缩回到位传感器输入点位
        public int GoAlarmId { get; set; }                      //	伸出超时报警代码 / 报警点位
        public int BackAlarmId { get; set; }                    //	缩回超时报警代码 / 报警点位
        public bool IsVacuum { get; set; }                      //  是否真空吸盘（区别普通气缸逻辑）
        public bool IsDoubleCoil { get; set; }                  //  是否双线圈电磁阀（自锁电磁阀，伸出 / 缩回各自线圈，保持状态不需要持续输出）
        public bool GoSensorShield { get; set; }                //  屏蔽伸出到位传感器（不检测到位信号，只靠时间判定完成）
        public bool BackSensorShield { get; set; }              //  屏蔽缩回到位传感器
        public double GoAlarmTime { get; set; }                 //  伸出动作最大超时时间，超时触发报警
        public double BackAlarmTime { get; set; }               //  缩回动作最大超时时间
        public double GoDoneDelay { get; set; }                 //  伸出到位后延时（到位信号满足后等待多久判定动作完成）
        public double BackDoneDelay { get; set; }               //  缩回到位后延时
        public double ActionTimeIfGoSensorShield { get; set; }  //  伸出传感器屏蔽时，固定执行时长（无传感器，单纯计时完成）
        public double ActionTimeIfBackSensorShield { get; set; } // 缩回传感器屏蔽时，固定执行时长
    }
}
