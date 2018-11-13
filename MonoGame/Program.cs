using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MonoGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        //private static BlockingCollection<Game1> _games = new BlockingCollection<Game1>();
        //private static AutoResetEvent _stayAlive = new AutoResetEvent(false);

        //[STAThread]
        //static void Main()
        //{
        //    SpawnWindow();
        //    SpawnWindow();

        //    // Keep the main UI thread alive until we decide we are done.
        //    _stayAlive.WaitOne();
        //}

        //static void SpawnWindow()
        //{
        //    Task.Factory.StartNew(() =>
        //    {
        //        using (var game = new Game1())
        //        {
        //            _games.Add(game);
        //            game.Exiting += HandleGameExiting;
        //            game.Run();
        //        }
        //    });
        //}

        //private static void HandleGameExiting(object sender, EventArgs e)
        //{
        //    var game = sender as Game1;
        //    if (game != null)
        //    {
        //        Game1 gameToRemove;
        //        _games.TryTake(out gameToRemove);

        //        if (gameToRemove == null)
        //        {
        //            Console.WriteLine("Failed to remove closing game.");
        //            return;
        //        }

        //        // If no more windows exist lets close everything down.
        //        if (_games.Count == 0)
        //            _stayAlive.Set();
        //    }
        //}



        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();

            //using (var game1 = new Game2())
            //    game1.Run();
        }

    }
#endif
}