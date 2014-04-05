using UnityEngine;
using System.Collections;

public class BarCrystalHealth : MonoBehaviour {

	Vector2 pos=new Vector2(20,40);
	Vector2 size=new Vector2(80,20);
	
	public Texture2D supBarHealth;
	public Texture2D barHealth;

	private float lifeLeft;

	void Update () {
		lifeLeft = GameObject.Find("Crystal").GetComponent<CrystalHealth>().crystalHealth ;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(10, 20, barHealth.width * lifeLeft / 100, barHealth.height), barHealth);
		GUI.DrawTexture(new Rect(10, 20, supBarHealth.width, barHealth.height), supBarHealth);
	}
}