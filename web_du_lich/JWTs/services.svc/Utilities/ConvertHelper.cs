using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

using System.Xml;

namespace services.svc.Utilities
{
    public class ConvertHelper
    {

        public static List<U> ConvertList<U, T>(List<T> listObject)
        {
            bool flag = listObject == null;
            List<U> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                List<U> list = new List<U>();
                foreach (T current in listObject)
                {
                    bool flag2 = current != null;
                    if (flag2)
                    {
                        U defaultValue = default(U);
                        U item = ConvertObject<U>(current, defaultValue);
                        list.Add(item);
                    }
                }
                result = list;
            }
            return result;
        }
        public static T ConvertObject<T>(object t, T defaultValue)
        {
            T t2 = default(T);
            T result;
            try
            {
                t2 = (T)t;
            }
            catch (InvalidCastException)
            {
                result = defaultValue;
                return result;
            }
            catch (ArgumentNullException)
            {
                result = defaultValue;
                return result;
            }
            result = t2;
            return result;
        }
        public static bool ToBoolean(object obj)
        {
            bool flag = obj == null;
            bool flag2;
            return !flag && (bool.TryParse(obj.ToString(), out flag2) & flag2);
        }
        //public static byte ToByte(object obj)
        //{
        //    return ToByte(obj, 255);
        //}
        //public static byte ToByte(object obj, byte defaultValue)
        //{
        //    byte b;
        //    bool flag = obj != null && byte.TryParse(obj.ToString(), out b);
        //    byte result;
        //    if (flag)
        //    {
        //        result = b;
        //    }
        //    else
        //    {
        //        result = defaultValue;
        //    }
        //    return result;
        //}
        public static DateTime ToDateTime(object obj)
        {
            return ToDateTime(obj, DateTime.Now);
        }
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            bool flag = obj == null;
            DateTime result;
            if (flag)
            {
                result = defaultValue;
            }
            else
            {
                DateTime dateTime;//biến truyền vào ref thì phải đc khởi tạo giá trị trc khi truyền vào,out thì k cần khởi tạo giá trị ban đầu nhưng phải đc khởi tạo ngay bên trong lời gọi hàm.
                bool flag2 = !DateTime.TryParse(obj.ToString(), out dateTime);//kết thúc lời gọi hàm giá trị dateTime có thể bị thay đổi nhờ từ khóa out
                if (flag2)
                {
                    dateTime = defaultValue;
                }
                result = dateTime;
            }
            return result;
        }
        public static DateTime ToDateTimeExact(object obj, string pattern)
        {
            bool flag = string.IsNullOrEmpty(pattern);
            if (flag)
            {
                pattern = "dd/MM/yyyy";
            }
            bool flag2 = obj == null;
            DateTime result;
            if (flag2)
            {
                result = DateTime.MinValue;
            }
            else
            {
                DateTime now;
                bool flag3 = !DateTime.TryParseExact(obj.ToString(), pattern, null, DateTimeStyles.None, out now);//IFormatProvider
                if (flag3)
                {
                    now = DateTime.Now;
                    bool flag4 = now == new DateTime(1, 1, 1);
                    if (flag4)
                    {
                        result = DateTime.MinValue;
                        return result;
                    }
                }
                result = now;
            }
            return result;
        }
        //public static decimal ToDecimal(object obj)
        //{
        //    return ToDecimal(obj, decimal.Zero);
        //}
        public static decimal ToDecimal(object obj, decimal defaultvalue)
        {
            decimal num = 0;
            bool flag = obj != null && decimal.TryParse(obj.ToString(), out num);
            decimal result;
            if (flag)
            {
                result = num;
            }
            else
            {
                result = defaultvalue;
            }
            return flag ? result : num;
        }
        //public static double ToDouble(object obj)
        //{
        //    return ToDouble(obj, 0.0);
        //}
        //public static double ToDouble(object obj, double defaultValue)
        //{
        //    double num;
        //    bool flag = obj != null && double.TryParse(obj.ToString(), out num);
        //    double result;
        //    if (flag)
        //    {
        //        result = num;
        //    }
        //    else
        //    {
        //        result = defaultValue;
        //    }
        //    return result;
        //}
        public static Guid ToGuid(object obj, Guid defaultValue) // use GUID for unique identifier
        {
            bool flag = obj == null;
            Guid result;
            if (flag)
            {
                result = defaultValue;
            }
            else
            {
                Guid guid = defaultValue;
                try
                {
                    guid = ToGuid(obj);
                }
                catch (Exception)
                {
                    guid = defaultValue;
                }
                result = guid;
            }
            return result;
        }
        //public static short ToInt16(object obj)
        //{
        //    return ToInt16(obj, 0);
        //}
        public static short ToInt16(object obj, short defaultValue)
        {
            short num = 0;
            bool flag = obj != null && short.TryParse(obj.ToString(), out num);
            short result;
            if (flag)
            {
                result = num;
            }
            else
            {
                result = defaultValue;
            }
            return result;
        }
        public static int ToInt32(object obj)
        {
            return ToInt32(obj, 0);
        }
        public static int ToInt32(object obj, int defaultValue)
        {
            bool flag = obj == null;
            int result;
            if (flag)
            {
                result = 0;
            }
            else
            {
                int num;
                bool flag2 = int.TryParse(obj.ToString(), out num);
                if (flag2)
                {
                    result = num;
                }
                else
                {
                    result = defaultValue;
                }
            }
            return result;
        }
        public static int ToInteger(object obj)
        {
            return ToInt32(obj);
        }
        //public static long ToInt64(object obj)
        //{
        //    return ToInt64(obj, 0L);
        //}
        public static long ToInt64(object obj, long defaultValue)
        {
            long num = 0;
            bool flag = obj != null && long.TryParse(obj.ToString(), out num);
            long result;
            if (flag)
            {
                result = num;
            }
            else
            {
                result = defaultValue;
            }
            return result;
        }
        public static string ToString(object obj)
        {
            return ToString(obj, string.Empty);
        }
        public static string ToString(object obj, string defaultValue)
        {
            bool flag = obj == null;
            string result;
            if (flag)
            {
                result = defaultValue;
            }
            else
            {
                string text = obj.ToString();
                bool flag2 = string.IsNullOrEmpty(text);
                if (flag2)
                {
                    text = defaultValue;
                }
                result = text;
            }
            return result;
        }
        public static string ToDateString(DateTime dt)
        {
            bool flag = dt == DateTime.MinValue;
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                result = dt.ToString("dd/MM/yyyy");
            }
            return result;
        }
        public static string ToDateStringPattern(DateTime? dt, string pattern)
        {
            DateTime d = dt.HasValue ? ToDateTime(dt) : DateTime.MinValue;
            bool flag = d == DateTime.MinValue;
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                pattern = (string.IsNullOrEmpty(pattern) ? "dd/MM/yyyy" : pattern);
                result = d.ToString(pattern);
            }
            return result;
        }
        public static string ToTimeString(DateTime dt)
        {
            bool flag = dt == DateTime.MinValue;
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                result = dt.ToShortTimeString();
            }
            return result;
        }
        public static Guid ToGuid(object obj)
        {
            bool flag = obj == null || obj == DBNull.Value;
            Guid result;
            if (flag)
            {
                result = Guid.Empty;
            }
            else
            {
                bool flag2 = obj.GetType() == Type.GetType("System.Guid");
                if (flag2)
                {
                    result = (Guid)obj;
                }
                else
                {
                    string text = obj.ToString();
                    bool flag3 = text == string.Empty;
                    if (flag3)
                    {
                        result = Guid.Empty;
                    }
                    else
                    {
                        result = XmlConvert.ToGuid(text);
                    }
                }
            }
            return result;
        }
        public static bool IsEmptyGuid(Guid g)
        {
            return g == Guid.Empty;
        }
        public static bool IsEmptyGuid(object obj)
        {
            bool flag = obj == null || obj == DBNull.Value;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                string text = obj.ToString();
                bool flag2 = text == string.Empty;
                if (flag2)
                {
                    result = true;
                }
                else
                {
                    Guid a = XmlConvert.ToGuid(text);
                    bool flag3 = a == Guid.Empty;
                    result = flag3;
                }
            }
            return result;
        }
        public static byte[] ToBinary(object obj)
        {
            bool flag = obj == null || obj == DBNull.Value;
            byte[] result;
            if (flag)
            {
                result = new byte[0];
            }
            else
            {
                result = (byte[])obj;
            }
            return result;
        }
        public static object ToDBGuid(Guid g)
        {
            bool flag = g == Guid.Empty;
            object result;
            if (flag)
            {
                result = DBNull.Value;
            }
            else
            {
                result = g;
            }
            return result;
        }
        public static object ToDBGuid(object obj)
        {
            bool flag = obj == null || obj == DBNull.Value;
            object result;
            if (flag)
            {
                result = DBNull.Value;
            }
            else
            {
                bool flag2 = obj.GetType() == Type.GetType("System.Guid");
                if (flag2)
                {
                    result = obj;
                }
                else
                {
                    string text = obj.ToString();
                    bool flag3 = text == string.Empty;
                    if (flag3)
                    {
                        result = DBNull.Value;
                    }
                    else
                    {
                        Guid guid = XmlConvert.ToGuid(text);
                        bool flag4 = guid == Guid.Empty;
                        if (flag4)
                        {
                            result = DBNull.Value;
                        }
                        else
                        {
                            result = guid;
                        }
                    }
                }
            }
            return result;
        }
        public static object ToDBDateTime(DateTime dt)
        {
            bool flag = dt == DateTime.MinValue;
            object result;
            if (flag)
            {
                result = DBNull.Value;
            }
            else
            {
                bool flag2 = dt.Year < 1753;
                if (flag2)
                {
                    result = DBNull.Value;
                }
                else
                {
                    result = dt;
                }
            }
            return result;
        }
        
        public static object ToDBInteger(int n)
        {
            return n;
        }
        

    }
}
