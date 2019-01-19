using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using Newtonsoft;
using LitJson;

/// <summary>
/// Class to retrieve data from json file
/// </summary>
public static class JsonRetrieval  {


	public static LevelModel ReadFile(int id){
		 string jsonString;
		 JsonData itemData;
		LevelModel model = new LevelModel ();

		TextAsset textAsset = (TextAsset)Resources.Load("data"); 
		string jsonString2 = textAsset.text;

		itemData = JsonMapper.ToObject (jsonString2);
		model.Id = (int)itemData["levels"][id-1]["id"];
		model.LaserInterval = (int) itemData["levels"][id-1]["laserInterval"];
		model.LaserVelocity = (int) itemData["levels"][id-1]["laserVelocity"];
		model.CoinInterval = (int) itemData["levels"][id-1]["coinInterval"];
		model.CoinVelocity = (int) itemData["levels"][id-1]["coinVelocity"];
		model.IsBossEnabled = (bool) itemData["levels"][id-1]["bossEnabled"];
		model.DistanceNeeded = (int) itemData["levels"][id-1]["distanceNeeded"];
		model.Stars = (int) itemData["levels"][0]["stars"];
		model.OneStarThreshold = (int) itemData["levels"][id-1]["oneStarThreshold"];
		model.TwoStarThreshold =  (int)itemData["levels"][id-1]["twoStarThreshold"];
		model.ThreeStarThreshold = (int) itemData["levels"][id-1]["threeStarThreshold"];
		model.TreeSpeed =(int)  itemData["levels"][id-1]["treeSpeed"];
		model.FloorSpeed = (int) itemData["levels"][id-1]["floorSpeed"];
		model.SkySpeed = (int) itemData["levels"][id-1]["skySpeed"];
		return model;

	}

}
