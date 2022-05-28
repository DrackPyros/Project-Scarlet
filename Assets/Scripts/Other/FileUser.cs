using System.IO;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Entry{
    public string name;
    public string score;
    public Entry(string name, string score){
        this.name = name;
        this.score = score;
    }
}

[System.Serializable]
public class Players{
    public List<Entry> players = new List<Entry>();
    public Players(){}
    public Players(List<Entry> data){
        for(int i = 0; i< data.Count; i++){
            players.Add(data[i]);
        }
    }
}
public static class FileUser{
    public static Players _highscoreRaw = new Players();
    private static readonly string _path = Application.dataPath + "/Resources/score.json";
    private static readonly string _path2 = Application.dataPath + "/Resources/score2.json";

    public static string Init(int opcion){ //TODO: Rehacer correctamente para sistema de varios mundos
        string result = "";
        switch(opcion){
            case 1:
                if (File.Exists (_path)){
                    result =  File.ReadAllText(_path);
                }
                break;
            case 2:
                if (File.Exists (_path2)){
                    result =  File.ReadAllText(_path2);
                }
                break;
        }
        return result;
    }
    public static List<Entry> Load(int page){
        string file = Init(page);
        List<Entry> list = new List<Entry>();
        if (file != null){
            _highscoreRaw = JsonUtility.FromJson<Players>(file);
            foreach (Entry e in _highscoreRaw.players){
                list.Add(e);
            }
            list.Sort(SortByScore);
        }
        return list;
    }
    static int SortByScore(Entry p1, Entry p2){
        return p1.score.CompareTo(p2.score);
    }

    public static void Save(string time, string name, int scene){
        List<Entry> highscoreEntryList = Load(scene);
        Entry newEntry = new Entry(name.ToUpper(), time);

        highscoreEntryList.Add(newEntry);
        highscoreEntryList.Sort(SortByScore);
        if(highscoreEntryList.Count > 8){
            highscoreEntryList.RemoveAt(highscoreEntryList.Count - 1);
        }
        _highscoreRaw = new Players(highscoreEntryList);
        // Debug.Log(JsonUtility.ToJson(_highscoreRaw));
        if(scene == 2)
            File.WriteAllText(_path2, JsonUtility.ToJson(_highscoreRaw));
        else
            File.WriteAllText(_path, JsonUtility.ToJson(_highscoreRaw));
    }
}