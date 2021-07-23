using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HammingDecoder
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var b = new byte[2];
                Console.Write("Enter two numbers, which was encoded by Hamming code (15, 11), only separated by whitespace: ");
                string[] tokens = Console.ReadLine().Split();

                b[0] = Convert.ToByte(tokens[0]);
                b[1] = Convert.ToByte(tokens[1]);

                Console.WriteLine(Decode_15_11(b));
                Console.WriteLine();
            }
        }

        static int Decode_15_11(byte[] b)
        {
            var codeWord = new BitArray(b);
            var syndrome = new BitArray(4);

            syndrome.Set(0, codeWord[3] ^ codeWord[5] ^ codeWord[7] ^ codeWord[9] ^ codeWord[11]
                ^ codeWord[13] ^ codeWord[15] ^ codeWord[1]);

            syndrome.Set(1, codeWord[3] ^ codeWord[6] ^ codeWord[7] ^ codeWord[10] ^ codeWord[11]
                ^ codeWord[14] ^ codeWord[15] ^ codeWord[2]);

            syndrome.Set(2, codeWord[5] ^ codeWord[6] ^ codeWord[7] ^ codeWord[12] ^ codeWord[13]
                ^ codeWord[14] ^ codeWord[15] ^ codeWord[4]);

            syndrome.Set(3, codeWord[9] ^ codeWord[10] ^ codeWord[11] ^ codeWord[12] ^ codeWord[13]
                ^ codeWord[14] ^ codeWord[15] ^ codeWord[8]);

            int syn = (Convert.ToInt32(syndrome.Get(3)) << 3) | (Convert.ToInt32(syndrome.Get(2)) << 2) |
                (Convert.ToInt32(syndrome.Get(1)) << 1) | (Convert.ToInt32(syndrome.Get(0)));

            var cor = new BitArray(11);

            switch (syn)
            {
                case 3:
                    cor.Set(0, true);
                    break;

                case 5:
                    cor.Set(1, true);
                    break;

                case 6:
                    cor.Set(2, true);
                    break;

                case 7:
                    cor.Set(3, true);
                    break;

                case 9:
                    cor.Set(4, true);
                    break;

                case 10:
                    cor.Set(5, true);
                    break;

                case 11:
                    cor.Set(6, true);
                    break;

                case 12:
                    cor.Set(7, true);
                    break;

                case 13:
                    cor.Set(8, true);
                    break;

                case 14:
                    cor.Set(9, true);
                    break;

                case 15:
                    cor.Set(10, true);
                    break;

                default:
                    break;
            }

            var data = new BitArray(11);

            data.Set(0, codeWord.Get(3));
            data.Set(1, codeWord.Get(5));
            data.Set(2, codeWord.Get(6));
            data.Set(3, codeWord.Get(7));
            data.Set(4, codeWord.Get(9));
            data.Set(5, codeWord.Get(10));
            data.Set(6, codeWord.Get(11));
            data.Set(7, codeWord.Get(12));
            data.Set(8, codeWord.Get(13));
            data.Set(9, codeWord.Get(14));
            data.Set(10, codeWord.Get(15));

            data.Xor(cor);

            var decodeArr = new int[1];
            data.CopyTo(decodeArr, 0);

            int decode = decodeArr[0];

            return decode;
        }

    }
}
