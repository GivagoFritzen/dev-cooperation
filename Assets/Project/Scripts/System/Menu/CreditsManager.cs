using System.Text;
using TMPro;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public static CreditsManager Instance;

    [SerializeField]
    private TextMeshProUGUI textPro = null;
    [SerializeField]
    private TextAsset jsonFile = null;
    private StringBuilder text = new StringBuilder();

    private Credits credits = null;
    [SerializeField]
    private TextUI creditsArtist = null;
    [SerializeField]
    private TextUI creditsProgrammers = null;
    [SerializeField]
    private TextUI creditsAssets = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Open()
    {
        ReadFile();
        textPro.text = text.ToString();
    }

    private void ReadFile()
    {
        text.Clear();

        credits = JsonUtility.FromJson<Credits>(jsonFile.text);

        AddArtistsInformation();
        AddProgrammersInformation();
        AddAssetsInformation();
    }

    private void AddArtistsInformation()
    {
        if (credits.artists == null || credits.artists.Length == 0)
            return;

        text.Append(creditsArtist.GetCurrentText());
        text.AppendLine();

        for (int index = 0; index < credits.artists.Length; index++)
        {
            text.Append(credits.artists[index]);

            if ((index + 1) < credits.artists.Length)
                text.Append(",");
        }

        text.AppendLine();
        text.AppendLine();
    }

    private void AddProgrammersInformation()
    {
        if (credits.programmers == null || credits.programmers.Length == 0)
            return;

        text.Append(creditsProgrammers.GetCurrentText());
        text.AppendLine();

        for (int index = 0; index < credits.programmers.Length; index++)
        {
            text.Append(credits.programmers[index]);

            if ((index + 1) < credits.programmers.Length)
                text.Append(",");
        }

        text.AppendLine();
        text.AppendLine();
    }

    private void AddAssetsInformation()
    {
        if (credits.assets == null || credits.assets.Length == 0)
            return;

        text.Append(creditsAssets.GetCurrentText());
        text.AppendLine();

        for (int index = 0; index < credits.assets.Length; index++)
        {
            text.Append(credits.assets[index]);

            if ((index + 1) < credits.assets.Length)
                text.Append(",");
        }

        text.AppendLine();
        text.AppendLine();
    }
}
