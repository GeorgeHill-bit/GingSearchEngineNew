using System.Diagnostics.CodeAnalysis;

namespace CloudAcademy.CA151.Lab.Ging.UI
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
