using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public ConsecutiveTiles[] mConsecutiveTiles;
	public Text[] mTilePlayerTags;
	public GameObject mGameOverPanel;
	public Text mGameOverText;
	public Text mTotalLevelWinsText;
	public Text mTotalLevelLossesText;
	public Text mBestStreakText;
	public Text mCurrentStreakText;

	public int mGameBoardSize;
	public int mWinCondition;
	public int mDrawCondition;

	private string mActivePlayer;
	private int mTotalMoves;
	private int mTotalLevelWins;
	private int mTotalLevelLosses;
	private int mBestStreak;
	private int mCurrentStreak;
	
	void Awake ( )
	{
		InitializeTiles ( );
		InitializeGameStats ( );
		UpdateText ( );
		mActivePlayer = "X"; // player currently defaults to X, and always starts first
		mTotalMoves = 0;
		mGameOverPanel.SetActive ( false );
	}

	public void InitializeTiles ( )
	{
		foreach ( Text t in mTilePlayerTags )
		{
			// give each tile a reference to GameManager so that they can call CheckForVictory
			// when their button is pressed
			t.GetComponentInParent<Tile> ( ).SetGameManagerReference ( this );

			// clear text- tiles are labeled with numbers in the inspector to simplify level design
			t.text = "";
		}
	}

	public void InitializeGameStats ( )
	{
		mTotalLevelWins = 0;
		mTotalLevelLosses = 0;
		mBestStreak = 0;
		mCurrentStreak = 0;
	}

	public void UpdateGameStats ( int gameResult )
	{
		// if the game was a tie, interrupt the current win streak count
		if(gameResult == 0 )
		{
			mCurrentStreak = 0;
		}

		else
		{
		// only update the stats displayed on the UI if the player won
		// (player currently defaults to X)
			if ( mActivePlayer == "X" )
			{
		// player won- increment victory stats
				mTotalLevelWins++;
				mCurrentStreak++;

		// update the players best winning streak if a new record is set
				if ( mCurrentStreak > mBestStreak )
				{
					mBestStreak = mCurrentStreak;
				}
			}
			else
			{
		// player lost- increment defeat stats
				mTotalLevelLosses++;
				mCurrentStreak = 0;
			}
		}

		// reflect the changes in the UI
		UpdateText ( );
	}

	public void UpdateText ( )
	{
		mTotalLevelWinsText.text = "Wins at this level: " + mTotalLevelWins;
		mTotalLevelLossesText.text = "Losses at this level: " + mTotalLevelLosses;
		mBestStreakText.text = "Best streak: " + mBestStreak;
		mCurrentStreakText.text = "Current streak: " + mCurrentStreak;
	}

	public string GetActivePlayer ( )
	{
		return mActivePlayer;
	}

	void AlternateActivePlayer ( )
	{
		// toggle between the 'X' and 'O' for the text used to update tiles on button press
		mActivePlayer = ( mActivePlayer == "X" ) ? "O" : "X";
	}

	public void CheckForVictory ( )
	{
		int tilesInARow = 0;
		string tempString = "";

		// cycles through each grouping of adjoining tiles (vertical, horizontal and diagonal rows)
		foreach(ConsecutiveTiles c in mConsecutiveTiles )
		{
			foreach(Tile t in c.rowOfTiles )
			{
				tempString = t.mButtonText.text;
		// checks if the text of current tile matches the symbol associated with the active player
				if ( t.mButtonText.text == mActivePlayer)
				{
					tilesInARow++;
				}

		// check if the active player has enough adjoining tiles to win the game
				if ( tilesInARow == mWinCondition )
				{
					GameOver ( 1 );
		// return is neccessary to break out of the foreach loop and ensure victory is reported for the active player=
		// failing to include the return results in the rest of the code being executed, and for a draw to be falsely
		// reported if a player wins on the last tile- investigate a better approach
					return; 
				}

			}

			tilesInARow = 0;
		}

		// check if the active player has enough adjoining tiles to win the game
		if ( tilesInARow == mWinCondition )
		{
			GameOver ( 1 );
		}

		// the game was not won, increment the move count and test for draw condition
		mTotalMoves++;

		if ( mTotalMoves == mDrawCondition )
			GameOver ( 0 );
			
		// the game is not over, alternate players
		AlternateActivePlayer ( );
	}

	public void GameOver ( int gameResult )
	{
		// set the game board components to inactive
		SetGameBoardActive ( false );

		// show the game over panel
		mGameOverPanel.SetActive ( true );

		// check for a win/lose result
		if ( gameResult == 1)
		{
			// the active player won, update the game over panel text
			mGameOverText.text = mActivePlayer + " wins!";
			UpdateGameStats ( 1 );
		}
		else
		{
			// there was no winner, update the game over panel text
			mGameOverText.text = "It's a tie!";
			UpdateGameStats ( 0 );
		}
		
	}

	public void RestartGame ( )
	{
		// the starting player currently always defaults to X
		mActivePlayer = "X";

		// reset the move count, activeate the game board components
		mTotalMoves = 0;
		mGameOverPanel.SetActive ( false );
		SetGameBoardActive ( true );

		// clear the tile strings
		foreach ( Text t in mTilePlayerTags )
		{
			t.text = "";
		}

	}

	public void SetGameBoardActive(bool toggle )
	{
		// deactivate the buttons for each tile on the gameboard
		foreach(Text t in mTilePlayerTags )
		{
			t.GetComponentInParent<Button> ( ).interactable = toggle;
		}
	}
}
