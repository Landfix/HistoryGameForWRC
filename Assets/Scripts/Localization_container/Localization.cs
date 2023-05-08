using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Localization_container
{
    [CreateAssetMenu(fileName = "Localization", menuName = "Soular/Localization")]
    public class Localization : ScriptableObject
    {
        const string urlPart = "https://docs.google.com/spreadsheets/d/1lrJ9B_JqKND1KxLOe250LGRu7SW9QbAlDqfovvvimJ8/edit#gid=813032552";

        [SerializeField] private TextAsset _textAsset;
        [SerializeField] public List<string> Keys;
        [SerializeField] public List<LangSet> LangSets;

        public LanguageType Language { get; set; } = LanguageType.en;

        public event Action OnLanguageChange;

        private static Localization _instance;
        int keyIndex = 0;

        public static Localization Instance
        {
            get
            {
                if (!_instance)
                    _instance = Resources.Load<Localization>("Localization/Localization");
                return _instance;
            }
        }

        public void SetLanguage(LanguageType language)
        {
            Language = language;
            OnLanguageChange?.Invoke();
        }

        public string Get(string key, params object[] parameters)
        {
            if (LangSets == null)
                return key;

            var keyIndex = Keys.FindIndex(k => k == key);
            if (keyIndex < 0)
                return "NO KEY FOUND: " + key;

            var langSet = LangSets.FirstOrDefault(l => l.Language == Language);
            if (langSet == null)
            {
                return "LNG`S NOT FOUND " + Language;
            }

            var foundString = langSet.GetString(keyIndex);

            if (string.IsNullOrEmpty(foundString))
                return key;

            return parameters != null ? string.Format(foundString, parameters) : foundString;
        }
        
       private UnityWebRequestAsyncOperation _asyncOperation;
        public async void UpdateLocalization() => 
            OnFirstLocalizationReceived();


        private void OnFirstLocalizationReceived()
        {
            using var reader = new StringReader(_textAsset.text);
            NoSerializedPair<IList<string>, IEnumerable<IList<string>>> data =
                CsvParser.ParseHeadAndTail(reader, ',', '"');

            IList<string> headers = data.Key;
            IEnumerable<IList<string>> rows = data.Value;
            

            int langCount = headers.Count(header => !string.IsNullOrEmpty(header));
            if (langCount <= 0)
            {
                UnityEngine.Debug.Log("Localization file is corrupt.");
                return;
            }

            Keys.Clear();
            LangSets.Clear();

            for (int headerIndex = 1; headerIndex < headers.Count; headerIndex++)
            {
                for (int languageIndex = 0; languageIndex < (int) LanguageType.Unknown; languageIndex++)
                    if (headers[headerIndex] == ((LanguageType) languageIndex).ToString())
                        LangSets.Add(new LangSet
                        {
                            Language = (LanguageType) languageIndex
                        });
            }

            keyIndex = 0;

            FillLangSets(rows);
            UnityEngine.Debug.Log($"Successfully imported {LangSets.Count} languages and {Keys.Count} keys.");
        }

        private void AddExtraDataToTable(string reseivedText)
        {
            using var reader = new StringReader(reseivedText);
            NoSerializedPair<IList<string>, IEnumerable<IList<string>>> data =
                CsvParser.ParseHeadAndTail(reader, ',', '"');

            IEnumerable<IList<string>> rows = data.Value;

            FillLangSets(rows);
        }

        private void FillLangSets(IEnumerable<IList<string>> rows)
        {
            foreach (IList<string> row in rows)
            {
                Keys.Add(row[0]);

                for (int cellIndex = 1; cellIndex < LangSets.Count + 1; cellIndex++)
                {
                    LangSets[cellIndex - 1].AddPair(keyIndex, row[cellIndex]);
                }

                keyIndex++;
            }
        }

        [Serializable]
        public class LangSet : SerializableDictionary<int, string>
        {
            [SerializeField] public LanguageType Language = LanguageType.en;

            public void AddPair(int keyIndex, string value)
            {
                Add(keyIndex, value);
            }

            public string GetString(int keyIndex)
            {
                return ContainsKey(keyIndex) ? this[keyIndex] : string.Empty;
            }
        }
    }
}