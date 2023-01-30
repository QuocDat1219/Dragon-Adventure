// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Pathfinding;

// public class BirdAI : MonoBehaviour
// {
//     [SerializeField] private AIPath aiPath;
//     private void Update()
//     {
//         if (aiPath.desiredVelocity.x >= 0.01f)
//         {
//             transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
//         }else if(aiPath.desiredVelocity.x <= -0.01f)
//         {
//             transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
//         }
//     }
// }
