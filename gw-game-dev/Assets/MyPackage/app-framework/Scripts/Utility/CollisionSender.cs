using UnityEngine;
using UnityEngine.Events;


namespace AppFw.Utility
{
    /// <summary>
    /// Collisionイベント発行用スクリプト.
    /// </summary>
    public class CollisionSender : MonoBehaviour
    {
        [System.Serializable]
        public class CollisionSendEvent : UnityEvent<Collision>{}

        [System.Serializable]
        public class TriggerSendEvent : UnityEvent<Collider>{}


        [SerializeField] public CollisionSendEvent onCollisionEnter;

        [SerializeField] public CollisionSendEvent onCollisionStay;
        [SerializeField] public CollisionSendEvent onCollisionExit;

        [SerializeField] public TriggerSendEvent onTriggerEnter;
        [SerializeField] public TriggerSendEvent onTriggerStay;
        [SerializeField] public TriggerSendEvent onTriggerExit;


        void OnCollisionEnter(Collision other)=> onCollisionEnter?.Invoke(other);
        void OnCollisionStay(Collision other)=> onCollisionStay?.Invoke(other);
        void OnCollisionExit(Collision other)=> onCollisionExit?.Invoke(other);


        void OnTriggerEnter(Collider other)=> onTriggerEnter?.Invoke(other);
        void OnTriggerStay(Collider other)=> onTriggerStay?.Invoke(other);
        void OnTriggerExit(Collider other)=> onTriggerExit?.Invoke(other);
    }
}