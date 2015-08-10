using System.Configuration;
using System.Threading.Tasks;

namespace Squirrel.OctopusDelivery.App
{
    public class SquirrelUpdater
    {
        private static readonly string UpdatePath = ConfigurationManager.AppSettings["SquirrelUpdatePath"];
        private static readonly string AppName = ConfigurationManager.AppSettings["AppName"];

        public static async Task<ReleaseEntry> CheckForUpdate()
        {
            if (!CanUpdate()) return null;

            using (var mgr = new UpdateManager(UpdatePath, AppName))
            {
                var updateInfo = await mgr.CheckForUpdate();
                return updateInfo.FutureReleaseEntry;
            }
        }

        public static async Task Update()
        {
            if (!CanUpdate()) return;

            using (var mgr = new UpdateManager(UpdatePath, AppName))
            {
                await mgr.UpdateApp();
            }
        }

        private static bool CanUpdate()
        {
            return !string.IsNullOrWhiteSpace(UpdatePath) &&
                   !string.IsNullOrWhiteSpace(AppName);
        }
    }
}