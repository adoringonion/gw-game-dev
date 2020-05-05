using UnityEngine;

namespace AppFw.Core
{
    /// <summary>
    /// ゲームの開始時,Awakeより前に一度だけ呼び出される処理.
    /// </summary>
    public static class ApplicationInitialize
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeApp()
        {
            CreateGameObject("Prefabs/ManagerProvider");
            CreateGameObject("Prefabs/CameraRigProvider");
        }


        /// <summary>
        /// GameObjectの生成.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isRemoveCloneString"></param>
        static void CreateGameObject(
            string path,
            bool isRemoveCloneString = true)
        {
            var go = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            if (isRemoveCloneString)
            {
                go.name = go.name.Replace("(Clone)", "");
            } 
        }
        
        
        
    }
}

