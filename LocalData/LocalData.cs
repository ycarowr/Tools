using System;
using System.Collections.Generic;
using System.IO;
using FullSerializer;
using UnityEngine;

/// <summary>
///     Wraps up serialization functionalities and local data storage.
/// </summary>
public class LocalData
{
    /// <summary>
    ///     ID of the local serialized data.
    /// </summary>
    private const string localDataId = "LocalData";

    /// <summary>
    ///     Register for all the local data.
    /// </summary>
    [fsProperty]
    private static Dictionary<string, string> Register { get; } = LoadAllData();

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
        Register[id] = sData;
        StoreAllData();
        PrintLocalData();
    }

    /// <summary>
    ///     Retrieves the data from the id.
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T LoadData<T>(string id) where T : class
    {
        var data = string.Empty;
        Register?.TryGetValue(id, out data);
        return Deserialize<T>(data);
    }

    /// <summary>
    ///     Remove the data from the id.
    /// </summary>
    /// <param name="id"></param>
    public static void DeleteData(string id)
    {
        Register.Remove(id);
        StoreAllData();
    }

    /// <summary>
    ///     Check if id is present in the current data.
    /// </summary>
    /// <param name="id"></param>
    public static bool HasData(string id)
    {
        return Register.ContainsKey(id);
    }

    /// <summary>
    ///     Remove all the data from the app.
    /// </summary>
    public static void DeleteAll()
    {
        Register.Clear();
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
        //return JsonUtility.FromJson<T>(json);
        
        //-------------------------------------------------------
        
        //using Fullserializer solution
        var type = typeof(T);
        return FullSerializerWrapper.Deserialize(type, json) as T;
    }

    /// <summary>
    ///     Serialize the data using json format.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="isPretty"></param>
    /// <returns></returns>
    public static string Serialize(object data, bool isPretty = false)
    {
        //using Unity built in solution
        //return JsonUtility.ToJson(data, isPretty);
        
        //-------------------------------------------------------
        
        //using Fullserializer solution
        var type = data.GetType();
        return FullSerializerWrapper.Serialize(type, data);
    }

    // -----------------------------------------------------------------------------------------------------------------

    private static Dictionary<string, string> LoadAllData()
    {
        var localData = PlayerPrefs.GetString(localDataId);
        return string.IsNullOrEmpty(localData)
            ? new Dictionary<string, string>()
            : JsonUtility.FromJson<Dictionary<string, string>>(localData);
    }

    private static void StoreAllData()
    {
        var allData = Serialize(Register);
        PlayerPrefs.SetString(localDataId, allData);
    }

    // -----------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Prints with Debug.Log all the local stored data and its IDs.
    /// </summary>
    public static void PrintLocalData()
    {
        SaveCopyAtAssets();
        Debug.Log(Serialize(Register, true));
    }

    public static void SaveCopyAtAssets()
    {
        var text = Serialize(Register, true);
        File.WriteAllText("Assets/Snapshot.json", text);
    }

    /// <summary>
    ///     Wrapper for FullSerializer. Ref: https://github.com/jacobdufault/fullserializer.
    /// </summary>
    private static class FullSerializerWrapper
    {
        private static readonly fsSerializer _serializer = new fsSerializer();

        public static string Serialize(Type type, object value)
        {
            // serialize the data
            fsData data;
            _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

            // emit the data via JSON
            return fsJsonPrinter.CompressedJson(data);
        }

        public static object Deserialize(Type type, string serializedState)
        {
            // step 1: parse the JSON data
            var data = fsJsonParser.Parse(serializedState);

            // step 2: deserialize the data
            object deserialized = null;
            _serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

            return deserialized;
        }
    }
}