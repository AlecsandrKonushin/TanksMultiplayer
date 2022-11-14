using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class Player : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Slider healthSlider, shieldSlider;



        private int maxHealth = 10;
        private short turrentRotation;
        private float fireRate = 0.75f;
        private float moveSpeed = 8f;
        


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }
    }
}
