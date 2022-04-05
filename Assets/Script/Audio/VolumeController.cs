using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("Image Settings")]
    // Image preview
    [SerializeField]
    private Image _imagePreview;
    // Sound icons
    //--------------------------------
    [SerializeField]
    private Sprite _muteSoundIcon;
    [SerializeField]
    private Sprite[] _soundPhaseIcons;
    //--------------------------------

    [Header("Sound Settings")]
    // Sliding Sound
    [SerializeField]
    private AudioClip _effect;

    // Volume Settings
    //--------------------------------
    private const float _MIN_VOLUME = 0;
    private const float _MAX_VOLUME = 1;
    //--------------------------------


    /* The volume changed variable is used to indicate
     * When the slider value changes using the OnValueChanges
     * listener. Afterward the program will wait untill the 
     * user release the mouse button to play the sound effect */
    // The volatile keyword to make sure the value is being written
    // to the main memory immeditly.
    private volatile bool _volumeChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the slider
        Slider slider = GetComponent<Slider>();

        // Set the min and max value of the slider
        slider.maxValue = _MAX_VOLUME;
        slider.minValue = _MIN_VOLUME;

        // Make sure the icons exists
        if (_muteSoundIcon == null || _soundPhaseIcons == null)
            throw new System.NullReferenceException("The Sound Icon Is Missing");

        // Make sure the audio clip exists
        if(_effect == null)
            throw new System.NullReferenceException("The Sound Effect Is Missing");

        // Add a listener to the slider that fires when the value changes
        slider.onValueChanged.AddListener(_UpdateVolume);
    }

    private void Update()
    {
        if(_volumeChanged && Input.GetMouseButtonUp(0))
        { // The volume has changed and the mouse was released

            // Play the sound effect that is associated with this script
            _PlaySoundEffect();
        }
    }

    /// <summary>
    /// Change the sound volume, using the AudioManager's AdjustVolume(float). 
    /// Controls the volume icons if they were added to the array. 
    /// </summary>
    /// <param name="volume">Take a float in the range of (0-1)</param>
    private void _UpdateVolume(float volume)
    {
        // Indicate that the volume has been changed
        _volumeChanged = true;
        // Check if the volume is zero or less to display the mute icon
        if (volume <= 0)
            _imagePreview.sprite = _muteSoundIcon;
        else
        {
            // Get the index of the icon to be displayed 
            int spiritIndex = (int)(Mathf.Ceil(_soundPhaseIcons.Length * (volume / _MAX_VOLUME)) - 1);
            _imagePreview.sprite = _soundPhaseIcons[spiritIndex];
        }
        // Change the audio volume
        AudioManager.audioManager.AdjustVolume(volume);
    }

    private void _PlaySoundEffect()
    {
        // Indicate that the sound has effect has been played
        _volumeChanged = false;
        // Play the sound effect of this object
        AudioManager.audioManager.PlayEffect(_effect);
    }

}
