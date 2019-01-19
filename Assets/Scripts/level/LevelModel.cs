using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  model for each level
/// </summary>
public class LevelModel  {

	private int id;

	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	private int laserInterval;

	public int LaserInterval {
		get {
			return laserInterval;
		}
		set {
			laserInterval = value;
		}
	}

	private int coinInterval;

	public int CoinInterval {
		get {
			return coinInterval;
		}
		set {
			coinInterval = value;
		}
	}

	private int laserVelocity;

	public int LaserVelocity {
		get {
			return laserVelocity;
		}
		set {
			laserVelocity = value;
		}
	}

	private int coinVelocity;

	public int CoinVelocity {
		get {
			return coinVelocity;
		}
		set {
			coinVelocity = value;
		}
	}

	private bool isBossEnabled;

	public bool IsBossEnabled {
		get {
			return isBossEnabled;
		}
		set {
			isBossEnabled = value;
		}
	}

	private int distanceNeeded;

	public int DistanceNeeded {
		get {
			return distanceNeeded;
		}
		set {
			distanceNeeded = value;
		}
	}

	private int stars;

	public int Stars {
		get {
			return stars;
		}
		set {
			stars = value;
		}
	}

	private int oneStarThreshold;

	public int OneStarThreshold {
		get {
			return oneStarThreshold;
		}
		set {
			oneStarThreshold = value;
		}
	}

	private int twoStarThreshold;

	public int TwoStarThreshold {
		get {
			return twoStarThreshold;
		}
		set {
			twoStarThreshold = value;
		}
	}

	private int threeStarThreshold;

	public int ThreeStarThreshold {
		get {
			return threeStarThreshold;
		}
		set {
			threeStarThreshold = value;
		}
	}

	private int skySpeed;

	public int SkySpeed {
		get {
			return skySpeed;
		}
		set {
			skySpeed = value;
		}
	}


	public int FloorSpeed {
		get {
			return floorSpeed;
		}
		set {
			floorSpeed = value;
		}
	}
	private int floorSpeed;


	public int TreeSpeed {
		get {
			return treeSpeed;
		}
		set {
			treeSpeed = value;
		}
	}		

	private int treeSpeed;






}
