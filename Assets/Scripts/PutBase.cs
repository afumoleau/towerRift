using UnityEngine;
using System.Collections;

public class PutBase : MonoBehaviour {

	public Transform randomPosition;
	public Transform towerCubePrefab;
	public TextMesh playerGold;
	public Camera c;


	private double money =100;
	private const int nombreBaseMax = 10;
	public Transform effect;
	private Hashtable numberOfBase= new Hashtable(nombreBaseMax);
	private int indiceKey=0;
	private string keyCurrent;
	

	bool is_full(Hashtable a){
		return a.Count == nombreBaseMax;
	}

	void swap_base(Hashtable baseTable){
		Destroy(((Transform)baseTable[keyCurrent]).gameObject);
		numberOfBase.Remove(keyCurrent);
		}

	bool can_PutTurret(){
		return this.money >= 100;
	}

	void setMoney(int takeMoney){
		this.money -= takeMoney;
	}
	
	void setTextGold(){
		playerGold.text = money.ToString();
	}

	public void putMoney(int g){
		money += g;
	}

	void Start(){
		setTextGold ();
	}		

	/* avoid overlaps */
	void onTriggerEnter(Collision other){
		Destroy (other.gameObject);
	}

	// Update is called once per frame
	void Update () {
		/* take the middle point of the screen */
		float myWidth = (float)(Screen.width * 0.25);
		float myHeight = (float)(Screen.height * 0.5);

		/* Print how many golds the player got */
		setTextGold ();

		Vector3 middleScreen = new Vector3 (myWidth, myHeight, 0f);
		RaycastHit hit;
		//Ray rayPlayer = new Ray (transform.position, Vector3.forward);

		Ray cameraShot = c.ScreenPointToRay (middleScreen);
		if(Input.GetKeyDown(KeyCode.B)){
			if (Physics.Raycast (cameraShot, out hit, 100) && (hit.transform.CompareTag("Grid"))){
				Debug.Log("BASE TOUCH");
				Vector3 positionTouched = new Vector3(hit.point.x, hit.point.y + effect.transform.localScale.z/2, hit.point.z);
				if(can_PutTurret()){
					if(is_full(numberOfBase)){
						keyCurrent ="base"+ indiceKey%nombreBaseMax;
						Destroy(((Transform)numberOfBase[keyCurrent]).gameObject);
						numberOfBase.Remove(keyCurrent);

					}
					Object baseClone = Instantiate(effect,positionTouched,Quaternion.LookRotation(hit.normal));

					/*Add BaseCube into numberOfBase HashTable*/
					numberOfBase.Add("base"+indiceKey%nombreBaseMax, ((Transform)baseClone));

					/*Give 100 money to the player*/
					int moneyForPlayer = 100;
					setMoney(moneyForPlayer);

					indiceKey++;
					((Transform)baseClone).renderer.material.color = Color.red;

					/*Give the number you want to create number of cubeTurret*/
					spawnTowerCube(3);
				}
			}
		}
	}



	public void spawnTowerCube(int numberCubeSpawn)
	{
		for (int i = 0; i < numberCubeSpawn; i++) {	
			Transform newTowerCube = Instantiate (towerCubePrefab) as Transform;
			newTowerCube.parent = randomPosition.transform;

			/*avoid to be eveywhere out the map*/
			int bornMap = 25;
			int scaleYMap = 2;

			/*Scale on the y axis of the map */
			newTowerCube.position = new Vector3 (Random.value * 75 - bornMap, scaleYMap, Random.value * 75 - bornMap);
		}
	}


	
}
