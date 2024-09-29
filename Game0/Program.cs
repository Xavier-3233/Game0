using System;

namespace Game0
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SimpleManage())
                game.Run();
        }
    }
}
//using var game = new Game0.Breakdown();
//game.Run();
