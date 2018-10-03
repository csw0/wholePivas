using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace PIVAsCommon.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Struct转byte[]
        /// </summary>
        /// <param name="structObj"></param>
        /// <returns></returns>
        public static byte[] StructToBytes(this object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("byte[]转换为struct错误:" + ex.Message);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// byte[]转换为struct
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="strcutType"></param>
        /// <returns></returns>
        public static object BytesToStruct(this byte[] bytes, Type strcutType)
        {
            int size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            catch(Exception ex)
            {
                InternalLogger.Log.Error("byte[]转换为struct错误:" + ex.Message);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// 按照顺序合并数组
        /// </summary>
        /// <param name="firstBytes"></param>
        /// <param name="secondBytes"></param>
        /// <returns></returns>
        public static byte[] MergeBytes(this byte[] firstBytes, byte[] secondBytes)
        {
            try
            {
                int size = firstBytes.Length + secondBytes.Length;
                byte[] mergeBytes = new byte[size];
                firstBytes.CopyTo(mergeBytes, 0);
                secondBytes.CopyTo(mergeBytes, firstBytes.Length);
                return mergeBytes;
            }
            catch
            {
                InternalLogger.Log.Error("按照顺序合并数组错误");
                return null;
            }
        }

        /// <summary>
        /// byte[]转十六进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHexString(this byte[] bytes)
        {
            if (bytes == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (byte bt in bytes)
            {
                sb.Append(bt.ToString("X2") + " ");
            }
            return sb.ToString();
        }
    }
}
