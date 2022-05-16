using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LocalCapture.Core
{
    internal static class Serialize
    {
        public static void SaveFile()
        {
            try
            {
                BinaryFormatter b = new();
                using FileStream fs = new("./CaptureHistories.sv", FileMode.Create);
#pragma warning disable SYSLIB0011 // 类型或成员已过时
                b.Serialize(fs, Program.CaptureHistories);
#pragma warning restore SYSLIB0011 // 类型或成员已过时
            }
            catch (Exception)
            {

            }
        }

        public static void LoadFile()
        {
            try
            {
                BinaryFormatter b = new();
                using FileStream fs = new("./CaptureHistories.sv", FileMode.Open);
#pragma warning disable SYSLIB0011 // 类型或成员已过时
                Program.CaptureHistories = (Dictionary<string, CaptureHistory>)b.Deserialize(fs);
#pragma warning restore SYSLIB0011 // 类型或成员已过时
                foreach (var dic in Program.CaptureHistories)
                {
#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                    new PinForm((Point)dic.Value.Size, dic.Value.Bitmap, dic.Key);
#pragma warning restore CS8604 // 引用类型参数可能为 null。
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
