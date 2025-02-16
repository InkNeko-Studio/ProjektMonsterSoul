using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.SaveSystem
{
    public static class SaveController
    {
        private static readonly int SaveSlotLimit = 3;
        public static SaveData CurrentSave;
        public static int StartPlayTime;
        public static SaveData[] SaveSlots;

        public static Action OnSaveChange;

        public static void NewSave()
        {
            CurrentSave = new SaveData();
            StartPlayTime = (int)Time.time;
        }
        
        public static void SelectSave(int index)
        {
            CurrentSave = LoadSave(index);
            StartPlayTime = (int)Time.time;
            OnSaveChange?.Invoke();
        }
        
        public static void SaveAt(int index)
        {
            CurrentSave.saveTime += (int)Time.time - StartPlayTime;
            StartPlayTime = (int)Time.time;
            SaveSlots[index] = CurrentSave;
            var serializedData = JsonUtility.ToJson(CurrentSave);
            SaveModel.SaveData(index, serializedData);
            CurrentSave = LoadSave(index);
        }

        public static void LoadAllSaves()
        {
            SaveSlots = new SaveData[SaveSlotLimit];
            for (int i = 0; i < SaveSlotLimit; i++)
            {
                SaveSlots[i] = LoadSave(i);
            }
        }

        private static SaveData LoadSave(int index)
        {
            if (!SaveModel.HasData(index)) return null;
                
            string serializedData = SaveModel.RetrieveData(index);
            if (string.IsNullOrEmpty(serializedData)) return null;
                
            try
            {
                var data = JsonUtility.FromJson<SaveData>(serializedData);
                return data;
            }
            catch
            {
                Debug.LogError($"Corrupted Data at save slot {index}");
                SaveModel.EraseData(index);
                return null;
            }
        }
    }
}