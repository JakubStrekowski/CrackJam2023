using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

public class HackActivation : MonoBehaviour
{
    [SerializeField] private GameObject hackText;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private Slider slider;

    private bool _wasActivated;
    private void OnTriggerEnter(Collider other)
    {
        if (_wasActivated) return;
        
        hackText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        hackText.SetActive(false);
    }

    public void OnStartHack(InputAction.CallbackContext ctx )
    {
        if (_wasActivated) return;
        if (!hackText.activeInHierarchy) return;

        hackText.SetActive(false);
        director.Play();
        _wasActivated = true;
        slider.gameObject.SetActive(true);
    }
}
