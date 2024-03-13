using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class Realtime : MonoBehaviour
{
    DatabaseReference databaseReference;

    void Start()
{
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Suscribe a cambios en tiempo real
        databaseReference.Child("Prefabs").ValueChanged += HandleValueChanged;

        // Llama a la función para recoger y posicionar elementos
        GetAndPositionElements();
    });
}

void HandleValueChanged(object sender, ValueChangedEventArgs args)
{
    // Maneja los cambios en la base de datos
    GetAndPositionElements();
}

    void GetAndPositionElements()
    {
        // Accede a la referencia del nodo de posiciones en la base de datos
        DatabaseReference positionsRef = databaseReference.Child("Prefabs");

        // Lee los datos de la base de datos
        positionsRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Maneja el error
                Debug.LogError("Error al obtener datos de la base de datos");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                // Recorre los elementos en la base de datos y posiciónalos en Unity
                foreach (DataSnapshot elementSnapshot in snapshot.Children)
                {
                    string elementName = elementSnapshot.Key;

                    // Accede a los campos "x", "y", y "z" en el documento
                    float x = float.Parse(elementSnapshot.Child("0").Value.ToString());
                    float y = float.Parse(elementSnapshot.Child("1").Value.ToString());
                    float z = float.Parse(elementSnapshot.Child("2").Value.ToString());

                    // Encuentra el objeto por el nombre y actualiza su posición
                    GameObject element = GameObject.Find("PickUp" + elementName.Substring(1));
                    if (element != null)
                    {
                        element.transform.position = new Vector3(x, y, z);
                    }
                }
            }
        });
    }
}