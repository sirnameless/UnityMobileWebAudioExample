using UnityEngine;
using System.Runtime.InteropServices;

public class GameScript : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void PlayAudioFile(string str); //our JS plugin's function to play an audio file

    const float flightSpeed = 2500f; //how fast an animal should fly away when tapped/clicked

    //function for tapping or clicking an animal object
    public void SelectAnimal(GameObject animal)
    {
        PlaySound(animal.name);

        //find the animal's rigid body component, and if it exists, add a random force
        //that will move the object left or right as well as upwards
        Rigidbody2D rigidbody = animal.GetComponent<Rigidbody2D>();
        
        if(rigidbody)
        {
            Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), 1);
            rigidbody.AddForce(randomForce * flightSpeed);
        }
    }

    //play a sound, dependant on the game object's name
    void PlaySound(string name)
    {
        //if we are not in the Editor, play the sound using external JavaScript
        #if !UNITY_EDITOR
            PlayAudioFile(name);
        #endif
    }
}
