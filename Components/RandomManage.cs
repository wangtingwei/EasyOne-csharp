namespace EasyOne.Components
{
    using System;
    using System.Text;

    public sealed class RandomManage
    {
        private static Random rand = new Random((int) DateTime.Now.Ticks);
        private static readonly char[] RandChar = new char[] { 
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
            'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 
            'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
         };
        private static int s_RoCount = 1;

        private RandomManage()
        {
        }

        public static int GetFormatedNumeric(int min, int max)
        {
            int num = 0;
            num = new Random(s_RoCount * ((int) DateTime.Now.Ticks)).Next(min, max);
            s_RoCount++;
            return num;
        }

        private static char GetRandChar()
        {
            return RandChar[rand.Next(0x3e)];
        }

        private static char GetRandNum()
        {
            return RandChar[rand.Next(0, 10)];
        }

        public static string GetRandString(int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(GetRandChar());
            }
            return builder.ToString();
        }

        public static string GetRandStringByPattern(string pattern)
        {
            if ((!pattern.Contains("#") && !pattern.Contains("?")) && !pattern.Contains("*"))
            {
                return pattern;
            }
            char[] chArray = pattern.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < chArray.Length; i++)
            {
                switch (chArray[i])
                {
                    case '#':
                        chArray[i] = GetRandNum();
                        goto Label_0069;

                    case '*':
                        chArray[i] = GetRandChar();
                        goto Label_0069;

                    case '?':
                        chArray[i] = GetRandWord();
                        break;
                }
            Label_0069:
                builder.Append(chArray[i]);
            }
            return builder.ToString();
        }

        private static char GetRandWord()
        {
            return RandChar[rand.Next(10, 0x3e)];
        }
    }
}

