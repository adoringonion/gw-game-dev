using UnityEngine;
using UnityEngine.SceneManagement;
using AppFw.Core;

namespace CameraSystem
{
    /// <summary>
    /// カメラを取得するためのプロバイダ.
    /// </summary>
    public class CameraProvider : SingletonMonoBehaviour<CameraProvider>
    {

        public enum DeviceId
        {
            OculusQuest,
        }

        [Header("今はとりあえずここに列挙してる")] 
        [SerializeField] DeviceId device;

        [SerializeField] GameObject oculusQuestCameraRig;
        [Header("-----------------------------")]        
        
        ICameraRig rig;
        
        
        protected override void Awake()
        {
            base.Awake();

            SceneManager.sceneUnloaded += CameraProvider.SceneUnLoadedListener;
            SceneManager.activeSceneChanged += CameraProvider.ActiveSceneChangedListener;
            SceneManager.sceneLoaded += CameraProvider.SceneLoadedListener;
        }
        
        
        // シーンのアンロードを検知.
        static void SceneUnLoadedListener(
            Scene scene )
        {
            Instance.rig = null;
        }


        // シーン変更検知,Unityの仕様上Beforeには何も入っていない,Afterにロードされたシーンがある.
        static void ActiveSceneChangedListener(
            Scene before,
            Scene after )
        {
        }


        // シーンがロードされたことを検知.
        static void SceneLoadedListener(
            Scene scene,
            LoadSceneMode mode )
        {
            // 既にCameraRigが存在する場合は生成しない.
            if (Instance.rig != null) return;

            // TODO : ここでさらにシーンによって分岐するかもしれない.
            GameObject cameraRigObj = null;
            switch (Instance.device)
            {
                case DeviceId.OculusQuest:
                    cameraRigObj = Instantiate(Instance.oculusQuestCameraRig);
                    break;
            }

            if (cameraRigObj != null)
            {
                CameraProvider.RemoveCloneString(cameraRigObj);
                Instance.rig = cameraRigObj.GetComponent<ICameraRig>();
            }
        }


        /// <summary>
        /// GameObject生成時の(Clone)文字列を削除.
        /// </summary>
        /// <param name="obj"></param>
        static void RemoveCloneString(GameObject obj) => obj.name = obj.name.Replace("(Clone)", "");


        /// <summary>
        /// CameraRigの取得.
        /// </summary>
        /// <returns></returns>
        public static ICameraRig GetCameraRig() => Instance.rig;
        
        
    }
}
