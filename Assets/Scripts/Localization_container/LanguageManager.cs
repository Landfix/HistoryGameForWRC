using System;
using UnityEngine;
using Utils;

namespace Localization_container
{
    public enum LanguageType
    {
        ru = 0,
        en,
        tr,
        Unknown
    }

    public class LanguageManager : SingletonMono<LanguageManager>
    {
        private static LanguageManager _instance;
    
        public static LanguageManager I => _instance;
    
    
        public void Init()
        {
            // Localization.Instance.SetLanguage((LanguageType) Enum.Parse(typeof(LanguageType),
            //     GameScore.GS_Language.Current()));
        }
    }
}