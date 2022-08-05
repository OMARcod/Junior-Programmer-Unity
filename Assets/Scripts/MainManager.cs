using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour //system to pass data between the scenes in your application
{
    //class member declaration
    public static MainManager Instance;

    public Color TeamColor;

    //static: This keyword means that the values stored in this class member will be shared by all the instances of that class.

    /// <summary>
    /// For example, if there were ten instances of MainManager in your scene,
    /// they would all share the same value stored in Instance.
    /// If any of those 10 MainManagers changed the value in it,
    /// it would also be changed for the other nine.
    /// </summary>


    //Next, you added code to the the Awake method, which is called as soon as the object is created:
    private void Awake()
    {

        //when you start the game instance is null but when you are in main secene .. instance is not null 
        //then when we head back an awake the gameobject .. we check if its null or not .. if not then destroy 
        //so that we don't have multiple mainManager
        //https://learn.unity.com/tutorial/implement-data-persistence-between-scenes?
        //uv=2020.3&pathwayId=5f7e17e1edbc2a5ec21a20af&missionId=5f751af7edbc2a0022cdbbb6#60b7425dedbc2a54f13d5f52
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this; // You can now call MainManager.Instance from any other script
        DontDestroyOnLoad(gameObject);
        //This code enables you to access the MainManager object from any other script.  
    }


    //This line is required for JsonUtility, as you just learned — it will only transform things to JSON if they are tagged as Serializable.
    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        //WE CREATED A new instance of the SaveData and filled its team color class member with the TeamColor variable
        SaveData data = new SaveData(); 
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data); //Next, you transformed that instance to JSON with JsonUtility.ToJson:


        //Finally, you used the special method File.WriteAllText to write a string to a file: 
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
}
