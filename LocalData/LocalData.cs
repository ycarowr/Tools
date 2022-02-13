using System;
using System.IO;
using UnityEngine;

namespace YWR.Tools
{
    /// <summary>
    ///     Wraps up serialization functionalities and local data storage.
    /// </summary>
    public static partial class LocalData
    {
        /// <summary> ID of the local serialized data. </summary>
        private const string LocalDataId = "LocalData";

        /// <summary> Register for all the local data. </summary>
        private static Data _localFiles = LoadAllData();

        /// <summary> Store the data locally. </summary>
        public static void StoreData<T>(T data, string id) where T : class
        {
            string sData = Serialize(data);
            _localFiles.Add(id, sData);
            StoreAllData();
        }

        /// <summary> Retrieves the data from the id. </summary>
        public static T LoadData<T>(string id) where T : class
        {
            string data = _localFiles.TryGet(id);
            return data == null ? null : Deserialize<T>(data);
        }

        /// <summary> Remove the data from the id. </summary>
        public static void DeleteData(string id)
        {
            _localFiles.Remove(id);
            StoreAllData();
        }

        /// <summary> Check whether an id is present in the current data. </summary>
        public static bool HasData(string id)
        {
            return _localFiles.Has(id);
        }

        /// <summary> Remove all data. </summary>
        public static void DeleteAll()
        {
            _localFiles = new Data();
            PlayerPrefs.DeleteAll();
        }

        /// <summary> Deserialize the data from string json format. </summary>
        public static T Deserialize<T>(string json) where T : class
        {
            //using Unity built in solution
            //return JsonUtility.FromJson<T>(json);

            //-------------------------------------------------------

            //using Fullserializer
            Type type = typeof(T);
            return FullSerializer.Deserialize(type, json) as T;
        }

        /// <summary> Serialize the data using json format. </summary>
        public static string Serialize(object data, bool isPretty = false)
        {
            // using Unity built in solution
            // return JsonUtility.ToJson(data, isPretty);

            //-------------------------------------------------------

            //using Fullserializer 
            Type type = data.GetType();
            return FullSerializer.Serialize(type, data, isPretty);
        }

        private static Data LoadAllData()
        {
            string localData = PlayerPrefs.GetString(LocalDataId);
            return string.IsNullOrEmpty(localData)
                ? new Data()
                : Deserialize<Data>(localData);
        }

        private static void StoreAllData()
        {
            string allData = Serialize(_localFiles);
            PlayerPrefs.SetString(LocalDataId, allData);
        }

        /// <summary> Prints with all the local data and its IDs. </summary>
        public static void PrintLocalData()
        {
            Debug.Log(Serialize(_localFiles, true));
        }

        /// <summary> Saves a copy of the data into a file inside the Assets </summary>
        public static void SaveCopyAt(string path)
        {
            string text = Serialize(_localFiles, true);
            File.WriteAllText(path, text);
        }
    }
}