using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class VuMarkIdManager : MonoBehaviour {

	VuMarkManager m_VuMarkManager;

	public Text txtId; //ID del VuMark
	public Text txtDescripcion; //Descripcion del VuMark
	public UnityEngine.UI.Image imgVuMark; //Imagen del VuMark
	public GameObject[] arrayObjetos; //Array que contiene los objetos a mostrar
	int valorVuMark; //Variable que almacena el ID del VuMark
	int valorObjeto; //Variable utilizada para guardar la posicion del objeto dentro del array


	void Start () {
		m_VuMarkManager = TrackerManager.Instance.GetStateManager ().GetVuMarkManager ();
		m_VuMarkManager.RegisterVuMarkDetectedCallback (OnVuMarkDetected);
		m_VuMarkManager.RegisterVuMarkLostCallback (OnVuMarkLost);

		//Desactiva todos los objetos
		for (int i = 0; i < arrayObjetos.Length; i++) {
			arrayObjetos[i].SetActive (false);
		}

		txtId.text = ""; //Borra el texto actual
		imgVuMark.sprite = null; //Borra el texto actual
		txtDescripcion.text = ""; //Borra la imagen actual
	}

	//Se ejecuta al detectar un VuMark
	public void OnVuMarkDetected (VuMarkTarget target) {
		txtId.text = GetVuMarkId (target); //ID del VuMark
		imgVuMark.sprite = GetVuMarkImage (target); //Imagen del VuMark
		txtDescripcion.text = GetNumericVuMarkDescription (target); //Descripcion del VuMark

		valorVuMark = int.Parse (GetVuMarkId (target)); //Almacena el ID del VuMark en la variable

		//Recorre todo el array hasta encontrar el objeto que tenga el mismo nombre que el ID del VuMark detectado
		for (int i = 0; i < arrayObjetos.Length; i++) {
			if (arrayObjetos[i].name == valorVuMark.ToString()) { //Si el nombre del objeto es igual al ID del VuMark..
				arrayObjetos[i].SetActive (true); //..Activa el objeto..
				valorObjeto = i; //..y almacena su posicion en el array.
				break;
			}
		}
	}

	//Cuando pierde de vista al VuMark
	public void OnVuMarkLost (VuMarkTarget target) {
		txtId.text = ""; //Borra el texto actual
		imgVuMark.sprite = null; //Borra el texto actual
		txtDescripcion.text = ""; //Borra la imagen actual

		arrayObjetos[valorObjeto].SetActive (false); //Desactiva el objeto actual
	}

	//Obtiene el ID del VuMark
	string GetVuMarkId (VuMarkTarget vumark) {
		switch (vumark.InstanceId.DataType) {
			case InstanceIdType.BYTES:
				return vumark.InstanceId.HexStringValue;
			case InstanceIdType.STRING:
				return vumark.InstanceId.StringValue;
			case InstanceIdType.NUMERIC:
				return vumark.InstanceId.NumericValue.ToString ();
		}
		return string.Empty;
	}

	//Obtiene la imagen del VuMark
	Sprite GetVuMarkImage (VuMarkTarget vumark) {
		//Toma la imagen del VuMark
		var instanceImg = vumark.InstanceImage;
		if (instanceImg == null) {
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
	string GetNumericVuMarkDescription (VuMarkTarget vumark) {
		int vuMarkIdNumeric; //Almacenara el ID del VuMark

		if (int.TryParse (GetVuMarkId (vumark), out vuMarkIdNumeric)) { //Convierte el ID del VuMark en una variable numerica

			//Cambia la descripcion de acuerdo al ID del VuMark
			switch (vuMarkIdNumeric) {
				case 10:
					return "Cubo - Rojo";
				case 72:
					return "Esfera - Amarillo";
				case 548:
					return "Cilindro - Azul";
				default:
					return "-ERROR-";
			}
		}

		return string.Empty; 
	}


}
