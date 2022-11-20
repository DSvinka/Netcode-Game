namespace Code.Singletons
{
    public static class PlayerDataManager
    {
        public static string PlayerName;
        public static float Health;
        public static ulong ClientId;

        public static void Clear()
        {
            PlayerName = "Unknown";
            Health = -1f;
            ClientId = 0;
        }
    }
}