using UnityEngine;
using System.Collections;

public class PutBase : MonoBehaviour {

	public TextMesh playerGold;

	private double money =100;
	private const int nombreBaseMax = 10;
	public Transform effect;
	private Hashtable numberOfBase= new Hashtable(nombreBaseMax);
	private int indiceKey=0;
	string keyCurrent;

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
		//playerGold.transform.position.x = Screen.width/2;
		//playerGold.transform.position.y = Screen.height - 10f;

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

		Ray cameraShot = Camera.main.ScreenPointToRay (middleScreen);
		if(Input.GetKeyDown(KeyCode.A)){
			if (Physics.Raycast (cameraShot, out hit, 100) && (hit.collider.gameObject.tag == "Grid")){
				Vector3 positionTouched = new Vector3(hit.point.x, hit.point.y - effect.transform.localScale.z/2, hit.point.z);
				if(can_PutTurret()){
					if(is_full(numberOfBase)){
						keyCurrent ="base"+ indiceKey%nombreBaseMax;
						Debug.Log("modulo " + indiceKey%nombreBaseMax);
						Debug.Log ("trop de bases crées");
						Destroy(((Transform)numberOfBase[keyCurrent]).gameObject);
						numberOfBase.Remove(keyCurrent);
						Debug.Log( is_full(numberOfBase));

					}
					Object baseClone = Instantiate(effect,positionTouched,Quaternion.LookRotation(hit.normal));
					numberOfBase.Add("base"+indiceKey%nombreBaseMax, ((Transform)baseClone));
					setMoney(100);
					Debug.Log(indiceKey);
					indiceKey++;
					Debug.Log(numberOfBase);
					((Transform)baseClone).renderer.material.color = Color.red;
				}
			}
		}
	}

	void onTriggerEnter(Collision other){
		Destroy (other.gameObject);
	}
}
