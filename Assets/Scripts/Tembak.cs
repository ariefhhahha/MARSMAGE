using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tembak : MonoBehaviour {

	public AudioSource tembak;
	public GameObject bolaPeluru;
	private GameObject peluru;
	public GameObject kubusnya;

	// Use this for initialization
	public void Menembak(){
		if (GameObject.Find ("MainGameController").GetComponent<MainGameController> ().lagiMain && !GameObject.Find ("MainGameController").GetComponent<MainGameController> ().gameOver && !GameObject.Find ("MainGameController").GetComponent<MainGameController> ().lagiJeda) {
			tembak.Play ();
			peluru = Instantiate (bolaPeluru, (Vector3)this.gameObject.transform.position, Quaternion.identity);
			Rigidbody rb = peluru.GetComponent<Rigidbody> ();
			peluru.transform.rotation = this.gameObject.transform.rotation;
			peluru.transform.position = this.gameObject.transform.position;
			rb.velocity = transform.forward * 120;
			Destroy (peluru, 10);
		}
	}

	//yah kubusnya ilang :(
	public void KembalikanKubus(){
		kubusnya.transform.position = this.gameObject.transform.position + transform.forward*13;
	}
}
