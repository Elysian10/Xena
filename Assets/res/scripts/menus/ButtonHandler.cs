using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void startGame()
    {
        Loader.loadScene(Loader.Scene.main);
    }

    public void exitGame(){
        Application.Quit();
    }
}
