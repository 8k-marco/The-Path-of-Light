using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotCapture : MonoBehaviour
{
    public KeyCode screenshotKey = KeyCode.F12; // default Screenshot Taste wird auf F12 gemapped
    public int superSize = 2; // Der Faktor, mit dem die Auflösung erhöht werden soll.

    public RawImage screenshotDisplayImage;// Es zeigt den Screenshot den man gemacht hat 
    public Button displayButton; // Knopf zum anzeigen des letzten Screenshots

    private string lastCapturedFilePath; // letzen Screenshot Adresse als Pfad 

    /* 
    Wenn kein Screenshot da ist oder Wenn kein Button da ist, dann passiert nichts.
    Ansonsten macht er sich bereit um neue Screenshots aufzunehmen.
    */
    void Start()
    {
        if (screenshotDisplayImage == null || displayButton == null) 
        {
            return;
        }
        displayButton.onClick.AddListener(DisplayCapturedScreenshot); 
    }

    /*
    Wenn die Taste gedrückt wird,dann macht man einen Screenshot.
    */
    void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            CaptureScreenshot();
        }
    }

   
    void CaptureScreenshot()
    {
        //Suche nach den Download Ordner 
        string downloadsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "\\Downloads";
        // Screenshot File Name (z.B. "Screenshot_20240113_224059.png")
        string filename = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        // Kombiniert den Dateiname mit dem Download Pfad wo es gespeichert wurde.
        lastCapturedFilePath = Path.Combine(downloadsPath, filename);
        // Aufnahme des Screenshot in doppelter auflösung in dem Download Ordner mit dem richtigen File name.
        ScreenCapture.CaptureScreenshot(lastCapturedFilePath, superSize);

        PlayerPrefs.SetString("LastCapturedFilePath", lastCapturedFilePath);
        PlayerPrefs.Save();
    }

    /* 
    Azeige des letzten Screensots
    */
    void DisplayCapturedScreenshot()
    {
        //Finden vom Pfad vom letzten Screenshot
        string savedFilePath = PlayerPrefs.GetString("LastCapturedFilePath", "");

        /*Wenn die Zeichenabfolge vom letzten Screenshot existiert oder nicht leer ist und die Screenshotdatei vorhanden ist,
        dann lädt er den Screenshot hoch und aschließend zeigt er es an.*/ 
        if (!string.IsNullOrEmpty(savedFilePath) && File.Exists(savedFilePath))
        {
            byte[] fileData = File.ReadAllBytes(savedFilePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            screenshotDisplayImage.texture = texture;
        }
    }
}
