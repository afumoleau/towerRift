using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	private int maxHealth=100;
	private float health = 100f;
	public TextMesh barHealth;
	private GameObject player; 

	// Update is called once per frame
	void Update () {
		if (get_Health () <= 0){
			player = GameObject.Find("Player");
			if( player ) Debug.Log ("Player found");
			PutBase playerBase = ((PutBase)player.GetComponent ("PutBase"));
			if( playerBase ) Debug.Log ("playerBase Found");
			playerBase.putMoney (100);
			Dead();
		}
	}

	void ApplyDamage ( float damage){
		set_Health (get_Health() - damage);
		if (get_Health() <= 0) {
				set_Health (0);
		}			

		barHealth.text = get_Health().ToString()+ "/"+ maxHealth;
	}


	void Dead(){
		Destroy(this.gameObject);
	}

	float get_Health(){
		return this.health;
	}

	void set_Health(float a){
		this.health = a;
	}

}
