using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public static class PlayerExtensions
    {
        public const string TEAM = "team";
        public const string HEALTH = "health";
        public const string SHIELD = "shield";
        public const string AMMO = "ammo";
        public const string BULLET = "bullet";

        public static string GetName(this PhotonView player)
        {
            if(PhotonNetwork.OfflineMode == true)
            {
                //player
            }

            return "";
        }
    }
}