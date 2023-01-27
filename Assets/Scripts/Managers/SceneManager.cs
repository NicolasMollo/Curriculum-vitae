using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

namespace Managers {

    [DisallowMultipleComponent]
    public class SceneManager : MonoBehaviour {

        public Action onChangeScene = null;
        public GameObject Loading = null;


        private void Update() {
            if (Input.GetKeyDown(KeyCode.P)) {
                //UnityEngine.SceneManagement.SceneManager.LoadScene("NextScene", LoadSceneMode.Single);
                //onChangeScene?.Invoke();
                //ChangeScene();
                StartCoroutine(LoadSceneAsync());
            }
        }

        private void OnEnable() {
            
        }

        private void OnDisable() {
            
        }


        private IEnumerator LoadSceneAsync() {
            onChangeScene?.Invoke();

            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);

            Loading.SetActive(true);

            while (!operation.isDone) {
                yield return null;
            }


        }

        #region API
        /// <summary>
        /// Method for trigger new scene
        /// </summary>
        /// <param name="_nextScene"></param>
        /// <param name="_mode"></param>
        public void ChangeScene(/*Scene _nextScene, LoadSceneMode _mode*/) {
            //UnityEngine.SceneManagement.SceneManager.LoadScene(_nextScene.name, _mode);
            //UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(1);
        }
        #endregion
    }

}