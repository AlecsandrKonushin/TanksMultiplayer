using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class GameManager : MonoBehaviourPun
    {
        private static GameManager instance;

        public static GameManager Instance { get => instance; }

        public Team[] teams;

        private int maxScore = 30;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public bool IsGameOver()
        {
            bool isOver = false;
            int[] score = PhotonNetwork.CurrentRoom.GetScore();

            for (int i = 0; i < teams.Length; i++)
            {
                if (score[i] >= maxScore)
                {
                    isOver = true;
                    break;
                }
            }

            return isOver;
        }

        public int GetTeamFill()
        {
            int[] size = PhotonNetwork.CurrentRoom.GetSize();
            int teamNumber = 0;
            int minimum = size[0];

            for (int i = 0; i < teams.Length; i++)
            {
                if(size[i] < minimum)
                {
                    minimum = size[i];
                    teamNumber = i;
                }
            }

            return teamNumber;
        }
    }

    [Serializable]
    public class Team
    {
        public string Name;
        public Material Material;
        public Transform Spawn;
    }
}