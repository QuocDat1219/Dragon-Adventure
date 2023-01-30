using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //Vẫn phát nhạc khi qua cảnh mới
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(transform.root.gameObject);
        }
        //Loại bỏ tất cả bản sao của các gameobject trùng lặp
        else if (instance != null && instance != this)
            Destroy(gameObject);

        //Lấy giá trị âm lượng
        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }
    //Phát nhạc
    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    //Hàm thay đổi âm thanh
    public void ChangeSoundVolume(float _change)
    {
        #region Copy pasta
        //Đặt âm lượng cơ bản
        float baseVolume = 1;

        //Tải giá trị đã lưu
        float currentValue = PlayerPrefs.GetFloat("soundVolume", 100);

        //Thay đổi giá trị âm lượng (nằm trong phạm vi 0-100)
        currentValue += _change;

        if (currentValue > 100)
            currentValue = 0;
        else if (currentValue < 0)
            currentValue = 100;

        //Lưu giá trị hiện tại
        PlayerPrefs.SetFloat("soundVolume", currentValue);

        //Tính toán giá trị cuối cùng và gán cho âm thanh nguồn
        float finalValue = baseVolume * currentValue / 100;
        soundSource.volume = finalValue;
        #endregion

        //ChangeSourceVolume(1, _change, "soundVolume", soundSource);
    }
    public void ChangeMusicVolume(float _change)
    {
        #region Copy pasta
        //Đặt âm lượng cơ bản
        float baseVolume = 0.3f;

        //Tải giá trị đã lưu
        float currentValue = PlayerPrefs.GetFloat("musicVolume", 100);

        //Thay đổi giá trị âm lượng (nằm trong phạm vi 0-100)
        currentValue += _change;

        if (currentValue > 100)
            currentValue = 0;
        else if (currentValue < 0)
            currentValue = 100;

        //Lưu giá trị hiện tại
        PlayerPrefs.SetFloat("musicVolume", currentValue);

        //Tính toán giá trị cuối cùng và gán cho âm thanh nguồn
        float finalValue = baseVolume * currentValue / 100;
        musicSource.volume = finalValue;
        #endregion

        //ChangeSourceVolume(0.3f, _change, "musicVolume", musicSource);
    }

    private void ChangeSourceVolume(float _baseValue, float _change, string _volumeName, AudioSource _source)
    {
        //Đặt âm lượng cơ bản
        float baseVolume = _baseValue;

        //Tải giá trị đã lưu
        float currentValue = PlayerPrefs.GetFloat(_volumeName, 100);

        //Thay đổi giá trị âm lượng (nằm trong phạm vi 0-100)
        currentValue += _change;

        if (currentValue > 100)
            currentValue = 0;
        else if (currentValue < 0)
            currentValue = 100;

        //Lưu giá trị hiện tại
        PlayerPrefs.SetFloat(_volumeName, currentValue);

        //Tính toán giá trị cuối cùng và gán cho âm thanh nguồn
        float finalValue = baseVolume * currentValue / 100;
        _source.volume = finalValue;
    }
}