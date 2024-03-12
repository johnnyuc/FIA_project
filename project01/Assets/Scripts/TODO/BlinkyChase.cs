using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyChase : GhostChase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Blinky's chase behavior
        // The direction that will minimize the distance to Pacman will be selected

        // Instantiating the intersection node object
        Node node = other.GetComponent<Node>();

        // First check if this behaviour is enabled
        // and the ghost is not frightened
        if (!(node != null && isChasing() && !isFrightened()))
            return;
        
        // Get the available directions in this intersection
        List<Vector2> dirs = getAvailableDirections(node);

        // Get Pacman's position
        Vector3 pacmanPos = getPacmanPosition();
        // Get Blinky's position
        Vector3 blinkyPos = currentPosition();

        Vector2 closest_dir = dirs[0];
        float min_dist = int.MaxValue;
        float dist;
        // Find the best direction to go to Pacman's current position
        foreach (Vector2 dir in dirs) 
        {
            // Skip the direction Blinky came from
            if (dir == -currentDirection())          
                continue;
                
            Vector3 futurePos = new(blinkyPos.x + dir.x, blinkyPos.y + dir.y, 0) ;
            futurePos.x = blinkyPos.x + dir.x;
            futurePos.y = blinkyPos.y + dir.y;
            futurePos.z = 0;
            dist = Vector3.Distance(futurePos, pacmanPos);
                    
            // If the distance to Pacman when the current direction is followed
            // is less than the minimum distance yet, update it with the current values
            if (dist < min_dist) {
                min_dist = dist;
                closest_dir = dir;
            }
        }
        setDirection(closest_dir);
    }
}
