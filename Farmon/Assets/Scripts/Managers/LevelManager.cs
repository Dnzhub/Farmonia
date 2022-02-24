using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrassMan.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        private void Awake()
        {
            CreateSingleton();
        }

        private void CreateSingleton()
        {
            if (Instance == null)
            {
                Instance = this;

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public void StartGame()
        {
           
            StartCoroutine(StartGameAsync());
        }
        private IEnumerator StartGameAsync()
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            yield return SceneManager.LoadSceneAsync("Game");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
         
            
        }
        
        public void MainMenu()
        {                     
            StartCoroutine(MainMenuAsync());
        }
        private IEnumerator MainMenuAsync()
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            yield return SceneManager.LoadSceneAsync("Menu");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
        }
       
        public void QuitGame()
        {
            Application.Quit();
        }

     

       


    }

}
