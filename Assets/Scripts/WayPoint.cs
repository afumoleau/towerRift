using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour {

	public int[] tabNodes;

	public WayPoint(){
		tabNodes = new int[] {0, 0, 0};
	}

	public WayPoint(int node, int node1, int node2) {
		tabNodes = new int[] {node, node1, node2};
	}

	public void setWayPoint(int node, int node1, int node2) {
		tabNodes [0] = node;
		tabNodes [1] = node1;
		tabNodes [2] = node2;
	}

	public void displayWayPoint() {
		Debug.Log (tabNodes[0] + " " + tabNodes[1] + " " + tabNodes[2]);
	}
}
