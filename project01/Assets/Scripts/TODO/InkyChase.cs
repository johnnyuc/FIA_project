using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyChase : GhostChase
{
    protected override void OnTriggerEnter2D(Collider2D other){
        //Inky's custom chase behavior
        //We only have to select the next direction to move to

        //instantiating the intersection node object
        Node node = other.GetComponent<Node>();


        //First check if this behaviour is enabled
        //and the ghost is not frightened
        if (!(node != null && isChasing() && !isFrightened()))
            return;

        Vector3[] pellets = getRemainingPellets();
        Vector3 avgPos = Vector3.zero;

        foreach (Vector3 p in pellets)
        {
            avgPos.x += p.x;
            avgPos.y += p.y;
        }

        avgPos.x /= pellets.Length;
        avgPos.y /= pellets.Length;

        Vector3 inkyPos = currentPosition();
        Vector3 pacmanPos = getPacmanPosition();

        Vector3 chosenPos;

        print(Vector3.Distance(pacmanPos, avgPos));
        print(pacmanPos);
        print(avgPos);

        if (Vector3.Distance(pacmanPos, avgPos) < 15)
            chosenPos = pacmanPos;
        else
            chosenPos = avgPos;

        //Get the available directions in this intersection
        List<Vector2> dirs = getAvailableDirections(node);
        Vector2 closest_dir = dirs[0];
        float min_dist = int.MaxValue;
        float dist;
        // Find the best direction to go to the average position or after pacman if he is 
        foreach (Vector2 dir in dirs)
        {
            // Skip the direction Blinky came from
            if (dir == -currentDirection())
                continue;

            Vector3 futurePos = new(inkyPos.x + dir.x, inkyPos.y + dir.y, 0);
            futurePos.x = inkyPos.x + dir.x;
            futurePos.y = inkyPos.y + dir.y;
            futurePos.z = 0;
            dist = Vector3.Distance(futurePos, chosenPos);

            // If the distance to the average positiong when the current direction is followed
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
