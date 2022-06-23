using System;
using System.Runtime.InteropServices;

namespace StringUtils.Utils.Extensions
{
    public static class StringExtension
    {
        public static string ReplaceChar(this string str, char originChar, char replaceChar)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == originChar)
                {
                    ReplaceAt(str, i, replaceChar);
                    break;
                }
            }
            
            return str;
        }

        public static string ReplaceAll(this string str, char originChar, char replaceChar)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == originChar)
                {
                    ReplaceAt(str, i, replaceChar);
                }
            }

            return str;
        }
        
        public static string ReplaceAt(this string str, int index, char character)
        {
            if (index >= str.Length)
                throw new ArgumentOutOfRangeException();
            
            var handle = GCHandle.Alloc(str, GCHandleType.Pinned);
        
            try
            {
                Marshal.WriteInt16(handle.AddrOfPinnedObject(), index * 2, character);
            }
            finally
            {
                handle.Free();
            }

            return str;
        }
    }
}
