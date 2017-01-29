using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	public Button mButton;
	public Text mButtonText;

	// values to be used for jumping straight to the row or column of any given tile
	// in the GameManager class
	public int mRow;
	public int mColum;
	public bool mDiagonal1;
	public bool mDiagonal2;

	private GameManager mGameManager;

	public void SetGameManagerReference ( GameManager gameManager )
	{
		mGameManager = gameManager;
	}

	public void UpdateTile ( )
	{
		// update the tile string with the active players symbol
		mButtonText.text = mGameManager.GetActivePlayer ( );
	
		// deactivate the button and check if the games win condition has been met
		mButton.interactable = false;
		mGameManager.CheckForVictory ( );
	}
}


