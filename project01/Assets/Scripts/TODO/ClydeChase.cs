using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeChase : GhostChase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Clyde's chase behavior
        // The direction that will maximize the distance to nearest ghost will be selected

        // instantiating the intersection node object
        Node node = other.GetComponent<Node>();

        // First check if this behaviour is enabled
        // and the ghost is not frightened
        if (!(node != null && isChasing() && !isFrightened()))
            return;

        //Get the available directions in this intersection
        List<Vector2> dirs = getAvailableDirections(node);

        // Get the position of the closest ghost
        Vector3 closestGhostPos = getClosestGhostPosition();
        // Get Clyde's position
        Vector3 clydePos = currentPosition();

        Vector2 furthest_dir = dirs[0];
        float max_dist = int.MinValue;
        float dist;
        // Find the best direction to go to the target destination
        foreach (Vector2 dir in dirs)
        {
            // Skip the direction Clyde came from
            if (dir == -currentDirection())
                continue;

            Vector3 futurePos = new(clydePos.x + dir.x, clydePos.y + dir.y, 0);
            futurePos.x = clydePos.x + dir.x;
            futurePos.y = clydePos.y + dir.y;
            futurePos.z = 0;
            dist = Vector3.Distance(futurePos, closestGhostPos);

            // If the distance to the target location when the current direction is followed
            // is greater than the maximum distance yet, update it with the current values
            if (dist > max_dist)
            {
                max_dist = dist;
                furthest_dir = dir;
            }
        }
        setDirection(furthest_dir);
    }
}
