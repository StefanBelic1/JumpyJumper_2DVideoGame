using System.IO;
using UnityEngine;

public static class SaveSystem
{
   public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/saves";
   public static readonly string FILE_EXT = ".json";

    public static void Save(string fileName, string dataToSave){
        if(!Directory.Exists(SAVE_FOLDER)){
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        File.WriteAllText(SAVE_FOLDER + fileName + FILE_EXT, dataToSave);
    }


    public static string Load(string fileName){
        if(File.Exists(SAVE_FOLDER+ fileName + FILE_EXT)){
            string loadedData = File.ReadAllText (SAVE_FOLDER+ fileName + FILE_EXT);

            return loadedData;
        } else{
            return null;
        }
    }
    public static void ResetHighscore(string fileName){
    // Samo prepisujemo fajl sa početnom vrednošću
    Save(fileName, "0");
}

}
