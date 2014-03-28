using UnityEngine;
using System.Collections;

public class buildTower : MonoBehaviour {
	private Transform[] tabTurret;
	public Transform cubeBuild;
	private int caseSwitch;
	private float widthCube;
	public Transform turret;
	private Vector3 positionCube= Vector3.zero;
	private float scaleBase;
	private Vector3 positionBase = Vector3.zero;

	void Start(){
		scaleBase = this.transform.localScale.z*2;
		Debug.Log (scaleBase);
		caseSwitch = 0;
		tabTurret = new Transform[3];
		positionCube = new Vector3(this.transform.position.x,this.transform.position.y + (-scaleBase),this.transform.position.z); 
		positionBase = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	}
	
	bool isFull(Transform[] a, int sizeTab){
		int i = 0;
		int cpt = 0;
		while(i < sizeTab ){
			if(a[i])
				cpt++;
			i++;
		}
		return cpt == sizeTab ;
	}
	
	void Update(){
		if(isFull(tabTurret,3)) {
			Debug.Log("Je suis full");
			GameObject.Destroy (this.gameObject);
			Transform turretBuild = Instantiate (turret) as Transform;
			turretBuild.position = positionBase + new Vector3 (0, turret.transform.localScale.y, 0);
			
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("towerCube"))
		{
			other.transform.parent = null;
			Destroy (other.gameObject);
			Debug.Log (caseSwitch);
			tabTurret [caseSwitch] = Instantiate (cubeBuild) as Transform;
			widthCube = cubeBuild.transform.localScale.y;
		}
		switch (caseSwitch)
		{
		case 0:
			tabTurret [0].transform.position = positionCube;
			tabTurret [0].renderer.material.color = Color.yellow;
			tabTurret [0].transform.parent = this.transform;
			break;
		case 1:
			Vector3 position2 = new Vector3 (positionCube.x, positionCube.y + widthCube, positionCube.z);
			tabTurret [1].transform.position = position2;
			tabTurret [1].renderer.material.color = Color.green;
			tabTurret [1].transform.parent = this.transform;
			break;
		case 2:
			Vector3 position3 = new Vector3 (positionCube.x, positionCube.y + 2 * widthCube, positionCube.z);
			tabTurret [2].transform.position = position3;
			tabTurret [2].renderer.material.color = Color.cyan;
			tabTurret [2].transform.parent = this.transform;
			break;
		default:
			break;
		}
		if (caseSwitch < 2)
			caseSwitch++;
	}
}
