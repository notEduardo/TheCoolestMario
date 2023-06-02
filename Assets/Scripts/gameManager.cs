using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {
	// vanilla variables
	public GameObject playerOBJ;
	public Button startBtn;
	public Text titleLabel;
	public Text timerLabel;
	public Text pointsLabel;

	// procedural variables
	public camerControl cam;
	public GameObject[] maps = new GameObject[25];
	public GameObject fMap;
	public GameObject lMap;
	float mapLength;
	int mapSections;
	int mapChooser;
	int level;
	GameObject[] currentMap = new GameObject[27];

	GameObject flag;
	float finishLine;
	bool finale = false;
	float flagYMax = 1.44f;
	float flagYMin = 0.15f;


	float timer;
	SpriteRenderer playerRend;
	Animator playerAnim;

	public bool gameStarted;
	public int marioState;


	// Use this for initialization
	void Start () {
		gameStarted = false;
		timer = 0f;
		marioState = 0;
		level = 1;

		playerRend = playerOBJ.GetComponent<SpriteRenderer>();
		playerAnim = playerOBJ.GetComponent<Animator>();

		mapLength = 0;
		mapSections = 25;
		mapChooser = 0;
		createMap();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameStarted){
			if(playerOBJ.transform.position.x < cam.camBackEnd){
				gameEnded();
			}

			if(cam.camPos >= (currentMap[level-1]).transform.position.x){
				level += 1;
				pointsLabel.text = level.ToString();
			}

			timer += Time.deltaTime;
			timerLabel.text = ((Mathf.Round(timer*100))/100).ToString();

			if(playerOBJ.transform.position.y < -1){
				gameEnded();
			}
			if(playerOBJ.transform.position.x > finishLine){
				gameWon();
			}

			switch(marioState){
				case 0:
					//playerRend.sprite = sMario;
					playerAnim.SetInteger("marioState", marioState);
					break;
				case 1:
					//playerRend.sprite = lMario;
					playerAnim.SetInteger("marioState", marioState);
					break;
				case 2:
					//playerRend.sprite = godMario;
					playerAnim.SetInteger("marioState", marioState);
					break;
				default:
					Debug.Log("Mario State Wrong");
					break;
			}

		}

		if(finale){
			float percent;

			timer -= Time.deltaTime;
			percent = timer/2;

			percent = ((flagYMax - flagYMin)*percent) + flagYMin;

			flag.transform.position = new Vector3(flag.transform.position.x, percent, -1f);
		}


	}

	public void startGame(){
		startBtn.gameObject.SetActive(false);
		titleLabel.gameObject.SetActive(false);
		timerLabel.gameObject.SetActive(true);
		pointsLabel.gameObject.SetActive(true);

		level = 1;
		pointsLabel.text = level.ToString();

		gameStarted = true;

	}

	public void gameEnded()
	{
		gameStarted = false;

		startBtn.gameObject.SetActive(true);
		titleLabel.gameObject.SetActive(true);
		timerLabel.gameObject.SetActive(false);
		pointsLabel.gameObject.SetActive(false);

		timer = 0f;
		timerLabel.text = "";

		playerOBJ.transform.position = new Vector3(1.2f, 2f, -1f);
		marioState = 0;
		level = 1;
		pointsLabel.text = level.ToString();

		cam.startPos();

		destroyMap();
		createMap();
	}

	void gameWon()
	{
		gameStarted = false;

		timer = 2f;

		finale = true;
		//timerLabel.text = "";
	}

	void createMap(){

		// Place the first Map
		SpriteRenderer mapRender = fMap.GetComponent<SpriteRenderer>(); 
		mapLength += mapRender.bounds.size.x;
		float xPos = (mapLength)/2f;
		Vector3 startPos = new Vector3(xPos, 0f, 0f);

		currentMap[26] = Instantiate(fMap, startPos, Quaternion.identity);

		// Place 25 random Maps
		for(int i = 0; i < mapSections; i++){
			mapChooser = Random.Range(0, mapSections);

			mapRender = (maps[mapChooser]).GetComponent<SpriteRenderer>();
			xPos = (mapRender.bounds.size.x)/2f;
			startPos = new Vector3((mapLength + xPos), 0f, 0f);
			mapLength += mapRender.bounds.size.x;

			currentMap[i] = Instantiate(maps[mapChooser], startPos, Quaternion.identity);
		}

		// Place the last Map
		mapRender = lMap.GetComponent<SpriteRenderer>();
		xPos = (mapRender.bounds.size.x)/2f;
		startPos = new Vector3((mapLength + xPos), 0f, 0f);
		currentMap[25] = Instantiate(lMap, startPos, Quaternion.identity);

		// find flag game marker
		flag = GameObject.FindGameObjectsWithTag("flagOBJ")[0];

		GameObject finishObj = GameObject.FindGameObjectsWithTag("finish")[0];
		finishLine = finishObj.transform.position.x;

		cam.max = mapLength;
	}

	void destroyMap(){
		for(int i = 0; i < currentMap.Length; i++){
			Destroy(currentMap[i]);
		}

		mapLength = 0;
	}
}
