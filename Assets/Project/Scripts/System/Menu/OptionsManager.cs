using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using static TMPro.TMP_Dropdown;

public class OptionsManager : MenuController
{
    [Header("Menu")]
    [SerializeField]
    private GameObject menu = null;
    [SerializeField]
    private TMP_Dropdown languageDropdown = null;

    [Header("Audio")]
    [SerializeField]
    private AudioMixer mixer = null;

    private void Start()
    {
        Init();

        List<OptionData> listOptionData = new List<OptionData>();
        List<string> listLanguageTags = Enum.GetNames(typeof(LanguageTag)).ToList();

        foreach (string language in listLanguageTags)
        {
            OptionData data = new OptionData();
            data.text = language;
            listOptionData.Add(data);
        }

        languageDropdown.AddOptions(listOptionData);
        languageDropdown.onValueChanged.AddListener(delegate
        {
            LanguageManager.Instance.ChangeLanguage(languageDropdown.value.ToString());
        });

        languageDropdown.value = languageDropdown.options.FindIndex(option => option.text == LanguageManager.Instance.currentLanguage.ToString());
    }

    private void Update()
    {
        SelectControllerVertical();
    }

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", sliderValue);
    }

    public void Close()
    {
        if (menu.GetComponentInParent<MenuManager>() != null)
            menu.GetComponentInParent<MenuManager>().canMove = true;

        menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
