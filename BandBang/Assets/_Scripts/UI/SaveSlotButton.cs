using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveSlotButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI slotText;
    [SerializeField]
    TextMeshProUGUI dateText;
    void Start()
    {
        BuildSaveSlot();
    }
    void BuildSaveSlot()
    {
        // var mngr = GameManager.Instance;
        dateText.gameObject.SetActive(false);


       

        var saveSlotLoader =GetComponent<ALoader>();
        saveSlotLoader.LoadValues();
        
        if (saveSlotLoader == null) { Debug.Log("SaveSlotLoader not found"); return; }

        if (saveSlotLoader .GetValue<bool>("HasPlayedBefore"))
        {
            slotText.text = "File_" + (transform.GetSiblingIndex() + 1).ToString();
            dateText.gameObject.SetActive(true);
            int unix = saveSlotLoader.GetValue<int>("TimePlayed");
            DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(unix);
            string time = dt.ToString("HH:mm:ss");
            dateText.text = time;
        }
        else
        {
            dateText.gameObject.SetActive(false);
        }

    }


}
