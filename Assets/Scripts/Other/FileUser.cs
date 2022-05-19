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
    private static string _file;

    public static void Init(){
        if (File.Exists (_path)){
            _file = File.ReadAllText(_path);
        }
    }
    public static List<Entry> Load(){
        Init();
        List<Entry> list = new List<Entry>();
        if (_file != null){
            _highscoreRaw = JsonUtility.FromJson<Players>(_file);
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

    public static void Save(string time, string name){
        List<Entry> highscoreEntryList = Load();
        Entry newEntry = new Entry(name.ToUpper(), time);

        highscoreEntryList.Add(newEntry);
        if(highscoreEntryList.Count > 8){
            highscoreEntryList.RemoveAt(highscoreEntryList.Count - 1);
        }
        _highscoreRaw = new Players(highscoreEntryList);
        // Debug.Log(JsonUtility.ToJson(_highscoreRaw));
        File.WriteAllText(_path, JsonUtility.ToJson(_highscoreRaw));
    }
}