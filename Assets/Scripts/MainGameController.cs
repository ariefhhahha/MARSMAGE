using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour {

	private static MainGameController instance;

	//Ngatur canvas
	public GameObject canvasMenu;
	public GameObject canvasCaraBermain;
	public GameObject canvasUIGame;
	public GameObject canvasSkor;
	public GameObject canvasTentang;
	public GameObject canvasNotifResetSkor;
	public GameObject canvasNotifKeluarApp;
	public GameObject canvasJeda;
	public GameObject canvasGameOver;

	//ngatur teks skor
	public GameObject txtskor1;
	public GameObject txtskor2;
	public GameObject txtskor3;
	public GameObject txtskor4;
	public GameObject txtskor5;

	//buat nampung nilai skor
	int skor1, skor2, skor3, skor4, skor5;

	/*ngatur status di permainan*/
	//cek dalam keadaan lagi main atau tidak. nilai awalnya false
	public bool lagiMain = false;
	//cek permainan udah beres tau enggak. nilai awalnya false
	public bool gameOver = false;
	//cek soal yang muncul udah dijawab atau belom. nilai awalnya false
	public bool kuisSelesai = false;
	//lagi jeda atau tidak
	public bool lagiJeda = false;

	//buat audio
	public AudioSource klik;
	public AudioSource tembak;
	public AudioSource munculQuiz;
	public AudioSource balonMuncul;
	public AudioSource jawabBenar;
	public AudioSource jawabSalah;


	/*buat nampung objek di UI game-nya*/
	//notif jawaban
	public GameObject notifJawabBenar;
	public GameObject notifJawabSalah;
	//kubus
	public GameObject kubus;
	//prebaf peluru
	public GameObject bolaPeluru;
	//instantiate peluru
	public GameObject peluru;
	//4 balon jawaban
	public GameObject balon1pref;
	public GameObject balon2pref;
	public GameObject balon3pref;
	public GameObject balon4pref;
	//3 lambang nyawa (hati)
	public GameObject nyawa1;
	public GameObject nyawa2;
	public GameObject nyawa3;
	//buat instantiate balon jawaban;
	private GameObject balonJawab1;
	private GameObject balonJawab2;
	private GameObject balonJawab3;
	private GameObject balonJawab4;
	//teks buat skor di UI gameplay
	public Text skorGame;
	//teks baut skor di canvas gameover
	public Text skorCanvasGameOver;
	//teks buat soal di-kubus
	private GameObject[] txtKuis;
	//teks buat jawaban dibalon-balon
	private GameObject[] txtBalon1;
	private GameObject[] txtBalon2;
	private GameObject[] txtBalon3;
	private GameObject[] txtBalon4;

	//ngambil posisi lobang buat spawn balon
	public Transform lobang1;
	public Transform lobang2;
	public Transform lobang3;
	public Transform lobang4;

	//variable lain-lain
	int skorMain;
	const int nyawaMax = 3;
	int nyawaSekarang = nyawaMax;
	string kuisKubus = "Lihat Kesini";
	int operasi = 0;
	int x = 0;
	int y = 0;
	int jawaban = 0;
	int jawabanAwal = 0;
	int pilihBalonJawaban = 0;

	// Use this for initialization
	void Start () {
		instance = this;



		canvasMenu.SetActive (true);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);

		skorMain = 0;
	}
	
	// Update is called once per frame
	void Update () {
		skor1 = PlayerPrefs.GetInt ("skorTertinggi1");
		skor2 = PlayerPrefs.GetInt ("skorTertinggi2");
		skor3 = PlayerPrefs.GetInt ("skorTertinggi3");
		skor4 = PlayerPrefs.GetInt ("skorTertinggi4");
		skor5 = PlayerPrefs.GetInt ("skorTertinggi5");
		if (lagiMain) {
			skorGame.text = skorMain.ToString ();
			if (lagiJeda == false) {
				Time.timeScale = 1;
			} else {
				Time.timeScale = 0;
			}
			kubus.transform.eulerAngles += new Vector3 (0, 20 * Time.deltaTime, 0);
			txtKuis = GameObject.FindGameObjectsWithTag ("txtkuis");
			foreach (GameObject kuis in txtKuis) {
				kuis.GetComponent<TextMesh> ().text = kuisKubus;
			}
			if (kuisSelesai && !gameOver) {
				StartCoroutine (TungguBikinKuisBaru ());
				kuisSelesai = false;
			} else if (!kuisSelesai) {
				
			}
			if (nyawaSekarang < 3) {
				nyawa3.SetActive (false);
			}
			if (nyawaSekarang < 2) {
				nyawa2.SetActive (false);
			}
			if (nyawaSekarang < 1) {
				nyawa1.SetActive (false);
				gameOver = true;
			}

			if (gameOver == true) {
				Time.timeScale = 0;
				if (skorMain > skor5) {
					skorCanvasGameOver.GetComponent<Text> ().text = "Skor Baru : " + skorMain.ToString ();
				} else {
					skorCanvasGameOver.GetComponent<Text> ().text = skorMain.ToString ();
				}
				canvasGameOver.SetActive (true);
			}
		}
		txtskor1.GetComponent<Text>().text = skor1.ToString();
		txtskor2.GetComponent<Text>().text = skor2.ToString();
		txtskor3.GetComponent<Text>().text = skor3.ToString();
		txtskor4.GetComponent<Text>().text = skor4.ToString();
		txtskor5.GetComponent<Text>().text = skor5.ToString();
	}

	/*
	 *      MAIN MENU
	 */
	public void Mainkan(){
		klik.Play ();
		canvasMenu.SetActive (false);
		canvasCaraBermain.SetActive (true);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
		nyawa1.SetActive (true);
		nyawa2.SetActive (true);
		nyawa3.SetActive (true);
	}

	public void MulaiMain(){
		klik.Play ();
		canvasMenu.SetActive (false);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (true);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);

		nyawa1.SetActive (true);
		nyawa2.SetActive (true);
		nyawa3.SetActive (true);

		kuisSelesai = false;
		lagiJeda = false;
		lagiMain = true;
		gameOver = false;
		kuisKubus = "Lihat Kesini"; 
		skorMain = 0;
		nyawaSekarang = 3;
		StartCoroutine (TungguBikinKuisBaru ());
	}

	public void Skor(){
		klik.Play ();
		txtskor1.GetComponent<Text>().text = skor1.ToString();
		txtskor2.GetComponent<Text>().text = skor2.ToString();
		txtskor3.GetComponent<Text>().text = skor3.ToString();
		txtskor4.GetComponent<Text>().text = skor4.ToString();
		txtskor5.GetComponent<Text>().text = skor5.ToString();
			
		canvasMenu.SetActive (false);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (true);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
	}

	public void Tentang(){
		klik.Play ();
		canvasMenu.SetActive (false);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (true);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
	}

	public void KembaliKeMenu(){
		klik.Play ();
		canvasMenu.SetActive (true);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
	}

	public void Keluar(){
		klik.Play ();
		canvasMenu.SetActive (false);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (true);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
	}

	/*
	 * Notifikasi Reset Skor
	 */
	public void AturUlangSkor(){
		klik.Play ();
		canvasMenu.SetActive (false);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (true);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
	}

	public void KonfirmasiYaResetSkor(){
		klik.Play ();
		PlayerPrefs.SetInt ("skorTertinggi1", 0);
		PlayerPrefs.SetInt ("skorTertinggi2", 0);
		PlayerPrefs.SetInt ("skorTertinggi3", 0);
		PlayerPrefs.SetInt ("skorTertinggi4", 0);
		PlayerPrefs.SetInt ("skorTertinggi5", 0);

		canvasMenu.SetActive (false);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (true);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
	}

	public void KonfirmasiTidakResetSkor(){
		klik.Play ();
		canvasMenu.SetActive (false);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (true);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
	}

	/*
	 *  Notifikasi Keluar Aplikasi
	 */
	public void KonfirmasiYaKeluarApp(){
		klik.Play ();
		Application.Quit ();
	}

	public void KonfirmasiTidakKeluarApp(){
		klik.Play ();
		canvasMenu.SetActive (true);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
	}

	/*
	 *  DALAM GAMEPLAY
	 */


	//Bagian Jeda
	public void Jeda(){
		klik.Play ();
		notifJawabBenar.SetActive (false);
		notifJawabSalah.SetActive (false);
		lagiJeda = true;
		canvasJeda.SetActive (true);
	}
	public void LanjutkanBermain(){
		klik.Play ();
		Destroy (balonJawab1);
		Destroy (balonJawab2);
		Destroy (balonJawab3);
		Destroy (balonJawab4);

		kuisSelesai = true;
		lagiJeda = false;
		canvasJeda.SetActive (false);
	}
	public void BerhentiBermain(){
		klik.Play ();
		Destroy (balonJawab1);
		Destroy (balonJawab2);
		Destroy (balonJawab3);
		Destroy (balonJawab4);
		if (skorMain > skor5) {
			skorCanvasGameOver.GetComponent<Text> ().text = "Skor Baru : " + skorMain.ToString ();
		} else {
			skorCanvasGameOver.GetComponent<Text> ().text = skorMain.ToString ();
		}
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (true);
	}

	//Bagian Gameover
	public void UlangiPermainan(){
		klik.Play();
		Destroy (balonJawab1);
		Destroy (balonJawab2);
		Destroy (balonJawab3);
		Destroy (balonJawab4);
		nyawa1.SetActive (true);
		nyawa2.SetActive (true);
		nyawa3.SetActive (true);

		if (skorMain > skor1) {
			PlayerPrefs.SetInt ("skorTertinggi1", skorMain);
		} else if (skorMain > skor2) {
			PlayerPrefs.SetInt ("skorTertinggi2", skorMain);
		} else if (skorMain > skor3) {
			PlayerPrefs.SetInt ("skorTertinggi3", skorMain);
		} else if (skorMain > skor4) {
			PlayerPrefs.SetInt ("skorTertinggi4", skorMain);
		} else if (skorMain > skor5) {
			PlayerPrefs.SetInt ("skorTertinggi5", skorMain);
		}

		kuisSelesai = false;
		lagiJeda = false;
		lagiMain = true;
		gameOver = false;
		kuisKubus = "Lihat Kesini"; 
		nyawaSekarang = 3;
		skorMain = 0;
		StartCoroutine (TungguBikinKuisBaru ());
		canvasGameOver.SetActive(false);
	}
	public void TidakUlangiPermainan(){
		klik.Play();
		canvasMenu.SetActive (true);
		canvasCaraBermain.SetActive (false);
		canvasUIGame.SetActive (false);
		canvasSkor.SetActive (false);
		canvasTentang.SetActive (false);
		canvasNotifResetSkor.SetActive (false);
		canvasNotifKeluarApp.SetActive (false);
		canvasJeda.SetActive (false);
		canvasGameOver.SetActive (false);
		Destroy (balonJawab1);
		Destroy (balonJawab2);
		Destroy (balonJawab3);
		Destroy (balonJawab4);

		if (skorMain > skor1) {
			PlayerPrefs.SetInt ("skorTertinggi5", PlayerPrefs.GetInt("skorTertinggi4"));
			PlayerPrefs.SetInt ("skorTertinggi4", PlayerPrefs.GetInt("skorTertinggi3"));
			PlayerPrefs.SetInt ("skorTertinggi3", PlayerPrefs.GetInt("skorTertinggi2"));
			PlayerPrefs.SetInt ("skorTertinggi2", PlayerPrefs.GetInt("skorTertinggi1"));

			PlayerPrefs.SetInt ("skorTertinggi1", skorMain);
		} else if (skorMain > skor2) {
			PlayerPrefs.SetInt ("skorTertinggi5", PlayerPrefs.GetInt("skorTertinggi4"));
			PlayerPrefs.SetInt ("skorTertinggi4", PlayerPrefs.GetInt("skorTertinggi3"));
			PlayerPrefs.SetInt ("skorTertinggi3", PlayerPrefs.GetInt("skorTertinggi2"));

			PlayerPrefs.SetInt ("skorTertinggi2", skorMain);
		} else if (skorMain > skor3) {
			PlayerPrefs.SetInt ("skorTertinggi5", PlayerPrefs.GetInt("skorTertinggi4"));
			PlayerPrefs.SetInt ("skorTertinggi4", PlayerPrefs.GetInt("skorTertinggi3"));

			PlayerPrefs.SetInt ("skorTertinggi3", skorMain);
		} else if (skorMain > skor4) {
			PlayerPrefs.SetInt ("skorTertinggi5", PlayerPrefs.GetInt("skorTertinggi4"));

			PlayerPrefs.SetInt ("skorTertinggi4", skorMain);
		} else if (skorMain > skor5) {
			PlayerPrefs.SetInt ("skorTertinggi5", skorMain);
		}

		lagiMain = false;
	}

	public void TambahSkor(int skor){
		if (lagiMain && !gameOver) {
			if (skor == 100) {
				jawabBenar.Play ();
				notifJawabBenar.SetActive (true);
				Destroy (balonJawab1);
				Destroy (balonJawab2);
				Destroy (balonJawab3);
				Destroy (balonJawab4);
				kuisSelesai = true;
				skorMain += skor;
			} else if (skor == -10) {
				jawabSalah.Play ();
				notifJawabSalah.SetActive (true);
				nyawaSekarang -= 1;
				Destroy (balonJawab1);
				Destroy (balonJawab2);
				Destroy (balonJawab3);
				Destroy (balonJawab4);
				kuisSelesai = true;
				if (skorMain > 0) {
					skorMain += skor;
				}
			}
		}
	}

	//tunggu selama 3 detik dulu pas baru main sebelom nampilin soal
	//abis itu, tunggu selama 4 detik dulu baru muncul balon jawabannya
	IEnumerator TungguBikinKuisBaru(){
		yield return  new WaitForSecondsRealtime (3);
		munculQuiz.Play ();
		notifJawabBenar.SetActive (false);
		notifJawabSalah.SetActive (false);
		BikinKuis ();

		pilihBalonJawaban = Random.Range (1, 4);
		//bikin balon jawaban
		balonJawab1 = (GameObject) Instantiate (balon1pref, (Vector3)lobang1.position, Quaternion.Euler(270,0,0));
		balonJawab1.transform.SetParent (lobang1);
		txtBalon1 = GameObject.FindGameObjectsWithTag ("jawab1");
		balonJawab2 = (GameObject) Instantiate (balon2pref, (Vector3)lobang2.position, Quaternion.Euler(270,0,0));
		balonJawab2.transform.SetParent (lobang2);
		txtBalon2 = GameObject.FindGameObjectsWithTag ("jawab2");
		balonJawab3 = (GameObject) Instantiate (balon3pref, (Vector3)lobang3.position, Quaternion.Euler(270,0,0));
		balonJawab3.transform.SetParent (lobang3);
		txtBalon3 = GameObject.FindGameObjectsWithTag ("jawab3");
		balonJawab4 = (GameObject) Instantiate (balon4pref, (Vector3)lobang4.position, Quaternion.Euler(270,0,0));
		balonJawab4.transform.SetParent (lobang4);
		txtBalon4 = GameObject.FindGameObjectsWithTag ("jawab4");

		//BALON 1
		if (!kuisSelesai) {
			yield return new WaitForSecondsRealtime (4);
			balonJawab1.GetComponent<Rigidbody> ().AddForce (Vector3.up * 30);
			balonMuncul.Play ();
			if (pilihBalonJawaban == 1) {
				balonJawab1.SendMessage ("jawaban", true);
				foreach (GameObject tb1 in txtBalon1) {
					tb1.GetComponent<TextMesh> ().text = jawaban.ToString ();
				}
			} else {
				int jawabanAcak = Random.Range (jawaban - 11, jawaban + 12);
				if (jawaban == jawabanAcak) {
					jawabanAcak += 1;
				}
				foreach (GameObject tb1 in txtBalon1) {
					tb1.GetComponent<TextMesh> ().text = jawabanAcak.ToString ();
				}
			}
		}

		//tunggu 2 detik sebelom balon selanjutnya spawn
		//BALON 2
		if (!kuisSelesai) {
			yield return new WaitForSecondsRealtime (3);
			balonJawab2.GetComponent<Rigidbody> ().AddForce (Vector3.up * 30);
			balonMuncul.Play ();
			if (pilihBalonJawaban == 2) {
				balonJawab2.SendMessage ("jawaban", true);
				foreach (GameObject tb2 in txtBalon2) {
					tb2.GetComponent<TextMesh> ().text = jawaban.ToString ();
				}
			} else {
				int jawabanAcak = Random.Range (jawaban - 11, jawaban + 12);
				if (jawaban == jawabanAcak) {
					jawabanAcak += 1;
				}
				foreach (GameObject tb2 in txtBalon2) {
					tb2.GetComponent<TextMesh> ().text = jawabanAcak.ToString ();
				}
			}
		}

		//tunggu 2 detik sebelom balon selanjutnya spawn
		//BALON 3
		if (!kuisSelesai) {
			yield return new WaitForSecondsRealtime (3);
			balonJawab3.GetComponent<Rigidbody> ().AddForce (Vector3.up * 30);
			balonMuncul.Play ();
			if (pilihBalonJawaban == 3) {
				balonJawab3.SendMessage ("jawaban", true);
				foreach (GameObject tb3 in txtBalon3) {
					tb3.GetComponent<TextMesh> ().text = jawaban.ToString ();
				}
			} else {
				int jawabanAcak = Random.Range (jawaban - 11, jawaban + 12);
				if (jawaban == jawabanAcak) {
					jawabanAcak += 1;
				}
				foreach (GameObject tb3 in txtBalon3) {
					tb3.GetComponent<TextMesh> ().text = jawabanAcak.ToString ();
				}
			}
		}

		//tunggu 2 detik sebelom balon selanjutnya spawn
		//BALON 4
		if (!kuisSelesai) {
			yield return new WaitForSecondsRealtime (3);
			balonJawab4.GetComponent<Rigidbody> ().AddForce (Vector3.up * 30);
			balonMuncul.Play ();
			balonJawab4.SendMessage ("NoBalon", 4);
			if (pilihBalonJawaban == 4) {
				balonJawab4.SendMessage ("jawaban", true);
				foreach (GameObject tb4 in txtBalon4) {
					tb4.GetComponent<TextMesh> ().text = jawaban.ToString ();
				}
			} else {
				int jawabanAcak = Random.Range (jawaban - 11, jawaban + 12);
				if (jawaban == jawabanAcak) {
					jawabanAcak += 1;
				}
				foreach (GameObject tb4 in txtBalon4) {
					tb4.GetComponent<TextMesh> ().text = jawabanAcak.ToString ();
				}
			}
		}
	}
		
	//bikin kuisnya disini
	public void BikinKuis(){
		operasi = Random.Range (1, 4);
		if (operasi == 1) { //tambah
			x = Random.Range (-10, 10);
			y = Random.Range (-10, 10);
			kuisKubus = x.ToString () + " + " + y.ToString ();
			jawaban = x + y;
		} else if (operasi == 2) { //kurang
			x = Random.Range (-10, 10);
			y = Random.Range (-10, 10);
			kuisKubus = x.ToString () + " - " + y.ToString ();
			jawaban = x - y;
		} else if (operasi == 3) { //kali
			x = Random.Range (-10, 10);
			y = Random.Range (-10, 10);
			kuisKubus = x.ToString () + " x " + y.ToString ();
			jawaban = x * y;
		} else if (operasi == 4) { //bagi
			x = Random.Range (-10, 10);
			y = Random.Range (-10, 10);
			jawabanAwal = x * y; //x jadi jawaban kuis
			kuisKubus = jawabanAwal.ToString () + " / " + y.ToString ();
			jawaban = x;
		}
	}

	//jadiin objek ini sebagai instance
	public static MainGameController GetInstance(){
		return instance;
	}
}
