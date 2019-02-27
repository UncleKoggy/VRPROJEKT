using UnityEngine;
using System.Collections;
namespace Climbing
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class CharacterFootCollider : MonoBehaviour
    {
        BoxCollider col;
        Transform head;
        void Update()
        {
            col.center = new Vector3(head.localPosition.x, 0, head.localPosition.z);
        }
        void Start()
        {
            head = GetComponentInChildren<Camera>(true).transform;
            if (head == null)
                Debug.LogError("Camera not found");
            col = GetComponent<BoxCollider>();
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
}