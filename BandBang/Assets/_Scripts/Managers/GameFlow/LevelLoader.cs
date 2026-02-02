using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum countries
{
    Russia,
    Egypt,
    France
}
public class LevelLoader : MonoBehaviour
{

    [SerializeField]
    SimpleScene mainMenu;
    [SerializeField]
    SimpleScene russia;
    [SerializeField]
    SimpleScene egypt;
    [SerializeField]
    SimpleScene france;
    // Start is called before the first frame update
    [ContextMenu("Load Main Menu")]
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu.Index);
    }
    public void LoadLevel(countries country)
    {
        int TEMP = 0;
        switch (country)
        {
            case countries.Russia:
                TEMP = russia.Index;

                break;
            case countries.Egypt:
                TEMP = egypt.Index;

                break;
            case countries.France:
                TEMP = france.Index;

                break;
            default:
                return;
                break;
        }
        SceneManager.LoadScene(TEMP);
        return;

    }


}