﻿/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/25 10:08:40
*   描述说明：
*
*****************************************************************/


using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerManager:BaseManager
    {
        public Dictionary<string, PlayerInfo> players = new Dictionary<string, PlayerInfo>();
        //GameObject bodyPrefab= (GameObject) Resources.Load("Prefabs/Body/skin1_2");
        public List<Snake> snakes = new List<Snake>();
        public Action<PlayerInfo> UpdateFunc;
        public PlayerInfo temp;

        public GameObject Prefab { get; private set; }
        private GameObject otherPlayer;
        /// <summary>
        /// 改成在主线程中调用
        /// </summary>
        /// <param name="player"></param>
        public void UpdatePlayer(PlayerInfo player)
        {
            if (player.username == GameManager.Instance.chaManager.GetLocalPlayerName())
                return;
            if (players.ContainsKey(player.username))
            {
                players[player.username] = player;
                //更新角色信息
                Snake snake = snakes.Find(s => s.username == player.username);
                snake.UpdatePos(player);
            }
            else
            {
                //创建一个新角色
                players.Add(player.username, player);
                GameObject go = new GameObject("player");
                var root = GameObject.Find("Snakes");
                go.transform.SetParent(root.transform);
                Snake sn = go.AddComponent<Snake>();
                snakes.Add(sn);
                //初始化所有信息和数据
                sn.Init(player);
            }

            //if (player.username != GameManager.Instance.chaManager.GetLocalPlayerName())
            //{
            //    if (otherPlayer == null)
            //    {
            //        otherPlayer = GameObject.Instantiate(Prefab);
            //        otherPlayer.AddComponent<OtherPlayer>();
            //    }
            //    else
            //    {
            //        var scri = otherPlayer.GetComponent<OtherPlayer>();
            //        var vec2 = player.pos[0];
            //        scri.ReceiveMovementMessage(new Vector3(vec2.posx, vec2.posy, 0f), player.time);
            //    }
            //}
        }


        public override void Update()
        {
            try
            {
                if (UpdateFunc?.Method != null)
                {
                    UpdateFunc.Invoke(temp);
                    UpdateFunc -= UpdatePlayer;
                }
            }catch(Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            
        }

        public override void OnInit()
        {
            Prefab = Resources.Load("Prefabs/Body/skin1_1") as GameObject;
            Prefab.transform.localScale = new Vector3(0.5f, 0.5f);

        }

        public override void OnDestroy()
        {
            
        }
    }
}
