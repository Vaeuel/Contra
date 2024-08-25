using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (Minimum,maximum x values)(Min,max Y value)

public class CameraFollow : MonoBehaviour
    
{
    public Transform player; // creates a variable in the inspector that obtains transforms named "player"

    public float minXClamp = 0f;  // Creates float that's accessible in the inspector named minXClamp and sets it to a specific value
    public float maxXClamp = 139f;

    private void LateUpdate() // Unity specifies that this update method is required for cameras, because it updates after everythign else in game.
    {
        Vector3 cameraPos = transform.position; // Creates a 3 vector variable called cameraPos, gets/sets the var = to the position of objects that the script is attached to.

        cameraPos.x = Mathf.Clamp(player.transform.position.x, minXClamp, maxXClamp); // Clamps cameraPos.x to stay between minXClamp and maxXClamp based on the Variable player.
                                                                                      
        transform.position = cameraPos; // Sets the Trans Pos of the object this script is attached to, to the value held within the 'cameraPos' variable
        
        /* The entire above script uses variables created here. Those variables get their values through attachments. "player" has the player object attached to it in the inspector,
        "cameraPos" is attached to the game object Camera. Vector 3 defines teh variable type. 'transform.position' gets trans pos from what ever the script is attached to and then
        sets the Variable to those values respectively. 'Mathf' is a class within unity that contains math functions, Clamp is used to specify a range and prevent values from being
        set beyond it.
            Transform is a unity class that can contain all of the spacial properties of an object, it includes two sets of Vector 3 variables (position and local scale), and
        it allows for the storage of rotational info using a 'Quaternion' variable.
        */
    }
}
