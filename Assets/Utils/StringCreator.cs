using System;
using StringUtils.Utils.Extensions;

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

        private readonly char[] numberCharsBuffer = new char[11];
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
            return stringCreator.Append(str);
        }
        
        public static StringCreator operator +(StringCreator stringCreator, string str)
        {
            stringCreator.Append(str);
            return stringCreator;
        }
        
        public static StringCreator operator +(StringCreator stringCreator, int value)
        {
            stringCreator.Append(value);
            return stringCreator;
        }
        
        public static StringCreator operator +(StringCreator stringCreator, bool value)
        {
            stringCreator.Append(value);
            return stringCreator;
        }
        
        public static StringCreator operator +(StringCreator stringCreator, char value)
        {
            stringCreator.Append(value);
            return stringCreator;
        }

        public static implicit operator string(StringCreator stringCreator)
        {
            return stringCreator.stringOutput;
        }

        public StringCreator Set(string str)
        {
            Clear();
            Append(str);

            return this;
        }
        
        public string Append(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (i + currentIndex < stringOutput.Length)
                    stringOutput.ReplaceAt(i + currentIndex, str[i]);
            }

            AddIndexOffset(str.Length);
            return stringOutput;
        }

        public string Append(char value)
        {
            stringOutput.ReplaceAt(currentIndex, value);
            
            AddIndexOffset(1);
            return stringOutput;
        }

        public string Append(bool value)
        {
            return Append(value.ToString());
        }

        public string Append(int value)
        {
            int bufferIndex = 0;
            int maxLength = 11;

            int offset = 0;

            if (value == 0)
            {
                stringOutput.ReplaceAt(currentIndex, '0');
                AddIndexOffset(1);
                return stringOutput;
            }
            
            
            if (value < 0)
            {
                numberCharsBuffer[bufferIndex] = '-';
                bufferIndex++;
                offset = 1;

                if (value == int.MinValue)
                    value++;
                
                value = Abs(value);
            }

            int startIndex = bufferIndex - offset + maxLength - 1;
            int index = startIndex;
        
            do
            {
                numberCharsBuffer[index] = (char)('0' + value % 10);
                value /= 10;
                --index;
                
                if(index<0)
                    break;
            }
            while (value != 0);

            int length = startIndex + offset - index;

            if (bufferIndex != index + 1)
            {
                while (index != startIndex)
                {
                    ++index;
                    numberCharsBuffer[bufferIndex] = numberCharsBuffer[index];
                    ++bufferIndex;
                }
            }

            length = Clamp(length, 0, stringOutput.Length - currentIndex);
            
            for (int i = 0; i < length; i++)
            {
                stringOutput.ReplaceAt(i + currentIndex, numberCharsBuffer[i]);
            }
            AddIndexOffset(length);

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
            currentIndex = Clamp(currentIndex, 0, charsBuffer.Length);
        }

        private int Clamp(int value, int a, int b)
        {
            int min = b < value ? b : value;
            int max = a > min ? a : min;
            return max;
        }

        private int Abs(int value)
        {
            return value > -value ? value : -value;
        }
        
        public static int max(int x, int y) { return x > y ? x : y; }

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
