using System.Diagnostics.CodeAnalysis;

namespace Ging.UI
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ui = new ProgramUI(new RealConsole());
            ui.Run();
        }
    }
}
