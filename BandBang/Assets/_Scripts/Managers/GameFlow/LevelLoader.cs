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
    SimpleScene england;
    [SerializeField]
    SimpleScene india;
    [SerializeField]
    SimpleScene newYork;
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
                TEMP = england.Index;

                break;
            case countries.Egypt:
                TEMP = india.Index;

                break;
            case countries.France:
                TEMP = newYork.Index;

                break;
            default:
                return;
        }
        SceneManager.LoadScene(TEMP);
        return;

    }


}