using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using GoogleMobileAds.Android;

public class MainManager : MonoBehaviour {
	
	public int[,] values = new int[,]{{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}};
	public Vector3[,] positions = new Vector3[,]{ {new Vector3(-1.93f, 1.92f, -3.05f), new Vector3(-0.93f, 1.92f, -3.05f), new Vector3(0.04f, 1.92f, -3.05f), new Vector3(0.96f, 1.92f, -3.05f), new Vector3(1.97f, 1.92f, -3.05f)}, 
		{new Vector3(-1.93f, 0.95f, -3.05f), new Vector3(-0.93f, 0.95f, -3.05f), new Vector3(0.04f, 0.95f, -3.05f), new Vector3(0.96f, 0.95f, -3.05f), new Vector3(1.97f, 0.95f, -3.05f)}, 
		{new Vector3(-1.93f, 0f, -3.05f), new Vector3(-0.93f, 0f, -3.05f), new Vector3(0.04f, 0f, -3.05f), new Vector3(0.96f, 0f, -3.05f), new Vector3(1.97f, 0f, -3.05f)},
		{new Vector3(-1.93f, -0.97f, -3.05f), new Vector3(-0.93f, -0.97f, -3.05f), new Vector3(0.04f, -0.97f, -3.05f), new Vector3(0.96f, -0.97f, -3.05f), new Vector3(1.97f, -0.97f, -3.05f)}, 
		{new Vector3(-1.93f, -1.94f, -3.05f), new Vector3(-0.93f, -1.94f, -3.05f), new Vector3(0.04f, -1.94f, -3.05f), new Vector3(0.96f, -1.94f, -3.05f), new Vector3(1.97f, -1.94f, -3.05f)} };
	public GameObject[] Circle = new GameObject[10];
	
	public struct encode{
		public int ltogo;
		public int ctogo;
	};
	
	public encode[,] movementposition= new encode[5, 5];
	public bool[,] movementselected = new bool[5, 5];
	public bool[,] alreadyspawned = new bool[5, 5];
	public bool[,] adiac = new bool[5, 5];
	private float timer = 0.0f;
	public bool GameOver = false;
	public bool spawned = false;	
	public bool maximized = false;
	private int l , c;
	public float[] distances = new float[4];
	public GameObject PauseManager;
	public bool GamePaused = false;
	public bool FinishedMoving;
	public int[,] ComponentValues = new int[5, 5];
	public int Score = 0;
	public int Steps = 0;
	public Text text;
	public Text StepsText;
	public bool HadCollided = false;
	public AudioSource audio;
	public AudioSource GameOverAudio;
	public bool ranGameOverSound = false;
	public Slider VolumeSlider;
	public Canvas GameOverImage;
	public Canvas RetryCanvas;
	public bool accepted;
	public bool didntacceptyet = false;
	public bool finishedad = false;
	public float testtimer = 1.0f;
	public bool fullyshowedretrycanvas = false;
	public Text HighscoreText;
	public Vector3 initialclickposition = Vector3.zero;
	public bool movetoright = false;
	public bool movetoleft = false;
	public bool movetotop = false;
	public bool movetobottom = false;
	public List<Vector3> touchPositions = new List<Vector3>();
	public Vector3 lp;
	public Vector3 fp;
	public float dragDistance;
	public bool touchedmovement = false;
	public bool requestinterstitialvar = false;
	private BannerView bannerView;
	private InterstitialAd interstitial;
	private static string outputMessage = "";
	public bool mustrunad = false;
	public Canvas LoadingAdCanvas;
	public int adused = 0;

	public static string OutputMessage
	{
		set { outputMessage = value; }
	}
	// Use this for initialization
	void Start () {
		GameOverImage.gameObject.SetActive (false);
		RetryCanvas.gameObject.SetActive (false);
		LoadingAdCanvas.gameObject.SetActive (false);
		for (int i = 0; i <= 4; i++)
		for (int j = 1; j <= 4; j++) {
			movementposition[i,j].ltogo = 0;
			movementposition[i,j].ctogo = 0;
		}
		for (int i = 0; i <= 4; i++)
		for (int j = 1; j <= 4; j++) {
			adiac[i, j] = false;
		}
		for (int i = 1; i <= 4; i++)
			distances [i - 1] = Vector3.Distance (positions [0, i], positions [0, 0]);
		VolumeSlider.value = 1;
		dragDistance = Screen.width / 120;
		RequestBanner ();
	}
	
