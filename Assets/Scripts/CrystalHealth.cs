using UnityEngine;
using System.Collections;

public class CrystalHealth : MonoBehaviour {

	public float crystalHealth;
	private const float maxHealth = 100.0f;

	// Use this for initialization
	void Start () {
		crystalHealth = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		if ( crystalHealth != maxHealth) {
			if (Dead ()) {
				GameObject.Destroy(gameObject);
			}
		}
	}

	bool Dead(){
		return crystalHealth <= 0.0f;
	}

	void ApplyDamage(float damage){
		crystalHealth -= damage;
		if (crystalHealth < 0) {
				crystalHealth = 0.0f;
		}
	}

}
