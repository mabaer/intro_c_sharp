using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //For FileStream
using System.Security.Cryptography; //For Hash

/*
 *  Copyright 2012 Marc-André Bär
 
 *  This project is for educational use and part of the workshop "Introduction to C#" from Marc-André Bär.
 *  Task summary: Blockhashing; Blocksize of 1kB; Get hash0 of a file; Use of SHA265
 
 *  This file is part of Demo1-GetHash0.

    Demo1-GetHash0 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Demo1-GetHash0 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Demo1-GetHash0.  If not, see <http://www.gnu.org/licenses/>.
  
 */
namespace GetHash0
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"TODO ENTER PATH";
            Console.WriteLine("---Start Reading---");
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Console.WriteLine("---Reading Done---");

            Console.WriteLine("---Start Building Packets---");

            HashAlgorithm hash = new SHA256Managed();

            byte[] dataBytes = new byte[1024];
            LinkedList<byte[]> ll = new LinkedList<byte[]>();
            int ind = 0;
            //Save 1kB packets in linkedList(Reverse order)
            while ((ind = fileStream.Read(dataBytes, 0, dataBytes.Length)) > 0)
            {
                byte[] bytes = new byte[1024];
                Array.Copy(dataBytes, 0, bytes, 0, 1024);//bytes = (dataBytes.CopyTo());
                ll.AddFirst(bytes);
                dataBytes = new byte[1024];
            }
            fileStream.Close();
            Console.WriteLine(ll.Count + " Packets");
            Console.WriteLine("---Building Packets Done---");
            
            //First Hash
            dataBytes = ll.First.Value;
            ll.RemoveFirst();

            //Remove the additional bytes from the last block
            int lastGoodChar = 0;
            for (int i = 0; i < dataBytes.Length; i++)
            {
                int bint = (dataBytes[i]);
                if (bint != 0)
                {
                    lastGoodChar = i;
                }
            }
            byte[] firstBlock = new byte[lastGoodChar + 1];
            for (int i = 0; i < lastGoodChar + 1; i++)
            {
                firstBlock[i] = dataBytes[i];
            }

            Console.WriteLine("---Start Searching For h0---");
            byte[] blockBytes = new byte[1056];
            byte[] mdBytes = new byte[32];
            dataBytes = new byte[1024];

            //Calculate the first hash
            mdBytes = hash.ComputeHash(firstBlock);

            int count = ll.Count;
            //Iterate over all blocks
            for (int k = 0; k < count; k++)
            {
                dataBytes = ll.First.Value;
                ll.RemoveFirst();
                //Copy of the data
                for (int i = 0; i < dataBytes.Length; i++)
                    blockBytes[i] = dataBytes[i];

                //Add of the hash
                for (int i = 0; i < mdBytes.Length; i++)
                    blockBytes[dataBytes.Length + i] = mdBytes[i];

                //Hash for the next interation
                mdBytes = hash.ComputeHash(blockBytes);
            }
            Console.WriteLine("---Searching For h0 Done---");

            Console.WriteLine("Result for h0 = " + BitConverter.ToString(mdBytes));
            Console.ReadLine();
        }
    }
}