	// Update is called once per frame
	void Update () {
		if (mustrunad == true) {
			ShowInterstitial ();
			if (interstitial.IsLoaded ()) {
				LoadingAdCanvas.gameObject.SetActive (false);
				mustrunad = false;
			}
		}
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		audio.volume = VolumeSlider.value;
		GameOverAudio.volume = VolumeSlider.value;
		GamePaused = PauseManager.GetComponent<CanvasManager> ().paused;
		if (GamePaused == false) {
			timer -= Time.deltaTime;
			if (GameOver == false && timer <= 0.0f && CheckIfMovementHadFinished() == true) {
				CheckIfAllSpheresAreAtTheirPlace();
				text.text = Score.ToString ();
				StepsText.text = Steps.ToString ();
				for (int i = 0; i <= 4; i++)
					for (int j = 0; j <= 4; j++)
						movementselected[i, j] = false;
				if (spawned == false) {
					Spawn ();
				}
				if(HadCollided == true)
				{
					audio.Play();
					HadCollided = false;
				}
				foreach(Touch touch in Input.touches)
				{
					if(touch.phase == TouchPhase.Began)
					{
						fp = touch.position;
						lp = touch.position;
					}
					if(touch.phase == TouchPhase.Moved)
					{
						lp = touch.position;
					}
					if(touch.phase == TouchPhase.Ended)
					{
						//touchedmovement = true;
						if(Mathf.Abs(fp.x - lp.x) > Mathf.Abs(fp.y - lp.y)){
							if(Mathf.Abs(fp.x - lp.x) > dragDistance){
								touchedmovement = true;
							if(lp.x > fp.x) // right movement
							{
								timer = 0.2f;
								spawned = false;
								Steps++;
								//touchedmovement = true;
								RightArrowAction();
							}
							else // left movement
							{
								timer = 0.2f;
								spawned = false;
								Steps++;
								//touchedmovement = true;
								LeftArrowAction();
							}
							}
						}
						else
						{
							if(Mathf.Abs(fp.y - lp.y) > dragDistance){
								touchedmovement = true;
							if(lp.y > fp.y) // movement upside
							{
								timer = 0.2f;
								spawned = false;
								Steps++;
								//touchedmovement = true;
								UpArrowAction();
							}
							else // movement downside
							{
								timer = 0.2f;
								spawned = false;
								Steps++;
								//touchedmovement = true;
								DownArrowAction();
							}
							}
						}
					}
				}
			}
			/*if(Input.GetMouseButtonDown(0))
				initialclickposition = Input.mousePosition;
			if(Input.GetMouseButtonUp(0))
			{
				if(Mathf.Abs(Input.mousePosition.x - initialclickposition.x) > Mathf.Abs(Input.mousePosition.y - initialclickposition.y)){
					if(Input.mousePosition.x > initialclickposition.x) 
						movetoright = true;
					else movetoleft = true;
				}
				else
				{
					if(Input.mousePosition.y > initialclickposition.y)
						movetotop = true;
					else movetoleft = true;
				}
			}*/
		}
		if (GameOver == true) {
			if(ranGameOverSound == false)
			{
				GameOverAction();
				ranGameOverSound = true;
			}
			if(adused == 0){
			if(finishedad == false)
				RetryCanvas.gameObject.SetActive(true);
			RunGameOverAnimation ();
			}
			else 
			{
				if(PlayerPrefs.GetInt("Highscore") < Score)
					PlayerPrefs.SetInt("Highscore", Score);
				HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
				GameOverImage.gameObject.SetActive(true);
			}
		}
	}
	
	void GameOverAction()
	{
		GameOverAudio.Play ();
	}
	
	void RunGameOverAnimation()
	{
		//if (fullyshowedretrycanvas == true) {
		if (didntacceptyet == true) {
			RetryCanvas.gameObject.SetActive (false);
			if (accepted == false) {
				GameOverImage.gameObject.SetActive (true);
				RetryCanvas.gameObject.SetActive (false);
				if(PlayerPrefs.GetInt("Highscore") < Score)
					PlayerPrefs.SetInt("Highscore", Score);
				HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
			} else {
				RunAd ();
				if (finishedad == true) {
					for (int i = 1; i <= 3; i++)
						for (int j = 1; j <= 3; j++)
							values [i, j] = 0;
					accepted = false;
					didntacceptyet = false;
					GameOver = false;
					Spawn ();
					GameOverImage.gameObject.SetActive (false);
					RetryCanvas.gameObject.SetActive (false);
					finishedad = false;
					testtimer = 1.0f;
					adused = 1;
					RetryCanvas.GetComponent<appearenceeffect>().ForAlpha.alpha = 0;
					//fullyshowedretrycanvas = false;
				}
			}
		} //else
		//RetryCanvas.gameObject.SetActive (true);
		
		//}
	}

