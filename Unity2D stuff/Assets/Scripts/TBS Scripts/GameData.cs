﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameData : MonoBehaviour {

    public static GameData data;

    public CharacterBaseClass[] Characters = new CharacterBaseClass[6];//list of player's characters

	// Use this for initialization
	void Awake () {

        //check for existing gamedata object
	    if(data == null)//if there's no existing gamedata object
        {
            DontDestroyOnLoad(gameObject);//persist this object between scenes
            data = this;//set static reference to this gamedata object so it can be globally accessed
        }
        else if(data != this)//if there's already a gamedata object and this isn't it
        {
            Destroy(gameObject);//destroy this one
        }

        //populate character list
        for (int i = 0; i < Characters.Length; i++)
            Characters[i] = new CharacterBaseClass(CharacterBaseClass.Faction.Player);//create four player characters
	}

    //saving function
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        PlayerData pd;
        //open file for saving stuff
        if(File.Exists(Application.persistentDataPath + "/gameData.dat"))//if save file already exists
        {
            //open file
            file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            pd = (PlayerData)bf.Deserialize(file);//retrieve playerdata object from file 
        }
        else//else create a new save file
        {
            file = File.Create(Application.persistentDataPath + "/gameData.dat");
            pd = new PlayerData();//create a new PlayerData class to contain the data for serialization
        }
        
        //save characters into serializable class
        pd.Characters = Characters;

        bf.Serialize(file, pd);//write PlayerData to file location
        file.Close();//close filestream after done
    }

    //loading function
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/gameData.dat"))//if save file already exists
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            PlayerData pd = (PlayerData)bf.Deserialize(file);//retrieve playerdata object from file
            file.Close();

            Characters = pd.Characters;
        }
    }
}

//class for holding the actual data when saving and loading
[Serializable]//allows the class to be serialized, and therefore saved to a file in binary format
class PlayerData
{
    //character list
    public CharacterBaseClass[] Characters;
}