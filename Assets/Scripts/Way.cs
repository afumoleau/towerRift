using UnityEngine;
using System.Collections;

/// <summary>
///  Class "Way" initializes all possible paths on the map for enemies in a table structure.
/// </summary>
public class Way {
	
	public WayPoint[] waysOfWayPoints;
	private Transform[] listOfTransforms;
	private int index;
	private Transform path;
	private Transform[] pathComp;

	/// <summary>
	/// Constructor of the class Way. It initializes the class variables.
	/// </summary>
	public Way ()
	{
		waysOfWayPoints = new WayPoint[65];
		pathComp = new Transform[65];

		for (int i = 0; i < 65; ++i) {
			waysOfWayPoints [i] = new WayPoint();
		}

		initializeWay ();
	}

	/// <summary>
	/// Function that initializes the tree of ways.
	/// </summary>
	void initializeWay () {
		setWay(1, 12, 2);
		setWay(2, 14, 0);
		setWay(3, 15, 0);
		setWay(4, 3, 5);
		setWay(5, 17, 0);
		setWay(6, 18, 0);
		setWay(7, 6, 8);
		setWay(8, 20, 0);
		setWay(9, 21, 0);
		setWay(10, 9, 8);
		setWay(11, 23, 0);
		setWay(12, 24, 0);
		setWay(13, 14, 24);
		setWay(14, 13, 46);
		setWay(15, 16, 47);
		setWay(16, 15, 17);
		setWay(17, 16, 49);
		setWay(18, 19, 50);
		setWay(19, 18, 20);
		setWay(20, 19, 52);
		setWay(21, 22, 53);
		setWay(22, 21, 23);
		setWay(23, 22, 55);
		setWay(24, 13, 56);
		setWay(46, 56, 65);
		setWay(47, 49, 65);
		setWay(49, 47, 65);
		setWay(50, 52, 65);
		setWay(52, 50, 65);
		setWay(53, 55, 65);
		setWay(55, 53, 65);
		setWay(56, 46, 65);
		setWay(65, 65, 65);
	}

	/// <summary>
	/// Function that gives the number of checkpoint at the index given. 
	/// </summary>
	/// <param name=n>The index in the table of checkpoints.</param>
	/// <returns>The number of checkpoints.</returns>
	public int getNumWayPoint (int n) {
		return waysOfWayPoints[n].tabNodes[0];
	}

	/// <summary>
	/// Function that setting a way on the map.
	/// </summary>
	/// <param name=></param>
	/// <param name=></param>
	/// <param name=></param>
	public void setWay(int node, int node1, int node2) {
		waysOfWayPoints [node - 1].setWayPoint (node, node1, node2);
	}

	/// <summary>
	/// Function that displays all possible ways. It's used for debugging.
	/// </summary>
	public void displayWay () {
		for (int i = 0; i < 65; i++) {
			Debug.Log (i + " : " + waysOfWayPoints[i].tabNodes[0] + " " + waysOfWayPoints[i].tabNodes[1] + " " + waysOfWayPoints[i].tabNodes[2]);
		}
	}
}