	//


	void RunAd()
	{
		RequestInterstitial();
		ShowInterstitial ();
		if (mustrunad == true) {
			ShowInterstitial ();
			if (interstitial.IsLoaded ()){
				LoadingAdCanvas.gameObject.SetActive(false);
				mustrunad = false;
			}
		}
		testtimer -= Time.deltaTime;
		if(testtimer <= 0.0f)
			finishedad = true;
	}
	
	public void OnClickForAd(string x)
	{
		if (string.Equals (x, "Yes") == true) {
			RequestInterstitial();
			ShowInterstitial();
			LoadingAdCanvas.gameObject.SetActive(true);
			mustrunad = true;
			accepted = true;
			didntacceptyet = true;
		} else {
			accepted = false;
			didntacceptyet = true;
		}
	}
	
	public bool CheckIfMovementHadFinished()
	{
		for (int i = 0; i <= 4; i++)
			for (int j = 0; j <= 4; j++)
				if (movementselected [i, j] == true)
					return false;
		return true;
	}
	
	void CheckIfAllSpheresAreAtTheirPlace()
	{
		for (int i = 0; i <= 4; i++)
			for (int j = 0; j <= 4; j++)
			if ((i == 0 || i == 4 || j == 0 || j == 4) && values [i, j] != ComponentValues [i, j]) {
				if(values[i, j] == 1)
					Instantiate (Circle [0], positions [i, j], Quaternion.identity);
				if(values[i, j] == 2)
					Instantiate (Circle [1], positions [i, j], Quaternion.identity);
				if(values[i, j] == 4)
					Instantiate (Circle [2], positions [i, j], Quaternion.identity);
				if(values[i, j] == 8)
					Instantiate (Circle [3], positions [i, j], Quaternion.identity);
				if(values[i, j] == 16)
					Instantiate (Circle [4], positions [i, j], Quaternion.identity);
				if(values[i, j] == 32)
					Instantiate (Circle [5], positions [i, j], Quaternion.identity);
				if(values[i, j] == 64)
					Instantiate (Circle [6], positions [i, j], Quaternion.identity);
				if(values[i, j] == 128)
					Instantiate (Circle [7], positions [i, j], Quaternion.identity);
				if(values[i, j] == 256)
					Instantiate (Circle [8], positions [i, j], Quaternion.identity);
				if(values[i, j] == 512)
					Instantiate (Circle [9], positions [i, j], Quaternion.identity);
			}
	}
	
	void Spawn(){
		l = (int)(Random.Range (1, 4));
		c = (int)(Random.Range (1, 4));
		Instantiate(Circle[0], positions[l, c], Quaternion.identity);
		spawned = true;
	}
	
	void UpArrowAction(){
		if (values [0, c] == 0 || values [0, c] == 1) {
			if(values[0, c] == 1){
				Score += 2;
				HadCollided = true;
			}
			values [0, c] ++;
			movementposition [l, c].ltogo = 0;
			movementposition [l, c].ctogo = c;
			movementselected[l, c] = true;
		} else {
			GameOver = true;
			movementposition [l, c].ltogo = 1;
			movementposition [l, c].ctogo = c;
			movementselected[l, c] = true;
			//Score++;
		}
		//left line move upside
		int i, j, lastmaximized = -1;
		for (i = 1; i <= 4; i++) {
			if(values[i, 0] != 0) {
				j = i;
				if(j > 0){
					while( j > 0 && values[j-1, 0] == 0)
						j--;
					if( j > 0 && values[j-1, 0] == values[i, 0] && lastmaximized < j - 1)
						j--;
				}
				if(j < i)
				{
					if(values[j, 0] == values[i, 0]){
						lastmaximized = j;
						HadCollided = true;
					}
					values[j, 0] += values[i, 0];
					values[i, 0] = 0;
					Score += values[j,0];
					movementposition[i, 0].ltogo = j;
					movementposition[i, 0].ctogo = 0;
					movementselected[i, 0] = true;
				}
			}
		}
		//rightline move upside
		lastmaximized = -1;
		for (i = 1; i <= 4; i++) {
			if(values[i, 4] != 0) {
				j = i;
				if(j > 0){
					while( j > 0 && values[j-1, 4] == 0)
						j--;
					if( j > 0 && values[j-1, 4] == values[i, 4] && lastmaximized < j - 1)
						j--;
				}
				if(j < i)
				{
					if(values[j, 4] == values[i, 4]){
						lastmaximized = j;
						HadCollided = true;
					}
					values[j, 4] += values[i, 4];
					values[i, 4] = 0;
					Score += values[j, 4];
					movementposition[i, 4].ltogo = j;
					movementposition[i, 4].ctogo = 4;
					movementselected[i, 4] = true;
				}
			}
		}
	}
	
