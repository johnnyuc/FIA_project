using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyChase : GhostChase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Pinky's chase behavior
        // The direction that will minimize the distance to 4 tiles in front of Pacman will be selected

        // instantiating the intersection node object
        Node node = other.GetComponent<Node>();

        // First check if this behaviour is enabled
        // and the ghost is not frightened
        if (!(node != null && isChasing() && !isFrightened()))
            return;

        //Get the available directions in this intersection
        List<Vector2> dirs = getAvailableDirections(node);
    
        // Get the position of the 4 tiles in front of Pacman's position
        Vector3 pacman4FrontTilesPos = getPacmanPosition() + new Vector3(getPacmanDirection().x, getPacmanDirection().y, 0)*4;
        // Get Pinky's position
        Vector3 pinkyPos = currentPosition();

        Vector2 closest_dir = dirs[0];
        float min_dist = int.MaxValue;
        float dist;
        // Find the best direction to go to the target destination
        foreach (Vector2 dir in dirs)
        {
            // Skip the direction Pinky came from
            if (dir == -currentDirection())
                continue;

            Vector3 futurePos = new(pinkyPos.x + dir.x, pinkyPos.y + dir.y, 0);
            futurePos.x = pinkyPos.x + dir.x;
            futurePos.y = pinkyPos.y + dir.y;
            futurePos.z = 0;
            dist = Vector3.Distance(futurePos, pacman4FrontTilesPos);

            // If the distance to the target location when the current direction is followed
            // is lesser than the minimum distance yet, update it with the current values
            if (dist < min_dist)
            {
                min_dist = dist;
                closest_dir = dir;
            }
        }
        setDirection(closest_dir);
    }
}

