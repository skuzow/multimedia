using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class VuMarkIdManager : MonoBehaviour
{
	World vuforiaWorld;

    public Text txtId; //ID del VuMark
	public Text txtDescripcion; //Descripcion del VuMark
	public UnityEngine.UI.Image imgVuMark; //Imagen del VuMark
	public GameObject[] arrayObjetos; //Array que contiene los objetos a mostrar
	int valorVuMark; //Variable que almacena el ID del VuMark
	int valorObjeto; //Variable utilizada para guardar la posicion del objeto dentro del array

	void Start ()
	{
		vuforiaWorld = VuforiaBehaviour.Instance.World;

        vuforiaWorld.OnObserverCreated += OnVuMarkDetected;
        vuforiaWorld.OnObserverDestroyed += OnVuMarkLost;

		//Desactiva todos los objetos
		for (int i = 0; i < arrayObjetos.Length; i++) {
			arrayObjetos[i].SetActive (false);
		}

		txtId.text = ""; //Borra el texto actual
		imgVuMark.sprite = null; //Borra el texto actual
		txtDescripcion.text = ""; //Borra la imagen actual
	}

	//Se ejecuta al detectar un VuMark
	public void OnVuMarkDetected (ObserverBehaviour observerBehaviour)
	{
		if (valorObjeto != null)
		{
            arrayObjetos[valorObjeto].SetActive(false);
        }

		if (observerBehaviour is VuMarkBehaviour)
		{
			VuMarkBehaviour vuMarkBehaviour = (VuMarkBehaviour) observerBehaviour;

            txtId.text = GetVuMarkId(vuMarkBehaviour); //ID del VuMark
            imgVuMark.sprite = GetVuMarkImage(vuMarkBehaviour); //Imagen del VuMark
            txtDescripcion.text = GetNumericVuMarkDescription(vuMarkBehaviour); //Descripcion del VuMark

            valorVuMark = int.Parse(GetVuMarkId(vuMarkBehaviour)); //Almacena el ID del VuMark en la variable

            //Recorre todo el array hasta encontrar el objeto que tenga el mismo nombre que el ID del VuMark detectado
            for (int i = 0; i < arrayObjetos.Length; i++)
            {
                if (arrayObjetos[i].name == valorVuMark.ToString())
                { //Si el nombre del objeto es igual al ID del VuMark..
                    arrayObjetos[i].SetActive(true); //..Activa el objeto..
                    valorObjeto = i; //..y almacena su posicion en el array.
                    break;
                }
            }
        }
	}

	//Cuando pierde de vista al VuMark
	public void OnVuMarkLost (ObserverBehaviour _)
	{
		txtId.text = ""; //Borra el texto actual
		imgVuMark.sprite = null; //Borra el texto actual
		txtDescripcion.text = ""; //Borra la imagen actual

		arrayObjetos[valorObjeto].SetActive (false); //Desactiva el objeto actual
	}

    //Obtiene el ID del VuMark
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
		}
		return string.Empty;
	}

	//Obtiene la imagen del VuMark
	Sprite GetVuMarkImage (VuMarkBehaviour vumarkTarget) {
		//Toma la imagen del VuMark
		var instanceImg = vumarkTarget.InstanceImage;

		if (instanceImg == null)
		{
			Debug.Log ("La instancia de la imagen del VuMark no existe");
			return null;
		}

		//Se crea una textura a partir de la instancia de la Imagen del VuMark
		Texture2D texture = new Texture2D (instanceImg.Width, instanceImg.Height, TextureFormat.RGBA32, false) {
			wrapMode = TextureWrapMode.Clamp
		};
		instanceImg.CopyToTexture (texture);

		//Se convierte la textura en un Sprite
		Rect rect = new Rect (0, 0, texture.width, texture.height);
		return Sprite.Create (texture, rect, new Vector2 (0.5f, 0.5f));
	}

	//Descripcion del VuMark
	string GetNumericVuMarkDescription (VuMarkBehaviour vumarkTarget)
	{
		int vuMarkIdNumeric; //Almacenara el ID del VuMark

		if (int.TryParse (GetVuMarkId (vumarkTarget), out vuMarkIdNumeric))
		{ //Convierte el ID del VuMark en una variable numerica

			//Cambia la descripcion de acuerdo al ID del VuMark
			switch (vuMarkIdNumeric)
			{
				case 1:
					return "Cube - Yellow";
				case 2:
					return "Sphere - Blue";
				case 3:
					return "Cylinder - Red";
				default:
					return "-ERROR-";
			}
		}

		return string.Empty; 
	}


}
