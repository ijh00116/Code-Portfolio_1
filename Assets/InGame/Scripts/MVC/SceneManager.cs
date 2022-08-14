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

        Dictionary<SceneName, GameObject> _scenes=new Dictionary<SceneName, GameObject>();
        LinkedList<SceneName> _showList=new LinkedList<SceneName>();

        Transform _root;

        public bool AllSceneLoaded;

        public bool BeforeGameDataLoading;
        public override void Init()
        {
            base.Init();
            _root = this.transform;

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

        public void LoadScene(SceneName sceneName)
        {
            AllSceneLoaded = false;

            UnloadAll();

            nowScene = sceneName;
            var scene = GetRoot(nowScene);
            if (scene == null)
            {
                var fullPath = string.Format("Scenes/{0}", sceneName);
                StartCoroutine(ResourcesLoader.Instance.Load<GameObject>(fullPath, o => OnPostLoadProcess(o)));
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError(string.Format("{0} is arleady exist", sceneName));
#endif
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

        public void UnloadAll(bool ExitGame = false)
        {
            LinkedListNode<SceneName> node;

            while (true)
            {
                node = _showList.First;
                if (node == null)
                    break;
                Unload(node.Value, ExitGame);
            }
        }

        public void Unload(SceneName sceneName, bool ExitGame)
        {
            var scene = GetRoot(sceneName);
            if (scene != null)
            {
                scene.GetComponent<IScene>().Unload(ExitGame);

                _scenes.Remove(sceneName);
                _showList.Remove(sceneName);
            }
        }
    }

}
