using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageHandler : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager _trackedImageManager;
    [SerializeField] private GameObject _content;
    [SerializeField] private ArStartGame _arStartGame;
    [SerializeField] private TextMeshProUGUI _debugText;

    private void OnEnable()
    {
        _debugText.text = "Started! \n";
        _trackedImageManager.trackedImagesChanged += OnChanged;
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= OnChanged;
        _debugText.text = "Finished! \n";
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        _debugText.text = "Image tracked \n";
        foreach(var trackedImage in eventArgs.added)
        {
            _debugText.text = "Event add entered \n";
            var minLocalScalar = Mathf.Min(trackedImage.size.x, trackedImage.size.y) / 2;
            trackedImage.transform.localScale = new Vector3(minLocalScalar, minLocalScalar, minLocalScalar);
            _content.transform.parent = trackedImage.transform;
            _content.transform.localScale = Vector3.one;
            _debugText.text = "Event add finished \n";
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            _debugText.text = "Event update entered \n";
            var minLocalScalar = Mathf.Min(trackedImage.size.x, trackedImage.size.y) / 2;
            trackedImage.transform.localScale = new Vector3(minLocalScalar, minLocalScalar, minLocalScalar);
            _content.transform.parent = trackedImage.transform;
            _content.transform.localScale = Vector3.one;
            _debugText.text = "Event update finished \n";
        }
        _arStartGame.Play();
        _debugText.text = "Showing Gameobjects \n";
    }
}