using System;
using StringUtils.Utils.Extensions;
using UnityEngine;

namespace StringUtils.Utils
{
    public sealed class StringCreator
    {
        public int Length => currentIndex;
        public char this[int i]
        {
            get
            {
                if (i >= currentIndex)
                    throw new ArgumentOutOfRangeException();
                    
                return stringOutput[i];
            }
            set
            {
                if (i >= currentIndex)
                    throw new ArgumentOutOfRangeException();
                
                stringOutput.ReplaceAt(i, value);
            }
        }

        private readonly char[] charsBuffer;
        private readonly string stringOutput;
        private int currentIndex;

        private const char DefaultChar = default;

        public StringCreator(char[] chars)
        {
            stringOutput = new string(chars);
            currentIndex = stringOutput.Length;
            charsBuffer = new char[chars.Length];
        }

        public StringCreator(string str)
        {
            stringOutput = str;
            currentIndex = str.Length;
            charsBuffer = new char[str.Length];
        }
        
        public StringCreator(int length)
        {
            stringOutput = new string(DefaultChar, length);
            charsBuffer = new char[length];
        }

        public static string operator +(string str, StringCreator stringCreator)
        {
            return stringCreator.Add(str);
        }
        
        public static StringCreator operator +(StringCreator stringCreator, string str)
        {
            stringCreator.Add(str);
            return stringCreator;
        }

        public static implicit operator string(StringCreator stringCreator)
        {
            return stringCreator.stringOutput;
        }

        public StringCreator Set(string str)
        {
            Clear();
            Add(str);

            return this;
        }
        
        public string Add(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (i + currentIndex < stringOutput.Length)
                    stringOutput.ReplaceAt(i + currentIndex, str[i]);
            }

            AddIndexOffset(str.Length);
            return stringOutput;
        }

        public void Insert(int startIndex, string str)
        {
            int stringLength = str.Length;
            int charBufferLength = charsBuffer.Length;
            
            if (startIndex > currentIndex)
                throw new ArgumentOutOfRangeException();
            
            for (int i = 0; i < currentIndex; i++)
            {
                charsBuffer[i] = stringOutput[i];
            }

            for (int i = 0; i < stringLength; i++)
            {
                if(i + startIndex < charBufferLength) 
                    stringOutput.ReplaceAt(i + startIndex, str[i]);
            }
            
            AddIndexOffset(stringLength);
            
            for (int i = startIndex; i < currentIndex; i++)
            {
                if(i + stringLength < charBufferLength) 
                    stringOutput.ReplaceAt(i + stringLength, charsBuffer[i]);
            }
        }

        private void AddIndexOffset(int value)
        {
            currentIndex += value;
            currentIndex = Mathf.Clamp(currentIndex, 0, charsBuffer.Length);
        }

        public void Clear()
        {
            for (int i = 0; i < stringOutput.Length; i++)
            {
                stringOutput.ReplaceAt(i, DefaultChar);
                charsBuffer[i] = DefaultChar;
            }
            
            currentIndex = 0;
        }

        public override string ToString()
        {
            return stringOutput;
        }
    }
}
