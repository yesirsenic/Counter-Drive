using UnityEngine;

public class UserCar : MonoBehaviour
{
    [SerializeField]
    GameObject clearPopup;

    private void Update()
    {
        if(!GameManager.Instance.isControl && !GameManager.Instance.isGameOver)
        {
            transform.Translate(Vector3.forward * 10f * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ClearCollider"))
        {
            clearPopup.SetActive(true);
        }
    }
}
