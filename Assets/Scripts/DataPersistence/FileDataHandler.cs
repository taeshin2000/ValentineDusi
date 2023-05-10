using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler {
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath,string dataFileName){
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public WordImages Load(){
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        WordImages loadData = null;
        if (File.Exists(fullPath)){
            try{
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath,FileMode.Open)){
                    using(StreamReader reader = new StreamReader(stream)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadData = JsonUtility.FromJson<WordImages>(dataToLoad);

            }catch{
                Debug.LogError("Can't load file"+fullPath);
            }
        }
        return loadData;
    }
    public void Save(WordImages data){
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        try{
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data,true);
            using (FileStream stream = new FileStream(fullPath,FileMode.Create)){
                using (StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }catch{
            Debug.LogError("Can't save file"+fullPath);
        }
    }
}
