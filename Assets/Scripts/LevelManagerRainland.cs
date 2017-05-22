using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{	
	/// <summary>
	/// Spawns the player, and 
	/// </summary>
	public class LevelManagerRainland : LevelManager
	{	
		
		protected override void Awake()
		{
            GameObject thePlayer = GameObject.Find("Player");
            _player = thePlayer.GetComponent<CharacterBehavior>();

            if (PlayerPrefab!= null && _player == null)
	        { 
	    		_player = (CharacterBehavior)Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
	        }
            GameManager.Instance.Player=_player;
	    }
		
	}
}