	void DownArrowAction(){
		if (values [4, c] == 0 || values [4, c] == 1) {
			if(values[4, c] == 1){
				Score += 2;
				HadCollided = true;
			}
			values [4, c] ++;
			movementposition [l, c].ltogo = 4;
			movementposition [l, c].ctogo = c;
			movementselected[l, c] = true;
		} else {
			GameOver = true;
			movementposition [l, c].ltogo = 3;
			movementposition [l, c].ctogo = c;
			movementselected[l, c] = true;
			//	Score++;
		}
		//left line move downside
		int i, j, lastmaximized = 5;
		for (i = 3; i >= 0; i--) {
			if(values[i, 0] != 0) {
				j = i;
				while( j < 4 && values[j + 1, 0] == 0)
					j++;
				if( j < 4 && values[j + 1, 0] == values[i, 0] && lastmaximized > j + 1)
					j++;
				if(j > i)
				{
					if(values[j, 0] == values[i, 0]){
						lastmaximized = j;
						HadCollided = true;
					}
					values[j, 0] += values[i, 0];
					values[i, 0] = 0;
					Score += values[j, 0];
					movementposition[i, 0].ltogo = j;
					movementposition[i, 0].ctogo = 0;
					movementselected[i, 0] = true;
				}
			}
		}
		//right line move downside
		lastmaximized = 5;
		for (i = 3; i >= 0; i--) {
			if (values [i, 4] != 0) {
				j = i;
				while (j < 4 && values[j + 1, 4] == 0)
					j++;
				if (j < 4 && values [j + 1, 4] == values [i, 4] && lastmaximized > j + 1)
					j++;
				if (j > i) {
					if (values [j, 4] == values [i, 4]){
						lastmaximized = j;
						HadCollided = true;
					}
					values [j, 4] += values [i, 4];
					values [i, 4] = 0;
					Score += values[j, 4];
					movementposition [i, 4].ltogo = j;
					movementposition [i, 4].ctogo = 4;
					movementselected [i, 4] = true;
				}
			}
		}
	}
	
	void RightArrowAction(){
		if (values [l, 4] == 0 || values [l, 4] == 1) {
			if(values[l, 4] == 1){
				Score += 2;
				HadCollided = true;
			}
			values [l, 4] ++;
			movementposition [l, c].ltogo = l;
			movementposition [l, c].ctogo = 4;
			movementselected[l, c] = true;
		} else {
			GameOver = true;
			movementposition [l, c].ltogo = l;
			movementposition [l, c].ctogo = 3;
			movementselected[l, c] = true;
			//Score++;
		}
		
		
		// up line movement to right
		int i, j, lastmaximized = 5;
		for (i = 3; i >= 0; i--) {
			if(values[0, i] != 0) {
				j = i;
				while( j < 4 && values[0, j + 1] == 0)
					j++;
				if( j < 4 && values[0, j + 1] == values[0, i] && lastmaximized > j + 1)
					j++;
				if(j > i)
				{
					if(values[0, j] == values[0, i]){
						lastmaximized = j;
						HadCollided = true;
					}
					values[0, j] += values[0, i];
					values[0, i] = 0;
					Score += values[0, j];
					movementposition[0, i].ltogo = 0;
					movementposition[0, i].ctogo = j;
					movementselected[0, i] = true;
				}
			}
		}
		// down line movement to right
		lastmaximized = 5;
		for (i = 3; i >= 0; i--) {
			if(values[4, i] != 0) {
				j = i;
				while( j < 4 && values[4, j + 1] == 0)
					j++;
				if( j < 4 && values[4, j + 1] == values[4, i] && lastmaximized > j + 1)
					j++;
				if(j > i)
				{
					if(values[4, j] == values[4, i]){
						lastmaximized = j;
						HadCollided = true;
					}
					values[4, j] += values[4, i];
					values[4, i] = 0;
					Score += values[4, j];
					movementposition[4, i].ltogo = 4;
					movementposition[4, i].ctogo = j;
					movementselected[4, i] = true;
				}
			}
		}
	}
	
