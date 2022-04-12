using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class Board
{
    public class WordPlacement
    {
        public string word;
        public Cell startingPosition;
        public int verticalDirection;
        public int horizontalDirection;
    }
    public enum WordDirection
    {
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft,

        COUNT
    }
    public int rows;
    public int cols;
    public List<string> words;
    public List<List<char>> boardCharacters;
    public List<WordPlacement> wordPlacements;
    public HashSet<string> foundWords = new HashSet<string>();

    public HashSet<char> letterHintsUsed = new HashSet<char>();
    public void FromJson(TextAsset levelFile)
    {
        string contents = levelFile.text;
        JSONNode json = JSON.Parse(contents);
        rows = json["rows"].AsInt;
        cols = json["cols"].AsInt;
        words = new List<string>();
        boardCharacters = new List<List<char>>();
        wordPlacements = new List<WordPlacement>();

        for (int i = 0; i < json["words"].AsArray.Count; i++)
        {
            words.Add(json["words"].AsArray[i].Value);
        }
        for (int i = 0; i < json["boardCharacters"].AsArray.Count; i++)
        {
            boardCharacters.Add(new List<char>());

            for (int j = 0; j < json["boardCharacters"][i].AsArray.Count; j++)
            {
                char character = json["boardCharacters"][i][j].Value[0];
                boardCharacters[i].Add(character);
            }
        }

        for (int i = 0; i < json["wordPlacements"].AsArray.Count; i++)
        {
            JSONNode wordPlacementJson = json["wordPlacements"].AsArray[i];
            WordPlacement wordPlacement = new WordPlacement();

            wordPlacement.word = wordPlacementJson["word"].Value;
            wordPlacement.startingPosition = new Cell(wordPlacementJson["row"].AsInt, wordPlacementJson["col"].AsInt);
            wordPlacement.horizontalDirection = wordPlacementJson["h"].AsInt;
            wordPlacement.verticalDirection = wordPlacementJson["v"].AsInt;

            wordPlacements.Add(wordPlacement);
        }
        for (int i = 0; i < json["foundWords"].AsArray.Count; i++)
        {
            foundWords.Add(json["foundWords"].AsArray[i].Value);
        }
        for (int i = 0; i < json["letterHintsUsed"].AsArray.Count; i++)
        {
            letterHintsUsed.Add(json["letterHintsUsed"].AsArray[i].Value[0]);
        }

    }



}
