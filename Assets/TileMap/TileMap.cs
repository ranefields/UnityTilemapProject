﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
	// TODO: Explore other data storage options
	[SerializeField] TextAsset mapTextFile;

	// Array of tile indicies referenced by coordinate [row, column]
	int[,] mapData;

	// Public properties
	public int TilesWide { get; private set; }
	public int TilesHigh { get; private set; }
	public int NumTiles { get; private set; }


	// TODO: Make sure MapData is square/not empty
	void Awake()
	{
		mapData = ReadMapFromTextFile(mapTextFile);
		TilesWide = mapData.GetLength(1);
		TilesHigh = mapData.GetLength(0);
		NumTiles = TilesWide * TilesHigh;
	}

	void Update()
	{
		
	}


	// Retrieves tile at the specified (x,y) coordinate -- origin at bottom left
	// Returns -1 if invalid indicies
	// TODO: Add a tile data structure
	// TODO: Proper exception handling
	public int GetTile(int x, int y)
	{
		if (x < 0 || x >= TilesWide || y < 0 || y >= TilesHigh)
		{
			return -1;
		}

		return mapData[TilesHigh - 1 - y, x];
	}


	// Retrieves tile by index, counting from bottom left
	// Returns -1 if invalid indicies
	// TODO: Add a tile data structure
	// TODO: Proper exception handling
	public int GetTileByIndex(int i)
	{
		if (i < 0 || i >= NumTiles)
		{
			return -1;
		}

		int x = i % TilesWide;
		int y = i / TilesWide;

		return GetTile(x, y);
	}

	// Decrypts CSV map data file into a 2D int array of tile data
	// Return null if improperly formatted
	// TODO: Proper exception handling
	int[,] ReadMapFromTextFile(TextAsset mapDataText)
	{
		// Split along each newline and store in an array of strings
		string[] lines = mapDataText.text.Split('\n');
		int numRows = lines.Length;


		// Split each line along each comma and store in a 2D array
		string[][] entries = new string[numRows][];

		for (int i = 0; i < numRows; i++)
		{
			entries[i] = lines[i].Split(',');
		}

		int numColumns = entries[0].Length;


		// Iterate through and convert each entry to an int
		int [,] mapData = new int[numRows, numColumns];

		for (int y = 0; y < entries.Length; y++)
		{
			int currentRowLength = entries[y].Length;
			if(currentRowLength != numColumns)
			{
				return null;
			}

			for (int x = 0; x < currentRowLength; x++)
			{
				int.TryParse(entries[y][x], out mapData[y, x]);
			}
		}

		return mapData;
	}


	// Public functions for inspector mode
	#if UNITY_EDITOR
	public void InspectorRefreshAwake()
	{
		Awake();
	}

	public void InspectorRefreshStart()
	{
		//Start();
	}
	#endif
}
