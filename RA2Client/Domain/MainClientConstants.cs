using ClientCore;
using System.Collections.Generic;

namespace Ra2Client.Domain
{
    public static class MainClientConstants
    {
        //public const string CNCNET_TUNNEL_LIST_URL = "http://cncnet.org/master-list";

        public static readonly Dictionary<string, string> TunnelServerUrls = new Dictionary<string, string>
        {
            { "仅官方自建", "https://ra2yr.dreamcloud.top:9999/tunnels/official" },
            { "仅中国内地", "https://ra2yr.dreamcloud.top:9999/tunnels/cn" },
            { "仅中国内地+港澳台", "https://ra2yr.dreamcloud.top:9999/tunnels/zh" },
            { "原服务器列表", "https://ra2yr.dreamcloud.top:9999/tunnels/all" }
        };

        public static string CurrentTunnelServerUrl = TunnelServerUrls["仅官方自建"];

        public static string GAME_NAME_LONG = "Red Alert 2 Reunion Client";
        public static string GAME_NAME_SHORT = "Reunion";

        //public static string CREDITS_URL = Path.Combine(ProgramConstants.游戏目录, "Resources\\ThanksList");

        public static string SUPPORT_URL_SHORT = "ra2.dreamcloud.top:9999";

        public static bool USE_ISOMETRIC_CELLS = true;
        public static int TDRA_WAYPOINT_COEFFICIENT = 128;
        public static int MAP_CELL_SIZE_X = 48;
        public static int MAP_CELL_SIZE_Y = 24;

        public static OSVersion OSId = OSVersion.UNKNOWN;

        public static void Initialize()
        {
            var clientConfiguration = ClientConfiguration.Instance;

            OSId = clientConfiguration.GetOperatingSystemVersion();

            GAME_NAME_SHORT = clientConfiguration.LocalGame;
            GAME_NAME_LONG = clientConfiguration.LongGameName;

            SUPPORT_URL_SHORT = clientConfiguration.ShortSupportURL;

            //CREDITS_URL = clientConfiguration.CreditsURL;

            USE_ISOMETRIC_CELLS = clientConfiguration.UseIsometricCells;
            TDRA_WAYPOINT_COEFFICIENT = clientConfiguration.WaypointCoefficient;
            MAP_CELL_SIZE_X = clientConfiguration.MapCellSizeX;
            MAP_CELL_SIZE_Y = clientConfiguration.MapCellSizeY;

            if (string.IsNullOrEmpty(GAME_NAME_SHORT))
                throw new ClientConfigurationException("LocalGame 设置为空值.");

            if (GAME_NAME_SHORT.Length > ProgramConstants.GAME_ID_MAX_LENGTH)
            {
                throw new ClientConfigurationException("LocalGame 设置的长度超过了 " +
                    ProgramConstants.GAME_ID_MAX_LENGTH);
            }
        }
    }
}
