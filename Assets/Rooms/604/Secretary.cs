using UnityEngine;

public class Secretary : MonoBehaviour
{
    private bool _allowIn;
    private DoorScript _deanDoor;
    private GameObject _player;

    void Start()
    {
        _allowIn = ArtefactStorage.ContainsKeys(ArtefactStorage.Required);
        _deanDoor = GameObject.FindGameObjectWithTag("DeanDoor").GetComponent<DoorScript>();
        _deanDoor.RequiredKeys = ArtefactStorage.Required;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnGUI()
    {
        if ((_player.transform.position - transform.position).magnitude < 3)
        {
            if (!_allowIn)
            {
                GUI.Label(new Rect(100, 100, 200, 200), "Вы же не собрали всё, что нужно! Приходите, когда будете готовы к пересдаче");
            }
            
        }
    }
}