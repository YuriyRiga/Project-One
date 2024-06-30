using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour 
{
    [SerializeField] private Door _door;
    [SerializeField] private HouseAlarm _alarm;

    private bool _isOpened = false;
    private bool _hasOpener;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DoorOpener>())
        {
            _hasOpener = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DoorOpener>())
        {
            _hasOpener = false;
            _isOpened = false;
            _alarm.AdjustVolume(false);
        }
    }

    private void Update()
    {
        if (_isOpened)
            return;

        if (_hasOpener && Input.GetKeyDown(KeyCode.Space))
        {
            _door.Open();
            _isOpened = true;
            _alarm.AdjustVolume(true);
        }
    }
}
