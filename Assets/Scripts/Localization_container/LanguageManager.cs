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

    public class LanguageManager
    {

        public LanguageManager()
        {
            // Localization.Instance.SetLanguage((LanguageType) Enum.Parse(typeof(LanguageType),
            //     GameScore.GS_Language.Current()));   
        }
    }
}