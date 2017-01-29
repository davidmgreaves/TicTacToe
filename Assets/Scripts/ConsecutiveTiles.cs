using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Wrapper Class to expose an array of arrays to the editor- this allows 
 * the level designer to drag and drop tiles onto individual 'rows' and 
 * 'columns', and to drag these arrays into an array of 'row's and 
 * 'columns' making it easy to iterate over speciffic groups of tiles */

[System.Serializable]
public class ConsecutiveTiles : MonoBehaviour {

	public Tile[] rowOfTiles;
	
}
