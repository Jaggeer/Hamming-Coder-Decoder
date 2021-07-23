using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HammingCoder
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int a;
                Console.Write("Enter number, which you want to encode with Hamming code (15, 11): ");
                a = int.Parse(Console.ReadLine());
                var b = new BitArray(BitConverter.GetBytes(a));
                DisplayBitArray(Encode_15_11(b));
            }
        }

        static BitArray Encode_15_11(BitArray b)
        {
            var data = new BitArray(11);
            for (int i = 0; i < 11; i++)
            {
                data.Set(i, b.Get(i)); 
            }

            var codeWord = new BitArray(16);

            codeWord.Set(0, false);

            for (int i = 0; i < 4; i++)
            {
                double d = 2;
                int index = Convert.ToInt32(Math.Pow(d, i));
                if(index == 1)
                {
                    codeWord.Set(index, data.Get(0) ^ data.Get(1) ^ data.Get(3) ^ data.Get(4) ^
                            data.Get(6) ^ data.Get(8) ^ data.Get(10));
                }else if(index == 2)
                {
                    codeWord.Set(index, data.Get(0) ^ data.Get(2) ^ data.Get(3) ^ data.Get(5) ^
                            data.Get(6) ^ data.Get(9) ^ data.Get(10));
                }
                else if (index == 4)
                {
                    codeWord.Set(index, data.Get(1) ^ data.Get(2) ^ data.Get(3) ^ data.Get(7) ^
                            data.Get(8) ^ data.Get(9) ^ data.Get(10));
                }
                else if (index == 8)
                {
                    codeWord.Set(index, data.Get(4) ^ data.Get(5) ^ data.Get(6) ^ data.Get(7) ^
                            data.Get(8) ^ data.Get(9) ^ data.Get(10));
                }
            }

            codeWord.Set(3, data.Get(0));
            codeWord.Set(5, data.Get(1));
            codeWord.Set(6, data.Get(2));
            codeWord.Set(7, data.Get(3));
            codeWord.Set(9, data.Get(4));
            codeWord.Set(10, data.Get(5));
            codeWord.Set(11, data.Get(6));
            codeWord.Set(12, data.Get(7));
            codeWord.Set(13, data.Get(8));
            codeWord.Set(14, data.Get(9));
            codeWord.Set(15, data.Get(10));

            return codeWord;
        }

        static void DisplayBitArray(BitArray bitArray)
        {
            var enc = new byte[2];
            bitArray.CopyTo(enc, 0);

            //for (int i = bitArray.Count - 1; i >= 0; i--)
            //{
            //    bool bit = bitArray.Get(i);
            //    Console.Write(bit ? 1 : 0);
            //}

            for (int i = 0; i < 2; i++)
            {
                Console.Write(enc[i] + "  ");
            }

            Console.Write("= ");

            for (int i = 0; i < 2; i++)
            {
                Console.Write(Convert.ToString(enc[i], 16) + " ");
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
