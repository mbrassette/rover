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
    /// <summary>RoverExistsOnXYCoordinateException class</summary>
    public class RoverExistsOnXYCoordinateException : Exception
    {
        /// <summary>Initializes a new instance of the RoverExistsOnXYCoordinateException class</summary>
        /// <param name="roverCount">The rover number that is currently being reported in the input prompt</param>
        /// <param name="xCoordinate">X coordinate position being reported in the message</param>
        /// <param name="yCoordinate">Y coordinate position being reported in the message</param>
        public RoverExistsOnXYCoordinateException(int roverCount, int xCoordinate, int yCoordinate)
        {
            Console.Write("Another rover already exists at coordinate ({1}, {2}).  Enter Rover {0} Starting Position (type Exit to quit anytime): ", roverCount, xCoordinate, yCoordinate);
        }
    }
}