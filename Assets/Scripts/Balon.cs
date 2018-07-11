using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balon : MonoBehaviour {

	public bool jawabanBenar = false;
	public int noBalon;

	// Update is called once per frame
	void Update () {
		if (transform.position.y >= 10) {
			Destroy (this.gameObject);
			if (noBalon == 4) {
				MainGameController.GetInstance ().kuisSelesai = true;
				MainGameController.GetInstance ().TambahSkor (-10);
			}
		}
	}

	void NoBalon(int nobal){
		noBalon = nobal;
	}

	void jawaban(bool jawab){
		jawabanBenar = jawab;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("peluru")) {
			Destroy (this.gameObject);
			if (jawabanBenar) {
				MainGameController.GetInstance ().TambahSkor (100);
			} else {
				MainGameController.GetInstance ().TambahSkor (-10);
			}
		}
	}
}