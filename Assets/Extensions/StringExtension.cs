using System;
using System.Runtime.InteropServices;

namespace StringUtils.Extensions
{
    public static class StringExtension
    {
        public static void ReplaceChar(this string str, char originChar, char replaceChar)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == originChar)
                {
                    ReplaceAt(str, i, replaceChar);
                    break;
                }
            }
        }

        public static void ReplaceAll(this string str, char originChar, char replaceChar)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == originChar)
                {
                    ReplaceAt(str, i, replaceChar);
                }
            }
        }
        
        public static void ReplaceAt(this string str, int index, char character)
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
        }
        
    }
}
