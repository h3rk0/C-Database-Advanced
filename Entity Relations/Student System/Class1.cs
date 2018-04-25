using P01_StudentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem
{
    class Class1
    {
        static void Main(string[] args)
        {
            var context = new StudentSystemContext();
            context.Database.EnsureCreated();
            Console.WriteLine();

        }
    }
}
