using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Squirrel.OctopusDelivery.App.Extensions;
using Squirrel.OctopusDelivery.App.Properties;

namespace Squirrel.OctopusDelivery.App
{
    internal class Program
    {
        private const int _secondsToWait = 30;
        private static readonly Version CurrentVersion = Assembly.GetExecutingAssembly().GetAssemblyFileVersion(); // extension method
        private static bool _exitApplication;

        private static void Main()
        {
            // write out the current app info.
            Console.WriteLine(Resources.CurrentEnvironment, ConfigurationManager.AppSettings["Environment"]);
            Console.WriteLine(Resources.CurrentVersion, CurrentVersion);

            // Timer to check for updates every X seconds
            var timer = new Timer(CheckForAppUpdates, null, 0, _secondsToWait*1000);
            while (!_exitApplication)
            {
                // keep the app updated until someone chooses to restart the app.
                Thread.Sleep(500); // adding a thread.sleep simply reduces CPU load.
            }

            // dispose of the timer before exiting.
            timer.Dispose();
        }

        private static async void CheckForAppUpdates(object state)
        {
            // Check for updates.
            var futureVersion = await SquirrelUpdater.CheckForUpdate();
            if (futureVersion != null && futureVersion.Version != CurrentVersion)
            {
                // new update found, should we update?
                Console.WriteLine(Resources.UpdateAvailable, futureVersion.Version);
                if (Console.ReadKey(true).Key != ConsoleKey.U) return;

                // update approved, install the update.
                await SquirrelUpdater.Update();

                // new release is installed, should we re-launch.
                Console.WriteLine(Resources.Relaunch, futureVersion.Version);
                if (Console.ReadKey(true).Key != ConsoleKey.X) return;

                RestartConsoleApplication();
            }
            else
            {
                Console.WriteLine(Resources.NoUpdateFound, _secondsToWait);
            }
        }

        private static void RestartConsoleApplication()
        {
            var fileName = Assembly.GetExecutingAssembly().Location;
            Process.Start(fileName);
            _exitApplication = true;
        }
    }
}