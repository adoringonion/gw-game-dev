using UnityEngine;
using UnityEngine.SceneManagement;
using AppFw.Core;

namespace CameraSystem
{
    /// <summary>
    /// カメラを取得するためのプロバイダ.
    /// </summary>
    public class CameraRigProvider : SingletonMonoBehaviour<CameraRigProvider>
    {

        public enum DeviceId
        {
            Editor,
            OculusQuest,
        }
        
        [Header("今はとりあえずここに列挙してる")] 
        [SerializeField] DeviceId device;

        [SerializeField] GameObject oculusQuestCameraRig;
        [SerializeField] GameObject editorCameraRig;
        [Header("-----------------------------")]        
        
        ICameraRig rig;
        
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);

#if UNITY_EDITOR
            device = DeviceId.Editor;
#endif
            SceneManager.sceneUnloaded += CameraRigProvider.SceneUnLoadedListener;
            SceneManager.activeSceneChanged += CameraRigProvider.ActiveSceneChangedListener;
            SceneManager.sceneLoaded += CameraRigProvider.SceneLoadedListener;
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
                case DeviceId.Editor:
                    cameraRigObj = Instantiate(Instance.editorCameraRig);
                    break;
                
                case DeviceId.OculusQuest:
                    cameraRigObj = Instantiate(Instance.oculusQuestCameraRig);
                    break;
            }

            if (cameraRigObj != null)
            {
                CameraRigProvider.RemoveCloneString(cameraRigObj);
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
