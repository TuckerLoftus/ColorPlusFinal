using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	public bool CubeActive; 
	public bool CubeLive;
	public bool CubeColored;
	public int x,y; 


	void PaintItWhite (int a,int b){
		MasterScript.AllCubes[a,b].gameObject.transform.localScale = new Vector3(1,1,1);
		MasterScript.AllCubes[a,b].GetComponent<Renderer> ().material.color = Color.white ;
		MasterScript.AllCubes[a,b].GetComponent<CubeBehavior> ().CubeActive = false;
		MasterScript.AllCubes [a, b].GetComponent<CubeBehavior> ().CubeColored = false; 
			
	} 


	bool AjecentCheck (int x,int y) {
		if (MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().x == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().x +1 && 
		    MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().y == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().y) {
			return true; 
		}
		if (MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().x == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().x -1 && 
		    MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().y == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().y) {
			return true; 
		}
		if (MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().x == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().x && 
		    MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().y == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().y +1) {
			return true; 
		}
		if (MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().x == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().x && 
		    MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().y == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().y - 1) {
			return true;
		} 
		if (MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().x == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().x +1 && 
		    MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().y == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().y - 1) {
			return true; 
		}
		if (MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().x == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().x +1 && 
		     MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().y == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().y +1) {
			return true; 
		}
		if (MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().x == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().x -1 && 
		     MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().y == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().y - 1) {
			return true; 
		}
		if (MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().x == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().x -1 && 
		     MasterScript.AllCubes [x, y].GetComponent<CubeBehavior> ().y == MasterScript.AllCubes [MasterScript.activeX, MasterScript.activeY].GetComponent<CubeBehavior> ().y + 1) {
			return true; 
		}
		else {
			print ("doesnt pass ac") ; 
			return false;

		}

	}

	void OnMouseDown () {
		print (MasterScript.ColorNumbers(MasterScript.AllCubes[x,y].GetComponent<Renderer> ().material.color)) ;
		if (CubeLive) { 
			if (CubeColored) {

				//inactive colored cube
				if (!CubeActive) {
					if (!MasterScript.thereIsCubeActive){
						MasterScript.AllCubes[x,y].gameObject.transform.localScale = new Vector3(1.3f,1.3f,1);
						CubeActive = true ;
						MasterScript.activeX = x ;
						MasterScript.activeY = y ;
						MasterScript.thereIsCubeActive = true;
						MasterScript.colorToMove = MasterScript.AllCubes[MasterScript.activeX,MasterScript.activeY].GetComponent<Renderer>().material.color;
						print ("cube is active");
						return; 
					}
					else if (MasterScript.thereIsCubeActive){
						MasterScript.AllCubes[MasterScript.activeX,MasterScript.activeY].GetComponent<CubeBehavior> ().CubeActive = false;
						MasterScript.AllCubes[MasterScript.activeX,MasterScript.activeY].gameObject.transform.localScale = new Vector3(1,1,1);
						MasterScript.activeX = x;
						MasterScript.activeY = y;
						CubeActive = true; 
						MasterScript.colorToMove = MasterScript.AllCubes[x,y].GetComponent<Renderer>().material.color;
						MasterScript.AllCubes[x,y].gameObject.transform.localScale = new Vector3(1.3f,1.3f,1);
						print ("cube is a new active");
						return; 
					}
				
					//active colored cube
				} else if (CubeActive) {
					CubeActive = false;
					MasterScript.AllCubes[x,y].gameObject.transform.localScale = new Vector3(1,1,1);
					MasterScript.thereIsCubeActive = false; 
					return;
				}
			}
			//inactive white cube 
			else if (!CubeColored) {
				if (MasterScript.thereIsCubeActive){
					if(AjecentCheck(x,y)){
						PaintItWhite(MasterScript.activeX,MasterScript.activeY);
						MasterScript.activeX = x;
						MasterScript.activeY = y; 
						MasterScript.AllCubes[x,y].GetComponent<Renderer> ().material.color = MasterScript.colorToMove;
						MasterScript.AllCubes[x,y].GetComponent<CubeBehavior> ().CubeColored = true;
						MasterScript.AllCubes[x,y].GetComponent<CubeBehavior> ().CubeActive = true;
						MasterScript.AllCubes[x,y].gameObject.transform.localScale = new Vector3(1.3f,1.3f,1);



					}

				}
				else if (!MasterScript.thereIsCubeActive)
					return;
			} 
		}
	}  







	// Use this for initialization
	void Start () {

	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
