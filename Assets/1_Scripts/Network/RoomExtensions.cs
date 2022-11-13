using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Network
{
    public static class RoomExtensions
    {
        public const string SIZE = "size";
        public const string SCORE = "score";

        public static int[] GetSize(this Room room)
        {
            return (int[])room.CustomProperties[SIZE];
        }

        public static int[] AddSize(this Room room, int teamIndex, int value)
        {
            int[] sizes = room.GetSize();
            sizes[teamIndex] += value;

            room.SetCustomProperties(new Hashtable() { { SIZE, sizes } });
            return sizes;
        }

        public static int[] GetScore(this Room room)
        {
            return (int[])room.CustomProperties[SCORE];
        }

        public static int[] AddScore(this Room room, int teamIndex, int value)
        {
            int[] scores = room.GetScore();
            scores[teamIndex] += value;

            room.SetCustomProperties(new Hashtable() { { SCORE, scores } });
            return scores;
        }
    }
}