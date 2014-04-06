using UnityEngine;
using System.Collections;

/// <summary>
///  Class "WayPoint" initializes the structure representing a transit point for enemies.
/// </summary>
public class WayPoint {

	public int[] tabNodes;

	/// <summary>
	/// Constructor of the class Way. It initializes the class variables with 0 values.
	/// </summary>
	public WayPoint(){
		tabNodes = new int[] {0, 0, 0};
	}

	/// <summary>
	/// Constructor of the class Way. It initializes the class variables.
	/// </summary>
	/// <param name=node>The current node.</param>
	/// <param name=node1>The first destination node.</param>
	/// <param name=node2>The second destination node.</param>
	public WayPoint(int node, int node1, int node2) {
		tabNodes = new int[] {node, node1, node2};
	}

	/// <summary>
	/// Function that configures a node of wayPoint;
	/// </summary>
	/// <param name=node>The current node.</param>
	/// <param name=node1>The first destination node.</param>
	/// <param name=node2>The second destination node.</param>
	public void setWayPoint(int node, int node1, int node2) {
		tabNodes [0] = node;
		tabNodes [1] = node1;
		tabNodes [2] = node2;
	}

	/// <summary>
	/// Function that displays a node of wayPoint;
	/// </summary>
	public void displayWayPoint() {
		Debug.Log (tabNodes[0] + " " + tabNodes[1] + " " + tabNodes[2]);
	}
}
