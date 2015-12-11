using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI; 

public class MasterScript : MonoBehaviour {
	//public static MasterScript instance ; 
	Color[ ] AllColors = {Color.green,
		Color.blue,
		Color.yellow,
		Color.magenta,
		Color.red } ;
	public static int xSize = 8; 
	public static int ySize = 5; 
	float cycleTime = 0f ;
	float cycleTime2 = 0f ;
	float turnTime = 2f ;
	int startTime = 1;
	int startCounter = 3;
	int gameOverTime = 64;
	bool go = false;
	bool gameOverLine1 = false;
	bool gameOverLine2 = false; 
	bool gameOverLine3 = false; 
	bool gameOverLine4 = false; 
	bool gameOverLine5  = false; 
	public GameObject cube ;
	public static GameObject [,] AllCubes = new GameObject[xSize,ySize] ; 
	public GameObject colorCube;
	Color nextColor;
	bool keyPressed ;
	int score = 0;
	public static int activeX = 0; // set to null, how? 
	public static int activeY = 0;
	public static bool thereIsCubeActive = false;
	public static Color colorToMove = Color.clear ; 
	public Text scoreUI ;
	public Text timerUI; 
	bool cubeDeathNeeded = false;
	bool gameOver = false; 
	public int cloneActiveX = activeX;
	public int cloneActivey = activeY;
	public  bool cloneThereIsCubeActive = thereIsCubeActive;
	// public int timerCounter = 60; 

	// Use this for initialization
	void Start () { 
		colorCube =  (GameObject) Instantiate (cube, new Vector2 (-3, 5), Quaternion.identity);
		colorCube.GetComponent<Renderer> ().material.color = Color.white;
		
		for (int x = 0; x < (xSize);x++) {
			for (int y = 0; y < (ySize);y++){
				AllCubes[x,y] = (GameObject) Instantiate(cube, new Vector2 ((x)*1.5f,(y)*1.5f), Quaternion.identity) ;
				AllCubes[x,y].GetComponent<Renderer> ().material.color = Color.white ;
				AllCubes[x,y].GetComponent<CubeBehavior> ().CubeLive = true ;
				AllCubes[x,y].GetComponent<CubeBehavior> ().CubeActive = false ; 
				AllCubes[x,y].GetComponent<CubeBehavior> ().CubeColored = false ; 
				AllCubes[x,y].GetComponent<Renderer> ().material.color = Color.white;
				AllCubes[x,y].GetComponent<CubeBehavior> ().x = x ;
				AllCubes[x,y].GetComponent<CubeBehavior> ().y = y ;

				}
			}
		}

	void KillCube (int x, int y) {
		AllCubes [x, y].GetComponent<Renderer> ().material.color = Color.black; 
		AllCubes [x, y].GetComponent<CubeBehavior> ().CubeLive = false ; 
	}
	Color NewColor () {
		return AllColors[Random.Range (0, AllColors.Length)];
	}

	
	void ChangeColorOfCube (int x,int y) {
		if (colorCube.GetComponent<Renderer> ().material.color != Color.white) {
			AllCubes [x, y].GetComponent<Renderer> ().material.color = nextColor;
			colorCube.GetComponent<Renderer> ().material.color = Color.white;
			AllCubes [x, y].GetComponent<CubeBehavior> ().CubeColored = true; 
		}
	}

	public static int ColorNumbers (Color myColor) {
		if (myColor == Color.red) {
			return 1; 
		}
		if (myColor == Color.magenta) {
			return 10; 
		}
		if (myColor == Color.blue) {
			return 100; 
		}
		if (myColor == Color.yellow) {
			return 1000; 
		}
		if (myColor == Color.green) {
			return 10000; 
		} else {
			return 0; 
		}
	}

