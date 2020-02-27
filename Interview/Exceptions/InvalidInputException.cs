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
    /// <summary>InvalidInputException class</summary>
    public class InvalidInputException : Exception
    {
        /// <summary>Initializes a new instance of the InvalidInputException class</summary>
        /// <param name="message">Prompt message that the user will see</param>        
        public InvalidInputException(string message)
        {
            Console.Write(message);
        }
    }
}