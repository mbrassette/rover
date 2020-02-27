// ======================================================================================
// Author: Michael Brassette
// Date: Tuesday, February 25th, 2020
// Notes: I am adding two assumptions that were not mentioned in the word document.
//        1) The rover's movement plan cannot have it go off the graph.  In that case,
//           that move action is discarded and the next action in the movement plan
//           is executed.
//        2) Only one rover can occupy a coordinate in the graph at one time.  If 
//           a rover tries to move where another rover is currently occupying, the move
//           action is discarded and the next action in the movement plan is executed.
// ======================================================================================

using System;

namespace SkyKick.Interview
{
    /// <summary>FirstEntryNotValidMovementPlan class</summary>
    public class FirstEntryNotValidMovementPlan : Exception
    {
        /// <summary>Initializes a new instance of the FirstEntryNotValidMovementPlan class</summary>
        /// <param name="roverCount">The rover number that is currently being reported in the input prompt</param>
        public FirstEntryNotValidMovementPlan(int roverCount)
        {
            Console.Write("Rover {0}'s X movement plan is not valid.  It has to consist of L, R and M characters.  Enter Rover {0} Movement Plan (type Exit to quit anytime): ", roverCount);
        }
    }
}