	bool CheckForSamePlus (int x, int y) { 
		Color targetColor = AllCubes [x, y].GetComponent<Renderer> ().material.color;
		if (targetColor == Color.black || targetColor == Color.white) {
			return false ; 
		}
		if (targetColor == AllCubes [x + 1, y].GetComponent<Renderer> ().material.color &&
			targetColor == AllCubes [x - 1, y].GetComponent<Renderer> ().material.color &&
			targetColor == AllCubes [x, y + 1].GetComponent<Renderer> ().material.color &&
			targetColor == AllCubes [x, y - 1].GetComponent<Renderer> ().material.color) {
			return true; 
		} 
		else {
			return false; 
		}
	}

	 bool CheckForDiffPlus (int x, int y) { 
		Color targetColor = AllCubes [x, y].GetComponent<Renderer> ().material.color;
		int totalColorNumbers = 0; 
		if (targetColor == Color.black || targetColor == Color.white) {
			return false ; 
		}
		totalColorNumbers += ColorNumbers (AllCubes [x , y].GetComponent<Renderer> ().material.color);
		totalColorNumbers += ColorNumbers (AllCubes [x + 1, y].GetComponent<Renderer> ().material.color);
		totalColorNumbers += ColorNumbers (AllCubes [x -1, y].GetComponent<Renderer> ().material.color);
		totalColorNumbers += ColorNumbers (AllCubes [x, y + 1].GetComponent<Renderer> ().material.color);
		totalColorNumbers += ColorNumbers (AllCubes [x, y - 1].GetComponent<Renderer> ().material.color);
		if (totalColorNumbers == 11111) {
			print ("same plus");
			return true; 
		} 
		else {
			return false ;
			}
		}
		 

	//checks for pluses and if it finds them turn them black 
	void ActualizePlus () {
		for (int x = 1; x < xSize-1; x++) {
			for (int y = 1; y < ySize-1; y++) {
				if(CheckForDiffPlus(x,y)){
					KillCube(x,y);
					KillCube(x+1,y);
					KillCube(x-1,y);
					KillCube(x,y+1);
					KillCube(x,y-1);
					score += 5;
				}
				if(CheckForSamePlus(x,y)){
					KillCube(x,y);
					KillCube(x+1,y);
					KillCube(x-1,y);
					KillCube(x,y+1);
					KillCube(x,y-1);
					score += 10;
				}
			}
		}
	}

	void OnKeyInput(){
		int[] validIndexes;
		if (Input.GetKey (KeyCode.Alpha1)) {
			validIndexes = CheckLineAvalibility(0);
			if (gameOverLine1 && validIndexes.Length == 0) {
				gameOver = true;
				return; 
			}
				ChangeColorOfCube(validIndexes[Random.Range (0, validIndexes.Length)], 0);
				print ("key 1 is down");
				keyPressed = true;

		}
		
		if (Input.GetKey (KeyCode.Alpha2)) {
			validIndexes = CheckLineAvalibility(1);
			if (gameOverLine2) {
				gameOver = true;
				return; 
			}
				ChangeColorOfCube(validIndexes[Random.Range (0, validIndexes.Length)],1);
				print ("key 2 is down");
				keyPressed = true; 

		}
		
		if (Input.GetKey (KeyCode.Alpha3)) {
			validIndexes = CheckLineAvalibility(2);
			if (gameOverLine3) {
				gameOver = true;
				return; 
			}
			//if  (validIndexes.Length != 0){
				ChangeColorOfCube(validIndexes[Random.Range (0, validIndexes.Length)],2);
				print ("key 3 is down");
				keyPressed = true;
			//}
		}
		
		if (Input.GetKey (KeyCode.Alpha4)) {
			validIndexes = CheckLineAvalibility(3);
			if (gameOverLine4) {
				gameOver = true;
				return; 
			}
			ChangeColorOfCube(validIndexes[Random.Range (0, validIndexes.Length)],3);
			print ("key 4 is down");
			keyPressed = true;
			
		}
		
		if (Input.GetKey (KeyCode.Alpha5)) {
			validIndexes = CheckLineAvalibility(4);
			if (gameOverLine5) {
				gameOver = true;
				return; 
			}
	
				ChangeColorOfCube (validIndexes[Random.Range (0, validIndexes.Length)],4);
				print ("key 5 is down");
				keyPressed = true;
		
		} 
		
	}


