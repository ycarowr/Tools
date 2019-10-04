﻿using System.IO;
using UnityEngine;

/// <summary>
///     Wraps up serialization functionalities and local data storage.
/// </summary>
public static partial class LocalData
{
    /// <summary> ID of the local serialized data. </summary>
    const string localDataId = "LocalData";

    /// <summary> Register for all the local data. </summary>
    static Data localFiles = LoadAllData();

    /// <summary> Store the data locally. </summary>
    public static void StoreData<T>(T data, string id) where T : class
    {
        var sData = Serialize(data);
        localFiles.Add(id, sData);
        StoreAllData();
    }

    /// <summary> Retrieves the data from the id. </summary>
    public static T LoadData<T>(string id) where T : class
    {
        var data = localFiles.TryGet(id);
        return data == null ? null : Deserialize<T>(data);
    }

    /// <summary> Remove the data from the id. </summary>
    public static void DeleteData(string id)
    {
        localFiles.Remove(id);
        StoreAllData();
    }

    /// <summary> Check whether an id is present in the current data. </summary>
    public static bool HasData(string id)
    {
        return localFiles.Has(id);
    }

    /// <summary> Remove all data. </summary>
    public static void DeleteAll()
    {
        localFiles = new Data();
        PlayerPrefs.DeleteAll();
    }

    /// <summary> Deserialize the data from string json format. </summary>
    public static T Deserialize<T>(string json) where T : class
    {
        //using Unity built in solution
        //return JsonUtility.FromJson<T>(json);

        //-------------------------------------------------------

        //using Fullserializer
        var type = typeof(T);
        return FullSerializer.Deserialize(type, json) as T;
    }

    /// <summary> Serialize the data using json format. </summary>
    public static string Serialize(object data, bool isPretty = false)
    {
        // using Unity built in solution
        // return JsonUtility.ToJson(data, isPretty);

        //-------------------------------------------------------

        //using Fullserializer 
        var type = data.GetType();
        return FullSerializer.Serialize(type, data, isPretty);
    }

    static Data LoadAllData()
    {
        var localData = PlayerPrefs.GetString(localDataId);
        return string.IsNullOrEmpty(localData)
            ? new Data()
            : Deserialize<Data>(localData);
    }

    static void StoreAllData()
    {
        var allData = Serialize(localFiles);
        PlayerPrefs.SetString(localDataId, allData);
    }

    /// <summary> Prints with all the local data and its IDs. </summary>
    public static void PrintLocalData()
    {
        Debug.Log(Serialize(localFiles, true));
    }

    /// <summary> Saves a copy of the data into a file inside the Assets </summary>
    public static void SaveCopyAtAssets()
    {
        var text = Serialize(localFiles, true);
        File.WriteAllText("Assets/localFiles.json", text);
    }
}