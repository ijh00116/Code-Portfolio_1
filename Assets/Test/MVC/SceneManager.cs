using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testb
{
    public enum SceneName
    {
        Main,
        InGame,
    }
    public class SceneManager : Monosingleton<SceneManager>
    {
        public SceneName startScene;
        public SceneName nowScene;

        Dictionary<SceneName, GameObject> _scenes;
        LinkedList<SceneName> _showList;

        Transform _root;

        public bool AllSceneLoaded;

        public bool BeforeGameDataLoading;
        public override void Init()
        {
            base.Init();

            LoadStartScene();
        }

        public void LoadStartScene()
        {
            AllSceneLoaded = false;

            var sceneName = startScene;
            var scene = GetRoot(sceneName);
            if (scene == null)
            {
                var fullpath = string.Format("Scenes/{0}", sceneName);
                StartCoroutine(ResourcesLoader.Instance.Load<GameObject>(fullpath, o => OnPostLoadProcess(o)));
            }
        }

        public GameObject GetRoot(SceneName sceneName)
        {
            GameObject scene;
            _scenes.TryGetValue(sceneName, out scene);
            return scene;
        }
        void OnPostLoadProcess(Object o)
        {
            var scene = Instantiate(o) as GameObject;

            var sceneScript = scene.GetComponent<IScene>();
            scene.name = sceneScript.sceneName.ToString();
            scene.transform.SetParent(_root);

            _scenes.Add(sceneScript.sceneName, scene);
            _showList.AddLast(sceneScript.sceneName);
            SetupScene(sceneScript);
        }

        void SetupScene(IScene scene)
        {
            var scenescript = scene.GetComponent<IScene>();
            scenescript.LoadAssets(
                () =>
                {
                    AllSceneLoaded = true;
                });
        }
    }

}
