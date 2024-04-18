using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class VuMarkIdManager : MonoBehaviour
{
    public Text txtId; // Text Vumark Id
	public Text txtDescripcion; // Text Vumark Description
	public UnityEngine.UI.Image imgVuMark; // Vumark Image
	public GameObject[] objectsToDisplay; // Objects to display

    World vuforiaWorld;
	GameObject activeVumark = null; // Active Vumark

    // Start point
	void Start()
	{
		vuforiaWorld = VuforiaBehaviour.Instance.World;

		// Setup events
        vuforiaWorld.OnObserverCreated += OnVuMarkDetected;
        vuforiaWorld.OnObserverDestroyed += OnVuMarkLost;

		ResetScene();
	}

	// When a Vumark is detected
	public void OnVuMarkDetected(ObserverBehaviour observerBehaviour)
	{
        if (observerBehaviour is VuMarkBehaviour vuMarkBehaviour)
        {
            ResetScene();

            string activeVumarkId = GetVuMarkId(vuMarkBehaviour); // Active Vumark Id

            txtId.text = activeVumarkId;
            txtDescripcion.text = GetNumericVuMarkDescription(activeVumarkId);
            imgVuMark.sprite = GetVuMarkImage(vuMarkBehaviour);

            // Get active Vumark with Find comparing names with id
			activeVumark = Array.Find(objectsToDisplay, element => element.name == activeVumarkId);

            // Activate if was found
            if (activeVumark) activeVumark.SetActive(true);
            // Show Log Message if not
            else Debug.Log($"Display object for Vumark {activeVumarkId} Id doesn't exist");
        }
    }

    // When a Vumark is lost from view
    public void OnVuMarkLost(ObserverBehaviour _)
	{
		ResetScene();
    }

    // Obtain Vumark Id
    string GetVuMarkId(VuMarkBehaviour vumarkTarget)
    {
        switch (vumarkTarget.InstanceId.DataType)
        {
            case InstanceIdType.BYTE:
                return vumarkTarget.InstanceId.HexStringValue;
            case InstanceIdType.STRING:
                return vumarkTarget.InstanceId.StringValue;
            case InstanceIdType.NUMERIC:
                return vumarkTarget.InstanceId.NumericValue.ToString();
            default:
                break;
        }
        return string.Empty;
    }

    // Obtain Vumark Description
    string GetNumericVuMarkDescription(string activeVumarkId)
    {
        return int.Parse(activeVumarkId) switch
        {
            1 => "Alien",
            2 => "Cat",
            3 => "Tiger",
            _ => "-ERROR-",
        };
    }

    // Obtain Vumark Img
    Sprite GetVuMarkImage(VuMarkBehaviour vumarkTarget)
    {
        // Takes Vumark Img
        Vuforia.Image instanceImg = vumarkTarget.InstanceImage;

        if (instanceImg == null)
		{
			Debug.Log("Instance of Vumark doesn't exist");
			return null;
		}

		// It creates a texture from the instace of the Vumark Img
		Texture2D texture = new(instanceImg.Width, instanceImg.Height, TextureFormat.RGBA32, false)
        {
			wrapMode = TextureWrapMode.Clamp
        };
        instanceImg.CopyToTexture(texture);

		// Converts texture into a sprite
		Rect rect = new(0, 0, texture.width, texture.height);
		return Sprite.Create(texture, rect, new Vector2 (0.5f, 0.5f));
	}

    // Reset Vumark related scene elements to default values
	void ResetScene()
	{
		txtId.text = ""; // Deletes actual Text Vumark Id
		txtDescripcion.text = ""; // Deletes actual Text Vumark Description
		imgVuMark.sprite = null; // Deletes actual Vumark Image

        activeVumark = null; // Reset active object

        // Disable all objects
        foreach (GameObject gameObject in objectsToDisplay)
        {
            gameObject.SetActive(false);
        }
    }
}
