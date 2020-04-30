using UnityEngine;

namespace CameraSystem
{
    /// <summary>
    /// 各種CameraRigが実装すべきインターフェースをまとめたインターフェース.
    /// </summary>
    public interface ICameraRig : 
        ICameraTransform,
        ICameraRigController
    {
    }


    /// <summary>
    /// サンプル用,Transform周り.
    /// </summary>
    public interface ICameraTransform
    {
        Transform GetMainCamera();
        void Recenter();
    }


    /// <summary>
    /// サンプル用,キー入力的な.
    /// </summary>
    public interface ICameraRigController
    {
        bool GetDownClick();
    }

}