	int[] CheckLineAvalibility (int rowNumber) {
		List<int> validIndexes = new List<int> ();
		for (int x = 0; x < 8;x++) {
			if(AllCubes[x, rowNumber].GetComponent<CubeBehavior>().CubeLive == true && AllCubes[x, rowNumber].GetComponent<CubeBehavior>().CubeColored == false){
				validIndexes.Add (x) ; 
			}
		}
		return validIndexes.ToArray();
	}

	// finds a random live cube and kills it 
	void SearchandDestroy(int a,int b) {
		int deadCubeCount = 0; 
		for (int x = 0; x < (xSize);x++) {
			for (int y = 0; y < (ySize);y++){
				if (AllCubes[x,y].GetComponent<CubeBehavior> ().CubeLive == false) {
					deadCubeCount++; 
				}
			}
		}
		if (deadCubeCount == (xSize * ySize)) {
			cubeDeathNeeded = false;
			gameOver = true; 
		}

		while (cubeDeathNeeded) {
			if (AllCubes[a,b].GetComponent<CubeBehavior> ().CubeLive == true && AllCubes[a,b].GetComponent<CubeBehavior> ().CubeColored == false) {
				KillCube(a,b);
				cubeDeathNeeded = false; 
			}
			else {
				a = Random.Range(0,xSize);
				b = Random.Range(0,ySize);
			}
		}
	} 
	//keeps score above zero 
	void ScoreKeeper (){
		if (score < 0) {
			score = 0;
			 
		}
	}

	bool CheckLineFull (int lineNumber) {
		int lineCount = 0; 
		for (int x = 0; x < xSize; x++) {
			if (AllCubes[x,lineNumber].GetComponent<CubeBehavior> ().CubeLive == false || AllCubes[x,lineNumber].GetComponent<CubeBehavior> ().CubeColored == true) {
				lineCount += 1; 
			}
		}
		if (lineCount == 8) {
			return true; 
		}
		else {
			return false; 
		}
	}


	// Update is called once per frame
	void Update () {
		if (Time.time < 4) {
			cycleTime += Time.deltaTime;
			if (cycleTime > startTime && startCounter > 0) { 
				scoreUI.text = "Game starts in: " + startCounter; 
				startCounter -= 1;
				cycleTime = 0;
				go = true; 
				return;
			}
		} else if (gameOver) {
			scoreUI.text = "You Lose" ;
			return; 
		} else if (Time.time > gameOverTime) {
			if (score > 0) {
				scoreUI.text = "You Win.Final Score: " + score;
			}
			if (score == 0) {
				scoreUI.text = "You Lose.Final Score: " + score;
			}
		}
		else if (!gameOver && Time.time < gameOverTime && Time.time >= 4){ 
			if (go) {
				colorCube.GetComponent<Renderer> ().material.color = NewColor ();
				nextColor = colorCube.GetComponent<Renderer> ().material.color;
				go = false; 
			}
			cycleTime2 += Time.deltaTime; 
			ActualizePlus ();
			OnKeyInput ();
			ScoreKeeper (); 
			scoreUI.text = "Score: " + score;
			if (cycleTime2 > turnTime) { 
				if (keyPressed == false){
					cubeDeathNeeded = true; 
					SearchandDestroy((Random.Range(0,xSize)),(Random.Range(0,ySize))); 
					score--; 
				}
			colorCube.GetComponent<Renderer> ().material.color = NewColor ();
			nextColor = colorCube.GetComponent<Renderer> ().material.color;
			if(CheckLineFull(0)){
					gameOverLine1 = true; 
				}
			if(CheckLineFull(1)){
				gameOverLine2 = true; 
			}
			if(CheckLineFull(2)){
				gameOverLine3 = true; 
			}
			if(CheckLineFull(3)){
				gameOverLine4 = true; 
			}
			if(CheckLineFull(4)){
				gameOverLine5 = true; 
			}
			cycleTime2 = 0;
			keyPressed = false ; 
			return; 
			}
		
		}
	}

}