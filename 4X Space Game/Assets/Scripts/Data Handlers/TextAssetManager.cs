using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TextAssetManager {

    public static List<string> TextToList(TextAsset textAsset)
    {
        List<string> textList = textAsset.text.Split("\n"[0]).ToList<string>();

        return textList;
    }

}