	void LeftArrowAction(){
		if (values [l, 0] == 0 || values [l, 0] == 1) {
			if(values[l, 0] == 1){
				Score += 2;
				HadCollided = true;
			}
			values [l, 0] ++;
			movementposition [l, c].ltogo = l;
			movementposition [l, c].ctogo = 0;
			movementselected[l, c] = true;
		} else {
			GameOver = true;
			movementposition [l, c].ltogo = l;
			movementposition [l, c].ctogo = 1;
			movementselected[l, c] = true;
			//Score++;
		}
		
		// up line movement to left
		int i, j, lastmaximized = -1;
		for (i = 1; i <= 4; i++) {
			if(values[0, i] != 0) {
				j = i;
				if(j > 0){
					while( j > 0 && values[0, j - 1] == 0)
						j--;
					if( j > 0 && values[0, j - 1] == values[0, i] && lastmaximized < j - 1)
						j--;
				}
				if(j < i)
				{
					if(values[0, j] == values[0, i]){
						lastmaximized = j;
						HadCollided = true;
					}
					values[0, j] += values[0, i];
					values[0, i] = 0;
					Score += values[0,j];
					movementposition[0, i].ltogo = 0;
					movementposition[0, i].ctogo = j;
					movementselected[0, i] = true;
				}
			}
		}
		
		lastmaximized = -1;
		
		for (i = 1; i <= 4; i++) {
			if(values[4, i] != 0) {
				j = i;
				if(j > 0){
					while( j > 0 && values[4, j - 1] == 0)
						j--;
					if( j > 0 && values[4, j - 1] == values[4, i] && lastmaximized < j - 1)
						j--;
				}
				if(j < i)
				{
					if(values[4, j] == values[4, i]){
						lastmaximized = j;
						HadCollided = true;
					}
					values[4, j] += values[4, i];
					values[4, i] = 0;
					Score += values[4, j];
					movementposition[4, i].ltogo = 4;
					movementposition[4, i].ctogo = j;
					movementselected[4, i] = true;
				}
			}
		}
	}

	private void RequestBanner()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-6762592788015310/6291988380";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-6762592788015310/6291988380";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		bannerView.LoadAd(createAdRequest());
	}
	
	private void RequestInterstitial()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-6762592788015310/1722187989";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-6762592788015310/1722187989";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		GoogleMobileAdsDemoHandler handler = new GoogleMobileAdsDemoHandler();
		interstitial.SetInAppPurchaseHandler(handler);
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
	}
	
	// Returns an ad request with custom ad targeting.
	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword("game")
				.SetGender(Gender.Male)
				.SetBirthday(new System.DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
		
	}
	
	private void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			print("Interstitial is not ready yet.");
		}
	}
	
	#region Banner callback handlers
	
	public void HandleAdLoaded(object sender, System.EventArgs args)
	{
		print("HandleAdLoaded event received.");
	}
	
	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}
	
	public void HandleAdOpened(object sender, System.EventArgs args)
	{
		print("HandleAdOpened event received");
	}
	
	void HandleAdClosing(object sender, System.EventArgs args)
	{
		print("HandleAdClosing event received");
	}
	
	public void HandleAdClosed(object sender, System.EventArgs args)
	{
		print("HandleAdClosed event received");
	}
	
	public void HandleAdLeftApplication(object sender, System.EventArgs args)
	{
		print("HandleAdLeftApplication event received");
	}
	
	#endregion
	
	#region Interstitial callback handlers
	
	public void HandleInterstitialLoaded(object sender, System.EventArgs args)
	{
		print("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}
	
	public void HandleInterstitialOpened(object sender, System.EventArgs args)
	{
		print("HandleInterstitialOpened event received");
	}
	
	void HandleInterstitialClosing(object sender, System.EventArgs args)
	{
		print("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, System.EventArgs args)
	{
		print("HandleInterstitialClosed event received");
	}
	
	public void HandleInterstitialLeftApplication(object sender, System.EventArgs args)
	{
		print("HandleInterstitialLeftApplication event received");
	}
	
	#endregion
	
}
