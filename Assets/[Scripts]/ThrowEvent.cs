using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEvent : MonoBehaviour
{

    public bool Throw;
    [SerializeField] Transform Mesh;
    GameObject ThrowObject;
    public bool oneShot;
    [SerializeField] Transform ThrowPos;
    [SerializeField] float UpPower, ForwardPower, TorquePower;
    [SerializeField] GameObject GarbageCollector;
 
    void Update()
    {
        if(Throw)
        {
            Throw = false;

            if (!oneShot)
            {
                oneShot = true;
                foreach (Transform child in Mesh)
                {
                    if (child.gameObject.activeSelf)
                    {
                        ThrowObject = child.gameObject;
                        break;
                    }
                }

                var CopyObject = Instantiate(ThrowObject, ThrowObject.transform.position, ThrowObject.transform.rotation, ThrowPos);
                //CopyObject.transform.localScale = Vector3.one;
                CopyObject.GetComponent<Collider>().enabled = true;
                CopyObject.GetComponent<Rigidbody>().isKinematic = false;
                CopyObject.GetComponent<Rigidbody>().AddForce(ThrowPos.transform.forward * ForwardPower * 1000f);
                CopyObject.GetComponent<Rigidbody>().AddForce(ThrowPos.transform.up * UpPower * 1000f);
                CopyObject.GetComponent<Rigidbody>().AddTorque(ThrowPos.transform.up * TorquePower * 1000f);

                StartCoroutine(Delay(CopyObject.gameObject));

            }
        }
    }

    IEnumerator Delay(GameObject copy)
    {
        yield return new WaitForSeconds(3f);
        oneShot = false;

        yield return new WaitForSeconds(10f);

        copy.transform.gameObject.SetActive(false);
        copy.transform.parent = GarbageCollector.transform;

    }


}
