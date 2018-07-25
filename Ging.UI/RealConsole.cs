using System;
using System.Diagnostics.CodeAnalysis;

namespace CloudAcademy.CA151.Lab.Ging.UI
{
    [ExcludeFromCodeCoverage]
    internal class RealConsole : IConsole
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public void Write(string message)
        {
            Console.Write(message);
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }
        public void WriteLine(object o)
        {
            Console.WriteLine(o.ToString());
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
