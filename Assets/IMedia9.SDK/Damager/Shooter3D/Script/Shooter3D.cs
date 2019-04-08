using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMedia9
{

    public class Shooter3D : MonoBehaviour
    {
        bool isCooldown = false;
        public enum CForceTrigger { TriggerByIndex, TriggerByKey }
        public CForceTrigger TriggerMode;

        [System.Serializable]
        public class CBullet3D
        {
            public bool isEnabled;
            public KeyCode TriggerKey;
            public GameObject BulletObject;
            public GameObject BulletPosition;
            public int ExecuteDelay;
            public int DestroyDelay;
        }

        [Header("Bullet Settings")]
        public int ActiveBulletIndex = 0;
        public CBullet3D[] Bullet3D;

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
                if (TriggerMode == CForceTrigger.TriggerByIndex) { 
                    if (Bullet3D[ActiveBulletIndex].isEnabled && Input.GetKeyUp(Bullet3D[ActiveBulletIndex].TriggerKey) && !isCooldown)
                    {
                        isCooldown = true;
                        Invoke("Cooldown", Bullet3D[ActiveBulletIndex].ExecuteDelay - 1);
                        Invoke("ExecuteShooter", Bullet3D[ActiveBulletIndex].ExecuteDelay);
                    }
                }
                if (TriggerMode == CForceTrigger.TriggerByKey)
                {
                    for (int i = 0; i < Bullet3D.Length; i++) {
                        if (Bullet3D[i].isEnabled && Input.GetKeyUp(Bullet3D[i].TriggerKey) && !isCooldown)
                        {
                            isCooldown = true;
                            ActiveBulletIndex = i;
                            Invoke("Cooldown", Bullet3D[i].ExecuteDelay - 1);
                            Invoke("ExecuteShooter", Bullet3D[i].ExecuteDelay);
                        }
                    }
                }
        }

        void ExecuteShooter()
        {
            GameObject temp = GameObject.Instantiate(Bullet3D[ActiveBulletIndex].BulletObject, Bullet3D[ActiveBulletIndex].BulletPosition.transform.position, Bullet3D[ActiveBulletIndex].BulletPosition.transform.rotation);
            Destroy(temp.gameObject, Bullet3D[ActiveBulletIndex].DestroyDelay);
        }

        void Cooldown()
        {
            isCooldown = false;
        }
    }

}