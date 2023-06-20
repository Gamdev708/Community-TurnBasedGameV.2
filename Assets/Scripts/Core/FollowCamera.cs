using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBRPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}