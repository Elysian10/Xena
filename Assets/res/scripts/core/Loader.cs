using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader
{
    public class Scene{
        public static string main = "Assets/scenes/game/main.unity";
    }
    public static void loadScene(string path){
        SceneManager.LoadScene(path);
    }
}
