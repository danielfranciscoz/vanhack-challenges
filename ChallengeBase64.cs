
namespace base64
{
    public class Challenge
    {
        static char[] base64Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();

        public static string ToBase64(string str)
        {
            string completeStringBinary = "";
            string completeStringDecimal = "";

            foreach (int item in StringToASCII(str))
            {
                completeStringBinary += CompleteBinaryToLengthLeftToRigth(ASCIIToBinary(item), 8);
            }


            foreach (string item in ChunkStringByLength(completeStringBinary, 6))
            {
                completeStringDecimal += DecimalToBase64Alphabet(BinaryToASCII(item));

            }


            return CompleteBase64WithEquals(completeStringDecimal);
        }

        public static int[] StringToASCII(string str)
        {
            int[] asciiValue = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                asciiValue[i] = (int)str[i];
            }

            return asciiValue;

        }

        public static long ASCIIToBinary(int decimalValue)
        {
            long binary = 0;
            const int splitBy = 2;
            long digit = 0;

            for (int i = decimalValue % splitBy, j = 0; decimalValue > 0; decimalValue /= splitBy, i = decimalValue % splitBy, j++)
            {
                digit = i % splitBy;
                binary += digit * (long)Pow(10, j);
            }


            return binary;
        }

        public static int BinaryToASCII(string binaryValue)
        {
            char[] array = ReserveCharArray(binaryValue.ToCharArray());

            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '1')
                {
                    if (i == 0)
                        sum += 1;
                    else
                        sum += (int)Pow(2, i);
                }

            }

            return sum;
        }
        public static string CompleteBinaryToLengthLeftToRigth(long binaryValue, int length)
        {

            return binaryValue.ToString($"D{length}");
        }

        public static string CompleteBinaryToLengthRigthToLeft(string binaryValue, int length)
        {
            int amount = length - binaryValue.Length;
            int Zero = 0;
            if (amount > 0)
            {
                binaryValue += Zero.ToString($"D{amount}");

            }
            return binaryValue;
        }

        public static string[] ChunkStringByLength(string str, int chunkSize)
        {

            int subStringlength = RoundUpValue((decimal)(str.Length / (decimal)chunkSize));
            int originalChunkSize = chunkSize;
            string[] subString = new string[subStringlength];
            int stringLength = str.Length;
            for (int i = 0, j = 0; i < stringLength; i += chunkSize, j++)
            {
                if (i + chunkSize > stringLength) chunkSize = stringLength - i;

                if (chunkSize == originalChunkSize)
                    subString[j] = str.Substring(i, chunkSize);
                else
                    subString[j] = CompleteBinaryToLengthRigthToLeft(str.Substring(i, chunkSize), originalChunkSize);


            }

            return subString;
        }

        public static string DecimalToBase64Alphabet(int decimalValue)
        {
            return base64Alphabet[decimalValue].ToString();
        }

        public static string CompleteBase64WithEquals(string str)
        {
            if (str.Length % 4 != 0)
            {
                str = CompleteBase64WithEquals($"{str}=");
            }
            return str;
        }

        public static decimal Pow(decimal number, int power)
        {
            decimal result = 1;

            int absPow = power < 0 ? power * (-1) : power;
            for (int i = 1; i <= absPow; ++i)
            {
                if (power > 0)
                    result *= number;

                else
                    result /= number;
            }
            return result;
        }

        public static int RoundUpValue(decimal number)
        {
            decimal result = number - (int)number;
            return (int)(result > 0 ? number + 1 : number);
        }

        public static char[] ReserveCharArray(char[] array)
        {

            for (int i = 0; i < array.Length / 2; i++)
            {
                char tmp = array[i];
                array[i] = array[array.Length - i - 1];
                array[array.Length - i - 1] = tmp;
            }
            return array;
        }
    }

}
