using System.IO;
using UnityEngine;

/// <summary>
///     Wraps up serialization functionalities and local data storage.
/// </summary>
public static partial class LocalData
{
    /// <summary>
    ///     ID of the local serialized data.
    /// </summary>
    const string localDataId = "LocalData";

    /// <summary>
    ///     Register for all the local data.
    /// </summary>
    static Data localFiles = LoadAllData();

    // -----------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Store the data locally. Makes use of PlayerPrefs.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="id"></param>
    /// <typeparam name="T"></typeparam>
    public static void StoreData<T>(T data, string id) where T : class
    {
        var sData = Serialize(data);
        localFiles.Add(id, sData);
        StoreAllData();
    }

    /// <summary>
    ///     Retrieves the data from the id.
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T LoadData<T>(string id) where T : class
    {
        var data = localFiles.TryGet(id);
        return data == null ? null : Deserialize<T>(data);
    }

    /// <summary>
    ///     Remove the data from the id.
    /// </summary>
    /// <param name="id"></param>
    public static void DeleteData(string id)
    {
        localFiles.Remove(id);
        StoreAllData();
    }

    /// <summary>
    ///     Check if id is present in the current data.
    /// </summary>
    /// <param name="id"></param>
    public static bool HasData(string id)
    {
        return localFiles.Has(id);
    }

    /// <summary>
    ///     Remove all the data from the app.
    /// </summary>
    public static void DeleteAll()
    {
        localFiles = new Data();
        PlayerPrefs.DeleteAll();
    }

    // -----------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Deserialize the data from json format.
    /// </summary>
    /// <param name="json"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Deserialize<T>(string json) where T : class
    {
        //using Unity built in solution
//        return JsonUtility.FromJson<T>(json);

        //-------------------------------------------------------

        //using Fullserializer solution
        var type = typeof(T);
        return FullSerializer.Deserialize(type, json) as T;
    }

    /// <summary>
    ///     Serialize the data using json format.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="isPretty"></param>
    /// <returns></returns>
    public static string Serialize(object data, bool isPretty = false)
    {
        // using Unity built in solution
        // return JsonUtility.ToJson(data, isPretty);

        //-------------------------------------------------------

        //using Fullserializer solution
        var type = data.GetType();
        return FullSerializer.Serialize(type, data, isPretty);
    }

    // -----------------------------------------------------------------------------------------------------------------

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

    // -----------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Prints with Debug.Log all the local stored data and its IDs.
    /// </summary>
    public static void PrintLocalData()
    {
        Debug.Log(Serialize(localFiles, true));
    }

    public static void SaveCopyAtAssets()
    {
        var text = Serialize(localFiles, true);
        File.WriteAllText("Assets/localFiles.json", text);
    }
}